using System;
using System.IO;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a base file container.
    /// </summary>
    public abstract class FileModel : NotifyPropertyChangedViewModel
    {
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

        private string displayName = string.Empty;
        private string fileName = string.Empty;
        private bool isDirty = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileModel"/> class using the specified file name.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        public FileModel(string fileName)
        {
            this.FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        }
        /// <summary>
        /// Loads the file.
        /// </summary>
        public abstract void LoadFromFile();
        /// <summary>
        /// Saves the file.
        /// </summary>
        public abstract void SaveToFile();
    }
}
