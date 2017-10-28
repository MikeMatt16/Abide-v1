using Abide.Guerilla.Tags;
using Abide.HaloLibrary;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Abide.Guerilla
{
    /// <summary>
    /// Me want banana.
    /// </summary>
    public static class Guerilla
    {
        /// <summary>
        /// The image load address used for translating virtual addresses into physical addresses.
        /// </summary>
        public const int BaseAddress = 0x400000;
        /// <summary>
        /// The virtual addrss of the tag layout table.
        /// </summary>
        public const int TagLayoutTableAddress = 0x00901B90;
        /// <summary>
        /// The number of tag layouts in the layout table.
        /// </summary>
        public const int NumberOfTagLayouts = 120;
        /// <summary>
        /// Returns a dictionary of tag types based on a given tag.
        /// </summary>
        public static Dictionary<Tag, Type> TagGroupDictionary
        {
            //get { return Tags.Guerilla.TagGroupDictionary; }
            get { return null; }
        }
        
        /// <summary>
        /// Gets the block attribute from a supplied type.
        /// </summary>
        /// <param name="blockType">The type to retrieve the block attribute from.</param>
        /// <returns>An instance of <see cref="BlockAttribute"/> if the attribute was found; otherwise null.</returns>
        public static BlockAttribute GetBlock(Type blockType)
        {
            //Return
            return blockType.GetCustomAttribute<BlockAttribute>();
        }
        /// <summary>
        /// Gets the block attribute from a supplied type.
        /// </summary>
        /// <param name="fieldSetType">The type to retrieve the field set attribute from.</param>
        /// <returns>An instance of <see cref="FieldSetAttribute"/> if the attribute was found; otherwise null.</returns>
        public static FieldSetAttribute GetFieldSet(Type fieldSetType)
        {
            //Return
            return fieldSetType.GetCustomAttribute<FieldSetAttribute>();
        }
        /// <summary>
        /// Gets the block attribute from a supplied type.
        /// </summary>
        /// <param name="tagGroupType">The type to retrieve the tag group attribute from.</param>
        /// <returns>An instance of <see cref="TagGroupAttribute"/> if the attribute was found; otherwise null.</returns>
        public static TagGroupAttribute GetTagGroup(Type tagGroupType)
        {
            //Return
            return tagGroupType.GetCustomAttribute<TagGroupAttribute>();
        }
    }
}
