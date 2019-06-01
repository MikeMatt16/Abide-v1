﻿using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
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
        private const short C_NullShort = unchecked((short)0xffff);
        private const byte C_NullByte = unchecked(0xff);

        public static ITagGroup ToGuerilla(ITagGroup cacheTagGroup, ITagGroup soundCacheFileGestalt, IndexEntry entry, MapFile map)
        {
            //Prepare
            ITagGroup tagGroup = null;

            //Check
            if (cacheTagGroup == null) throw new ArgumentNullException(nameof(cacheTagGroup));
            if ((tagGroup = Abide.Tag.Guerilla.Generated.TagLookup.CreateTagGroup(cacheTagGroup.GroupTag)) != null)
            {
                switch (tagGroup.Name)
                {
                    case "sound":
                        tagGroup = CacheFileSound_ToGuerilla(cacheTagGroup, soundCacheFileGestalt, map);
                        break;
                    case "multilingual_unicode_string_list":
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
                    case FieldType.FieldStringId:
                    case FieldType.FieldOldStringId:
                        if (cache.Fields[i].Value is StringId stringIdValue && stringIdValue.Index < map.Strings.Count)
                        {
                            string stringId = map.Strings[stringIdValue.Index] ?? string.Empty;
                            guerilla.Fields[i].Value = stringId;
                        }
                        break;
                    case FieldType.FieldTagReference:
                        if (cache.Fields[i].Value is TagReference tagReferenceValue)
                        {
                            ((TagReferenceField)guerilla.Fields[i]).GroupTag = tagReferenceValue.Tag;
                            if (map.IndexEntries[tagReferenceValue.Id] != null)
                            {
                                tagGroup = Abide.Tag.Guerilla.Generated.TagLookup.CreateTagGroup(map.IndexEntries[tagReferenceValue.Id].Root);
                                string tagPath = $@"{map.IndexEntries[tagReferenceValue.Id].Filename}.{tagGroup.Name}";
                                guerilla.Fields[i].Value = tagPath;
                            }
                        }
                        break;

                    case FieldType.FieldBlock:
                        BlockField guerillaBlockField = (BlockField)guerilla.Fields[i];
                        BlockField cacheBlockField = (BlockField)cache.Fields[i];
                        for (int j = 0; j < cacheBlockField.BlockList.Count; j++)
                        {
                            ITagBlock guerillaBlock = guerillaBlockField.Add(out success);
                            if (success)
                                TagBlock_ToGuerilla(guerillaBlock, cacheBlockField.BlockList[j], map);
                        }
                        break;
                    case FieldType.FieldStruct:
                        StructField guerillaStructField = (StructField)guerilla.Fields[i];
                        StructField cacheStructField = (StructField)cache.Fields[i];
                        TagBlock_ToGuerilla((ITagBlock)guerillaStructField.Value, (ITagBlock)cacheStructField.Value, map);
                        break;
                    case FieldType.FieldData:
                        DataField guerillaDataField = (DataField)guerilla.Fields[i];
                        DataField cacheDataField = (DataField)cache.Fields[i];
                        guerillaDataField.SetBuffer(cacheDataField.GetBuffer());
                        break;
                    case FieldType.FieldTagIndex:
                        if(cache.Fields[i].Value is TagId tagId && map.IndexEntries[tagId] != null)
                        {
                            tagGroup = Abide.Tag.Guerilla.Generated.TagLookup.CreateTagGroup(map.IndexEntries[tagId].Root);
                            string tagPath = $@"{map.IndexEntries[tagId].Filename}.{tagGroup.Name}";
                            guerilla.Fields[i].Value = tagPath;
                        }
                        break;
                    default: guerilla.Fields[i].Value = cache.Fields[i].Value; break;
                }
            }
        }

        private static ITagGroup Bitmap_ToGuerilla(ITagGroup cacheBitmap, RawList bitmapData, MapFile map)
        {
            //Prepare
            ITagGroup bitmap = new Abide.Tag.Guerilla.Generated.Bitmap();
            ITagBlock cacheBitmapBlock = cacheBitmap[0];
            ITagBlock bitmapBlock = bitmap[0];

            //Create stream
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bitmapDataWriter = new BinaryWriter(ms))
            {
                //Prepare
                BlockField cacheSequences = (BlockField)cacheBitmapBlock.Fields[28];
                BlockField cacheBitmaps = (BlockField)cacheBitmapBlock.Fields[29];
                BlockField sequences = (BlockField)bitmapBlock.Fields[28];
                BlockField bitmaps = (BlockField)bitmapBlock.Fields[29];

                //Convert fields
                for (int i = 0; i < 28; i++)
                {
                    if (bitmapBlock.Fields[i].Type != FieldType.FieldData)
                        bitmapBlock.Fields[i].Value = cacheBitmapBlock.Fields[i].Value;
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
                    if(success)
                    {
                        //Convert
                        TagBlock_ToGuerilla(bitmapDataBlock, cacheBitmapDataBlock, map);
                    }
                }
            }

            //Return
            return bitmap;
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
            ITagGroup tagGroup = new Abide.Tag.Guerilla.Generated.MultilingualUnicodeStringList();
            ITagBlock unicodeStringListBlock = tagGroup[0];

            //Prepare
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                //Loop
                foreach (string stringId in stringIds)
                {
                    //Add block
                    ITagBlock stringReferenceBlock = ((BlockField)unicodeStringListBlock.Fields[0]).Add(out bool successful);
                    if (successful)
                    {
                        //Setup
                        stringReferenceBlock.Fields[0].Value = stringId;
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
            ITagGroup sound = new Abide.Tag.Guerilla.Generated.Sound();
            ITagBlock soundCacheFileGestaltBlock = soundCacheFileGestalt[0];
            ITagBlock cacheFileSoundBlock = cacheFileSound[0];
            ITagBlock soundBlock = sound[0];

            //Get block fields from sound cache file gestalt
            BlockField playbacks = (BlockField)soundCacheFileGestaltBlock.Fields[0];
            BlockField scales = (BlockField)soundCacheFileGestaltBlock.Fields[1];
            BlockField importNames = (BlockField)soundCacheFileGestaltBlock.Fields[2];
            BlockField pitchRangeParameters = (BlockField)soundCacheFileGestaltBlock.Fields[3];
            BlockField pitchRanges = (BlockField)soundCacheFileGestaltBlock.Fields[4];
            BlockField permutations = (BlockField)soundCacheFileGestaltBlock.Fields[5];
            BlockField customPlaybacks = (BlockField)soundCacheFileGestaltBlock.Fields[6];
            BlockField runtimePermutationFlags = (BlockField)soundCacheFileGestaltBlock.Fields[7];
            BlockField chunks = (BlockField)soundCacheFileGestaltBlock.Fields[8];
            BlockField promotions = (BlockField)soundCacheFileGestaltBlock.Fields[9];
            BlockField extraInfos = (BlockField)soundCacheFileGestaltBlock.Fields[10];

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
                BlockField platformParameters = (BlockField)soundBlock.Fields[14];
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
                BlockField unnamed = (BlockField)soundBlock.Fields[15];
                ITagBlock extraInfo = unnamed.Add(out bool success);
                if (success)
                {
                    //Get encoded permutation section
                    ITagBlock soundGestaltExtraInfo = extraInfos.BlockList[(short)cacheFileSoundBlock.Fields[11].Value];
                    foreach (ITagBlock section in ((BlockField)soundGestaltExtraInfo.Fields[0]).BlockList)
                        ((BlockField)extraInfo.Fields[1]).BlockList.Add(section);

                    //Get geometry block info
                    TagBlock_ToGuerilla((ITagBlock)extraInfo.Fields[2].Value, (ITagBlock)soundGestaltExtraInfo.Fields[1].Value, map);
                    ((ITagBlock)extraInfo.Fields[2].Value).Fields[6].Value = string.Empty;
                }
            }

            //Get pitch ranges
            if ((short)cacheFileSoundBlock.Fields[6].Value != C_NullShort)
            {
                short pitchRangeIndex = (short)cacheFileSoundBlock.Fields[6].Value;
                byte pitchRangeCount = (byte)cacheFileSoundBlock.Fields[7].Value;
                BlockField soundPitchRanges = (BlockField)soundBlock.Fields[13];
                for (int i = 0; i < pitchRangeCount; i++)
                {
                    ITagBlock soundPitchRange = soundPitchRanges.Add(out bool success);
                    if (success)
                    {
                        ITagBlock pitchRange = pitchRanges.BlockList[pitchRangeIndex + i];
                        ITagBlock importName = importNames.BlockList[(short)pitchRange.Fields[0].Value];
                        ITagBlock pitchRangeParameter = pitchRangeParameters.BlockList[(short)pitchRange.Fields[1].Value];

                        //Setup sound pitch range
                        soundPitchRange.Fields[0].Value = map.Strings[((StringId)importName.Fields[0].Value).Index];
                        soundPitchRange.Fields[2].Value = (short)pitchRangeParameter.Fields[0].Value;
                        soundPitchRange.Fields[4].Value = (ShortBounds)pitchRangeParameter.Fields[1].Value;
                        soundPitchRange.Fields[5].Value = (ShortBounds)pitchRangeParameter.Fields[2].Value;

                        //Get sound permutations
                        if ((short)pitchRange.Fields[4].Value != C_NullShort)
                        {
                            short permutationIndex = (short)pitchRange.Fields[4].Value;
                            short permutationCount = (short)pitchRange.Fields[5].Value;
                            BlockField soundPermutations = (BlockField)soundPitchRange.Fields[7];
                            for (int j = 0; j < permutationCount; j++)
                            {
                                ITagBlock soundPermutation = soundPermutations.Add(out success);
                                if (success)
                                {
                                    ITagBlock permutation = permutations.BlockList[permutationIndex + j];
                                    importName = importNames.BlockList[(short)permutation.Fields[0].Value];
                                    soundPermutation.Fields[0].Value = map.Strings[((StringId)importName.Fields[0].Value).Index];
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
                                        BlockField soundChunks = (BlockField)soundPermutation.Fields[6];
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
