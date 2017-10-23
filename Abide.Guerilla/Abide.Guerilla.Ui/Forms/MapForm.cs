using Abide.HaloLibrary.Halo2Map;
using System;
using System.IO;
using System.Windows.Forms;

namespace Abide.Guerilla.Ui.Forms
{
    public partial class MapForm : Form
    {
        private readonly MapFile map = new MapFile();

        public MapForm(MapFile map) : this()
        {
            this.map = map;
            Text = map.Name;

            foreach (IndexEntry entry in map.IndexEntries)
            {
                //Split
                string[] parts = $"{entry.Filename}.{entry.Root}".Split('\\');
                TreeNodeCollection currentCollection = tagTreeView.Nodes;
                TreeNode currentNode = null;

                //Handle
                foreach (string part in parts)
                {
                    //Create node if needed.
                    if (currentCollection.ContainsKey(part))
                    {
                        currentNode = currentCollection[part];
                        currentCollection = currentNode.Nodes;
                    }
                    else
                    {
                        currentNode = new TreeNode(part) { Name = part };
                        currentCollection.Add(currentNode);
                        currentCollection = currentNode.Nodes;
                    }
                }

                //Setup
                currentNode.Tag = entry;
            }
        }

        private MapForm()
        {
            InitializeComponent();
        }

        private void TagTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Check
            if (e.Node.Tag is IndexEntry entry)
                using (BinaryReader tagReader = new BinaryReader(entry.TagData))
                {
                    //Goto
                    entry.TagData.Seek(entry.PostProcessedOffset, SeekOrigin.Begin);

                    //Get Type
                    if (Guerilla.TagGroupDictionary[entry.Root] == typeof(Tags.BitmapBlock))
                    {
                        var group = tagReader.ReadStructure<Tags.BitmapBlock>();
                    }
                }
        }
    }
}
