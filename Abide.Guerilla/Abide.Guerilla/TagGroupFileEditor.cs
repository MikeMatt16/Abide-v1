using Abide.Guerilla.Library;
using Abide.Tag;
using System;
using System.IO;
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
        public AbideTagGroupFile TagGroupFile { get; }

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
        public TagGroupFileEditor(string fileName, AbideTagGroupFile tagGroupFile) : this()
        {
            //Setup
            TagGroupFile = tagGroupFile ?? throw new ArgumentNullException(nameof(tagGroupFile));
            m_FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));

            //Set
            Text = fileName;
        }
        private void TagGroupFileEditor_Load(object sender, EventArgs e)
        {
            foreach (ITagBlock tagBlock in TagGroupFile.TagGroup)
                Tags.GenerateControls(guerillaFlowLayoutPanel, tagBlock);
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

        private void TagGroupFileEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Save changes?", "Save", MessageBoxButtons.YesNoCancel);
            switch (result)
            {
                case DialogResult.Cancel:
                    if (e.CloseReason == CloseReason.UserClosing) e.Cancel = true;  //cancel
                    break;
                case DialogResult.Yes:
                    //Save to memory stream to prevent file corruption
                    using (MemoryStream ms = new MemoryStream())
                    {
                        //Save to stream
                        TagGroupFile.Save(ms);

                        //Save to file
                        TagGroupFile.Save(m_FileName);
                    }
                    break;
            }
        }
    }
}
