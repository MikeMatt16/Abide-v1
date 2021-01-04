using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Abide.HaloLibrary.Halo2.Retail.Tag
{
    public abstract class Group : ITagGroup, IEnumerable<Block>
    {
        public int TagBlockCount => TagBlocks.Count;
        public abstract string GroupName { get; }
        public abstract TagFourCc GroupTag { get; }
        public List<Block> TagBlocks { get; } = new List<Block>();
        public long GroupAddress { get; private set; } = 0;
        protected Group() { }
        protected virtual void Dispose(bool disposing)
        {
            TagBlocks.ForEach(b => b.Dispose());
            TagBlocks.Clear();
        }
        public virtual void Read(BinaryReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            GroupAddress = reader.BaseStream.Position;

            foreach (var block in TagBlocks)
            {
                block.Read(reader);
            }
        }
        public virtual void Write(BinaryWriter writer)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            GroupAddress = writer.BaseStream.Position;

            foreach (var block in TagBlocks)
            {
                block.Write(writer);
            }

            foreach (var block in TagBlocks)
            {
                block.PostWrite(writer);
            }
        }
        public virtual void Overwrite(BinaryWriter writer)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            foreach (var block in TagBlocks)
            {
                block.Overwrite(writer);
            }

            foreach (var block in TagBlocks)
            {
                block.PostOverwrite(writer);
            }
        }
        public override string ToString()
        {
            return GroupName ?? base.ToString();
        }
        public void Dispose()
        {
            Dispose(true);
        }
        public IEnumerator<Block> GetEnumerator()
        {
            return TagBlocks.GetEnumerator();
        }

        ITagBlock ITagGroup.this[int index]
        {
            get { return TagBlocks[index]; }
        }
        string ITagGroup.GroupName => GroupName;
        TagFourCc ITagGroup.GroupTag => GroupTag;
        void IReadable.Read(BinaryReader reader)
        {
            Read(reader);
        }
        void IWritable.Write(BinaryWriter writer)
        {
            Write(writer);
        }
        IEnumerator<ITagBlock> IEnumerable<ITagBlock>.GetEnumerator()
        {
            foreach (ITagBlock tagBlock in TagBlocks)
                yield return tagBlock;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
