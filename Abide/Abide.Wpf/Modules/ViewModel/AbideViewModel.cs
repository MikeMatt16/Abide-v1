using Abide.AddOnApi;
using Abide.AddOnApi.Wpf;
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
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Abide.Wpf.Modules.ViewModel
{
    /// <summary>
    /// Represents a model that contains the state of Abide.
    /// </summary>
    public sealed class AbideViewModel : DependencyObject, IHost
    {
        private const string DefaultWindowTitle = "Abide";

        private static readonly DependencyPropertyKey ProgressBarPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ProgressBar), typeof(ProgressBarReporter), typeof(AbideViewModel), new PropertyMetadata(new ProgressBarReporter()));
        private static readonly DependencyPropertyKey BackgroundOperationsPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(BackgroundOperations), typeof(ObservableCollection<BackgroundOperation>), typeof(AbideViewModel), new PropertyMetadata(new ObservableCollection<BackgroundOperation>()));
        private static readonly DependencyPropertyKey FilesPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Files), typeof(FileCollection), typeof(AbideViewModel), new PropertyMetadata(new FileCollection()));
        private static readonly DependencyPropertyKey FactoryPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Factory), typeof(EditorAddOnFactory), typeof(AbideViewModel), new PropertyMetadata(null));
        private static readonly DependencyPropertyKey NewProjectCommandPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(NewProjectCommand), typeof(ICommand), typeof(AbideViewModel), new PropertyMetadata());
        private static readonly DependencyPropertyKey NewCommandPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(NewCommand), typeof(ICommand), typeof(AbideViewModel), new PropertyMetadata());
        private static readonly DependencyPropertyKey OpenProjectCommandPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(OpenProjectCommand), typeof(ICommand), typeof(AbideViewModel), new PropertyMetadata());
        private static readonly DependencyPropertyKey OpenCommandPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(nameof(OpenCommand), typeof(ICommand), typeof(AbideViewModel), new PropertyMetadata());
        private static readonly DependencyPropertyKey DecompileMapCommandPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(nameof(DecompileMapCommand), typeof(ICommand), typeof(AbideViewModel), new PropertyMetadata());
        private static readonly DependencyPropertyKey CompileMapCommandPropertyKey =
             DependencyProperty.RegisterAttachedReadOnly(nameof(CompileMapCommand), typeof(ICommand), typeof(AbideViewModel), new PropertyMetadata());

        /// <summary>
        /// Identifies the <see cref="MainWindow"/> property.
        /// </summary>
        public static readonly DependencyProperty WindowProperty =
            DependencyProperty.Register(nameof(MainWindow), typeof(Window), typeof(AbideViewModel));
        /// <summary>
        /// Identifies the <see cref="WindowTitle"/> property.
        /// </summary>
        public static readonly DependencyProperty WindowTitleProperty =
            DependencyProperty.Register(nameof(WindowTitle), typeof(string), typeof(AbideViewModel), new PropertyMetadata(DefaultWindowTitle));
        /// <summary>
        /// Identifies the <see cref="PrimaryOperation"/> property.
        /// </summary>
        public static readonly DependencyProperty PrimaryOperationProperty =
            DependencyProperty.Register(nameof(PrimaryOperation), typeof(BackgroundOperation), typeof(AbideViewModel));
        /// <summary>
        /// Identifies the <see cref="Files"/> property.
        /// </summary>
        public static readonly DependencyProperty FilesProperty =
            FilesPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="BackgroundOperations"/> property.
        /// </summary>
        public static readonly DependencyProperty BackgroundOperationsProperty =
            BackgroundOperationsPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="SelectedFile"/> property.
        /// </summary>
        public static readonly DependencyProperty SelectedFileProperty =
            DependencyProperty.Register(nameof(SelectedFile), typeof(FileItem), typeof(AbideViewModel), new PropertyMetadata(SelectedFilePropretyChanged));
        /// <summary>
        /// Identifies the <see cref="CurrentSolution"/> property.
        /// </summary>
        public static readonly DependencyProperty CurrentSolutionProperty =
            DependencyProperty.Register(nameof(CurrentSolution), typeof(SolutionViewModel), typeof(AbideViewModel),
                new PropertyMetadata(CurrentSolutionPropertyChanged));
        /// <summary>
        /// Identifies the <see cref="Factory"/> property.
        /// </summary>
        public static readonly DependencyProperty FactoryProperty =
            FactoryPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="NewProjectCommand"/> property.
        /// </summary>
        public static readonly DependencyProperty NewProjectCommandProperty =
            NewProjectCommandPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="NewCommand"/> property.
        /// </summary>
        public static readonly DependencyProperty NewCommandProperty =
            NewCommandPropertyKey.DependencyProperty;
        public static readonly DependencyProperty OpenProjectCommandProperty =
            OpenProjectCommandPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="OpenCommand"/> property.
        /// </summary>
        public static readonly DependencyProperty OpenCommandProperty =
            OpenCommandPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="DecompileMapCommand"/> property.
        /// </summary>
        public static readonly DependencyProperty DecompileMapCommandProperty =
            DecompileMapCommandPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="CompileMapCommand"/> property.
        /// </summary>
        public static readonly DependencyProperty CompileMapCommandProperty =
             CompileMapCommandPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets or sets the window.
        /// </summary>
        public Window MainWindow
        {
            get => (Window)GetValue(WindowProperty);
            set => SetValue(WindowProperty, value);
        }
        /// <summary>
        /// Gets or sets the title of the window.
        /// </summary>
        public string WindowTitle
        {
            get => (string)GetValue(WindowTitleProperty);
            set => SetValue(WindowTitleProperty, value);
        }
        /// <summary>
        /// 
        /// </summary>
        public ProgressBarReporter ProgressBar
        {
            get => (ProgressBarReporter)GetValue(ProgressBarPropertyKey.DependencyProperty);
            private set => SetValue(ProgressBarPropertyKey, value);
        }
        /// <summary>
        /// 
        /// </summary>
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
        public ObservableCollection<BackgroundOperation> BackgroundOperations => (ObservableCollection<BackgroundOperation>)GetValue(BackgroundOperationsProperty);
        /// <summary>
        /// Gets and returns a list of open files.
        /// </summary>
        public FileCollection Files => (FileCollection)GetValue(FilesProperty);
        /// <summary>
        /// Gets or sets the selected file.
        /// </summary>
        public FileItem SelectedFile
        {
            get => (FileItem)GetValue(SelectedFileProperty);
            set => SetValue(SelectedFileProperty, value);
        }
        /// <summary>
        /// Gets and returns the AddOn factory.
        /// </summary>
        public EditorAddOnFactory Factory
        {
            get => (EditorAddOnFactory)GetValue(FactoryProperty);
            private set => SetValue(FactoryPropertyKey, value);
        }
        /// <summary>
        /// Gets and returns the new project command.
        /// </summary>
        public ICommand NewProjectCommand
        {
            get => (ICommand)GetValue(NewProjectCommandProperty);
            private set => SetValue(NewProjectCommandPropertyKey, value);
        }
        /// <summary>
        /// Gets and returns the new file command.
        /// </summary>
        public ICommand NewCommand
        {
            get => (ICommand)GetValue(NewCommandProperty);
            private set => SetValue(NewCommandPropertyKey, value);
        }
        /// <summary>
        /// Gets and returns the open project command.
        /// </summary>
        public ICommand OpenProjectCommand
        {
            get => (ICommand)GetValue(OpenProjectCommandProperty);
            set => SetValue(OpenProjectCommandPropertyKey, value);
        }
        /// <summary>
        /// Gets and returns the open file command.
        /// </summary>
        public ICommand OpenCommand
        {
            get => (ICommand)GetValue(OpenCommandProperty);
            private set => SetValue(OpenCommandPropertyKey, value);
        }
        /// <summary>
        /// Gets and returns the decompile map command.
        /// </summary>
        public ICommand DecompileMapCommand
        {
            get => (ICommand)GetValue(DecompileMapCommandProperty);
            private set => SetValue(DecompileMapCommandPropertyKey, value);
        }
        /// <summary>
        /// Gets and returns the compile map command.
        /// </summary>
        public ICommand CompileMapCommand
        {
            get => (ICommand)GetValue(CompileMapCommandProperty);
            private set => SetValue(CompileMapCommandPropertyKey, value);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AbideViewModel"/> class.
        /// </summary>
        public AbideViewModel()
        {
            //Load AddOn Factory
            Factory = new EditorAddOnFactory(this);
            Factory.InitializeAddOns();

            //Setup
            NewProjectCommand = new ActionCommand(o => { NewProject(); });
            OpenProjectCommand = new ActionCommand(OpenProject);
            NewCommand = new ActionCommand(NewFile);
            OpenCommand = new ActionCommand(OpenFile);
            DecompileMapCommand = new ActionCommand(DecompileMap);
            CompileMapCommand = new ActionCommand(CompileMap);

            //Load files from ApplicationSettings
            ApplicationSettings.FilePaths.ForEach(p => OpenFile(p));
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
#if !DEBUG
            throw new NotImplementedException();
#endif
            OpenFileDialog openDlg = new OpenFileDialog() { Filter = "Halo 2 Map Files (*.map)|*.map" };
            if (openDlg.ShowDialog() ?? false)
            {
                DecompileMapFile(openDlg.FileName);
            }
        }
        private void CompileMap(object arg)
        {
            throw new NotImplementedException();
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

            //Ask
            if (editors.Length > 1)
            {
                //Create dialog
                ChooseEditorDialog editorDialog = new ChooseEditorDialog();
                editorDialog.Editors.Clear();

                //Load editors
                for (int i = 0; i < types.Length; i++)
                {
                    editorDialog.Editors.Add(editors[i]);
                }

                //Set selected editor
                editorDialog.SelectedEditor = editors[0];

                //Show
                if (editorDialog.ShowDialog() ?? false)
                {
                    selectedEditor = editorDialog.SelectedEditor;
                }
            }
            else if (editors.Length == 1)
            {
                selectedEditor = editors[0];
            }

            //Check
            if (selectedEditor != null)
            {
                //Create new instance
                IFileEditor editor = Factory.Instantiate<IFileEditor>(selectedEditor.GetType());

                //Create tab item
                FileItem file = Files.New(path, editor);

                //Add
                Files.Add(file);
                SelectedFile = file;
            }
        }
        private void DecompileMapFile(string path)
        {
            //Check
            if (!File.Exists(path))
            {
                return;
            }

            if (PrimaryOperation != null && PrimaryOperation.IsRunning)
            {
                return;
            }

            var operation = new HaloMapDecompileOperation(path);
            operation.Start(DecompileOperationCompleted, ProgressBar);
            PrimaryOperation = operation;
        }
        private void DecompileOperationCompleted(object state)
        {
            PrimaryOperation = null;
            ProgressBar.Report(0);
            _ = MessageBox.Show("Decompile Complete!");
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
    }

    public sealed class OperationsStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "Ready";
            }

            if (value is IList<BackgroundOperation> operations && targetType == typeof(string))
            {
                if (operations.Count == 0)
                {
                    return "Ready";
                }
                else if (operations.Count == 1)
                {
                    return operations[0].Status;
                }
                else
                {
                    return "Multiple operations...";
                }
            }
            else if (value is BackgroundOperation operation)
            {
                if (string.IsNullOrEmpty(operation.Status))
                {
                    return "Ready";
                }

                return operation.Status;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class ProgressBarReporter : INotifyPropertyChanged, IProgress<int>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int min = 0;
        private int max = 100;
        private int value = 0;

        public int Minimum
        {
            get => min;
            set
            {
                if (min != value)
                {
                    min = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int Maximum
        {
            get => max;
            set
            {
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
                if (this.value != value)
                {
                    this.value = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public void Report(int value)
        {
            Value = value;
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
