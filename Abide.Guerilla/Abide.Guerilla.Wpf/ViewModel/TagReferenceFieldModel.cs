using Abide.Guerilla.Library;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Input;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a tag reference field container.
    /// </summary>
    public class TagReferenceFieldModel : FieldModel
    {
        /// <summary>
        /// Gets and returns the current tags directory.
        /// </summary>
        private static string TagsDirectory
        {
            get { return Path.Combine(RegistrySettings.WorkspaceDirectory, "tags"); }
        }
        /// <summary>
        /// Gets or sets the value of the value field as a string.
        /// </summary>
        public new string Value
        {
            get
            {
                //Check
                if (TagField == null) return null;

                //Prepare
                string value = string.Empty;
                if (base.Value is string) value = (string)base.Value;

                //Return
                return value;
            }
            set
            {
                //Check
                if (TagField == null) return;
                base.Value = value;
            }
        }
        /// <summary>
        /// Gets and returns the command to browse for a different tag reference.
        /// </summary>
        public ICommand BrowseCommand { get; }
        /// <summary>
        /// Gets and returns the command to open the current tag reference.
        /// </summary>
        public ICommand OpenCommand { get; }
        /// <summary>
        /// Gets and returns the command to clear the current tag reference.
        /// </summary>
        public ICommand ClearCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagReferenceFieldModel"/> class.
        /// </summary>
        public TagReferenceFieldModel()
        {
            //Setup commands
            BrowseCommand = new RelayCommand(p => Browse(), p => CanBrowse());
            OpenCommand = new RelayCommand(p => OpenTag(), p => TagPresent());
            ClearCommand = new RelayCommand(p => ClearTag(), p => TagPresent());
        }
        private bool CanBrowse()
        {
            return true;
        }
        private bool TagPresent()
        {
            return !string.IsNullOrEmpty(Value);
        }
        private void Browse()
        {
            //Prepare
            string tagFilePath = Path.Combine(TagsDirectory, Value);
            string initialDirectory = TagsDirectory;

            //Check
            if (File.Exists(tagFilePath))
                initialDirectory = Path.GetDirectoryName(tagFilePath);

            //Initialize
            OpenFileDialog openDlg = new OpenFileDialog()
            {
                FileName = tagFilePath,
                InitialDirectory = initialDirectory,
                Filter = "All Files (*.*)|*.*"
            };

            //Add custom place
            openDlg.CustomPlaces.Add(new FileDialogCustomPlace(TagsDirectory));

            //Show
            if (openDlg.ShowDialog() ?? false)
                Value = openDlg.FileName.Replace(TagsDirectory, string.Empty).Substring(1);
        }
        private void OpenTag()
        {
            //Notify
            Owner.NotifyOpenTagReferenceRequested(Value);
        }
        private void ClearTag()
        {
            //Empty
            Value = string.Empty;
        }
    }
}
