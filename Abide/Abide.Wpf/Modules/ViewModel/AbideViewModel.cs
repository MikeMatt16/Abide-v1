using Abide.AddOnApi;
using Abide.AddOnApi.Wpf;
using Abide.Wpf.Modules.Tools;
using Abide.Wpf.Modules.Dialogs;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Abide.Wpf.Modules.AddOns;
using System.Windows.Markup;

namespace Abide.Wpf.Modules.ViewModel
{
    /// <summary>
    /// Represents a model that contains the state of Abide.
    /// </summary>
    public sealed class AbideViewModel : DependencyObject, IHost
    {
        private const string DefaultWindowTitle = "Abide";

        private static readonly DependencyPropertyKey FilesPropertyKey =
            DependencyProperty.RegisterReadOnly("Files", typeof(FileCollection), typeof(AbideViewModel), new PropertyMetadata(new FileCollection()));
        private static readonly DependencyPropertyKey FactoryPropertyKey =
            DependencyProperty.RegisterReadOnly("Factory", typeof(EditorAddOnFactory), typeof(AbideViewModel), new PropertyMetadata(null));
        private static readonly DependencyPropertyKey NewCommandPropertyKey =
            DependencyProperty.RegisterReadOnly("NewCommand", typeof(ICommand), typeof(AbideViewModel), new PropertyMetadata());
        private static readonly DependencyPropertyKey OpenCommandPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("OpenCommand", typeof(ICommand), typeof(AbideViewModel), new PropertyMetadata());
        /// <summary>
        /// Identifies the <see cref="WindowTitle"/> property.
        /// </summary>
        public static readonly DependencyProperty WindowTitleProperty =
            DependencyProperty.Register("WindowTitle", typeof(string), typeof(AbideViewModel), new PropertyMetadata(DefaultWindowTitle));
        /// <summary>
        /// Identifies the <see cref="Files"/> property.
        /// </summary>
        public static readonly DependencyProperty FilesProperty =
            FilesPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="SelectedFile"/> property.
        /// </summary>
        public static readonly DependencyProperty SelectedFileProperty =
            DependencyProperty.Register("SelectedFile", typeof(FileItem), typeof(AbideViewModel), new PropertyMetadata(SelectedFilePropretyChanged));
        /// <summary>
        /// Identifies the <see cref="Factory"/> property.
        /// </summary>
        public static readonly DependencyProperty FactoryProperty =
            FactoryPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="NewCommand"/> property.
        /// </summary>
        public static readonly DependencyProperty NewCommandProperty =
            NewCommandPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="OpenCommand"/> property.
        /// </summary>
        public static readonly DependencyProperty OpenCommandProperty =
            OpenCommandPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets or sets the title of the window.
        /// </summary>
        public string WindowTitle
        {
            get { return (string)GetValue(WindowTitleProperty); }
            set { SetValue(WindowTitleProperty, value); }
        }
        /// <summary>
        /// Gets and returns a list of open files.
        /// </summary>
        public FileCollection Files
        {
            get { return (FileCollection)GetValue(FilesProperty); }
        }
        /// <summary>
        /// Gets or sets the selected file.
        /// </summary>
        public FileItem SelectedFile
        {
            get { return (FileItem)GetValue(SelectedFileProperty); }
            set { SetValue(SelectedFileProperty, value); }
        }
        /// <summary>
        /// Gets and returns the AddOn factory.
        /// </summary>
        public EditorAddOnFactory Factory
        {
            get { return (EditorAddOnFactory)GetValue(FactoryProperty); }
            private set { SetValue(FactoryPropertyKey, value); }
        }
        /// <summary>
        /// Gets and returns the new file command.
        /// </summary>
        public ICommand NewCommand
        {
            get { return (ICommand)GetValue(NewCommandProperty); }
            private set { SetValue(NewCommandPropertyKey, value); }
        }
        /// <summary>
        /// Gets and returns the open file command.
        /// </summary>
        public ICommand OpenCommand
        {
            get { return (ICommand)GetValue(OpenCommandProperty); }
            private set { SetValue(OpenCommandPropertyKey, value); }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AbideViewModel"/> class.
        /// </summary>
        public AbideViewModel()
        {
            //Load AddOn Factory
            Factory = new EditorAddOnFactory();
            Factory.InitializeAddOns();

            //Setup
            NewCommand = new ActionCommand(NewFile);
            OpenCommand = new ActionCommand(OpenFile);

            //Load files from ApplicationSettings
            ApplicationSettings.FilePaths.ForEach(p => OpenFile(p));
        }
        private void NewFile()
        {

        }
        private void OpenFile()
        {
            //Get file filter
            StringBuilder filterBuilder = new StringBuilder();
            filterBuilder.Append("All Supported Files|");
            filterBuilder.Append(string.Join(";", Factory.FileEditors.Select(e => e.Extension).ToArray()) + "|");
            filterBuilder.Append(string.Join("|", Factory.FileEditors.Select(e => $"{e.TypeName} ({e.Extension})|{e.Extension}").ToArray()) + "|");
            filterBuilder.Append("All Files (*.*)|*.*");

            //Show OpenFileDialog
            OpenFileDialog openDlg = new OpenFileDialog() { Filter = filterBuilder.ToString() };
            if (openDlg.ShowDialog() ?? false)
                OpenFile(openDlg.FileName);
        }
        private void OpenFile(string path)
        {
            //Check
            if (!File.Exists(path)) return;

            //Get extension
            string ext = Path.GetExtension(path);

            //Get valid types
            IFileEditor selectedEditor = null;
            IFileEditor[] editors = Factory.FileEditors.Where(e => e.Extension.Trim('*') == ext && e.IsValidEditor(path)).ToArray();
            Type[] types = editors.Select(e => e.GetType()).ToArray();

            //Ask
            if (editors.Length > 1)
            {
                //Create dialog
                ChooseEditorDialog editorDialog = new ChooseEditorDialog();
                editorDialog.Editors.Clear();

                //Load editors
                for (int i = 0; i < types.Length; i++)
                    editorDialog.Editors.Add(editors[i]);

                //Set selected editor
                editorDialog.SelectedEditor = editors[0];

                //Show
                if (editorDialog.ShowDialog() ?? false)
                    selectedEditor = editorDialog.SelectedEditor;
            }
            else if (editors.Length == 1)
                selectedEditor = editors[0];

            //Check
            if (selectedEditor != null)
            {
                //Create new instance
                IFileEditor editor = Factory.Instantiate<IFileEditor>(selectedEditor.GetType(), this);

                //Create tab item
                FileItem file = new FileItem()
                {
                    Path = path,
                    Editor = editor,
                };

                //Add
                Files.Add(file);
                SelectedFile = file;
            }
        }

        bool ISynchronizeInvoke.InvokeRequired { get { return false; } }
        object IHost.Invoke(Delegate method)
        {
            //Invoke
            return method?.DynamicInvoke();
        }
        object IHost.Request(IAddOn sender, string request, params object[] args)
        {
            //Check
            if (sender == null) return null;
            if (request == null) return null;

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
                if (e.NewValue is FileItem file) viewModel.WindowTitle = $"{file.FileName} - {DefaultWindowTitle}";
                else viewModel.WindowTitle = DefaultWindowTitle;
            }
        }
    }

    /// <summary>
    /// Represents a simple action command.
    /// </summary>
    public sealed class ActionCommand : ICommand
    {
        /// <summary>
        /// Occurs when the can execute state of the action changes.
        /// This will never occur because an <see cref="ActionCommand"/> can always execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// Gets and returns the action associated with this command.
        /// </summary>
        public Action Action { get; } = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class using the specified action.
        /// </summary>
        /// <param name="action">The action that this command performs.</param>
        public ActionCommand(Action action)
        {
            Action = action;    //Set action
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }


        /// <summary>
        /// Invokes the <see cref="Action"/> property.
        /// </summary>
        /// <param name="parameter">The unused parameter.</param>
        public void Execute(object parameter = null)
        {
            //Raise action
            Action?.Invoke();
        }
        /// <summary>
        /// Returns <see langword="true"/>.
        /// </summary>
        /// <param name="parameter">The optional parameter.</param>
        /// <returns><see langword=""="true"/>.</returns>
        public bool CanExecute(object parameter = null)
        {
            return true;
        }
    }
}
