using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using System;
using System.IO;
using System.Linq;

namespace Abide.TagBuilder.Halo2
{
    internal static class Helper
    {
        public static void RecalculateRawAddresses(this RawContainer raws, string root, VirtualStream tagData, BinaryReader reader, BinaryWriter writer)
        {
            //Prepare
            long offsetAddress = 0, lengthAddress = 0;
            int rawOffset = 0;

            //Handle
            switch (root)
            {
                #region ugh!
                case HaloTags.ugh_:
                    tagData.Seek(tagData.MemoryAddress + 64, SeekOrigin.Begin);
                    int soundsCount = reader.ReadInt32();
                    int soundsOffset = reader.ReadInt32();
                    for (int i = 0; i < soundsCount; i++)
                    {
                        //Goto
                        tagData.Seek(soundsOffset + (i * 12), SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (raws[RawSection.Sound].ContainsRawOffset(rawOffset))
                        {
                            raws[RawSection.Sound][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.Sound][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.Sound][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.Sound][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    tagData.Seek(tagData.MemoryAddress + 80, SeekOrigin.Begin);
                    int extraInfosCount = reader.ReadInt32();
                    int extraInfosOffset = reader.ReadInt32();
                    for (int i = 0; i < extraInfosCount; i++)
                    {
                        //Goto
                        tagData.Seek(extraInfosOffset + (i * 44) + 8, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (raws[RawSection.LipSync].ContainsRawOffset(rawOffset))
                        {
                            raws[RawSection.LipSync][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.LipSync][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.LipSync][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.LipSync][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region mode
                case HaloTags.mode:
                    tagData.Seek(tagData.MemoryAddress + 36, SeekOrigin.Begin);
                    int sectionCount = reader.ReadInt32();
                    int sectionOffset = reader.ReadInt32();
                    for (int i = 0; i < sectionCount; i++)
                    {
                        tagData.Seek(sectionOffset + (i * 92) + 56, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (raws[RawSection.Model].ContainsRawOffset(rawOffset))
                        {
                            raws[RawSection.Model][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.Model][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.Model][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.Model][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    tagData.Seek(tagData.MemoryAddress + 116, SeekOrigin.Begin);
                    int prtCount = reader.ReadInt32();
                    int prtOffset = reader.ReadInt32();
                    for (int i = 0; i < prtCount; i++)
                    {
                        tagData.Seek(prtOffset + (i * 88) + 52, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (raws[RawSection.Model].ContainsRawOffset(rawOffset))
                        {
                            raws[RawSection.Model][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.Model][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.Model][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.Model][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region weat
                case HaloTags.weat:
                    tagData.Seek(tagData.MemoryAddress, SeekOrigin.Begin);
                    int particleSystemCount = reader.ReadInt32();
                    int particleSystemOffset = reader.ReadInt32();
                    for (int i = 0; i < particleSystemCount; i++)
                    {
                        tagData.Seek(particleSystemOffset + (i * 140) + 64, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if (raws[RawSection.Weather].ContainsRawOffset(rawOffset))
                        {
                            raws[RawSection.Weather][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.Weather][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.Weather][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.Weather][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region DECR
                case HaloTags.DECR:
                    tagData.Seek(tagData.MemoryAddress + 56, SeekOrigin.Begin);
                    offsetAddress = tagData.Position;
                    rawOffset = reader.ReadInt32();
                    lengthAddress = tagData.Position;

                    //Check
                    if (raws[RawSection.DecoratorSet].ContainsRawOffset(rawOffset))
                    {
                        raws[RawSection.DecoratorSet][rawOffset].OffsetAddresses.Clear();
                        raws[RawSection.DecoratorSet][rawOffset].LengthAddresses.Clear();
                        raws[RawSection.DecoratorSet][rawOffset].OffsetAddresses.Add(offsetAddress);
                        raws[RawSection.DecoratorSet][rawOffset].LengthAddresses.Add(lengthAddress);
                    }
                    break;
                #endregion
                #region PRTM
                case HaloTags.PRTM:
                    tagData.Seek(tagData.MemoryAddress + 160, SeekOrigin.Begin);
                    offsetAddress = tagData.Position;
                    rawOffset = reader.ReadInt32();
                    lengthAddress = tagData.Position;

                    //Check
                    if (raws[RawSection.ParticleModel].ContainsRawOffset(rawOffset))
                    {
                        raws[RawSection.ParticleModel][rawOffset].OffsetAddresses.Clear();
                        raws[RawSection.ParticleModel][rawOffset].LengthAddresses.Clear();
                        raws[RawSection.ParticleModel][rawOffset].OffsetAddresses.Add(offsetAddress);
                        raws[RawSection.ParticleModel][rawOffset].LengthAddresses.Add(lengthAddress);
                    }
                    break;
                #endregion
                #region jmad
                case HaloTags.jmad:
                    tagData.Seek(tagData.MemoryAddress + 172, SeekOrigin.Begin);
                    int animationCount = reader.ReadInt32();
                    int animationOffset = reader.ReadInt32();
                    for (int i = 0; i < animationCount; i++)
                    {
                        tagData.Seek(animationOffset + (i * 20) + 4, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;
                        reader.ReadInt32();
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();

                        //Check
                        if (raws[RawSection.Animation].ContainsRawOffset(rawOffset))
                        {
                            raws[RawSection.Animation][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.Animation][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.Animation][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.Animation][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region bitm
                case HaloTags.bitm:
                    tagData.Seek(tagData.MemoryAddress + 68, SeekOrigin.Begin);
                    int bitmapCount = reader.ReadInt32();
                    int bitmapOffset = reader.ReadInt32();
                    for (int i = 0; i < bitmapCount; i++)
                    {
                        //LOD0
                        tagData.Seek(bitmapOffset + (i * 116) + 28, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 52, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD1
                        tagData.Seek(bitmapOffset + (i * 116) + 32, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 56, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD2
                        tagData.Seek(bitmapOffset + (i * 116) + 36, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 60, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD3
                        tagData.Seek(bitmapOffset + (i * 116) + 40, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 64, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD4
                        tagData.Seek(bitmapOffset + (i * 116) + 44, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 68, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }

                        //LOD5
                        tagData.Seek(bitmapOffset + (i * 116) + 48, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        tagData.Seek(bitmapOffset + (i * 116) + 72, SeekOrigin.Begin);
                        lengthAddress = tagData.Position;

                        //Check
                        if (raws[RawSection.Bitmap].ContainsRawOffset(rawOffset))
                        {
                            raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.Bitmap][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }
                    break;
                #endregion
                #region sbsp
                case HaloTags.sbsp:
                    //Goto Clusters
                    tagData.Seek(tagData.MemoryAddress + 156, SeekOrigin.Begin);
                    uint clusterCount = reader.ReadUInt32();
                    uint clusterOffset = reader.ReadUInt32();
                    for (int i = 0; i < clusterCount; i++)
                    {
                        tagData.Seek(clusterOffset + (i * 176) + 40, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if ((rawOffset & 0xC0000000) == 0)
                        {
                            raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    //Goto Geometries definitions
                    tagData.Seek(tagData.MemoryAddress + 312, SeekOrigin.Begin);
                    uint geometriesCount = reader.ReadUInt32();
                    uint geometriesOffset = reader.ReadUInt32();
                    for (int i = 0; i < geometriesCount; i++)
                    {
                        tagData.Seek(geometriesOffset + (i * 200) + 40, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if ((rawOffset & 0xC0000000) == 0)
                        {
                            raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    //Goto Water definitions
                    tagData.Seek(tagData.MemoryAddress + 532, SeekOrigin.Begin);
                    uint watersCount = reader.ReadUInt32();
                    uint watersOffset = reader.ReadUInt32();
                    for (int i = 0; i < watersCount; i++)
                    {
                        tagData.Seek(watersOffset + (i * 172) + 16, SeekOrigin.Begin);
                        offsetAddress = tagData.Position;
                        rawOffset = reader.ReadInt32();
                        lengthAddress = tagData.Position;

                        //Check
                        if ((rawOffset & 0xC0000000) == 0)
                        {
                            raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                            raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                            raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                            raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                        }
                    }

                    //Goto Decorators Definitions
                    tagData.Seek(tagData.MemoryAddress + 564, SeekOrigin.Begin);
                    uint decoratorsCount = reader.ReadUInt32();
                    uint decoratorsOffset = reader.ReadUInt32();
                    for (int i = 0; i < decoratorsCount; i++)
                    {
                        tagData.Seek(decoratorsOffset + (i * 48) + 16, SeekOrigin.Begin);
                        uint cachesCount = reader.ReadUInt32();
                        uint cachesOffset = reader.ReadUInt32();
                        for (int j = 0; j < cachesCount; j++)
                        {
                            tagData.Seek(cachesOffset + (j * 44), SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = reader.ReadInt32();
                            lengthAddress = tagData.Position;

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                            {
                                raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                                raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                                raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                                raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                            }
                        }
                    }
                    break;
                #endregion
                #region ltmp
                case HaloTags.ltmp:
                    //Goto Lightmap Groups
                    tagData.Seek(tagData.MemoryAddress + 128);
                    uint groupsCount = reader.ReadUInt32();
                    uint groupsPointer = reader.ReadUInt32();
                    for (int i = 0; i < groupsCount; i++)
                    {
                        //Goto Cluster Definitions
                        tagData.Seek(groupsPointer + (i * 104) + 32, SeekOrigin.Begin);
                        uint clustersCount = reader.ReadUInt32();
                        uint clustersOffset = reader.ReadUInt32();
                        for (int j = 0; j < clustersCount; j++)
                        {
                            tagData.Seek(clustersOffset + (j * 84) + 40, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = reader.ReadInt32();
                            lengthAddress = tagData.Position;

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                            {
                                raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                                raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                                raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                                raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                            }
                        }

                        //Goto Poop Definitions
                        tagData.Seek(groupsPointer + (i * 104) + 48, SeekOrigin.Begin);
                        uint poopsCount = reader.ReadUInt32();
                        uint poopsOffset = reader.ReadUInt32();
                        for (int j = 0; j < poopsCount; j++)
                        {
                            tagData.Seek(poopsOffset + (j * 84) + 40, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = reader.ReadInt32();
                            lengthAddress = tagData.Position;

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                            {
                                raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                                raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                                raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                                raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                            }
                        }

                        //Goto Geometry Buckets
                        tagData.Seek(groupsPointer + (i * 104) + 64, SeekOrigin.Begin);
                        uint bucketsCount = reader.ReadUInt32();
                        uint bucketsOffset = reader.ReadUInt32();
                        for (int j = 0; j < bucketsCount; j++)
                        {
                            tagData.Seek(bucketsOffset + (j * 56) + 12, SeekOrigin.Begin);
                            offsetAddress = tagData.Position;
                            rawOffset = reader.ReadInt32();
                            lengthAddress = tagData.Position;

                            //Check
                            if ((rawOffset & 0xC0000000) == 0)
                            {
                                raws[RawSection.BSP][rawOffset].OffsetAddresses.Clear();
                                raws[RawSection.BSP][rawOffset].LengthAddresses.Clear();
                                raws[RawSection.BSP][rawOffset].OffsetAddresses.Add(offsetAddress);
                                raws[RawSection.BSP][rawOffset].LengthAddresses.Add(lengthAddress);
                            }
                        }
                    }
                    break;
                    #endregion
            }
        }
        
        public static void AddScenarioStructureTags(this MapFile map, params ScenarioStructureTag[] tags)
        {
            //Check
            if (tags.Length == 0) return;

            /*
             * So a few notes about this method:
             * This doesn't actually *add* a scenario structure tag to the map. It replaces an existing one 
             * (or a null reference) as determined by the map's scenario tag. If you choose a BSP index that is 
             * being used by another tag, then the data from the existing tag will be lost. This can be avoided 
             * by adding a new Structure BSPs tag block to the scenario and leaving the SBSP and LTMP tags 
             * unreferenced (or null). Adding the scenario structure tags to the new index will create a new 
             * scenario structure tag block, independant of the others.
             */

            //Get sound gestalt
            IndexEntry soundGestalt = map.GetSoundGestaltEntry();

            //Check
            if (soundGestalt.Root != "ugh!") return;

            //Remove
            map.IndexEntries.Remove(soundGestalt);
            Console.WriteLine("Removed {0}.{1} temporarily...", soundGestalt.Filename, soundGestalt.Root);

            //Prepare
            long bspLength = 0;
            long virtualBspAddress = (Index.IndexMemoryAddress - Index.Length) + map.IndexLength;
            IndexEntry newEntry = null;
            TagId newTagId = TagId.Null;
            Group tagGroup = null;
            byte[] tagBuffer = null;
            byte[] sbspTagBuffer = null;
            byte[] ltmpTagBuffer = null;
            
            //Loop
            foreach (var tag in tags)
            {
                //Get entries that are referenced by the BSP index
                IndexEntry ltmpEntry = map.IndexEntries.FirstOrDefault(e => e.Root == HaloTags.ltmp && e.TagData == map.GetBspTagDataStream(tag.Index)) ?? null;
                IndexEntry sbspEntry = map.IndexEntries.FirstOrDefault(e => e.Root == HaloTags.sbsp && e.TagData == map.GetBspTagDataStream(tag.Index)) ?? null;
                TagId sbspId = TagId.Null;
                TagId ltmpId = TagId.Null;

                //Create stream
                using (VirtualStream bspTagDataStream = new VirtualStream(virtualBspAddress))
                using (BinaryWriter writer = new BinaryWriter(bspTagDataStream))
                using (BinaryReader reader = new BinaryReader(bspTagDataStream))
                {
                    //Skip header
                    bspTagDataStream.Seek(StructureBspBlockHeader.Length, SeekOrigin.Current);
                    StructureBspBlockHeader blockHeader = new StructureBspBlockHeader() { StructureBsp = "sbsp" };

                    //Create scenario structure BSP tag
                    if (tag.TagGroup.GroupTag == HaloTags.sbsp)
                    {
                        //Create virtual tag stream
                        using (VirtualStream sbspTagDataStream = new VirtualStream(bspTagDataStream.Position))
                        using (BinaryWriter tagWriter = new BinaryWriter(sbspTagDataStream))
                        using (BinaryReader tagReader = new BinaryReader(sbspTagDataStream))
                        {
                            //Write new tag group
                            tag.TagGroup.Write(tagWriter);

                            //Pad
                            sbspTagDataStream.Align(4098);

                            //Fix raw addresses
                            if (tag.SourceEntry != null) tag.SourceEntry.Raws.RecalculateRawAddresses(tag.TagGroup.GroupTag, sbspTagDataStream, tagReader, tagWriter);

                            //Get buffer
                            sbspTagBuffer = sbspTagDataStream.ToArray();
                        }

                        //Check SBSP entry
                        if (sbspEntry != null)
                        {
                            //Modify
                            Console.WriteLine("Modifying {0}.{1}...", sbspEntry.Filename, sbspEntry.Root);
                            sbspEntry.Filename = tag.Name;
                            sbspEntry.PostProcessedOffset = (int)bspTagDataStream.Position;
                            sbspEntry.PostProcessedSize = sbspTagBuffer.Length;
                            if (tag.SourceEntry != null) sbspEntry.Raws.CopyFrom(tag.SourceEntry.Raws);
                            Console.WriteLine("... To {0}.{1}", sbspEntry.Filename, sbspEntry.Root);
                            sbspId = sbspEntry.Id;
                        }
                        else
                        {
                            //Get tag ID
                            if (tag.Id.IsNull) { newTagId = map.IndexEntries.Last.Id; newTagId++; }
                            else newTagId = tag.Id;

                            //Add sbsp tag
                            newEntry = new IndexEntry(new ObjectEntry()
                            {
                                Id = newTagId,
                                Tag = tag.TagGroup.GroupTag,
                            }, tag.Name, map.Tags[tag.TagGroup.GroupTag])
                            {
                                PostProcessedOffset = (int)bspTagDataStream.Position,
                                PostProcessedSize = sbspTagBuffer.Length,
                                TagData = map.GetBspTagDataStream(tag.Index)
                            };
                            if (tag.SourceEntry != null) newEntry.Raws.CopyFrom(tag.SourceEntry.Raws);
                            sbspId = newEntry.Id;

                            //Add tag
                            if (map.IndexEntries.Add(newEntry))
                                Console.WriteLine("Added {0}.{1} ({2})", tag.Name, tag.TagGroup.GroupTag, sbspId);
                        }
                    }
                    else if (sbspEntry != null)
                    {
                        //Read tag group
                        tagGroup = new ScenarioStructureBsp();
                        sbspEntry.TagData.Seek((uint)sbspEntry.PostProcessedOffset, SeekOrigin.Begin);
                        using (BinaryReader tagReader = sbspEntry.TagData.CreateReader()) tagGroup.Read(tagReader);

                        //Create virtual tag stream
                        using (VirtualStream vs = new VirtualStream(bspTagDataStream.Position))
                        using (BinaryWriter tagWriter = new BinaryWriter(vs))
                        using (BinaryReader tagReader = new BinaryReader(vs))
                        {
                            //Write tag group
                            tagGroup.Write(tagWriter);

                            //Pad
                            vs.Align(4098);

                            //Fix raw addresses
                            sbspEntry.Raws.RecalculateRawAddresses(sbspEntry.Root, vs, tagReader, tagWriter);

                            //Get buffer
                            sbspTagBuffer = vs.ToArray();
                        }

                        //Modify entry
                        sbspEntry.SetObjectEntry(new ObjectEntry()
                        {
                            Id = sbspEntry.Id,
                            Tag = sbspEntry.Root,
                        });
                        sbspEntry.PostProcessedOffset = (int)bspTagDataStream.Position;
                        sbspEntry.PostProcessedSize = sbspTagBuffer.Length;
                        sbspId = sbspEntry.Id;
                    }
                    else sbspTagBuffer = new byte[0];

                    //Write structure BSP to stream
                    if (sbspTagBuffer.Length > 0) blockHeader.StructureBspOffset = (uint)bspTagDataStream.Position;
                    writer.Write(sbspTagBuffer);

                    //Create scenario structure lightmap tag
                    if (tag.TagGroup.GroupTag == HaloTags.ltmp)
                    {
                        //Remove existing LTMP
                        if (ltmpEntry != null && map.IndexEntries.Remove(ltmpEntry))
                            Console.WriteLine($"Removing {ltmpEntry.Filename}.{ltmpEntry.Root}");

                        //Create virtual tag stream
                        using (VirtualStream ltmpTagDataStream = new VirtualStream(bspTagDataStream.Position))
                        using (BinaryWriter tagWriter = new BinaryWriter(ltmpTagDataStream))
                        using (BinaryReader tagReader = new BinaryReader(ltmpTagDataStream))
                        {
                            //Write new tag group
                            tag.TagGroup.Write(tagWriter);

                            //Align
                            ltmpTagDataStream.Align(4096);

                            //Fix raw addresses
                            if (tag.SourceEntry != null) tag.SourceEntry.Raws.RecalculateRawAddresses(tag.TagGroup.GroupTag, ltmpTagDataStream, tagReader, tagWriter);

                            //Get buffer
                            ltmpTagBuffer = ltmpTagDataStream.ToArray();
                        }

                        //Check SBSP entry
                        if (ltmpEntry != null)
                        {
                            //Modify
                            Console.WriteLine("Modifying {0}.{1}...", ltmpEntry.Filename, ltmpEntry.Root);
                            ltmpEntry.Filename = tag.Name;
                            ltmpEntry.PostProcessedOffset = (int)bspTagDataStream.Position;
                            ltmpEntry.PostProcessedSize = ltmpTagBuffer.Length;
                            if (tag.SourceEntry != null) ltmpEntry.Raws.CopyFrom(tag.SourceEntry.Raws);
                            Console.WriteLine("... To {0}.{1}", ltmpEntry.Filename, ltmpEntry.Root);
                            ltmpId = ltmpEntry.Id;
                        }
                        else
                        {
                            //Get tag ID
                            if (tag.Id.IsNull) { newTagId = map.IndexEntries.Last.Id; newTagId++; }
                            else newTagId = tag.Id;

                            //Add new tag
                            newEntry = new IndexEntry(new ObjectEntry()
                            {
                                Id = newTagId,
                                Tag = tag.TagGroup.GroupTag,
                            }, tag.Name, map.Tags[tag.TagGroup.GroupTag])
                            {
                                PostProcessedOffset = (int)bspTagDataStream.Position,
                                PostProcessedSize = ltmpTagBuffer.Length,
                                TagData = map.GetBspTagDataStream(tag.Index)
                            };
                            if (tag.SourceEntry != null) newEntry.Raws.CopyFrom(tag.SourceEntry.Raws);
                            ltmpId = newEntry.Id;

                            //Add tag
                            if (map.IndexEntries.Add(newEntry))
                                Console.WriteLine("Added {0}.{1} ({2})", tag.Name, tag.TagGroup.GroupTag, newTagId);
                        }
                    }
                    else if (ltmpEntry != null)
                    {
                        //Read tag group
                        tagGroup = new ScenarioStructureLightmap();
                        ltmpEntry.TagData.Seek((uint)ltmpEntry.PostProcessedOffset, SeekOrigin.Begin);
                        using (BinaryReader tagReader = ltmpEntry.TagData.CreateReader()) tagGroup.Read(tagReader);

                        //Create virtual tag stream
                        using (VirtualStream vs = new VirtualStream(bspTagDataStream.Position))
                        using (BinaryWriter tagWriter = new BinaryWriter(vs))
                        using (BinaryReader tagReader = new BinaryReader(vs))
                        {
                            //Write tag group
                            tagGroup.Write(tagWriter);

                            //Pad
                            vs.Align(4096);

                            //Fix raw addresses
                            ltmpEntry.Raws.RecalculateRawAddresses(ltmpEntry.Root, vs, tagReader, tagWriter);

                            //Get buffer
                            ltmpTagBuffer = vs.ToArray();
                        }

                        //Modify entry
                        ltmpEntry.SetObjectEntry(new ObjectEntry()
                        {
                            Id = ltmpEntry.Id,
                            Tag = ltmpEntry.Root,
                        });
                        ltmpEntry.PostProcessedOffset = (int)bspTagDataStream.Position;
                        ltmpEntry.PostProcessedSize = ltmpTagBuffer.Length;
                        ltmpId = ltmpEntry.Id;
                    }
                    else ltmpTagBuffer = new byte[0];

                    //Write structure lightmap to stream
                    if (ltmpTagBuffer.Length > 0) blockHeader.StructureLightmapOffset = (uint)bspTagDataStream.Position;
                    writer.Write(ltmpTagBuffer);

                    //Align to 4096 bytes
                    bspTagDataStream.Align(4096);

                    //Write header
                    bspTagDataStream.Seek(virtualBspAddress, SeekOrigin.Begin);
                    writer.Write(blockHeader);

                    //Swap BSP tag buffer
                    map.SwapBspTagBuffer(bspTagDataStream.ToArray(), tag.Index, bspTagDataStream.MemoryAddress);
                }

                //Write new IDs to scenario
                using (BinaryReader reader = map.Scenario.TagData.CreateReader())
                using (BinaryWriter writer = map.Scenario.TagData.CreateWriter())
                {
                    //Read scenario structure bsp reference tag block
                    map.Scenario.TagData.Seek(map.Scenario.Offset + 528, SeekOrigin.Begin);
                    int sbspRefsCount = reader.ReadInt32();
                    int sbspRefsOffset = reader.ReadInt32();
                    if (sbspRefsCount > 0)
                    {
                        //Goto
                        map.Scenario.TagData.Seek(sbspRefsOffset, SeekOrigin.Begin);

                        //Loop
                        map.Scenario.TagData.Seek(sbspRefsOffset + (tag.Index * 68), SeekOrigin.Begin);
                        int structureBspBlockAddress = reader.ReadInt32();
                        uint structureBspBlockSize = reader.ReadUInt32();
                        uint structureBspMemoryAddress = reader.ReadUInt32();
                        int zero = reader.ReadInt32();
                        writer.Write<TagFourCc>(HaloTags.sbsp);
                        writer.Write(sbspId);
                        writer.Write<TagFourCc>(HaloTags.ltmp);
                        writer.Write(ltmpId);
                    }
                }
            }

            //Get BSP length
            for (int i = 0; i < map.BspCount; i++)
                if (map.GetBspTagDataStream(i).Length > bspLength)
                    bspLength = map.GetBspTagDataStream(i).Length;
            
            //Create stream
            using (VirtualStream tagDataStream = new VirtualStream(virtualBspAddress + bspLength))
            using (BinaryWriter writer = new BinaryWriter(tagDataStream))
            using (BinaryReader reader = new BinaryReader(tagDataStream))
            {
                //Rebuild tags
                foreach (IndexEntry entry in map.IndexEntries.Where(e => e.TagData == map.TagDataStream && e != soundGestalt))
                {
                    //Create tag group
                    tagGroup = TagLookup.CreateTagGroup(entry.Root);

                    //Read tag group
                    entry.TagData.Seek((uint)entry.PostProcessedOffset, SeekOrigin.Begin);
                    using (BinaryReader tagReader = entry.TagData.CreateReader()) tagGroup.Read(tagReader);

                    //Create virtual tag stream
                    using (VirtualStream vs = new VirtualStream(tagDataStream.Position))
                    using (BinaryWriter tagWriter = new BinaryWriter(vs))
                    using (BinaryReader tagReader = new BinaryReader(vs))
                    {
                        //Write tag group
                        tagGroup.Write(tagWriter);

                        //Fix raw addresses
                        entry.Raws.RecalculateRawAddresses(entry.Root, vs, tagReader, tagWriter);

                        //Get buffer
                        tagBuffer = vs.ToArray();
                    }

                    //Modify entry
                    entry.SetObjectEntry(new ObjectEntry()
                    {
                        Id = entry.Id,
                        Tag = entry.Root,
                        Offset = (uint)tagDataStream.Position,
                        Size = (uint)tagBuffer.Length
                    });
                    entry.PostProcessedOffset = (int)tagDataStream.Position;
                    entry.PostProcessedSize = tagBuffer.Length;

                    //Write entry to tag data stream
                    writer.Write(tagBuffer);
                }
                
                //Build sound gestalt
                tagGroup = TagLookup.CreateTagGroup(soundGestalt.Root);

                //Read tag group
                map.TagDataStream.Seek((uint)soundGestalt.PostProcessedOffset, SeekOrigin.Begin);
                using (BinaryReader tagReader = map.TagDataStream.CreateReader()) tagGroup.Read(tagReader);

                //Create virtual tag stream
                using (VirtualStream vs = new VirtualStream(tagDataStream.Position))
                using (BinaryWriter tagWriter = new BinaryWriter(vs))
                using (BinaryReader tagReader = new BinaryReader(vs))
                {
                    //Write tag group
                    tagGroup.Write(tagWriter);

                    //Fix raw addresses
                    soundGestalt.Raws.RecalculateRawAddresses(soundGestalt.Root, vs, tagReader, tagWriter);

                    //Get buffer
                    tagBuffer = vs.ToArray();
                }

                //Modify sound gestalt
                newTagId = map.IndexEntries.Last.Id; newTagId++;
                soundGestalt.SetObjectEntry(new ObjectEntry()
                {
                    Id = newTagId.Id,
                    Tag = HaloTags.ugh_,
                    Offset = (uint)tagDataStream.Position,
                    Size = (uint)tagBuffer.Length
                });
                soundGestalt.PostProcessedOffset = (int)tagDataStream.Position;
                soundGestalt.PostProcessedSize = tagBuffer.Length;
                soundGestalt.Filename = "i've got a lovely bunch of coconuts";

                //Write entry to tag data stream
                writer.Write(tagBuffer);

                //Add tag
                if (map.IndexEntries.Add(soundGestalt))
                    Console.WriteLine("Added {0}.{1} ({2})", soundGestalt.Filename, soundGestalt.Root, soundGestalt.Id);

                //Align to 1024 bytes
                tagDataStream.Align(1024);

                //Swap
                map.SwapTagBuffer(tagDataStream.ToArray(), tagDataStream.MemoryAddress);

                //Change sound gestalt id
                map.ChangeSoundGestalt(newTagId);
            }
        }

        public static void AddTags(this MapFile map, params TagBase[] tags)
        {
            //Check
            if (tags.Length == 0) return;

            //Get sound gestalt
            IndexEntry soundGestalt = map.GetSoundGestaltEntry();

            //Check
            if (soundGestalt.Root != "ugh!") return;

            //Remove
            map.IndexEntries.Remove(soundGestalt);
            Console.WriteLine("Removed {0}.{1} temporarily...", soundGestalt.Filename, soundGestalt.Root);

            //Prepare
            TagId newTagId = TagId.Null;
            IndexEntry newEntry = null;
            Group tagGroup = null;
            byte[] tagBuffer = null;

            //Create stream
            using (VirtualStream tagDataStream = new VirtualStream(map.TagDataStream.MemoryAddress))
            using (BinaryWriter writer = new BinaryWriter(tagDataStream))
            using (BinaryReader reader = new BinaryReader(tagDataStream))
            {
                //Rebuild tags
                foreach (IndexEntry entry in map.IndexEntries.Where(e => e.TagData == map.TagDataStream && e != soundGestalt))
                {
                    //Create tag group
                    tagGroup = TagLookup.CreateTagGroup(entry.Root);

                    //Read tag group
                    map.TagDataStream.Seek((uint)entry.PostProcessedOffset, SeekOrigin.Begin);
                    using (BinaryReader tagReader = map.TagDataStream.CreateReader()) tagGroup.Read(tagReader);

                    //Create virtual tag stream
                    using (VirtualStream vs = new VirtualStream(tagDataStream.Position))
                    using (BinaryWriter tagWriter = new BinaryWriter(vs))
                    using (BinaryReader tagReader = new BinaryReader(vs))
                    {
                        //Write tag group
                        tagGroup.Write(tagWriter);

                        //Fix raw addresses
                        entry.Raws.RecalculateRawAddresses(entry.Root, vs, tagReader, tagWriter);

                        //Get buffer
                        tagBuffer = vs.ToArray();
                    }

                    //Modify entry
                    entry.SetObjectEntry(new ObjectEntry()
                    {
                        Id = entry.Id,
                        Tag = entry.Root,
                        Offset = (uint)tagDataStream.Position,
                        Size = (uint)tagBuffer.Length
                    });
                    entry.PostProcessedOffset = (int)tagDataStream.Position;
                    entry.PostProcessedSize = tagBuffer.Length;

                    //Write entry to tag data stream
                    writer.Write(tagBuffer);
                }

                //Loop through new tags
                foreach (TagBase tag in tags)
                {
                    //Create virtual tag stream
                    using (VirtualStream vs = new VirtualStream(tagDataStream.Position))
                    using (BinaryWriter tagWriter = new BinaryWriter(vs))
                    using (BinaryReader tagReader = new BinaryReader(vs))
                    {
                        //Write new tag group
                        tag.TagGroup.Write(tagWriter);

                        //Fix raw addresses
                        if (tag.SourceEntry != null) tag.SourceEntry.Raws.RecalculateRawAddresses(tag.TagGroup.GroupTag, vs, tagReader, tagWriter);

                        //Get buffer
                        tagBuffer = vs.ToArray();
                    }

                    //Get tag ID
                    if (tag.Id.IsNull) { newTagId = map.IndexEntries.Last.Id; newTagId++; }
                    else newTagId = tag.Id;

                    //Add new tag
                    newEntry = new IndexEntry(new ObjectEntry()
                    {
                        Id = newTagId,
                        Tag = tag.TagGroup.GroupTag,
                        Offset = (uint)tagDataStream.Position,
                        Size = (uint)tagBuffer.Length
                    }, tag.Name, map.Tags[tag.TagGroup.GroupTag])
                    {
                        PostProcessedOffset = (int)tagDataStream.Position,
                        PostProcessedSize = tagBuffer.Length,
                        TagData = map.TagDataStream
                    };
                    if (tag.SourceEntry != null) newEntry.Raws.CopyFrom(tag.SourceEntry.Raws);

                    //Write entry to tag data stream
                    writer.Write(tagBuffer);

                    //Add tag
                    if (map.IndexEntries.Add(newEntry))
                        Console.WriteLine("Added {0}.{1} ({2})", tag.Name, tag.TagGroup.GroupTag, newTagId);
                }

                //Build sound gestalt
                tagGroup = TagLookup.CreateTagGroup(soundGestalt.Root);

                //Read tag group
                map.TagDataStream.Seek((uint)soundGestalt.PostProcessedOffset, SeekOrigin.Begin);
                using (BinaryReader tagReader = map.TagDataStream.CreateReader()) tagGroup.Read(tagReader);

                //Create virtual tag stream
                using (VirtualStream vs = new VirtualStream(tagDataStream.Position))
                using (BinaryWriter tagWriter = new BinaryWriter(vs))
                using (BinaryReader tagReader = new BinaryReader(vs))
                {
                    //Write tag group
                    tagGroup.Write(tagWriter);

                    //Fix raw addresses
                    soundGestalt.Raws.RecalculateRawAddresses(soundGestalt.Root, vs, tagReader, tagWriter);

                    //Get buffer
                    tagBuffer = vs.ToArray();
                }

                //Modify entry
                newTagId = map.IndexEntries.Last.Id; newTagId++;
                soundGestalt.SetObjectEntry(new ObjectEntry()
                {
                    Id = newTagId,
                    Tag = HaloTags.ugh_,
                    Offset = (uint)tagDataStream.Position,
                    Size = (uint)tagBuffer.Length
                });
                soundGestalt.PostProcessedOffset = (int)tagDataStream.Position;
                soundGestalt.PostProcessedSize = tagBuffer.Length;
                soundGestalt.Filename = "i've got a lovely bunch of coconuts";

                //Write entry to tag data stream
                writer.Write(tagBuffer);

                //Add tag
                if (map.IndexEntries.Add(soundGestalt))
                    Console.WriteLine("Added {0}.{1} ({2})", soundGestalt.Filename, soundGestalt.Root, soundGestalt.Id);

                //Align to 1024 bytes
                tagDataStream.Align(1024);

                //Swap
                map.SwapTagBuffer(tagDataStream.ToArray(), tagDataStream.MemoryAddress);

                //Change sound gestalt id
                map.ChangeSoundGestalt(newTagId);
            }
        }

        public static void Rebuild(this MapFile map)
        {
            //Prepare
            Group tagGroup = null;
            byte[] tagBuffer = null;

            //Prepare
            long bspTagDataStart = (Index.IndexMemoryAddress - Index.Length) + map.IndexLength;
            long tagDataStart = 0;
            long bspLength = 0;

            //Write structure bsp tags
            for (int i = 0; i < map.BspCount; i++)
                using (VirtualStream bspTagDataStream = new VirtualStream(bspTagDataStart))
                using (BinaryReader reader = map.GetBspTagDataStream(i).CreateReader())
                using (BinaryWriter writer = bspTagDataStream.CreateWriter())
                {
                    //Skip header
                    bspTagDataStream.Seek(StructureBspBlockHeader.Length, SeekOrigin.Current);

                    //Prepare
                    StructureBspBlockHeader bspHeader = new StructureBspBlockHeader() { StructureBsp = "sbsp" };
                    byte[] sbspTagBuffer = new byte[0];
                    byte[] ltmpTagBuffer = new byte[0];

                    //Get tags
                    IndexEntry sbspTag = map.IndexEntries.Where(t => t.TagData == map.GetBspTagDataStream(i) && t.Root == HaloTags.sbsp).First();
                    IndexEntry ltmpTag = map.IndexEntries.Where(t => t.TagData == map.GetBspTagDataStream(i) && t.Root == HaloTags.ltmp)?.FirstOrDefault() ?? null;

                    //Check sbsp tag
                    if (sbspTag != null)
                    {
                        //Create tag group
                        tagGroup = TagLookup.CreateTagGroup(sbspTag.Root);

                        //Read
                        reader.BaseStream.Seek((uint)sbspTag.PostProcessedOffset, SeekOrigin.Begin);
                        tagGroup.Read(reader);

                        //Write
                        using (VirtualStream tagStream = new VirtualStream(bspTagDataStream.Position))
                        using (BinaryReader tagReader = tagStream.CreateReader())
                        using (BinaryWriter tagWriter = tagStream.CreateWriter())
                        {
                            //Setup Header
                            bspHeader.StructureBspOffset = (uint)tagStream.MemoryAddress;

                            //Write
                            tagGroup.Write(tagWriter);

                            //Re-point raws
                            sbspTag.Raws.RecalculateRawAddresses(HaloTags.sbsp, tagStream, tagReader, tagWriter);

                            //Get buffer
                            sbspTagBuffer = tagStream.ToArray();
                        }

                        //Setup tag
                        sbspTag.SetObjectEntry(new ObjectEntry()
                        {
                            Tag = sbspTag.Root,
                            Id = sbspTag.Id,
                            Offset = 0,
                            Size = 0,
                        });
                        sbspTag.PostProcessedOffset = (int)bspTagDataStream.Position;
                        sbspTag.PostProcessedSize = sbspTagBuffer.Length;
                    }

                    //Write structure bsp tag buffer
                    writer.Write(sbspTagBuffer);

                    //Check ltmp tag
                    if (ltmpTag != null)
                    {
                        //Create tag group
                        tagGroup = TagLookup.CreateTagGroup(ltmpTag.Root);

                        //Read
                        reader.BaseStream.Seek((uint)ltmpTag.PostProcessedOffset, SeekOrigin.Begin);
                        tagGroup.Read(reader);

                        //Write
                        using (VirtualStream tagStream = new VirtualStream(bspTagDataStream.Position))
                        using (BinaryReader tagReader = tagStream.CreateReader())
                        using (BinaryWriter tagWriter = tagStream.CreateWriter())
                        {
                            //Setup Header
                            bspHeader.StructureLightmapOffset = (uint)tagStream.MemoryAddress;

                            //Write
                            tagGroup.Write(tagWriter);

                            //Re-calculate raw addresses
                            ltmpTag.Raws.RecalculateRawAddresses(HaloTags.ltmp, tagStream, tagReader, tagWriter);

                            //Get buffer
                            ltmpTagBuffer = tagStream.ToArray();
                        }

                        //Setup tag
                        ltmpTag.SetObjectEntry(new ObjectEntry()
                        {
                            Tag = ltmpTag.Root,
                            Id = ltmpTag.Id,
                            Offset = 0,
                            Size = 0,
                        });
                        ltmpTag.PostProcessedOffset = (int)bspTagDataStream.Position;
                        ltmpTag.PostProcessedSize = ltmpTagBuffer.Length;
                    }

                    //Write lightmap tag buffer
                    writer.Write(ltmpTagBuffer);

                    //Align
                    bspTagDataStream.Align(1024);

                    //Get block length
                    bspHeader.BlockLength = (int)bspTagDataStream.Length;

                    //Write header
                    bspTagDataStream.Seek(bspTagDataStream.MemoryAddress, SeekOrigin.Begin);
                    writer.Write(bspHeader);

                    //Check
                    if (bspTagDataStream.Length > bspLength)
                        bspLength = bspTagDataStream.Length;

                    //Swap
                    map.SwapBspTagBuffer(bspTagDataStream.ToArray(), i, bspTagDataStream.MemoryAddress);
                }

            //Write remaining tags
            tagDataStart = bspTagDataStart + bspLength;
            long originalTagDataStart = map.IndexEntries.First.Offset;
            using (VirtualStream tagDataStream = new VirtualStream(tagDataStart))
            using (BinaryReader reader = map.TagDataStream.CreateReader())
            using (BinaryWriter writer = tagDataStream.CreateWriter())
            {
                //Loop
                foreach (IndexEntry tag in map.IndexEntries.Where(t => t.TagData == map.TagDataStream))
                {
                    //Create tag group
                    tagGroup = TagLookup.CreateTagGroup(tag.Root);

                    //Read
                    reader.BaseStream.Seek((uint)tag.PostProcessedOffset, SeekOrigin.Begin);
                    tagGroup.Read(reader);

                    //Write
                    using (VirtualStream tagStream = new VirtualStream(tagDataStream.Position))
                    using (BinaryReader tagReader = tagStream.CreateReader())
                    using (BinaryWriter tagWriter = tagStream.CreateWriter())
                    {
                        //Write
                        tagGroup.Write(tagWriter);
                        
                        //Re-calculate raw addresses
                        tag.Raws.RecalculateRawAddresses(tag.Root, tagStream, tagReader, tagWriter);

                        //Get buffer
                        tagBuffer = tagStream.ToArray();
                    }

                    //Setup tag
                    tag.SetObjectEntry(new ObjectEntry()
                    {
                        Tag = tag.Root,
                        Id = tag.Id,
                        Offset = (uint)tagDataStream.Position,
                        Size = (uint)tagBuffer.Length,
                    });

                    //Write
                    writer.Write(tagBuffer);
                }

                //Align to 1024
                tagDataStream.Align(1024);

                //Swap tag buffer
                map.SwapTagBuffer(tagDataStream.ToArray(), tagDataStream.MemoryAddress);
            }
        }

        public static void ChangeSoundGestalt(this MapFile map, TagId targetId)
        {
            //Check
            if (map.IndexEntries.Last.Root != HaloTags.ugh_) return;
            VirtualStream tagData = map.Globals.TagData;
            IndexEntry globals = map.Globals;

            //Create IO
            using (BinaryReader reader = tagData.CreateReader())
            using (BinaryWriter writer = tagData.CreateWriter())
            {
                //Goto sound globals
                tagData.Seek(globals.Offset + 192);
                TagBlock soundGlobalsBlock = reader.Read<TagBlock>();

                //Check
                if (soundGlobalsBlock.Count > 0)
                {
                    //Write
                    tagData.Seek(soundGlobalsBlock.Offset + 32);
                    writer.Write(targetId);
                }
            }
        }

        public static IndexEntry GetSoundGestaltEntry(this MapFile map)
        {
            //Check
            if (map.Globals == null) return null;
            TagId soundGestaltId = TagId.Null;

            //Create IO
            using (BinaryReader reader = map.TagDataStream.CreateReader())
            using (BinaryWriter writer = map.TagDataStream.CreateWriter())
            {
                //Goto sound globals
                map.TagDataStream.Seek(map.Globals.Offset + 192);
                TagBlock soundGlobalsBlock = reader.Read<TagBlock>();

                //Check
                if (soundGlobalsBlock.Count > 0)
                {
                    //Write
                    map.TagDataStream.Seek(soundGlobalsBlock.Offset + 32);
                    soundGestaltId = reader.ReadInt32();
                }
            }

            //Return
            return map.IndexEntries[soundGestaltId];
        }
    }
}
