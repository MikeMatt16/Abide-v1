using Abide.Guerilla.Managed;
using System;
using System.IO;
using System.Windows.Forms;

namespace Abide.Guerilla.Ui
{
    public partial class Main : Form
    {
        private readonly GuerillaReader guerilla = new GuerillaReader();

        public Main()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open H2alang.dll...
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Title = "Open H2alang.dll";
                openDlg.Filter = "Dynamic Link Libraries (*.dll)|*.dll";
                if (openDlg.ShowDialog() == DialogResult.OK)
                    Program.H2alangPath = openDlg.FileName;
            }

            //Open H2alang.dll...
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Title = "Open H2Guerilla.exe";
                openDlg.Filter = "Application Executable (*.exe)|*.exe";
                if (openDlg.ShowDialog() == DialogResult.OK)
                    Program.H2GuerillaPath = openDlg.FileName;
            }

            //Load
            loadGuerilla();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit
            Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //Load
            loadGuerilla();
        }

        private void loadGuerilla()
        {
            //Check
            if(File.Exists(Program.H2GuerillaPath) && File.Exists(Program.H2alangPath))
            {
                //Process
                guerilla.Process(Program.H2GuerillaPath, Program.H2alangPath);
                guerilla.PostProcess();

                //Clear Tree nodes
                tagGroupTreeView.BeginUpdate();
                tagGroupTreeView.Nodes.Clear();

                //Loop
                foreach (TagGroupDefinition tagGroup in guerilla.GetTagGroups())
                {
                    //Get Block
                    TagBlockDefinition block = guerilla.Search(tagGroup.DefinitionAddress);

                    //Create group node
                    TreeNode groupNode = new TreeNode($"[{tagGroup.GroupTag}] - {block?.DisplayName ?? tagGroup.Name}");
                    groupNode.Tag = tagGroup;

                    //Build Node hierarchy
                    if (block != null) loadBlock(groupNode, block);

                    //Add Node
                    tagGroupTreeView.Nodes.Add(groupNode);
                }

                //End
                tagGroupTreeView.Sort();
                tagGroupTreeView.EndUpdate();
            }
        }

        private void loadBlock(TreeNode blockNode, TagBlockDefinition block)
        {
            //Prepare
            TreeNode childBlockNode = null;
            TagBlockDefinition childBlock = null;
            TagFieldSet fieldSet = null;

            //Loop
            if (block.FieldSetCount > 0)
                foreach (TagFieldDefinition field in block.GetFieldDefinitionsH2Xbox())
                    switch (field.Type)
                    {
                        case FieldType.FieldBlock:
                            //Prepare
                            childBlock = guerilla.Search(field.DefinitionAddress);
                            fieldSet = childBlock.GetFieldSetH2Xbox();
                            childBlockNode = new TreeNode($"Block {childBlock.DisplayName} Size: {fieldSet.Size} Alignment: {fieldSet.Alignment}");
                            
                            //Build Node hierarchy
                            if (block != null) loadBlock(childBlockNode, childBlock);
                            blockNode.Nodes.Add(childBlockNode);
                            break;
                        case FieldType.FieldStruct:
                            //Prepare
                            TagStructDefinition structDef = (TagStructDefinition)field;
                            childBlock = guerilla.Search(structDef.BlockDefinitionAddresss);
                            fieldSet = childBlock.GetFieldSetH2Xbox();
                            childBlockNode = new TreeNode($"Struct {childBlock.DisplayName} Size: {fieldSet.Size} Alignment: {fieldSet.Alignment}");
                            
                            //Build Node hierarchy
                            if (block != null) loadBlock(childBlockNode, childBlock);
                            blockNode.Nodes.Add(childBlockNode);
                            break;
                    }
        }
    }
}
