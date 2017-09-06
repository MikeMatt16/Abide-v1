using Abide.HaloLibrary.Halo2Map;
using System.Windows.Forms;
using System;
using Abide.HaloLibrary;

namespace Unicode_Editor.Halo2
{
    public partial class UnicodeEditorForm : Form
    {
        private readonly MapFile map = new MapFile();
        private readonly StringContainer strings = new StringContainer();
        private StringEntry selectedEntry = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnicodeEditorForm"/> class using the specified map file and string container.
        /// </summary>
        /// <param name="map">The map file.</param>
        /// <param name="strings">The string container.</param>
        public UnicodeEditorForm(MapFile map, StringContainer strings) : this()
        {
            //Check
            if (map == null) throw new ArgumentNullException(nameof(map));
            if (strings == null) throw new ArgumentNullException(nameof(strings));
            
            //Setup
            this.map = map;
            this.strings = strings;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UnicodeEditorForm"/> class.
        /// </summary>
        public UnicodeEditorForm()
        {
            InitializeComponent();
            englishToolStripMenuItem.Checked = Properties.Settings.Default.Halo2StringLocale == StringLocale.English;
            englishToolStripMenuItem.Checked = Properties.Settings.Default.Halo2StringLocale == StringLocale.English;
            japaneseToolStripMenuItem.Checked = Properties.Settings.Default.Halo2StringLocale == StringLocale.Japanese;
            germanToolStripMenuItem.Checked = Properties.Settings.Default.Halo2StringLocale == StringLocale.German;
            frenchToolStripMenuItem.Checked = Properties.Settings.Default.Halo2StringLocale == StringLocale.French;
            spanishToolStripMenuItem.Checked = Properties.Settings.Default.Halo2StringLocale == StringLocale.Spanish;
            italianToolStripMenuItem.Checked = Properties.Settings.Default.Halo2StringLocale == StringLocale.Italian;
            koreanToolStripMenuItem.Checked = Properties.Settings.Default.Halo2StringLocale == StringLocale.Korean;
            chineseToolStripMenuItem.Checked = Properties.Settings.Default.Halo2StringLocale == StringLocale.Chinese;
            portugueseToolStripMenuItem.Checked = Properties.Settings.Default.Halo2StringLocale == StringLocale.Portuguese;
            englishToolStripMenuItem.Tag = StringLocale.English;
            japaneseToolStripMenuItem.Tag = StringLocale.Japanese;
            germanToolStripMenuItem.Tag = StringLocale.German;
            frenchToolStripMenuItem.Tag = StringLocale.French;
            spanishToolStripMenuItem.Tag = StringLocale.Spanish;
            italianToolStripMenuItem.Tag = StringLocale.Italian;
            koreanToolStripMenuItem.Tag = StringLocale.Korean;
            chineseToolStripMenuItem.Tag = StringLocale.Chinese;
            portugueseToolStripMenuItem.Tag = StringLocale.Portuguese;
        }

        private void UnicodeEditorForm_Load(object sender, EventArgs e)
        {
            //Load...
            if (englishToolStripMenuItem.Checked) strings_Load((StringLocale)englishToolStripMenuItem.Tag);
            if (japaneseToolStripMenuItem.Checked) strings_Load((StringLocale)japaneseToolStripMenuItem.Tag);
            if (germanToolStripMenuItem.Checked) strings_Load((StringLocale)germanToolStripMenuItem.Tag);
            if (frenchToolStripMenuItem.Checked) strings_Load((StringLocale)frenchToolStripMenuItem.Tag);
            if (spanishToolStripMenuItem.Checked) strings_Load((StringLocale)spanishToolStripMenuItem.Tag);
            if (italianToolStripMenuItem.Checked) strings_Load((StringLocale)italianToolStripMenuItem.Tag);
            if (koreanToolStripMenuItem.Checked) strings_Load((StringLocale)koreanToolStripMenuItem.Tag);
            if (chineseToolStripMenuItem.Checked) strings_Load((StringLocale)chineseToolStripMenuItem.Tag);
            if (portugueseToolStripMenuItem.Checked) strings_Load((StringLocale)portugueseToolStripMenuItem.Tag);
        }

        private void languageSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Toggle all off...
            englishToolStripMenuItem.Checked = false;
            japaneseToolStripMenuItem.Checked = false;
            germanToolStripMenuItem.Checked = false;
            frenchToolStripMenuItem.Checked = false;
            spanishToolStripMenuItem.Checked = false;
            italianToolStripMenuItem.Checked = false;
            koreanToolStripMenuItem.Checked = false;
            chineseToolStripMenuItem.Checked = false;
            portugueseToolStripMenuItem.Checked = false;

            //Check selected...
            if (sender is ToolStripMenuItem) ((ToolStripMenuItem)sender).Checked = true;

            //Load
            if (sender is ToolStripMenuItem && ((ToolStripMenuItem)sender).Tag is StringLocale)
                strings_Load((StringLocale)((ToolStripMenuItem)sender).Tag);
        }

