using Abide.AddOnApi.Wpf;
using System.Collections.ObjectModel;
using System.Windows;

namespace Abide.Wpf.Modules.ViewModel
{
    /// <summary>
    /// Represents a file reference.
    /// </summary>
    public sealed class FileItem : DependencyObject
    {
        private static readonly DependencyPropertyKey FileNamePropertyKey =
            DependencyProperty.RegisterReadOnly("FileName", typeof(string), typeof(FileItem), new PropertyMetadata(string.Empty));
        private static readonly DependencyPropertyKey EditorElementPropertyKey =
            DependencyProperty.RegisterReadOnly("EditorElement", typeof(FrameworkElement), typeof(FileItem), new PropertyMetadata(null));
        /// <summary>
        /// Identifies the <see cref="FileName"/> property.
        /// </summary>
        public static readonly DependencyProperty FileNameProperty =
            FileNamePropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="Path"/> property.
        /// </summary>
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(FileItem), new PropertyMetadata(string.Empty, PathPropertyChanged));
        /// <summary>
        /// Identifies the <see cref="Editor"/> property.
        /// </summary>
        public static readonly DependencyProperty EditorProperty =
            DependencyProperty.Register("Editor", typeof(IFileEditor), typeof(FileItem), new PropertyMetadata(null, EditorPropertyChanged));
        /// <summary>
        /// Identifies the <see cref="EditorElement"/> property.
        /// </summary>
        public static readonly DependencyProperty EditorElementProperty =
            EditorElementPropertyKey.DependencyProperty;
        /// <summary>
        /// Gets and returns the file name of <see cref="Path"/>.
        /// </summary>
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            private set { SetValue(FileNamePropertyKey, value); }
        }
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }
        /// <summary>
        /// Gets or sets the file editor.
        /// </summary>
        public IFileEditor Editor
        {
            get { return (IFileEditor)GetValue(EditorProperty); }
            set { SetValue(EditorProperty, value); }
        }
        /// <summary>
        /// Gets and returns the editor framework element.
        /// </summary>
        public FrameworkElement EditorElement
        {
            get { return (FrameworkElement)GetValue(EditorElementProperty); }
            private set { SetValue(EditorElementPropertyKey, value); }
        }
        /// <summary>
        /// Returns the value of the <see cref="Path"/> property.
        /// </summary>
        /// <returns>The value of the <see cref="Path"/> property.</returns>
        public override string ToString()
        {
            return Path;
        }

        private static void PathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Check
            if (d is FileItem tabItem && e.NewValue is string path)
            {
                tabItem.FileName = System.IO.Path.GetFileName(path);
                tabItem.Editor?.Initialize(path);
            }
        }
        private static void EditorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Check
            if (d is FileItem tabItem && e.NewValue is IFileEditor editor)
            {
                editor.Initialize(tabItem.Path);
                tabItem.EditorElement = editor.Element;
            }
        }
    }

    /// <summary>
    /// Represents a collection of file item elements. 
    /// </summary>
    public sealed class FileCollection : ObservableCollection<FileItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileCollection"/> class.
        /// </summary>
        public FileCollection() { }
    }
}
