using Abide.Tag;
using Abide.Tag.Definition;
using System.IO;
using System.Text;

namespace Abide.Wpf.Modules.Guerilla.Tag
{
    public abstract class BaseStringValueField : Field
    {
        public sealed override int Size => Encoding.UTF8.GetByteCount(Value) + 1;
        public new string Value
        {
            get => (string)FieldValue;
            set => FieldValue = value;
        }
        protected BaseStringValueField(FieldType type, string name) : base(type, name)
        {
            Value = string.Empty;
        }
        protected sealed override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadUTF8NullTerminated();
        }
        protected sealed override void OnWrite(BinaryWriter writer)
        {
            writer.WriteUTF8NullTerminated(Value);
        }
    }

    public sealed class StringIdField : BaseStringValueField
    {
        public StringIdField(string name) : base(FieldType.FieldStringId, name) { }
    }

    public sealed class OldStringIdField : BaseStringValueField
    {
        public OldStringIdField(string name) : base(FieldType.FieldOldStringId, name) { }
    }

    public sealed class TagReferenceField : BaseStringValueField
    {
        public TagReferenceField(string name) : base(FieldType.FieldTagReference, name) { }
    }

    public sealed class TagIndexField : BaseStringValueField
    {
        public TagIndexField(string name) : base(FieldType.FieldTagIndex, name) { }
    }
}
