using Abide.HaloLibrary;
using Abide.Tag;
using Abide.Tag.Guerilla.Generated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Abide.Guerilla.Library
{
    /// <summary>
    /// Represents a tag group file.
    /// </summary>
    public sealed class TagGroupFile
    {
        /// <summary>
        /// Gets and returns the number of raws in the tag group file.
        /// </summary>
        public int RawDataCount => rawData.Count;
        /// <summary>
        /// Gets or sets the tag group.
        /// </summary>
        public ITagGroup TagGroup { get; set; }
        /// <summary>
        /// Gets or sets the tag ID.
        /// </summary>
        public TagId Id { get; set; }

        private readonly Dictionary<int, byte[]> rawData;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagGroupFile"/> class.
        /// </summary>
        public TagGroupFile()
        {
            //Prepare
            rawData = new Dictionary<int, byte[]>();
        }
        /// <summary>
        /// Reads a tag group file from the specified file name.
        /// </summary>
        /// <param name="filename">The path to the file that contains the tag group file.</param>
        public void Load(string filename)
        {
            //Load
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                Load(fs);
        }
        /// <summary>
        /// Reads a tag group file from the specified stream.
        /// </summary>
        /// <param name="inStream">The stream that contains the tag file group.</param>
        public void Load(Stream inStream)
        {
            //Check
            if (!inStream.CanRead) return;
            if (!inStream.CanSeek) return;
            if (inStream.Length < TagGroupHeader.Size) return;

            //Clear
            rawData.Clear();
            TagGroup = null;

            //Prepare reader
            using (BinaryReader reader = new BinaryReader(inStream))
            {
                //Read header
                TagGroupHeader header = reader.Read<TagGroupHeader>();

                //Check
                inStream.Seek(TagGroupHeader.Size, SeekOrigin.Begin);
                if ((TagGroup = TagLookup.CreateTagGroup(header.GroupTag.FourCc)) != null)
                        TagGroup.Read(reader);

                //Read raws
                if(header.RawsCount > 0)
                {
                    //Read Raw Offsets
                    int[] rawOffsets = new int[header.RawsCount];
                    inStream.Seek(header.RawOffsetsOffset, SeekOrigin.Begin);
                    for (int i = 0; i < header.RawsCount; i++)
                        rawOffsets[i] = reader.ReadInt32();

                    //Read Raw lengths
                    int[] lengths = new int[header.RawsCount];
                    inStream.Seek(header.RawLengthsOffset, SeekOrigin.Begin);
                    for (int i = 0; i < header.RawsCount; i++)
                        lengths[i] = reader.ReadInt32();

                    //Read Raws
                    inStream.Seek(header.RawDataOffset, SeekOrigin.Begin);
                    for (int i = 0; i < header.RawsCount; i++)
                        if (!rawData.ContainsKey(rawOffsets[i])) rawData.Add(rawOffsets[i], reader.ReadBytes(lengths[i]));
                }
            }
        }
        /// <summary>
        /// Writes the tag group file to the speicified file name.
        /// </summary>
        /// <param name="filename">The path to the file to write the tag group file to.</param>
        public void Save(string filename)
        {
            //Load
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                Save(fs);
        }
        /// <summary>
        /// Writes a tag group file to the specified stream.
        /// </summary>
        /// <param name="outStream">The stream to write the tag group file to.</param>
        public void Save(Stream outStream)
        {
            //Prepare header
            TagGroupHeader header = new TagGroupHeader
            {
                GroupTag = TagGroup.GroupTag,
                RawsCount = (uint)rawData.Count,
                Id = new TagId(Id),
            };
            
            //Create writer
            using (BinaryWriter writer = new BinaryWriter(outStream))
            using (BinaryReader reader = new BinaryReader(outStream))
            {
                //Skip header
                outStream.Seek(TagGroupHeader.Size, SeekOrigin.Current);

                //Write tag group
                TagGroup.Write(writer);

                //Check
                if (rawData.Count > 0)
                {
                    //Write offsets
                    header.RawOffsetsOffset = (uint)outStream.Align(512);
                    foreach (int offset in rawData.Keys)
                        writer.Write(offset);

                    //Write Lengths
                    header.RawLengthsOffset = (uint)outStream.Align(512);
                    foreach (byte[] datum in rawData.Values)
                        writer.Write(datum.Length);

                    //Write Data
                    header.RawDataOffset = (uint)outStream.Align(512);
                    foreach (byte[] datum in rawData.Values)
                        writer.Write(datum);
                }

                //Align
                int count = (int)(outStream.Align(512) - TagGroupHeader.Size) / 4;

                //Get checksum
                outStream.Seek(TagGroupHeader.Size, SeekOrigin.Begin);
                for (int i = 0; i < count; i++)
                    header.Checksum ^= reader.ReadUInt32();

                //Write header
                outStream.Seek(0, SeekOrigin.Begin);
                writer.Write(header);
            }
        }
        /// <summary>
        /// Gets and returns an array of raw offsets contained within the tag.
        /// </summary>
        /// <returns>An array of raw offsets.</returns>
        public int[] GetRawOffsets()
        {
            return rawData.Keys.ToArray();
        }
        /// <summary>
        /// Gets a raw data buffer from the specified raw offset.
        /// </summary>
        /// <param name="rawOffset">The offset to the raw data.</param>
        /// <returns>An array of <see cref="byte"/> elements that contain the raw data if one is found at the given offset; otherwise, <see langword="null"/>.</returns>
        public byte[] GetRaw(int rawOffset)
        {
            //Return
            if (rawData.ContainsKey(rawOffset))
                return rawData[rawOffset];
            else return null;
        }
        /// <summary>
        /// Sets a raw data buffer using the specified raw offset and desired raw data buffer.
        /// </summary>
        /// <param name="rawOffset">The offset of the raw data.</param>
        /// <param name="rawData">The raw data buffer.</param>
        public void SetRaw(int rawOffset, byte[] rawData)
        {
            //Check
            if (rawData == null) throw new ArgumentNullException(nameof(rawData));

            //Set or add
            if (this.rawData.ContainsKey(rawOffset)) this.rawData[rawOffset] = rawData;
            else this.rawData.Add(rawOffset, rawData);
        }
        /// <summary>
        /// Returns a string representation of the current instance.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            if (TagGroup != null) return $"0x{Id.Dword:X8} {TagGroup.Name}";
            else return base.ToString();
        }
    }
}
