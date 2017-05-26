using Abide.AddOnApi;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using YeloDebug;

namespace Abide.Halo2
{
    public partial class Editor : Form, IHost
    {
        private const string AssemblyDemo = @"G:\Github\Abide\Abide\Abide.AddOnDemo\bin\Debug\Abide.AddOnDemo.dll";

        private readonly Dictionary<string, AddOnFactory> factories;
        private readonly MapFile map;
        private IndexEntry selectedEntry;
        private Xbox xbox;

        private Editor()
        {
            InitializeComponent();

            //Setup
            tagTree.TreeViewNodeSorter = new TagIdSorter();
            factories = new Dictionary<string, AddOnFactory>();
        }
        public Editor(MapFile map) : this()
        {
            //Setup
            this.map = map;
            xbox = new Xbox(Application.StartupPath);
        }
        public Editor(string filename) : this(new MapFile())
        {
            //Load
            map.Load(filename);

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

        private void tagTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Check
            if (e.Node.Tag is TAGID)
            {
                selectedEntry = map.IndexEntries[(TAGID)e.Node.Tag];
                OnSelectedEntryChanged(map.IndexEntries[(TAGID)e.Node.Tag]);
                tagPropertyGrid.SelectedObject = selectedEntry;
            }
            else tagPropertyGrid.SelectedObject = map;
        }

        protected virtual void OnSelectedEntryChanged(IndexEntry indexEntry)
        {
            //Setup
        }

        private void Halo2Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Close Map
            map.Close();
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
            throw new NotImplementedException();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            //Load Demo
            assembly_Load(AssemblyDemo);

            //TODO: Implement mass-assembly loading for addon assemblies.

            //Initialize AddOns
            foreach (KeyValuePair<string, AddOnFactory> factory in factories)
                addOns_FindInterfaces(factory.Value);
        }

        private void assembly_Load(string filename)
        {
            //Prepare
            AddOnFactory factory = null;
            string directory = Path.GetDirectoryName(filename);

            //Create or get factory...
            if (!factories.ContainsKey(directory))
            {
                //Create
                factory = new AddOnFactory() { AddOnDirectory = directory };
                factories.Add(directory, factory);
            }
            else factory = factories[directory];

            //Load Assembly
            try { factory.LoadAssembly(filename); }
            catch (Exception ex) { Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace); }
        }

        private void addOns_FindInterfaces(AddOnFactory factory)
        {
            //Check Types
            foreach (Type type in factory.AddOnTypes)
            {
                //Prepare...
                var halo = type.GetInterface(typeof(IHaloAddOn<MapFile, IndexEntry>).Name);
                var tool = type.GetInterface(typeof(ITool<MapFile, IndexEntry, Xbox>).Name);
                var menuButton = type.GetInterface(typeof(IMenuButton<MapFile, IndexEntry, Xbox>).Name);
                var tabPage = type.GetInterface(typeof(ITabPage<MapFile, IndexEntry, Xbox>).Name);
                var assemblyName = type.Assembly.GetName().Name;

                //Check
                if (halo != null && factory.CreateInstance<IHaloAddOn<MapFile, IndexEntry>>(assemblyName, type.FullName).Version == MapVersion.Halo2)
                {
                    //Initialize...
                    if (tool != null) addOn_InitializeTool(factory, assemblyName, type.FullName);
                    if (menuButton != null) addOn_InitializeMenuButton(factory, assemblyName, type.FullName);
                    if (tabPage != null) addOn_InitializeTabPage(factory, assemblyName, type.FullName);
                }
            }
        }

        private void addOn_InitializeTool(AddOnFactory factory, string assemblyName, string typeFullName)
        {
            //Create Instance
            ITool<MapFile, IndexEntry, Xbox> tool = factory.CreateInstance<ITool<MapFile, IndexEntry, Xbox>>(assemblyName, typeFullName);
            if (tool != null)
            {
                //Initialize
                tool.Initialize(this);

                //Create Menu Item
                ToolStripMenuItem toolItem = new ToolStripMenuItem(tool.Name, tool.Icon);
                toolItem.Tag = tool;
                toolItem.Click += ToolItem_Click;

                //Add
                toolStripDropDownButton.DropDownItems.Add(toolItem);
            }
        }

        private void addOn_InitializeTabPage(AddOnFactory factory, string assemblyName, string typeFullName)
        {
            //Create Instance
            ITabPage<MapFile, IndexEntry, Xbox> tabPage = factory.CreateInstance<ITabPage<MapFile, IndexEntry, Xbox>>(assemblyName, typeFullName);
            if (tabPage != null)
            {
                //Initialize
                tabPage.Initialize(this);
            }
        }

        private void addOn_InitializeMenuButton(AddOnFactory factory, string assemblyName, string typeFullName)
        {
            //Create Instance
            IMenuButton<MapFile, IndexEntry, Xbox> menuButton = factory.CreateInstance<IMenuButton<MapFile, IndexEntry, Xbox>>(assemblyName, typeFullName);
            if (menuButton != null)
            {
                //Initialize
                menuButton.Initialize(this);

                //Create Menu Item
                ToolStripButton toolButton = new ToolStripButton(menuButton.Name, menuButton.Icon);
                toolButton.Tag = menuButton;
                toolButton.Click += MenuButton_Click;

                //Add
                toolStrip1.Items.Add(toolButton);
            }
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
