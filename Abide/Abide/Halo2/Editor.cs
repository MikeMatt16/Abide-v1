using Abide.AddOnApi;
using Abide.Classes;
using Abide.Dialogs;
using Abide.Halo2.Designer;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using YeloDebug;

namespace Abide.Halo2
{
    /// <summary>
    /// Represents an editor for Halo 2 Map Files using the <see cref="MapFile"/> class.
    /// </summary>
    public partial class Editor : Form, IHost
    {
        /// <summary>
        /// Gets or sets the visibility of the Open toolstrip button.
        /// </summary>
        public bool OpenVisible
        {
            get { return openToolStripButton.Visible; }
            set { openToolStripButton.Visible = value; }
        }
        /// <summary>
        /// Gets or sets the visibility of the Options toolstrip button.
        /// </summary>
        public bool OptionsVisible
        {
            get { return optionsToolStripButton.Visible; }
            set { optionsToolStripButton.Visible = value; }
        }
        /// <summary>
        /// Gets the primary debug Xbox.
        /// </summary>
        private Xbox DebugXbox
        {
            get { if (ParentForm is Main) return ((Main)ParentForm).DebugXbox; return null; }
        }

        private readonly List<TabPage> tabPages;
        private readonly List<ToolStripButton> menuButtons;
        private readonly List<ToolStripMenuItem> contextMenuItems;
        private readonly HaloAddOnContainer<MapFile, IndexEntry, Xbox> container;
        private readonly Dictionary<TagId, IndexEntryWrapper> entries;
        private readonly MapFile map;

        private MapFileWrapper mapWrapper;
        private IndexEntry selectedEntry = null;
        private string filename = string.Empty;
        
        /// <summary>
        /// Initializes a new <see cref="Editor"/>.
        /// </summary>
        private Editor()
        {
            InitializeComponent();

            //Setup
            int result = SetWindowTheme(tagTreeView.Handle, "explorer", null).ToInt32();
            if (result == 1) Console.WriteLine("P/Invoke Function SetWindowTheme in Uxtheme.dll returned {0} on handle {1}", result, tagTreeView.Handle);
            
            tagTreeView.TreeViewNodeSorter = new TagIdSorter();
            entries = new Dictionary<TagId, IndexEntryWrapper>();
            map = new MapFile();

            //Prepare Container
            container = new HaloAddOnContainer<MapFile, IndexEntry, Xbox>(MapVersion.Halo2);
            container.BeginInit(this);

            //Load Tools
            foreach (var tool in container.GetTools())
            {
                //Create Item
                ToolStripMenuItem toolItem = new ToolStripMenuItem(tool.Name, tool.Icon) { Tag = tool };
                toolItem.ToolTipText = $"{tool.Name} by {tool.Author}{Environment.NewLine}{tool.Description}";
                toolItem.Click += ToolItem_Click;

                //Add
                toolStripDropDownButton.DropDownItems.Add(toolItem);
            }

            //Load Menu Items
            menuButtons = new List<ToolStripButton>();
            foreach (var menuButton in container.GetMenuButtons())
            {
                //Create Button
                ToolStripButton button = new ToolStripButton(menuButton.Name, menuButton.Icon);
                button.ToolTipText = $"{menuButton.Name} by {menuButton.Author}{Environment.NewLine}{menuButton.Description}";
                button.Click += MenuButton_Click;
                button.Name = menuButton.Name;
                button.Tag = menuButton;
                
                //Add
                menuButtons.Add(button);

                //Check
                if (!menuButton.ApplyFilter) mapToolStrip.Items.Add(button);
            }

            //Load Context Menu Items
            contextMenuItems = new List<ToolStripMenuItem>();
            foreach (var contextMenuItem in container.GetContextMenuItems())
            {
                //Create Button
                ToolStripMenuItem item = new ToolStripMenuItem(contextMenuItem.Name, contextMenuItem.Icon);
                item.ToolTipText = $"{contextMenuItem.Name} by {contextMenuItem.Author}{Environment.NewLine}{contextMenuItem.Description}";
                item.Click += MenuButton_Click;
                item.Name = contextMenuItem.Name;
                item.Tag = contextMenuItem;

                //Add
                contextMenuItems.Add(item);

                //Check
                if (!contextMenuItem.ApplyFilter) tagContextMenu.Items.Add(item);
            }

            //Load Tab Pages
            tabPages = new List<TabPage>();
            foreach (var tabPage in container.GetTabPages())
            {
                //Prepare
                tabPage.UserInterface.Dock = DockStyle.Fill;

                //Create Tab Page
                TabPage page = new TabPage(tabPage.Name);
                page.ToolTipText = $"{tabPage.Name} by {tabPage.Author}{Environment.NewLine}{tabPage.Description}";
                page.Controls.Add(tabPage.UserInterface);
                page.Name = tabPage.Name;
                page.Tag = tabPage;
                
                //Add
                tabPages.Add(page);

                //Check
                if (!tabPage.ApplyFilter) tagTabControl.TabPages.Add(page);
            }
        }
        /// <summary>
        /// Initializes a new <see cref="Editor"/> using the specified Halo map file.
        /// </summary>
        /// <param name="map">The <see cref="MapFile"/> to load into the editor.</param>
        public Editor(MapFile map) : this()
        {
            //Setup
            this.map = map;

            //Send trigger
            foreach (var addOn in container.GetHaloAddOns())
                addOn.OnMapLoad();
        }
        /// <summary>
        /// Initializes a new <see cref="Editor"/> using the specified Halo map file name.
        /// </summary>
        /// <param name="filename">The file path to a Halo map file.</param>
        public Editor(string filename) : this(new MapFile())
        {
            //Close
            map_Close();

            //Load
            map.Load(filename);
            this.filename = filename;

            //Load
            map_Load();
        }

