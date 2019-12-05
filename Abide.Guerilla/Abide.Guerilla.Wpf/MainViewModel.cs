using Abide.Decompiler;
using Abide.Guerilla.Library;
using Abide.Guerilla.Wpf.Dialogs;
using Abide.Guerilla.Wpf.ViewModel;
using Abide.HaloLibrary.Halo2Map;
using Abide.Tag;
using Abide.Tag.Guerilla.Generated;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using WinForms = System.Windows.Forms;

namespace Abide.Guerilla.Wpf
{
    /// <summary>
    /// Represents the main view container.
    /// </summary>
    public sealed class MainViewModel : NotifyPropertyChangedViewModel, IDecompileHost
    {
        /// <summary>
        /// Gets or sets the window that owns this view model.
        /// </summary>
        public Window Owner { get; set; }
        /// <summary>
        /// Gets or sets the window title
        /// </summary>
        public string WindowTitle
        {
            get
            {
                if (selectedFile == null) return windowTitle;
                else return $"{windowTitle} - {selectedFile.DisplayName}";
            }
            set
            {
                if(!windowTitle.Equals(value))
                {
                    windowTitle = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Gets or sets the status of the program.
        /// </summary>
        public string Status
        {
            get { return status; }
            set
            {
                if(status!= value)
                {
                    status = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Gets or sets the visibility of the progress bar.
        /// </summary>
        public bool ShowProgressBar
        {
            get { return showProgressBar; }
            set
            {
                if(showProgressBar != value)
                {
                    showProgressBar = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Gets or sets the progress of the progress bar.
        /// </summary>
        public double Progress
        {
            get { return progress; }
            set
            {
                if(progress!= value)
                {
                    progress = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Gets or sets the registry settings of the main view.
        /// </summary>
        public SettingsViewModel RegistrySettings { get; set; } = new SettingsViewModel();
        /// <summary>
        /// Gets or sets a list of files open in the editor.
        /// </summary>
        public ObservableCollection<FileModel> Files { get; set; } = new ObservableCollection<FileModel>();
        /// <summary>
        /// Gets or sets the selected file.
        /// </summary>
        public FileModel SelectedFile
        {
            get { return selectedFile; }
            set
            {
                if (selectedFile != value)
                {
                    selectedFile = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(WindowTitle));
                }
            }
        }
        /// <summary>
        /// Gets and returns the new file command.
        /// </summary>
        public ICommand NewFileCommand { get; }
        /// <summary>
        /// Gets and returns the open file command.
        /// </summary>
        public ICommand OpenFileCommand { get; }
        /// <summary>
        /// Gets and returns the close all command.
        /// </summary>
        public ICommand CloseAllCommand { get; }
        /// <summary>
        /// Gets and returns the save as file command.
        /// </summary>
        public ICommand SaveAsCommand { get; }
        /// <summary>
        /// Gets and returns the save all files command.
        /// </summary>
        public ICommand SaveAllCommand { get; }
        /// <summary>
        /// Gets and returns the configure command.
        /// </summary>
        public ICommand ConfigureCommand { get; }
        /// <summary>
        /// Gets and returns the exit command.
        /// </summary>
        public ICommand ExitCommand { get; }
        /// <summary>
        /// Gets and returns the compile map command.
        /// </summary>
        public ICommand CompileMapCommand { get; }
        /// <summary>
        /// Gets and returns the decompile map command.
        /// </summary>
        public ICommand DecompileMapCommand { get; }

        private FileModel selectedFile = null;
        private MapDecompiler decompiler = null;
        private string windowTitle = "Abide Guerilla";
        private string status = "Ready";
        private bool showProgressBar = false;
        private bool decompiling = false;
        private double progress = 0d;

        public MainViewModel()
        {
            //Prepare commands
            NewFileCommand = new RelayCommand(o => NewFile());
            OpenFileCommand = new RelayCommand(o => OpenFile());
            CloseAllCommand = new RelayCommand(o => CloseAllFiles(), o => Files.Count > 0);
            SaveAsCommand = new RelayCommand(o => SaveFileAs(), o => SelectedFile != null);
            SaveAllCommand = new RelayCommand(o => SaveAllFiles(), o => Files.Count > 0);
            ExitCommand = new RelayCommand(o => Exit());
            ConfigureCommand = new RelayCommand(o => Configure());
            CompileMapCommand = new RelayCommand(o => Compile());
            DecompileMapCommand = new RelayCommand(o => Decompile(), o => !decompiling);

            //Check
            if (string.IsNullOrEmpty(RegistrySettings.WorkspaceDirectory))
                RegistrySettings.WorkspaceDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "Abide", "Guerilla");
            
            //Upgrade
            if (Properties.Settings.Default.UpgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeRequired = false;
            }

            //Check
            if (Properties.Settings.Default.FirstRun)
            {
                Properties.Settings.Default.FirstRun = false;
                ConfigWindow configWindow = new ConfigWindow();
                configWindow.ShowDialog();
            }
            
            //Check workspace
            if (!Directory.Exists(RegistrySettings.WorkspaceDirectory))
                Directory.CreateDirectory(RegistrySettings.WorkspaceDirectory);

            //Check maps
            string mapsDirectory = Path.Combine(RegistrySettings.WorkspaceDirectory, "maps");
            if (!Directory.Exists(mapsDirectory))
                Directory.CreateDirectory(mapsDirectory);

            //Check tags
            string tagsDirectory = Path.Combine(RegistrySettings.WorkspaceDirectory, "maps");
            if (!Directory.Exists(tagsDirectory))
                Directory.CreateDirectory(tagsDirectory);

            //Save
            Properties.Settings.Default.Save();
        }

        private void NewFile()
        {
            //Create and show
            NewTagViewModel newTagViewModel = new NewTagViewModel();
            NewFileDialog newFileDialog = new NewFileDialog() { Owner = Owner, DataContext = newTagViewModel };
            if (newFileDialog.ShowDialog() ?? false)
            {
                //Get tag group
                ITagGroup tagGroup = TagLookup.CreateTagGroup(newTagViewModel.SelectedTagDefinition.GroupTag);

                //Create file
                TagFileModel newTagFileModel = new TagFileModel($"new_{tagGroup.Name}.{tagGroup.Name}", new AbideTagGroupFile()
                { TagGroup = tagGroup })
                { IsDirty = true, CloseCallback = File_Close };
                newTagFileModel.OpenTagReferenceRequested += OpenTagReferenceRequested;
                Files.Add(newTagFileModel);

                //Set
                SelectedFile = newTagFileModel;
            }
        }

        private void OpenFile()
        {
            //Prepare
            string tagsDirectory = Path.Combine(RegistrySettings.WorkspaceDirectory, "tags");

            //Create
            OpenFileDialog openDlg = new OpenFileDialog
            {
                InitialDirectory = tagsDirectory,
                Filter = "All Files (*.*)|*.*"
            };

            //Add custom place
            openDlg.CustomPlaces.Add(new FileDialogCustomPlace(tagsDirectory));

            //Show
            if (openDlg.ShowDialog() ?? false)
                if (!Files.Any(f => f.FileName == openDlg.FileName))
                    try
                    {
                        //Load
                        AbideTagGroupFile tagGroupFile = new AbideTagGroupFile();
                        using (var stream = openDlg.OpenFile())
                            tagGroupFile.Load(stream);

                        //Create
                        TagFileModel tagGroupFileViewModel = new TagFileModel(openDlg.FileName, tagGroupFile);
                        tagGroupFileViewModel.CloseCallback = File_Close;
                        tagGroupFileViewModel.OpenTagReferenceRequested += OpenTagReferenceRequested;
                        Files.Add(tagGroupFileViewModel);

                        //Set
                        SelectedFile = tagGroupFileViewModel;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to open the specified file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        throw ex;
                    }
        }

        private void CloseAllFiles()
        {
            //Prepare
            bool dirty = false;
            bool remove = true;
            bool save = false;

            //Check dirty
            foreach (var file in Files)
                dirty |= file.IsDirty;

            //Check
            if (dirty)
            {
                //Get result
                MessageBoxResult result = MessageBox.Show($"Save changes to modified files?", "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

                //Handle
                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        remove = false;
                        break;
                    case MessageBoxResult.Yes:
                        save = true;
                        break;
                }
            }

            //Loop through files
            if (remove)
            {
                for (int i = Files.Count - 1; i >= 0; i--)
                {
                    //Save?
                    if (save && Files[i].IsDirty)
                        Files[i].SaveToFile();

                    //Close
                    Files.RemoveAt(i);
                }
            }
        }

        private void SaveFileAs()
        {
            //Initialize
            SaveFileDialog saveDlg = new SaveFileDialog()
            {
                Filter = SelectedFile.FileFilter,
                FileName = SelectedFile.DisplayName
            };

            //Show
            if (saveDlg.ShowDialog() ?? false)
            {
                SelectedFile.FileName = saveDlg.FileName;
                SelectedFile.SaveToFile();
            }
        }

        private void SaveAllFiles()
        {
            //Loop through files
            foreach (FileModel file in Files)
                file.SaveToFile();  //Save
        }

        private void Exit()
        {
            //Close all files
            CloseAllFiles();

            //Exit
            Application.Current.Shutdown();
        }

        private void Configure()
        {
            //Create and show
            ConfigWindow configWindow = new ConfigWindow() { DataContext = RegistrySettings, Owner = Owner };
            configWindow.ShowDialog();
        }

        private void Decompile()
        {
            //Initialize
            OpenFileDialog openDlg = new OpenFileDialog() { Filter = "Halo Map Files (*.map)|*.map" };

            //Show
            if (openDlg.ShowDialog() ?? false)
            {
                //Prepare
                decompiling = true;
                string tagsDirectory = Path.Combine(RegistrySettings.WorkspaceDirectory, "tags");
                decompiler = new MapDecompiler(openDlg.FileName, tagsDirectory) { Host = this };
                Status = $"Decompiling {decompiler.Map.Name}...";
                ShowProgressBar = true;
                Progress = 0d;

                //Start
                decompiler.Start();
            }
        }

        private void Compile()
        {
            //Create
            MapCompilerModel compilerModel = new MapCompilerModel();

            //Check
            if (Files.Any(f => (f is TagFileModel model) && model.GroupTag == HaloTags.scnr))
                compilerModel.ScenarioFileName = (Files.First(f => (f is TagFileModel model) && model.GroupTag == HaloTags.scnr)).FileName;

            //Create and show
            CompileDialog compileDialog = new CompileDialog() { Owner = Owner, DataContext = compilerModel };
            compileDialog.ShowDialog();
        }

        private void File_Close(FileModel fileModel)
        {
            //Remove
            Files.Remove(fileModel);
        }

        private void OpenTagReferenceRequested(object sender, TagReferenceEventArgs e)
        {
            //Get file name
            string fileName = Path.Combine(RegistrySettings.WorkspaceDirectory, "tags", e.TagReference);

            try
            {
                //Load
                AbideTagGroupFile tagGroupFile = new AbideTagGroupFile();
                using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    tagGroupFile.Load(stream);

                //Create
                TagFileModel tagGroupFileViewModel = new TagFileModel(fileName, tagGroupFile);
                tagGroupFileViewModel.CloseCallback = File_Close;
                tagGroupFileViewModel.OpenTagReferenceRequested += OpenTagReferenceRequested;
                Files.Add(tagGroupFileViewModel);

                //Set
                SelectedFile = tagGroupFileViewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the specified file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw ex;
            }
        }

        void IDecompileHost.Complete()
        {
            //Set status
            Status = $"{decompiler.Map.Name} Decompiled successfully";
            ShowProgressBar = false;
            Progress = 0d;

            //Dispose
            decompiling = false;
            decompiler.Dispose();
            decompiler = null;
        }

        void IDecompileHost.Fail()
        {
            //Set status
            Status = $"{decompiler.Map.Name} failed to decompile";
            ShowProgressBar = false;
            Progress = 0d;

            //Dispose
            decompiling = false;
            decompiler.Dispose();
            decompiler = null;
        }

        void IProgress<float>.Report(float value)
        {
            //Set progress
            Progress = value * 100d;
        }
    }

    /// <summary>
    /// Represents a settings container.
    /// </summary>
    public class SettingsViewModel : NotifyPropertyChangedViewModel
    {
        /// <summary>
        /// Gets or sets the workspace directory.
        /// </summary>
        public string WorkspaceDirectory
        {
            get { return RegistrySettings.WorkspaceDirectory; }
            set
            {
                value = value ?? string.Empty;
                bool changed = RegistrySettings.WorkspaceDirectory != value;
                RegistrySettings.WorkspaceDirectory = value;
                if (changed) NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the tags directory.
        /// </summary>
        public string TagsDirectory
        {
            get { return RegistrySettings.TagsDirectory; }
            set
            {
                value = value ?? string.Empty;
                bool changed = RegistrySettings.WorkspaceDirectory != value;
                RegistrySettings.TagsDirectory = value;
                if (changed) NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the mainmenu file name.
        /// </summary>
        public string MainmenuFileName
        {
            get { return RegistrySettings.MainmenuFileName; }
            set
            {
                value = value ?? string.Empty;
                bool changed = RegistrySettings.WorkspaceDirectory != value;
                RegistrySettings.MainmenuFileName = value;
                if (changed) NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the shared file name.
        /// </summary>
        public string SharedFileName
        {
            get { return RegistrySettings.SharedFileName; }
            set
            {
                value = value ?? string.Empty;
                bool changed = RegistrySettings.WorkspaceDirectory != value;
                RegistrySettings.SharedFileName = value;
                if (changed) NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the single player shared file name.
        /// </summary>
        public string SingleplayerSharedFileName
        {
            get { return RegistrySettings.SinglePlayerSharedFileName; }
            set
            {
                value = value ?? string.Empty;
                bool changed = RegistrySettings.WorkspaceDirectory != value;
                RegistrySettings.SinglePlayerSharedFileName = value;
                if (changed) NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Gets and returns the browse workspace directory command.
        /// </summary>
        public ICommand BrowseWorkspaceCommand { get; }
        /// <summary>
        /// Gets and returns the browse shared map file command.
        /// </summary>
        public ICommand BrowseSharedMapCommand { get; }
        /// <summary>
        /// Gets and returns the browse mainmenu map file command.
        /// </summary>
        public ICommand BrowseMainmenuMapCommand { get; }
        /// <summary>
        /// Gets and returns the browse singleplayer shared map file command.
        /// </summary>
        public ICommand BrowseSpSharedMapCommand { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        public SettingsViewModel()
        {
            BrowseWorkspaceCommand = new RelayCommand(BrowseWorkspaceDirectory);
            BrowseSharedMapCommand = new RelayCommand(BrowseSharedMapFile);
            BrowseMainmenuMapCommand = new RelayCommand(BrowseMainmenuMapFile);
            BrowseSpSharedMapCommand = new RelayCommand(BrowseSingleplayerSharedMapFile);
        }

        private void BrowseWorkspaceDirectory(object obj)
        {
            //Create
            using (WinForms.FolderBrowserDialog folderDlg = new WinForms.FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Select Abide Guerilla workspace directory.";
                folderDlg.SelectedPath = WorkspaceDirectory;

                //Show
                if (folderDlg.ShowDialog() == WinForms.DialogResult.OK)
                    WorkspaceDirectory = folderDlg.SelectedPath;
            }
        }
        private void BrowseSharedMapFile(object obj)
        {
            //Create
            OpenFileDialog openDlg = new OpenFileDialog
            {
                Title = "Browse Shared map file...",
                Filter = "Halo Map Files (*.map)|*.map",
                FileName = SharedFileName,
            };

            if (File.Exists(SharedFileName))
                openDlg.InitialDirectory = Path.GetDirectoryName(SharedFileName);

            if (openDlg.ShowDialog() ?? false)
            {
                //Set shared
                SharedFileName = openDlg.FileName;
                DiscoverResourceMaps(openDlg.FileName);
            }
        }
        private void BrowseMainmenuMapFile(object obj)
        {
            //Create
            OpenFileDialog openDlg = new OpenFileDialog
            {
                Title = "Browse Mainmenu map file...",
                Filter = "Halo Map Files (*.map)|*.map",
                FileName = MainmenuFileName,
            };

            if (File.Exists(MainmenuFileName))
                openDlg.InitialDirectory = Path.GetDirectoryName(MainmenuFileName);

            if (openDlg.ShowDialog() ?? false)
            {
                MainmenuFileName = openDlg.FileName;
                DiscoverResourceMaps(openDlg.FileName);
            }
        }
        private void BrowseSingleplayerSharedMapFile(object obj)
        {
            //Create
            OpenFileDialog openDlg = new OpenFileDialog
            {
                Title = "Browse Singleplayer Shared map file...",
                Filter = "Halo Map Files (*.map)|*.map",
                FileName = SingleplayerSharedFileName,
            };

            if (File.Exists(SingleplayerSharedFileName))
                openDlg.InitialDirectory = Path.GetDirectoryName(SingleplayerSharedFileName);

            if (openDlg.ShowDialog() ?? false)
            {
                SingleplayerSharedFileName = openDlg.FileName;
                DiscoverResourceMaps(openDlg.FileName);
            }
        }
        private void DiscoverResourceMaps(string resourceMapFileName)
        {
            // This method is a little sloppy. It works but could probably be improved.

            //Prepare
            List<string> resourceMapList = new List<string>();
            StringBuilder msgBuilder = new StringBuilder("The following resource maps were also detected in the same directory:\r\n");
            string[] resourceMapNames = { "mainmenu.map", "single_player_shared.map", "shared.map" };

            //Get directory
            string directory = Path.GetDirectoryName(resourceMapFileName);
            string currentResourceMap = Path.GetFileName(resourceMapFileName);

            //Loop
            foreach (string resourceMapName in resourceMapNames)
                if (resourceMapName != currentResourceMap && File.Exists(Path.Combine(directory, resourceMapName)))
                    resourceMapList.Add(Path.Combine(directory, resourceMapName));

            //Check
            if (resourceMapList.Count > 0)
            {
                //Loop
                foreach (string mapFileName in resourceMapList)
                    msgBuilder.AppendLine(Path.GetFileName(mapFileName));

                //Add final line
                msgBuilder.AppendLine("Would you like to set these maps as well?");

                //Show
                var result = MessageBox.Show(msgBuilder.ToString(), "Additional Maps found", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var mapFileName in resourceMapList)
                        switch (Path.GetFileName(mapFileName))
                        {
                            case "mainmenu.map": MainmenuFileName = mapFileName; break;
                            case "single_player_shared.map": SingleplayerSharedFileName = mapFileName; break;
                            case "shared.map": SharedFileName = mapFileName; break;
                        }
                }
            }
        }
    }

    /// <summary>
    /// Represents a view model that can notify when a propery has been changed.
    /// </summary>
    public class NotifyPropertyChangedViewModel : DependencyObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Pushes a notification that the specified property has been changed.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            //Prepare
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);

            //Call OnPropertyChanged
            OnPropertyChanged(e);

            //Raise event
            PropertyChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Occurs when a property has been changed.
        /// </summary>
        /// <param name="e">A <see cref="PropertyChangedEventArgs"/> that contains the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            //Do nothing
        }
    }

    /// <summary>
    /// Represents a basic command implementation.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Occurs when the ability to execute changes.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class using the specified execute method.
        /// </summary>
        /// <param name="execute">The action to be performed.</param>
        public RelayCommand(Action<object> execute) : this(execute, null) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class using the specified execute method and can execute function.
        /// </summary>
        /// <param name="execute">The method to be performed when the command is executed.</param>
        /// <param name="canExecute">The function that determines whether the command can be executed.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Returns a value that determines whether the command can be executed.
        /// </summary>
        /// <param name="param">The optional parameter.</param>
        /// <returns><see langword="true"/> if the command can be executed; otherwise, <see langword="false"/>.</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object param)
        {
            return canExecute == null ? true : canExecute(param);
        }
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="param">The optional parameter.</param>
        [DebuggerStepThrough]
        public void Execute(object param)
        {
            execute(param);
        }
    }

    /// <summary>
    /// Represents a method that contains data about a tag reference.
    /// </summary>
    /// <param name="sender">The object that calls the method.</param>
    /// <param name="e">The event data.</param>
    public delegate void TagReferenceEventHandler(object sender, TagReferenceEventArgs e);

    /// <summary>
    /// Represents an 
    /// </summary>
    public class TagReferenceEventArgs : EventArgs
    {
        /// <summary>
        /// Gets and returns the tag reference as a string.
        /// </summary>
        public string TagReference { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TagReferenceEventArgs"/> class using the specified tag reference.
        /// </summary>
        /// <param name="tagReference">The tag reference string.</param>
        public TagReferenceEventArgs(string tagReference)
        {
            TagReference = tagReference;
        }
    }
}
