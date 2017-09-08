using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Tag_Data_Editor.Controls;

namespace Tag_Data_Editor.Halo2
{
    public class TagDataEditor : FlowLayoutPanel
    {
        public MapFile Map
        {
            get { return map; }
            set { map = value; }
        }
        public IndexEntry Entry
        {
            get { return entry; }
            set { Entry_Load(value); }
        }
        public TagEditor Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        private MapFile map = null;
        private IndexEntry entry = null;
        private TagEditor owner = null;
        
        public TagDataEditor()
        {
            //Setup
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            FlowDirection = FlowDirection.TopDown;
            Location = new Point(12, 12);
            Name = "nestedPanel";
            Size = new Size(0, 0);
            TabIndex = 0;
            WrapContents = false;
        }

        private void Entry_Load(IndexEntry entry)
        {
            //Check
            if (entry == null) return;
            if (this.entry == entry) return;

            //Prepare
            string file = Path.Combine(AbideRegistry.Halo2PluginsDirectory, $"{entry.Root.FourCc.Replace('<', '_').Replace('>', '_')}.ent");
            bool newLayout = this.entry == null || this.entry.Root != entry.Root && File.Exists(file);
            bool load = File.Exists(file);

            //Set
            this.entry = entry;

            //Load Document?
            if (newLayout)
            {
                //Load document
                XmlDocument document = new XmlDocument();
                document.Load(file);

                //Clear
                Controls.Clear();

                //Layout
                LayoutControls(document);
            }

            //Setup
            //if (load) SetupControls(entry.PostProcessedOffset, this);
        }

        private void LayoutControls(XmlDocument doc)
        {
            //Get Plugin element
            var plugin = doc["plugin"];

            //Check
            if (plugin?.Attributes["class"]?.Value == entry.Root)
                foreach (XmlNode node in plugin?.ChildNodes)
                    CreateControl(node, Controls);
        }
        
        private void CreateControl(XmlNode node, ControlCollection controls)
        {
            //Prepare
            MetaControl control = null;
            Control owner = controls.Owner;

            //Handle
            switch (node.Name)
            {
                case "int":
                case "uint":
                case "short":
                case "ushort":
                case "byte":
                case "sbyte":
                case "float":
                case "undefined":
                    ValueControl valueControl = new ValueControl();
                    valueControl.Type = node.Name;
                    valueControl.ControlName = node.Attributes["name"]?.Value;
                    valueControl.ValueChanged = valueControl_ValueChanged;
                    control = valueControl;
                    break;

                case "enum8":
                case "enum16":
                case "enum32":
                    EnumControl enumControl = new EnumControl();
                    enumControl.Type = node.Name;
                    enumControl.ControlName = node.Attributes["name"]?.Value;
                    SetupEnum(node, enumControl);
                    enumControl.OptionSelected = enumControl_OptionSelected;
                    control = enumControl;
                    break;

                case "bitmask8":
                case "bitmask16":
                case "bitmask32":
                    BitflagsControl bitControl = new BitflagsControl();
                    bitControl.Type = node.Name;
                    bitControl.ControlName = node.Attributes["name"]?.Value;
                    SetupBitfield(node, bitControl);
                    bitControl.FlagsChanged = bitControl_FlagsChanged;
                    control = bitControl;
                    break;

                case "id":
                    TagControl tagControl = new TagControl();
                    tagControl.Type = node.Name;
                    tagControl.ControlName = node.Attributes["name"]?.Value;
                    tagControl.TagButtonClick = tagControl_TagButtonClick;
                    tagControl.GoToButtonClick = tagControl_GoToButtonClick;
                    control = tagControl;
                    break;

                case "stringid":
                    StringIDControl stringIdControl = new StringIDControl();
                    stringIdControl.Type = node.Name;
                    stringIdControl.ControlName = node.Attributes["name"]?.Value;
                    stringIdControl.StringButtonClick = stringIdControl_StringButtonClick;
                    control = stringIdControl;
                    break;

                case "string32":
                    StringControl string32Control = new StringControl();
                    string32Control.Type = node.Name;
                    string32Control.ControlName = node.Attributes["name"]?.Value;
                    string32Control.Length = 32;
                    string32Control.StringChanged = stringControl_StringChanged;
                    control = string32Control;
                    break;

                case "string64":
                    StringControl string64Control = new StringControl();
                    string64Control.Type = node.Name;
                    string64Control.ControlName = node.Attributes["name"]?.Value;
                    string64Control.Length = 64;
                    string64Control.StringChanged = stringControl_StringChanged;
                    control = string64Control;
                    break;

                case "unicode128":
                    StringControl unicode128Control = new StringControl();
                    unicode128Control.Type = node.Name;
                    unicode128Control.ControlName = node.Attributes["name"]?.Value;
                    unicode128Control.Length = 128;
                    unicode128Control.StringChanged = stringControl_StringChanged;
                    control = unicode128Control;
                    break;

                case "unicode256":
                    StringControl unicode256Control = new StringControl();
                    unicode256Control.Type = node.Name;
                    unicode256Control.ControlName = node.Attributes["name"]?.Value;
                    unicode256Control.Length = 256;
                    unicode256Control.StringChanged = stringControl_StringChanged;
                    control = unicode256Control;
                    break;

                case "struct":
                    TagBlockControl TagBlockControl = new TagBlockControl();
                    TagBlockControl.Type = node.Name;
                    TagBlockControl.ControlName = node.Attributes["name"]?.Value;
                    TagBlockControl.SelectedBlockChanged = tagBlockControl_SelectedBlockChanged;
                    SetupTagBlock(node, TagBlockControl);
                    control = TagBlockControl;
                    break;
            }

            //Check
            if (control != null)
            {
                control.PluginElement = node;
                controls.Add(control);
            }
        }