        private void strings_Load(StringLocale locale)
        {
            //Save Settings
            Properties.Settings.Default.Halo2StringLocale = locale;
            Properties.Settings.Default.Save();

            //Begin
            stringList.BeginUpdate();
            stringList.Items.Clear();

            //Loop
            foreach (var stringEntry in strings[locale])
            {
                //Create
                ListViewItem item = new ListViewItem(stringEntry.ID);
                item.SubItems.Add(UnicodeString.ConvertToReadable(stringEntry.Value));
                item.Tag = stringEntry;

                //Add
                stringList.Items.Add(item);
            }

            //End
            stringList.EndUpdate();
        }

        private void stringList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Prepare
            ListViewItem item = null;

            //Check
            if (stringList.SelectedItems.Count > 0)
            {
                //Get selcted...
                item = stringList.SelectedItems[0];

                //Check tag...
                if (item.Tag is StringEntry)
                {
                    selectedEntry = (StringEntry)item.Tag;
                    stringTextBox.Text = UnicodeString.ConvertToReadable(selectedEntry.Value);
                }
            }
        }

        private void stringTextBox_TextChanged(object sender, EventArgs e)
        {
            //Set
            if (selectedEntry != null) selectedEntry.Value = UnicodeString.ConvertToUnicode(stringTextBox.Text);
        }

        private void addStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize
            using (AddStringDialog addDlg = new AddStringDialog())
            {
                //Prepare
                addDlg.StringId = "new_string";

                //Show
                if (addDlg.ShowDialog() == DialogResult.OK)
                {
                    //Create Entry
                    StringEntry entry = new StringEntry(string.Empty, addDlg.StringId);

                    //Add StringId?
                    if (map.AddStringId(addDlg.StringId) != StringId.Zero)
                    {
                        //Add to container
                        if (englishToolStripMenuItem.Checked) strings[(StringLocale)englishToolStripMenuItem.Tag].Add(entry);
                        if (japaneseToolStripMenuItem.Checked) strings[(StringLocale)japaneseToolStripMenuItem.Tag].Add(entry);
                        if (germanToolStripMenuItem.Checked) strings[(StringLocale)germanToolStripMenuItem.Tag].Add(entry);
                        if (frenchToolStripMenuItem.Checked) strings[(StringLocale)frenchToolStripMenuItem.Tag].Add(entry);
                        if (spanishToolStripMenuItem.Checked) strings[(StringLocale)spanishToolStripMenuItem.Tag].Add(entry);
                        if (italianToolStripMenuItem.Checked) strings[(StringLocale)italianToolStripMenuItem.Tag].Add(entry);
                        if (koreanToolStripMenuItem.Checked) strings[(StringLocale)koreanToolStripMenuItem.Tag].Add(entry);
                        if (chineseToolStripMenuItem.Checked) strings[(StringLocale)chineseToolStripMenuItem.Tag].Add(entry);
                        if (portugueseToolStripMenuItem.Checked) strings[(StringLocale)portugueseToolStripMenuItem.Tag].Add(entry);

                        //Add to list
                        ListViewItem item = new ListViewItem(entry.ID);
                        item.SubItems.Add(UnicodeString.ConvertToReadable(entry.Value));
                        item.Tag = entry;

                        //Add
                        stringList.Items.Add(item);
                    }
                }
            }
        }

        private void removeStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check
            if(selectedEntry != null && stringList.SelectedIndices.Count > 0)
            {
                //Remove from container
                if (englishToolStripMenuItem.Checked) strings[(StringLocale)englishToolStripMenuItem.Tag].Remove(selectedEntry);
                if (japaneseToolStripMenuItem.Checked) strings[(StringLocale)japaneseToolStripMenuItem.Tag].Remove(selectedEntry);
                if (germanToolStripMenuItem.Checked) strings[(StringLocale)germanToolStripMenuItem.Tag].Remove(selectedEntry);
                if (frenchToolStripMenuItem.Checked) strings[(StringLocale)frenchToolStripMenuItem.Tag].Remove(selectedEntry);
                if (spanishToolStripMenuItem.Checked) strings[(StringLocale)spanishToolStripMenuItem.Tag].Remove(selectedEntry);
                if (italianToolStripMenuItem.Checked) strings[(StringLocale)italianToolStripMenuItem.Tag].Remove(selectedEntry);
                if (koreanToolStripMenuItem.Checked) strings[(StringLocale)koreanToolStripMenuItem.Tag].Remove(selectedEntry);
                if (chineseToolStripMenuItem.Checked) strings[(StringLocale)chineseToolStripMenuItem.Tag].Remove(selectedEntry);
                if (portugueseToolStripMenuItem.Checked) strings[(StringLocale)portugueseToolStripMenuItem.Tag].Remove(selectedEntry);

                //Remove from list
                stringList.Items.RemoveAt(stringList.SelectedIndices[0]);
            }
        }
    }
}
