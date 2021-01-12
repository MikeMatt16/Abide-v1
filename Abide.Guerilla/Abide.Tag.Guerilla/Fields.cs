using Abide.Tag.Definition;
using System;
using System.IO;
using System.Text;

namespace Abide.Tag.Guerilla
{
    public class NullTerminatedStringField : Field
    {
        public sealed override int Size => Encoding.UTF8.GetByteCount(Value) + 1;
        public new string Value
        {
            get => (string)FieldValue;
            set => FieldValue = value;
        }
        public NullTerminatedStringField(FieldType type, string name) : base(type, name) { }
        protected sealed override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadUTF8NullTerminated();
        }
        protected sealed override void OnWrite(BinaryWriter writer)
        {
            writer.WriteUTF8NullTerminated(Value?.ToString() ?? string.Empty);
        }
    }

    public sealed class StringIdField : NullTerminatedStringField
    {
        public StringIdField(string name) : base(FieldType.FieldStringId, name)
        {
            Value = string.Empty;
        }
    }

    public sealed class OldStringIdField : NullTerminatedStringField
    {
        public OldStringIdField(string name) : base(FieldType.FieldOldStringId, name)
        {
            Value = string.Empty;
        }
    }

    public sealed class TagReferenceField : NullTerminatedStringField
    {
        public string GroupTag { get; set; }
        public TagReferenceField(string name, string groupTag = "") : base(FieldType.FieldTagReference, name)
        {
            GroupTag = groupTag;
            Value = string.Empty;
        }
        public TagReferenceField(string name, int groupTag = 0) : base(FieldType.FieldTagReference, name)
        {
            GroupTag = Encoding.UTF8.GetString(BitConverter.GetBytes(groupTag)).Trim('\0');
            Value = string.Empty;
        }
    }

    public sealed class TagIndexField : NullTerminatedStringField
    {
        public TagIndexField(string name) : base(FieldType.FieldTagIndex, name)
        {
            Value = string.Empty;
        }
    }
}
