using Abide.H2Guerilla.H2Guerilla;

namespace Abide.H2Guerilla.Managed
{
    /// <summary>
    /// Represents an enum field definition.
    /// </summary>
    public sealed class TagFieldEnumDefinition : TagFieldDefinition
    {
        /// <summary>
        /// Gets and returns this enum's option strings.
        /// </summary>
        public string[] Options
        {
            get { return options; }
        }
        /// <summary>
        /// Gets and returns this enum's option count.
        /// </summary>
        public int OptionCount
        {
            get { return optionCount; }
        }
        /// <summary>
        /// Gets and returns this enum's flags.
        /// </summary>
        public int Flags
        {
            get { return flags; }
        }

        private int optionCount;
        private int flags;
        private string[] options;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagFieldEnumDefinition"/> class.
        /// </summary>
        public TagFieldEnumDefinition() : base() { }
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

            //Read fields
            optionCount = reader.ReadInt32();
            int optionsAddress = reader.ReadInt32();
            flags = reader.ReadInt32();

            //Prepare
            reader.BaseStream.Seek(optionsAddress - Guerilla.BaseAddress, System.IO.SeekOrigin.Begin);
            int[] addresses = new int[optionCount];
            for (int i = 0; i < optionCount; i++)
                addresses[i] = reader.ReadInt32();

            //Loop
            options = new string[optionCount];
            for (int i = 0; i < optionCount; i++)
            {
                if (addresses[i] != 0) options[i] = reader.ReadLocalizedString(addresses[i]);
                else options[i] = $"o.O {i}";
            }
        }
    }
}
