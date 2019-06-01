using Abide.Decompiler;
using Abide.Guerilla.Library;
using Abide.Guerilla.Wpf.Dialogs;
using Abide.Guerilla.Wpf.ViewModel;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Abide.Guerilla.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel Model
        {
            get { if (DataContext is MainViewModel mainView) return mainView; return null; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                ConfigWindow configWindow = new ConfigWindow() { Owner = this };
                configWindow.ShowDialog();
            }

            //Save
            Properties.Settings.Default.Save();
        }

        private void configureMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow configWindow = new ConfigWindow() { Owner = this };
            configWindow.ShowDialog();
        }

        private void CompilerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Create
            CompileDialog compileDialog = new CompileDialog();

            //Show
            compileDialog.ShowDialog();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Exit
            Application.Current.Shutdown();
        }

        private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DataContext is MainViewModel model)
            {
                //Create
                OpenFileDialog openDlg = new OpenFileDialog
                {
                    InitialDirectory = RegistrySettings.WorkspaceDirectory,
                    Filter = "All Files (*.*)|*.*"
                };

                //Add custom place
                openDlg.CustomPlaces.Add(new FileDialogCustomPlace(Path.Combine(RegistrySettings.WorkspaceDirectory, "tags")));

                //Show
                if (openDlg.ShowDialog() ?? false)
                    if (!model.Files.Any(f => f.FileName == openDlg.FileName))
                    {
                        //try
                        {
                            //Load
                            AbideTagGroupFile tagGroupFile = new AbideTagGroupFile();
                            using (var stream = openDlg.OpenFile())
                                tagGroupFile.Load(stream);

                            //Create
                            TagFileModel tagGroupFileViewModel = new TagFileModel(openDlg.FileName, tagGroupFile);
                            tagGroupFileViewModel.OpenTagReferenceRequested += TagGroupFileViewModel_OpenTagReferenceRequested;
                            model.Files.Add(tagGroupFileViewModel);

                            //Set
                            model.SelectedFile = tagGroupFileViewModel;
                        }
                        //catch (Exception ex) { MessageBox.Show("Unable to open the specified file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); throw ex; }
                    }
            }
        }

        private void TagGroupFileViewModel_OpenTagReferenceRequested(object sender, TagReferenceEventArgs e)
        {
            //Get file name
            string fileName = Path.Combine(RegistrySettings.WorkspaceDirectory, "tags", e.TagReference);

            //Check
            if (DataContext is MainViewModel model)
            {
                try
                {
                    //Load
                    AbideTagGroupFile tagGroupFile = new AbideTagGroupFile();
                    using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        tagGroupFile.Load(stream);

                    //Create
                    TagFileModel tagGroupFileViewModel = new TagFileModel(fileName, tagGroupFile);
                    tagGroupFileViewModel.OpenTagReferenceRequested += TagGroupFileViewModel_OpenTagReferenceRequested;
                    model.Files.Add(tagGroupFileViewModel);

                    //Set
                    model.SelectedFile = tagGroupFileViewModel;
                }
                catch { }
            }
        }

        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //Can execute
            e.CanExecute = Model.SelectedFile != null && Model.SelectedFile.IsDirty;
        }

        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Check
            if (Model.SelectedFile != null)
            {
                try { Model.SelectedFile.SaveToFile(); }
                catch(IOException)
                {
                    //Initialize
                    SaveFileDialog saveDlg = new SaveFileDialog()
                    {
                        Filter = Model.SelectedFile.FileFilter,
                        FileName = Model.SelectedFile.DisplayName
                    };

                    //Show
                    if (saveDlg.ShowDialog() ?? false)
                    {
                        Model.SelectedFile.FileName = saveDlg.FileName;
                        Model.SelectedFile.SaveToFile();
                    }
                }
            }
        }

        private void SaveAsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Check
            if (Model.SelectedFile != null)
            {
                //Initialize
                SaveFileDialog saveDlg = new SaveFileDialog()
                {
                    Filter = Model.SelectedFile.FileFilter,
                    FileName = Model.SelectedFile.DisplayName
                };

                //Show
                if(saveDlg.ShowDialog() ?? false)
                {
                    Model.SelectedFile.FileName = saveDlg.FileName;
                    Model.SelectedFile.SaveToFile();
                }
            }
        }

        private void CloseCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //Check
            e.CanExecute = mainTabControl.HasItems && mainTabControl.TabIndex >= 0;
        }

        private void CloseCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Prepare
            bool remove = true;

            //Check
            if (mainTabControl.SelectedItem is FileModel file && DataContext is MainViewModel modal)
            {
                //Check
                if (file.IsDirty)
                {
                    //Get result
                    MessageBoxResult result = MessageBox.Show($"Save changes to {file.DisplayName}?", "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

                    //Handle
                    switch (result)
                    {
                        case MessageBoxResult.Cancel:
                            remove = false;
                            break;
                        case MessageBoxResult.Yes:
                            file.SaveToFile();
                            break;
                    }
                }

                //Remove
                if (remove) modal.Files.Remove(file);
            }
        }

        private void CloseAllCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Prepare
            bool dirty = false;
            bool remove = true;
            bool save = false;

            //Check
            if (DataContext is MainViewModel modal)
            {
                //Check dirty
                foreach (var file in modal.Files)
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
                    for (int i = modal.Files.Count - 1; i >= 0; i--)
                    {
                        //Save?
                        if (save && modal.Files[i].IsDirty)
                            modal.Files[i].SaveToFile();

                        //Close
                        modal.Files.RemoveAt(i);
                    }
                }
            }
        }

        private void CloseTabButton_Click(object sender, RoutedEventArgs e)
        {
            //Prepare
            bool remove = true;

            //Check
            if (sender is FrameworkElement element && element.DataContext is FileModel file && DataContext is MainViewModel model)
            {
                //Check
                if (file.IsDirty)
                {
                    //Get result
                    MessageBoxResult result = MessageBox.Show($"Save changes to {file.DisplayName}?", "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

                    //Handle
                    switch (result)
                    {
                        case MessageBoxResult.Cancel:
                            remove = false;
                            break;
                        case MessageBoxResult.Yes:
                            file.SaveToFile();
                            break;
                    }
                }

                //Remove
                if (remove) model.Files.Remove(file);
            }
        }

        private void DecompilerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Initialize
            using (CacheDecompiler decompiler = new CacheDecompiler())
                decompiler.ShowDialog();
        }
    }

    public static class CustomCommands
    {
        public static readonly RoutedUICommand CloseAll = new RoutedUICommand("Close All", "Close All", typeof(CustomCommands));
    }

}
