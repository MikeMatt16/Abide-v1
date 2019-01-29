using Abide.Tag.Definition;
using System.IO;
using System.Text;

namespace Abide.Tag.Guerilla
{
    /// <summary>
    /// Represents a simple string value object.
    /// </summary>
    public sealed class StringValue : IReadWrite
    {
        /// <summary>
        /// Gets and returns an empty <see cref="StringValue"/> object.
        /// </summary>
        public static readonly StringValue Empty = new StringValue() { Value = string.Empty };

        /// <summary>
        /// Gets and returns the serialized length of the string value.
        /// </summary>
        public int SerializedLength
        {
            get { return 4 + Encoding.UTF8.GetByteCount(Value); }
        }
        /// <summary>
        /// Gets and returns the string value.
        /// </summary>
        public string Value { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="StringValue"/> class using the specified text string.
        /// </summary>
        /// <param name="value">The optional string value to initialize the class with. Defaults to <see cref="System.String.Empty"/>.</param>
        public StringValue(string value = "")
        {
            //Setup
            Value = value;
        }
        /// <summary>
        /// Reads a string value using the specified binary reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void Read(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            Value = Encoding.UTF8.GetString(reader.ReadBytes(length));
        }
        /// <summary>
        /// Writes a string value using the specified binary writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Write(BinaryWriter writer)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(Value ?? string.Empty);
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
            return Value.ToString();
        }

        /// <summary>
        /// Converts a <see cref="StringValue"/> object to a <see cref="string"/> object.
        /// </summary>
        /// <param name="stringValue">The <see cref="StringValue"/> object to convert to a <see cref="string"/>.</param>
        public static implicit operator string(StringValue stringValue)
        {
            return stringValue.Value;
        }
        /// <summary>
        /// Converts a <see cref="string"/> object to a <see cref="StringValue"/> object.
        /// </summary>
        /// <param name="str">The <see cref="string"/> object to conver to a <see cref="string"/>.</param>
        public static implicit operator StringValue(string str)
        {
            return new StringValue() { Value = str };
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
            get { return ((StringValue)Value).SerializedLength; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="StringIdField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public StringIdField(string name) : base(FieldType.FieldStringId, name)
        {
            //Prepare
            Value = StringValue.Empty;
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
            Value = value;
        }
        /// <summary>
        /// Writes the value of the string ID to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            ((StringValue)Value).Write(writer);
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
            get { return ((StringValue)Value).SerializedLength; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OldStringIdField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public OldStringIdField(string name) : base(FieldType.FieldOldStringId, name)
        {
            //Prepare
            Value = StringValue.Empty;
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
            Value = value;
        }
        /// <summary>
        /// Writes the value of the string ID to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            ((StringValue)Value).Write(writer);
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
            get { return ((StringValue)Value).SerializedLength; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TagReferenceField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public TagReferenceField(string name, string groupTag = "") : base(FieldType.FieldTagReference, name)
        {
            //Prepare
            Value = StringValue.Empty;
            GroupTag = groupTag;
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
            Value = value;
        }
        /// <summary>
        /// Writes the value of the tag reference to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            ((StringValue)Value).Write(writer);
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
            get { return ((StringValue)Value).SerializedLength; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TagIndexField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public TagIndexField(string name) : base(FieldType.FieldTagIndex, name)
        {
            //Prepare
            Value = StringValue.Empty;
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
            Value = value;
        }
        /// <summary>
        /// Writes the value of the tag reference to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            ((StringValue)Value).Write(writer);
        }
    }
}
