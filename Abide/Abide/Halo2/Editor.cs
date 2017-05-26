using Abide.AddOnApi;
using Abide.Classes;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections;
using System.Windows.Forms;
using YeloDebug;

namespace Abide.Halo2
{
    /// <summary>
    /// Represents an editor for Halo 2 Map Files using the <see cref="MapFile"/> class.
    /// </summary>
    public partial class Editor : Form, IHost
    {
        private const string DemoAssembly = @"G:\Github\Abide\Abide\Abide.AddOnDemo\bin\Debug\Abide.AddOnDemo.dll";

        /// <summary>
        /// Gets or sets the visibility of the Open toolstrip button.
        /// </summary>
        public bool OpenVisible
        {
            get { return openToolStripButton.Visible; }
            set { openToolStripButton.Visible = value; }
        }

        private readonly AddOnContainer<MapFile, IndexEntry, Xbox> container;
        private readonly Xbox xbox;
        private readonly MapFile map;

        private IndexEntry selectedEntry = null;
        private string filename = string.Empty;
        
        /// <summary>
        /// Initializes a new <see cref="Editor"/>.
        /// </summary>
        private Editor()
        {
            InitializeComponent();

            //Initialize
            tagTree.TreeViewNodeSorter = new TagIdSorter();
            container = new AddOnContainer<MapFile, IndexEntry, Xbox>(MapVersion.Halo2);
            xbox = new Xbox(Application.StartupPath);

            //Load AddOns
            //TODO Load Addons Here!
            container.AddAssemblySafe(DemoAssembly);

            //Initialize AddOns
            container.BeginInit(this);

            //Loop
            foreach (var tool in container.GetTools())
            {
                //Create Item
                ToolStripMenuItem toolItem = new ToolStripMenuItem(tool.Name, tool.Icon) { Tag = tool };
                toolItem.Click += ToolItem_Click;

                //Add
                toolStripDropDownButton.DropDownItems.Add(toolItem);
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

            //Begin
            tagTree.BeginUpdate();
            tagTree.Nodes.Clear();
            tagTree.EndUpdate();

            //Setup
            Text = "Halo 2";
            tagPropertyGrid.SelectedObject = null;

            //Send trigger
            foreach (var addOn in container.GetHaloAddOns())
                addOn.OnMapLoad();
        }

        private void map_Load()
        {
            //Begin
            tagTree.BeginUpdate();
            tagTree.Nodes.Clear();

            //Load Entries
            foreach (IndexEntry entry in map.IndexEntries)
                entry_BuildTagTree(entry);

            //End
            tagTree.Sort();
            tagTree.EndUpdate();

            //Setup
            Text = $"Halo 2 - {map.Name}";
            tagPropertyGrid.SelectedObject = map;
        }

        private void entry_BuildTagTree(IndexEntry entry)
        {
            //Get Path Parts
            string[] parts = entry.Filename.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

            //Prepare
            TreeNodeCollection collection = tagTree.Nodes;
            TreeNode node = null;

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
            node.Tag = entry.ID;

            //Add
            collection.Add(node);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
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

        private void tagTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Check
            if (e.Node.Tag is TAGID)
            {
                //Setup
                selectedEntry = map.IndexEntries[(TAGID)e.Node.Tag];
                tagPropertyGrid.SelectedObject = selectedEntry;

                //Send trigger
                foreach (var addOn in container.GetHaloAddOns())
                    addOn.OnSelectedEntryChanged();
            }
            else tagPropertyGrid.SelectedObject = map;
        }
        
        private void Halo2Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Close Map
            map_Close();
            
            //Dispose
            container.Dispose();
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
            IMenuButton<MapFile, IndexEntry, Xbox> menuButton = null;

            //Get Sender
            if (sender is ToolStripButton)
            {
                //Get
                ToolStripButton Sender = (ToolStripButton)sender;
                if (Sender.Tag is IMenuButton<MapFile, IndexEntry, Xbox>)
                    menuButton = (IMenuButton<MapFile, IndexEntry, Xbox>)Sender.Tag;
            }

            //Click
            menuButton?.OnClick();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
        }

        bool IHost.InvokeRequired
        {
            get { return InvokeRequired; }
        }

        object IHost.Request(IAddOn sender, string request, params object[] args)
        {
            //Handle Request
            switch (request)
            {
                case "Map": return map;
                case "SelectedEntry": return selectedEntry;
                case "Xbox": return xbox;

                default: return null;
            }
        }

        object IHost.Invoke(Delegate method)
        {
            return Invoke(method);
        }
        
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
                if (x.Tag is TAGID && y.Tag == null)
                    return 1;
                else if (y.Tag is TAGID && x.Tag == null)
                    return -1;
                else return x.Name.CompareTo(y.Name);
            }
        }
    }
}
