using Abide.Ifp;
using Abide.Tag.CodeDom;
using Abide.Tag.Definition;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Windows.Forms;

namespace Abide.Tag.Ui
{
    public partial class Main : Form
    {
        private readonly TagDefinitionCollection collection = new TagDefinitionCollection();

        public Main()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit
            Application.Exit();
        }

        private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Browse to tag definitions folder.";
                folderDlg.ShowNewFolderButton = false;

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    //Build cache
                    tagDefinitions_BuildCache(folderDlg.SelectedPath);
                }
            }
        }

        private void cacheLibraryFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear
            AbideCodeDomGlobals.Clear();

            //Prepare
            CodeCompileUnit compileUnit = null;
            CodeGeneratorOptions options = new CodeGeneratorOptions() { BracingStyle = "C", BlankLinesBetweenMembers = false };

            //Preprocess
            AbideCodeDomGlobals.PreprocessForCache(collection);

            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Browse to directory to create *.cs files.";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    //Clear directory
                    foreach (string file in Directory.GetFiles(folderDlg.SelectedPath))
                        File.Delete(file);

                    //Create Code Files
                    using (CSharpCodeProvider provider = new CSharpCodeProvider())
                    {
                        //Loop through tag blocks
                        foreach (AbideTagBlock block in AbideCodeDomGlobals.GetTagBlocks())
                        {
                            //Create group code compile unit
                            compileUnit = new AbideTagBlockCodeCompileUnit(block, "Abide.HaloLibrary.Halo2.Retail.Tag", "Abide.HaloLibrary.Halo2.Retail.Tag");

                            //Create writer
                            using (StreamWriter writer = new StreamWriter(Path.Combine(folderDlg.SelectedPath, $"{AbideCodeDomGlobals.GetMemberName(block)}.Generated.{provider.FileExtension}")))
                            {
                                //Write
                                provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                            }
                        }

                        //Loop through tag groups
                        foreach (AbideTagGroup group in AbideCodeDomGlobals.GetTagGroups())
                        {
                            //Create group code compile unit
                            compileUnit = new AbideTagGroupCodeCompileUnit(group, "Abide.HaloLibrary.Halo2.Retail.Tag", "Abide.HaloLibrary.Halo2.Retail.Tag");

                            //Create writer
                            using (StreamWriter writer = new StreamWriter(Path.Combine(folderDlg.SelectedPath, $"{AbideCodeDomGlobals.GetMemberName(group)}.Generated.{provider.FileExtension}")))
                            {
                                //Write
                                provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                            }
                        }

                        //Create static lookup
                        using (StreamWriter writer = new StreamWriter(Path.Combine(folderDlg.SelectedPath, $"TagLookup.Generated.{provider.FileExtension}")))
                        {
                            //Create tag lookup compile unit
                            compileUnit = new AbideTagLookupCodeCompileUnit("Abide.HaloLibrary.Halo2.Retail.Tag", "Abide.HaloLibrary.Halo2.Retail.Tag");

                            //Write
                            provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                        }
                    }
                }
            }
        }

        private void cacheFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear
            AbideCodeDomGlobals.Clear();

            //Prepare
            CodeCompileUnit compileUnit = null;
            CodeGeneratorOptions options = new CodeGeneratorOptions() { BracingStyle = "C", BlankLinesBetweenMembers = false };

            //Preprocess
            AbideCodeDomGlobals.PreprocessForCache(collection);

            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Browse to directory to create *.cs files.";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    //Clear directory
                    foreach (string file in Directory.GetFiles(folderDlg.SelectedPath))
                        File.Delete(file);

                    //Create Code Files
                    using (CSharpCodeProvider provider = new CSharpCodeProvider())
                    {
                        //Loop through tag blocks
                        foreach (AbideTagBlock block in AbideCodeDomGlobals.GetTagBlocks())
                        {
                            //Create group code compile unit
                            compileUnit = new AbideTagBlockCodeCompileUnit(block, "Abide.Tag.Cache");

                            //Create writer
                            using (StreamWriter writer = new StreamWriter(Path.Combine(folderDlg.SelectedPath, $"{AbideCodeDomGlobals.GetMemberName(block)}.Generated.{provider.FileExtension}")))
                            {
                                //Write
                                provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                            }
                        }

                        //Loop through tag groups
                        foreach (AbideTagGroup group in AbideCodeDomGlobals.GetTagGroups())
                        {
                            //Create group code compile unit
                            compileUnit = new AbideTagGroupCodeCompileUnit(group, "Abide.Tag.Cache");

                            //Create writer
                            using (StreamWriter writer = new StreamWriter(Path.Combine(folderDlg.SelectedPath, $"{AbideCodeDomGlobals.GetMemberName(group)}.Generated.{provider.FileExtension}")))
                            {
                                //Write
                                provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                            }
                        }

                        //Create static lookup
                        using (StreamWriter writer = new StreamWriter(Path.Combine(folderDlg.SelectedPath, $"TagLookup.Generated.{provider.FileExtension}")))
                        {
                            //Create tag lookup compile unit
                            compileUnit = new AbideTagLookupCodeCompileUnit("Abide.Tag.Cache");

                            //Write
                            provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                        }
                    }
                }
            }
        }

        private void guerillaFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear
            AbideCodeDomGlobals.Clear();

            //Prepare
            CodeCompileUnit compileUnit = null;
            CodeGeneratorOptions options = new CodeGeneratorOptions() { BracingStyle = "C", BlankLinesBetweenMembers = false };

            //Preprocess
            AbideCodeDomGlobals.PreprocessForGuerilla(collection);

            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Browse to directory to create *.cs files.";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    //Clear directory
                    foreach (string file in Directory.GetFiles(folderDlg.SelectedPath))
                        File.Delete(file);

                    //Create Code Files
                    using (CSharpCodeProvider provider = new CSharpCodeProvider())
                    {
                        //Loop through tag blocks
                        foreach (AbideTagBlock block in AbideCodeDomGlobals.GetTagBlocks())
                        {
                            //Create group code compile unit
                            compileUnit = new AbideTagBlockCodeCompileUnit(block, "Abide.Tag.Guerilla");

                            //Create writer
                            using (StreamWriter writer = new StreamWriter(Path.Combine(folderDlg.SelectedPath, $"{AbideCodeDomGlobals.GetMemberName(block)}.Generated.{provider.FileExtension}")))
                            {
                                //Write
                                provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                            }
                        }

                        //Loop through tag groups
                        foreach (AbideTagGroup group in AbideCodeDomGlobals.GetTagGroups())
                        {
                            //Create group code compile unit
                            compileUnit = new AbideTagGroupCodeCompileUnit(group, "Abide.Tag.Guerilla");

                            //Create writer
                            using (StreamWriter writer = new StreamWriter(Path.Combine(folderDlg.SelectedPath, $"{AbideCodeDomGlobals.GetMemberName(group)}.Generated.{provider.FileExtension}")))
                            {
                                //Write
                                provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                            }
                        }

                        //Create static lookup
                        using (StreamWriter writer = new StreamWriter(Path.Combine(folderDlg.SelectedPath, $"TagLookup.Generated.{provider.FileExtension}")))
                        {
                            //Create tag lookup compile unit
                            compileUnit = new AbideTagLookupCodeCompileUnit("Abide.Tag.Guerilla");

                            //Write
                            provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                        }
                    }
                }
            }
        }

        private void betaFilesToolStripMenuItem_Click(object sender, EventArgs e)

        {
            //Clear
            AbideCodeDomGlobals.Clear();

            //Prepare
            CodeCompileUnit compileUnit = null;
            CodeGeneratorOptions options = new CodeGeneratorOptions() { BracingStyle = "C", BlankLinesBetweenMembers = false };

            //Preprocess
            AbideCodeDomGlobals.PreprocessForCache(collection);

            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Browse to directory to create *.cs files.";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    //Clear directory
                    foreach (string file in Directory.GetFiles(folderDlg.SelectedPath))
                        File.Delete(file);

                    //Create Code Files
                    using (CSharpCodeProvider provider = new CSharpCodeProvider())
                    {
                        //Loop through tag blocks
                        foreach (AbideTagBlock block in AbideCodeDomGlobals.GetTagBlocks())
                        {
                            //Create group code compile unit
                            compileUnit = new AbideTagBlockCodeCompileUnit(block, "Beta");

                            //Create writer
                            using (StreamWriter writer = new StreamWriter(Path.Combine(folderDlg.SelectedPath, $"{AbideCodeDomGlobals.GetMemberName(block)}.Generated.{provider.FileExtension}")))
                            {
                                //Write
                                provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                            }
                        }

                        //Loop through tag groups
                        foreach (AbideTagGroup group in AbideCodeDomGlobals.GetTagGroups())
                        {
                            //Create group code compile unit
                            compileUnit = new AbideTagGroupCodeCompileUnit(group, "Beta");

                            //Create writer
                            using (StreamWriter writer = new StreamWriter(Path.Combine(folderDlg.SelectedPath, $"{AbideCodeDomGlobals.GetMemberName(group)}.Generated.{provider.FileExtension}")))
                            {
                                //Write
                                provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                            }
                        }

                        //Create static lookup
                        using (StreamWriter writer = new StreamWriter(Path.Combine(folderDlg.SelectedPath, $"TagLookup.Generated.{provider.FileExtension}")))
                        {
                            //Create tag lookup compile unit
                            compileUnit = new AbideTagLookupCodeCompileUnit("Beta");

                            //Write
                            provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                        }
                    }
                }
            }
        }

        private void generateentFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Browse to directory to create *.ent files.";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    //Clear directory
                    foreach (string file in Directory.GetFiles(folderDlg.SelectedPath))
                        File.Delete(file);

                    //Loop through tag groups
                    foreach (AbideTagGroup tagGroup in collection.GetTagGroups())
                    {
                        string pluginName = tagGroup.GroupTag.FourCc.Replace('<', '_').Replace('>', '_').Replace('?', '_');
                        string path = Path.Combine(folderDlg.SelectedPath, $"{pluginName}.ent");
                    }
                }
            }
        }

        private void tagGroupTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Clear
            controlsPanel.Controls.Clear();

            //Check
            if (e.Node.Tag is AbideTagBlock tagBlock)
                Guerilla.Guerilla.GenerateControls(tagBlock, controlsPanel);
            else if (e.Node.Tag is AbideTagGroup tagGroup)
                foreach (TreeNode child in e.Node.Nodes)
                    if (child.Tag is AbideTagBlock childBlock)
                        Guerilla.Guerilla.GenerateControls(childBlock, controlsPanel);
        }

        private void tagDefinitions_BuildCache(string directory)
        {
            //Clear
            collection.Clear();

            //Get files
            string[] abideTagGroupFiles = Directory.GetFiles(directory, "*.atg");
            string[] abideTagBlockfiles = Directory.GetFiles(directory, "*.atb");

            //Loop
            foreach (string abideTagGroup in abideTagGroupFiles)
            {
                //Load
                AbideTagGroup group = new AbideTagGroup();
                group.Load(abideTagGroup);

                //Add
                collection.Add(group);
            }
            foreach (string abideTagGroup in abideTagBlockfiles)
            {
                //Load
                AbideTagBlock block = new AbideTagBlock();
                block.Load(abideTagGroup);

                //Add
                collection.Add(block);
            }

            //Begin
            tagGroupTreeView.BeginUpdate();

            //Clear
            tagGroupTreeView.Nodes.Clear();

            //Recursive block node creation
            Func<AbideTagBlock, TreeNode> createTagBlockNode = null;
            createTagBlockNode = new Func<AbideTagBlock, TreeNode>((tagBlock) =>
            {
                //Create Node
                TreeNode blockNode = new TreeNode(tagBlock.DisplayName)
                {
                    Tag = tagBlock
                };

                //Loop
                AbideTagBlock block = null;
                foreach (AbideTagField field in tagBlock.FieldSet)
                {
                    //Check
                    switch (field.FieldType)
                    {
                        case FieldType.FieldBlock:
                            block = collection.GetTagBlock(field.BlockName);
                            blockNode.Nodes.Add(createTagBlockNode(block));
                            field.ReferencedBlock = block;
                            break;
                        case FieldType.FieldStruct:
                            block = collection.GetTagBlock(field.StructName);
                            blockNode.Nodes.Add(createTagBlockNode(block));
                            field.ReferencedBlock = block;
                            break;
                    }
                }

                //Return
                return blockNode;
            });

            //Recursive group node creation
            Action<AbideTagGroup, TreeNode> createTagGroupNode = null;
            createTagGroupNode = new Action<AbideTagGroup, TreeNode>((tagGroup, node) =>
            {
                //Check
                if (tagGroup.ParentGroupTag.Dword != 0)
                    createTagGroupNode(collection.GetTagGroup(tagGroup.ParentGroupTag), node);

                //Add blocks
                node.Nodes.Add(createTagBlockNode(collection.GetTagBlock(tagGroup.BlockName)));
            });

            //Loop
            foreach (AbideTagGroup group in collection.GetTagGroups())
            {
                //Create Node
                TreeNode groupNode = new TreeNode($"[{group.GroupTag}] - {group.Name}")
                {
                    Tag = group
                };

                //Add parent blocks
                createTagGroupNode(group, groupNode);

                //Add
                tagGroupTreeView.Nodes.Add(groupNode);
            }

            //End
            tagGroupTreeView.EndUpdate();
        }

        private void mapFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using (MapForm mapForm = new MapForm())
            //    mapForm.ShowDialog();
        }
    }
}
