using Abide.HaloLibrary.Halo2Map;
using System;
using System.IO;

namespace Abide.HaloLibrary.Builder
{
    /// <summary>
    /// Provides a set of <c>static</c> (<c>Shared</c> in Visual Basic) methods for building tag data into a <see cref="MapFile"/>.
    /// </summary>
    public static class MapBuilder
    {
        /// <summary>
        /// Attempts to create a new tag using the supplied map file, tag four-character code, filename, and length.
        /// </summary>
        /// <param name="map">The map file to create a tag.</param>
        /// <param name="tagFourCc">The tag four-character code.</param>
        /// <param name="filename">The tag file name.</param>
        /// <param name="pad">The amount to pad the offset of the new tag to.</param>
        /// <param name="length">The length of the tag data.</param>
        /// <param name="tagId">If the function returns true, the new tag ID; otherwise null.</param>
        /// <returns>true if a new tag was successfully created; otherwise false.</returns>
        public static bool CreateTag(this MapFile map, string tagFourCc, string filename, uint pad, uint length, out TagId tagId)
        {
            //Prepare
            bool success = false;
            tagId = TagId.Null;

            //Check
            if (map == null) throw new ArgumentNullException(nameof(map));
            if (tagFourCc == null) throw new ArgumentNullException(nameof(tagFourCc));
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            if (string.IsNullOrEmpty(tagFourCc)) throw new ArgumentException("Tag string cannot be blank.", nameof(tagFourCc));
            if (string.IsNullOrEmpty(filename)) throw new ArgumentException("Tag file name cannot be blank.", nameof(tagFourCc));
            if (!map.Tags.ContainsTag(tagFourCc)) throw new ArgumentException($"Map does not contain any tags matching {tagFourCc}");
            if (tagFourCc == HaloTags.sbsp) throw new ArgumentException($"Cannot add new tag of type {HaloTags.sbsp}.");
            if (tagFourCc == HaloTags.ltmp) throw new ArgumentException($"Cannot add new tag of type {HaloTags.ltmp}.");
            if (map.IndexEntries.Count < 2) throw new InvalidOperationException("Invalid map file.");

            //Get Last Tag
            IndexEntry last = map.IndexEntries.Last;

            //Get Tag
            TagHierarchy tag = map.Tags[tagFourCc];
            TagId id = GetNextId(map.IndexEntries.Last.Id);

            //Get Offsets
            uint newOffset = last.Offset.PadTo(pad);
            uint coconutsOffset = (newOffset + length).PadTo(4);
            uint totalShift = coconutsOffset - last.Offset;

            //Shfit raw addresses
            foreach (var container in last.Raws[RawSection.Sound])
            {
                for (int i = 0; i < container.OffsetAddresses.Count; i++)
                    container.OffsetAddresses[i] = container.OffsetAddresses[i] + totalShift;
                for (int i = 0; i < container.LengthAddresses.Count; i++)
                    container.LengthAddresses[i] = container.LengthAddresses[i] + totalShift;
            }
            foreach (var container in last.Raws[RawSection.LipSync])
            {
                for (int i = 0; i < container.OffsetAddresses.Count; i++)
                    container.OffsetAddresses[i] = container.OffsetAddresses[i] + totalShift;
                for (int i = 0; i < container.LengthAddresses.Count; i++)
                    container.LengthAddresses[i] = container.LengthAddresses[i] + totalShift;
            }

            //Shift Coconuts
            ShiftCoconuts(last, totalShift);

            //Return
            return success;
        }

        private static void ShiftCoconuts(IndexEntry coconuts, uint shift)
        {
            //Goto
            coconuts.TagData.Seek(coconuts.Offset);

            //Initialize IO
            using (BinaryReader reader = new BinaryReader(coconuts.TagData))
            using (BinaryWriter writer = new BinaryWriter(coconuts.TagData))
            {
                TagBlock Playbacks = reader.ReadInt64();
                TagBlock Scales = reader.ReadInt64();
                TagBlock ImportNames = reader.ReadInt64();
                TagBlock PitchRangeParameters = reader.ReadInt64();
                TagBlock PitchRanges = reader.ReadInt64();
                TagBlock Permutations = reader.ReadInt64();
                TagBlock CustomPlaybacks = reader.ReadInt64();
                TagBlock RuntimePermutationFlags = reader.ReadInt64();
                TagBlock Chunks = reader.ReadInt64();
                TagBlock Promotions = reader.ReadInt64();
                TagBlock ExtraInfos = reader.ReadInt64();
                TagBlock[] PromotionRules = new TagBlock[Promotions.Count];
                TagBlock[] EmptyString = new TagBlock[Promotions.Count];
                TagBlock[] EncodedPermutationSection = new TagBlock[ExtraInfos.Count];
                TagBlock[][] EncodedData = new TagBlock[ExtraInfos.Count][];
                TagBlock[][] SoundDialogueInfo = new TagBlock[ExtraInfos.Count][];
            }
        }

        private static TagId GetNextId(TagId id)
        {
            //Get Dword
            uint idDword = id.Dword;

            ushort idLow = (ushort)((idDword & 0xFFFF) + 1);
            ushort idHi = (ushort)((idDword >> 16 & 0xFFFF) + 1);

            //Return
            return idLow | (idHi << 16);
        }
    }
}