        private void SetupTagBlock(XmlNode structElement, TagBlockControl control)
        {
            //Loop
            foreach (XmlNode node in structElement.ChildNodes)
                CreateControl(node, control.Contents.Controls);
        }

        private void SetupBitfield(XmlNode bitmaskElement, BitflagsControl control)
        {
            foreach (XmlNode child in bitmaskElement.ChildNodes)
                if (child.Name == "option")
                    control.AddOption(1 << int.Parse(child.Attributes["value"].Value), child.Attributes["name"].Value);
        }

        private void SetupEnum(XmlNode enumElement, EnumControl control)
        {
            foreach (XmlNode child in enumElement.ChildNodes)
                if (child.Name == "option")
                    control.AddOption(int.Parse(child.Attributes["value"].Value), child.Attributes["name"].Value);
        }

        private void SetupControls(long address, Control container)
        {
            //Prepare
            long controlAddress = 0;

            //Prepare I/0
            using (BinaryReader tagReader = new BinaryReader(entry.TagData))
                foreach (MetaControl control in container.Controls)
                    if (control.FieldOffset >= 0)
                    {
                        //Get Address
                        controlAddress = address + control.FieldOffset;
                        entry.TagData.Seek(controlAddress, SeekOrigin.Begin);
                        control.Address = controlAddress;

                        //Handle type
                        switch (control.PluginElement.Name)
                        {
                            case "int":
                            case "uint":
                            case "short":
                            case "ushort":
                            case "byte":
                            case "sbyte":
                            case "float":
                            case "undefined": ReadValue((ValueControl)control, tagReader); break;

                            case "enum8":
                            case "enum16":
                            case "enum32": ReadEnum((EnumControl)control, tagReader); break;

                            case "bitmask8":
                            case "bitmask16":
                            case "bitmask32": ReadBitmask((BitflagsControl)control, tagReader); break;

                            case "id": ReadTagId((TagControl)control, tagReader); break;
                            case "stringid": ReadStringId((StringIDControl)control, tagReader); break;

                            case "string32":
                            case "string64":
                            case "unicode128":
                            case "unicode256": ReadString((StringControl)control, tagReader); break;

                            case "struct": ReadStructure((TagBlockControl)control, tagReader); break;
                        }
                    }
        }

        private void ReadStructure(TagBlockControl control, BinaryReader tagReader)
        {
            //Prepare
            TagBlock tagBlock = TagBlock.Zero;
            string labelName = null;

            //Check
            if (control.FieldSize > 0)
            {
                //Read
                tagBlock = tagReader.ReadStructure<TagBlock>();

                //Get Label name
                labelName = control.PluginElement.Attributes["label"]?.Value;

                //Prepare
                BlockOption[] options = new BlockOption[tagBlock.Count];
                for (int i = 0; i < tagBlock.Count; i++)
                    options[i] = new BlockOption(tagBlock.Offset, i, control.FieldSize, control.ControlName);

                //Set
                control.SetBlocks(options);

                //Select
                if (tagBlock.Count > 0) { control.Enabled = true; control.SelectedBlock = 0; }
                else { control.Enabled = false; }
            }
        }

