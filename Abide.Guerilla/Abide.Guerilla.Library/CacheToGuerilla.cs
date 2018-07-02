using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.Tag;
using Abide.Tag.Guerilla;
using System.IO;
using static Abide.HaloLibrary.Halo2Map.MapFile;

namespace Abide.Guerilla.Library
{
    /// <summary>
    /// Provides a set of <see langword="static"/> (<see langword="Shared"/> in Visual Basic) methods for converting Cache tag objects to Guerilla tag objects.
    /// </summary>
    public static class CacheToGuerilla
    {
        /// <summary>
        /// Creates a guerilla tag group object based on a specified index entry.
        /// </summary>
        /// <param name="entry">The cache file index entry.</param>
        /// <returns>A new guerilla tag group instance.</returns>
        public static ITagGroup ToGuerilla(this IndexEntry entry)
        {
            //Create block
            ITagGroup guerillaGroup = Tag.Guerilla.Generated.TagLookup.CreateTagGroup(entry.Root);
            ITagGroup cacheGroup = Tag.Cache.Generated.TagLookup.CreateTagGroup(entry.Root);

            //Read cache group
            using (BinaryReader reader = entry.TagData.CreateReader())
            {
                entry.TagData.Seek((uint)entry.PostProcessedOffset, SeekOrigin.Begin);
                cacheGroup.Read(reader);
            }

            //Check
            switch (entry.Root)
            {
                case "snd!":
                    break;
                case "unic":
                    break;
                default:
                    for (int i = 0; i < guerillaGroup.Count; i++)
                        guerillaGroup[i].ToGuerilla(cacheGroup[i], entry);
                    break;
            }

            //Return
            return guerillaGroup;
        }
        private static void ToGuerilla(this ITagBlock guerillaBlock, ITagBlock cacheBlock, IndexEntry entry)
        {
            //Loop through fields
            for (int i = 0; i < guerillaBlock.Fields.Count; i++)
                guerillaBlock.Fields[i].ToGuerilla(cacheBlock.Fields[i], entry);
        }
        private static void ToGuerilla(this Field guerillaField, Field cacheField, IndexEntry entry)
        {
            //Prepare
            IndexEntry referencedEntry = null;
            StringList strings = entry.Owner.Strings;
            IndexEntryList indexList = entry.Owner.IndexEntries;
            string tagGroupName = string.Empty;
            
            //Handle type
            switch (guerillaField.Type)
            {
                case Tag.Definition.FieldType.FieldOldStringId:
                case Tag.Definition.FieldType.FieldStringId:
                    if (cacheField.Value is StringId sid)
                    {
                        string stringId = string.Empty;
                        if (strings.Count > sid.Index) stringId = strings[sid.Index];
                        guerillaField.Value = new StringValue(stringId);
                    }
                    break;
                case Tag.Definition.FieldType.FieldTagReference:
                    if(cacheField.Value is TagReference tagRef && !tagRef.Id.IsNull && indexList.ContainsID(tagRef.Id))
                    {
                        referencedEntry = indexList[tagRef.Id];
                        using (var group = Tag.Guerilla.Generated.TagLookup.CreateTagGroup(referencedEntry.Root)) tagGroupName = group.Name;
                        guerillaField.Value = new StringValue($"{referencedEntry.Filename}.{tagGroupName}");
                    }
                    break;
                case Tag.Definition.FieldType.FieldTagIndex:
                    if(cacheField.Value is TagId id && !id.IsNull && indexList.ContainsID(id))
                    {
                        referencedEntry = indexList[id];
                        using (var group = Tag.Guerilla.Generated.TagLookup.CreateTagGroup(referencedEntry.Root)) tagGroupName = group.Name;
                        guerillaField.Value = new StringValue($"{referencedEntry.Filename}.{tagGroupName}");
                    }
                    break;
                case Tag.Definition.FieldType.FieldData:
                    if (cacheField is DataField cacheData && guerillaField is DataField guerillaData)
                        guerillaData.SetBuffer(cacheData.GetBuffer());
                    break;
                case Tag.Definition.FieldType.FieldBlock:
                    if (cacheField is BaseBlockField cacheBlock && guerillaField is BaseBlockField guerillaBlock)
                        foreach (ITagBlock cacheChildBlock in cacheBlock.BlockList)
                        {
                            ITagBlock guerillaChildBlock = guerillaBlock.Add(out bool success);
                            if (success) guerillaChildBlock.ToGuerilla(cacheChildBlock, entry);
                        }
                    break;
                case Tag.Definition.FieldType.FieldStruct:
                    ((ITagBlock)guerillaField.Value).ToGuerilla(((ITagBlock)cacheField.Value), entry);
                    break;
                default: guerillaField.Value = cacheField.Value; break;
            }
        }
    }
}
