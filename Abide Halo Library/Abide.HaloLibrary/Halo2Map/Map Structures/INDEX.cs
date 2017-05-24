using System;
using System.Runtime.InteropServices;

namespace Abide.HaloLibrary.Halo2Map
{
    /// <summary>
    /// Represents a 32-byte length Halo 2 Map <see cref="INDEX"/> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = Length), Serializable]
    public struct INDEX
    {
        /// <summary>
        /// Represents the length of an <see cref="INDEX"/> structure in bytes.
        /// </summary>
        public const int Length = 32;
        /// <summary>
        /// Represents the tags four-character code located at the end of this structure.
        /// This value is constant.
        /// </summary>
        public const string TagsFourCC = HaloTags.tags;
        /// <summary>
        /// The Xbox memory address of the index.
        /// This value is constant.
        /// </summary>
        public const int IndexMemoryAddress = -2147086304;

        /// <summary>
        /// Gets or sets the address of the index.
        /// </summary>
        public int IndexAddress
        {
            get { return indexAddress; }
            set { indexAddress = value; }
        }
        /// <summary>
        /// Gets or sets the number of tags that are present in the index.
        /// </summary>
        public int TagCount
        {
            get { return tagCount; }
            set { tagCount = value; }
        }
        /// <summary>
        /// Gets or sets the offset of the object list.
        /// </summary>
        public int ObjectOffset
        {
            get { return objectOffset; }
            set { objectOffset = value; }
        }
        /// <summary>
        /// Gets or sets the scenario id.
        /// </summary>
        public TAGID ScenarioID
        {
            get { return scenarioId; }
            set { scenarioId = value; }
        }
        /// <summary>
        /// Gets or sets the match globals id.
        /// </summary>
        public TAGID GlobalsID
        {
            get { return globalsId; }
            set { globalsId = value; }
        }
        /// <summary>
        /// Gets or sets the number of object entries that are present in the index.
        /// </summary>
        public int ObjectCount
        {
            get { return objectCount; }
            set { objectCount = value; }
        }
        /// <summary>
        /// Gets or sets the tags four-character code string.
        /// </summary>
        public TAG Tags
        {
            get { return tags; }
            set { tags = value; }
        }
        
        private int indexAddress;
        private int tagCount;
        private int objectOffset;
        private TAGID scenarioId;
        private TAGID globalsId;
        private TAGID secondaryId;
        private int objectCount;
        private TAG tags;
        
        /// <summary>
        /// Creates a new <see cref="INDEX"/> structure.
        /// </summary>
        /// <returns>A new <see cref="INDEX"/> structure.</returns>
        public static INDEX Create()
        {
            //Create
            INDEX index = new INDEX();

            //Setup
            index.indexAddress = IndexMemoryAddress;
            index.objectOffset = IndexMemoryAddress + Length;
            index.scenarioId = -1;
            index.globalsId = -1;
            index.secondaryId = -1;
            index.tags = HaloTags.tags;

            //Return
            return index;
        }
	}
}