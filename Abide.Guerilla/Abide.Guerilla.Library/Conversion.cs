using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2.Retail;
using Abide.Tag;
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

        public static Group ToGuerilla(Group cacheTagGroup, Group soundCacheFileGestalt, IndexEntry entry, HaloMap map)
        {
            if (cacheTagGroup == null) throw new ArgumentNullException(nameof(cacheTagGroup));
            Group tagGroup;

            if ((tagGroup = Abide.Tag.Guerilla.Generated.TagLookup.CreateTagGroup(cacheTagGroup.Tag)) != null)
            {
                switch (tagGroup.Name)
                {
                    case "sound":
                        tagGroup = CacheFileSound_ToGuerilla(cacheTagGroup, soundCacheFileGestalt, map);
                        break;

                    case "multilingual_unicode_string_list":
                        tagGroup = MultilingualUnicode_ToGuerilla(entry.Strings);
                        break;

                    default:
                        if (cacheTagGroup.TagBlockCount != tagGroup.TagBlockCount)
                        {
                            throw new TagException("Tag block count mismatch. Unable to convert.");
                        }
                        for (int i = 0; i < tagGroup.TagBlockCount; i++)
                        {
                            TagBlock_ToGuerilla(tagGroup.TagBlocks[i], cacheTagGroup.TagBlocks[i], map);
                        }
                        break;
                }
            }
            else
            {
                throw new TagException("Unable to lookup tag group of specified type.", new ArgumentException(nameof(ITagGroup.Tag)));
            }

            return tagGroup;
        }

        private static void TagBlock_ToGuerilla(Block guerilla, Block cache, HaloMap map)
        {
            if (guerilla.Name != cache.Name)
            {
                return;
            }

            string tagPath;
            Group tagGroup;

            for (int i = 0; i < guerilla.FieldCount; i++)
            {
                if (guerilla.Fields[i].Type != cache.Fields[i].Type)
                {
                    continue;
                }

                switch (cache.Fields[i])
                {
                    case Abide.Tag.Cache.BaseStringIdField stringField:
                        if (map.Strings.Count > stringField.Id.Index)
                        {
                            string stringId = map.Strings[stringField.Id.Index] ?? string.Empty;
                            guerilla.Fields[i].Value = stringId;
                        }
                        break;

                    case Abide.Tag.Cache.TagReferenceField tagReferenceField:
                        if (map.IndexEntries.ContainsId(tagReferenceField.Reference.Id))
                        {
                            tagGroup = Abide.Tag.Guerilla.Generated.TagLookup.CreateTagGroup(map.IndexEntries[tagReferenceField.Reference.Id].Root);
                            tagPath = $@"{map.IndexEntries[tagReferenceField.Reference.Id].Filename}.{tagGroup.Name}";
                            guerilla.Fields[i].Value = tagPath;
                        }
                        break;

                    case Abide.Tag.Cache.TagIndexField tagIndexField:
                        if (map.IndexEntries.ContainsId(tagIndexField.Id))
                        {
                            tagGroup = Abide.Tag.Guerilla.Generated.TagLookup.CreateTagGroup(map.IndexEntries[tagIndexField.Id].Root);
                            tagPath = $@"{map.IndexEntries[tagIndexField.Id].Filename}.{tagGroup.Name}";
                            guerilla.Fields[i].Value = tagPath;
                        }
                        break;

                    case BlockField cacheBlockField:
                        BlockField guerillaBlockField = (BlockField)guerilla.Fields[i];
                        for (int j = 0; j < cacheBlockField.BlockList.Count; j++)
                        {
                            if (guerillaBlockField.Add(out Block guerillaBlock))
                            {
                                TagBlock_ToGuerilla(guerillaBlock, cacheBlockField.BlockList[j], map);
                            }
                        }
                        break;

                    case StructField cacheStructField:
                        StructField guerillaStructField = (StructField)guerilla.Fields[i];
                        TagBlock_ToGuerilla(guerillaStructField.Block, cacheStructField.Block, map);
                        break;

                    case DataField cacheDataField:
                        DataField guerillaDataField = (DataField)guerilla.Fields[i];
                        guerillaDataField.SetBuffer(cacheDataField.GetBuffer());
                        break;

                    default: guerilla.Fields[i].Value = cache.Fields[i].Value; break;
                }
            }
        }

        private static Group Bitmap_ToGuerilla(Group cacheBitmap, TagResourceContainer data, HaloMap map)
        {
            Group bitmap = new Abide.Tag.Guerilla.Generated.Bitmap();
            Block cacheBitmapBlock = cacheBitmap.TagBlocks[0];
            Block bitmapBlock = bitmap.TagBlocks[0];

            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bitmapDataWriter = new BinaryWriter(ms))
            {
                BlockField cacheSequences = (BlockField)cacheBitmapBlock.Fields[28];
                BlockField cacheBitmaps = (BlockField)cacheBitmapBlock.Fields[29];
                BlockField sequences = (BlockField)bitmapBlock.Fields[28];
                BlockField bitmaps = (BlockField)bitmapBlock.Fields[29];

                for (int i = 0; i < 28; i++)
                {
                    if (bitmapBlock.Fields[i].Type != FieldType.FieldData)
                    {
                        bitmapBlock.Fields[i].Value = cacheBitmapBlock.Fields[i].Value;
                    }
                }

                foreach (Block cacheBitmapSequenceBlock in cacheSequences.BlockList)
                {
                    if (sequences.Add(out Block bitmapSequenceBlock))
                    {
                        TagBlock_ToGuerilla(bitmapSequenceBlock, cacheBitmapSequenceBlock, map);
                    }
                }

                foreach (Block cacheBitmapDataBlock in cacheBitmaps.BlockList)
                {
                    if (bitmaps.Add(out Block bitmapDataBlock))
                    {
                        TagBlock_ToGuerilla(bitmapDataBlock, cacheBitmapDataBlock, map);
                    }
                }
            }

            return bitmap;
        }

        private static Group MultilingualUnicode_ToGuerilla(StringContainer strings)
        {
            List<string> stringIds = new List<string>();
            Group tagGroup = new Abide.Tag.Guerilla.Generated.MultilingualUnicodeStringList();
            ITagBlock unicodeStringListBlock = tagGroup.TagBlocks[0];

            foreach (var str in strings.English)
            {
                if (!stringIds.Contains(str.ID))
                    stringIds.Add(str.ID);
            }
            foreach (var str in strings.Japanese)
            {
                if (!stringIds.Contains(str.ID))
                    stringIds.Add(str.ID);
            }
            foreach (var str in strings.German)
            {
                if (!stringIds.Contains(str.ID))
                    stringIds.Add(str.ID);
            }
            foreach (var str in strings.French)
            {
                if (!stringIds.Contains(str.ID))
                    stringIds.Add(str.ID);
            }
            foreach (var str in strings.Spanish)
            {
                if (!stringIds.Contains(str.ID))
                    stringIds.Add(str.ID);
            }
            foreach (var str in strings.Italian)
            {
                if (!stringIds.Contains(str.ID))
                    stringIds.Add(str.ID);
            }
            foreach (var str in strings.Korean)
            {
                if (!stringIds.Contains(str.ID))
                    stringIds.Add(str.ID);
            }
            foreach (var str in strings.Chinese)
            {
                if (!stringIds.Contains(str.ID))
                    stringIds.Add(str.ID);
            }
            foreach (var str in strings.Portuguese)
            {
                if (!stringIds.Contains(str.ID))
                    stringIds.Add(str.ID);
            }

            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                foreach (string stringId in stringIds)
                {
                    if (((BlockField)unicodeStringListBlock[0]).Add(out Block stringReferenceBlock))
                    {
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

                        if (strings.English.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[1].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.English.First(s => s.ID == stringId).Value);
                        }

                        if (strings.Japanese.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[2].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Japanese.First(s => s.ID == stringId).Value);
                        }

                        if (strings.German.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[3].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.German.First(s => s.ID == stringId).Value);
                        }

                        if (strings.French.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[4].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.French.First(s => s.ID == stringId).Value);
                        }

                        if (strings.Spanish.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[5].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Spanish.First(s => s.ID == stringId).Value);
                        }

                        if (strings.Spanish.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[5].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Italian.First(s => s.ID == stringId).Value);
                        }

                        if (strings.Korean.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[6].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Korean.First(s => s.ID == stringId).Value);
                        }

                        if (strings.Chinese.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[7].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Chinese.First(s => s.ID == stringId).Value);
                        }

                        if (strings.Portuguese.Any(e => e.ID == stringId))
                        {
                            stringReferenceBlock.Fields[8].Value = (int)ms.Position;
                            writer.WriteUTF8NullTerminated(strings.Portuguese.First(s => s.ID == stringId).Value);
                        }
                    }
                }

                DataField stringData = (DataField)unicodeStringListBlock[1];
                stringData.SetBuffer(ms.ToArray());
            }

            return tagGroup;
        }

        private static Group CacheFileSound_ToGuerilla(Group cacheFileSound, Group soundCacheFileGestalt, HaloMap map)
        {
            Group sound = new Abide.Tag.Guerilla.Generated.Sound();
            Block soundCacheFileGestaltBlock = soundCacheFileGestalt.TagBlocks[0];
            Block cacheFileSoundBlock = cacheFileSound.TagBlocks[0];
            Block soundBlock = sound.TagBlocks[0];
            BlockField playbacks = (BlockField)soundCacheFileGestaltBlock.Fields[0];
            BlockField scales = (BlockField)soundCacheFileGestaltBlock.Fields[1];
            BlockField importNames = (BlockField)soundCacheFileGestaltBlock.Fields[2];
            BlockField pitchRangeParameters = (BlockField)soundCacheFileGestaltBlock.Fields[3];
            BlockField pitchRanges = (BlockField)soundCacheFileGestaltBlock.Fields[4];
            BlockField permutations = (BlockField)soundCacheFileGestaltBlock.Fields[5];
            BlockField customPlaybacks = (BlockField)soundCacheFileGestaltBlock.Fields[6];
            BlockField chunks = (BlockField)soundCacheFileGestaltBlock.Fields[8];
            BlockField promotions = (BlockField)soundCacheFileGestaltBlock.Fields[9];
            BlockField extraInfos = (BlockField)soundCacheFileGestaltBlock.Fields[10];

            soundBlock.Fields[0].Value = (int)(short)cacheFileSoundBlock.Fields[0].Value;   // flags
            soundBlock.Fields[1].Value = cacheFileSoundBlock.Fields[1].Value;   // class
            soundBlock.Fields[2].Value = cacheFileSoundBlock.Fields[2].Value;   // sample rate
            soundBlock.Fields[9].Value = cacheFileSoundBlock.Fields[3].Value;   // encoding
            soundBlock.Fields[10].Value = cacheFileSoundBlock.Fields[4].Value;  // compression

            using (MemoryStream ms = new MemoryStream((byte[])soundBlock.Fields[12].Value)) // padding field
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                writer.Write((int)cacheFileSoundBlock.Fields[12].Value);    // maximum play time
                writer.Write((byte)cacheFileSoundBlock.Fields[9].Value);    // promotion index
            }

            if ((short)cacheFileSoundBlock.Fields[5].Value != nullShortIndex)   // playback short index
            {
                Block playback = playbacks.BlockList[(short)cacheFileSoundBlock.Fields[5].Value];
                soundBlock.Fields[5].Value = playback.Fields[0].Value;  // playback parameters
            }

            if ((byte)cacheFileSoundBlock.Fields[8].Value != nullByteIndex) // scale byte index
            {
                Block scale = scales.BlockList[(byte)cacheFileSoundBlock.Fields[8].Value];
                soundBlock.Fields[6].Value = scale.Fields[0].Value; // scale
            }

            if ((byte)cacheFileSoundBlock.Fields[9].Value != nullByteIndex) // promotion byte index
            {
                Block promotion = promotions.BlockList[(byte)cacheFileSoundBlock.Fields[9].Value];
                soundBlock.Fields[11].Value = promotion.Fields[0].Value;    // platform parameters
            }

            if ((byte)cacheFileSoundBlock.Fields[10].Value != nullByteIndex)    // custom playback byte index
            {
                BlockField platformParameters = (BlockField)soundBlock.Fields[14];
                if (platformParameters.Add(out Block soundPlatformPlaybackBlock))
                {
                    Block customPlayback = customPlaybacks.BlockList[(byte)cacheFileSoundBlock.Fields[10].Value];
                    soundPlatformPlaybackBlock.Fields[0].Value = customPlayback.Fields[0].Value;    //playback definition
                }
            }

            if ((short)cacheFileSoundBlock.Fields[11].Value != nullShortIndex)  // extra infos
            {
                BlockField unnamed = (BlockField)soundBlock.Fields[15];
                if (unnamed.Add(out Block extraInfo))
                {
                    //Get encoded permutation section
                    Block soundGestaltExtraInfo = extraInfos.BlockList[(short)cacheFileSoundBlock.Fields[11].Value];
                    foreach (Block section in ((BlockField)soundGestaltExtraInfo.Fields[0]).BlockList)
                        ((BlockField)extraInfo.Fields[1]).BlockList.Add(section);

                    //Get geometry block info
                    TagBlock_ToGuerilla((Block)extraInfo.Fields[2].Value, (Block)soundGestaltExtraInfo.Fields[1].Value, map);
                    ((Block)extraInfo.Fields[2].Value).Fields[6].Value = string.Empty;
                }
            }

            if ((short)cacheFileSoundBlock.Fields[6].Value != nullShortIndex)   // pitch range short index
            {
                short pitchRangeIndex = (short)cacheFileSoundBlock.Fields[6].Value;
                byte pitchRangeCount = (byte)cacheFileSoundBlock.Fields[7].Value;
                BlockField soundPitchRanges = (BlockField)soundBlock.Fields[13];
                for (int i = 0; i < pitchRangeCount; i++)
                {
                    if (soundPitchRanges.Add(out Block soundPitchRange))
                    {
                        Block pitchRange = pitchRanges.BlockList[pitchRangeIndex + i];
                        Block importName = importNames.BlockList[(short)pitchRange.Fields[0].Value];
                        Block pitchRangeParameter = pitchRangeParameters.BlockList[(short)pitchRange.Fields[1].Value];

                        //Setup sound pitch range
                        soundPitchRange.Fields[0].Value = map.Strings[((StringId)importName.Fields[0].Value).Index];
                        soundPitchRange.Fields[2].Value = (short)pitchRangeParameter.Fields[0].Value;
                        soundPitchRange.Fields[4].Value = (ShortBounds)pitchRangeParameter.Fields[1].Value;
                        soundPitchRange.Fields[5].Value = (ShortBounds)pitchRangeParameter.Fields[2].Value;

                        if ((short)pitchRange.Fields[4].Value != nullShortIndex)    // sound permutation short index
                        {
                            short permutationIndex = (short)pitchRange.Fields[4].Value;
                            short permutationCount = (short)pitchRange.Fields[5].Value;
                            BlockField soundPermutations = (BlockField)soundPitchRange.Fields[7];
                            for (int j = 0; j < permutationCount; j++)
                            {
                                if (soundPermutations.Add(out Block soundPermutation))
                                {
                                    Block permutation = permutations.BlockList[permutationIndex + j];
                                    importName = importNames.BlockList[(short)permutation.Fields[0].Value];
                                    soundPermutation.Fields[0].Value = map.Strings[((StringId)importName.Fields[0].Value).Index];
                                    soundPermutation.Fields[1].Value = (ushort)(short)permutation.Fields[1].Value / 65535f;
                                    soundPermutation.Fields[2].Value = (byte)permutation.Fields[2].Value / 255f;
                                    soundPermutation.Fields[3].Value = (int)permutation.Fields[5].Value;
                                    soundPermutation.Fields[4].Value = (short)(byte)permutation.Fields[3].Value;
                                    soundPermutation.Fields[5].Value = (short)permutation.Fields[4].Value;

                                    if ((short)permutation.Fields[6].Value != nullShortIndex)   // sound chunk short index
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

            return sound;
        }
    }
}
