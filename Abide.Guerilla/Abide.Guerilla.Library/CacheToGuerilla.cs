using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.Tag;
using Abide.Tag.Guerilla;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        /// <param name="map">The cache map file that contains <paramref name="entry"/>.</param>
        /// <returns>A new guerilla tag group instance.</returns>
        public static ITagGroup ToGuerilla(this IndexEntry entry, MapFile map)
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
                case HaloTags.snd_: break;
                case HaloTags.unic:
                    MultilingualUnicode_ToGuerilla(guerillaGroup, entry.Strings);
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
        private static void MultilingualUnicode_ToGuerilla(ITagGroup multilingualUnicodeStringList, StringContainer strings)
        {
            //Get string IDs
            List<string> stringIds = new List<string>();
            foreach (var str in strings.English)
                if (!stringIds.Contains(str.ID)) stringIds.Add(str.ID);
            foreach (var str in strings.Japanese)
                if (!stringIds.Contains(str.ID)) stringIds.Add(str.ID);
            foreach (var str in strings.German)
                if (!stringIds.Contains(str.ID)) stringIds.Add(str.ID);
            foreach (var str in strings.French)
                if (!stringIds.Contains(str.ID)) stringIds.Add(str.ID);
            foreach (var str in strings.Spanish)
                if (!stringIds.Contains(str.ID)) stringIds.Add(str.ID);
            foreach (var str in strings.Italian)
                if (!stringIds.Contains(str.ID)) stringIds.Add(str.ID);
            foreach (var str in strings.Korean)
                if (!stringIds.Contains(str.ID)) stringIds.Add(str.ID);
            foreach (var str in strings.Chinese)
                if (!stringIds.Contains(str.ID)) stringIds.Add(str.ID);
            foreach (var str in strings.Portuguese)
                if (!stringIds.Contains(str.ID)) stringIds.Add(str.ID);

            //Reinitialize
            ITagBlock unicodeStringListBlock = multilingualUnicodeStringList[0];
            unicodeStringListBlock.Fields[2].Value = new byte[36];

            //Prepare
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                //Loop
                foreach (string stringId in stringIds)
                {
                    //Add block
                    ITagBlock stringReferenceBlock = ((BaseBlockField)unicodeStringListBlock.Fields[0]).Add(out bool successful);
                    if (successful)
                    {
                        //Setup
                        stringReferenceBlock.Fields[0].Value = new StringValue(stringId);
                        stringReferenceBlock.Fields[1].Value = -1;
                        stringReferenceBlock.Fields[2].Value = -1;
                        stringReferenceBlock.Fields[3].Value = -1;
                        stringReferenceBlock.Fields[4].Value = -1;
                        stringReferenceBlock.Fields[5].Value = -1;
                        stringReferenceBlock.Fields[6].Value = -1;
                        stringReferenceBlock.Fields[7].Value = -1;
                        stringReferenceBlock.Fields[8].Value = -1;
                        stringReferenceBlock.Fields[9].Value = -1;

                        //Get English offset
                        if (strings.English.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[1].Value = (int)ms.Position;
                            writer.Write(Encoding.UTF8.GetBytes(strings.English.First(s => s.ID == stringId).Value));
                            writer.Write((byte)0);
                        }

                        //Get Japanese offset
                        if (strings.Japanese.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[2].Value = (int)ms.Position;
                            writer.Write(Encoding.UTF8.GetBytes(strings.Japanese.First(s => s.ID == stringId).Value));
                            writer.Write((byte)0);
                        }

                        //Get German offset
                        if (strings.German.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[3].Value = (int)ms.Position;
                            writer.Write(Encoding.UTF8.GetBytes(strings.German.First(s => s.ID == stringId).Value));
                            writer.Write((byte)0);
                        }

                        //Get French offset
                        if (strings.French.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[4].Value = (int)ms.Position;
                            writer.Write(Encoding.UTF8.GetBytes(strings.French.First(s => s.ID == stringId).Value));
                            writer.Write((byte)0);
                        }

                        //Get Spanish offset
                        if (strings.Spanish.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[5].Value = (int)ms.Position;
                            writer.Write(Encoding.UTF8.GetBytes(strings.Spanish.First(s => s.ID == stringId).Value));
                            writer.Write((byte)0);
                        }

                        //Get Italian offset
                        if (strings.Spanish.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[5].Value = (int)ms.Position;
                            writer.Write(Encoding.UTF8.GetBytes(strings.Spanish.First(s => s.ID == stringId).Value));
                            writer.Write((byte)0);
                        }

                        //Get Korean offset
                        if (strings.Korean.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[6].Value = (int)ms.Position;
                            writer.Write(Encoding.UTF8.GetBytes(strings.Korean.First(s => s.ID == stringId).Value));
                            writer.Write((byte)0);
                        }

                        //Get Chinese offset
                        if (strings.Chinese.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[7].Value = (int)ms.Position;
                            writer.Write(Encoding.UTF8.GetBytes(strings.Chinese.First(s => s.ID == stringId).Value));
                            writer.Write((byte)0);
                        }

                        //Get Portuguese offset
                        if (strings.Portuguese.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[8].Value = (int)ms.Position;
                            writer.Write(Encoding.UTF8.GetBytes(strings.Portuguese.First(s => s.ID == stringId).Value));
                            writer.Write((byte)0);
                        }
                    }
                }

                //Set
                DataField stringData = (DataField)unicodeStringListBlock.Fields[1];
                stringData.SetBuffer(ms.ToArray());
            }
        }
    }
}
