using Abide.Guerilla.H2Guerilla;
using System;

namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents a field definition.
    /// </summary>
    public class TagFieldDefinition
    {
        private const int fieldStructSize = 16;

        /// <summary>
        /// Gets the number to offset the address by to arrive at the next field.
        /// By default, this value is the size of a single field definition structure.
        /// </summary>
        public virtual int Nudge
        {
            get { return fieldStructSize; }
        }
        /// <summary>
        /// Gets and returns the name of the field.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets and returns the field type.
        /// </summary>
        public FieldType Type
        {
            get { return type; }
        }
        /// <summary>
        /// Gets and returns the field's definition address.
        /// </summary>
        public int DefinitionAddress
        {
            get { return definitionAddress; }
        }
        /// <summary>
        /// Gets and returns the field's group tag.
        /// </summary>
        public int GroupTag
        {
            get { return groupTag; }
        }
        
        private FieldType type;
        private int definitionAddress;
        private int groupTag;
        private string name;

        /// <summary>
        /// Initaializes a new instance of the <see cref="TagFieldDefinition"/> class.
        /// </summary>
        public TagFieldDefinition()
        {
            name = string.Empty;
        }
        /// <summary>
        /// Reads this field using the supplied guerilla binary reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        internal virtual void Read(GuerillaBinaryReader reader)
        {
            //Read field
            TagField field = reader.ReadTagField();
            type = field.Type;
            definitionAddress = field.DefinitionAddress;
            groupTag = field.GroupTag;
            name = reader.ReadLocalizedString(field.NameAddress);
        }
        /// <summary>
        /// Retrieves a string representation of this field.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{Enum.GetName(typeof(FieldType), type).Replace("Field", string.Empty)} {name}";
        }
    }
}
