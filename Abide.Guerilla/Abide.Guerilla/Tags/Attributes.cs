using Abide.HaloLibrary;
using System;

namespace Abide.Guerilla.Tags
{
    /// <summary>
    /// Represents a tag padding field attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class PaddingAttribute : Attribute
    {
        /// <summary>
        /// Gets and returns the size of the padding.
        /// </summary>
        public int Size
        {
            get { return size; }
        }

        private readonly int size;

        /// <summary>
        /// Initializes a new <see cref="PaddingAttribute"/> instance using the supplied padding size.
        /// </summary>
        /// <param name="size">The padding size.</param>
        public PaddingAttribute(int size)
        {
            this.size = size;
        }
    }

    /// <summary>
    /// Represents a tag field attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FieldAttribute : Attribute
    {
        /// <summary>
        /// Gets and returns the name of the field.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets and returns the optional field type.
        /// </summary>
        public Type Type
        {
            get { return type; }
        }

        private readonly string name;
        private readonly Type type;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldAttribute"/> class using the supplied name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="type">The optional type of the field.</param>
        public FieldAttribute(string name, Type type = null)
        {
            this.name = name;
            this.type = type;
        }
    }

    /// <summary>
    /// Represents an tag field options attribute.
    /// </summary>
    public sealed class OptionsAttribute : Attribute
    {
        /// <summary>
        /// Gets and returns the enum type for this field.
        /// </summary>
        public Type EnumType
        {
            get { return enumType; }
        }
        /// <summary>
        /// Gets and returns true if this field has flag options; otherwise returns false for enum options.
        /// </summary>
        public bool IsFlags
        {
            get { return isFlags; }
        }

        private readonly Type enumType;
        private readonly bool isFlags;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsAttribute"/> class using the supplied enum type, and flags options.
        /// </summary>
        /// <param name="enumType">The enumerator type of this field.</param>
        /// <param name="isFlags">Determines if the options are flags or simple enumeration.</param>
        public OptionsAttribute(Type enumType, bool isFlags)
        {
            this.enumType = enumType;
            this.isFlags = isFlags;
        }
    }

    /// <summary>
    /// Represents a tag field array.
    /// </summary>
    public sealed class ArrayAttribute : Attribute
    {
        /// <summary>
        /// Gets and returns the number of elements within the array.
        /// </summary>
        public int Length
        {
            get { return length; }
        }
        /// <summary>
        /// Gets and returns the array element type.
        /// </summary>
        public Type ElementType
        {
            get { return elementType; }
        }
        
        private readonly int length;
        private readonly Type elementType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayAttribute"/> structure using the specified array length and array element type.
        /// </summary>
        /// <param name="length">The length of the array.</param>
        /// <param name="elementType">The array element type.</param>
        public ArrayAttribute(int length, Type elementType)
        {
            this.length = length;
            this.elementType = elementType;
        }
    }

    /// <summary>
    /// Represents a tag group attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class TagGroupAttribute : Attribute
    {
        /// <summary>
        /// Gets and returns the name of this tag group.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets and returns the group of this tag group.
        /// </summary>
        public Tag GroupTag
        {
            get { return groupTag; }
        }
        /// <summary>
        /// Gets and returns the parent group of this tag group.
        /// </summary>
        public Tag ParentGroupTag
        {
            get { return parentGroupTag; }
        }
        /// <summary>
        /// Gets and returns the type of this tag group.
        /// </summary>
        public Type BlockType
        {
            get { return blockType; }
        }

        private readonly string name;
        private readonly Tag groupTag;
        private readonly Tag parentGroupTag;
        private readonly Type blockType;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagGroupAttribute"/> class using the supplied name, group tag, parent group tag, and block type.
        /// </summary>
        /// <param name="name">The name of the tag group.</param>
        /// <param name="groupTag">The group tag (or tag class) of the tag group.</param>
        /// <param name="parentGroupTag">The parent group tag (or parent tag class) of the tag group.</param>
        /// <param name="blockType">The block type of the tag group.</param>
        public TagGroupAttribute(string name, uint groupTag, uint parentGroupTag, Type blockType)
        {
            this.name = name;
            this.groupTag = (Tag)groupTag;
            this.parentGroupTag = (Tag)parentGroupTag;
            this.blockType = blockType;
        }
    }

    /// <summary>
    /// Represents a tag block attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class BlockAttribute : Attribute
    {
        /// <summary>
        /// Gets and returns the type of this block.
        /// </summary>
        public Type BlockType
        {
            get { return blockType; }
        }
        /// <summary>
        /// Gets and returns the display name of this block.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements in this block.
        /// </summary>
        public int MaximumElementCount
        {
            get { return maximumElementCount; }
        }

        private readonly string name;
        private readonly int maximumElementCount;
        private readonly Type blockType;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockAttribute"/> class using the supplied display name, tag block type, and maximum element count value.
        /// </summary>
        /// <param name="name">The display name of the tag block.</param>
        /// <param name="maximumElementCount">The maximum number of elements in the tag block.</param>
        /// <param name="blockType">The type of the tag block.</param>
        public BlockAttribute(string name, int maximumElementCount, Type blockType)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.maximumElementCount = maximumElementCount;
            this.blockType = blockType ?? throw new ArgumentNullException(nameof(blockType));
        }
    }

    /// <summary>
    /// Represents a tag data block attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DataAttribute : Attribute
    {
        /// <summary>
        /// Gets and returns the maximum number of data elements.
        /// </summary>
        public int MaximumElementCount
        {
            get { return maximumElementCount; }
        }

        private readonly int maximumElementCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAttribute"/> class using the supplied maximum element count value.
        /// </summary>
        /// <param name="maximumElementCount">The maximum number of data elements.</param>
        public DataAttribute(int maximumElementCount)
        {
            this.maximumElementCount = maximumElementCount;
        }
    }

    /// <summary>
    /// Represents a tag structure attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class StructureAttribute : Attribute
    {
        /// <summary>
        /// Gets and returns the type of this structure.
        /// </summary>
        public Type StructType
        {
            get { return structType; }
        }
        /// <summary>
        /// Gets and returns the display name of this structure.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        private readonly string name;
        private readonly Type structType;

        /// <summary>
        /// Initializes a new <see cref="StructureAttribute"/> using the supplied name.
        /// </summary>
        /// <param name="name">The structure name.</param>
        /// <param name="structType">The type of the structure.</param>
        public StructureAttribute(string name, Type structType)
        {
            this.name = name;
            this.structType = structType;
        }
    }

    /// <summary>
    /// Represents a tag field set attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class FieldSetAttribute : Attribute
    {
        /// <summary>
        /// Gets and returns the size of the field set.
        /// </summary>
        public int Size
        {
            get { return size; }
        }
        /// <summary>
        /// Gets and returns the alignment of the field set.
        /// </summary>
        public int Alignment
        {
            get { return alignment; }
        }

        private readonly int size;
        private readonly int alignment;

        /// <summary>
        /// Initializes a new <see cref="FieldSetAttribute"/> instance using the supplied size and alignment values.
        /// </summary>
        /// <param name="size">The size of the field set.</param>
        /// <param name="alignment">The alignment of the field set.</param>
        public FieldSetAttribute(int size, int alignment)
        {
            this.size = size;
            this.alignment = alignment;
        }
    }
}
