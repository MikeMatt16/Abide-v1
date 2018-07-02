using Abide.Tag.Definition;
using System;
using System.Collections.Generic;

namespace Abide.Tag
{
    /// <summary>
    /// Represents a collection of tag definition objects.
    /// </summary>
    public class TagDefinitionCollection
    {
        private readonly List<AbideTagGroup> tagGroups = new List<AbideTagGroup>();
        private readonly List<AbideTagBlock> tagBlocks = new List<AbideTagBlock>();
        private readonly Dictionary<string, int> tagGroupLookup = new Dictionary<string, int>();
        private readonly Dictionary<string, int> tagBlockLookup = new Dictionary<string, int>();

        /// <summary>
        /// Clears the collection.
        /// </summary>
        public void Clear()
        {
            //Clear
            tagGroupLookup.Clear();
            tagBlockLookup.Clear();
            tagGroups.Clear();
            tagBlocks.Clear();

            //Collect
            GC.Collect();
        }
        /// <summary>
        /// Adds an Abide tag group definition to the collection.
        /// </summary>
        /// <param name="tagGroup">The tag group definition.</param>
        public void Add(AbideTagGroup tagGroup)
        {
            //Check
            if (tagGroup == null) return;

            //Check
            if (!tagGroups.Contains(tagGroup) && !tagGroupLookup.ContainsKey(tagGroup.GroupTag))
            {
                tagGroupLookup.Add(tagGroup.GroupTag, tagGroups.Count);
                tagGroups.Add(tagGroup);
            }
        }
        /// <summary>
        /// Adds an Abide tag block definition to the collection.
        /// </summary>
        /// <param name="tagBlock">The tag block definition.</param>
        public void Add(AbideTagBlock tagBlock)
        {
            //Check
            if (tagBlock == null) return;

            //Check
            if (!tagBlocks.Contains(tagBlock) && !tagBlockLookup.ContainsKey(tagBlock.Name))
            {
                tagBlockLookup.Add(tagBlock.Name, tagBlocks.Count);
                tagBlocks.Add(tagBlock);
            }
        }
        /// <summary>
        /// Gets and returns an Abide tag group definition by its group tag.
        /// </summary>
        /// <param name="groupTag">The tag four-character code of the tag group to retrieve.</param>
        /// <returns>An <see cref="AbideTagGroup"/> instance whose <see cref="AbideTagGroup.GroupTag"/> property matches <paramref name="groupTag"/>; otherwise, returns <see langword="null"/>.</returns>
        public AbideTagGroup GetTagGroup(string groupTag)
        {
            if (tagGroupLookup.ContainsKey(groupTag))
                return tagGroups[tagGroupLookup[groupTag]];
            else return null;
        }
        /// <summary>
        /// Gets and returns an Abide tag block definition by its name.
        /// </summary>
        /// <param name="blockName">The name of the tag block definition.</param>
        /// <returns>An <see cref="AbideTagBlock"/> definition whose <see cref="AbideTagBlock.Name"/> property matches <paramref name="blockName"/>; otherwise, returns <see langword="null"/>.</returns>
        public AbideTagBlock GetTagBlock(string blockName)
        {
            if (tagBlockLookup.ContainsKey(blockName))
                return tagBlocks[tagBlockLookup[blockName]];
            else return null;
        }
        /// <summary>
        /// Gets and returns an array of all Abide tag group definitions present in this collection.
        /// </summary>
        /// <returns>An arry of <see cref="AbideTagGroup"/> elements.</returns>
        public AbideTagGroup[] GetTagGroups()
        {
            return tagGroups.ToArray();
        }
        /// <summary>
        /// Gets and returns an array of all Abide tag block definitions present in this collection.
        /// </summary>
        /// <returns>An array of <see cref="AbideTagBlock"/> elements.</returns>
        public AbideTagBlock[] GetTagBlocks()
        {
            return tagBlocks.ToArray();
        }
    }
}
