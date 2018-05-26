using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abide.Tag.Ui
{
    public partial class MapForm : Form
    {
        private readonly MapFile map = new MapFile();

        public MapForm()
        {
            InitializeComponent();
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
                    map.Load(openDlg.FileName);
            }

            //Build tree view
            MapFile_BuildTreeView(map);
        }

        private void tagsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Check
            if (e.Node.Tag is IndexEntry entry)
                Tag_Selected(entry);
        }
        
        private void MapFile_BuildTreeView(MapFile mapFile)
        {
            //Begin
            tagsTreeView.BeginUpdate();
            tagsTreeView.TreeViewNodeSorter = new TagNodeSorter();

            //Clear
            tagsTreeView.Nodes.Clear();

            //Prepare
            string[] parts = null;
            TreeNodeCollection currentCollection = null;
            TreeNode currentNode = null;

            //Loop
            foreach (IndexEntry tag in mapFile.IndexEntries)
            {
                //Setup
                currentNode = null;
                currentCollection = tagsTreeView.Nodes;

                //Break
                parts = tag.Filename.Split('\\');

                //Loop
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    //Create?
                    if (!currentCollection.ContainsKey(parts[i]))
                        currentNode = currentCollection.Add(parts[i]);
                    else currentNode = currentCollection[parts[i]];

                    //Set Name
                    currentNode.Name = currentNode.Text = parts[i];
                    currentCollection = currentNode.Nodes;
                }

                //Prepare
                currentNode = currentCollection.Add(parts[parts.Length - 1]);
                currentNode.Name = currentNode.Text = $"{parts[parts.Length - 1]}.{tag.Root}";
                currentNode.Tag = tag;
            }

            //Sort
            tagsTreeView.Sort();

            //End
            tagsTreeView.EndUpdate();

            //Enable
            dumpTagsButton.Enabled = true;
        }

        private void Tag_Selected(IndexEntry entry)
        {
            //Initialize
            using (BinaryReader reader = new BinaryReader(entry.TagData))
            {
                //Prepare
                Group tagGroup = Generated.TagLookup.CreateTagGroup(entry.Root);

                //Create
                if (tagGroup != null)
                {
                    //Read
                    entry.TagData.Seek(entry.PostProcessedOffset, SeekOrigin.Begin);
                    tagGroup.Read(reader);

                    //Yay
                    Console.WriteLine("Read {0}.{1}", entry.Filename, entry.Root);
                }
            }
        }

        private void dumpTagsButton_Click(object sender, EventArgs e)
        {
            //Prepare
            Group tagGroup = null;

            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Browse to tags folder...";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    //Loop
                    foreach (IndexEntry tag in map.IndexEntries)
                        using (BinaryReader reader = new BinaryReader(tag.TagData))
                        {
                            //Goto
                            tag.TagData.Seek(tag.PostProcessedOffset, SeekOrigin.Begin);

                            //Check
                            if((tagGroup = Generated.TagLookup.CreateTagGroup(tag.Root)) != null)
                            {
                                //Read
                                tagGroup.Read(reader);

                                //Get filename and create directory if needed
                                string filename = Path.Combine(folderDlg.SelectedPath, $"{tag.Filename}.{ tagGroup.Name}");
                                string directory = Path.GetDirectoryName(filename);
                                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                                //Create File
                                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    //Write tag
                                    using (BinaryWriter writer = new BinaryWriter(ms))
                                    {
                                        tagGroup.Write(writer);
                                        fs.Write(ms.GetBuffer(), 0, (int)ms.Position);
                                    }
                                }
                            }
                        }
                }
            }
        }

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
    }
}
