using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a base file container.
    /// </summary>
    public abstract class FileModel : NotifyPropertyChangedViewModel
    {
        /// <summary>
        /// Gets or sets the close file callback.
        /// </summary>
        public CloseFileCallback CloseCallback
        {
            get;
            set;
        }
        /// <summary>
        /// Gets and returns a filter string for the file.
        /// </summary>
        public abstract string FileFilter
        {
            get;
        }
        /// <summary>
        /// Gets and returns the display name of the file.
        /// </summary>
        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(FileName)) return string.Empty;
                displayName = Path.GetFileName(FileName);
                return displayName;
            }
            set
            {
                bool changed = displayName != value;
                displayName = value;
                if (changed) NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the name of the file being modified.
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set
            {
                bool changed = fileName != value;
                fileName = value;
                if (changed)
                {
                    NotifyPropertyChanged();
                    DisplayName = Path.GetFileName(value);
                }
            }
        }
        /// <summary>
        /// Gets or sets the dirty state of the file.
        /// </summary>
        public bool IsDirty
        {
            get { return isDirty; }
            set
            {
                bool changed = isDirty != value;
                isDirty = value;
                if (changed) NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Gets and returns the close file command.
        /// </summary>
        public ICommand CloseFileCommand { get; }
        /// <summary>
        /// Gets and returns the save file command.
        /// </summary>
        public ICommand SaveFileCommand { get; }
                
        private string displayName = string.Empty;
        private string fileName = string.Empty;
        private bool isDirty = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileModel"/> class using the specified file name.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        public FileModel(string fileName)
        {
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            CloseFileCommand = new RelayCommand(o => CloseFile());
            SaveFileCommand = new RelayCommand(o => SaveToFile());
        }
        /// <summary>
        /// Loads the file.
        /// </summary>
        public abstract void LoadFromFile();
        /// <summary>
        /// Saves the file.
        /// </summary>
        public abstract void SaveToFile();
        protected virtual void CloseFile()
        {
            //Prepare
            bool remove = true;

            //Check
            if (IsDirty)
            {
                //Get result
                MessageBoxResult result = MessageBox.Show($"Save changes to {DisplayName}?", 
                    "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

                //Handle
                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        remove = false;
                        break;
                    case MessageBoxResult.Yes:
                        SaveToFile();
                        break;
                }
            }

            //Check and remove
            if (remove) CloseCallback?.Invoke(this);
        }
    }

    public delegate void CloseFileCallback(FileModel fileModel);
}
