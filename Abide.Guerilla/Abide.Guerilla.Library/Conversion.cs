using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.Tag;
using Abide.Tag.Guerilla;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Abide.Guerilla.Library
{
    /// <summary>
    /// Converts a tag object to another tag object.
    /// </summary>
    public static class Convert
    {
        private const short C_NullShort = unchecked((short)0xffff);
        private const byte C_NullByte = unchecked((byte)0xff);

        public static ITagGroup ToGuerilla(ITagGroup cacheTagGroup, ITagGroup soundCacheFileGestalt, IndexEntry entry, MapFile map)
        {
            //Prepare
            ITagGroup tagGroup = null;

            //Check
            if (cacheTagGroup == null) throw new ArgumentNullException(nameof(cacheTagGroup));
            if ((tagGroup = Tag.Guerilla.Generated.TagLookup.CreateTagGroup(cacheTagGroup.GroupTag)) != null)
            {
                switch (tagGroup.GroupTag)
                {
                    case HaloTags.snd_:
                        tagGroup = CacheFileSound_ToGuerilla(cacheTagGroup, soundCacheFileGestalt, map);
                        break;
                    case HaloTags.unic:
                        tagGroup = MultilingualUnicode_ToGuerilla(cacheTagGroup, entry.Strings);
                        break;
                    default:
                        if (cacheTagGroup.Count != tagGroup.Count) throw new TagException("Tag block count mismatch. Unable to convert.");
                        for (int i = 0; i < tagGroup.Count; i++)
                            TagBlock_ToGuerilla(tagGroup[i], cacheTagGroup[i], map);
                        break;
                }
            }
            else throw new TagException("Unable to lookup tag group of specified type.", new ArgumentException(nameof(ITagGroup.GroupTag)));

            //Return
            return tagGroup;
        }

        private static void TagBlock_ToGuerilla(ITagBlock guerilla, ITagBlock cache, MapFile map)
        {
            //Prepare
            bool success = false;
            ITagGroup tagGroup = null;

            //Check field count
            if (guerilla.Fields.Count != cache.Fields.Count) throw new TagException("Field count mismatch. Unable to convert.");

            //Loop
            for (int i = 0; i < guerilla.Fields.Count; i++)
            {
                //Check field type
                if (guerilla.Fields[i].Type != cache.Fields[i].Type) continue;

                //Handle type
                switch (guerilla.Fields[i].Type)
                {
                    case Tag.Definition.FieldType.FieldStringId:
                    case Tag.Definition.FieldType.FieldOldStringId:
                        guerilla.Fields[i].Value = new StringValue();
                        if (cache.Fields[i].Value is StringId stringIdValue && map.Strings.Count > stringIdValue.Index && stringIdValue.Index > 0)
                        {
                            string stringId = map.Strings[stringIdValue.Index] ?? string.Empty;
                            guerilla.Fields[i].Value = new StringValue(stringId);
                        }
                        break;
                    case Tag.Definition.FieldType.FieldTagReference:
                        guerilla.Fields[i].Value = new StringValue();
                        if (cache.Fields[i].Value is TagReference tagReferenceValue && !tagReferenceValue.Id.IsNull && map.IndexEntries[tagReferenceValue.Id] != null)
                        {
                            tagGroup = Tag.Guerilla.Generated.TagLookup.CreateTagGroup(map.IndexEntries[tagReferenceValue.Id].Root);
                            string tagPath = $@"{map.IndexEntries[tagReferenceValue.Id].Filename}.{tagGroup.Name}";
                            guerilla.Fields[i].Value = new StringValue(tagPath);
                        }
                        break;

                    case Tag.Definition.FieldType.FieldBlock:
                        BaseBlockField guerillaBlockField = (BaseBlockField)guerilla.Fields[i];
                        BaseBlockField cacheBlockField = (BaseBlockField)cache.Fields[i];
                        for (int j = 0; j < cacheBlockField.BlockList.Count; j++)
                        {
                            ITagBlock guerillaBlock = guerillaBlockField.Add(out success);
                            if (success)
                                TagBlock_ToGuerilla(guerillaBlock, cacheBlockField.BlockList[j], map);
                        }
                        break;
                    case Tag.Definition.FieldType.FieldStruct:
                        BaseStructField guerillaStructField = (BaseStructField)guerilla.Fields[i];
                        BaseStructField cacheStructField = (BaseStructField)cache.Fields[i];
                        TagBlock_ToGuerilla((ITagBlock)guerillaStructField.Value, (ITagBlock)cacheStructField.Value, map);
                        break;
                    case Tag.Definition.FieldType.FieldData:
                        DataField guerillaDataField = (DataField)guerilla.Fields[i];
                        DataField cacheDataField = (DataField)cache.Fields[i];
                        guerillaDataField.SetBuffer(cacheDataField.GetBuffer());
                        break;
                    case Tag.Definition.FieldType.FieldTagIndex:
                        guerilla.Fields[i].Value = new StringValue();
                        if(cache.Fields[i].Value is TagId tagId && !tagId.IsNull && map.IndexEntries[tagId] != null)
                        {
                            tagGroup = Tag.Guerilla.Generated.TagLookup.CreateTagGroup(map.IndexEntries[tagId].Root);
                            string tagPath = $@"{map.IndexEntries[tagId].Filename}.{tagGroup.Name}";
                            guerilla.Fields[i].Value = new StringValue(tagPath);
                        }
                        break;
                    default: guerilla.Fields[i].Value = cache.Fields[i].Value; break;
                }
            }
        }
        
        private static ITagGroup MultilingualUnicode_ToGuerilla(ITagGroup multilingualUnicodeStringList, StringContainer strings)
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

            //Create
            ITagGroup tagGroup = new Tag.Guerilla.Generated.MultilingualUnicodeStringList();
            ITagBlock unicodeStringListBlock = tagGroup[0];

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
                            writer.WriteUTF8NullTerminated(strings.English.First(s => s.ID == stringId).Value);
                        }

                        //Get Japanese offset
                        if (strings.Japanese.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[2].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Japanese.First(s => s.ID == stringId).Value);
                        }

                        //Get German offset
                        if (strings.German.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[3].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.German.First(s => s.ID == stringId).Value);
                        }

                        //Get French offset
                        if (strings.French.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[4].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.French.First(s => s.ID == stringId).Value);
                        }

                        //Get Spanish offset
                        if (strings.Spanish.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[5].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Spanish.First(s => s.ID == stringId).Value);
                        }

                        //Get Italian offset
                        if (strings.Spanish.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[5].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Italian.First(s => s.ID == stringId).Value);
                        }

                        //Get Korean offset
                        if (strings.Korean.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[6].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Korean.First(s => s.ID == stringId).Value);
                        }

                        //Get Chinese offset
                        if (strings.Chinese.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[7].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Chinese.First(s => s.ID == stringId).Value);
                        }

                        //Get Portuguese offset
                        if (strings.Portuguese.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[8].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Portuguese.First(s => s.ID == stringId).Value);
                        }
                    }
                }

                //Set
                DataField stringData = (DataField)unicodeStringListBlock.Fields[1];
                stringData.SetBuffer(ms.ToArray());
            }

            //Return
            return tagGroup;
        }

        private static ITagGroup CacheFileSound_ToGuerilla(ITagGroup cacheFileSound, ITagGroup soundCacheFileGestalt, MapFile map)
        {
            //Prepare
            ITagGroup sound = new Tag.Guerilla.Generated.Sound();
            ITagBlock soundCacheFileGestaltBlock = soundCacheFileGestalt[0];
            ITagBlock cacheFileSoundBlock = cacheFileSound[0];
            ITagBlock soundBlock = sound[0];

            //Get block fields from sound cache file gestalt
            BaseBlockField playbacks = (BaseBlockField)soundCacheFileGestaltBlock.Fields[0];
            BaseBlockField scales = (BaseBlockField)soundCacheFileGestaltBlock.Fields[1];
            BaseBlockField importNames = (BaseBlockField)soundCacheFileGestaltBlock.Fields[2];
            BaseBlockField pitchRangeParameters = (BaseBlockField)soundCacheFileGestaltBlock.Fields[3];
            BaseBlockField pitchRanges = (BaseBlockField)soundCacheFileGestaltBlock.Fields[4];
            BaseBlockField permutations = (BaseBlockField)soundCacheFileGestaltBlock.Fields[5];
            BaseBlockField customPlaybacks = (BaseBlockField)soundCacheFileGestaltBlock.Fields[6];
            BaseBlockField runtimePermutationFlags = (BaseBlockField)soundCacheFileGestaltBlock.Fields[7];
            BaseBlockField chunks = (BaseBlockField)soundCacheFileGestaltBlock.Fields[8];
            BaseBlockField promotions = (BaseBlockField)soundCacheFileGestaltBlock.Fields[9];
            BaseBlockField extraInfos = (BaseBlockField)soundCacheFileGestaltBlock.Fields[10];

            //Convert fields
            soundBlock.Fields[0].Value = (int)(short)cacheFileSoundBlock.Fields[0].Value;   //flags
            soundBlock.Fields[1].Value = cacheFileSoundBlock.Fields[1].Value;   //class
            soundBlock.Fields[2].Value = cacheFileSoundBlock.Fields[2].Value;   //sample rate
            soundBlock.Fields[9].Value = cacheFileSoundBlock.Fields[3].Value;   //encoding
            soundBlock.Fields[10].Value = cacheFileSoundBlock.Fields[4].Value;  //compression

            //Store extra data in the unused padding
            using (MemoryStream ms = new MemoryStream((byte[])soundBlock.Fields[12].Value))
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                //Write max playback time
                writer.Write((int)cacheFileSoundBlock.Fields[12].Value);

                //Write promotion index
                writer.Write((byte)cacheFileSoundBlock.Fields[9].Value);
            }

            //Get playback
            if ((short)cacheFileSoundBlock.Fields[5].Value != C_NullShort)
            {
                ITagBlock playback = playbacks.BlockList[(short)cacheFileSoundBlock.Fields[5].Value];
                soundBlock.Fields[5].Value = playback.Fields[0].Value;
            }

            //Get scale
            if ((byte)cacheFileSoundBlock.Fields[8].Value != C_NullByte)
            {
                ITagBlock scale = scales.BlockList[(byte)cacheFileSoundBlock.Fields[8].Value];
                soundBlock.Fields[6].Value = scale.Fields[0].Value;
            }

            //Get promotion
            if ((byte)cacheFileSoundBlock.Fields[9].Value != C_NullByte)
            {
                ITagBlock promotion = promotions.BlockList[(byte)cacheFileSoundBlock.Fields[9].Value];
                soundBlock.Fields[11].Value = promotion.Fields[0].Value;
            }

            //Get custom playback
            if ((byte)cacheFileSoundBlock.Fields[10].Value != C_NullByte)
            {
                BaseBlockField platformParameters = (BaseBlockField)soundBlock.Fields[14];
                ITagBlock soundPlatformPlaybackBlock = platformParameters.Add(out bool success);
                if (success)
                {
                    ITagBlock customPlayback = customPlaybacks.BlockList[(byte)cacheFileSoundBlock.Fields[10].Value];
                    soundPlatformPlaybackBlock.Fields[0].Value = customPlayback.Fields[0].Value;
                }
            }

            //Get extra infos
            if ((short)cacheFileSoundBlock.Fields[11].Value != C_NullShort)
            {
                BaseBlockField unnamed = (BaseBlockField)soundBlock.Fields[15];
                ITagBlock extraInfo = unnamed.Add(out bool success);
                if (success)
                {
                    //Get encoded permutation section
                    ITagBlock soundGestaltExtraInfo = extraInfos.BlockList[(short)cacheFileSoundBlock.Fields[11].Value];
                    foreach (ITagBlock section in ((BaseBlockField)soundGestaltExtraInfo.Fields[0]).BlockList)
                        ((BaseBlockField)extraInfo.Fields[1]).BlockList.Add(section);

                    //Get geometry block info
                    TagBlock_ToGuerilla((ITagBlock)extraInfo.Fields[2].Value, (ITagBlock)soundGestaltExtraInfo.Fields[1].Value, map);
                    ((ITagBlock)extraInfo.Fields[2].Value).Fields[6].Value = new StringValue();
                }
            }

            //Get pitch ranges
            if ((short)cacheFileSoundBlock.Fields[6].Value != C_NullShort)
            {
                short pitchRangeIndex = (short)cacheFileSoundBlock.Fields[6].Value;
                byte pitchRangeCount = (byte)cacheFileSoundBlock.Fields[7].Value;
                BaseBlockField soundPitchRanges = (BaseBlockField)soundBlock.Fields[13];
                for (int i = 0; i < pitchRangeCount; i++)
                {
                    ITagBlock soundPitchRange = soundPitchRanges.Add(out bool success);
                    if (success)
                    {
                        ITagBlock pitchRange = pitchRanges.BlockList[pitchRangeIndex + i];
                        ITagBlock importName = importNames.BlockList[(short)pitchRange.Fields[0].Value];
                        ITagBlock pitchRangeParameter = pitchRangeParameters.BlockList[(short)pitchRange.Fields[1].Value];

                        //Setup sound pitch range
                        soundPitchRange.Fields[0].Value = new StringValue(map.Strings[((StringId)importName.Fields[0].Value).Index]);
                        soundPitchRange.Fields[2].Value = (short)pitchRangeParameter.Fields[0].Value;
                        soundPitchRange.Fields[4].Value = (ShortBounds)pitchRangeParameter.Fields[1].Value;
                        soundPitchRange.Fields[5].Value = (ShortBounds)pitchRangeParameter.Fields[2].Value;

                        //Get sound permutations
                        if ((short)pitchRange.Fields[4].Value != C_NullShort)
                        {
                            short permutationIndex = (short)pitchRange.Fields[4].Value;
                            short permutationCount = (short)pitchRange.Fields[5].Value;
                            BaseBlockField soundPermutations = (BaseBlockField)soundPitchRange.Fields[7];
                            for (int j = 0; j < permutationCount; j++)
                            {
                                ITagBlock soundPermutation = soundPermutations.Add(out success);
                                if (success)
                                {
                                    ITagBlock permutation = permutations.BlockList[permutationIndex + j];
                                    importName = importNames.BlockList[(short)permutation.Fields[0].Value];
                                    soundPermutation.Fields[0].Value = new StringValue(map.Strings[((StringId)importName.Fields[0].Value).Index]);
                                    soundPermutation.Fields[1].Value = (ushort)(short)permutation.Fields[1].Value / 65535f;
                                    soundPermutation.Fields[2].Value = (byte)permutation.Fields[2].Value / 255f;
                                    soundPermutation.Fields[3].Value = (int)permutation.Fields[5].Value;
                                    soundPermutation.Fields[4].Value = (short)(byte)permutation.Fields[3].Value;
                                    soundPermutation.Fields[5].Value = (short)permutation.Fields[4].Value;

                                    //Copy chunks
                                    if ((short)permutation.Fields[6].Value != C_NullShort)
                                    {
                                        short chunkIndex = (short)permutation.Fields[6].Value;
                                        short chunkCount = (short)permutation.Fields[7].Value;
                                        BaseBlockField soundChunks = (BaseBlockField)soundPermutation.Fields[6];
                                        for (int k = 0; k < chunkCount; k++)
                                            soundChunks.BlockList.Add(chunks.BlockList[chunkIndex + k]);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Return
            return sound;
        }
    }
}
