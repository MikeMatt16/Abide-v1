using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Abide.AddOnApi.Wpf
{
    /// <summary>
    /// A custom file editor for Abide.
    /// </summary>
    public class FileEditorControl : UserControl, IFileEditor
    {
        /// <summary>
        /// Identifies the <see cref="Path"/> property.
        /// </summary>
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(FileEditorControl), new PropertyMetadata(string.Empty, PathPropertyChanged));
        /// <summary>
        /// Identifies the <see cref="IsDirty"/> property.
        /// </summary>
        public static readonly DependencyProperty IsDirtyProperty =
            DependencyProperty.Register("IsDirty", typeof(bool), typeof(FileEditorControl));

        /// <summary>
        /// Gets and returns the file history.
        /// </summary>
        [Browsable(false)]
        public FileHistoryCollection History { get; } = new FileHistoryCollection();
        /// <summary>
        /// Gets or sets a value that determines the state of the file.
        /// </summary>
        [Browsable(false)]
        public bool IsDirty
        {
            get { return (bool)GetValue(IsDirtyProperty); }
            set { SetValue(IsDirtyProperty, value); }
        }
        /// <summary>
        /// Gets or sets the path of the file.
        /// </summary>
        [Category("File Editor")]
        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }
        /// <summary>
        /// Gets and returns the AddOn host.
        /// </summary>
        public IHost Host { get; private set; }
        /// <summary>
        /// Gets or sets the extension of the file editor.
        /// </summary>
        [Category("File Editor")]
        public string Extension { get; set; } = "*.*";
        /// <summary>
        /// Gets or sets the type name of the file editor.
        /// </summary>
        [Category("File Editor")]
        public string TypeName { get; set; } = "All Files";
        /// <summary>
        /// Gets or sets the name of the AddOn.
        /// </summary>
        [Category("Abide AddOn")]
        public string AddOnName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the description of the AddOn.
        /// </summary>
        [Category("Abide AddOn")]
        public string AddOnDescription { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the author of the AddOn.
        /// </summary>
        [Category("Abide AddOn")]
        public string AddOnAuthor { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileEditorControl"/> class.
        /// </summary>
        public FileEditorControl() { }
        /// <summary>
        /// Finalizes the <see cref="FileEditorControl"/> class instance.
        /// </summary>
        ~FileEditorControl()
        {
            Dispose(false);
        }
        /// <summary>
        /// Determines if this <see cref="FileEditorControl"/> instance is an editor of the specified file.
        /// </summary>
        /// <param name="path">The path of the file to validate.</param>
        /// <returns><see cref="true"/> if <paramref langword="path"/> is a path of a valid file; otherwise, <see langword="false"/>.</returns>
        public virtual bool IsValidEditor(string path)
        {
            return false;
        }
        /// <summary>
        /// Loads a specified file for the file editor.
        /// </summary>
        /// <param name="path"></param>
        public virtual void Load(string path)
        {
            //Check
            Path = path;
            if (File.Exists(path))
            {
                //Set
                IsDirty = false;
            }
        }
        /// <summary>
        /// Initializes the <see cref="FileEditorControl"/> AddOn.
        /// </summary>
        public virtual void Initialize() { }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Releases all managed resources used by this instance and optionally releases all unmanaged resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) { }

        FrameworkElement IElementSupport.Element => this;
        string IFileEditor.Extension => Extension;
        string IFileEditor.TypeName => TypeName;
        string IAddOn.Name => AddOnName;
        string IAddOn.Description => AddOnDescription;
        string IAddOn.Author => AddOnAuthor;
        
        bool IFileEditor.IsValidEditor(string path) => IsValidEditor(path);
        void IFileEditor.Initialize(string path)
        {
            Load(path);
        }
        void IAddOn.Initialize(IHost host)
        {
            Host = host;
            Initialize();
        }

        private static void PathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Check
            if (d is FileEditorControl control && e.NewValue is string path)
                control.Load(path); //Load
        }
    }
}
