using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2BetaMap;
using Abide.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Abide.Dialogs.Halo2Beta
{
    internal partial class TagBrowserDialog : Form
    {
        public TagId SelectedID
        {
            get { return selectedId; }
            set { OnSelectedIdChanged(value); }
        }
        public bool AllowNull
        {
            get { return nullButton.Visible; }
            set { nullButton.Visible = value; }
        }

        private readonly FileSystemItem filesRoot;
        private Dictionary<TagId, FileSystemItem> itemIndex;
        private FileSystemItem currentDirectory = null;
        private ListViewItem currentListItem = null;
        private TagId selectedId = TagId.Null;

        private TagBrowserDialog()
        {
            InitializeComponent();
        }

        public TagBrowserDialog(MapFile.IndexEntryList entries, string mapName)
        {
            //Init
            InitializeComponent();
            TagList.ListViewItemSorter = new TagSorter();
            itemIndex = new Dictionary<TagId, FileSystemItem>();
            filesRoot = new FileSystemItem() { Name = mapName };

            //Loop
            foreach (IndexEntry entry in entries)
                filesRoot_CreateEntry(entry.Filename, entry.Root, entry.Size == 0 ? entry.PostProcessedSize : entry.Size, entry.Id);

            //Begin Update
            TagList.BeginUpdate();
            TagList.Items.Clear();

            //Load Path
            TagList_LoadPath(filesRoot);

            //Sort
            TagList.Sort();

            //End Update
            TagList.EndUpdate();
        }

        private void TagBrowserDialog_Load(object sender, EventArgs e)
        {
            //Setup
            int result = SetWindowTheme(TagList.Handle, "explorer", null).ToInt32();
            if (result == 1) Console.WriteLine("P/Invoke Function SetWindowTheme in Uxtheme.dll returned {0} on handle {1}", result, TagList.Handle);
            TagList.View = Settings.Default.TagBrowserView;
            Size = Settings.Default.TagBrowserWindowSize;

            //Check...
            largeIconToolStripMenuItem.Checked = false;
            detailsToolStripMenuItem.Checked = false;
            smallIconToolStripMenuItem.Checked = false;
            listToolStripMenuItem.Checked = false;
            tileToolStripMenuItem.Checked = false;
            switch (Settings.Default.TagBrowserView)
            {
                case View.LargeIcon: largeIconToolStripMenuItem.Checked = true; break;
                case View.Details: detailsToolStripMenuItem.Checked = true; break;
                case View.SmallIcon: smallIconToolStripMenuItem.Checked = true; break;
                case View.List: listToolStripMenuItem.Checked = true; break;
                case View.Tile: tileToolStripMenuItem.Checked = true; break;
            }
        }

        private void filesRoot_CreateEntry(string filename, string root, long length, TagId id)
        {
            //Split
            string[] parts = $"{filename}.{root}".Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            FileSystemItem item = filesRoot;

            //Create or open folders
            for (int i = 0; i < parts.Length; i++)
            { item = item[parts[i]]; item.Type = "Folder"; }

            //Setup Tag Item
            item.IsFolder = false;
            item.Length = length;
            item.Type = root;
            item.ID = id;

            //Add
            itemIndex.Add(item.ID, item);
        }

        private void TagList_LoadPath(FileSystemItem directory)
        {
            //Set
            currentDirectory = directory;
            selectedId = TagId.Null;

            //Build Directory Path
            directoryBox.Text = string.Empty;
            FileSystemItem parent = directory.Parent;
            while (parent != null)
            { directoryBox.Text = $@"{parent.Name}\" + directoryBox.Text; parent = parent.Parent; }
            directoryBox.Text += directory.Name;

            //Check
            if (directory.Parent != null)
                TagList.Items.Add(new ListViewItem("...") { Tag = directory.Parent, ImageIndex = 0 });

            //Loop
            foreach (FileSystemItem item in directory.Children)
            {
                //Get Length
                float length = item.Length;
                string suffix = "B";
                
                //Simplify String
                if (length > 1024)      //Kibibyte
                {
                    length /= 1024;
                    suffix = "KiB";
                }
                if(length > 1024)       //Mebibyte
                {
                    length /= 1024;
                    suffix = "MiB";
                }
                if (length > 1024)      //Gibibyte
                {
                    length /= 1024;
                    suffix = "GiB";
                }
                if (length > 1024)      //Tebibyte
                {
                    length /= 1024;
                    suffix = "TiB";
                }

                //Prepare
                string[] parts = new string[4];
                parts[0] = item.Name;
                parts[1] = item.Type;
                parts[2] = item.Length > 0 ? $"{length:0} {suffix}" : string.Empty;
                parts[3] = item.ID != 0 ? item.ID.ToString() : string.Empty;

                //Create
                ListViewItem listItem = new ListViewItem(parts);
                listItem.ImageIndex = item.IsFolder ? 0 : 1;
                listItem.Tag = item;

                //Add
                TagList.Items.Add(listItem);
            }
        }

        private void TagList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //Set Current Item
            currentListItem = e.Item;

            //Get File System Item
            FileSystemItem item = (FileSystemItem)e.Item.Tag;

            //Check
            if (item != null && item.ID != 0)
                selectedId = item.ID;
        }

        private void TagList_ItemActivate(object sender, EventArgs e)
        {
            //Check
            if (currentListItem == null) return;

            //Get File System Item
            FileSystemItem item = (FileSystemItem)currentListItem.Tag;

            //Check
            if (item != null && item.ID == 0)
            {
                //Begin Update
                TagList.BeginUpdate();
                TagList.Items.Clear();

                //Load Path
                TagList_LoadPath(item);

                //Sort
                TagList.Sort();

                //End Update
                TagList.EndUpdate();
            }
            else if (item != null)
            {
                //Select Tag
                selectedId = item.ID;
                DialogResult = DialogResult.OK;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            //Check
            if (currentListItem == null) return;

            //Get File System Item
            FileSystemItem item = (FileSystemItem)currentListItem.Tag;

            //Check
            if(item.ID == 0)
            {
                //Begin Update
                TagList.BeginUpdate();
                TagList.Items.Clear();

                //Load Path
                TagList_LoadPath(item);

                //Sort
                TagList.Sort();

                //End Update
                TagList.EndUpdate();
            }
            else if (item != null)
            {
                //Select Tag
                selectedId = item.ID;
                DialogResult = DialogResult.OK;
            }
        }

        private void nullButton_Click(object sender, EventArgs e)
        {
            selectedId = TagId.Null;
            DialogResult = DialogResult.OK;
        }

        private void TagBrowserDialog_SizeChanged(object sender, EventArgs e)
        {
            //Set
            Settings.Default.TagBrowserWindowSize = Size;
        }

        private void TagListViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check...
            largeIconToolStripMenuItem.Checked = false;
            smallIconToolStripMenuItem.Checked = false;
            tileToolStripMenuItem.Checked = false;
            listToolStripMenuItem.Checked = false;
            detailsToolStripMenuItem.Checked = false;
            if (sender == largeIconToolStripMenuItem) { Settings.Default.TagBrowserView = View.LargeIcon; largeIconToolStripMenuItem.Checked = true; }
            else if (sender == smallIconToolStripMenuItem) { Settings.Default.TagBrowserView = View.SmallIcon; smallIconToolStripMenuItem.Checked = true; }
            else if (sender == tileToolStripMenuItem) { Settings.Default.TagBrowserView = View.Tile; tileToolStripMenuItem.Checked = true; }
            else if (sender == listToolStripMenuItem) { Settings.Default.TagBrowserView = View.List; listToolStripMenuItem.Checked = true; }
            else if (sender == detailsToolStripMenuItem) { Settings.Default.TagBrowserView = View.Details; detailsToolStripMenuItem.Checked = true; }

            //Set
            TagList.View = Settings.Default.TagBrowserView;
        }

        protected virtual void OnSelectedIdChanged(TagId value)
        {
            //Check
            if (!itemIndex.ContainsKey(value)) return;

            //Begin Update
            TagList.BeginUpdate();
            TagList.Items.Clear();

            //Load Path
            TagList_LoadPath(itemIndex[value].Parent);

            //Sort
            TagList.Sort();

            //Select
            foreach (ListViewItem item in TagList.Items)
                if (item.Tag is FileSystemItem && ((FileSystemItem)item.Tag).ID == value)
                { item.Selected = true; TagList.EnsureVisible(item.Index); break; }

            //End Update
            TagList.EndUpdate();
        }

        [DllImport("Uxtheme.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdLIst);

        private class TagSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x is ListViewItem && y is ListViewItem)
                {
                    //Setup
                    ListViewItem item1 = (ListViewItem)x;
                    ListViewItem item2 = (ListViewItem)y;
                    
                    //Check
                    if(item1.Text == "..." || item2.Text == "...")
                        return item1.Text == "..." ? -1 : 1;

                    //Get File System Item
                    FileSystemItem fs1 = (FileSystemItem)item1.Tag;
                    FileSystemItem fs2 = (FileSystemItem)item2.Tag;

                    //Compare...
                    if (fs1.ID.CompareTo(fs2.ID) == 0)
                        return fs1.Name.CompareTo(fs2.Name);
                    else return fs1.ID.CompareTo(fs2.ID);
                }

                return 0;
                throw new NotImplementedException();
            }
        }

        private class FileSystemItem
        {
            public FileSystemItem Parent
            {
                get { return parent; }
            }
            public FileSystemItem this[string name]
            {
                get
                {
                    //Check
                    if (nameIndex.ContainsKey(name))
                        return children[nameIndex[name]];
                    else
                    {
                        //Create
                        FileSystemItem item = new FileSystemItem(this);
                        item.Name = name;

                        //Add
                        children.Add(item);
                        nameIndex.Add(name, children.IndexOf(item));
                        return item;
                    }
                }
            }
            public FileSystemItem[] Children
            {
                get { return children.ToArray(); }
            }
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public string Type
            {
                get { return type; }
                set { type = value; }
            }
            public bool IsFolder
            {
                get { return isFolder; }
                set { isFolder = value; }
            }
            public long Length
            {
                get { return length; }
                set { length = value; }
            }
            public TagId ID
            {
                get { return id; }
                set { id = value; }
            }


            private readonly FileSystemItem parent;
            private Dictionary<string, int> nameIndex = new Dictionary<string, int>();
            private List<FileSystemItem> children = new List<FileSystemItem>();
            private string name = string.Empty;
            private string type = string.Empty;
            private bool isFolder = true;
            private long length = 0;
            private TagId id = 0;

            public FileSystemItem()
            {
                parent = null;
            }
            public FileSystemItem(FileSystemItem parent)
            {
                this.parent = parent;
            }

            public override string ToString()
            {
                return name;
            }
        }
    }
}
