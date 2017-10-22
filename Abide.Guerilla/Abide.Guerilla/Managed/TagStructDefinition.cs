﻿using Abide.Guerilla.H2Guerilla;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents a tag structure field definition.
    /// </summary>
    public sealed class TagStructDefinition : TagFieldDefinition
    {
        /// <summary>
        /// Gets and returns the field's name address.
        /// </summary>
        public new int NameAddress
        {
            get { return nameAddress; }
        }
        /// <summary>
        /// Gets and returns the field's group tag.
        /// </summary>
        public new Tag GroupTag
        {
            get { return groupTag; }
        }
        /// <summary>
        /// Gets and returns the field's display name address.
        /// </summary>
        public int DisplayNameAddress
        {
            get { return displayNameAddress; }
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
        private int displayNameAddress;
        private int blockDefinitionAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagStructDefinition"/> class.
        /// </summary>
        public TagStructDefinition() : base() { }
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
            displayNameAddress = reader.ReadInt32();
            blockDefinitionAddress = reader.ReadInt32();
        }
    }
}
