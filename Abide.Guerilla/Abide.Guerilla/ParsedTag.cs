using Abide.Guerilla.Tags;
using Abide.HaloLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Collections;

namespace Abide.Guerilla
{
    public sealed class ParsedTag
    {
        private readonly Tag tagGroup;

        public ParsedTag(Tag tagGroup)
        {
            this.tagGroup = tagGroup;
        }
    }
    
    /// <summary>
    /// Represents a parsed tag block.
    /// </summary>
    public sealed class ParsedBlock : IEnumerable<ParsedField>
    {
        /// <summary>
        /// Gets and returns a parsed field at a given index.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>A parsed field.</returns>
        public ParsedField this[int index]
        {
            get
            {
                if (index >= 0 && index < fields.Count) return fields[index];
                else throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
        /// <summary>
        /// Gets and returns the block size.
        /// </summary>
        public int Size
        {
            get { return size; }
        }
        /// <summary>
        /// Gets and returns the block alignment.
        /// </summary>
        public int Alignment
        {
            get { return alignment; }
        }
        
        private readonly List<ParsedField> fields = new List<ParsedField>();
        private readonly int alignment;
        private readonly int size;

        /// <summary>
        /// Initializes a new parsed block using the specified field set type.
        /// </summary>
        /// <param name="fieldSetType">The field set type.</param>
        public ParsedBlock(Type fieldSetType)
        {
            //Prepare
            FieldSetAttribute fieldSetAttribute = fieldSetType.GetCustomAttribute<FieldSetAttribute>();
            
            //Check
            if (fieldSetAttribute != null)
            {
                //Initialize
                alignment = fieldSetAttribute.Alignment;
                size = fieldSetAttribute.Size;

                //Get Fields
                foreach (FieldInfo field in fieldSetType.GetFields())
                    fields.Add(CreateField(field));
            }
            else throw new ArgumentException($"Type must have {nameof(FieldSetAttribute)}.", nameof(fieldSetType));
        }
        /// <summary>
        /// Reads the fields (and sub-fields) of the block using the supplied reader.
        /// </summary>
        /// <param name="reader">The reader used to read the block.</param>
        public void Read(BinaryReader reader)
        {
            //Loop through each field and read
            foreach (ParsedField field in fields)
                field.Read(reader);
        }
        /// <summary>
        /// Creates a parsed field from the supplied field info.
        /// </summary>
        /// <param name="fieldInfo">The field information.</param>
        /// <returns>A parsed field.</returns>
        private static ParsedField CreateField(FieldInfo fieldInfo)
        {
            //Prepare
            ParsedField field = null;

            //Check
            if (fieldInfo.GetCustomAttribute<FieldAttribute>() == null) throw new ArgumentException("Invalid field.", nameof(fieldInfo));
            else if (fieldInfo.GetCustomAttribute<PaddingAttribute>() != null) field = new ParsedPaddingField(fieldInfo);
            else if (fieldInfo.GetCustomAttribute<DataAttribute>() != null) field = new ParsedDataField(fieldInfo);
            else if (fieldInfo.GetCustomAttribute<BlockAttribute>() != null) field = new ParsedBlockField(fieldInfo);
            else field = new ParsedField(fieldInfo);

            //Initialize
            field.Initialize();

            //Return
            return field;
        }
        /// <summary>
        /// Gets an enumerator that iterates the instance.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<ParsedField> GetEnumerator()
        {
            return fields.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return fields.GetEnumerator();
        }
    }

    /// <summary>
    /// Represents a parsed tag block field.
    /// </summary>
    public sealed class ParsedBlockField : ParsedField
    {
        /// <summary>
        /// Gets a value that determines if the block field is an in-line structure, or a tag block.
        /// </summary>
        public bool IsStruct
        {
            get { return !(Instance is TagBlock); }
        }
        /// <summary>
        /// Gets and returns the block type.
        /// </summary>
        public Type BlockType
        {
            get { return blockType; }
        }
        /// <summary>
        /// Gets and returns the block's maximum element count.
        /// </summary>
        public int MaximumElementCount
        {
            get { return maximumElementCount; }
        }
        /// <summary>
        /// Gets and returns the block's name.
        /// </summary>
        public string BlockName
        {
            get { return blockName; }
        }

        private readonly List<ParsedBlock> blockList;
        private readonly Type blockType;
        private readonly int maximumElementCount;
        private readonly string blockName;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ParsedBlockField"/> class.
        /// </summary>
        /// <param name="field">The field information to create a parsed block field.</param>
        public ParsedBlockField(FieldInfo field) : base(field)
        {
            //Prepare
            BlockAttribute blockAttribute = field.GetCustomAttribute<BlockAttribute>();

            //Check
            if (blockAttribute != null)
            {
                //Setup
                blockType = blockAttribute.BlockType;
                maximumElementCount = blockAttribute.MaximumElementCount;
                blockName = blockAttribute.Name;
            }
            else throw new ArgumentException($"Type must have {nameof(BlockAttribute)}.", nameof(field));

            //Initialize
            blockList = new List<ParsedBlock>(maximumElementCount);
        }
        /// <summary>
        /// Initializes the parsed tag block field.
        /// </summary>
        public override void Initialize()
        {
            Instance = TagBlock.Zero;
            blockList.Clear();
        }
        /// <summary>
        /// Reads the block using the supplied binary reader.
        /// </summary>
        /// <param name="reader">The binary reader used to read the value.</param>
        public override void Read(BinaryReader reader)
        {
            //Read TagBlock
            TagBlock block = reader.Read<TagBlock>();
            blockList.Clear();

            //Check
            if (block.Count > 0 && block.Count <= maximumElementCount && block.Offset > 0)
            {
                //Store
                long address = reader.BaseStream.Position;

                //Read
                reader.BaseStream.Seek(block.Offset, SeekOrigin.Begin);
                for (int i = 0; i < block.Count; i++)
                {
                    //Read Block
                    ParsedBlock blockInstance = new ParsedBlock(blockType);
                    blockInstance.Read(reader);

                    //Add
                    blockList.Add(blockInstance);
                }

                //Goto
                reader.BaseStream.Seek(address, SeekOrigin.Begin);
            }

            //Set
            Instance = block;
        }
        /// <summary>
        /// Gets a string representation of this parsed block field.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            if (Instance is TagBlock) return $"Block {Name} Count: {((TagBlock)Instance).Count}";
            else return $"Block {Name}";
        }
    }