        private void ReadString(StringControl control, BinaryReader tagReader)
        {
            //Prepare
            string value = string.Empty;

            //Read
            switch (control.PluginElement.Name)
            {
                case "string32": value = tagReader.ReadUTF8(32); break;
                case "string64": value = tagReader.ReadUTF8(64); break;
                case "unicode128": value = tagReader.ReadUTF8(128); break;
                case "unicode256": value = tagReader.ReadUTF8(256); break;
            }

            //Set
            control.String = value.Replace("\0", string.Empty);
        }

        private void ReadStringId(StringIDControl control, BinaryReader tagReader)
        {
            //Prepare
            string stringValue = string.Empty;

            //Read
            StringId id = tagReader.ReadInt32();
            if (map.Strings.Count > id.Index)
            {
                //Set
                control.SelectedString = map.Strings[id.Index];
                control.StringLabel = map.Strings[id.Index];
            }
            else control.StringLabel = "Welp... This is embarassing XD";
        }

        private void ReadTagId(TagControl control, BinaryReader tagReader)
        {
            //Read
            TagId id = tagReader.ReadInt32();
            string label = "Nulled";

            //Set
            if (id.IsNull) control.SelectedEntry = null;
            else
            {
                control.SelectedEntry = map.IndexEntries[id];
                if (control.SelectedEntry != null) label = $"{control.SelectedEntry.Filename}.{control.SelectedEntry.Root}";
            }

            //Set
            control.TagLabel = label;
        }

        private void ReadBitmask(BitflagsControl control, BinaryReader tagReader)
        {
            //Prepare
            uint flags = 0;

            //Read
            switch (control.PluginElement.Name)
            {
                case "bitmask8": flags = tagReader.ReadByte(); break;
                case "bitmask16": flags = tagReader.ReadUInt16(); break;
                case "bitmask32": flags = tagReader.ReadUInt32(); break;
            }

            //Set
            control.Flags = flags;
        }

        private void ReadEnum(EnumControl control, BinaryReader tagReader)
        {
            //Prepare
            long value = 0;

            //Read
            switch (control.PluginElement.Name)
            {
                case "enum8": value = tagReader.ReadByte(); break;
                case "enum16": value = tagReader.ReadUInt16(); break;
                case "enum32": value = tagReader.ReadUInt32(); break;
            }

            //Set
            control.SelectedOption = value;
        }

        private void ReadValue(ValueControl control, BinaryReader tagReader)
        {
            //Prepare
            string value = string.Empty;

            //Read
            switch (control.PluginElement.Name)
            {
                case "int": value = tagReader.ReadInt32().ToString(); break;
                case "undefined":
                case "uint": value = tagReader.ReadUInt32().ToString(); break;
                case "short": value = tagReader.ReadInt16().ToString(); break;
                case "ushort": value = tagReader.ReadUInt16().ToString(); break;
                case "byte": value = tagReader.ReadByte().ToString(); break;
                case "sbyte": value = tagReader.ReadSByte().ToString(); break;
                case "float": value = tagReader.ReadSingle().ToString(); break;
            }

            //Set
            control.Value = value;
        }

        private void valueControl_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueControl valueControl = (ValueControl)sender;
            entry.TagData.Seek(valueControl.Address, SeekOrigin.Begin);
            using (BinaryWriter writer = new BinaryWriter(entry.TagData))
                switch (valueControl.PluginElement.Name)
                {
                    case "int": int int32Value = 0; if (int.TryParse(e.Value, out int32Value)) writer.Write(int32Value); break;
                    case "undefined":
                    case "uint": uint uint32Value = 0; if (uint.TryParse(e.Value, out uint32Value)) writer.Write(uint32Value); break;
                    case "short": short int16Value = 0; if (short.TryParse(e.Value, out int16Value)) writer.Write(int16Value); break;
                    case "ushort": ushort uint16Value = 0; if (ushort.TryParse(e.Value, out uint16Value)) writer.Write(uint16Value); break;
                    case "byte": byte uint8Value = 0; if (byte.TryParse(e.Value, out uint8Value)) writer.Write(uint8Value); break;
                    case "sbyte": sbyte int8Value = 0; if (sbyte.TryParse(e.Value, out int8Value)) writer.Write(int8Value); break;
                    case "float": float singleValue = 0; if (float.TryParse(e.Value, out singleValue)) writer.Write(singleValue); break;
                }
        }

