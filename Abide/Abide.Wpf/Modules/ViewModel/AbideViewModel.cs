using Abide.AddOnApi;
using Abide.AddOnApi.Wpf;
using Abide.DebugXbox;
using Abide.Wpf.Modules.AddOns;
using Abide.Wpf.Modules.Dialogs;
using Abide.Wpf.Modules.Operations;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Abide.Wpf.Modules.ViewModel
{
    public sealed class AbideViewModel : DependencyObject, IHost
    {
        private const string DefaultWindowTitle = "Abide";

        private static readonly DependencyPropertyKey ProgressBarPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ProgressBar), typeof(ProgressBarReporter), typeof(AbideViewModel), new PropertyMetadata(new ProgressBarReporter()));
        private static readonly DependencyPropertyKey FactoryPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Factory), typeof(EditorAddOnFactory), typeof(AbideViewModel), new PropertyMetadata(null));

        public static readonly DependencyProperty WindowProperty =
            DependencyProperty.Register(nameof(MainWindow), typeof(Window), typeof(AbideViewModel));
        public static readonly DependencyProperty WindowTitleProperty =
            DependencyProperty.Register(nameof(WindowTitle), typeof(string), typeof(AbideViewModel), new PropertyMetadata(DefaultWindowTitle));
        public static readonly DependencyProperty PrimaryOperationProperty =
            DependencyProperty.Register(nameof(PrimaryOperation), typeof(BackgroundOperation), typeof(AbideViewModel), new PropertyMetadata(PrimaryOperationChanged));
        public static readonly DependencyProperty SelectedFileProperty =
            DependencyProperty.Register(nameof(SelectedFile), typeof(FileItem), typeof(AbideViewModel), new PropertyMetadata(SelectedFilePropretyChanged));
        public static readonly DependencyProperty SelectedXboxProperty =
            DependencyProperty.Register(nameof(SelectedXbox), typeof(XboxViewModel), typeof(AbideViewModel), new PropertyMetadata(SelectedXboxPropertyChanged));

        public static readonly DependencyProperty CurrentSolutionProperty =
            DependencyProperty.Register(nameof(CurrentSolution), typeof(SolutionViewModel), typeof(AbideViewModel), new PropertyMetadata(CurrentSolutionPropertyChanged));
        public static readonly DependencyProperty FactoryProperty =
            FactoryPropertyKey.DependencyProperty;

        public ObservableCollection<BackgroundOperation> BackgroundOperations { get; } = new ObservableCollection<BackgroundOperation>();
        public ObservableCollection<XboxViewModel> Xboxes { get; } = new ObservableCollection<XboxViewModel>();
        public FileCollection Files { get; } = new FileCollection();
        public Window MainWindow
        {
            get => (Window)GetValue(WindowProperty);
            set => SetValue(WindowProperty, value);
        }
        public string WindowTitle
        {
            get => (string)GetValue(WindowTitleProperty);
            set => SetValue(WindowTitleProperty, value);
        }
        public ProgressBarReporter ProgressBar
        {
            get => (ProgressBarReporter)GetValue(ProgressBarPropertyKey.DependencyProperty);
            private set => SetValue(ProgressBarPropertyKey, value);
        }
        public SolutionViewModel CurrentSolution
        {
            get => (SolutionViewModel)GetValue(CurrentSolutionProperty);
            set => SetValue(CurrentSolutionProperty, value);
        }
        public BackgroundOperation PrimaryOperation
        {
            get => (BackgroundOperation)GetValue(PrimaryOperationProperty);
            set => SetValue(PrimaryOperationProperty, value);
        }
        public XboxViewModel SelectedXbox
        {
            get => (XboxViewModel)GetValue(SelectedXboxProperty);
            set => SetValue(SelectedXboxProperty, value);
        }
        public FileItem SelectedFile
        {
            get => (FileItem)GetValue(SelectedFileProperty);
            set => SetValue(SelectedFileProperty, value);
        }
        public EditorAddOnFactory Factory
        {
            get => (EditorAddOnFactory)GetValue(FactoryProperty);
            private set => SetValue(FactoryPropertyKey, value);
        }
        public ICommand NewProjectCommand { get; }
        public ICommand NewCommand { get; }
        public ICommand OpenProjectCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand DecompileMapCommand { get; }
        public ICommand CompileMapCommand { get; }
        public ICommand RefreshXboxesCommand { get; }
        public AbideViewModel()
        {
            ApplicationSettings.GlobalState = this; //declare singleton
            Factory = new EditorAddOnFactory(this);
            Factory.InitializeAddOns();

            NewProjectCommand = new ActionCommand(o => { NewProject(); });
            OpenProjectCommand = new ActionCommand(OpenProject);
            NewCommand = new ActionCommand(NewFile);
            OpenCommand = new ActionCommand(OpenFile);
            DecompileMapCommand = new ActionCommand(DecompileMap);
            CompileMapCommand = new ActionCommand(CompileMap);
            RefreshXboxesCommand = new ActionCommand(RefreshXboxes);
            NameAnsweringProtocol.DiscoverAsync().ContinueWith(DiscoverContinue);

            ApplicationSettings.FilePaths.ForEach(p => OpenFile(p));
#if DEBUG
            Debug();
#endif
        }
        private void Debug()
        {
            // using (var map = new HaloLibrary.Halo2.Retail.HaloMap(@"F:\XBox\Original\Games\Halo 2\Clean Maps\ascension.map"))
            // using (var writer = File.CreateText(@"F:\map_info.txt"))
            // {
            //     writer.Write($"Map Name: ");
            //     writer.WriteLine(map.Name);
            //     writer.Write($"Entry Count: ");
            //     writer.WriteLine(map.IndexEntries.Count);
            // 
            //     for (int i = 0; i < map.IndexEntries.Count; i++)
            //     {
            //         writer.Write("0x");
            //         writer.Write(map.IndexEntries[i].Id.ToString());
            //         writer.Write(" ");
            //         writer.Write(map.IndexEntries[i].Filename);
            //         writer.Write(".");
            //         writer.WriteLine(map.IndexEntries[i].Root);
            //     }
            // }

            OpenFile(@"F:\Users\Mike\Documents\Abide\Guerilla\tags\objects\weapons\rifle\battle_rifle\battle_rifle.weapon");
            OpenFile(@"F:\Users\Mike\Documents\Abide\Guerilla\tags\scenarios\multi\ascension\ascension.scenario");
            OpenFile(@"F:\Users\Mike\Documents\Abide\Guerilla\tags\ui\hud\masterchief.new_hud_definition");
        }
        private void NewProject(NewProjectDialog dialog = null)
        {
            if (dialog == null)
            {
                dialog = new NewProjectDialog
                {
                    Location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Abide", "Projects"),
                    SolutionName = "Empty Solution"
                };
            }

            if (dialog.ShowDialog() ?? false)
            {
                CreateProject(dialog);
            }
        }
        private void CreateProject(NewProjectDialog dialog)
        {
            if (!Directory.Exists(dialog.Location))
            {
                _ = Directory.CreateDirectory(dialog.Location);
            }

            string solutionDirectory = Path.Combine(dialog.Location, dialog.SolutionName);
            if (Directory.Exists(solutionDirectory))
            {
                _ = MessageBox.Show("Solution folder already exists.", "Cannot Create Solution", MessageBoxButton.OK, MessageBoxImage.Error);
                NewProject(dialog);
                return;
            }
            else
            {
                _ = Directory.CreateDirectory(solutionDirectory);
                string solutionFileName = Path.Combine(solutionDirectory, $"{dialog.SolutionName}.sln");

                using (StreamWriter writer = File.CreateText(solutionFileName))
                {
                    var assembly = typeof(AbideViewModel).Assembly;
                    var asmName = assembly.GetName();

                    writer.WriteLine("Abide Solution File, Format Version 1.0");
                    writer.WriteLine($"# {asmName.Name} 1");
                    writer.WriteLine($"AbideVersion = {asmName.Version}");

#if DEBUG
                    if (AssemblyManager.ProjectTypes.Count > 0)
                    {
                        var projectTypeAttribute = AssemblyManager.ProjectTypes[0].GetCustomAttribute<ProjectTypeAttribute>();
                        writer.WriteLine($"Project(\"{{{projectTypeAttribute.Guid}}}\") = \"{dialog.SolutionName}\", \"{dialog.SolutionName}\\{dialog.SolutionName}.{projectTypeAttribute.ProjectExtension}\", \"{Guid.NewGuid().ToString()}\"");
                        writer.WriteLine("EndProject");
                    }
#endif
                }
            }
        }
        private void RefreshXboxes(object arg)
        {
            Xboxes.Clear();
            Xboxes.Add(new XboxViewModel("Refreshing..."));
            SelectedXbox = Xboxes[0];
            NameAnsweringProtocol.DiscoverAsync().ContinueWith(DiscoverContinue);
        }
        private void OpenProject(object arg)
        {
            //Get extensions
            string extensions = "*.sln" + string.Join(";", Factory.ProjectEditors.Select(e => e.Extension).ToArray());

            //Get file filter
            StringBuilder filterBuilder = new StringBuilder();
            _ = filterBuilder.Append($"All Project Types ({extensions})|{extensions}");
            if (Factory.ProjectEditors.Count > 0)
            {
                _ = filterBuilder.Append(string.Join(";", Factory.ProjectEditors.Select(e => e.Extension).ToArray()) + "|");
                _ = filterBuilder.Append(string.Join("|", Factory.ProjectEditors.Select(e => $"{e.TypeName} ({e.Extension})|{e.Extension}").ToArray()) + "|");
            }

            //Show OpenFileDialog
            OpenFileDialog openDlg = new OpenFileDialog() { Filter = filterBuilder.ToString() };
            if (openDlg.ShowDialog() ?? false)
            {
                if (Path.GetExtension(openDlg.FileName) == ".sln")
                {
                    OpenSolution(openDlg.FileName);
                }
                else
                {
                    OpenProject(openDlg.FileName);
                }
            }
        }
        private void NewFile(object arg)
        {

        }
        private void OpenFile(object arg)
        {
            //Get extensions
            string extensions = string.Join(";", Factory.FileEditors.Select(e => e.Extension).ToArray());

            //Get file filter
            StringBuilder filterBuilder = new StringBuilder();
            if (Factory.FileEditors.Count > 0)
            {
                _ = filterBuilder.Append($"All Supported Files ({extensions})|{extensions}");
                _ = filterBuilder.Append(string.Join(";", Factory.FileEditors.Select(e => e.Extension).ToArray()) + "|");
                _ = filterBuilder.Append(string.Join("|", Factory.FileEditors.Select(e => $"{e.TypeName} ({e.Extension})|{e.Extension}").ToArray()) + "|");
            }
            _ = filterBuilder.Append("All Files (*.*)|*.*");

            //Show OpenFileDialog
            OpenFileDialog openDlg = new OpenFileDialog() { Filter = filterBuilder.ToString() };
            openDlg.CustomPlaces.Add(new FileDialogCustomPlace(Abide.Guerilla.Library.RegistrySettings.WorkspaceDirectory));
            if (openDlg.ShowDialog() ?? false)
            {
                OpenFile(openDlg.FileName);
            }
        }
        private void DecompileMap(object arg)
        {
            OpenFileDialog openDlg = new OpenFileDialog() { Filter = "Halo 2 Map Files (*.map)|*.map" };
            if (openDlg.ShowDialog() ?? false)
            {
                DecompileMapFile(openDlg.FileName);
            }
        }
        private void CompileMap(object arg)
        {
#if !DEBUG
            throw new NotImplementedException();
#endif
            OpenFileDialog openDlg = new OpenFileDialog() { Filter = "Scenario Files (*.scenario)|*.scenario" };
            if (openDlg.ShowDialog() ?? false)
            {
                CompileMap(openDlg.FileName);
            }
        }
        private void OpenProject(string path)
        {
            //Check
            if (!File.Exists(path))
            {
                return;
            }

            //Get extension
            string ext = Path.GetExtension(path);

            //Get valid types
            IProjectEditor selectedEditor = null;
            IProjectEditor[] editors = Factory.ProjectEditors.Where(e => e.IsValidEditor(path)).ToArray();
            Type[] types = editors.Select(e => e.GetType()).ToArray();

            //Ask
            if (editors.Length > 1)
            {
                throw new NotImplementedException("TODO: Mutliple editors can apply to the selected project, but the program does not " +
                    "know how to handle this. By clicking 'Continue' Abide will use the first found valid editor.");

                //TODO: Read exception above
                selectedEditor = editors[0];
            }
            else if (editors.Length == 1)
            {
                selectedEditor = editors[0];
            }

            //Check
            if (selectedEditor != null)
            {

            }
        }
        private void OpenSolution(string path)
        {
            // Check
            if (!File.Exists(path))
            {
                return;
            }

            SolutionViewModel solution = SolutionViewModel.LoadFromFile(path);

            // TODO: save current solution, ask user, etc.

            CurrentSolution = solution;
        }
        private void OpenFile(string path)
        {
            //Check
            if (!File.Exists(path))
            {
                return;
            }

            //Get valid types
            IFileEditor selectedEditor = null;
            IFileEditor[] editors = Factory.FileEditors.Where(e => e.IsValidEditor(path)).ToArray();
            Type[] types = editors.Select(e => e.GetType()).ToArray();

            if (editors.Length > 1)
            {
                ChooseEditorDialog editorDialog = new ChooseEditorDialog();
                editorDialog.Editors.Clear();

                for (int i = 0; i < types.Length; i++)
                {
                    editorDialog.Editors.Add(editors[i]);
                }

                editorDialog.SelectedEditor = editors[0];

                if (editorDialog.ShowDialog() ?? false)
                {
                    selectedEditor = editorDialog.SelectedEditor;
                }
            }
            else if (editors.Length == 1)
            {
                selectedEditor = editors[0];
            }

            if (selectedEditor != null)
            {
                IFileEditor editor = Factory.Instantiate<IFileEditor>(selectedEditor.GetType());
                FileItem file = Files.New(path, editor);

                Files.Add(file);
                SelectedFile = file;
            }
        }
        private void DecompileMapFile(string path)
        {
            //Check
            if (File.Exists(path))
            {
                if (PrimaryOperation?.IsRunning ?? false)
                {
                    return;
                }

                var operation = new HaloMapDecompileOperation(path);
                operation.Start(DecompileOperationCompleted, ProgressBar);
                PrimaryOperation = operation;
            }
        }
        private void CompileMap(string path)
        {
            if (File.Exists(path))
            {
                if (PrimaryOperation?.IsRunning ?? false)
                {
                    return;
                }

                var operation = new HaloMapCompileOperation(path);
                operation.Start(CompileOperationCompleted, ProgressBar);
                PrimaryOperation = operation;
            }
        }
        private void DecompileOperationCompleted(object state)
        {
            var time = PrimaryOperation.ElapsedTime;
            PrimaryOperation = null;
            ProgressBar.ReportStatus(string.Empty);
            ProgressBar.Report(0);
        }
        private void CompileOperationCompleted(object state)
        {
            var time = PrimaryOperation.ElapsedTime;
            PrimaryOperation = null;
            ProgressBar.ReportStatus(string.Empty);
            ProgressBar.Report(0);
        }
        private void DiscoverContinue(Task<Xbox[]> discoverTask)
        {
            Dispatcher.Invoke(() =>
            {
                Xboxes.Clear();

                if (discoverTask != null && discoverTask.Result != null)
                {
                    foreach (var xbox in discoverTask.Result)
                    {
                        Xboxes.Add(new XboxViewModel(xbox));
                    }

                    if (Xboxes.Count == 0)
                    {
                        Xboxes.Add(new XboxViewModel("No Debug Xboxes Found"));
                    }
                }

                SelectedXbox = Xboxes.FirstOrDefault();
            });
        }

        bool ISynchronizeInvoke.InvokeRequired => false;
        object IHost.Invoke(Delegate method)
        {
            //Invoke
            return method?.DynamicInvoke();
        }
        object IHost.Request(IAddOn sender, string request, params object[] args)
        {
            //Check
            if (sender == null)
            {
                return null;
            }

            if (request == null)
            {
                return null;
            }

            //Check request
            switch (request)
            {
            }

            //Return
            return null;
        }
        object ISynchronizeInvoke.Invoke(Delegate method, object[] args)
        {
            //Invoke
            return method?.DynamicInvoke(args);
        }
        IAsyncResult ISynchronizeInvoke.BeginInvoke(Delegate method, object[] args)
        {
            throw new NotImplementedException();
        }
        object ISynchronizeInvoke.EndInvoke(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        private static void SelectedFilePropretyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AbideViewModel viewModel)
            {
                if (e.NewValue is FileItem file)
                {
                    viewModel.WindowTitle = $"{file.FileName} - {DefaultWindowTitle}";
                }
                else
                {
                    viewModel.WindowTitle = DefaultWindowTitle;
                }
            }
        }
        private static void CurrentSolutionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        private static void SelectedXboxPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        private static void PrimaryOperationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }

    public sealed class OperationsStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "Ready";
            }
            else if (value is string status)
            {
                if (string.IsNullOrEmpty(status))
                {
                    return "Ready";
                }

                return status;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class ProgressBarReporter : INotifyPropertyChanged, IProgressReporter
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int max = 100;
        private int value = 0;
        private bool visible = true;
        private string status = string.Empty;

        public int Maximum
        {
            get => max;
            set
            {
                if (value < 1) value = 1;
                if (Value < value) Value = value;

                if (max != value)
                {
                    max = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int Value
        {
            get => value;
            set
            {
                if (value > max) value = max;
                if (value < 0) value = 0;

                if (this.value != value)
                {
                    this.value = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool Visible
        {
            get => visible;
            set
            {
                if (visible != value)
                {
                    visible = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Status
        {
            get => status;
            set
            {
                if (status != value)
                {
                    status = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void Report(int value)
        {
            Value = value;
        }
        public void Reset(int max)
        {
            Maximum = max;
        }
        public void ReportStatus(string status)
        {
            Status = status;
        }
        public void SetVisibility(bool visible)
        {
            Visible = visible;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
