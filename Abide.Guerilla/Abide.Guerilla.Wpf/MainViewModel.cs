using Abide.Guerilla.Library;
using Abide.Guerilla.Wpf.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Abide.Guerilla.Wpf
{
    /// <summary>
    /// Represents the main view container.
    /// </summary>
    public sealed class MainViewModel : NotifyPropertyChangedViewModel
    {
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
                bool changed = selectedFile != value;
                selectedFile = value;
                if (changed) NotifyPropertyChanged();
            }
        }

        private FileModel selectedFile = null;
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
    }

    /// <summary>
    /// Represents a view model that can notify when a propery has been changed.
    /// </summary>
    public class NotifyPropertyChangedViewModel : INotifyPropertyChanged
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
