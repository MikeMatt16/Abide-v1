using Abide.H2Guerilla.H2Guerilla;
using Abide.HaloLibrary;

namespace Abide.H2Guerilla.Managed
{
    /// <summary>
    /// Represents a tag reference field definition.
    /// </summary>
    public sealed class TagFieldTagReferenceDefinition : TagFieldDefinition
    {
        /// <summary>
        /// Gets and returns the field's flags.
        /// </summary>
        public int Flags { get; private set; }
        /// <summary>
        /// Gets and returns the field's group tag.
        /// </summary>
        public new TagFourCc GroupTag { get; private set; }
        /// <summary>
        /// Gets and returns the field's group tags address.
        /// </summary>
        public int GroupTagsAddress { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagFieldTagReferenceDefinition"/> class.
        /// </summary>
        public TagFieldTagReferenceDefinition() : base() { }
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
            Flags = reader.ReadInt32();
            GroupTag = reader.ReadTag();
            GroupTagsAddress = reader.ReadInt32();
        }
        /// <summary>
        /// Retrieves a string representation of this tag reference definition.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{base.ToString()} ({GroupTag})";
        }
    }
}
