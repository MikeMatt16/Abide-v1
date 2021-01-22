using Abide.HaloLibrary;
using System.IO;

namespace Abide.Tag.Cache
{
    public class BaseStringIdField : Field
    {
        public sealed override int Size => 4;
        public StringId Id
        {
            get => (StringId)Value;
            set
            {
                if (Value is StringId id)
                {
                    if (id.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public BaseStringIdField(FieldType type, string name) : base(type, name)
        {
            Id = StringId.Zero;
        }
        protected sealed override void OnRead(BinaryReader reader)
        {
            Id = reader.Read<StringId>();
        }
        protected sealed override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Id);
        }
    }

    public sealed class StringIdField : BaseStringIdField
    {
        public StringIdField(string name) : base(FieldType.FieldStringId, name) { }
    }

    public sealed class OldStringIdField : BaseStringIdField
    {
        public OldStringIdField(string name) : base(FieldType.FieldOldStringId, name) { }
    }

    public sealed class TagReferenceField : Field
    {
        public TagReference Null => new TagReference() { Id = TagId.Null, Tag = GroupTag };
        public override int Size => 8;
        public TagFourCc GroupTag { get; }
        public TagReference Reference
        {
            get => (TagReference)Value;
            set
            {
                if (Value is TagReference reference)
                {
                    if (reference.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public TagReferenceField(string name, int groupTag = 0) : base(FieldType.FieldTagReference, name)
        {
            GroupTag = new TagFourCc(groupTag);
            Value = new TagReference() { Tag = (TagFourCc)(uint)groupTag, Id = TagId.Null };
        }
        public TagReferenceField(string name, string groupTag) : base(FieldType.FieldTagReference, name)
        {
            GroupTag = groupTag;
            Value = new TagReference() { Tag = groupTag, Id = TagId.Null };
        }
        protected override void OnRead(BinaryReader reader)
        {
            Reference = reader.Read<TagReference>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Reference);
        }
    }

    public sealed class TagIndexField : Field
    {
        public override int Size => 4;
        public TagId Id
        {
            get => (TagId)Value;
            set
            {
                if (Value is TagId reference)
                {
                    if (reference.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public TagIndexField(string name) : base(FieldType.FieldTagIndex, name)
        {
            Value = TagId.Null;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Id = reader.ReadTagId();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Id);
        }
    }
}
