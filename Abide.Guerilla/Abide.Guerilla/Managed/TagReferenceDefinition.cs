using Abide.Guerilla.H2Guerilla;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents a tag reference field definition.
    /// </summary>
    public sealed class TagReferenceDefinition : TagFieldDefinition
    {
        /// <summary>
        /// Gets and returns the field's flags.
        /// </summary>
        public int Flags
        {
            get { return flags; }
        }
        /// <summary>
        /// Gets and returns the field's group tag.
        /// </summary>
        public new Tag GroupTag
        {
            get { return groupTag; }
        }
        /// <summary>
        /// Gets and returns the field's group tags address.
        /// </summary>
        public int GroupTagsAddress
        {
            get { return groupTagsAddress; }
        }

        private int flags;
        private Tag groupTag;
        private int groupTagsAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagReferenceDefinition"/> class.
        /// </summary>
        public TagReferenceDefinition() : base() { }
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
            flags = reader.ReadInt32();
            groupTag = reader.ReadTag();
            groupTagsAddress = reader.ReadInt32();
        }
        /// <summary>
        /// Retrieves a string representation of this tag reference definition.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{base.ToString()} ({groupTag})";
        }
    }
}