    /// <summary>
    /// Represents a parsed tag padding field.
    /// </summary>
    public sealed class ParsedPaddingField : ParsedField
    {
        /// <summary>
        /// Gets and returns the size of the padding field.
        /// </summary>
        public int Size
        {
            get { return size; }
        }

        private readonly int size;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ParsedPaddingField"/> class using the specified field information.
        /// </summary>
        /// <param name="field">The field information to create a parsed padding field.</param>
        public ParsedPaddingField(FieldInfo field) : base(field)
        {
            //Prepare
            PaddingAttribute paddingAttribute = field.GetCustomAttribute<PaddingAttribute>();

            //Check
            if (paddingAttribute != null)
            {
                //Setup
                size = paddingAttribute.Size;
            }
            else throw new ArgumentException($"Type must have {nameof(PaddingAttribute)}.", nameof(field));
        }
        /// <summary>
        /// Initializes the padding field.
        /// </summary>
        public override void Initialize()
        {
            Instance = new byte[size];
        }
        /// <summary>
        /// Reads the value of this field using the supplied binary reader.
        /// </summary>
        /// <param name="reader">The binary reader used to read the value.</param>
        public override void Read(BinaryReader reader)
        {
            //Read
            Instance = reader.ReadBytes(size);
        }
        /// <summary>
        /// Gets a string representation of this parsed padding field.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"Padding {Name} Size: {size}";
        }
    }

    /// <summary>
    /// Represents a parsed tag data field.
    /// </summary>
    public sealed class ParsedDataField : ParsedField
    {
        /// <summary>
        /// Gets and returns the data buffer.
        /// </summary>
        public byte[] Buffer
        {
            get { return buffer.ToArray(); }
        }
        /// <summary>
        /// Gets and returns the data block's maximum element count.
        /// </summary>
        public int MaximumElementCount
        {
            get { return maximumElementCount; }
        }

