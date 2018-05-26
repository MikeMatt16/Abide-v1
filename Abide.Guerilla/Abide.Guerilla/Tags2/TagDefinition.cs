using System.Collections;
using System.Collections.Generic;

namespace Abide.Guerilla.Tags2
{
    /// <summary>
    /// Represents a Tags2 Blam! tag definition.
    /// </summary>
    public abstract class TagBlock
    {
        /// <summary>
        /// Gets and returns the name of the tag block.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets and returns the display name of the tag block.
        /// </summary>
        public string DisplayName
        {
            get { return displayName; }
        }
        /// <summary>
        /// Gets and returns the maximum number of blocks in an array.
        /// </summary>
        public int MaximumElementCount
        {
            get { return maximumElementCount; }
        }
        /// <summary>
        /// Gets and returns the tag block's field set.
        /// </summary>
        public FieldSet FieldSet
        {
            get { return fieldSet; }
        }

        private readonly string name, displayName;
        private readonly int maximumElementCount;
        private readonly FieldSet fieldSet;

        /// <summary>
        /// Initializes a new <see cref="TagBlock"/> class using the specified name, display name, and field set.
        /// </summary>
        /// <param name="name">The name of the tag block.</param>
        /// <param name="displayName">The display name of the tag block.</param>
        /// <param name="maximumElementCount">The maximum number of blocks allowed in an array.</param>
        /// <param name="fieldSet">The field set of the tag block.</param>
        public TagBlock(string name, string displayName, int maximumElementCount, FieldSet fieldSet)
        {
            this.name = name;
            this.displayName = displayName;
            this.maximumElementCount = maximumElementCount;
            this.fieldSet = fieldSet;
        }
    }

    /// <summary>
    /// Represents a Tags2 Blam! tag field set.
    /// </summary>
    public class FieldSet
    {
        /// <summary>
        /// Gets and returns the alignment of the field set.
        /// </summary>
        public int Alignment
        {
            get { return alignment; }
        }

        private readonly int alignment;
        private readonly FieldList fieldList;

        /// <summary>
        /// Intializes a new instance of the <see cref="FieldSet"/> class using the specified alignment, and field list.
        /// </summary>
        /// <param name="alignment">The alignment of the field set.</param>
        /// <param name="fieldList">The field list.</param>
        public FieldSet(int alignment, FieldList fieldList)
        {
            this.alignment = alignment;
            this.fieldList = fieldList;
        }
        /// <summary>
        /// Gets and returns an array of fields within the field set.
        /// </summary>
        /// <returns>An array of <see cref="Field"/> elements.</returns>
        public Field[] GetFields()
        {
            return fieldList.ToArray();
        }
    }

    /// <summary>
    /// Represents a Tags2 Blam! field.
    /// </summary>
    public abstract class Field
    {
        /// <summary>
        /// Gets and returns the name of the field.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets or sets the value of the field.
        /// </summary>
        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        private readonly string name;
        private object value = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class using the specified field name.
        /// </summary>
        /// <param name="name">The field name.</param>
        public Field(string name)
        {
            this.name = name;
        }
    }

    /// <summary>
    /// Represents a Tags2 Blam! value field.
    /// </summary>
    public sealed class ValueField : Field
    {
        /// <summary>
        /// Gets and returns the field type.
        /// </summary>
        public FieldType Type
        {
            get { return type; }
        }

        private readonly FieldType type;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueField"/> class using the specefied field type and name.
        /// </summary>
        /// <param name="name">The field name.</param>
        /// <param name="type">The field type.</param>
        public ValueField(string name, FieldType type) : base(name)
        {
            this.type = type;
        }
    }

    /// <summary>
    /// Represents a Tags2 Blam! explanation field.
    /// </summary>
    public sealed class ExplanationField : Field
    {
        /// <summary>
        /// Gets and returns the explanation.
        /// </summary>
        public string Explanation
        {
            get { return explanation; }
        }

        private readonly string explanation;

        /// <summary>
        /// Initializes a new instance of the Field class using the specified name and explanation.
        /// </summary>
        /// <param name="name">The field name.</param>
        /// <param name="explanation">The explanation.</param>
        public ExplanationField(string name, string explanation) : base(name)
        {
            this.explanation = explanation;
        }
    }
    
    /// <summary>
    /// Represents a Tags2 Blam! enum field.
    /// </summary>
    public sealed class EnumField : Field
    {
        private readonly EnumType type;
        private readonly string[] options;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumField"/> class using the specefied name and options.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="type">The type of the enum.</param>
        /// <param name="options">The options.</param>
        public EnumField(string name, EnumType type, params string[] options) : base(name)
        {
            this.type = type;
            this.options = options;
        }
    }

