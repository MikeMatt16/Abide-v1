using Abide.HaloLibrary;
using Abide.Tag.Definition;
using System;
using System.IO;
using System.Text;

namespace Abide.Tag.Cache
{
    /// <summary>
    /// Represents a tag block tag field.
    /// </summary>
    /// <typeparam name="T">The tag block type.</typeparam>
    public sealed class BlockField<T> : BlockField where T : ITagBlock, new()
    {
        internal static int identIndex = 0;
        /// <summary>
        /// Gets and returns the size of the block field.
        /// </summary>
        public override int Size => 8;
        /// <summary>
        /// Initializes a new instance of the <see cref="BlockField{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="maximumElementCount">The maximum number of blocks allowed.</param>
        public BlockField(string name, int maximumElementCount) : base(name, maximumElementCount)
        {
            //Prepare
            Value = TagBlock.Zero;
        }
        /// <summary>
        /// Reads the value of the block from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            base.Read(reader);

            //Read
            TagBlock block = ((TagBlock)Value);

            //Check
            if (block.Count > 0)
            {
                //Store position
                long position = reader.BaseStream.Position;

                //Loop
                reader.BaseStream.Seek(block.Offset, SeekOrigin.Begin);
                for (int i = 0; i < block.Count; i++)
                {
                    //Initialize
                    T tagBlock = new T();
                    tagBlock.Initialize();

                    //Read
                    tagBlock.Read(reader);

                    //Add
                    BlockList.Add(tagBlock);
                }

                //Goto
                reader.BaseStream.Position = position;
            }
        }
        /// <summary>
        /// Writes the value of the block to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Zero-out
            Value = TagBlock.Zero;

            //Write
            base.Write(writer);
        }
        /// <summary>
        /// Attemtps to add a new tag block of type <typeparamref name="T"/> to the block list.
        /// </summary>
        /// <param name="success">If successful, the value of <paramref name="success"/> will be <see langword="true"/>; otherwise, the value of <paramref name="success"/> will be <see langword="false"/>.</param>
        /// <returns>A new <see cref="ITagBlock"/> instance of type <typeparamref name="T"/>.</returns>
        public override ITagBlock Add(out bool success)
        {
            //Create block
            T tagBlock = new T();
            tagBlock.Initialize();

            //Attempt to add
            BlockList.Add(tagBlock, out success);

            //Return
            return tagBlock;
        }
        /// <summary>
        /// Returns a new <see cref="ITagBlock"/> of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A new instance of the <typeparamref name="T"/> class.</returns>
        public override ITagBlock Create()
        {
            //Create
            T tagBlock = new T();
            tagBlock.Initialize();

            //Return
            return tagBlock;
        }
    }
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
        /// <param name="groupTag">The group tag of the tag group as a string.</param>
        public TagReferenceField(string name, string groupTag = "") : base(FieldType.FieldTagReference, name)
        {
            //Prepare
            GroupTag = groupTag;
            Value = new TagReference() { Tag = groupTag, Id = TagId.Null };
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TagReferenceField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="groupTag">The group tag of the tag group as a 32-bit signed integer.</param>
        public TagReferenceField(string name, int groupTag = 0) : base(FieldType.FieldTagReference, name)
        {
            GroupTag = Encoding.UTF8.GetString(BitConverter.GetBytes(groupTag)).Trim('\0');
            Value = new TagReference() { Tag = (TagFourCc)(uint)groupTag, Id = TagId.Null };
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
            writer.Write<TagReference>(Value);
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
