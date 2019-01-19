using Abide.Guerilla.Library;
using System;
using System.Windows.Forms;

namespace Abide.Guerilla
{
    public partial class TagGroupFileEditor : Form
    {
        /// <summary>
        /// Occurs when the editor's file name is changed.
        /// </summary>
        public event EventHandler FileNameChanged;
        /// <summary>
        /// Gets and returns the path of the tag group file.
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }
            set { bool changed = value != m_FileName; m_FileName = value; if (changed) OnFileNameChanged(new EventArgs()); }
        }
        /// <summary>
        /// Gets and returns the tag group file.
        /// </summary>
        public TagGroupFile TagGroupFile { get; }

        private string m_FileName = string.Empty;
        
        private TagGroupFileEditor()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TagGroupFileEditor"/> class using the specified file name and tag group file.
        /// </summary>
        /// <param name="fileName">The path of the tag group file.</param>
        /// <param name="tagGroupFile">The tag group file.</param>
        public TagGroupFileEditor(string fileName, TagGroupFile tagGroupFile) : this()
        {
            //Setup
            TagGroupFile = tagGroupFile ?? throw new ArgumentNullException(nameof(tagGroupFile));
            m_FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));

            //Set
            Text = fileName;
        }
        /// <summary>
        /// Raises the <see cref="FileNameChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing event data.</param>
        protected virtual void OnFileNameChanged(EventArgs e)
        {
            //Raise event
            FileNameChanged?.Invoke(this, e);
        }
    }
}
