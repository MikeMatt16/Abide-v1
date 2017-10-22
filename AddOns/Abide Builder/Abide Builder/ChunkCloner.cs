using Abide.HaloLibrary.Halo2Map;
using System.IO;
using System.Windows.Forms;

namespace Abide.Builder
{
    public partial class ChunkCloner : Form
    {
        private AbideTag tag = null;
        private readonly MapFile mapFile;
        private readonly IndexEntry indexEntry;

        public ChunkCloner(MapFile mapFile, IndexEntry indexEntry) : this()
        {
            this.mapFile = mapFile;
            this.indexEntry = indexEntry;
            this.Text = $@"{Text} - {mapFile.Name}\{indexEntry.Filename}.{indexEntry.Root}";
        }
        private ChunkCloner()
        {
            InitializeComponent();
        }

        private void ChunkCloner_Load(object sender, System.EventArgs e)
        {
            //Load Tag
            tag = new AbideTag(mapFile, indexEntry);

            //Loop
            foreach (AbideTagBlock tagBlock in tag.TagBlocks)
            {
                //Create
                TagBlockTreeNode tagBlockTreeNode = new TagBlockTreeNode(tagBlock);

                //Add
                tagTreeView.Nodes.Add(tagBlockTreeNode);
            }
        }

        private sealed class TagBlockTreeNode : TreeNode
        {
            private const string nameFormat = "{0} Count: {1} Blocks Length: {2}";

            public AbideTagDefinitionMember Member
            {
                get { return tagBlock.Member; }
            }

            private readonly AbideTagBlock tagBlock;

            public TagBlockTreeNode(AbideTagBlock tagBlock)
            {
                //Set
                this.tagBlock = tagBlock;

                //Update Text
                UpdateText();

                //Add Children
                foreach (AbideBlock block in tagBlock)
                {
                    BlockTreeNode blockTreeNode = new BlockTreeNode(block);
                    Nodes.Add(blockTreeNode);
                }
            }

            public void UpdateText()
            {
                Text = string.Format(nameFormat, tagBlock.Member.Name, tagBlock.Count, tagBlock.Member.Size * tagBlock.Count);
            }
        }

        private sealed class BlockTreeNode : TreeNode
        {
            public AbideTagDefinitionMember Member
            {
                get { return block.Member; }
            }

            private readonly AbideBlock block;

            public BlockTreeNode(AbideBlock block)
            {
                //Set
                this.block = block;

                //Update Text
                Text = block.Member.Name;

                //Add Children
                foreach (AbideTagBlock tagBlock in block.TagBlocks)
                {
                    TagBlockTreeNode tagBlockTreeNode = new TagBlockTreeNode(tagBlock);
                    Nodes.Add(tagBlockTreeNode);
                }
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            //Compile
            byte[] data = tag.Compile((uint)indexEntry.PostProcessedOffset);

            //Save dlg
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                saveDlg.Title = "Save tag as...";
                saveDlg.Filter = "Binary files (*.bin)|*.bin";

                if (saveDlg.ShowDialog() == DialogResult.OK)
                    using (FileStream fs = new FileStream(saveDlg.FileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                        fs.Write(data, 0, data.Length);
            }
        }
    }
}
