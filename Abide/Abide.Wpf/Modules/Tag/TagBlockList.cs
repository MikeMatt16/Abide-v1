using Abide.Tag;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Abide.Wpf.Modules.Tag
{
    public sealed class TagBlockList : IEnumerable<Block>
    {
        private readonly List<Block> blocks = new List<Block>();

        public int Count { get; private set; }
        public string BlockName { get; private set; }

        public TagBlockList() { }
        public void Load(Stream inStream, TagContext context)
        {
            if (!inStream.CanRead) return;
            if (!inStream.CanSeek) return;
            blocks.Clear();

            using (BinaryReader reader = new BinaryReader(inStream))
            {
                BlockName = reader.ReadUTF8NullTerminated();
                Count = reader.ReadInt32();

                for (int i = 0; i < Count; i++)
                {
                    Block block = context.CreateBlock(BlockName);
                    block.Read(reader);
                    blocks.Add(block);
                }
            }
        }
        public void Save(Stream outStream)
        {
            if (!outStream.CanWrite) return;
            if (!outStream.CanRead) return;
            if (!outStream.CanSeek) return;

            using (BinaryWriter writer = new BinaryWriter(outStream))
            {
                writer.WriteUTF8NullTerminated(BlockName);
                writer.Write(Count);

                foreach (var block in blocks)
                {
                    block.Write(writer);
                }

                foreach (var block in blocks)
                {
                    block.PostWrite(writer);
                }
            }
        }
        public IEnumerator<Block> GetEnumerator()
        {
            return blocks.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static TagBlockList FromBlockField(BlockField blockField, int index, int count)
        {
            int addedCount = 0;
            var block = blockField.Create();
            var data = new TagBlockList() { BlockName = block.Name };

            for (int i = 0; i < count; i++)
            {
                int currentIndex = index + i;
                if (currentIndex < blockField.BlockList.Count)
                {
                    data.blocks.Add((Block)blockField.BlockList[currentIndex].Clone());
                    addedCount++;
                }
            }

            data.Count = addedCount;
            return data;
        }
    }
}