        private void stringControl_StringChanged(object sender, StringChangedEventArgs e)
        {
            StringControl stringControl = (StringControl)sender;
            byte[] dataBuffer = null;
            switch (stringControl.PluginElement.Name)
            {
                case "string32": dataBuffer = new byte[32]; break;
                case "string64": dataBuffer = new byte[64]; break;
                case "unicode128": dataBuffer = new byte[128]; break;
                case "unicode256": dataBuffer = new byte[256]; break;
            }

            if (dataBuffer != null)
            {
                using (MemoryStream ms = new MemoryStream(dataBuffer))
                using (BinaryWriter writer = new BinaryWriter(ms))
                    writer.WriteUTF8(e.String);

                entry.TagData.Seek(stringControl.Address);
                using (BinaryWriter writer = new BinaryWriter(entry.TagData))
                    writer.Write(dataBuffer);
            }
        }

        private void enumControl_OptionSelected(object sender, OptionSelectEventArgs e)
        {
            //Get Enum Control
            EnumControl enumControl = (EnumControl)sender;
            entry.TagData.Seek(enumControl.Address);
            using (BinaryWriter writer = new BinaryWriter(entry.TagData))
                switch (enumControl.PluginElement.Name)
                {
                    case "enum8": writer.Write((byte)e.SelectedOption.Value); break;
                    case "enum16": writer.Write((ushort)e.SelectedOption.Value); break;
                    case "enum32": writer.Write((uint)e.SelectedOption.Value); break;
                }
        }

        private void bitControl_FlagsChanged(object sender, FlagsChangedEventArgs e)
        {
            //Get Bitflags Control
            BitflagsControl bitControl = (BitflagsControl)sender;
            entry.TagData.Seek(bitControl.Address);
            using (BinaryWriter writer = new BinaryWriter(entry.TagData))
                switch (bitControl.PluginElement.Name)
                {
                    case "bitmask8": writer.Write((byte)e.Flags); break;
                    case "bitmask16": writer.Write((ushort)e.Flags); break;
                    case "bitmask32": writer.Write(e.Flags); break;
                }
        }

        private void tagControl_GoToButtonClick(object sender, TagButtonEventArgs e)
        {
            //Get Tag Control
            TagControl tagControl = (TagControl)sender;

            //Check
            if (tagControl.SelectedEntry != null) owner.Host.Request(owner, "SelectEntry", tagControl.SelectedEntry.Id);
        }

        private void tagControl_TagButtonClick(object sender, TagButtonEventArgs e)
        {
            //Get Tag Control
            TagControl tagControl = (TagControl)sender;

            //Get Id
            TagId selectedId = tagControl.SelectedEntry == null ? TagId.Null : tagControl.SelectedEntry.Id;
            selectedId = (TagId)owner.Host.Request(owner, "TagBrowserDialog", selectedId);

            //Setup control
            tagControl.SelectedEntry = selectedId.IsNull ? null : map.IndexEntries[selectedId];
            tagControl.TagLabel = selectedId.IsNull ? "Nulled" : $"{map.IndexEntries[selectedId].Filename}.{map.IndexEntries[selectedId].Root}";

            //Write
            entry.TagData.Seek(tagControl.Address, SeekOrigin.Begin);
            using (BinaryWriter writer = new BinaryWriter(entry.TagData))
                writer.Write(selectedId);
        }

        private void stringIdControl_StringButtonClick(object sender, StringButtonEventArgs e)
        {
            //Get StringID Control
            StringIDControl stringControl = (StringIDControl)sender;

            //Get Id
            StringId selectedId = map.Strings[stringControl.SelectedString];
            selectedId = (StringId)owner.Host.Request(owner, "StringBrowserDialog", selectedId);

            //Setup control
            stringControl.SelectedString = map.Strings[selectedId.Index];
            stringControl.StringLabel = map.Strings[selectedId.Index];

            //Write
            entry.TagData.Seek(stringControl.Address, SeekOrigin.Begin);
            using (BinaryWriter writer = new BinaryWriter(entry.TagData))
                writer.Write(selectedId);
        }

        private void tagBlockControl_SelectedBlockChanged(object sender, BlockOptionEventArgs e)
        {
            //Get TagBlock
            TagBlockControl tagBlockControl = (TagBlockControl)sender;

            //Setup children
            SetupControls(e.Option.Address + (e.Option.BlockIndex * e.Option.BlockLength), tagBlockControl.Contents);
        }
    }
}
