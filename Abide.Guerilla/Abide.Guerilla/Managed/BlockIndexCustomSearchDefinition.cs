using Abide.Guerilla.H2Guerilla;

namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents a block index custom search field definition.
    /// </summary>
    public sealed class BlockIndexCustomSearchDefinition : TagFieldDefinition
    {
        private int getBlockProcedure;
        private int isValidSourceBlockProcedure;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockIndexCustomSearchDefinition"/> class.
        /// </summary>
        public BlockIndexCustomSearchDefinition() : base() { }
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

            //Read Fields
            getBlockProcedure = reader.ReadInt32();
            isValidSourceBlockProcedure = reader.ReadInt32();
        }
    }
}
