using Abide.HaloLibrary;
using Abide.Tag.Definition;
using System;
using System.IO;
using System.Text;

namespace Abide.Tag.Guerilla
{
    public class NullTerminatedStringField : Field
    {
        public sealed override int Size => Encoding.UTF8.GetByteCount(String) + 1;
        public string String
        {
            get => (string)Value;
            set
            {
                if (Value is string str)
                {
                    if (str == value)
                    {
                        return;
                    }
                }

                Value = value;
            }
        }

        public NullTerminatedStringField(FieldType type, string name) : base(type, name)
        {
            String = string.Empty;
        }
        protected sealed override void OnRead(BinaryReader reader)
        {
            String = reader.ReadUTF8NullTerminated();
        }
        protected sealed override void OnWrite(BinaryWriter writer)
        {
            writer.WriteUTF8NullTerminated(String);
        }
    }

    public sealed class StringIdField : NullTerminatedStringField
    {
        public StringIdField(string name) : base(FieldType.FieldStringId, name) { }
    }

    public sealed class OldStringIdField : NullTerminatedStringField
    {
        public OldStringIdField(string name) : base(FieldType.FieldOldStringId, name) { }
    }

    public sealed class TagReferenceField : NullTerminatedStringField
    {
        public TagFourCc GroupTag { get; set; }
        public TagReferenceField(string name, int groupTag = 0) : base(FieldType.FieldTagReference, name)
        {
            GroupTag = new TagFourCc(groupTag);
            Value = string.Empty;
        }
        public TagReferenceField(string name, string groupTag) : base(FieldType.FieldTagReference, name)
        {
            GroupTag = groupTag;
        }
    }

    public sealed class TagIndexField : NullTerminatedStringField
    {
        public TagIndexField(string name) : base(FieldType.FieldTagIndex, name) { }
    }
}
