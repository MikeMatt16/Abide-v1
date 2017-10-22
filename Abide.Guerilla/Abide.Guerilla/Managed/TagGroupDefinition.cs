using Abide.HaloLibrary;

namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents a tag group definition.
    /// </summary>
    public sealed class TagGroupDefinition
    {
        /// <summary>
        /// Gets and returns the tag group's name.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets and returns the tag group's group tag.
        /// </summary>
        public Tag GroupTag
        {
            get { return groupTag; }
        }
        /// <summary>
        /// Gets and returns the tag group's parent group tag.
        /// </summary>
        public Tag ParentGroupTag
        {
            get { return parentGroupTag; }
        }
        /// <summary>
        /// Gets and returns the tag group's version address.
        /// </summary>
        public short Version
        {
            get { return version; }
        }
        /// <summary>
        /// Gets and returns the tag group's definition address.
        /// </summary>
        public int DefinitionAddress
        {
            get { return definitionAddress; }
        }

        private int nameAddress;
        private int flags;
        private Tag groupTag;
        private Tag parentGroupTag;
        private short version;
        private byte initialized;
        private int postProcessProcedure;
        private int saveProcessProcedure;
        private int definitionAddress;
        private int[] childGroupTags;
        private short childsCount;
        private int defaultTagPathAddress;
        private string name;

        /// <summary>
        /// Intializes a new instance of the <see cref="TagGroupDefinition"/> class.
        /// </summary>
        public TagGroupDefinition() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="TagGroupDefinition"/> class using the supplied tag group structure.
        /// </summary>
        /// <param name="tagGroup">The tag group structure to initialize this instance with.</param>
        internal TagGroupDefinition(H2Guerilla.TagGroup tagGroup) : this()
        {
            nameAddress = tagGroup.NameAddress;
            flags = tagGroup.Flags;
            groupTag = tagGroup.GroupTag;
            parentGroupTag = tagGroup.ParentGroupTag;
            version = tagGroup.Version;
            initialized = tagGroup.Initialized;
            postProcessProcedure = tagGroup.PostProcessProcedure;
            saveProcessProcedure = tagGroup.SavePostProcessProcedure;
            definitionAddress = tagGroup.DefinitionAddress;
            childGroupTags = tagGroup.ChildGroupTags;
            childsCount = tagGroup.ChildsCount;
            defaultTagPathAddress = tagGroup.DefaultTagPathAddress;
            name = tagGroup.Name;
        }
        /// <summary>
        /// Returns a string that represents this tag group definition.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{parentGroupTag}.{groupTag} {name}";
        }
    }
}
