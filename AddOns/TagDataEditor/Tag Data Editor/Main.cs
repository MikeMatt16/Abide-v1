using Abide.AddOnApi;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tag_Data_Editor.Halo2;
using YeloDebug;

namespace Tag_Data_Editor
{
    public partial class Main : Form, IHost
    {
        private Dictionary<IAddOn, uint> Editors = new Dictionary<IAddOn, uint>();
        private MapFile Map { get; set; } = new MapFile();

        public Main()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            TreeNode currentNode = null;
            TreeNodeCollection currentCollection = null;
            string[] parts = null;
            
            //Start
            tagTreeView.BeginUpdate();
            tagTreeView.Nodes.Clear();
            tagTreeView.TreeViewNodeSorter = null;

            //Open
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Filter = "Halo Map Files (*.map)|*.map";
                
                if(openDlg.ShowDialog() == DialogResult.OK)
                {
                    //Close
                    Map.Close();
                    Map.Dispose();

                    //Load
                    Map.Load(openDlg.FileName);
                }
            }

            //Loop
            foreach (IndexEntry entry in Map.IndexEntries)
            {
                //Setup
                currentCollection = tagTreeView.Nodes;
                parts = $"{entry.Filename}.{entry.Root}".Split('\\');

                for (int i = 0; i < parts.Length - 1; i++)
                {
                    if (!currentCollection.ContainsKey(parts[i]))
                        currentNode = currentCollection.Add(parts[i], parts[i]);
                    else currentNode = currentCollection[parts[i]];
                    currentCollection = currentNode.Nodes;
                }

                currentNode = currentCollection.Add(parts.Last(), parts.Last());
                currentNode.Tag = entry;
            }

            //End
            tagTreeView.TreeViewNodeSorter = null;
            tagTreeView.Sort();
            tagTreeView.EndUpdate();
        }
        
        private void tagTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //Check
            if (e.Node.Tag is IndexEntry entry)
            {
                //Check
                if (Editors.ContainsValue(entry.Id)) return;

                //Create tool
                ITool<MapFile, IndexEntry, Xbox> editor = new TagEditor();
                Editors.Add(editor, entry.Id);

                //Initialize
                editor.Initialize(this);
                
                //Create form
                Form editorForm = new Form() { Text = $"{editor.Name} - {entry.Filename}.{entry.Root}", Size = new Size(800, 600), MdiParent = this, Tag = editor };
                editorForm.FormClosed += EditorForm_FormClosed;
                editorForm.Load += EditorForm_Load;
                editorForm.Controls.Add(editor.UserInterface);

                //Show
                editorForm.Show();
            }
        }

        private void EditorForm_Load(object sender, EventArgs e)
        {
            //Select
            if (sender is Form editorForm && editorForm.Tag is ITool<MapFile, IndexEntry, Xbox> tool)
                tool.OnSelectedEntryChanged();
        }

        private void EditorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Check
            if (sender is Control ctrl && ctrl.Tag is IAddOn editor)
                Editors.Remove(editor);
        }

        public object Request(IAddOn sender, string request, params object[] args)
        {
            switch (request)
            {
                case "Map": return Map;
                case "SelectedEntry": return Map.IndexEntries[(TagId)Editors[sender]];
                case "Xbox": return new Xbox(Application.StartupPath);
                default:
                    System.Diagnostics.Debugger.Break();
                    return null;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                saveDlg.Filter = "Halo Map Files (*.map)|*.map";

                if (saveDlg.ShowDialog() == DialogResult.OK)
                    Map.Save(saveDlg.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
