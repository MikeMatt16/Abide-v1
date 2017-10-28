using Abide.Guerilla.H2Guerilla;

namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents a tag data field definition.
    /// </summary>
    public sealed class TagFieldDataDefinition : TagFieldDefinition
    {
        /// <summary>
        /// Gets and returns the field's alignment.
        /// </summary>
        public int Alignment
        {
            get { return alignmentBit != 0 ? (1 << alignmentBit) : 4; }
        }
        /// <summary>
        /// Gets and returns the field's definition name.
        /// </summary>
        public string DefinitionName
        {
            get { return definitionName; }
        }
        /// <summary>
        /// Gets and returns the field's maximum size string.
        /// </summary>
        public string MaximumSizeString
        {
            get { return maximumSizeString; }
        }
        /// <summary>
        /// Gets and returns the field's flags.
        /// </summary>
        public int Flags
        {
            get { return flags; }
        }
        /// <summary>
        /// Gets and returns the field's alignement bit.
        /// </summary>
        public int AlignmentBit
        {
            get { return alignmentBit; }
        }
        /// <summary>
        /// Gets and returns the field's maximum size.
        /// </summary>
        public int MaximumSize
        {
            get { return maximumSize; }
        }
        
        private int flags;
        private int alignmentBit;
        private int maximumSize;
        private string definitionName;
        private string maximumSizeString;

        /// <summary>
        /// Intializes a new instance of the <see cref="TagFieldDataDefinition"/> class.
        /// </summary>
        public TagFieldDataDefinition() : base() { }

        /// <summary>
        /// Reads the field using the supplied guerilla binary reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        internal override void Read(GuerillaBinaryReader reader)
        {
            //Read
            base.Read(reader);

            //Goto
            reader.BaseStream.Seek(DefinitionAddress - Guerilla.BaseAddress, System.IO.SeekOrigin.Begin);

            //Read Fields
            int definitionNameAddress = reader.ReadInt32();
            flags = reader.ReadInt32();
            alignmentBit = reader.ReadInt32();
            maximumSize = reader.ReadInt32();
            int maximumSizeStringAddress = reader.ReadInt32();

            //Read strings
            definitionName = reader.ReadLocalizedString(definitionNameAddress);
            maximumSizeString = reader.ReadLocalizedString(maximumSizeStringAddress);
        }
    }
}
