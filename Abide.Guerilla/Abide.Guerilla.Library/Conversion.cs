using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2;
using Abide.HaloLibrary.Halo2.Retail;
using Abide.Tag;
using Abide.Tag.Definition;
using Abide.Tag.Guerilla;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Abide.Guerilla.Library
{
    /// <summary>
    /// Converts a tag object to another tag object.
    /// </summary>
    public static class Convert
    {
        private const short nullShortIndex = -1;
        private const byte nullByteIndex = 255;

        public static Group ToGuerilla(ITagGroup cacheTagGroup, ITagGroup soundCacheFileGestalt, IndexEntry entry, HaloMap map)
        {
            //Prepare
            Group tagGroup = null;

            //Check
            if (cacheTagGroup == null) throw new ArgumentNullException(nameof(cacheTagGroup));
            if ((tagGroup = Abide.Tag.Guerilla.Generated.TagLookup.CreateTagGroup(cacheTagGroup.GroupTag)) != null)
            {
                switch (tagGroup.GroupName)
                {
                    case "sound":
                        tagGroup = CacheFileSound_ToGuerilla(cacheTagGroup, soundCacheFileGestalt, map);
                        break;
                    case "multilingual_unicode_string_list":
                        tagGroup = MultilingualUnicode_ToGuerilla(cacheTagGroup, entry.Strings);
                        break;
                    default:
                        if (cacheTagGroup.TagBlockCount != tagGroup.TagBlockCount) throw new TagException("Tag block count mismatch. Unable to convert.");
                        for (int i = 0; i < tagGroup.TagBlockCount; i++)
                            TagBlock_ToGuerilla(tagGroup.TagBlocks[i], cacheTagGroup[i], map);
                        break;
                }
            }
            else throw new TagException("Unable to lookup tag group of specified type.", new ArgumentException(nameof(ITagGroup.GroupTag)));

            //Return
            return tagGroup;
        }

        private static void TagBlock_ToGuerilla(ITagBlock guerilla, ITagBlock cache, HaloMap map)
        {
            //Prepare
            bool success = false;
            ITagGroup tagGroup = null;

            //Check field count
            if (guerilla.FieldCount != cache.FieldCount) throw new TagException("Field count mismatch. Unable to convert.");

            //Loop
            for (int i = 0; i < guerilla.FieldCount; i++)
            {
                //Check field type
                if (guerilla[i].Type != cache[i].Type) continue;

                //Handle type
                switch (guerilla[i].Type)
                {
                    case FieldType.FieldStringId:
                    case FieldType.FieldOldStringId:
                        if (cache[i].Value is StringId stringIdValue && stringIdValue.Index < map.Strings.Count)
                        {
                            string stringId = map.Strings[stringIdValue.Index] ?? string.Empty;
                            guerilla[i].Value = stringId;
                        }
                        break;
                    case FieldType.FieldTagReference:
                        if (cache[i].Value is TagReference tagReferenceValue &&
                            map.IndexEntries[tagReferenceValue.Id] != null)
                        {
                            tagGroup = Abide.Tag.Guerilla.Generated.TagLookup.CreateTagGroup(map.IndexEntries[tagReferenceValue.Id].Root);
                            string tagPath = $@"{map.IndexEntries[tagReferenceValue.Id].Filename}.{tagGroup.GroupName}";
                            guerilla[i].Value = tagPath;
                        }
                        break;

                    case FieldType.FieldBlock:
                        BlockField guerillaBlockField = (BlockField)guerilla[i];
                        BlockField cacheBlockField = (BlockField)cache[i];
                        for (int j = 0; j < cacheBlockField.BlockList.Count; j++)
                        {
                            ITagBlock guerillaBlock = guerillaBlockField.Add(out success);
                            if (success)
                                TagBlock_ToGuerilla(guerillaBlock, cacheBlockField.BlockList[j], map);
                        }
                        break;
                    case FieldType.FieldStruct:
                        StructField guerillaStructField = (StructField)guerilla[i];
                        StructField cacheStructField = (StructField)cache[i];
                        TagBlock_ToGuerilla((ITagBlock)guerillaStructField.Value, (ITagBlock)cacheStructField.Value, map);
                        break;
                    case FieldType.FieldData:
                        DataField guerillaDataField = (DataField)guerilla[i];
                        DataField cacheDataField = (DataField)cache[i];
                        guerillaDataField.SetBuffer(cacheDataField.GetBuffer());
                        break;
                    case FieldType.FieldTagIndex:
                        if (cache[i].Value is TagId tagId && map.IndexEntries[tagId] != null)
                        {
                            tagGroup = Abide.Tag.Guerilla.Generated.TagLookup.CreateTagGroup(map.IndexEntries[tagId].Root);
                            string tagPath = $@"{map.IndexEntries[tagId].Filename}.{tagGroup.GroupName}";
                            guerilla[i].Value = tagPath;
                        }
                        break;
                    default: guerilla[i].Value = cache[i].Value; break;
                }
            }
        }

        private static Group Bitmap_ToGuerilla(ITagGroup cacheBitmap, TagResourceContainer data, HaloMap map)
        {
            //Prepare
            Group bitmap = new Abide.Tag.Guerilla.Generated.Bitmap();
            ITagBlock cacheBitmapBlock = cacheBitmap[0];
            ITagBlock bitmapBlock = bitmap.TagBlocks[0];

            //Create stream
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bitmapDataWriter = new BinaryWriter(ms))
            {
                //Prepare
                BlockField cacheSequences = (BlockField)cacheBitmapBlock[28];
                BlockField cacheBitmaps = (BlockField)cacheBitmapBlock[29];
                BlockField sequences = (BlockField)bitmapBlock[28];
                BlockField bitmaps = (BlockField)bitmapBlock[29];

                //Convert fields
                for (int i = 0; i < 28; i++)
                {
                    if (bitmapBlock[i].Type != FieldType.FieldData)
                        bitmapBlock[i].Value = cacheBitmapBlock[i].Value;
                }

                //Copy sequence tag blocks
                foreach (ITagBlock cacheBitmapSequenceBlock in cacheSequences.BlockList)
                {
                    ITagBlock bitmapSequenceBlock = sequences.Add(out bool success);
                    if (success) TagBlock_ToGuerilla(bitmapSequenceBlock, cacheBitmapSequenceBlock, map);
                }

                //Copy bitmap data tag blocks
                foreach (ITagBlock cacheBitmapDataBlock in cacheBitmaps.BlockList)
                {
                    ITagBlock bitmapDataBlock = bitmaps.Add(out bool success);
                    if (success)
                    {
                        //Convert
                        TagBlock_ToGuerilla(bitmapDataBlock, cacheBitmapDataBlock, map);
                    }
                }
            }

            //Return
            return bitmap;
        }

        private static Group MultilingualUnicode_ToGuerilla(ITagGroup multilingualUnicodeStringList, StringContainer strings)
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
            Group tagGroup = new Abide.Tag.Guerilla.Generated.MultilingualUnicodeStringList();
            ITagBlock unicodeStringListBlock = tagGroup.TagBlocks[0];

            //Prepare
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                //Loop
                foreach (string stringId in stringIds)
                {
                    //Add block
                    ITagBlock stringReferenceBlock = ((BlockField)unicodeStringListBlock[0]).Add(out bool successful);
                    if (successful)
                    {
                        //Setup
                        stringReferenceBlock[0].Value = stringId;
                        stringReferenceBlock[1].Value = -1;
                        stringReferenceBlock[2].Value = -1;
                        stringReferenceBlock[3].Value = -1;
                        stringReferenceBlock[4].Value = -1;
                        stringReferenceBlock[5].Value = -1;
                        stringReferenceBlock[6].Value = -1;
                        stringReferenceBlock[7].Value = -1;
                        stringReferenceBlock[8].Value = -1;
                        stringReferenceBlock[9].Value = -1;

                        //Get English offset
                        if (strings.English.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock[1].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.English.First(s => s.ID == stringId).Value);
                        }

                        //Get Japanese offset
                        if (strings.Japanese.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock[2].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Japanese.First(s => s.ID == stringId).Value);
                        }

                        //Get German offset
                        if (strings.German.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock[3].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.German.First(s => s.ID == stringId).Value);
                        }

                        //Get French offset
                        if (strings.French.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock[4].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.French.First(s => s.ID == stringId).Value);
                        }

                        //Get Spanish offset
                        if (strings.Spanish.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock[5].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Spanish.First(s => s.ID == stringId).Value);
                        }

                        //Get Italian offset
                        if (strings.Spanish.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock[5].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Italian.First(s => s.ID == stringId).Value);
                        }

                        //Get Korean offset
                        if (strings.Korean.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock[6].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Korean.First(s => s.ID == stringId).Value);
                        }

                        //Get Chinese offset
                        if (strings.Chinese.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock[7].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Chinese.First(s => s.ID == stringId).Value);
                        }

                        //Get Portuguese offset
                        if (strings.Portuguese.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock[8].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Portuguese.First(s => s.ID == stringId).Value);
                        }
                    }
                }

                //Set
                DataField stringData = (DataField)unicodeStringListBlock[1];
                stringData.SetBuffer(ms.ToArray());
            }

            //Return
            return tagGroup;
        }

        private static Group CacheFileSound_ToGuerilla(ITagGroup cacheFileSound, ITagGroup soundCacheFileGestalt, HaloMap map)
        {
            //Prepare
            Group sound = new Abide.Tag.Guerilla.Generated.Sound();
            ITagBlock soundCacheFileGestaltBlock = soundCacheFileGestalt[0];
            ITagBlock cacheFileSoundBlock = cacheFileSound[0];
            ITagBlock soundBlock = sound.TagBlocks[0];

            //Get block fields from sound cache file gestalt
            BlockField playbacks = (BlockField)soundCacheFileGestaltBlock[0];
            BlockField scales = (BlockField)soundCacheFileGestaltBlock[1];
            BlockField importNames = (BlockField)soundCacheFileGestaltBlock[2];
            BlockField pitchRangeParameters = (BlockField)soundCacheFileGestaltBlock[3];
            BlockField pitchRanges = (BlockField)soundCacheFileGestaltBlock[4];
            BlockField permutations = (BlockField)soundCacheFileGestaltBlock[5];
            BlockField customPlaybacks = (BlockField)soundCacheFileGestaltBlock[6];
            BlockField runtimePermutationFlags = (BlockField)soundCacheFileGestaltBlock[7];
            BlockField chunks = (BlockField)soundCacheFileGestaltBlock[8];
            BlockField promotions = (BlockField)soundCacheFileGestaltBlock[9];
            BlockField extraInfos = (BlockField)soundCacheFileGestaltBlock[10];

            //Convert fields
            soundBlock[0].Value = (int)(short)cacheFileSoundBlock[0].Value;   //flags
            soundBlock[1].Value = cacheFileSoundBlock[1].Value;   //class
            soundBlock[2].Value = cacheFileSoundBlock[2].Value;   //sample rate
            soundBlock[9].Value = cacheFileSoundBlock[3].Value;   //encoding
            soundBlock[10].Value = cacheFileSoundBlock[4].Value;  //compression

            //Store extra data in the unused padding
            using (MemoryStream ms = new MemoryStream((byte[])soundBlock[12].Value))
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                //Write max playback time
                writer.Write((int)cacheFileSoundBlock[12].Value);

                //Write promotion index
                writer.Write((byte)cacheFileSoundBlock[9].Value);
            }

            //Get playback
            if ((short)cacheFileSoundBlock[5].Value != nullShortIndex)
            {
                ITagBlock playback = playbacks.BlockList[(short)cacheFileSoundBlock[5].Value];
                soundBlock[5].Value = playback[0].Value;
            }

            //Get scale
            if ((byte)cacheFileSoundBlock[8].Value != nullByteIndex)
            {
                ITagBlock scale = scales.BlockList[(byte)cacheFileSoundBlock[8].Value];
                soundBlock[6].Value = scale[0].Value;
            }

            //Get promotion
            if ((byte)cacheFileSoundBlock[9].Value != nullByteIndex)
            {
                ITagBlock promotion = promotions.BlockList[(byte)cacheFileSoundBlock[9].Value];
                soundBlock[11].Value = promotion[0].Value;
            }

            //Get custom playback
            if ((byte)cacheFileSoundBlock[10].Value != nullByteIndex)
            {
                BlockField platformParameters = (BlockField)soundBlock[14];
                ITagBlock soundPlatformPlaybackBlock = platformParameters.Add(out bool success);
                if (success)
                {
                    ITagBlock customPlayback = customPlaybacks.BlockList[(byte)cacheFileSoundBlock[10].Value];
                    soundPlatformPlaybackBlock[0].Value = customPlayback[0].Value;
                }
            }

            //Get extra infos
            if ((short)cacheFileSoundBlock[11].Value != nullShortIndex)
            {
                BlockField unnamed = (BlockField)soundBlock[15];
                ITagBlock extraInfo = unnamed.Add(out bool success);
                if (success)
                {
                    //Get encoded permutation section
                    ITagBlock soundGestaltExtraInfo = extraInfos.BlockList[(short)cacheFileSoundBlock[11].Value];
                    foreach (Block section in ((BlockField)soundGestaltExtraInfo[0]).BlockList)
                        ((BlockField)extraInfo[1]).BlockList.Add(section);

                    //Get geometry block info
                    TagBlock_ToGuerilla((ITagBlock)extraInfo[2].Value, (ITagBlock)soundGestaltExtraInfo[1].Value, map);
                    ((ITagBlock)extraInfo[2].Value)[6].Value = string.Empty;
                }
            }

            //Get pitch ranges
            if ((short)cacheFileSoundBlock[6].Value != nullShortIndex)
            {
                short pitchRangeIndex = (short)cacheFileSoundBlock[6].Value;
                byte pitchRangeCount = (byte)cacheFileSoundBlock[7].Value;
                BlockField soundPitchRanges = (BlockField)soundBlock[13];
                for (int i = 0; i < pitchRangeCount; i++)
                {
                    ITagBlock soundPitchRange = soundPitchRanges.Add(out bool success);
                    if (success)
                    {
                        ITagBlock pitchRange = pitchRanges.BlockList[pitchRangeIndex + i];
                        ITagBlock importName = importNames.BlockList[(short)pitchRange[0].Value];
                        ITagBlock pitchRangeParameter = pitchRangeParameters.BlockList[(short)pitchRange[1].Value];

                        //Setup sound pitch range
                        soundPitchRange[0].Value = map.Strings[((StringId)importName[0].Value).Index];
                        soundPitchRange[2].Value = (short)pitchRangeParameter[0].Value;
                        soundPitchRange[4].Value = (ShortBounds)pitchRangeParameter[1].Value;
                        soundPitchRange[5].Value = (ShortBounds)pitchRangeParameter[2].Value;

                        //Get sound permutations
                        if ((short)pitchRange[4].Value != nullShortIndex)
                        {
                            short permutationIndex = (short)pitchRange[4].Value;
                            short permutationCount = (short)pitchRange[5].Value;
                            BlockField soundPermutations = (BlockField)soundPitchRange[7];
                            for (int j = 0; j < permutationCount; j++)
                            {
                                ITagBlock soundPermutation = soundPermutations.Add(out success);
                                if (success)
                                {
                                    ITagBlock permutation = permutations.BlockList[permutationIndex + j];
                                    importName = importNames.BlockList[(short)permutation[0].Value];
                                    soundPermutation[0].Value = map.Strings[((StringId)importName[0].Value).Index];
                                    soundPermutation[1].Value = (ushort)(short)permutation[1].Value / 65535f;
                                    soundPermutation[2].Value = (byte)permutation[2].Value / 255f;
                                    soundPermutation[3].Value = (int)permutation[5].Value;
                                    soundPermutation[4].Value = (short)(byte)permutation[3].Value;
                                    soundPermutation[5].Value = (short)permutation[4].Value;

                                    //Copy chunks
                                    if ((short)permutation[6].Value != nullShortIndex)
                                    {
                                        short chunkIndex = (short)permutation[6].Value;
                                        short chunkCount = (short)permutation[7].Value;
                                        BlockField soundChunks = (BlockField)soundPermutation[6];
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
