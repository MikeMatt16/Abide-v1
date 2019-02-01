using Abide.HaloLibrary;
using Abide.Tag.Definition;
using System.IO;

namespace Abide.Tag.Cache
{
    /// <summary>
    /// Represents a tag reference field.
    /// </summary>
    public sealed class TagReferenceField : Field
    {
        /// <summary>
        /// Gets and returns a null tag reference for this field.
        /// </summary>
        public TagReference Null
        {
            get { return new TagReference() { Id = TagId.Null, Tag = GroupTag }; }
        }
        /// <summary>
        /// Gets and returns the reference group tag type.
        /// </summary>
        public string GroupTag { get; }
        /// <summary>
        /// Gets and returns the size of the tag reference field.
        /// </summary>
        public override int Size => 8;
        /// <summary>
        /// Initializes a new instance of the <see cref="TagReferenceField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="groupTag">The group tag of the tag group.</param>
        public TagReferenceField(string name, string groupTag = "") : base(FieldType.FieldTagReference, name)
        {
            //Prepare
            GroupTag = groupTag;
            Value = Null;
        }
        /// <summary>
        /// Reads the value of the tag reference from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<TagReference>();
        }
        /// <summary>
        /// Writes the value of the tag reference to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((TagReference)Value);
        }
    }
    /// <summary>
    /// Represents a string id field.
    /// </summary>
    public sealed class StringIdField : Field
    {
        /// <summary>
        /// Gets and returns the size of the string field.
        /// </summary>
        public override int Size => 4;
        /// <summary>
        /// Initializes a new instance of the <see cref="StringIdField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public StringIdField(string name) : base(FieldType.FieldStringId, name)
        {
            //Prepare
            Value = StringId.Zero;
        }
        /// <summary>
        /// Reads the value of the string ID from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<StringId>();
        }
        /// <summary>
        /// Writes the value of the string ID to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((StringId)Value);
        }
    }
    /// <summary>
    /// Represents an old string id field.
    /// </summary>
    public sealed class OldStringIdField : Field
    {
        /// <summary>
        /// Gets and returns the size of the string field.
        /// </summary>
        public override int Size => 4;  //?
        /// <summary>
        /// Initializes a new instance of the <see cref="OldStringIdField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public OldStringIdField(string name) : base(FieldType.FieldOldStringId, name)
        {
            //Prepare
            Value = StringId.Zero;
        }
        /// <summary>
        /// Reads the value of the string ID from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<StringId>();
        }
        /// <summary>
        /// Writes the value of the string ID to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((StringId)Value);
        }
    }
    /// <summary>
    /// Represents a tag index tag field.
    /// </summary>
    public sealed class TagIndexField : Field
    {
        /// <summary>
        /// Gets and returns the size of the tag index field.
        /// </summary>
        public override int Size => 4;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagIndexField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public TagIndexField(string name) : base(FieldType.FieldTagIndex, name)
        {
            Value = TagId.Null;
        }
        /// <summary>
        /// Reads the value of the tag index from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader used to read the tag index value.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = new TagId(reader.ReadUInt32());
        }
        /// <summary>
        /// Writes the value of the tag index to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer used to write the tag index value.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write(((TagId)Value).Dword);
        }
    }
}
