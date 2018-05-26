using Abide.Tag.CodeDOM;
using Abide.Tag.Definition;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Abide.Tag.Ui
{
    public partial class Main : Form
    {
        private readonly TagCache cache = new TagCache();

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
                    //Get files
                    string[] files = Directory.GetFiles(folderDlg.SelectedPath);

                    //Build cache
                    tagDefinitions_BuildCache(files);
                }
            }
        }

        private void generatecsFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            CodeCompileUnit compileUnit = null;
            CodeGeneratorOptions options = new CodeGeneratorOptions() { BracingStyle = "C", BlankLinesBetweenMembers = false };

            //Prepare Code DOM
            foreach (AbideTagBlock block in cache.GetTagBlocks())
                AbideCodeDomGlobals.Preprocess(block);
            foreach (AbideTagGroup group in cache.GetTagGroups())
                AbideCodeDomGlobals.Preprocess(group);

            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Browse to directory to create *.cs files.";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                    using (CSharpCodeProvider provider = new CSharpCodeProvider())
                    {
                        //Loop through tag blocks
                        foreach (AbideTagBlock block in cache.GetTagBlocks())
                        {
                            //Create group code compile unit
                            compileUnit = new AbideTagBlockCodeCompileUnit(block);

                            //Create writer
                            using (StreamWriter writer = new StreamWriter(Path.Combine(folderDlg.SelectedPath, $"{AbideCodeDomGlobals.GetMemberName(block)}.Generated.{provider.FileExtension}")))
                            {
                                //Write
                                provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                            }
                        }

                        //Loop through tag groups
                        foreach (AbideTagGroup group in cache.GetTagGroups())
                        {
                            //Create group code compile unit
                            compileUnit = new AbideTagGroupCodeCompileUnit(group);

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
                            compileUnit = new AbideTagLookupCodeCompileUnit();

                            //Write
                            provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
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

        private void tagDefinitions_BuildCache(string[] files)
        {
            //Clear
            cache.Clear();

            //Loop
            FileInfo info = null;
            foreach (string file in files)
            {
                //Get Info
                info = new FileInfo(file);

                //Open
                using (Stream s = info.OpenRead())
                    if (info.Extension == ".atg")       //Check for Abide tag group
                    {
                        //Load
                        AbideTagGroup group = new AbideTagGroup();
                        group.Load(s);

                        //Add
                        cache.Add(group);
                    }
                    else if (info.Extension == ".atb")  //Check for Abide tag block
                    {
                        //Load
                        AbideTagBlock block = new AbideTagBlock();
                        block.Load(s);

                        //Add
                        cache.Add(block);
                    }
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
                            block = cache.GetTagBlock(field.BlockName);
                            blockNode.Nodes.Add(createTagBlockNode(block));
                            field.ReferencedBlock = block;
                            break;
                        case FieldType.FieldStruct:
                            block = cache.GetTagBlock(field.StructName);
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
                    createTagGroupNode(cache.GetTagGroup(tagGroup.ParentGroupTag), node);

                //Add blocks
                node.Nodes.Add(createTagBlockNode(cache.GetTagBlock(tagGroup.BlockName)));
            });

            //Loop
            foreach (AbideTagGroup group in cache.GetTagGroups())
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
            using (MapForm mapForm = new MapForm())
                mapForm.ShowDialog();
        }
    }

    public class TagCache
    {
        private readonly List<AbideTagGroup> tagGroups = new List<AbideTagGroup>();
        private readonly List<AbideTagBlock> tagBlocks = new List<AbideTagBlock>();
        private readonly Dictionary<string, int> tagGroupLookup = new Dictionary<string, int>();
        private readonly Dictionary<string, int> tagBlockLookup = new Dictionary<string, int>();

        public void Clear()
        {
            //Clear
            tagGroupLookup.Clear();
            tagBlockLookup.Clear();
            tagGroups.Clear();
            tagBlocks.Clear();

            //Collect
            GC.Collect();
        }
        public void Add(AbideTagGroup tagGroup)
        {
            //Check
            if (tagGroup == null) return;

            //Check
            if (!tagGroups.Contains(tagGroup) && !tagGroupLookup.ContainsKey(tagGroup.GroupTag))
            {
                tagGroupLookup.Add(tagGroup.GroupTag, tagGroups.Count);
                tagGroups.Add(tagGroup);
            }
        }
        public void Add(AbideTagBlock tagBlock)
        {
            //Check
            if (tagBlock == null) return;

            //Check
            if (!tagBlocks.Contains(tagBlock) && !tagBlockLookup.ContainsKey(tagBlock.Name))
            {
                tagBlockLookup.Add(tagBlock.Name, tagBlocks.Count);
                tagBlocks.Add(tagBlock);
            }
        }
        public AbideTagGroup GetTagGroup(string groupTag)
        {
            if (tagGroupLookup.ContainsKey(groupTag))
                return tagGroups[tagGroupLookup[groupTag]];
            else return null;
        }
        public AbideTagBlock GetTagBlock(string blockName)
        {
            if (tagBlockLookup.ContainsKey(blockName))
                return tagBlocks[tagBlockLookup[blockName]];
            else return null;
        }
        public AbideTagGroup[] GetTagGroups()
        {
            return tagGroups.ToArray();
        }
        public AbideTagBlock[] GetTagBlocks()
        {
            return tagBlocks.ToArray();
        }
    }
}
