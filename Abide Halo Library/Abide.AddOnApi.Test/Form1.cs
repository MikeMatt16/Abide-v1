using Abide.HaloLibrary.Halo2Map;
using System;
using System.Windows.Forms;
using System.Collections;
using Abide.HaloLibrary;

namespace Abide.AddOnApi.Test
{
    public partial class Form1 : Form
    {
        private Halo2Manager manager = new Halo2Manager();

        public Form1()
        {
            InitializeComponent();
            tagTreeView.TreeViewNodeSorter = new CustomSorter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Prepare
            string fileName = string.Empty;
            bool open = false;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "Map Files (*.map)|*.map";
                openDlg.Title = "Open File...";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    fileName = openDlg.FileName;
                    open = true;
                }
            }

            //Check
            if (open)
            {
                //Load...
                manager.LoadMap(fileName);

                //Begin
                tagTreeView.BeginUpdate();
                tagTreeView.Nodes.Clear();

                //Loop
                foreach (IndexEntry entry in manager.Map.IndexEntries)
                {
                    //Split
                    string[] parts = entry.Filename.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

                    //Loop Through...
                    TreeNodeCollection currentCollection = tagTreeView.Nodes;
                    for (int i = 0; i < parts.Length - 1; i++)
                    {
                        if (currentCollection.ContainsKey(parts[i])) currentCollection = currentCollection[parts[i]].Nodes;
                        else currentCollection = currentCollection.Add(parts[i], parts[i]).Nodes;
                    }

                    //Get Name
                    string name = $"{parts[parts.Length - 1]}.{entry.Root}";

                    //Create
                    TreeNode node = new TreeNode(name);
                    node.Tag = entry.ID;
                    node.Name = name;

                    //Add
                    currentCollection.Add(node);
                }

                //Sort
                tagTreeView.Sort();

                //End
                tagTreeView.EndUpdate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void tagTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is TAGID)
            {
                TAGID selectedId = (TAGID)e.Node.Tag;
                manager.SelectedEntry = manager.Map.IndexEntries[selectedId];
            }
            else manager.SelectedEntry = null;
        }

        private class CustomSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x is TreeNode && y is TreeNode)
                    return CompareNodes((TreeNode)x, (TreeNode)y);
                else return 0;
            }

            private int CompareNodes(TreeNode x, TreeNode y)
            {
                if (x.Tag is TAGID && y.Tag == null)
                    return 1;
                else if (x.Tag == null && y.Tag is TAGID)
                    return -1;
                else
                {
                    return x.Name.CompareTo(y.Name);
                }
            }
        }
    }
}
