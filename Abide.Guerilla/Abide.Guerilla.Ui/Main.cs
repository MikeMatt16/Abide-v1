using Abide.Guerilla.CodeDom;
using Abide.Guerilla.Managed;
using Abide.Guerilla.Ui.Forms;
using Abide.HaloLibrary.Halo2Map;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Abide.Guerilla.Ui
{
    public partial class Main : Form
    {
        private readonly GuerillaInstance guerilla = new GuerillaInstance();

        public Main()
        {
            InitializeComponent();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
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
            LoadGuerilla();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit
            Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //Load
            LoadGuerilla();
        }

        private void LoadGuerilla()
        {
            //Check
            if (File.Exists(Program.H2GuerillaPath) && File.Exists(Program.H2alangPath))
            {
                //Process
                guerilla.Process(Program.H2GuerillaPath, Program.H2alangPath);

                //Clear Tree nodes
                tagGroupTreeView.BeginUpdate();
                tagGroupTreeView.Nodes.Clear();

                //Loop
                foreach (TagGroupDefinition tagGroup in guerilla.GetTagGroups())
                {
                    //Get Block
                    TagBlockDefinition block = guerilla.FindTagBlock(tagGroup.DefinitionAddress);

                    //Create group node
                    TreeNode groupNode = new TreeNode($"[{tagGroup.GroupTag}] - {block?.DisplayName ?? tagGroup.Name}");
                    groupNode.Tag = block;

                    //Build Node hierarchy
                    if (block != null) LoadBlock(block, groupNode, block);

                    //Add Node
                    tagGroupTreeView.Nodes.Add(groupNode);
                }

                //End
                tagGroupTreeView.Sort();
                tagGroupTreeView.EndUpdate();
            }
        }

        private void LoadBlock(TagBlockDefinition groupBlock, TreeNode blockNode, TagBlockDefinition block)
        {
            //Prepare
            TreeNode childBlockNode = null;
            TagBlockDefinition childBlock = null;
            TagFieldSet fieldSet = null;

            //Loop
            if (block.FieldSetCount > 0)
                foreach (TagFieldDefinition field in block.GetLatestFieldDefinitions())
                    switch (field.Type)
                    {
                        case FieldType.FieldBlock:
                            //Prepare
                            childBlock = guerilla.FindTagBlock(field.DefinitionAddress);
                            fieldSet = childBlock.GetFieldSet(childBlock.TagFieldSetLatestIndex);
                            childBlockNode = new TreeNode($"{childBlock.DisplayName} Size: {fieldSet.Size} Alignment: {fieldSet.Alignment}");
                            childBlockNode.Tag = groupBlock;

                            //Build Node hierarchy
                            if (block != null) LoadBlock(groupBlock, childBlockNode, childBlock);
                            blockNode.Nodes.Add(childBlockNode);
                            break;
                        case FieldType.FieldStruct:
                            //Prepare
                            TagFieldStructDefinition structDef = (TagFieldStructDefinition)field;
                            childBlock = guerilla.FindTagBlock(structDef.BlockDefinitionAddresss);

                            //Build Node hierarchy
                            if (block != null) LoadBlock(groupBlock, blockNode, childBlock);
                            break;
                    }
        }

        private void TagGroupTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Check
            if (e.Node.Tag is TagBlockDefinition block)
            {
                //Prepare
                StringBuilder codeDomStringBuilder = new StringBuilder();
                StringBuilder xmlStringBuilder = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };

                //Create CodeDOM
                TagGroupCodeCompileUnit compileUnit = new TagGroupCodeCompileUnit(guerilla, guerilla.FindTagGroup(block.Address), block);

                //Initialize
                using (CSharpCodeProvider provider = new CSharpCodeProvider())
                {
                    using (StringWriter writer = new StringWriter(codeDomStringBuilder))
                        provider.GenerateCodeFromCompileUnit(compileUnit, writer, new CodeGeneratorOptions() { BracingStyle = "C", BlankLinesBetweenMembers = false });
                }

                //Initialize
                using (XmlWriter xmlWriter = XmlWriter.Create(xmlStringBuilder, settings))
                {
                    //Write Start
                    xmlWriter.WriteStartDocument();

                    //Write XML
                    GenerateXml(xmlWriter, block);

                    //End
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    xmlWriter.Close();
                }

                //Inititalize
                TextForm ifpTextForm = new TextForm
                {
                    TextContent = xmlStringBuilder.ToString(),
                    Text = $"Ifp Content - {block.Name}",
                    MdiParent = this
                };
                ifpTextForm.Show();

                //Inititalize
                TextForm codeDomTextForm = new TextForm
                {
                    TextContent = codeDomStringBuilder.ToString(),
                    Text = $"C# Content - {block.Name}",
                    MdiParent = this
                };
                codeDomTextForm.Show();
            }
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            CodeCompileUnit pragmaDisable = new CodeSnippetCompileUnit("#pragma warning disable CS1591");
            CodeCompileUnit pragmaRestore = new CodeSnippetCompileUnit("#pragma warning restore CS1591");
            TagGroupCodeCompileUnit compileUnit = null;
            CodeGeneratorOptions options = new CodeGeneratorOptions() { BracingStyle = "C", BlankLinesBetweenMembers = false };

            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Browse to directory to create *.cs files.";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                    using (CSharpCodeProvider provider = new CSharpCodeProvider())
                        foreach (TagGroupDefinition tagGroup in guerilla.GetTagGroups())
                        {
                            //Create group code compile unit
                            compileUnit = new TagGroupCodeCompileUnit(guerilla, tagGroup, guerilla.FindTagBlock(tagGroup.DefinitionAddress));

                            //Create writer
                            using (StreamWriter writer = new StreamWriter(Path.Combine(folderDlg.SelectedPath, $"{compileUnit.BlockName}.Generated.{provider.FileExtension}")))
                            {
                                //Write Disable
                                provider.GenerateCodeFromCompileUnit(pragmaDisable, writer, options);

                                //Write
                                provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);

                                //Write Restore
                                provider.GenerateCodeFromCompileUnit(pragmaRestore, writer, options);
                            }
                        }
            }
        }

        private void MapFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            MapFile map = new MapFile(); ;

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

            //Check
            if (map.Scenario == null) return;

            //Initialize
            MapForm mapForm = new MapForm(map);
            mapForm.MdiParent = this;

            //Show
            mapForm.Show();
        }

        private void GenerateXml(XmlWriter writer, TagBlockDefinition tagBlock)
        {
            //Prepare
            int offset = 0;

            //Generate
            GenerateXml(writer, tagBlock, ref offset);
        }

        private void GenerateXml(XmlWriter writer, TagBlockDefinition tagBlock, ref int offset)
        {
            //Get Data
            TagFieldSet fieldSet = tagBlock.GetFieldSet(tagBlock.TagFieldSetLatestIndex);
            TagFieldDefinition[] fields = tagBlock.GetLatestFieldDefinitions();
            TagBlockDefinition childBlock = null;
            TagFieldSet childSet = null;

            //Check
            if (tagBlock.IsTagGroup)
            {
                //Lookup tag group
                TagGroupDefinition tagGroup = guerilla.FindTagGroup(tagBlock.Address);
                
                //Write Start
                writer.WriteStartElement("plugin");
                writer.WriteStartAttribute("class"); writer.WriteValue(tagGroup.GroupTag.FourCc); writer.WriteEndAttribute();
                writer.WriteStartAttribute("author"); writer.WriteValue("Abide.Guerilla"); writer.WriteEndAttribute();
                writer.WriteStartAttribute("headersize"); writer.WriteValue(fieldSet.Size); writer.WriteEndAttribute();
            }

            //Loop
            foreach (var field in fields)
                switch (field.Type)
                {
                    case FieldType.FieldBlock:
                        //Get Block
                        childBlock = guerilla.FindTagBlock(field.DefinitionAddress);
                        childSet = tagBlock.GetFieldSet(tagBlock.TagFieldSetLatestIndex);

                        //Write Struct
                        writer.WriteStartElement("struct");
                        writer.WriteStartAttribute("name"); writer.WriteValue(field.Name); writer.WriteEndAttribute();
                        writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                        writer.WriteStartAttribute("size"); writer.WriteValue(childSet.Size); writer.WriteEndAttribute();
                        writer.WriteStartAttribute("maxcount"); writer.WriteValue(childBlock.MaximumElementCount); writer.WriteEndAttribute();
                        writer.WriteStartAttribute("padalign"); writer.WriteValue(fieldSet.Alignment); writer.WriteEndAttribute();
                        GenerateXml(writer, childBlock);
                        writer.WriteEndElement();
                        offset += 8;
                        break;

                    default: CreateXmlElement(writer, field, ref offset); break;
                }

            //Write End
            if (tagBlock.IsTagGroup) writer.WriteEndElement();
        }

        private void CreateXmlElement(XmlWriter writer, TagFieldDefinition tagField, ref int offset)
        {
            //Prepare
            TagFieldExplanationDefinition explanationDefinition = null;
            TagFieldStructDefinition structDefinition = null;
            TagFieldEnumDefinition enumDefinition = null;
            TagBlockDefinition childBlock = null;
            TagFieldSet childFieldSet = null;

            //Handle type
            switch (tagField.Type)
            {
                case FieldType.FieldString:
                    writer.WriteStartElement("string32");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 32;
                    break;
                case FieldType.FieldLongString:
                    writer.WriteStartElement("string256");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 256;
                    break;
                case FieldType.FieldStringId:
                    writer.WriteStartElement("stringid");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldCharBlockIndex1:
                case FieldType.FieldCharBlockIndex2:
                case FieldType.FieldCharInteger:
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 1;
                    break;
                case FieldType.FieldShortBlockIndex1:
                case FieldType.FieldShortBlockIndex2:
                case FieldType.FieldShortInteger:
                    writer.WriteStartElement("short");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 2;
                    break;
                case FieldType.FieldLongBlockIndex1:
                case FieldType.FieldLongBlockIndex2:
                case FieldType.FieldLongInteger:
                    writer.WriteStartElement("int");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldAngle:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldTag:
                    writer.WriteStartElement("tag");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldCharEnum:
                    enumDefinition = (TagFieldEnumDefinition)tagField;
                    writer.WriteStartElement("enum8");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    for (int i = 0; i < enumDefinition.OptionCount; i++)
                    {
                        writer.WriteStartElement("option");
                        writer.WriteStartAttribute("name"); writer.WriteValue(enumDefinition.Options[i]); writer.WriteEndAttribute();
                        writer.WriteStartAttribute("value"); writer.WriteValue(i); writer.WriteEndAttribute();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    offset += 1;
                    break;
                case FieldType.FieldEnum:
                    enumDefinition = (TagFieldEnumDefinition)tagField;
                    writer.WriteStartElement("enum16");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    for (int i = 0; i < enumDefinition.OptionCount; i++)
                    {
                        writer.WriteStartElement("option");
                        writer.WriteStartAttribute("name"); writer.WriteValue(enumDefinition.Options[i]); writer.WriteEndAttribute();
                        writer.WriteStartAttribute("value"); writer.WriteValue(i); writer.WriteEndAttribute();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    offset += 2;
                    break;
                case FieldType.FieldLongEnum:
                    enumDefinition = (TagFieldEnumDefinition)tagField;
                    writer.WriteStartElement("enum32");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    for (int i = 0; i < enumDefinition.OptionCount; i++)
                    {
                        writer.WriteStartElement("option");
                        writer.WriteStartAttribute("name"); writer.WriteValue(enumDefinition.Options[i]); writer.WriteEndAttribute();
                        writer.WriteStartAttribute("value"); writer.WriteValue(i); writer.WriteEndAttribute();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldLongFlags:
                    enumDefinition = (TagFieldEnumDefinition)tagField;
                    writer.WriteStartElement("bitmask32");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    for (int i = 0; i < enumDefinition.OptionCount; i++)
                    {
                        writer.WriteStartElement("option");
                        writer.WriteStartAttribute("name"); writer.WriteValue(enumDefinition.Options[i]); writer.WriteEndAttribute();
                        writer.WriteStartAttribute("value"); writer.WriteValue(i); writer.WriteEndAttribute();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldWordFlags:
                    enumDefinition = (TagFieldEnumDefinition)tagField;
                    writer.WriteStartElement("bitmask16");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    for (int i = 0; i < enumDefinition.OptionCount; i++)
                    {
                        writer.WriteStartElement("option");
                        writer.WriteStartAttribute("name"); writer.WriteValue(enumDefinition.Options[i]); writer.WriteEndAttribute();
                        writer.WriteStartAttribute("value"); writer.WriteValue(i); writer.WriteEndAttribute();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    offset += 2;
                    break;
                case FieldType.FieldByteFlags:
                    enumDefinition = (TagFieldEnumDefinition)tagField;
                    writer.WriteStartElement("bitmask8");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    for (int i = 0; i < enumDefinition.OptionCount; i++)
                    {
                        writer.WriteStartElement("option");
                        writer.WriteStartAttribute("name"); writer.WriteValue(enumDefinition.Options[i]); writer.WriteEndAttribute();
                        writer.WriteStartAttribute("value"); writer.WriteValue(i); writer.WriteEndAttribute();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    offset += 1;
                    break;
                case FieldType.FieldPoint2D:
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} X"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 1;
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Y"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 1;
                    break;
                case FieldType.FieldRectangle2D:
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Top"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 1;
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Left"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 1;
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Right"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 1;
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Bottom"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 1;
                    break;
                case FieldType.FieldRgbColor:
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Red"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 1;
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Green"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 1;
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Blue"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 1;
                    break;
                case FieldType.FieldArgbColor:
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Alpha"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 1;
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Red"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 1;
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Green"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 1;
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Blue"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 1;
                    break;
                case FieldType.FieldReal:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldRealFraction:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldRealPoint2D:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} X"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Y"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldRealPoint3D:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} X"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Y"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Z"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldRealVector2D:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} X"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Y"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldRealVector3D:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} X"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Y"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Z"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldQuaternion:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} I"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} J"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} K"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} W"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldEulerAngles2D:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Pitch"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Yaw"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldEulerAngles3D:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Pitch"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Yaw"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Roll"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldRealRgbColor:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Red"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Green"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Blue"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldRealArgbColor:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Alpha"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Red"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Green"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Blue"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldRealHsvColor:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Hue"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Saturation"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Lightness"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldRealAhsvColor:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Alpha"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Hue"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Saturation"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name} Lightness"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldRealShortBounds:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name}..."); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"To"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldRealAngleBounds:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name}..."); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"To"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldRealBounds:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name}..."); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"To"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldRealFractionBounds:
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"{tagField.Name}..."); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    writer.WriteStartElement("float");
                    writer.WriteStartAttribute("name"); writer.WriteValue($"To"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldTagReference:
                    writer.WriteStartElement("id");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += 4;
                    break;
                case FieldType.FieldBlock:
                    childBlock = guerilla.FindTagBlock(tagField.DefinitionAddress);
                    childFieldSet = childBlock.GetFieldSet(childBlock.TagFieldSetLatestIndex);
                    writer.WriteStartElement("struct");
                    writer.WriteStartAttribute("name"); writer.WriteValue(childBlock.DisplayName); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("size"); writer.WriteValue(childFieldSet.Size); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("padalign"); writer.WriteValue(childFieldSet.Alignment); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("maxcount"); writer.WriteValue(childBlock.MaximumElementCount); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    GenerateXml(writer, childBlock);
                    writer.WriteEndElement();
                    break;
                case FieldType.FieldData:
                    TagFieldDataDefinition data = (TagFieldDataDefinition)tagField;
                    writer.WriteStartElement("struct");
                    writer.WriteStartAttribute("name"); writer.WriteValue(data.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("size"); writer.WriteValue(1); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("padalign"); writer.WriteValue(data.Alignment); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("maxcount"); writer.WriteValue(data.MaximumSize); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteStartElement("byte");
                    writer.WriteStartAttribute("name"); writer.WriteValue("Data"); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(0); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("visible"); writer.WriteValue("true"); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    offset += 8;
                    break;
                case FieldType.FieldPad:
                case FieldType.FieldUselessPad:
                case FieldType.FieldSkip:
                    writer.WriteStartElement("unused");
                    writer.WriteStartAttribute("name"); writer.WriteValue(tagField.Name); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("offset"); writer.WriteValue(offset); writer.WriteEndAttribute();
                    writer.WriteStartAttribute("size"); writer.WriteValue(tagField.DefinitionAddress); writer.WriteEndAttribute();
                    writer.WriteEndElement();
                    offset += tagField.DefinitionAddress;
                    break;
                case FieldType.FieldExplanation:
                    explanationDefinition = (TagFieldExplanationDefinition)tagField;
                    writer.WriteStartElement("explanation");
                    writer.WriteValue(explanationDefinition.Explanation);
                    writer.WriteEndElement();
                    break;
                case FieldType.FieldStruct:
                    structDefinition = (TagFieldStructDefinition)tagField;
                    childBlock = guerilla.FindTagBlock(structDefinition.BlockDefinitionAddresss);
                    GenerateXml(writer, childBlock, ref offset);
                    break;
                case FieldType.FieldArrayStart:
                case FieldType.FieldArrayEnd: break;
                case FieldType.FieldLongBlockFlags: break;
                case FieldType.FieldWordBlockFlags: break;
                case FieldType.FieldByteBlockFlags: break;
                case FieldType.FieldRealPlane2D: break;
                case FieldType.FieldRealPlane3D: break;
                case FieldType.FieldVertexBuffer: break;

                case FieldType.FieldCustom:
                case FieldType.FieldOldStringId:
                case FieldType.FieldTerminator: break;

                default: throw new NotImplementedException(Enum.GetName(typeof(FieldType), tagField.Type));
            }
        }
        
        private void GenerateCs(string directory, TagBlockDefinition tagBlock)
        {
            //Check
            if (tagBlock == null || directory == null) return;

            //Create
            using (CsWriter csWriter = new CsWriter(Path.Combine(directory, $"{tagBlock.Name}.cs")))
            {
                //Write Usings and namespace
                csWriter.WriteUsing("Abide.Guerilla.Types");
                csWriter.WriteUsing("Abide.HaloLibrary");
                csWriter.Write(string.Empty);

                //Suppress
                csWriter.WriteUnIndented("#pragma warning disable CS1591");
                csWriter.WriteStartNamespace("Abide.Guerilla.Tags");

                //Write C#
                GenerateCs(directory, csWriter, tagBlock);

                //End
                csWriter.WriteEndNamespace();

                //Unsuppress
                csWriter.WriteUnIndented("#pragma warning restore CS1591");

                //Close
                csWriter.Close();
            }
        }

        private void GenerateCs(string directory, CsWriter csWriter, TagBlockDefinition tagBlock)
        {
            //Prepare
            TagGroupDefinition tagGroup = null;
            TagFieldSet fieldSet = null;
            TagBlockDefinition childBlock = null;
            TagFieldDefinition[] fields = null;
            TagFieldEnumDefinition enumDefinition = null;
            TagFieldStructDefinition structDefinition = null;
            TagFieldDataDefinition dataDefinition = null;

            //Get Data
            fieldSet = tagBlock.GetFieldSet(tagBlock.TagFieldSetLatestIndex);
            fields = tagBlock.GetLatestFieldDefinitions();

            //Check
            if (tagBlock.IsTagGroup)
            {
                //Lookup tag group
                tagGroup = guerilla.FindTagGroup(tagBlock.Address);

                //Write Tag Group attribute
                if (tagGroup != null)
                    csWriter.WriteAttribute("TagGroup", $"\"{tagGroup.Name}\"", $"\"{tagGroup.GroupTag}\"", $"\"{tagGroup.ParentGroupTag}\"", $"typeof({tagBlock.Name})");
            }
            
            //Write Field set attribute
            csWriter.WriteAttribute("FieldSet", fieldSet.Size, fieldSet.Alignment);

            //Write Block Struct
            csWriter.WriteStartStruct(tagBlock.Name, "public", "unsafe");
            
            //Generate types
            int index = 0;
            foreach (var field in fields)
            {
                string name = GetFriendlyName(field.Name);
                switch (field.Type)
                {
                    case FieldType.FieldCharEnum:
                    case FieldType.FieldEnum:
                    case FieldType.FieldLongEnum:
                        enumDefinition = (TagFieldEnumDefinition)field;
                        csWriter.WriteStartEnum($"Options{index}{PascalFormat(name)}", "public");
                        for (int i = 0; i < enumDefinition.OptionCount; i++)
                            csWriter.Write($"{PascalFormat(enumDefinition.Options[i])}_{i} = {i},");
                        csWriter.WriteEndEnum();
                        break;
                    case FieldType.FieldByteFlags:
                    case FieldType.FieldWordFlags:
                    case FieldType.FieldLongFlags:
                        enumDefinition = (TagFieldEnumDefinition)field;
                        csWriter.WriteStartEnum($"Options{index}{PascalFormat(name)}", "public");
                        for (int i = 0; i < enumDefinition.OptionCount; i++)
                            csWriter.Write($"{PascalFormat(enumDefinition.Options[i])}_{i} = {1 << i},");
                        csWriter.WriteEndEnum();
                        break;
                    case FieldType.FieldBlock:
                        //Create
                        childBlock = guerilla.FindTagBlock(field.DefinitionAddress);
                        using (CsWriter writer = new CsWriter(Path.Combine(directory, $"{childBlock.Name}.cs")))
                        {
                            //Write Usings and namespace
                            writer.WriteUsing("Abide.Guerilla.Types");
                            writer.WriteUsing("Abide.HaloLibrary");
                            writer.Write(string.Empty);

                            //Suppress
                            writer.WriteUnIndented("#pragma warning disable CS1591");
                            writer.WriteStartNamespace("Abide.Guerilla.Tags");
                            
                            //Write
                            GenerateCs(directory, writer, childBlock);

                            //End
                            writer.WriteEndNamespace();

                            //Unsuppress
                            writer.WriteUnIndented("#pragma warning restore CS1591");

                            //Close
                            writer.Close();
                        }
                        break;
                    case FieldType.FieldStruct:
                        //Create
                        structDefinition = (TagFieldStructDefinition)field;
                        childBlock = guerilla.FindTagBlock(structDefinition.BlockDefinitionAddresss);
                        using (CsWriter writer = new CsWriter(Path.Combine(directory, $"{childBlock.Name}.cs")))
                        {
                            //Write Usings and namespace
                            writer.WriteUsing("Abide.Guerilla.Types");
                            writer.WriteUsing("Abide.HaloLibrary");
                            writer.Write(string.Empty);

                            //Suppress
                            writer.WriteUnIndented("#pragma warning disable CS1591");
                            writer.WriteStartNamespace("Abide.Guerilla.Tags");
                            
                            //Write
                            GenerateCs(directory, writer, childBlock);

                            //End
                            writer.WriteEndNamespace();

                            //Unsuppress
                            writer.WriteUnIndented("#pragma warning restore CS1591");

                            //Close
                            writer.Close();
                        }
                        break;
                }

                //Increment
                index++;
            }

            //Write field set
            index = 0;
            foreach (var field in fields)
            {
                string enumName = $"{index}{PascalFormat(GetFriendlyName(field.Name))}";
                string fieldName = $"Field{index}_{PascalFormat(GetFriendlyName(field.Name))}";
                string friendlyFieldName = field.Name.Replace("\"", "\\\"");    // replace " with \" so it becomes an escape sequence.

                switch (field.Type)
                {
                    case FieldType.FieldString:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(String)");
                        csWriter.WriteField("String", fieldName, "public");
                        break;
                    case FieldType.FieldLongString:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(LongString)");
                        csWriter.WriteField("LongString", fieldName, "public");
                        break;

                    case FieldType.FieldStringId:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(StringId)");
                        csWriter.WriteField("StringId", fieldName, "public");
                        break;

                    case FieldType.FieldCharInteger:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(byte)");
                        csWriter.WriteField("byte", fieldName, "public");
                        break;
                    case FieldType.FieldShortInteger:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(short)");
                        csWriter.WriteField("short", fieldName, "public");
                        break;
                    case FieldType.FieldLongInteger:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(int)");
                        csWriter.WriteField("int", fieldName, "public");
                        break;

                    case FieldType.FieldAngle:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(float)");
                        csWriter.WriteField("float", fieldName, "public");
                        break;

                    case FieldType.FieldTag:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(Tag)");
                        csWriter.WriteField("Tag", fieldName, "public");
                        break;

                    case FieldType.FieldByteFlags:
                    case FieldType.FieldCharEnum:
                        enumDefinition = (TagFieldEnumDefinition)field;
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", $"typeof(Options{enumName})");
                        csWriter.WriteField("byte", fieldName, "public");
                        break;
                    case FieldType.FieldWordFlags:
                    case FieldType.FieldEnum:
                        enumDefinition = (TagFieldEnumDefinition)field;
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", $"typeof(Options{enumName})");
                        csWriter.WriteField("short", fieldName, "public");
                        break;
                    case FieldType.FieldLongFlags:
                    case FieldType.FieldLongEnum:
                        enumDefinition = (TagFieldEnumDefinition)field;
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", $"typeof(Options{enumName})");
                        csWriter.WriteField("int", fieldName, "public");
                        break;

                    case FieldType.FieldPoint2D:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(Vector2)");
                        csWriter.WriteField("Vector2", fieldName, "public");
                        break;
                    case FieldType.FieldRectangle2D:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(Vector2)");
                        csWriter.WriteField("Vector2", fieldName, "public");
                        break;

                    case FieldType.FieldRgbColor:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(ColorRgb)");
                        csWriter.WriteField("ColorRgb", fieldName, "public");
                        break;
                    case FieldType.FieldArgbColor:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(ColorArgb)");
                        csWriter.WriteField("ColorArgb", fieldName, "public");
                        break;

                    case FieldType.FieldReal:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(float)");
                        csWriter.WriteField("float", fieldName, "public");
                        break;
                    case FieldType.FieldRealFraction:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(float)");
                        csWriter.WriteField("float", fieldName, "public");
                        break;

                    case FieldType.FieldRealPoint2D:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(Vector2)");
                        csWriter.WriteField("Vector2", fieldName, "public");
                        break;
                    case FieldType.FieldRealPoint3D:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(Vector3)");
                        csWriter.WriteField("Vector3", fieldName, "public");
                        break;

                    case FieldType.FieldRealVector2D:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(Vector2)");
                        csWriter.WriteField("Vector2", fieldName, "public");
                        break;
                    case FieldType.FieldRealVector3D:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(Vector3)");
                        csWriter.WriteField("Vector3", fieldName, "public");
                        break;

                    case FieldType.FieldQuaternion:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(Quaternion)");
                        csWriter.WriteField("Quaternion", fieldName, "public");
                        break;

                    case FieldType.FieldEulerAngles2D:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(Vector2)");
                        csWriter.WriteField("Vector2", fieldName, "public");
                        break;
                    case FieldType.FieldEulerAngles3D:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(Vector3)");
                        csWriter.WriteField("Vector3", fieldName, "public");
                        break;

                    case FieldType.FieldRealPlane2D:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(Vector3)");
                        csWriter.WriteField("Vector3", fieldName, "public");
                        break;
                    case FieldType.FieldRealPlane3D:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(Vector4)");
                        csWriter.WriteField("Vector4", fieldName, "public");
                        break;

                    case FieldType.FieldRealRgbColor:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(ColorRgbF)");
                        csWriter.WriteField("ColorRgbF", fieldName, "public");
                        break;
                    case FieldType.FieldRealArgbColor:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(ColorArgbF)");
                        csWriter.WriteField("ColorArgbF", fieldName, "public");
                        break;

                    case FieldType.FieldRealHsvColor:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(ColorHsvF)");
                        csWriter.WriteField("ColorHsvF", fieldName, "public");
                        break;
                    case FieldType.FieldRealAhsvColor:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(ColorAhsvF)");
                        csWriter.WriteField("ColorAhsvF", fieldName, "public");
                        break;

                    case FieldType.FieldRealShortBounds:
                    case FieldType.FieldRealAngleBounds:
                    case FieldType.FieldRealBounds:
                    case FieldType.FieldRealFractionBounds:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(FloatBounds)");
                        csWriter.WriteField("FloatBounds", fieldName, "public");
                        break;

                    case FieldType.FieldTagReference:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(TagReference)");
                        csWriter.WriteField("TagReference", fieldName, "public");
                        break;

                    case FieldType.FieldBlock:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(TagBlock)");
                        childBlock = guerilla.FindTagBlock(field.DefinitionAddress);
                        csWriter.WriteAttribute("Block", $"\"{childBlock.DisplayName}\"", childBlock.MaximumElementCount, $"typeof({childBlock.Name})");
                        csWriter.WriteField("TagBlock", fieldName, "public");
                        break;

                    case FieldType.FieldCharBlockIndex1:
                    case FieldType.FieldCharBlockIndex2:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", $"typeof(byte)");
                        csWriter.WriteField("byte", fieldName, "public");
                        break;
                    case FieldType.FieldShortBlockIndex1:
                    case FieldType.FieldShortBlockIndex2:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(short)");
                        csWriter.WriteField("short", fieldName, "public");
                        break;
                    case FieldType.FieldLongBlockIndex1:
                    case FieldType.FieldLongBlockIndex2:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(int)");
                        csWriter.WriteField("int", fieldName, "public");
                        break;

                    case FieldType.FieldData:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(TagBlock)");
                        dataDefinition = (TagFieldDataDefinition)field;
                        csWriter.WriteAttribute("Data", dataDefinition.MaximumSize);
                        csWriter.WriteField("TagBlock", fieldName, "public");
                        break;

                    case FieldType.FieldPad:
                    case FieldType.FieldUselessPad:
                    case FieldType.FieldSkip:
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", "typeof(byte*)");
                        csWriter.WriteAttribute("Padding", field.DefinitionAddress);
                        csWriter.Write($"public fixed byte {fieldName}[{field.DefinitionAddress}];");
                        break;

                    case FieldType.FieldStruct:
                        structDefinition = (TagFieldStructDefinition)field;
                        childBlock = guerilla.FindTagBlock(structDefinition.BlockDefinitionAddresss);
                        csWriter.WriteAttribute("Field", $"\"{friendlyFieldName}\"", $"typeof({childBlock.Name})");
                        csWriter.WriteAttribute("Block", $"\"{childBlock.DisplayName}\"", childBlock.MaximumElementCount, $"typeof({childBlock.Name})");
                        csWriter.WriteField(childBlock.Name, fieldName, "public");
                        break;
                }

                //Increment
                index++;
            }

            //Write End
            csWriter.WriteEndStruct();
        }

        private static string GetFriendlyName(string name)
        {
            //Get name
            string value = name;

            //Check
            if (value.Contains("*")) value = value.Substring(0, value.IndexOf('*'));    //Split label
            if (value.Contains("#")) value = value.Substring(0, value.IndexOf('#'));    //Tooltip
            if (value.Contains("^")) value = value.Substring(0, value.IndexOf('^'));    //Tag Block Name
            if (value.Contains(":")) value = value.Substring(0, value.IndexOf(':'));    //Just... why?

            //Return
            return value;
        }

        private static string PascalFormat(string str)
        {
            //Check
            if (string.IsNullOrEmpty(str)) return "_";
            string fixedString = CsWriter.GetSafeString(str, CsWriter.NameType.Class);

            //Prepare
            StringBuilder builder = new StringBuilder();

            //Split
            string[] parts = fixedString.Split('-', '.', ' ', '_', ')', '(', '[', ']', '{', '}');
            foreach (string part in parts)
            {
                if (string.IsNullOrEmpty(part)) continue;
                StringBuilder partBuilder = new StringBuilder(part);
                partBuilder[0] = part.ToUpper()[0];
                builder.Append(partBuilder.ToString());
            }

            //Convert
            return CsWriter.GetSafeString(builder.ToString().Trim(), CsWriter.NameType.Class);
        }
    }
}
