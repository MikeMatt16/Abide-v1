using Abide.HaloLibrary;
using Abide.Tag.Definition;
using System;
using System.IO;
using System.Text;
using HaloTag = Abide.HaloLibrary.TagFourCc;

namespace Abide.Tag.Guerilla
{
    /// <summary>
    /// Represents a tag block tag field.
    /// </summary>
    /// <typeparam name="T">The tag block type.</typeparam>
    public sealed class BlockField<T> : BaseBlockField where T : ITagBlock, new()
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
    /// Represents a simple string value object.
    /// </summary>
    public sealed class StringValue : IReadWrite, IEquatable<StringValue>
    {
        /// <summary>
        /// Gets and returns an empty <see cref="StringValue"/> object.
        /// </summary>
        public static readonly StringValue Empty = new StringValue() { String = string.Empty };

        /// <summary>
        /// Gets and returns the serialized length of the string value.
        /// </summary>
        public int SerializedLength
        {
            get { return 4 + Encoding.UTF8.GetByteCount(String); }
        }
        /// <summary>
        /// Gets and returns the string value.
        /// </summary>
        public string String { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="StringValue"/> class using the specified text string.
        /// </summary>
        /// <param name="value">The optional string value to initialize the class with. Defaults to <see cref="System.String.Empty"/>.</param>
        public StringValue(string value = "")
        {
            //Setup
            String = value;
        }
        /// <summary>
        /// Reads a string value using the specified binary reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void Read(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            String = Encoding.UTF8.GetString(reader.ReadBytes(length));
        }
        /// <summary>
        /// Writes a string value using the specified binary writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Write(BinaryWriter writer)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(String ?? string.Empty);
            writer.Write(buffer.Length);
            writer.Write(buffer);
        }
        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            //Return
            return String.ToString();
        }
        /// <summary>
        /// Determines whether this instance and another <see cref="StringValue"/> object have the same value.
        /// </summary>
        /// <param name="other">The <see cref="StringValue"/> to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="other"/> is equal to the value of this instance; otherwise <see langword="false"/>.</returns>
        public bool Equals(StringValue other)
        {
            return String.Equals(other.String);
        }

        /// <summary>
        /// Converts a <see cref="StringValue"/> object to a <see cref="string"/> object.
        /// </summary>
        /// <param name="stringValue">The <see cref="StringValue"/> object to convert to a <see cref="string"/>.</param>
        public static implicit operator string(StringValue stringValue)
        {
            return stringValue.String;
        }
        /// <summary>
        /// Converts a <see cref="string"/> object to a <see cref="StringValue"/> object.
        /// </summary>
        /// <param name="str">The <see cref="string"/> object to conver to a <see cref="string"/>.</param>
        public static implicit operator StringValue(string str)
        {
            return new StringValue() { String = str };
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
        public override int Size
        {
            get { return new StringValue((string)Value).SerializedLength; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="StringIdField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public StringIdField(string name) : base(FieldType.FieldStringId, name)
        {
            //Prepare
            Value = string.Empty;
        }
        /// <summary>
        /// Reads the value of the string ID from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read value
            StringValue value = new StringValue();
            value.Read(reader);

            //Set value
            Value = value.String;
        }
        /// <summary>
        /// Writes the value of the string ID to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            new StringValue((string)Value).Write(writer);
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
        public override int Size
        {
            get { return new StringValue((string)Value).SerializedLength; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OldStringIdField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public OldStringIdField(string name) : base(FieldType.FieldOldStringId, name)
        {
            //Prepare
            Value = string.Empty;
        }
        /// <summary>
        /// Reads the value of the string ID from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read value
            StringValue value = new StringValue();
            value.Read(reader);

            //Set value
            Value = value.String;
        }
        /// <summary>
        /// Writes the value of the string ID to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            new StringValue((string)Value).Write(writer);
        }
    }
    /// <summary>
    /// Represents a tag reference field.
    /// </summary>
    public sealed class TagReferenceField : Field
    {
        /// <summary>
        /// Gets and returns the reference group tag type.
        /// </summary>
        public string GroupTag { get; set; }
        /// <summary>
        /// Gets and returns the size of the tag reference field.
        /// </summary>
        public override int Size
        {
            get { return HaloTag.Size + new StringValue((string)Value).SerializedLength; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TagReferenceField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="groupTag">The group tag of the tag reference.</param>
        public TagReferenceField(string name, string groupTag = "") : base(FieldType.FieldTagReference, name)
        {
            //Prepare
            GroupTag = groupTag;
            Value = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TagReferenceField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="groupTag">The group tag of the tag group as a 32-bit signed integer.</param>
        public TagReferenceField(string name, int groupTag = 0) : base(FieldType.FieldTagReference, name)
        {
            GroupTag = Encoding.UTF8.GetString(BitConverter.GetBytes(groupTag)).Trim('\0');
            Value = string.Empty;
        }
        /// <summary>
        /// Reads the value of the tag reference from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read group tag
            GroupTag = reader.Read<HaloTag>();

            //Read value
            StringValue value = new StringValue();
            value.Read(reader);

            //Set value
            Value = value.String;
        }
        /// <summary>
        /// Writes the value of the tag reference to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write group tag
            writer.Write<HaloTag>(new HaloTag(GroupTag));

            //Write
            string stringValue = Value?.ToString() ?? string.Empty;
            new StringValue(stringValue).Write(writer);
        }
    }
    /// <summary>
    /// Represents a tag index tag field.
    /// </summary>
    public sealed class TagIndexField : Field
    {
        /// <summary>
        /// Gets and returns the size of the tag index field.
        /// </summary
        public override int Size
        {
            get { return new StringValue((string)Value).SerializedLength; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TagIndexField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public TagIndexField(string name) : base(FieldType.FieldTagIndex, name)
        {
            //Prepare
            Value = string.Empty;
        }
        /// <summary>
        /// Reads the value of the tag reference from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read value
            StringValue value = new StringValue();
            value.Read(reader);

            //Set value
            Value = value.String;
        }
        /// <summary>
        /// Writes the value of the tag reference to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            new StringValue((string)Value ?? string.Empty).Write(writer);
        }
    }
}