        private void map_Close()
        {
            //Close
            map.Close();
            entries.Clear();

            //Begin
            tagTreeView.BeginUpdate();
            tagTreeView.Nodes.Clear();
            tagTreeView.EndUpdate();

            //Setup
            Text = "Halo 2";
            TagPropertyGrid.SelectedObject = null;

            //Send trigger
            List<Exception> errors = new List<Exception>();
            foreach (var addOn in container.GetHaloAddOns())
                try { addOn.OnMapLoad(); }
                catch (Exception ex) { errors.Add(ex); }
        }

        private void map_Load()
        {
            //Begin
            tagTreeView.BeginUpdate();
            tagTreeView.Nodes.Clear();
            entries.Clear();

            //Load Entries
            foreach (IndexEntry entry in map.IndexEntries)
                entry_BuildTagTree(entry);

            //Load Map Wrapper
            mapWrapper = new MapFileWrapper(map.Name, map.Strings, entries[map.Scenario.Id]);

            //End
            tagTreeView.Sort();
            tagTreeView.EndUpdate();

            //Setup
            Text = $"Halo 2 - {map.Name}";
            TagPropertyGrid.SelectedObject = mapWrapper;

            //Send trigger
            List<Exception> errors = new List<Exception>();
            foreach (var addOn in container.GetHaloAddOns())
                try { addOn.OnMapLoad(); }
                catch (Exception ex) { errors.Add(ex); }
        }

        private TreeNode entry_BuildTagTree(IndexEntry entry)
        {
            //Prepare
            TreeNodeCollection collection = null;
            IndexEntryWrapper wrapper = null;
            TreeNode node = null;

            //Get or create wrapper...
            if (entries.ContainsKey(entry.Id)) wrapper = entries[entry.Id];
            else { wrapper = IndexEntryWrapper.FromEntry(entry, map); wrapper.FilenameChanged += Wrapper_FilenameChanged; entries.Add(entry.Id, wrapper); }

            //Get Path Parts
            string[] parts = entry.Filename.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            string last = parts[parts.Length - 1];

            //Check...
            if ((!string.IsNullOrEmpty(tagSearchBox.Text) && string.Join(".", last, entry.Root).ToLower().Contains(tagSearchBox.Text.ToLower())) || string.IsNullOrEmpty(tagSearchBox.Text))
            {
                //Prepare
                collection = tagTreeView.Nodes;

                //Loop
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    //Create or get node
                    if (collection.ContainsKey(parts[i])) node = collection[parts[i]];
                    else node = collection.Add(parts[i], parts[i]);

                    //Setup...
                    node.ImageIndex = 0; node.SelectedImageIndex = 0;
                    collection = node.Nodes;
                }

                //Create Node
                string name = $"{parts[parts.Length - 1]}.{entry.Root}";
                node = new TreeNode(name, 1, 1);
                node.Name = name;
                node.Tag = entry.Id;

                //Add
                collection.Add(node);

                //Return
                return node;
            }
            else return null;
        }

