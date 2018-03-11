using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using static Abide.HaloLibrary.Halo2Map.MapFile;

namespace Abide.Halo2.Designer
{
    internal partial class StringListEditorDialog : Form
    {
        private const string newString = "new_string";
        private readonly StringList stringList = new StringList();

        /// <summary>
        /// Initializes a new instance of the <see cref="StringListEditorDialog"/> class.
        /// </summary>
        public StringListEditorDialog()
        {
            InitializeComponent();
            stringList.Add(string.Empty);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="StringListEditorDialog"/> class using the specified string list.
        /// </summary>
        /// <param name="stringList">The string list.</param>
        public StringListEditorDialog(StringList stringList) : this()
        {
            //Set
            this.stringList = stringList;

            //Begin
            stringListBox.BeginUpdate();
            
            //Add
            foreach (string s in stringList)
                stringListBox.Items.Add(s);

            //End
            stringListBox.EndUpdate();
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            StringId id = StringId.Zero;
            if(stringList.Add(newString, out id))
            {
                stringListBox.Items.Add(newString);
                stringListBox.SelectedIndex = id.Index;
            }
        }
        private void stringListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check
            if (stringListBox.SelectedIndex >= 0)
            {
                stringTextBox.Text = stringList[stringListBox.SelectedIndex];
                removeToolStripMenuItem.Enabled = true;
                stringTextBox.Enabled = true;
            }
            else
            {
                stringTextBox.Text = string.Empty;
                removeToolStripMenuItem.Enabled = false;
                stringTextBox.Enabled = false;
            }
        }
        private void stringTextBox_TextChanged(object sender, EventArgs e)
        {
            if (stringListBox.SelectedIndex >= 0)
            {
                stringList[stringListBox.SelectedIndex] = stringTextBox.Text;
                stringListBox.Items[stringListBox.SelectedIndex] = stringTextBox.Text;
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stringListBox.SelectedIndex >= 0)
            {
                stringList.RemoveAt(stringListBox.SelectedIndex);
                stringListBox.Items.RemoveAt(stringListBox.SelectedIndex);
            }
        }
    }

    internal class StringsListEditor : UITypeEditor
    {
        public StringsListEditor() : base() { }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            //Check
            if (value is StringList)
                using (StringListEditorDialog stringListDlg = new StringListEditorDialog((StringList)value))
                    stringListDlg.ShowDialog();

            //Return
            return value;
        }
    }
}
