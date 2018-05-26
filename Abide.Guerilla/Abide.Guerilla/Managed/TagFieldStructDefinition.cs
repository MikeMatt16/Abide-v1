using Abide.Guerilla.H2Guerilla;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents a tag structure field definition.
    /// </summary>
    public sealed class TagFieldStructDefinition : TagFieldDefinition
    {
        /// <summary>
        /// Gets and returns the structure's name.
        /// </summary>
        public string StructName
        {
            get { return name; }
        }
        /// <summary>
        /// Gets and returns the structure's display name.
        /// </summary>
        public string DisplayName
        {
            get { return displayName; }
        }
        /// <summary>
        /// Gets and returns the field's group tag.
        /// </summary>
        public new Tag GroupTag
        {
            get { return groupTag; }
        }
        /// <summary>
        /// Gets and returns the field's block definition address.
        /// </summary>
        public int BlockDefinitionAddresss
        {
            get { return blockDefinitionAddress; }
        }

        private int nameAddress;
        private Tag groupTag;
        private int blockDefinitionAddress;
        private string name;
        private string displayName;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagFieldStructDefinition"/> class.
        /// </summary>
        public TagFieldStructDefinition() : base() { }
        /// <summary>
        /// Reads this field using the supplied guerilla binary reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        internal override void Read(GuerillaBinaryReader reader)
        {
            //Read
            base.Read(reader);

            //Goto
            reader.BaseStream.Seek(DefinitionAddress - Guerilla.BaseAddress, System.IO.SeekOrigin.Begin);
            
            //Read fields
            nameAddress = reader.ReadInt32();
            groupTag = reader.ReadTag();
            int displayNameAddress = reader.ReadInt32();
            blockDefinitionAddress = reader.ReadInt32();

            //Read strings
            name = reader.ReadLocalizedString(nameAddress);
            displayName = reader.ReadLocalizedString(displayNameAddress);
        }
    }
}
