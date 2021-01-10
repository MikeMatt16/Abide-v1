using Abide.HaloLibrary;
using Abide.Ifp;
using Abide.Tag;
using Abide.Tag.Cache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail
{
    public sealed class IfpTagGroup : Group
    {
        private int readWriteOffset = 0;

        public override string GroupName { get; } = "ifp_tag_group";
        public override TagFourCc GroupTag { get; } = "ifp ";

        public IfpTagGroup(IfpDocument ifpDocument)
        {
            GroupTag = ifpDocument.Plugin.Class;

            switch (GroupTag)
            {
                case "sbsp":    //scenario_structure_bsp
                    if (ifpDocument.Plugin.HeaderSize == 588)
                    {
                        return;
                    }
                    break;
            }

            if (ifpDocument.Plugin.Nodes.Count > 0)
            {
                TagBlocks.Add(new IfpTagBlock(ifpDocument.Plugin));
            }
        }
        public override void Read(BinaryReader reader)
        {
            reader.BaseStream.Seek(readWriteOffset, SeekOrigin.Current);
            base.Read(reader);
        }
        public IfpTagGroup(IfpDocument ifpDocument, string groupName) : this(ifpDocument)
        {
            GroupName = groupName;
        }
    }

    public sealed class IfpTagBlock : Block, INotifyPropertyChanged
    {
        private readonly List<Field> labelFields = new List<Field>();

        public event PropertyChangedEventHandler PropertyChanged;
        public override string BlockName { get; } = "ifp_tag_block";
        public override int Alignment { get; } = 4;
        public override string DisplayName => GetDisplayName();

        public IfpTagBlock(IfpNode ifpNode)
        {
            int size;
            if (ifpNode.HeaderSize > ifpNode.TagBlockSize)
            {
                size = ifpNode.HeaderSize;
            }
            else
            {
                size = ifpNode.TagBlockSize;
            }

            AnalyzeFieldSet(ifpNode.Nodes, size);

            int offset = 0, previousOffset = 0;
            Field field = null;

            if(ifpNode.Alignment > 0)
            {
                Alignment = ifpNode.Alignment;
            }

            foreach (var node in ifpNode.Nodes)
            {
                if (node.FieldOffset >= offset)
                {
                    offset = node.FieldOffset;
                }
                else if (node.FieldOffset > 0)
                {
                    continue;
                }

                switch (node.Type)
                {
                    case IfpNodeType.TagBlock:
                        field = new IfpBlockField(node);
                        break;

                    case IfpNodeType.UnusedArray:
                        field = new PadField(node.Name, node.Length);
                        break;

                    case IfpNodeType.Byte:
                    case IfpNodeType.SignedByte:
                        field = new CharIntegerField(node.Name);
                        break;

                    case IfpNodeType.Short:
                    case IfpNodeType.UnsignedShort:
                        field = new ShortIntegerField(node.Name);
                        break;

                    case IfpNodeType.Int:
                    case IfpNodeType.UnsignedInt:
                        field = new LongIntegerField(node.Name);
                        break;

                    case IfpNodeType.Single:
                        field = new RealField(node.Name);
                        break;

                    case IfpNodeType.Enumerator8:
                        field = new CharEnumField(node.Name);
                        break;
                    case IfpNodeType.Enumerator16:
                        field = new EnumField(node.Name);
                        break;
                    case IfpNodeType.Enumerator32:
                        field = new LongEnumField(node.Name);
                        break;
                    case IfpNodeType.Bitfield8:
                        field = new ByteFlagsField(node.Name);
                        break;
                    case IfpNodeType.Bitfield16:
                        field = new WordFlagsField(node.Name);
                        break;
                    case IfpNodeType.Bitfield32:
                        field = new LongFlagsField(node.Name);
                        break;

                    case IfpNodeType.String32:
                        field = new StringField(node.Name);
                        break;

                    case IfpNodeType.String64:
                        break;
                    case IfpNodeType.Unicode128:
                        break;
                    case IfpNodeType.Unicode256:
                        break;

                    case IfpNodeType.Tag:
                        field = new TagField(node.Name);
                        break;

                    case IfpNodeType.TagId:
                        field = new TagIndexField(node.Name);
                        break;

                    case IfpNodeType.StringId:
                        field = new StringIdField(node.Name);
                        break;
                }

                if (field != null)
                {
                    if (node.Type == IfpNodeType.Enumerator8 || node.Type == IfpNodeType.Enumerator16 || node.Type == IfpNodeType.Enumerator32 ||
                    node.Type == IfpNodeType.Bitfield8 || node.Type == IfpNodeType.Bitfield16 || node.Type == IfpNodeType.Bitfield32)
                    {
                        var optionField = (OptionField)field;
                        foreach (var optionNode in node.Nodes)
                        {
                            optionField.Options.Add(new Option(optionNode.Name, optionNode.Value));
                        }
                    }

                    if (previousOffset <= offset)
                    {
                        int length = offset - previousOffset;
                        if (length > 0)
                        {
                            Fields.Add(new PadField("ifp padding", length));
                        }

                        offset += field.Size;
                        previousOffset = offset;
                        Fields.Add(field);
                    }
                }
            }

            if (offset < size)
            {
                Fields.Add(new PadField("ifp padding", size - offset));
            }

            if (!string.IsNullOrEmpty(ifpNode.Label))
            {
                var labelFields = Fields.Where(f => f.Name.Equals(ifpNode.Label, StringComparison.OrdinalIgnoreCase));
                foreach (var labelField in labelFields)
                {
                    labelField.PropertyChanged += LabelField_PropertyChanged;
                    this.labelFields.Add(labelField);
                }
            }
        }

        private void LabelField_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Field.Value))
            {
                PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(nameof(DisplayName)));
            }
        }

        private void AnalyzeFieldSet(IfpNodeCollection nodes, int size)
        {
            List<IfpAnalysisBlock> blocks = new List<IfpAnalysisBlock>();
            int current = 0;

            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];

                if (node.UseFieldOffset)
                {
                    if (node.FieldOffset > current)
                    {
                        blocks.Add(new IfpAnalysisBlock() { Start = current, End = node.FieldOffset });
                    }
                    else if(node.FieldOffset < current)
                    {
                        continue;
                    }
                }

                switch (node.Type)
                {
                    case IfpNodeType.UnusedArray:
                        current += node.Length;
                        break;

                    case IfpNodeType.Byte:
                    case IfpNodeType.SignedByte:
                    case IfpNodeType.Enumerator8:
                    case IfpNodeType.Bitfield8:
                        current++;
                        break;

                    case IfpNodeType.Short:
                    case IfpNodeType.UnsignedShort:
                    case IfpNodeType.Enumerator16:
                    case IfpNodeType.Bitfield16:
                        current += 2;
                        break;

                    case IfpNodeType.Int:
                    case IfpNodeType.UnsignedInt:
                    case IfpNodeType.Single:
                    case IfpNodeType.Enumerator32:
                    case IfpNodeType.Bitfield32:
                    case IfpNodeType.Tag:
                    case IfpNodeType.TagId:
                    case IfpNodeType.StringId:
                        current += 4;
                        break;

                    case IfpNodeType.Long:
                    case IfpNodeType.UnsignedLong:
                    case IfpNodeType.TagBlock:
                    case IfpNodeType.Double:
                    case IfpNodeType.Enumerator64:
                    case IfpNodeType.Bitfield64:
                        current += 8;
                        break;

                    case IfpNodeType.String32:
                        current += 32;
                        break;
                    case IfpNodeType.String64:
                        current += 64;
                        break;
                    case IfpNodeType.Unicode128:
                        current += 128;
                        break;

                    case IfpNodeType.Unicode256:
                        current += 256;
                        break;
                }
            }
        }
        private string GetDisplayName()
        {
            if (labelFields.Any())
            {
                return string.Join(" ", labelFields.Select(f => GetFieldValueString(f)));
            }

            return BlockName;
        }

        private string GetFieldValueString(Field field)
        {
            switch (field)
            {
                case BlockField blockField:
                    break;

                case StringIdField stringIdField:
                    break;

                case TagReferenceField tagReferenceField:
                    break;

                case TagIndexField tagIndexField:
                    break;

                default:
                    return field.Value.ToString();
            }

            return field.Value.ToString();
        }

        private class IfpAnalysisBlock
        {
            public int Start { get; set; }
            public int End { get; set; }
        }
    }

    public sealed class IfpBlockField : BlockField
    {
        private readonly IfpNode node = null;

        public override int Size => 8;

        public IfpBlockField(IfpNode structIfpNode) : base(structIfpNode.Name, ushort.MaxValue)
        {
            node = structIfpNode;
        }

        public override Block Add(out bool success)
        {
            if (node.MaxElements > 0 && BlockList.Count >= node.MaxElements)
            {
                success = false;
                return null;
            }

            var block = Create();
            BlockList.Add(block, out success);
            return block;
        }

        public override bool Add(out Block block)
        {
            if (node.MaxElements > 0 && BlockList.Count >= node.MaxElements)
            {
                block = null;
                return false;
            }

            block = Create();
            BlockList.Add(block, out bool success);
            return success;
        }

        public override Block Create()
        {
            return new IfpTagBlock(node);
        }
    }
}
