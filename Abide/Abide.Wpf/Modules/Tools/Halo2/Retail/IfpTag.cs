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

        public override TagFourCc Tag { get; } = "ifp ";
        public override string Name { get; } = "ifp_tag_group";

        public IfpTagGroup(IfpDocument ifpDocument)
        {
            Tag = ifpDocument.Plugin.Class;

            switch (Tag)
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
            if (!string.IsNullOrEmpty(ifpDocument.Plugin.Class))
            {
                Tag = ifpDocument.Plugin.Class;
            }
            
            Name = groupName;
        }
    }

    public sealed class IfpTagBlock : Block
    {
        private readonly List<Field> labelFields = new List<Field>();
        private readonly int alignment = 4;

        public override string Name => "ifp_tag_block";
        public override int Alignment => alignment;
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
            if (ifpNode.Alignment > 0)
            {
                alignment = ifpNode.Alignment;
            }

            bool skipId = false;
            foreach (var node in ifpNode.Nodes)
            {
                Field field = null;
                if (node.FieldOffset >= offset)
                {
                    offset = node.FieldOffset;
                }
                else if (node.FieldOffset > 0)
                {
                    continue;
                }

                if (node.Type == IfpNodeType.Tag)
                {
                    int nodeIndex = ifpNode.Nodes.IndexOf(node);
                    if (ifpNode.Nodes.Count > nodeIndex && ifpNode.Nodes[nodeIndex + 1].Type == IfpNodeType.TagId)
                    {
                        skipId = true;
                        continue;
                    }
                }
                else if (node.Type == IfpNodeType.TagId && skipId)
                {
                    skipId = false;
                    field = new TagReferenceField(node.Name);
                }
                else
                {
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
                    this.labelFields.Add(labelField);
                }
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

            return Name;
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

        public IfpBlockField(IfpNode structIfpNode) : base(structIfpNode.Name, ushort.MaxValue)
        {
            node = structIfpNode;
        }
        public override Block Create()
        {
            return new IfpTagBlock(node);
        }
        protected override Field CloneField()
        {
            var f = new IfpBlockField(node);

            foreach (var block in BlockList)
            {
                if (!f.BlockList.Add((Block)block.Clone()))
                {
                    Console.WriteLine("Unable to add block to cloned block field.");
                    break;
                }
            }

            return f;
        }
    }
}
