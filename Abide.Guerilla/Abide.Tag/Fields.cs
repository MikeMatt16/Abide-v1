using Abide.HaloLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using GroupTag = Abide.HaloLibrary.Tag;

namespace Abide.Tag
{
    /// <summary>
    /// Represents a method that contains a block search procedure.
    /// </summary>
    /// <param name="tagBlock">The tag block containing the block indexer.</param>
    /// <param name="blockIndex">The index of the tag block.</param>
    /// <returns>A <see cref="Block"/> class instance or <see langword="null"/>.</returns>
    public delegate Block BlockSearchProcedure<T>(Block tagBlock, int blockIndex) where T : IConvertible, IComparable, IComparable<T>, IEquatable<T>;
    /// <summary>
    /// Represents an option.
    /// </summary>
    /// <typeparam name="T">The indexer type.</typeparam>
    public sealed class Option<T> where T : IConvertible, IComparable, IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Gets and returns the name of the option.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Gets and returns the index of the option.
        /// </summary>
        public T Index { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Option{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <param name="index">The index of the option.</param>
        public Option(string name, T index)
        {
            Name = name;
            Index = index;
        }
    }
    /// <summary>
    /// Represents a base tag field.
    /// </summary>
    public abstract class Field : IReadWrite
    {
        /// <summary>
        /// Gets and returns the name of the field.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Gets and returns the size of the field.
        /// </summary>
        public abstract int Size
        {
            get;
        }
        /// <summary>
        /// Represents the field's value.
        /// </summary>
        public object Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public Field(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = null;
        }
        /// <summary>
        /// Returns a string representation of this tag field.
        /// </summary>
        /// <returns>A </returns>
        public override string ToString()
        {
            return $"{Name} = {Value}";
        }
        /// <summary>
        /// Reads the value of the field from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public virtual void Read(BinaryReader reader) { }
        /// <summary>
        /// Writes the value of the field to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public virtual void Write(BinaryWriter writer) { }
    }
    /// <summary>
    /// Represents an explanation field.
    /// </summary>
    public sealed class ExplanationField : Field
    {
        /// <summary>
        /// Gets and returns the size of the explanation field.
        /// </summary>
        public override int Size => 0;
        /// <summary>
        /// Gets and returns the explanation.
        /// </summary>
        public string Explanation { get; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExplanationField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="explanation">The explanation.</param>
        public ExplanationField(string name, string explanation) : base(name)
        {
            //Set explanation
            Explanation = explanation;
        }
        /// <summary>
        /// Returns the name of the explanation field.
        /// </summary>
        /// <returns>The name.</returns>
        public override string ToString()
        {
            return $"{Name}";
        }
    }
    /// <summary>
    /// Represents a string field.
    /// </summary>
    public sealed class StringField : Field
    {
        /// <summary>
        /// Gets and returns the size of the string field.
        /// </summary>
        public override int Size => 32;
        /// <summary>
        /// Initializes a new instance of the <see cref="StringField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public StringField(string name) : base(name)
        {
            //Prepare
            Value = new String32();
        }
        /// <summary>
        /// Reads the value of the string from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<String32>();
        }
        /// <summary>
        /// Writes the value of the string to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((String32)Value);
        }
    }
    /// <summary>
    /// Represents a long string field.
    /// </summary>
    public sealed class LongStringField : Field
    {
        /// <summary>
        /// Gets and returns the size of the string field.
        /// </summary>
        public override int Size => 256;
        /// <summary>
        /// Initializes a new instance of the <see cref="LongStringField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public LongStringField(string name) : base(name)
        {
            //Prepare
            Value = new String256();
        }
        /// <summary>
        /// Reads the value of the string from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<String256>();
        }
        /// <summary>
        /// Writes the value of the string to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((String256)Value);
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
        public StringIdField(string name) : base(name)
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
        public OldStringIdField(string name) : base(name)
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
    /// Represents a char integer field.
    /// </summary>
    public sealed class CharIntegerField : Field
    {
        /// <summary>
        /// Gets and returns the size of the integer field.
        /// </summary>
        public override int Size => 1;
        /// <summary>
        /// Initializes a new instance of the <see cref="CharIntegerField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public CharIntegerField(string name) : base(name)
        {
            //Prepare
            Value = (byte)0;
        }
        /// <summary>
        /// Reads the value of the integer from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadByte();
        }
        /// <summary>
        /// Writes the value of the integer to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((byte)Value);
        }
    }
    /// <summary>
    /// Represents a short integer field.
    /// </summary>
    public sealed class ShortIntegerField : Field
    {
        /// <summary>
        /// Gets and returns the size of the integer field.
        /// </summary>
        public override int Size => 2;
        /// <summary>
        /// Initializes a new instance of the <see cref="ShortIntegerField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public ShortIntegerField(string name) : base(name)
        {
            //Prepare
            Value = (byte)0;
        }
        /// <summary>
        /// Reads the value of the integer from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadInt16();
        }
        /// <summary>
        /// Writes the value of the integer to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((short)Value);
        }
    }
    /// <summary>
    /// Represents a long integer field.
    /// </summary>
    public sealed class LongIntegerField : Field
    {
        /// <summary>
        /// Gets and returns the size of the integer field.
        /// </summary>
        public override int Size => 4;
        /// <summary>
        /// Initializes a new instance of the <see cref="LongIntegerField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public LongIntegerField(string name) : base(name)
        {
            //Prepare
            Value = 0;
        }
        /// <summary>
        /// Reads the value of the integer from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadInt32();
        }
        /// <summary>
        /// Writes the value of the integer to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((int)Value);
        }
    }
    /// <summary>
    /// Represents an angle field.
    /// </summary>
    public sealed class AngleField : Field
    {
        /// <summary>
        /// Gets and returns the size of the angle field.
        /// </summary>
        public override int Size => 4;
        /// <summary>
        /// Intializes a new instance of the <see cref="AngleField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public AngleField(string name) : base(name)
        {
            //Prepare
            Value = 0f;
        }
        /// <summary>
        /// Reads the value of the angle from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadSingle();
        }
        /// <summary>
        /// Writes the value of the angle to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((float)Value);
        }
    }
    /// <summary>
    /// Represents a tag field.
    /// </summary>
    public sealed class TagField : Field
    {
        /// <summary>
        /// Gets and returns the size of the tag field.
        /// </summary>
        public override int Size => 4;
        /// <summary>
        /// Initializes a new instance of the <see cref="TagField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public TagField(string name) : base(name)
        {
            //Prepare
            Value = new GroupTag(string.Empty);
        }
        /// <summary>
        /// Reads the value of the tag from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<GroupTag>();
        }
        /// <summary>
        /// Writes the value of the tag to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write<GroupTag>((GroupTag)Value);
        }
    }
    /// <summary>
    /// Represents a char enum tag field.
    /// </summary>
    public sealed class CharEnumField : Field
    {
        /// <summary>
        /// Gets and returns the size of the enum field.
        /// </summary>
        public override int Size => 1;
        /// <summary>
        /// Gets or sets the current option.
        /// </summary>
        public Option<byte> Option
        {
            get { return Options[(byte)Value]; }
            set { if (Options.Contains(value)) Value = value.Index; }
        }
        /// <summary>
        /// Gets and returns a list of options for the enum.
        /// </summary>
        public List<Option<byte>> Options { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharEnumField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="options">The enum options.</param>
        public CharEnumField(string name, params string[] options) : base(name)
        {
            //Prepare
            Options = new List<Option<byte>>();
            Value = (byte)0;

            //Add options
            for (byte i = 0; i < options.Length; i++)
                Options.Add(new Option<byte>(options[i], i));
        }
        /// <summary>
        /// Reads the value of the enum from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadByte();
        }
        /// <summary>
        /// Writes the value of the enum to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((byte)Value);
        }
        /// <summary>
        /// Returns a string representation of this enum.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{base.ToString()} ({Options[(byte)Value]?.Name ?? "IndexOutOfRange"})";
        }
    }
    /// <summary>
    /// Represents an enum tag field.
    /// </summary>
    public sealed class EnumField : Field
    {
        /// <summary>
        /// Gets and returns the size of the enum field.
        /// </summary>
        public override int Size => 2;
        /// <summary>
        /// Gets or sets the current option.
        /// </summary>
        public Option<short> Option
        {
            get { return Options[(short)Value]; }
            set { if (Options.Contains(value)) Value = value.Index; }
        }
        /// <summary>
        /// Gets and returns a list of options for the enum.
        /// </summary>
        public List<Option<short>> Options { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="options">The enum options.</param>
        public EnumField(string name, params string[] options) : base(name)
        {
            //Prepare
            Options = new List<Option<short>>();
            Value = (short)0;

            //Add options
            for (short i = 0; i < options.Length; i++)
                Options.Add(new Option<short>(options[i], i));
        }
        /// <summary>
        /// Reads the value of the enum from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadInt16();
        }
        /// <summary>
        /// Writes the value of the enum to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((short)Value);
        }
        /// <summary>
        /// Returns a string representation of this enum.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{base.ToString()} ({Options[(short)Value]?.Name ?? "IndexOutOfRange"})";
        }
    }
    /// <summary>
    /// Represents a long enum tag field.
    /// </summary>
    public sealed class LongEnumField : Field
    {
        /// <summary>
        /// Gets and returns the size of the enum field.
        /// </summary>
        public override int Size => 4;
        /// <summary>
        /// Gets or sets the current option.
        /// </summary>
        public Option<int> Option
        {
            get { return Options[(int)Value]; }
            set { if (Options.Contains(value)) Value = value.Index; }
        }
        /// <summary>
        /// Gets and returns a list of options for the enum.
        /// </summary>
        public List<Option<int>> Options { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LongEnumField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="options">The enum options.</param>
        public LongEnumField(string name, params string[] options) : base(name)
        {
            //Prepare
            Options = new List<Option<int>>();
            Value = 0;

            //Add options
            for (int i = 0; i < options.Length; i++)
                Options.Add(new Option<int>(options[i], i));
        }
        /// <summary>
        /// Reads the value of the enum from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadInt32();
        }
        /// <summary>
        /// Writes the value of the enum to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((int)Value);
        }
        /// <summary>
        /// Returns a string representation of this enum.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{base.ToString()} ({Options[(int)Value]?.Name ?? "IndexOutOfRange"})";
        }
    }
    /// <summary>
    /// Represents a long flags tag field.
    /// </summary>
    public sealed class LongFlagsField : Field
    {
        /// <summary>
        /// Gets and returns the size of the flags field.
        /// </summary>
        public override int Size => 4;
        /// <summary>
        /// Gets and returns a list of options for the enum.
        /// </summary>
        public List<Option<int>> Options { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LongFlagsField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="options">The flags options</param>
        public LongFlagsField(string name, params string[] options) : base(name)
        {
            //Prepare
            Options = new List<Option<int>>();
            Value = 0;

            //Add options
            for (int i = 0; i < options.Length; i++)
                Options.Add(new Option<int>(options[i], i));
        }
        /// <summary>
        /// Reads the value of the flags from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadInt32();
        }
        /// <summary>
        /// Writes the value of the flags to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((int)Value);
        }
        /// <summary>
        /// Returns a string representation of the flags.
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            List<string> flagged = new List<string>();
            foreach (Option<int> option in Options)
                if ((((int)Value) & (1 << (option.Index + 1))) == (1 << (option.Index + 1)))
                    flagged.Add(option.Name);

            return $"{Name} = [{string.Join(",", flagged.ToArray())}]";
        }
        /// <summary>
        /// Returns a value that determines whether or not the supplied option is toggled for the flags.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <returns><see langword="true"/> if the option is on; otherwise, <see langword="false"/>.</returns>
        public bool HasFlag(Option<int> option)
        {
            int flag = (1 << (option.Index + 1));
            return (((int)Value) & flag) == flag;
        }
        /// <summary>
        /// Sets a flag on or off.
        /// </summary>
        /// <param name="option">The option to set.</param>
        /// <param name="toggle">on or off.</param>
        /// <returns>The new flags value.</returns>
        public int SetFlag(Option<int> option, bool toggle)
        {
            //Toggle off first, then back on if needed
            int flags = (int)Value;
            int flag = 1 << (option.Index + 1);
            flags = flags & (~flag);
            if (toggle) flags |= flag;

            //Set
            Value = flags;
            return flags;
        }
    }
    /// <summary>
    /// Represents a word flags tag field.
    /// </summary>
    public sealed class WordFlagsField : Field
    {
        /// <summary>
        /// Gets and returns the size of the flags field.
        /// </summary>
        public override int Size => 2;
        /// <summary>
        /// Gets and returns a list of options for the enum.
        /// </summary>
        public List<Option<short>> Options { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WordFlagsField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="options">The flags options</param>
        public WordFlagsField(string name, params string[] options) : base(name)
        {
            //Prepare
            Options = new List<Option<short>>();
            Value = 0;

            //Add options
            for (short i = 0; i < options.Length; i++)
                Options.Add(new Option<short>(options[i], i));
        }
        /// <summary>
        /// Reads the value of the flags from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadInt16();
        }
        /// <summary>
        /// Writes the value of the flags to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((short)Value);
        }
        /// <summary>
        /// Returns a string representation of the flags.
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            List<string> flagged = new List<string>();
            foreach (Option<short> option in Options)
                if ((((short)Value) & (1 << (option.Index + 1))) == (1 << (option.Index + 1)))
                    flagged.Add(option.Name);

            return $"{Name} = [{string.Join(",", flagged.ToArray())}]";
        }
        /// <summary>
        /// Returns a value that determines whether or not the supplied option is toggled for the flags.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <returns><see langword="true"/> if the option is on; otherwise, <see langword="false"/>.</returns>
        public bool HasFlag(Option<int> option)
        {
            int flag = (1 << (option.Index + 1));
            return (((short)Value) & flag) == flag;
        }
        /// <summary>
        /// Sets a flag on or off.
        /// </summary>
        /// <param name="option">The option to set.</param>
        /// <param name="toggle">on or off.</param>
        /// <returns>The new flags value.</returns>
        public short SetFlag(Option<short> option, bool toggle)
        {
            //Toggle off first, then back on if needed
            int flags = (short)Value;
            int flag = 1 << (option.Index + 1);
            flags = flags & (~flag);
            if (toggle) flags |= flag;

            //Set
            Value = (short)flags;
            return (short)flags;
        }
    }
    /// <summary>
    /// Represents a byte flags tag field.
    /// </summary>
    public sealed class ByteFlagsField : Field
    {
        /// <summary>
        /// Gets and returns the size of the flags field.
        /// </summary>
        public override int Size => 1;
        /// <summary>
        /// Gets and returns a list of options for the enum.
        /// </summary>
        public List<Option<byte>> Options { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteFlagsField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="options">The flags options</param>
        public ByteFlagsField(string name, params string[] options) : base(name)
        {
            //Prepare
            Options = new List<Option<byte>>();
            Value = 0;

            //Add options
            for (byte i = 0; i < options.Length; i++)
                Options.Add(new Option<byte>(options[i], i));
        }
        /// <summary>
        /// Reads the value of the flags from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadByte();
        }
        /// <summary>
        /// Writes the value of the flags to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((byte)Value);
        }
        /// <summary>
        /// Returns a string representation of the flags.
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            List<string> flagged = new List<string>();
            foreach (Option<byte> option in Options)
                if ((((byte)Value) & (1 << (option.Index + 1))) == (1 << (option.Index + 1)))
                    flagged.Add(option.Name);

            return $"{Name} = [{string.Join(",", flagged.ToArray())}]";
        }
        /// <summary>
        /// Returns a value that determines whether or not the supplied option is toggled for the flags.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <returns><see langword="true"/> if the option is on; otherwise, <see langword="false"/>.</returns>
        public bool HasFlag(Option<byte> option)
        {
            int flag = (1 << (option.Index + 1));
            return (((short)Value) & flag) == flag;
        }
        /// <summary>
        /// Sets a flag on or off.
        /// </summary>
        /// <param name="option">The option to set.</param>
        /// <param name="toggle">on or off.</param>
        /// <returns>The new flags value.</returns>
        public short SetFlag(Option<byte> option, bool toggle)
        {
            //Toggle off first, then back on if needed
            int flags = (short)Value;
            int flag = 1 << (option.Index + 1);
            flags = flags & (~flag);
            if (toggle) flags |= flag;

            //Set
            Value = (byte)flags;
            return (byte)flags;
        }
    }
    /// <summary>
    /// Represents a 2D point tag field.
    /// </summary>
    public sealed class Point2dField : Field
    {
        /// <summary>
        /// Gets and returns the size of the point field.
        /// </summary>
        public override int Size => 4;
        /// <summary>
        /// Initializes a new instance of the <see cref="Point2dField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public Point2dField(string name) : base(name)
        {
            //Prepare
            Value = Point2.Zero;
        }
        /// <summary>
        /// Reads the value of the point from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<Point2>();
        }
        /// <summary>
        /// Writes the value of the point to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((Point2)Value);
        }
    }
    /// <summary>
    /// Represents a 2D rectangle tag field.
    /// </summary>
    public sealed class Rectangle2dField : Field
    {
        /// <summary>
        /// Gets and returns the size of the rectangle field.
        /// </summary>
        public override int Size => 8;
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2dField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public Rectangle2dField(string name) : base(name)
        {
            //Prepare
            Value = Rectangle2.Empty;
        }
        /// <summary>
        /// Reads the value of the rectangle from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<Rectangle2>();
        }
        /// <summary>
        /// Writes the value of the rectangle to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((Rectangle2)Value);
        }
    }
    /// <summary>
    /// Represents a RGB color tag field.
    /// </summary>
    public sealed class RgbColorField : Field
    {
        /// <summary>
        /// Gets and returns the size of the color field.
        /// </summary>
        public override int Size => 3;
        /// <summary>
        /// Initializes a new instance of the <see cref="RgbColorField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RgbColorField(string name) : base(name)
        {
            //Prepare
            Value = ColorRgb.Zero;
        }
        /// <summary>
        /// Reads the value of the color from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<ColorRgb>();
        }
        /// <summary>
        /// Writes the value of the color to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((ColorRgb)Value);
        }
    }
    /// <summary>
    /// Represents an ARGB color tag field.
    /// </summary>
    public sealed class ArgbColorField : Field
    {
        /// <summary>
        /// Gets and returns the size of the color field.
        /// </summary>
        public override int Size => 4;
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgbColorField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public ArgbColorField(string name) : base(name)
        {
            //Prepare
            Value = ColorArgb.Zero;
        }
        /// <summary>
        /// Reads the value of the color from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<ColorArgb>();
        }
        /// <summary>
        /// Writes the value of the color to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((ColorArgb)Value);
        }
    }
    /// <summary>
    /// Represents a real tag field.
    /// </summary>
    public sealed class RealField : Field
    {
        /// <summary>
        /// Gets and returns the size of the real field.
        /// </summary>
        public override int Size => 4;
        /// <summary>
        /// Intializes a new instance of the <see cref="RealField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RealField(string name) : base(name)
        {
            Value = 0f;
        }
        /// <summary>
        /// Reads the value of the real from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadSingle();
        }
        /// <summary>
        /// Writes the value of the real to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((float)Value);
        }
    }
    /// <summary>
    /// Represents a real fraction tag field.
    /// </summary>
    public sealed class RealFractionField : Field
    {
        /// <summary>
        /// Gets and returns the size of the real field.
        /// </summary>
        public override int Size => 4;
        /// <summary>
        /// Intializes a new instance of the <see cref="RealFractionField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RealFractionField(string name) : base(name)
        {
            Value = 0f;
        }
        /// <summary>
        /// Reads the value of the real from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadSingle();
        }
        /// <summary>
        /// Writes the value of the real to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((float)Value);
        }
    }
    /// <summary>
    /// Represents a real 2D point tag field.
    /// </summary>
    public sealed class RealPoint2dField : Field
    {
        /// <summary>
        /// Gets and returns the size of the point field.
        /// </summary>
        public override int Size => 8;
        /// <summary>
        /// Initializes a new instance of the <see cref="RealPoint2dField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RealPoint2dField(string name) : base(name)
        {
            //Setup
            Value = Point2F.Zero;
        }
        /// <summary>
        /// Reads the value of the real point from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<Point2F>();
        }
        /// <summary>
        /// Writes the value of the real point to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((Point2F)Value);
        }
    }
    /// <summary>
    /// Represents a real 3D point tag field.
    /// </summary>
    public sealed class RealPoint3dField : Field
    {
        /// <summary>
        /// Gets and returns the size of the point field.
        /// </summary>
        public override int Size => 12;
        /// <summary>
        /// Initializes a new instance of the <see cref="RealPoint3dField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RealPoint3dField(string name) : base(name)
        {
            //Setup
            Value = Point3F.Zero;
        }
        /// <summary>
        /// Reads the value of the real point from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<Point3F>();
        }
        /// <summary>
        /// Writes the value of the real point to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((Point3F)Value);
        }
    }
    /// <summary>
    /// Represents a real 2D vector tag field.
    /// </summary>
    public sealed class RealVector2dField : Field
    {
        /// <summary>
        /// Gets and returns the size of the vector field.
        /// </summary>
        public override int Size => 8;
        /// <summary>
        /// Initializes a new instance of the <see cref="RealVector2dField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RealVector2dField(string name) : base(name)
        {
            //Prepare
            Value = Vector2.Zero;
        }
        /// <summary>
        /// Reads the value of the vector from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<Vector2>();
        }
        /// <summary>
        /// Writes the value of the vector to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((Vector2)Value);
        }
    }
    /// <summary>
    /// Represents a real 3D vector tag field.
    /// </summary>
    public sealed class RealVector3dField : Field
    {
        /// <summary>
        /// Gets and returns the size of the vector field.
        /// </summary>
        public override int Size => 12;
        /// <summary>
        /// Initializes a new instance of the <see cref="RealVector3dField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RealVector3dField(string name) : base(name)
        {
            //Prepare
            Value = Vector3.Zero;
        }
        /// <summary>
        /// Reads the value of the vector from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<Vector3>();
        }
        /// <summary>
        /// Writes the value of the vector to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((Vector3)Value);
        }
    }
    /// <summary>
    /// Represents a real quaternion tag field.
    /// </summary>
    public sealed class QuaternionField : Field
    {
        /// <summary>
        /// Gets and returns the size of the quaternion field.
        /// </summary>
        public override int Size => 16;
        /// <summary>
        /// Initializes a new instance of the <see cref="QuaternionField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public QuaternionField(string name) : base(name)
        {
            //Prepare
            Value = Quaternion.Zero;
        }
        /// <summary>
        /// Reads the value of the quaternion from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<Quaternion>();
        }
        /// <summary>
        /// Writes the value of the quaternion to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((Quaternion)Value);
        }
    }
    /// <summary>
    /// Represents a 2D Euler angles tag field.
    /// </summary>
    public sealed class EulerAngles2dField : Field
    {
        /// <summary>
        /// Gets and returns the size of the angles field.
        /// </summary>
        public override int Size => 8;
        /// <summary>
        /// Initializes a new instance of the <see cref="EulerAngles2dField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public EulerAngles2dField(string name) : base(name)
        {
            //Prepare
            Value = Vector2.Zero;
        }
        /// <summary>
        /// Reads the value of the Euler angles from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<Vector2>();
        }
        /// <summary>
        /// Writes the value of the Euler angles to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((Vector2)Value);
        }
    }
    /// <summary>
    /// Represents a 3D Euler angles tag field.
    /// </summary>
    public sealed class EulerAngles3dField : Field
    {
        /// <summary>
        /// Gets and returns the size of the angles field.
        /// </summary>
        public override int Size => 12;
        /// <summary>
        /// Initializes a new instance of the <see cref="EulerAngles3dField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public EulerAngles3dField(string name) : base(name)
        {
            //Prepare
            Value = Vector3.Zero;
        }
        /// <summary>
        /// Reads the value of the Euler angles from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<Vector3>();
        }
        /// <summary>
        /// Writes the value of the Euler angles to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((Vector3)Value);
        }
    }
    /// <summary>
    /// Represents a real 2D plane tag field.
    /// </summary>
    public sealed class RealPlane2dField : Field
    {
        /// <summary>
        /// Gets and returns the size of the plane field.
        /// </summary>
        public override int Size => 12;
        /// <summary>
        /// Initializes a new instance of the <see cref="RealPlane2dField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RealPlane2dField(string name) : base(name)
        {
            //Prepare
            Value = Vector3.Zero;
        }
        /// <summary>
        /// Reads the value of the real plane from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<Vector3>();
        }
        /// <summary>
        /// Writes the value of the real plane to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((Vector3)Value);
        }
    }
    /// <summary>
    /// Represents a real 3D plane tag field.
    /// </summary>
    public sealed class RealPlane3dField : Field
    {
        /// <summary>
        /// Gets and returns the size of the plane field.
        /// </summary>
        public override int Size => 16;
        /// <summary>
        /// Initializes a new instance of the <see cref="RealPlane3dField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RealPlane3dField(string name) : base(name)
        {
            //Prepare
            Value = Vector4.Zero;
        }
        /// <summary>
        /// Reads the value of the real plane from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<Vector4>();
        }
        /// <summary>
        /// Writes the value of the real plane to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((Vector4)Value);
        }
    }
    /// <summary>
    /// Represents a real RGB color tag field.
    /// </summary>
    public sealed class RealRgbColorField : Field
    {
        /// <summary>
        /// Gets and returns the size of the color field.
        /// </summary>
        public override int Size => 12;
        /// <summary>
        /// Initializes a new instance of the <see cref="RealRgbColorField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RealRgbColorField(string name) : base(name)
        {
            //Prepare
            Value = ColorRgbF.Zero;
        }
        /// <summary>
        /// Reads the value of the real color from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<ColorRgbF>();
        }
        /// <summary>
        /// Writes the value of the real color to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((ColorRgbF)Value);
        }
    }
    /// <summary>
    /// Represents a real ARGB color tag field.
    /// </summary>
    public sealed class RealArgbColorField : Field
    {
        /// <summary>
        /// Gets and returns the size of the color field.
        /// </summary>
        public override int Size => 16;
        /// <summary>
        /// Initializes a new instance of the <see cref="RealArgbColorField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RealArgbColorField(string name) : base(name)
        {
            //Prepare
            Value = ColorArgbF.Zero;
        }
        /// <summary>
        /// Reads the value of the real color from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<ColorArgbF>();
        }
        /// <summary>
        /// Writes the value of the real color to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((ColorArgbF)Value);
        }
    }
    /// <summary>
    /// Represents a real HSV color tag field.
    /// </summary>
    public sealed class RealHsvColorField : Field
    {
        /// <summary>
        /// Gets and returns the size of the color field.
        /// </summary>
        public override int Size => 12;
        /// <summary>
        /// Initializes a new instance of the <see cref="RealHsvColorField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RealHsvColorField(string name) : base(name)
        {
            //Prepare
            Value = ColorHsv.Zero;
        }
        /// <summary>
        /// Reads the value of the real color from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<ColorHsv>();
        }
        /// <summary>
        /// Writes the value of the real color to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((ColorHsv)Value);
        }
    }
    /// <summary>
    /// Represents a real AHSV color tag field.
    /// </summary>
    public sealed class RealAhsvColorField : Field
    {
        /// <summary>
        /// Gets and returns the size of the color field.
        /// </summary>
        public override int Size => 16;
        /// <summary>
        /// Initializes a new instance of the <see cref="RealAhsvColorField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RealAhsvColorField(string name) : base(name)
        {
            //Prepare
            Value = ColorAhsv.Zero;
        }
        /// <summary>
        /// Reads the value of the real color from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<ColorAhsv>();
        }
        /// <summary>
        /// Writes the value of the real color to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((ColorAhsv)Value);
        }
    }
    /// <summary>
    /// Represents a short bounds tag field.
    /// </summary>
    public sealed class ShortBoundsField : Field
    {
        /// <summary>
        /// Gets and returns the size of the bounds field.
        /// </summary>
        public override int Size => 4;
        /// <summary>
        /// Initializes a new instance of the <see cref="ShortBoundsField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public ShortBoundsField(string name) : base(name)
        {
            //Prepare
            Value = ShortBounds.Zero;
        }
        /// <summary>
        /// Reads the value of the bounds from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<ShortBounds>();
        }
        /// <summary>
        /// Writes the value of the bounds to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((ShortBounds)Value);
        }
    }
    /// <summary>
    /// Represents an angle bounds tag field.
    /// </summary>
    public sealed class AngleBoundsField : Field
    {
        /// <summary>
        /// Gets and returns the size of the bounds field.
        /// </summary>
        public override int Size => 8;
        /// <summary>
        /// Initializes a new instance of the <see cref="AngleBoundsField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public AngleBoundsField(string name) : base(name)
        {
            //Prepare
            Value = FloatBounds.Zero;
        }
        /// <summary>
        /// Reads the value of the bounds from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<FloatBounds>();
        }
        /// <summary>
        /// Writes the value of the bounds to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((FloatBounds)Value);
        }
    }
    /// <summary>
    /// Represents a real bounds tag field.
    /// </summary>
    public sealed class RealBoundsField : Field
    {
        /// <summary>
        /// Gets and returns the size of the bounds field.
        /// </summary>
        public override int Size => 8;
        /// <summary>
        /// Initializes a new instance of the <see cref="RealBoundsField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RealBoundsField(string name) : base(name)
        {
            //Prepare
            Value = FloatBounds.Zero;
        }
        /// <summary>
        /// Reads the value of the bounds from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<FloatBounds>();
        }
        /// <summary>
        /// Writes the value of the bounds to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((FloatBounds)Value);
        }
    }
    /// <summary>
    /// Represents a real fraction bounds tag field.
    /// </summary>
    public sealed class RealFractionBoundsField : Field
    {
        /// <summary>
        /// Gets and returns the size of the bounds field.
        /// </summary>
        public override int Size => 8;
        /// <summary>
        /// Initializes a new instance of the <see cref="RealFractionBoundsField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public RealFractionBoundsField(string name) : base(name)
        {
            //Prepare
            Value = FloatBounds.Zero;
        }
        /// <summary>
        /// Reads the value of the bounds from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<FloatBounds>();
        }
        /// <summary>
        /// Writes the value of the bounds to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((FloatBounds)Value);
        }
    }
    /// <summary>
    /// Represents a tag reference field.
    /// </summary>
    public sealed class TagReferenceField : Field
    {
        /// <summary>
        /// Gets and returns the size of the tag reference field.
        /// </summary>
        public override int Size => 8;
        /// <summary>
        /// Initializes a new instance of the <see cref="TagReferenceField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public TagReferenceField(string name) : base(name)
        {
            //Prepare
            Value = TagReference.Null;
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
    /// Represents a block tag field.
    /// </summary>
    /// <typeparam name="T">The tag block type.</typeparam>
    public sealed class BlockField<T> : Field where T : ITagBlock, new()
    {
        /// <summary>
        /// Gets and returns the list of blocks.
        /// </summary>
        public BlockList<T> BlockList
        {
            get;
        }
        
        private long writeAddress = -1;

        /// <summary>
        /// Gets and returns the size of the block field.
        /// </summary>
        public override int Size => 8;
        /// <summary>
        /// Initializes a new instance of the <see cref="BlockField{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="maximumElementCount">The maximum number of blocks allowed.</param>
        public BlockField(string name, int maximumElementCount) : base(name)
        {
            //Prepare
            Value = TagBlock.Zero;
            BlockList = new BlockList<T>(maximumElementCount);
        }
        /// <summary>
        /// Reads the value of the block from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Clear
            BlockList.Clear();

            //Read
            TagBlock block = reader.Read<TagBlock>();
            Value = block;

            //Check
            if (block.Count > 0)
            {
                //Store position
                long position = reader.BaseStream.Position;

                //Goto
                reader.BaseStream.Seek(block.Offset, SeekOrigin.Begin);

                //Loop
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
            //Get Address
            writeAddress = writer.BaseStream.Position;

            //Write zero
            writer.Write(TagBlock.Zero);
        }
        /// <summary>
        /// Writes the child blocks of the block field to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public void WriteChildren(BinaryWriter writer)
        {
            //Prepare
            TagBlock tagBlock = TagBlock.Zero;

            //Get Address
            if(BlockList.Count > 0)
            {
                //Pad?
                int remainder = (int)(writer.BaseStream.Position % BlockList[0].Alignment);
                for (int i = 0; i < remainder; i++)
                    writer.Write((byte)0xcd);

                //Setup
                tagBlock.Count = (uint)BlockList.Count;
                tagBlock.Offset = (uint)writer.BaseStream.Position;

                //Write
                foreach (T block in BlockList)
                    block.Write(writer);
            }

            //Get End Point
            long position = writer.BaseStream.Position;

            //Write Tag Block
            writer.BaseStream.Position = writeAddress;
            writer.Write(tagBlock);

            //Goto
            writer.BaseStream.Position = position;
        }
    }
    /// <summary>
    /// Represents a char block index tag field.
    /// </summary>
    public sealed class CharBlockIndexField : Field
    {
        /// <summary>
        /// Gets and returns the size of the block index field.
        /// </summary>
        public override int Size => 1;
        /// <summary>
        /// Gets or sets the search procedure used to find the block that this field is indexing.
        /// </summary>
        public BlockSearchProcedure<byte> SearchProcedure { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharBlockIndexField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public CharBlockIndexField(string name) : base(name)
        {
            //Prepare
            Value = (byte)0;
        }
        /// <summary>
        /// Reads the value of the char block index from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadByte();
        }
        /// <summary>
        /// Writes the value of the char block index to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((byte)Value);
        }
    }
    /// <summary>
    /// Represents a short block index tag field.
    /// </summary>
    public sealed class ShortBlockIndexField : Field
    {
        /// <summary>
        /// Gets and returns the size of the block index field.
        /// </summary>
        public override int Size => 2;
        /// <summary>
        /// Gets or sets the search procedure used to find the block that this field is indexing.
        /// </summary>
        public BlockSearchProcedure<short> SearchProcedure { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortBlockIndexField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public ShortBlockIndexField(string name) : base(name)
        {
            //Prepare
            Value = (short)0;
        }
        /// <summary>
        /// Reads the value of the char block index from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadInt16();
        }
        /// <summary>
        /// Writes the value of the char block index to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((short)Value);
        }
    }
    /// <summary>
    /// Represents a long block index tag field.
    /// </summary>
    public sealed class LongBlockIndexField : Field
    {
        /// <summary>
        /// Gets and returns the size of the block index field.
        /// </summary>
        public override int Size => 4;
        /// <summary>
        /// Gets or sets the search procedure used to find the block that this field is indexing.
        /// </summary>
        public BlockSearchProcedure<int> SearchProcedure { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LongBlockIndexField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public LongBlockIndexField(string name) : base(name)
        {
            //Prepare
            Value = 0;
        }
        /// <summary>
        /// Reads the value of the char block index from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadInt32();
        }
        /// <summary>
        /// Writes the value of the char block index to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((int)Value);
        }
    }
    /// <summary>
    /// Represents a data tag field.
    /// </summary>
    public sealed class DataField : Field
    {
        /// <summary>
        /// Gets and returns the size of the data field.
        /// </summary>
        public override int Size => 8;
        /// <summary>
        /// Initializes a new instance of the <see cref="DataField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public DataField(string name) : base(name)
        {
            //Prepare
            Value = TagBlock.Zero;
        }
        /// <summary>
        /// Reads the value of the data from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<TagBlock>();
        }
        /// <summary>
        /// Writes the value of the data to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((TagBlock)Value);
        }
    }
    /// <summary>
    /// Represents a vertex buffer tag field.
    /// </summary>
    public sealed class VertexBufferField : Field
    {
        /// <summary>
        /// Gets and returns the size of the vertex buffer field.
        /// </summary>
        public override int Size => 32;
        /// <summary>
        /// Initializes a new instance of the <see cref="VertexBufferField"/>.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public VertexBufferField(string name) : base(name)
        {
            //Prepare
            Value = new VertexBuffer();
        }
        /// <summary>
        /// Reads the value of the vertex buffer from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.Read<VertexBuffer>();
        }
        /// <summary>
        /// Writes the value of the vertex buffer to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((VertexBuffer)Value);
        }
    }
    /// <summary>
    /// Represents a pad tag field.
    /// </summary>
    public sealed class PadField : Field
    {
        /// <summary>
        /// Gets and returns the size of the pad field.
        /// </summary>
        public override int Size => Length;
        /// <summary>
        /// Gets and returns the length of the padding.
        /// </summary>
        public int Length { get; }

        private const byte padByte = 0xcd;

        /// <summary>
        /// Initializes a new instance of the <see cref="PadField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="length">The length of the padding.</param>
        public PadField(string name, int length) : base(name)
        {
            //Setup
            Length = length;
        }
        /// <summary>
        /// Returns a string representation of this pad field.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"Padding ({Length})";
        }
        /// <summary>
        /// Reads the value of the vertex buffer from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            reader.ReadBytes(Length);
        }
        /// <summary>
        /// Writes the value of the vertex buffer to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            for (int i = 0; i < Length; i++)
                writer.Write(padByte);
        }
    }
    /// <summary>
    /// Represents a skip tag field.
    /// </summary>
    public sealed class SkipField : Field
    {
        /// <summary>
        /// Gets and returns the size of the pad field.
        /// </summary>
        public override int Size => Length;
        /// <summary>
        /// Gets and returns the length of the padding.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SkipField"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="length">The length of the skip.</param>
        public SkipField(string name, int length) : base(name)
        {
            //Setup
            Length = length;
            Value = new byte[length];
        }
        /// <summary>
        /// Returns a string representation of this skip field.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"Skip ({Length})";
        }
        /// <summary>
        /// Reads the value of the skip from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Value = reader.ReadBytes(Length);
        }
        /// <summary>
        /// Writes the value of the skip to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Write
            writer.Write((byte[])Value);
        }
    }
    /// <summary>
    /// Represents a struct tag field.
    /// </summary>
    /// <typeparam name="T">The tag block type.</typeparam>
    public sealed class StructField<T> : Field where T : ITagBlock, new()
    {
        /// <summary>
        /// Gets and returns the size of the struct field.
        /// </summary>
        public override int Size => ((T)Value).Size;

        /// <summary>
        /// Initializes a new instance of the <see cref="StructField{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public StructField(string name) : base(name)
        {
            //Prepare
            Value = new T();
            ((T)Value).Initialize();
        }
        /// <summary>
        /// Reads the value of the struct from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void Read(BinaryReader reader)
        {
            //Pad?
            int remainder = (int)(reader.BaseStream.Position % ((T)Value).Alignment);
            if (remainder > 0) reader.BaseStream.Seek(((T)Value).Alignment - remainder, SeekOrigin.Current);

            //Read
            ((T)Value).Read(reader);
        }
        /// <summary>
        /// Writes the value of the struct to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer.</param>
        public override void Write(BinaryWriter writer)
        {
            //Pad?
            int remainder = (int)(writer.BaseStream.Position % ((T)Value).Alignment);
            if (remainder > 0) writer.BaseStream.Seek(((T)Value).Alignment - remainder, SeekOrigin.Current);

            //Write
            ((T)Value).Write(writer);
        }
    }
}
