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
            //Prepare
            StringBuilder xmlBuilder = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };

            //Initialize
            using (XmlWriter writer = XmlWriter.Create(xmlBuilder, settings))
            {

                //Write Start
                writer.WriteStartDocument();

                //Get Tag Block
                if (e.Node.Tag is TagBlockDefinition block)
                    GenerateXml(writer, block);

                //End
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
            }

            //Clear
            xmlRichTextBox.Clear();
            xmlRichTextBox.AppendText(xmlBuilder.ToString());
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
    }
}
