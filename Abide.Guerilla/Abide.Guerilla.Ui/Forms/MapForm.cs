using Abide.Guerilla.Tags;
using Abide.HaloLibrary.Halo2Map;
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

            //Begin
            tagTreeView.BeginUpdate();
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

            //Sort
            tagTreeView.Sort();
            tagTreeView.EndUpdate();
        }

        private MapForm()
        {
            InitializeComponent();
        }

        private void TagTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Create
            AbideTagBlock tagGroup = null;

            //Check
            if (e.Node.Tag is IndexEntry entry)
                using (BinaryReader reader = new BinaryReader(entry.TagData))
                {
                    //Goto
                    entry.TagData.Seek(entry.PostProcessedOffset, SeekOrigin.Begin);

                    //Handle root
                    switch (entry.Root)
                    {
                        case HaloTags.ugh_:
                            tagGroup = AbideTagBlock.Instantiate<SoundCacheFileGestaltBlock>(map);
                            tagGroup.Read(reader);
                            break;
                        case HaloTags.vehi:
                            tagGroup = AbideTagBlock.Instantiate<VehicleBlock>(map);
                            tagGroup.Read(reader);
                            break;
                        case HaloTags.bitm:
                            tagGroup = AbideTagBlock.Instantiate<BitmapBlock>(map);
                            tagGroup.Read(reader);
                            break;
                        case HaloTags.phmo:
                            tagGroup = AbideTagBlock.Instantiate<PhysicsModelBlock>(map);
                            tagGroup.Read(reader);
                            break;
                    }
                }
        }
    }
}
