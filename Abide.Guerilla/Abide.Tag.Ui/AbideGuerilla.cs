﻿using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Abide.Tag.Ui
{
    public partial class AbideGuerilla : Form
    {
        private class TagNodeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x is TreeNode node1 && y is TreeNode node2)
                    return Compare(node1, node2);
                else return 0;
            }
            public int Compare(TreeNode node1, TreeNode node2)
            {
                int result = 0;
                IndexEntry entry1 = node1.Tag as IndexEntry;
                IndexEntry entry2 = node2.Tag as IndexEntry;

                if (entry1 == null && entry2 == null) return node1.Name.CompareTo(node2.Name);
                else if (entry1 != null && entry2 == null) result = 1;
                else if (entry1 == null && entry2 != null) result = -1;
                else if (entry1 != null && entry2 != null)
                {
                    result = entry1.Filename.CompareTo(entry2.Filename);
                    if (result == 0) return entry1.Root.CompareTo(entry2.Root);
                }
                return result;
            }
        }

        private Dictionary<IndexEntry, Form> tagForms = new Dictionary<IndexEntry, Form>();

        public AbideGuerilla()
        {
            InitializeComponent();
        }

        private void Tag_Selected(IndexEntry tag)
        {
            //Check
            if (tagForms.ContainsKey(tag))
            { tagForms[tag].BringToFront(); return; }

            //Create
            // Guerilla.TagForm tagForm = new Guerilla.TagForm(map, tag) { MdiParent = this };
            // tagForm.FormClosed += TagForm_FormClosed;

            //Show
            // tagForm.Show();
        }

        private void TagForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Check
            if(sender is Guerilla.TagForm tagForm)
            {
                //Remove
                tagForms.Remove(tagForm.Tag);
                tagForm.Dispose();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Title = "Open Halo 2 Map";
                openDlg.Filter = "Halo Map Files (*.map)|*.map";

                //Show
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Not implemented");
                }
            }
        }

        private void tagsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Check
            if (e.Node.Tag is IndexEntry entry)
                Tag_Selected(entry);
        }
    }
}