        private void Wrapper_FilenameChanged(object sender, EventArgs e)
        {
            //Prepare...
            IndexEntryWrapper wrapper = sender as IndexEntryWrapper;
            TreeNode node = tagTreeView.SelectedNode;

            //Check
            if (wrapper != null && node != null)
            {
                //Set filename...
                map.IndexEntries[wrapper.ID].Filename = wrapper.Filename;

                //Prepare
                TreeNodeCollection collection = tagTreeView.Nodes;
                TreeNode parent = null;

                //Loop
                while (node.Parent != null)
                {
                    //Get Parent
                    parent = node.Parent;
                    collection = parent.Nodes;

                    //Remove
                    if (node.Nodes.Count == 0) collection.Remove(node);

                    //Set
                    node = parent;
                }

                //Remove from tree...
                if (node.Nodes.Count == 0) tagTreeView.Nodes.Remove(node);

                //Build
                node = entry_BuildTagTree(map.IndexEntries[wrapper.ID]);

                //Re-sort...
                tagTreeView.BeginUpdate();
                tagTreeView.Sort();
                tagTreeView.EndUpdate();

                //Select
                tagTreeView.SelectedNode = node;
                node.EnsureVisible();
            }
        }

        private void TagTree_DragEnter(object sender, DragEventArgs e)
        {
            //Check
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void TagTree_DragDrop(object sender, DragEventArgs e)
        {
            //Prepare
            AbideTagFile Tag = new AbideTagFile();
            FileInfo info = null;

            //Get Files...
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
                foreach (string filename in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    //Get File Info
                    info = new FileInfo(filename);

                    //Check
                    if(info.Extension == ".aTag" && info.Length > 16)
                    {
                        //Load ATag
                        Tag.Load(info.FullName);
                    }
                }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            //Check
            if (!File.Exists(filename) || string.IsNullOrEmpty(filename))
                using (SaveFileDialog saveDlg = new SaveFileDialog())
                {
                    //Setup
                    saveDlg.Filter = "Halo 2 Maps (*map)|*.map";
                    saveDlg.FileName = filename;

                    //Show
                    if (saveDlg.ShowDialog() == DialogResult.OK)
                        filename = saveDlg.FileName;
                }

            //Save
            map.Save(filename);
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            //Prepare
            string filename = string.Empty;
            bool open = false;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "Halo 2 Map Files (*.map)|*.map";
                openDlg.Title = "Open Halo 2 Map...";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    filename = openDlg.FileName;
                    open = true;
                }
            }

