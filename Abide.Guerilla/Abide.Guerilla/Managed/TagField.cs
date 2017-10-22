using System;

namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents a field within a block.
    /// </summary>
    public class TagFieldDefinition
    {
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
        private int nameAddress;
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
        internal virtual void Read(H2Guerilla.GuerillaBinaryReader reader)
        {
            //Read field
            H2Guerilla.TagField field = reader.ReadTagField();
            type = field.Type;
            nameAddress = field.NameAddress;
            definitionAddress = field.DefinitionAddress;
            groupTag = field.GroupTag;
            name = field.Name;
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
