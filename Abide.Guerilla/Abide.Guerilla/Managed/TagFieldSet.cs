namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents a tag field set.
    /// </summary>
    public sealed class TagFieldSet
    {
        /// <summary>
        /// Gets and returns the field set's alignment.
        /// </summary>
        public int Alignment
        {
            get { return alignmentBit != 0 ? (1 << alignmentBit) : 4; }
        }
        /// <summary>
        /// Gets and returns the tag field set's size string.
        /// </summary>
        public string SizeString
        {
            get { return sizeString; }
            internal set { sizeString = value; }
        }
        /// <summary>
        /// Gets and returns the tag field set's version.
        /// </summary>
        public FieldSetVersion Version
        {
            get { return version; }
            internal set { version = value; }
        }
        /// <summary>
        /// Gets and returns the tag field set's size.
        /// </summary>
        public int Size
        {
            get { return size; }
            internal set { size = value; }
        }
        /// <summary>
        /// Gets and returns the tag field set's alignment bit.
        /// </summary>
        public int AlignmentBit
        {
            get { return alignmentBit; }
            internal set { alignmentBit = value; }
        }
        /// <summary>
        /// Gets and returns the tag field set's parent version index.
        /// </summary>
        public int ParentVersionIndex
        {
            get { return parentVersionIndex; }
            internal set { parentVersionIndex = value; }
        }
        /// <summary>
        /// Gets and returns the tag field set's fields address.
        /// </summary>
        public int FieldsAddress
        {
            get { return fieldsAddress; }
            internal set { fieldsAddress = value; }
        }
        /// <summary>
        /// Gets and returns the tag field set's address.
        /// </summary>
        public int Address
        {
            get { return address; }
            internal set { address = value; }
        }
        
        private FieldSetVersion version;
        private int size;
        private int alignmentBit;
        private int parentVersionIndex;
        private int fieldsAddress;
        private int address;
        private string sizeString;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagFieldSet"/> class.
        /// </summary>
        public TagFieldSet()
        {
            version = new FieldSetVersion();
        }
        /// <summary>
        /// Reads the field set using the supplied guerilla binary reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        internal void Read(H2Guerilla.GuerillaBinaryReader reader)
        {
            var fieldSet = reader.ReadTagFieldSet();
            version = new FieldSetVersion(fieldSet.Version);
            size = fieldSet.Size;
            alignmentBit = fieldSet.AlignmentBit;
            parentVersionIndex = fieldSet.ParentVersionIndex;
            fieldsAddress = fieldSet.FieldsAddress;
            address = fieldSet.Address;
            sizeString = reader.ReadLocalizedString(fieldSet.SizeStringAddress);
        }

        /// <summary>
        /// Represents a tag field set version.
        /// </summary>
        public sealed class FieldSetVersion
        {
            /// <summary>
            /// Gets and returns the version's address.
            /// </summary>
            public int FieldsAddress
            {
                get { return fieldsAddress; }
                internal set { fieldsAddress = value; }
            }
            /// <summary>
            /// Gets and returns the version's index.
            /// </summary>
            public int Index
            {
                get { return index; }
                internal set { index = value; }
            }
            /// <summary>
            /// Gets and returns the version's size.
            /// </summary>
            public int SizeOf
            {
                get { return sizeOf; }
                internal set { sizeOf = value; }
            }
            /// <summary>
            /// Gets and returns the version's upgrade procedure.
            /// </summary>
            internal int UpgradeProcedure
            {
                get { return upgradeProcedure; }
                set { upgradeProcedure = value; }
            }

            private int fieldsAddress;
            private int index;
            private int upgradeProcedure;
            private int sizeOf;

            /// <summary>
            /// Initializes a new instance of the <see cref="FieldSetVersion"/> class.
            /// </summary>
            public FieldSetVersion() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="FieldSetVersion"/> class using the supplied tag field set version structure.
            /// </summary>
            /// <param name="version">The tag field set version structure to initialize this instance.</param>
            internal FieldSetVersion(H2Guerilla.TagFieldSetVersion version)
            {
                fieldsAddress = version.FieldsAddress;
                index = version.Index;
                upgradeProcedure = version.UpgradeProcedure;
                sizeOf = version.SizeOf;
            }
        }
    }
}
