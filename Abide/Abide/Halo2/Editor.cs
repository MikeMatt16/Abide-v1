using Abide.AddOnApi;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections;
using System.Windows.Forms;
using YeloDebug;

namespace Abide.Halo2
{
    public partial class Editor : Form, IHost
    {
        private readonly MapFile map;
        private IndexEntry selectedEntry;
        private Xbox xbox;

        private Editor()
        {
            InitializeComponent();
            tagTree.TreeViewNodeSorter = new TagIdSorter();
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