        private readonly int maximumElementCount;
        private readonly List<byte> buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsedDataField"/> class using the supplied field information.
        /// </summary>
        /// <param name="field">The field information to create a parsed data field.</param>
        public ParsedDataField(FieldInfo field) : base(field)
        {
            //Prepare
            DataAttribute dataAttribute = field.GetCustomAttribute<DataAttribute>();

            //Check
            if (dataAttribute != null)
            {
                //Setup
                maximumElementCount = dataAttribute.MaximumElementCount;
            }
            else throw new ArgumentException($"Type must have {nameof(DataAttribute)}.", nameof(field));

            //?Initialize
            buffer = new List<byte>();
        }
        /// <summary>
        /// Initializes the data field.
        /// </summary>
        public override void Initialize()
        {
            Instance = TagBlock.Zero;
            buffer.Clear();
        }
        /// <summary>
        /// Reads the data structure field and populates the buffer.
        /// </summary>
        /// <param name="reader">The binary reader used to read the value.</param>
        public override void Read(BinaryReader reader)
        {
            //Read TagBlock
            TagBlock block = reader.Read<TagBlock>();
            buffer.Clear();

            //Check
            if (block.Count > 0 && block.Count < maximumElementCount && block.Offset > 0)
            {
                //Store
                long address = reader.BaseStream.Position;

                //Read
                reader.BaseStream.Seek(block.Offset, SeekOrigin.Begin);
                buffer.AddRange(reader.ReadBytes((int)block.Count));

                //Goto
                reader.BaseStream.Seek(address, SeekOrigin.Begin);
            }

            //Set
            Instance = block;
        }
        /// <summary>
        /// Gets a string representation of this parsed data field.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Data {Name} Size: {((TagBlock)Instance).Count}";
        }
    }

    /// <summary>
    /// Represents a parsed tag field.
    /// </summary>
    public class ParsedField
    {
        /// <summary>
        /// Gets or sets the field instance value.
        /// </summary>
        public object Instance
        {
            get { return instance; }
            set { instance = value; }
        }
        /// <summary>
        /// Gets and returns the name of this field.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        private readonly string name;
        private readonly Type fieldType;
        private readonly Type definedType;
        private object instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsedField"/> class using the specified field information.
        /// </summary>
        /// <param name="field">The field information to create a parsed field.</param>
        public ParsedField(FieldInfo field)
        {
            //Prepare
            FieldAttribute fieldAttribute = field.GetCustomAttribute<FieldAttribute>();

            //Check
            if (fieldAttribute != null)
            {
                //Setup
                name = fieldAttribute.Name;
                fieldType = field.FieldType;
                definedType = fieldAttribute.Type;
            }
            else throw new ArgumentException($"Type must have {nameof(FieldAttribute)}.", nameof(field));
        }
        /// <summary>
        /// Initializes the field.
        /// </summary>
        public virtual void Initialize()
        {
            //Set instance
            instance = Activator.CreateInstance(fieldType);
        }
        /// <summary>
        /// Reads the value of this field using the supplied binary reader.
        /// </summary>
        /// <param name="reader">The binary reader used to read the value.</param>
        public virtual void Read(BinaryReader reader)
        {
            //Read
            instance = reader.Read(fieldType);
        }
        /// <summary>
        /// Gets a string representation of this parsed field.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{fieldType.Name} {GetNameString()} Value: {instance}";
        }

        private string GetNameString()
        {
            //Get name
            string value = name;

            //Check
            if (value.Contains("*")) value = value.Substring(0, value.IndexOf('*'));    //Split label
            if (value.Contains("#")) value = value.Substring(0, value.IndexOf('#'));    //Tooltip
            if (value.Contains("^")) value = value.Substring(0, value.IndexOf('^'));    //Tag Block Name
            if (value.Contains(":")) value = value.Substring(0, value.IndexOf(':'));    //Just... why?

            //Return
            return value;
        }
    }
}
