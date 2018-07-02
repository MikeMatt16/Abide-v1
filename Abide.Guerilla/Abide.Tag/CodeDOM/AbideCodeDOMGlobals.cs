using Abide.Tag.Definition;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;
using Abide.Tag.Preprocess;

namespace Abide.Tag.CodeDom
{
    /// <summary>
    /// Represents global variables to be used with Abide Tag CodeDOM
    /// </summary>
    public static class AbideCodeDomGlobals
    {
        private static readonly List<string> memberNames = new List<string>();
        private static readonly Dictionary<string, AbideTagGroup> tagGroupTagLookup = new Dictionary<string, AbideTagGroup>();
        private static readonly Dictionary<AbideTagBlock, string> tagBlockMemberLookup = new Dictionary<AbideTagBlock, string>();
        private static readonly Dictionary<AbideTagGroup, string> tagGroupMemberLookup = new Dictionary<AbideTagGroup, string>();
        private static readonly Dictionary<string, AbideTagBlock> tagBlockNameLookup = new Dictionary<string, AbideTagBlock>();
        private static readonly Dictionary<string, AbideTagGroup> tagGroupNameLookup = new Dictionary<string, AbideTagGroup>();

        /// <summary>
        /// Clears the global singleton.
        /// </summary>
        public static void Clear()
        {
            //Clear
            memberNames.Clear();
            tagGroupTagLookup.Clear();
            tagBlockMemberLookup.Clear();
            tagGroupMemberLookup.Clear();
            tagBlockNameLookup.Clear();
            tagGroupNameLookup.Clear();

            //Collect
            GC.Collect();
        }

