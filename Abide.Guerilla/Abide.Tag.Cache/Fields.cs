using Abide.HaloLibrary;
using Abide.Tag.Definition;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Abide.Tag.Cache
{
    public class BaseStringIdField : Field
    {
        public sealed override int Size => 4;
        public new StringId Value
        {
            get => (StringId)base.Value;
            set => base.Value = value;
        }
        public BaseStringIdField(FieldType type, string name) : base(type, name)
        {
            Value = StringId.Zero;
        }
        protected sealed override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<StringId>();
        }
        protected sealed override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class StringIdField : BaseStringIdField
    {
        public StringIdField(string name) : base(FieldType.FieldStringId, name)
        {
            Value = StringId.Zero;
        }
    }

    public sealed class OldStringIdField : BaseStringIdField
    {
        public OldStringIdField(string name) : base(FieldType.FieldOldStringId, name)
        {
            Value = StringId.Zero;
        }
    }

    public sealed class TagReferenceField : Field
    {
        public override int Size => 8;
        public string GroupTag { get; }
        public TagReference Null
        {
            get { return new TagReference() { Id = TagId.Null, Tag = GroupTag }; }
        }
        public new TagReference Value
        {
            get => (TagReference)FieldValue;
            set => FieldValue = value;
        }
        public TagReferenceField(string name, string groupTag = "") : base(FieldType.FieldTagReference, name)
        {
            GroupTag = groupTag;
            Value = new TagReference() { Tag = groupTag, Id = TagId.Null };
        }
        public TagReferenceField(string name, int groupTag = 0) : base(FieldType.FieldTagReference, name)
        {
            GroupTag = Encoding.UTF8.GetString(BitConverter.GetBytes(groupTag).Reverse().ToArray()).Trim('\0');
            Value = new TagReference() { Tag = (TagFourCc)(uint)groupTag, Id = TagId.Null };
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<TagReference>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write<TagReference>(Value);
        }
    }

    public sealed class TagIndexField : Field
    {
        public override int Size => 4;
        public new TagId Value
        {
            get => (TagId)base.Value;
            set => base.Value = value;
        }
        public TagIndexField(string name) : base(FieldType.FieldTagIndex, name)
        {
            Value = TagId.Null;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = new TagId(reader.ReadUInt32());
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(((TagId)Value).Dword);
        }
    }
}
