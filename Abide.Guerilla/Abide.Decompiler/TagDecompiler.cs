using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abide.Decompiler
{
    public partial class TagDecompiler : Form
    {
        private readonly MapFile m_MapFile = new MapFile();

        public TagDecompiler(MapFile mapFile) : this()
        {
            m_MapFile = mapFile ?? throw new ArgumentNullException(nameof(mapFile));
            Map_Load(m_MapFile);

        }
        public TagDecompiler(string fileName) : this()
        {
            //Load
            m_MapFile.Load(fileName ?? throw new ArgumentNullException(nameof(fileName)));
            Map_Load(m_MapFile);
        }
        private TagDecompiler()
        {
            InitializeComponent();
        }

        private void Map_Load(MapFile map)
        {
            //Prepare
            tagTreeView.BeginUpdate();
            tagTreeView.Nodes.Clear();
            tagTreeView.PathSeparator = "\\";

            //Loop
            foreach (IndexEntry indexEntry in map.IndexEntries)
                TreeView_CreateTagNode(tagTreeView, $"{indexEntry.Filename}.{ indexEntry.Root.FourCc}", indexEntry.Id);

            //End
            tagTreeView.TreeViewNodeSorter = new TagNodeSorter();
            tagTreeView.Sort();
            tagTreeView.EndUpdate();
        }

        private void TreeView_CreateTagNode(TreeView treeView, string fileName, TagId id)
        {
            //Split
            TreeNode node = null;
            string[] parts = fileName.Split('\\');
            TreeNodeCollection collection = treeView.Nodes;
            string lastPart = parts.Last();

            //Loop
            for (int i = 0; i < parts.Length - 1; i++)
            {
                //Check
                if (!collection.ContainsKey(parts[i]))
                    node = collection.Add(parts[i], parts[i], 0);
                else node = collection[parts[i]];

                //Get nodes
                collection = node.Nodes;
            }

            //Add file node
            node = collection.Add(lastPart, lastPart, 1);
            node.Tag = id;
        }

        private void tagTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //Check
        }

        private class TagNodeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x is TreeNode n1 && y is TreeNode n2)
                    return CompareNodes(n1, n2);
                return 0;
            }

            private int CompareNodes(TreeNode n1, TreeNode n2)
            {
                if (n1.Tag == null && n2.Tag != null) return -1;
                else if (n1.Tag != null && n2.Tag == null) return 1;
                else return n1.Name.CompareTo(n2.Name);
            }
        }
    }
}
