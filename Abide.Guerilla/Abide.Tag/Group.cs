using Abide.HaloLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Abide.Tag
{
    public abstract class Group : ITagGroup, IEnumerable<Block>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int TagBlockCount => TagBlocks.Count;
        public abstract string Name { get; }
        public abstract TagFourCc Tag { get; }
        public ObservableCollection<Block> TagBlocks { get; } = new ObservableCollection<Block>();
        public long GroupAddress { get; private set; } = 0;
        protected Group() { }
        public IEnumerator<Block> GetEnumerator()
        {
            return TagBlocks.GetEnumerator();
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
        }
        public override string ToString()
        {
            return Name ?? base.ToString();
        }

        ITagBlock ITagGroup.this[int index]
        {
            get { return TagBlocks[index]; }
        }
        string ITagGroup.Name => Name;
        TagFourCc ITagGroup.Tag => Tag;
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