        /// <summary>
        /// Preprocesses a tag cache object for cache.
        /// </summary>
        /// <param name="collection">The tag collection object containing tag blocks and tag groups.</param>
        public static void PreprocessForCache(TagDefinitionCollection collection)
        {
            //Loop
            foreach(AbideTagBlock tagBlock in collection.GetTagBlocks())
            {
                GeneralPreprocess.Preprocess(tagBlock, collection);
                CachePreprocess.Preprocess(tagBlock, collection);
            }

            //Preprocess
            Preprocess(collection.GetTagGroups(), collection.GetTagBlocks());
        }
        /// <summary>
        /// Preprocesses a tag cache object for guerilla.
        /// </summary>
        /// <param name="collection">The tag collection object containing tag blocks and tag groups.</param>
        public static void PreprocessForGuerilla(TagDefinitionCollection collection)
        {
            //Loop
            foreach (AbideTagBlock tagBlock in collection.GetTagBlocks())
            {
                GeneralPreprocess.Preprocess(tagBlock, collection);
                GuerillaPreprocess.Preprocess(tagBlock, collection);
            }

            //Preprocess
            Preprocess(collection.GetTagGroups(), collection.GetTagBlocks());
        }
        /// <summary>
        /// Preprocesses tag groups and their tag blocks.
        /// </summary>
        /// <param name="tagGroups"></param>
        /// <param name="tagBlocks"></param>
        public static void Preprocess(AbideTagGroup[] tagGroups, AbideTagBlock[] tagBlocks)
        {
            //Create find function
            Func<string, AbideTagBlock> blockFindFunction = new Func<string, AbideTagBlock>((blockName) =>
            {
                //Find
                return Array.Find(tagBlocks, b => b.Name == blockName);
            });

            //Create block search function
            Action<AbideTagBlock> blockSearchAction = null;
            blockSearchAction = new Action<AbideTagBlock>((block) =>
            {
                //Loop
                foreach (AbideTagField field in block.FieldSet)
                {
                    //Check
                    if(field.FieldType == FieldType.FieldBlock)
                    {
                        //Find
                        AbideTagBlock child = blockFindFunction(field.BlockName);
                        
#if DEBUG
                        //Check
                        if (child == null) System.Diagnostics.Debugger.Break();
#endif

                        //Process
                        Preprocess(child);

                        //Search
                        blockSearchAction.Invoke(child);
                    }
                    else if(field.FieldType == FieldType.FieldStruct)
                    {
                        //Find
                        AbideTagBlock child = blockFindFunction(field.StructName);

#if DEBUG
                        //Check
                        if (child == null) System.Diagnostics.Debugger.Break();
#endif

                        //Process
                        Preprocess(child);

                        //Search
                        blockSearchAction.Invoke(child);
                    }
                }
            });

            //Loop through tag groups
            foreach (AbideTagGroup tagGroup in tagGroups)
            {
                //Preprocess group
                Preprocess(tagGroup);

                //Preprocess the group's tag block
                AbideTagBlock block = blockFindFunction.Invoke(tagGroup.BlockName);
#if DEBUG
                //Check
                if (block == null) System.Diagnostics.Debugger.Break();
#endif

                //Preprocess
                Preprocess(block);

                //Search
                blockSearchAction.Invoke(block);
            }
        }
        /// <summary>
        /// Preprocesses a tag group, preparing a valid member name.
        /// </summary>
        /// <param name="tagGroup">The tag group.</param>
        public static void Preprocess(AbideTagGroup tagGroup)
        {
            memberNames.Add(GetMemberName(tagGroup));
        }
        /// <summary>
        /// Preprocesses a tag block, preparing a valid member name.
        /// </summary>
        /// <param name="tagBlock">The tag block.</param>
        private static void Preprocess(AbideTagBlock tagBlock)
        {
            memberNames.Add(GetMemberName(tagBlock));
        }
        /// <summary>
        /// Returns a pascal-cased member name of the tag block.
        /// </summary>
        /// <param name="tagBlock">The tag block.</param>
        /// <returns>A string.</returns>
        public static string GetMemberName(AbideTagBlock tagBlock)
        {
            //Check
            if (!tagBlockMemberLookup.ContainsKey(tagBlock))
            {
                tagBlockNameLookup.Add(tagBlock.Name, tagBlock);
                tagBlockMemberLookup.Add(tagBlock, GeneratePascalMemberName(tagBlock.Name));
            }

            //Return
            return tagBlockMemberLookup[tagBlock];
        }
        /// <summary>
        /// Returns a pascal-cased member name of the tag group.
        /// </summary>
        /// <param name="tagGroup">The tag group.</param>
        /// <returns>A string.</returns>
        public static string GetMemberName(AbideTagGroup tagGroup)
        {
            //Check
            if (!tagGroupMemberLookup.ContainsKey(tagGroup))
            {
                tagGroupTagLookup.Add(tagGroup.GroupTag, tagGroup);
                tagGroupNameLookup.Add(tagGroup.Name, tagGroup);
                tagGroupMemberLookup.Add(tagGroup, GeneratePascalMemberName(tagGroup.Name));
            }

            //Return
            return tagGroupMemberLookup[tagGroup];
        }
        /// <summary>
        /// Returns an <see cref="AbideTagGroup"/> based on its group tag.
        /// </summary>
        /// <param name="groupTag">The group tag.</param>
        /// <returns>A, <see cref="AbideTagGroup"/> instance; or <see langref="null"/>.</returns>
        public static AbideTagGroup GetTagGroup(string groupTag)
        {
            //Check
            if (groupTag == null) return null;

            //Return
            if (tagGroupTagLookup.ContainsKey(groupTag))
                return tagGroupTagLookup[groupTag];
            return null;
        }
        /// <summary>
        /// Returns an <see cref="AbideTagBlock"/> based on its name.
        /// </summary>
        /// <param name="blockName">The name of the block.</param>
        /// <returns>An <see cref="AbideTagBlock"/> instance; or <see langref="null"/>.</returns>
        public static AbideTagBlock GetTagBlock(string blockName)
        {
            //Check
            if (blockName == null) return null;

            //Return
            if (tagBlockNameLookup.ContainsKey(blockName))
                return tagBlockNameLookup[blockName];
            return null;
        }
        /// <summary>
        /// Returns all tag groups present in the global scope.
        /// </summary>
        /// <returns>An array of <see cref="AbideTagGroup"/> elements.</returns>
        public static AbideTagGroup[] GetTagGroups()
        {
            //Return
            return tagGroupTagLookup.Select(kvp => kvp.Value).ToArray();
        }
        /// <summary>
        /// Returns all tag blocks present in the global scope.
        /// </summary>
        /// <returns>An array of <see cref="AbideTagBlock"/> elements.</returns>
        public static AbideTagBlock[] GetTagBlocks()
        {
            //Return
            return tagBlockNameLookup.Select(kvp => kvp.Value).ToArray();
        }

        private static string GenerateCamelCasedMemberName(string name)
        {
            //Prepare
            StringBuilder memberName = new StringBuilder();
            string[] parts = name.Split('_', ' ', '-', '.', ',');

            //Append
            memberName.Append(parts[0].ToLower());

            //Loop
            for (int i = 1; i < parts.Length; i++)
                if (parts[i].Length > 0)
                {
                    memberName.Append(char.ToUpper(parts[i][0]));
                    if (parts[i].Length > 1)
                        memberName.Append(parts[i].Substring(1));
                }

            //Check
            int duplicateCount = 0;
            string baseMemberName = memberName.ToString();
            while (memberNames.Contains(memberName.ToString()))
                memberName = new StringBuilder(baseMemberName + (++duplicateCount).ToString());

            //Return
            return memberName.ToString();
        }
        private static string GeneratePascalMemberName(string name)
        {
            //Prepare
            StringBuilder memberName = new StringBuilder();
            string[] parts = name.Split('_', ' ', '-', '.', ',');

            //Loop
            for (int i = 0; i < parts.Length; i++)
                if (parts[i].Length > 0)
                {
                    memberName.Append(char.ToUpper(parts[i][0]));
                    if (parts[i].Length > 1)
                        memberName.Append(parts[i].Substring(1));
                }
            
            //Check
            int duplicateCount = 0;
            string baseMemberName = memberName.ToString();
            while (memberNames.Contains(memberName.ToString()))
                memberName = new StringBuilder(baseMemberName + (++duplicateCount).ToString());

            //Return
            return memberName.ToString();
        }
    }
}