    /// <summary>
    /// Represents a list of field objects.
    /// </summary>
    public sealed class FieldList : IList<Field>, IEnumerable<Field>
    {
        /// <summary>
        /// Gets or sets the field within the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>A <see cref="Field"/> object.</returns>
        public Field this[int index]
        {
            get => fields[index];
            set => fields[index] = value;
        }
        /// <summary>
        /// Gets and returns the number of fields within the list.
        /// </summary>
        public int Count
        {
            get { return fields.Count; }
        }

        bool ICollection<Field>.IsReadOnly => false;

        private readonly List<Field> fields;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldList"/> class.
        /// </summary>
        public FieldList()
        {
            fields = new List<Field>();
        }
        
        /// <summary>
        /// Copeies the elemends from the <see cref="FieldList"/> to a new array.
        /// </summary>
        /// <returns>An array of <see cref="Field"/> elements.</returns>
        public Field[] ToArray()
        {
            return fields.ToArray();
        }
        /// <summary>
        /// Adds a field to the end of the <see cref="FieldList"/>.
        /// </summary>
        /// <param name="field">The field to add.</param>
        public void Add(Field field)
        {
            fields.Add(field);
        }
        /// <summary>
        /// Adds an <see cref="ExplanationField"/> object to the end of the <see cref="FieldList"/>.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="explanation">The explanation.</param>
        public void Add(string name, string explanation)
        {
            fields.Add(new ExplanationField(name, explanation));
        }
        /// <summary>
        /// Adds a <see cref="ValueField"/> object to the end of the <see cref="FieldList"/>.
        /// </summary>
        /// /// <param name="name">The name of the field.</param>
        /// <param name="fieldType">The value type.</param>
        public void Add(string name, FieldType fieldType)
        {
            fields.Add(new ValueField(name, fieldType));
        }
        /// <summary>
        /// Adds an <see cref="EnumField"/> object to the end of the <see cref="FieldList"/>.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="enumType">The enum type of the field.</param>
        /// <param name="options">The enum options.</param>
        public void Add(string name, EnumType enumType, params string[] options)
        {
            fields.Add(new EnumField(name, enumType, options));
        }
        /// <summary>
        /// Removes all fields from the <see cref="FieldList"/>.
        /// </summary>
        public void Clear()
        {
            fields.Clear();
        }
        /// <summary>
        /// Determines whether a field is in the <see cref="FieldList"/>.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool Contains(Field field)
        {
            return fields.Contains(field);
        }

        void ICollection<Field>.CopyTo(Field[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        IEnumerator<Field> IEnumerable<Field>.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        int IList<Field>.IndexOf(Field item)
        {
            throw new System.NotImplementedException();
        }

        void IList<Field>.Insert(int index, Field item)
        {
            throw new System.NotImplementedException();
        }

        bool ICollection<Field>.Remove(Field item)
        {
            throw new System.NotImplementedException();
        }

        void IList<Field>.RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// Represents an enumerator containing possible enum field types.
    /// </summary>
    public enum EnumType : byte
    {
        /// <summary>
        /// Represents an enumerator type.
        /// </summary>
        Enumerator,
        /// <summary>
        /// Represents a flags type.
        /// </summary>
        Flags
    };
    /// <summary>
    /// Represents an enumerator containing possible field types.
    /// </summary>
    public enum FieldType : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        String,
        LongString,
        StringId,
        OldStringId,
        CharInteger,
        ShortInteger,
        LongInteger,
        Angle,
        Tag,
        Point2D,
        Rectangle2D,
        RgbColor,
        ArgbColor,
        Real,
        RealFraction,
        RealPoint2D,
        RealPoint3D,
        RealVector2D,
        RealVector3D,
        Quaternion,
        EulerAngles2D,
        EulerAngles3D,
        RealPlane2D,
        RealPlane3D,
        RealRgbColor,
        RealArgbColor,
        RealHsvColor,
        RealAhsvColor,
        RealShortBounds,
        RealAngleBounds,
        RealBounds,
        RealFractionBounds,
        TagReference,
        CharBlockIndex1,
        CharBlockIndex2,
        ShortBlockIndex1,
        ShortBlockIndex2,
        LongBlockIndex1,
        LongBlockIndex2,
        VertexBuffer,
        ArrayStart,
        ArrayEnd,
        Pad,
        UselessPad,
        Skip,
#pragma warning restore CS1591
    }
}
