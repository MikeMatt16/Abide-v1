using Abide.Guerilla.Managed;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

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
            LoadGuerilla();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
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
            //Err
            string dir = @"G:\Abide.Guerilla.Tags";

            //Check
            if (File.Exists(Program.H2GuerillaPath) && File.Exists(Program.H2alangPath))
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
                    TagBlockDefinition block = guerilla.SearchTagBlocks(tagGroup.DefinitionAddress);

                    //Create group node
                    TreeNode groupNode = new TreeNode($"[{tagGroup.GroupTag}] - {block?.DisplayName ?? tagGroup.Name}");
                    groupNode.Tag = block;

                    //Build Node hierarchy
                    if (block != null) LoadBlock(block, groupNode, block);

                    //Add Node
                    tagGroupTreeView.Nodes.Add(groupNode);

                    //Create
                    using (CsWriter writer = new CsWriter(Path.Combine(dir, $"{tagGroup.Name}.cs")))
                    {
                        //Write Usings and namespace
                        writer.WriteUsing("Abide.Guerilla.Types");
                        writer.WriteUsing("Abide.HaloLibrary");
                        writer.WriteStartNamespace("Abide.Guerilla.Tags");

                        //Suppress
                        writer.WriteUnIndented("#pragma warning disable CS1591");

                        //Write C#
                        GenerateCs(writer, block);

                        //End
                        writer.WriteEndNamespace();

                        //Unsuppress
                        writer.WriteUnIndented("#pragma warning restore CS1591");

                        //Close
                        writer.Close();
                    }
                }

                //End
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
                foreach (TagFieldDefinition field in block.GetFieldDefinitionsH2Xbox())
                    switch (field.Type)
                    {
                        case FieldType.FieldBlock:
                            //Prepare
                            childBlock = guerilla.SearchTagBlocks(field.DefinitionAddress);
                            fieldSet = childBlock.GetFieldSetH2Xbox();
                            childBlockNode = new TreeNode($"{childBlock.DisplayName} Size: {fieldSet.Size} Alignment: {fieldSet.Alignment}");
                            childBlockNode.Tag = groupBlock;

                            //Build Node hierarchy
                            if (block != null) LoadBlock(groupBlock, childBlockNode, childBlock);
                            blockNode.Nodes.Add(childBlockNode);
                            break;
                        case FieldType.FieldStruct:
                            //Prepare
                            TagStructDefinition structDef = (TagStructDefinition)field;
                            childBlock = guerilla.SearchTagBlocks(structDef.BlockDefinitionAddresss);

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
                StringBuilder xmlBuilder = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };

                //Initialize
                using (XmlWriter xmlWriter = XmlWriter.Create(xmlBuilder, settings))
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

                //Clear
                xmlRichTextBox.Clear();
                xmlRichTextBox.AppendText(xmlBuilder.ToString());

                //Write CS file
                using (CsWriter csWriter = new CsWriter(@"G:\Abide.Guerilla.Tag.cs"))
                {
                    //Write Usings and namespace
                    csWriter.WriteUsing("Abide.Guerilla.Types");
                    csWriter.WriteStartNamespace("Abide.Guerilla.Tags");

                    //Write C#
                    GenerateCs(csWriter, block);

                    //End
                    csWriter.WriteEndNamespace();

                    //Close
                    csWriter.Close();
                }
            }
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
            TagFieldSet fieldSet = tagBlock.GetFieldSetH2Xbox();
            TagFieldDefinition[] fields = tagBlock.GetFieldDefinitionsH2Xbox();
            TagBlockDefinition childBlock = null;
            TagFieldSet childSet = null;

            //Check
            if (tagBlock.IsTagGroup)
            {
                //Lookup tag group
                TagGroupDefinition tagGroup = guerilla.SearchTagGroups(tagBlock.Address);
                
                //Write Start
                writer.WriteStartElement("plugin");
                writer.WriteStartAttribute("class"); writer.WriteValue(tagGroup.GroupTag); writer.WriteEndAttribute();
                writer.WriteStartAttribute("author"); writer.WriteValue("Abide.Guerilla"); writer.WriteEndAttribute();
                writer.WriteStartAttribute("headersize"); writer.WriteValue(fieldSet.Size); writer.WriteEndAttribute();
            }

            //Loop
            foreach (var field in fields)
                switch (field.Type)
                {
                    case FieldType.FieldBlock:
                        //Get Block
                        childBlock = guerilla.SearchTagBlocks(field.DefinitionAddress);
                        childSet = childBlock.GetFieldSetH2Xbox();

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
            ExplanationDefinition explanationDefinition = null;
            TagStructDefinition structDefinition = null;
            EnumDefinition enumDefinition = null;
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
                    enumDefinition = (EnumDefinition)tagField;
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
                    enumDefinition = (EnumDefinition)tagField;
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
                    enumDefinition = (EnumDefinition)tagField;
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
                    enumDefinition = (EnumDefinition)tagField;
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
                    enumDefinition = (EnumDefinition)tagField;
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
                    enumDefinition = (EnumDefinition)tagField;
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
                    childBlock = guerilla.SearchTagBlocks(tagField.DefinitionAddress);
                    childFieldSet = childBlock.GetFieldSetH2Xbox();
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
                    TagDataDefinition data = (TagDataDefinition)tagField;
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
                    explanationDefinition = (ExplanationDefinition)tagField;
                    writer.WriteStartElement("explanation");
                    writer.WriteValue(explanationDefinition.Explanation);
                    writer.WriteEndElement();
                    break;
                case FieldType.FieldStruct:
                    structDefinition = (TagStructDefinition)tagField;
                    childBlock = guerilla.SearchTagBlocks(structDefinition.BlockDefinitionAddresss);
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

        private void GenerateCs(CsWriter csWriter, TagBlockDefinition tagBlock)
        {
            //Prepare
            TagFieldSet fieldSet = null, childFieldSet = null;
            TagBlockDefinition childBlock = null;
            TagFieldDefinition[] fields = null;
            EnumDefinition enumDefinition = null;
            TagDataDefinition dataDefinition = null;

            //Get Data
            fieldSet = tagBlock.GetFieldSetH2Xbox();
            fields = tagBlock.GetFieldDefinitionsH2Xbox();

            //Check
            if (tagBlock.IsTagGroup)
            {
                //Lookup tag group
                TagGroupDefinition tagGroup = guerilla.SearchTagGroups(tagBlock.Address);

                //Write Tag Group attribute
                csWriter.WriteAttribute("TagGroup", $"\"{tagGroup.Name}\"", $"\"{tagGroup.GroupTag}\"", $"\"{tagGroup.ParentGroupTag}\"", $"typeof({tagBlock.Name})");
            }
            
            //Write Field set attribute
            csWriter.WriteAttribute("FieldSet", fieldSet.Size, fieldSet.Alignment);

            //Write Block Struct
            csWriter.WriteStartStruct(tagBlock.Name, "public", "unsafe");

            //Generate types
            foreach (var field in fields)
                switch (field.Type)
                {
                    case FieldType.FieldCharEnum:
                    case FieldType.FieldEnum:
                    case FieldType.FieldLongEnum:
                        enumDefinition = (EnumDefinition)field;
                        csWriter.WriteStartEnum($"{PascalFormat(enumDefinition.Name)}Options", "public");
                        for (int i = 0; i < enumDefinition.OptionCount; i++)
                            csWriter.Write($"{PascalFormat(enumDefinition.Options[i])}_{i} = {i},");
                        csWriter.WriteEndEnum();
                        break;
                    case FieldType.FieldByteFlags:
                    case FieldType.FieldWordFlags:
                    case FieldType.FieldLongFlags:
                        enumDefinition = (EnumDefinition)field;
                        csWriter.WriteStartEnum($"{PascalFormat(enumDefinition.Name)}Options", "public");
                        for (int i = 0; i < enumDefinition.OptionCount; i++)
                            csWriter.Write($"{PascalFormat(enumDefinition.Options[i])}_{i} = {1 << i},");
                        csWriter.WriteEndEnum();
                        break;
                    case FieldType.FieldBlock:
                        //Get Block
                        childBlock = guerilla.SearchTagBlocks(field.DefinitionAddress);
                        GenerateCs(csWriter, childBlock);
                        break;
                }

            //Write field set
            int index = 0;
            foreach (var field in fields)
            {
                string fieldName = CsWriter.GetSafeString(field.Name, CsWriter.NameType.Field);

                switch (field.Type)
                {
                    case FieldType.FieldString:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("String", $"{fieldName}_{index}", "public");
                        break;
                    case FieldType.FieldLongString:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("LongString", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldStringId:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("StringId", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldCharInteger:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("int", $"{fieldName}_{index}", "public");
                        break;
                    case FieldType.FieldShortInteger:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("short", $"{fieldName}_{index}", "public");
                        break;
                    case FieldType.FieldLongInteger:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("int", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldAngle:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("float", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldTag:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("Tag", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldByteFlags:
                    case FieldType.FieldCharEnum:
                        enumDefinition = (EnumDefinition)field;
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", $"typeof({PascalFormat(enumDefinition.Name)}Options)");
                        csWriter.WriteField("byte", $"{fieldName}_{index}", "public");
                        break;
                    case FieldType.FieldWordFlags:
                    case FieldType.FieldEnum:
                        enumDefinition = (EnumDefinition)field;
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", $"typeof({PascalFormat(enumDefinition.Name)}Options)");
                        csWriter.WriteField("short", $"{fieldName}_{index}", "public");
                        break;
                    case FieldType.FieldLongFlags:
                    case FieldType.FieldLongEnum:
                        enumDefinition = (EnumDefinition)field;
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", $"typeof({PascalFormat(enumDefinition.Name)}Options)");
                        csWriter.WriteField("int", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldPoint2D:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("Vector2", $"{fieldName}_{index}", "public");
                        break;
                    case FieldType.FieldRectangle2D:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("Rectangle2", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldRgbColor:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("ColorRgb", $"{fieldName}_{index}", "public");
                        break;
                    case FieldType.FieldArgbColor:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("ColorArgb", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldReal:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("float", $"{fieldName}_{index}", "public");
                        break;
                    case FieldType.FieldRealFraction:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("float", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldRealPoint2D:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("Vector2", $"{fieldName}_{index}", "public");
                        break;
                    case FieldType.FieldRealPoint3D:
                        csWriter.WriteField("Vector3", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldRealVector2D:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("Vector2", $"{fieldName}_{index}", "public");
                        break;
                    case FieldType.FieldRealVector3D:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("Vector3", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldQuaternion:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("Quaternion", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldEulerAngles2D:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("Vector2", $"{fieldName}_{index}", "public");
                        break;
                    case FieldType.FieldEulerAngles3D:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("Vector3", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldRealPlane2D:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("Plane2", $"{fieldName}_{index}", "public");
                        break;
                    case FieldType.FieldRealPlane3D:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("Plane3", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldRealRgbColor:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("ColorFRgb", $"{fieldName}_{index}", "public");
                        break;
                    case FieldType.FieldRealArgbColor:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("ColorArgbF", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldRealHsvColor:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("ColorHsvF", $"{fieldName}_{index}", "public");
                        break;
                    case FieldType.FieldRealAhsvColor:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("ColorAhsvF", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldRealShortBounds:
                    case FieldType.FieldRealAngleBounds:
                    case FieldType.FieldRealBounds:
                    case FieldType.FieldRealFractionBounds:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("FloatBounds", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldTagReference:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.WriteField("TagReference", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldBlock:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        childBlock = guerilla.SearchTagBlocks(field.DefinitionAddress);
                        csWriter.WriteAttribute("Block", $"\"{childBlock.DisplayName}\"", childBlock.MaximumElementCount, $"typeof({childBlock.Name})");
                        csWriter.WriteField("TagBlock", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldCharBlockIndex1:
                    case FieldType.FieldCharBlockIndex2:
                        break;
                    case FieldType.FieldShortBlockIndex1:
                    case FieldType.FieldShortBlockIndex2:
                        break;
                    case FieldType.FieldLongBlockIndex1:
                    case FieldType.FieldLongBlockIndex2:
                        break;

                    case FieldType.FieldData:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        dataDefinition = (TagDataDefinition)field;
                        csWriter.WriteAttribute("Data", dataDefinition.MaximumSize);
                        csWriter.WriteField("TagBlock", $"{fieldName}_{index}", "public");
                        break;

                    case FieldType.FieldPad:
                    case FieldType.FieldUselessPad:
                    case FieldType.FieldSkip:
                        csWriter.WriteAttribute("Field", $"\"{field.Name}\"", "null");
                        csWriter.Write($"public fixed byte {fieldName}_{index}[{field.DefinitionAddress}];");
                        break;

                    case FieldType.FieldStruct:
                        break;
                }

                //Increment
                index++;
            }

            //Write End
            csWriter.WriteEndStruct();
        }

        private static string PascalFormat(string str)
        {
            //Check
            if (string.IsNullOrEmpty(str)) return "_";
            string fixedString = CsWriter.GetSafeString(str, CsWriter.NameType.Class);

            //Prepare
            StringBuilder builder = new StringBuilder();

            //Split
            string[] parts = str.Split('-', '.', ' ', '_', ')', '(', '[', ']', '{', '}');
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
