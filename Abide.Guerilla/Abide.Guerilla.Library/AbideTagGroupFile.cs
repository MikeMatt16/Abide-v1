using Abide.Tag;
using Abide.Tag.Guerilla.Generated;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Abide.Guerilla.Library
{
    public sealed class AbideTagGroupFile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Dictionary<long, byte[]> resources = new Dictionary<long, byte[]>();

        public int ResourceCount => resources.Count;
        public Group TagGroup { get; set; }
        public AbideTagGroupFile() { }
        public void Load(string filename)
        {
            Load(File.OpenRead(filename));
        }
        public void Load(Stream inStream)
        {
            if (!inStream.CanRead) return;
            if (!inStream.CanSeek) return;
            if (inStream.Length < TagGroupHeader.Size) return;
            resources.Clear();
            TagGroup = null;

            using (BinaryReader reader = new BinaryReader(inStream))
            {
                TagGroupHeader header = reader.Read<TagGroupHeader>();
                inStream.Seek(TagGroupHeader.Size, SeekOrigin.Begin);
                if ((TagGroup = TagLookup.CreateTagGroup(header.GroupTag)) != null)
                        TagGroup.Read(reader);

                if(header.TagResourceCount > 0)
                {
                    int[] rawOffsets = new int[header.TagResourceCount];
                    inStream.Seek(header.RawOffsetsOffset, SeekOrigin.Begin);
                    for (int i = 0; i < header.TagResourceCount; i++)
                        rawOffsets[i] = reader.ReadInt32();

                    int[] lengths = new int[header.TagResourceCount];
                    inStream.Seek(header.RawLengthsOffset, SeekOrigin.Begin);
                    for (int i = 0; i < header.TagResourceCount; i++)
                        lengths[i] = reader.ReadInt32();

                    inStream.Seek(header.RawDataOffset, SeekOrigin.Begin);
                    for (int i = 0; i < header.TagResourceCount; i++)
                        if (!resources.ContainsKey(rawOffsets[i])) resources.Add(rawOffsets[i], reader.ReadBytes(lengths[i]));
                }
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TagGroup)));
        }
        public void Save(string filename)
        {
            Save(File.OpenWrite(filename));
        }
        public void Save(Stream outStream)
        {
            TagGroupHeader header = new TagGroupHeader
            {
                AbideTag = "atag",
                GroupTag = TagGroup.Tag,
                TagResourceCount = (uint)resources.Count,
            };
            
            using (BinaryWriter writer = new BinaryWriter(outStream))
            using (BinaryReader reader = new BinaryReader(outStream))
            {
                outStream.Seek(TagGroupHeader.Size, SeekOrigin.Current);    // skip header
                TagGroup.Write(writer);

                if (resources.Count > 0)
                {
                    header.RawOffsetsOffset = (uint)outStream.Align(512);
                    foreach (int offset in resources.Keys)
                        writer.Write(offset);

                    header.RawLengthsOffset = (uint)outStream.Align(512);
                    foreach (byte[] resource in resources.Values)
                        writer.Write(resource.Length);

                    header.RawDataOffset = (uint)outStream.Align(512);
                    foreach (byte[] datum in resources.Values)
                        writer.Write(datum);
                }

                int count = (int)(outStream.Align(16) - TagGroupHeader.Size) / 4;

                outStream.Seek(TagGroupHeader.Size, SeekOrigin.Begin);
                for (int i = 0; i < count; i++)
                    header.Checksum ^= reader.ReadUInt32();

                outStream.Seek(0, SeekOrigin.Begin);
                writer.Write(header);
            }
        }
        public long[] GetResourceAddresses()
        {
            return resources.Keys.ToArray();
        }
        public byte[] GetResource(long address)
        {
            if (resources.ContainsKey(address))
                return (byte[])resources[address].Clone();
            
            return new byte[0];
        }
        public void SetResource(long address, byte[] data)
        {
            if (data == null && resources.ContainsKey(address))
            {
                resources.Remove(address);
            }

            if (resources.ContainsKey(address))
            {
                resources[address] = data;
            }
            else
            {
                resources.Add(address, data);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResourceCount)));
            }
        }
        public override string ToString()
        {
            if (TagGroup != null) return $"{TagGroup.Name}";
            else return base.ToString();
        }
    }
}
