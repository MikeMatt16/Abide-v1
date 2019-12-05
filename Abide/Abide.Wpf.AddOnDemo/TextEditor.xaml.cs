using Abide.AddOnApi;
using Abide.AddOnApi.Wpf;
using System.IO;
using System.Windows;

namespace Abide.Wpf.AddOnDemo
{
    /// <summary>
    /// Interaction logic for TextEditor.xaml
    /// </summary>
    [AddOn]
    public sealed partial class TextEditor : FileEditorControl
    {
        /// <summary>
        /// Identifies the <see cref="Text"/> property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextEditor), new PropertyMetadata(string.Empty));
        /// <summary>
        /// Gets or sets the text of the editor.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TextEditor"/> class.
        /// </summary>
        public TextEditor()
        {
            InitializeComponent();
        }
        public override bool IsValidEditor(string path)
        {
            return true;    //Technically anything can be a text file.
        }
        public override void Load(string path)
        {
            //Read
            using (StreamReader reader = File.OpenText(path))
                Text = reader.ReadToEnd();

            //Base load procedures
            base.Load(path);
        }
    }
}