            //Check
            if (open)
            {
                //Close
                map_Close();

                //Load
                map.Load(filename);

                //Set
                this.filename = filename;

                //Send trigger
                foreach (var addOn in container.GetHaloAddOns())
                    addOn.OnMapLoad();

                //Load
                map_Load();
            }
        }

        private void optionsToolStripButton_Click(object sender, EventArgs e)
        {
            //Create Options Dialog
            using (OptionsDialog optDlg = new OptionsDialog())
                optDlg.ShowDialog();
        }

        private void TagTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Check
            if (e.Node.Tag is TagId)
            {
                //Setup
                selectedEntry = map.IndexEntries[(TagId)e.Node.Tag];
                TagPropertyGrid.SelectedObject = entries[selectedEntry.Id];

                //Send trigger
                List<Exception> errors = new List<Exception>();
                foreach (var addOn in container.GetHaloAddOns())
                    try { addOn.OnSelectedEntryChanged(); }
                    catch(Exception ex) { errors.Add(ex); }

                //Check Tab Pages
                foreach (TabPage tabPage in tabPages)
                {
                    //Get ITabPage
                    ITabPage<MapFile, IndexEntry, Xbox> page = (ITabPage<MapFile, IndexEntry, Xbox>)tabPage.Tag;

                    //Check
                    if (page.ApplyFilter)
                        if (page.Filter.Contains(selectedEntry.Root))
                        {
                            if (!tagTabControl.TabPages.ContainsKey(tabPage.Name))
                            { tagTabControl.TabPages.Add(tabPage); tagTabControl.SelectedTab = tabPage; }
                        }
                        else tagTabControl.TabPages.RemoveByKey(tabPage.Name);
                }

                //Check Menu Buttons
                foreach (ToolStripButton menuButton in menuButtons)
                {
                    //Get ITabPage
                    IMenuButton<MapFile, IndexEntry, Xbox> button = (IMenuButton<MapFile, IndexEntry, Xbox>)menuButton.Tag;

                    //Check
                    if (button.ApplyFilter)
                        if (button.Filter.Contains(selectedEntry.Root))
                        {
                            if (!mapToolStrip.Items.ContainsKey(menuButton.Name))
                                mapToolStrip.Items.Add(menuButton);
                        }
                        else mapToolStrip.Items.RemoveByKey(menuButton.Name);
                }

                //Check Context Menu Buttons
                foreach (ToolStripMenuItem menuItem in contextMenuItems)
                {
                    //Get IContextMenu
                    IContextMenuItem<MapFile, IndexEntry, Xbox> item = (IContextMenuItem<MapFile, IndexEntry, Xbox>)menuItem.Tag;

                    //Check
                    if (item.ApplyFilter)
                        if (item.Filter.Contains(selectedEntry.Root))
                        {
                            if (!tagContextMenu.Items.ContainsKey(menuItem.Name))
                                tagContextMenu.Items.Add(menuItem);
                        }
                        else tagContextMenu.Items.RemoveByKey(menuItem.Name);
                }
            }
            else TagPropertyGrid.SelectedObject = mapWrapper;
        }
        
        private void Halo2Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Close Map
            map_Close();
            
            //Dispose
            container.Dispose();
        }

        private void saveTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check
            if (selectedEntry == null) return;

            //Prepare
            AbideTagFile TagFile = new AbideTagFile();
            string filename = string.Empty;
            bool save = false;

            //Initialize
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                //Setup
                saveDlg.Filter = "Abide Tag Files (*.aTag)|*.aTag";
                saveDlg.Title = "Save Tag as...";
                saveDlg.FileName = $"{selectedEntry.Filename.Split('\\').Last()}.{selectedEntry.Root}";
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    filename = saveDlg.FileName;
                    save = true;
                }
            }

            //Check
            if (save)
            {
                //Load from entry...
                TagFile.LoadEntry(selectedEntry);

                //Save
                TagFile.Save(filename);
            }
        }

        private void tagSearchBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //Check for return...
            if (e.KeyCode == Keys.Return)
            {
                //Begin
                tagTreeView.BeginUpdate();
                tagTreeView.Nodes.Clear();
                entries.Clear();

                //Load Entries
                foreach (IndexEntry entry in map.IndexEntries)
                    entry_BuildTagTree(entry);

                //End
                tagTreeView.Sort();
                tagTreeView.EndUpdate();

                //Expand?
                if (!string.IsNullOrEmpty(tagSearchBox.Text)) tagTreeView.ExpandAll();
            }

            //Check for escape...
            if (e.KeyCode == Keys.Escape)
            {
                //Clear
                tagSearchBox.Text = string.Empty;

                //Begin
                tagTreeView.BeginUpdate();
                tagTreeView.Nodes.Clear();
                entries.Clear();

                //Load Entries
                foreach (IndexEntry entry in map.IndexEntries)
                    entry_BuildTagTree(entry);

                //End
                tagTreeView.Sort();
                tagTreeView.EndUpdate();
            }
        }

        private void ToolItem_Click(object sender, EventArgs e)
        {
            //Prepare
            ITool<MapFile, IndexEntry, Xbox> tool = null;

            //Get Sender
            if (sender is ToolStripMenuItem)
            {
                //Get
                ToolStripMenuItem Sender = (ToolStripMenuItem)sender;
                if(Sender.Tag is ITool<MapFile, IndexEntry, Xbox> )
                    tool = (ITool<MapFile, IndexEntry, Xbox>)Sender.Tag;
            }

            //Check
            if (tool != null && tool.UserInterface != null)
            {
                //Setup
                tool.UserInterface.Dock = DockStyle.Fill;

                //Begin
                toolPanel.SuspendLayout();

                //Set
                toolPanel.Controls.Clear();
                toolPanel.Controls.Add(tool.UserInterface);

                //Resume
                toolPanel.ResumeLayout();
            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            //Prepare
            IMenuButton<MapFile, IndexEntry, Xbox> menuItem = null;
            IContextMenuItem<MapFile, IndexEntry, Xbox> contextMenuItem = null;

            //Get Sender
            if (sender is ToolStripItem)
            {
                //Get
                ToolStripItem Sender = (ToolStripItem)sender;
                if (Sender.Tag is IMenuButton<MapFile, IndexEntry, Xbox>) menuItem = (IMenuButton<MapFile, IndexEntry, Xbox>)Sender.Tag;
                if (Sender.Tag is IContextMenuItem<MapFile, IndexEntry, Xbox>) contextMenuItem = (IContextMenuItem<MapFile, IndexEntry, Xbox>)Sender.Tag;
            }

            //Click
            menuItem?.OnClick();
            contextMenuItem?.OnClick();
        }
        
        object IHost.Request(IAddOn sender, string request, params object[] args)
        {
            //Prepare
            int value = 0;
            RawSection section = 0;
            StringId stringId = StringId.Zero;
            TagId tagId = TagId.Null;
            Stream dataStream = null;
            IndexEntry entry = null;

            //Handle Request
            switch (request)
            {
                case "Map": return map;
                case "SelectedEntry": return selectedEntry;
                case "Xbox": return DebugXbox;
                case "StringBrowserDialog":
                    //Prepare
                    if (args.Length > 0 && args[0] is StringId) stringId = (StringId)args[0];

                    //Initialize Tag Browser Dialog
                    using (StringSelectDialog stringDlg = new StringSelectDialog(map.Strings.ToList()))
                    {
                        //Set
                        stringDlg.SelectedString = stringId;

                        //Show
                        if (stringDlg.ShowDialog() == DialogResult.OK)
                            stringId = stringDlg.SelectedString;
                    }

                    //Return
                    return stringId;
                case "TagBrowserDialog":
                    //Prepare
                    if (args.Length > 0 && args[0] is TagId) tagId = (TagId)args[0];

                    //Initialize Tag Browser Dialog
                    using (TagBrowserDialog tagDlg = new TagBrowserDialog(map.IndexEntries, map.Name))
                    {
                        //Set
                        tagDlg.SelectedID = tagId;

                        //Show
                        if (tagDlg.ShowDialog() == DialogResult.OK)
                            tagId = tagDlg.SelectedID;
                    }

                    //Return
                    return tagId;
                case "SelectEntry":
                    //Prepare
                    if (args.Length > 0 && args[0] is TagId) tagId = (TagId)args[0];

                    //Check ID...
                    if (!tagId.IsNull && entries.ContainsKey(tagId))
                    {
                        //Select
                        var wrapper = entries[tagId];
                        string[] parts = $"{wrapper.Filename}.{wrapper.Root}".Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                        TreeNodeCollection collection = tagTreeView.Nodes; TreeNode node = null;
                        for (int i = 0; i < parts.Length; i++)
                        {
                            //Get Node
                            node = collection[parts[i]];

                            //Check
                            if (node != null) collection = node.Nodes;
                        }

                        //Check
                        if(node != null)
                        {
                            //Select and goto
                            tagTreeView.SelectedNode = node;
                            node.EnsureVisible();
                        }
                    }
                    return selectedEntry;
                case "RawDataStream":
                    //Check Parameters
                    if (args.Length > 2 && (args[0] is IndexEntry || args[0] is TagId) && args[1] is RawSection && args[2] is int)
                    {
                        //Get Parameters
                        if (args[0] is IndexEntry) entry = (IndexEntry)args[0];
                        else entry = map.IndexEntries[(TagId)args[0]];
                        section = (RawSection)args[1];
                        value = (int)args[2];

                        //Check
                        if (entry != null)
                            switch (value & 0xC0000000 >> 29)
                            {
                                case 0: if (entry.Raws[section].ContainsRawOffset(value)) dataStream = (Stream)entry.Raws[section][value].Clone(); break;
                                case 1: break;
                                case 2: break;
                                case 3: break;
                            }
                    }
                    return dataStream;
                default: return null;
            }
        }

        /// <summary>
        /// Causes a window to use a different set of visual style information than its class normally uses.
        /// </summary>
        /// <param name="hwnd">Handle to the window whose visual style information is to be changed.</param>
        /// <param name="pszSubAppName">A string that contains the application name to use in place of the calling application's name. If this parameter is null, the calling application's name is used.</param>
        /// <param name="pszSubIdList">A string that contains a semicolon-separated list of CLSID names to use in place of the actual list passed by the window's class. If this parameter is null, the ID list from the calling class is used.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [DllImport("Uxtheme.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        /// <summary>
        /// Represents a Tag ID Sorter.
        /// </summary>
        private class TagIdSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x is TreeNode && y is TreeNode)
                    return Compare((TreeNode)x, (TreeNode)y);
                else return 0;
            }
            public int Compare(TreeNode x, TreeNode y)
            {
                if (x.Tag is TagId && y.Tag == null)
                    return 1;
                else if (y.Tag is TagId && x.Tag == null)
                    return -1;
                else return x.Name.CompareTo(y.Name);
            }
        }

        /// <summary>
        /// Represents a wraper to a <see cref="MapFile"/> object.
        /// </summary>
        private class MapFileWrapper
        {
            /// <summary>
            /// Gets or sets the name of the map.
            /// </summary>
            [Category("Map Properties"), Description("The name of the map.")]
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            /// <summary>
            /// Gets or sets the map's scenario.
            /// </summary>
            [Category("Map Properties"), Description("The map's scenario.")]
            [Editor(typeof(IndexEntryWrapper.IndexEntryWrapperEditor), typeof(UITypeEditor))]
            public IndexEntryWrapper Scenario
            {
                get { return scenario; }
                set { scenario = value; }
            }
            /// <summary>
            /// Gets and returns the map's string list.
            /// </summary>
            [Category("Map Properties"), Description("The map's string list.")]
            [Editor(typeof(StringsListEditor), typeof(UITypeEditor))]
            public MapFile.StringList Strings
            {
                get { return strings; }
            }
            
            private string name;
            private IndexEntryWrapper scenario;
            private readonly MapFile.StringList strings;

            /// <summary>
            /// Initializes a new <see cref="MapFileWrapper"/> instance.
            /// </summary>
            /// <param name="mapName">The name of the map.</param>
            /// <param name="strings">The map's strings.</param>
            /// <param name="scenario">The map's scenario ID.</param>
            public MapFileWrapper(string mapName, MapFile.StringList strings, IndexEntryWrapper scenario)
            {
                this.name = mapName;
                this.strings = strings;
                this.scenario = scenario;
            }
            /// <summary>
            /// Returns the name of the map.
            /// </summary>
            /// <returns>The map name.</returns>
            public override string ToString()
            {
                return name;
            }
        }

        /// <summary>
        /// Represents a wrapper to an <see cref="IndexEntry"/> object.
        /// </summary>
        private class IndexEntryWrapper
        {
            /// <summary>
            /// Occurs when the index entry's filename is changed.
            /// </summary>
            [Category("Property Changed Events"), Description("Occurs when the index entry's filename is changed.")]
            public event EventHandler FilenameChanged
            {
                add { filenameChanged += value; }
                remove { filenameChanged -= value; }
            }
            /// <summary>
            /// Gets and returns the root of the index entry.
            /// </summary>
            [Category("Tag Properties"), Description("The root of the index entry.")]
            public Tag Root
            {
                get { return root; }
            }
            /// <summary>
            /// Gets or sets the filename of the index entry.
            /// </summary>
            [Category("Tag Properties"), Description("The filename of the index entry")]
            public string Filename
            {
                get { return filename; }
                set { filename = value; filenameChanged?.Invoke(this, new EventArgs()); }
            }
            /// <summary>
            /// Gets and returns the ID of the index entry.
            /// </summary>
            [Category("Tag Properties"), Description("The ID of the index entry.")]
            public TagId ID
            {
                get { return id; }
            }
            /// <summary>
            /// Gets and returns the offset at which the Tag data begins within <see cref="TagData"/>.
            /// </summary>
            [Category("Tag Properties"), Description("The offset where the Tag data begins.")]
            public uint Offset
            {
                get { return offset; }
            }
            /// <summary>
            /// Gets and returns the length of the Tag data.
            /// </summary>
            [Category("Tag Properties"), Description("The length of the Tag data")]
            public uint Size
            {
                get { return size; }
            }
            /// <summary>
            /// Gets and returns the Tag data stream of the index entry.
            /// </summary>
            [Browsable(false)]
            public Stream TagData
            {
                get { return tagData; }
            }

            private event EventHandler filenameChanged;
            
            private string filename;
            private readonly Tag root;
            private readonly TagId id;
            private readonly uint offset;
            private readonly uint size;
            private readonly Stream tagData;
            private readonly MapFile mapFile;

            /// <summary>
            /// Initializes a new <see cref="IndexEntryWrapper"/> instance.
            /// </summary>
            /// <param name="mapFile">The map file that contains this entry.</param>
            /// <param name="root">The root of the entry.</param>
            /// <param name="filename">The filename of the entry.</param>
            /// <param name="id">The ID of the entry.</param>
            /// <param name="tagData">The Tag data stream of the entry.</param>
            /// <param name="offset">The offset of the entry.</param>
            /// <param name="size">The size of the entry.</param>
            private IndexEntryWrapper(MapFile mapFile, Tag root, string filename, TagId id, FixedMemoryMappedStream tagData, uint offset, uint size)
            {
                //Setup
                this.mapFile = mapFile;
                this.root = root;
                this.filename = filename;
                this.id = id;
                this.tagData = tagData;
                this.offset = offset;
                this.size = size;
            }
            /// <summary>
            /// Creates a new <see cref="IndexEntryWrapper"/> instance from a <see cref="IndexEntry"/> object and <see cref="MapFile"/>.
            /// </summary>
            /// <param name="entry">The entry to wrap.</param>
            /// <param name="mapFile">The map that owns the entry.</param>
            /// <returns>A new <see cref="IndexEntryWrapper"/> whose values are referenced from the supplied entry.</returns>
            public static IndexEntryWrapper FromEntry(IndexEntry entry, MapFile mapFile)
            {
                //Get offset and size...
                int offset = entry.PostProcessedOffset != 0 ? entry.PostProcessedOffset : (int)entry.Offset;
                int size = entry.PostProcessedSize != 0 ? entry.PostProcessedSize : entry.Size;

                //Return
                return new IndexEntryWrapper(mapFile, entry.Root, entry.Filename, entry.Id, entry.TagData, (uint)offset, (uint)size);
            }
            /// <summary>
            /// Gets a string representation of the Index Entry.
            /// </summary>
            /// <returns>A string containing the full path of the entry.</returns>
            public override string ToString()
            {
                return $"{filename}.{root}";
            }

            /// <summary>
            /// Represents an Index Entry wrapper design-time type editor.
            /// </summary>
            public class IndexEntryWrapperEditor : UITypeEditor
            {
                /// <summary>
                /// Gets the editor style used by the <see cref="EditValue(ITypeDescriptorContext, IServiceProvider, object)"/> method.
                /// </summary>
                /// <param name="context">The context to get additional information.</param>
                /// <returns>The editor style.</returns>
                public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
                {
                    return UITypeEditorEditStyle.Modal;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="context"></param>
                /// <param name="provider"></param>
                /// <param name="value"></param>
                /// <returns></returns>
                public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
                {
                    //Prepare
                    IndexEntryWrapper wrappedEntry;
                    IndexEntry entry;

                    //Check
                    if (value is IndexEntryWrapper)
                    {
                        //Get Entry
                        wrappedEntry = (IndexEntryWrapper)value;

                        //Get Index Entry
                        entry = SelectEntry(wrappedEntry.mapFile, wrappedEntry.mapFile.IndexEntries[wrappedEntry.id]);

                        //Create Wrapper
                        return new IndexEntryWrapper(wrappedEntry.mapFile, entry.Root, entry.Filename, entry.Id, entry.TagData, entry.Offset, (uint)entry.Size);
                    }

                    //Return
                    return value;
                }

                protected virtual IndexEntry SelectEntry(MapFile map, IndexEntry entry)
                {
                    //Prepare
                    IndexEntry result = entry;

                    //Create Dialog
                    using (TagBrowserDialog tagDlg = new TagBrowserDialog(map.IndexEntries, map.Name))
                    {
                        //Setup
                        tagDlg.AllowNull = false;
                        tagDlg.SelectedID = entry.Id;

                        //Show
                        if (tagDlg.ShowDialog() == DialogResult.OK)
                            result = map.IndexEntries[tagDlg.SelectedID];
                    }

                    //Return
                    return entry;
                }
            }
        }
    }
}
