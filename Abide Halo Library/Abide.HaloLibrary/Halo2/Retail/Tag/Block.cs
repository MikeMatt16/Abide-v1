using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Abide.HaloLibrary.Halo2.Retail.Tag
{
    internal class Block : ITagBlock, IEnumerable<Field>, IEquatable<Block>
    {
        public List<Field> Fields { get; } = new List<Field>();
        public int Size => GetBlockSize();
        public int FieldCount => Fields.Count;
        public virtual int Alignment => 4;
        public virtual int MaximumElementCount => 0;
        public virtual string BlockName => string.Empty;
        public virtual string DisplayName => string.Empty;
        public long BlockAddress { get; private set; } = 0;
        protected Block() { }
        public bool Equals(Block other)
        {
            bool equals = Fields.Count == other.Fields.Count && BlockName == other.BlockName;

            if (equals)
                for (int i = 0; i < Fields.Count; i++)
                {
                    if (!equals) break;
                    Field f1 = Fields[i], f2 = other.Fields[i];
                    equals &= f1.Type == f2.Type;
                    if (equals)
                        switch (Fields[i].Type)
                        {
                            case FieldType.FieldBlock:
                                BlockField bf1 = (BlockField)f1;
                                BlockField bf2 = (BlockField)f2;
                                equals &= bf1.BlockList.Count == bf2.BlockList.Count;
                                if (equals)
                                    for (int j = 0; j < bf1.BlockList.Count; j++)
                                        if (equals) equals = bf1.BlockList[j].Equals(bf2.BlockList[j]);
                                break;
                            case FieldType.FieldStruct:
                                equals &= ((Block)f1.Value).Equals((Block)f2.Value);
                                break;
                            case FieldType.FieldPad:
                                PadField pf1 = (PadField)f1;
                                PadField pf2 = (PadField)f2;
                                for (int j = 0; j < pf1.Length; j++)
                                    if (equals) equals &= ((byte[])pf1.Value)[j] == ((byte[])pf2.Value)[j];
                                break;
                            default:
                                if (f1.Value == null && f2.Value == null) continue;
                                else equals &= f1.Value.Equals(f2.Value);
                                break;
                        }
                }

            return equals;
        }
        public override string ToString()
        {
            if (Fields.Any(f => f.IsBlockName)) return string.Join(", ",
                Fields.Where(f => f.IsBlockName).Select(f => f.Value.ToString()).ToArray());
            return DisplayName;
        }
        public virtual void Initialize() { }
        public virtual void Read(BinaryReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            BlockAddress = reader.BaseStream.Position;

            foreach (Field field in Fields)
            {
                field.Read(reader);
            }
        }
        public virtual void Write(BinaryWriter writer)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            BlockAddress = writer.BaseStream.Position;

            foreach (Field field in Fields)
            {
                field.Write(writer);
            }
        }
        public virtual void Overwrite(BinaryWriter writer)
        {
            foreach (Field field in Fields)
            {
                field.Overwrite(writer);
            }
        }
        public virtual void PostWrite(BinaryWriter writer)
        {
            foreach (Field field in Fields)
            {
                field.PostWrite(writer);
            }
        }
        private int GetBlockSize()
        {
            int size = 0;
            Fields.ForEach(f => size += f.Size);
            return size;
        }
        public virtual IEnumerator<Field> GetEnumerator()
        {
            return Fields.GetEnumerator();
        }
        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            Fields.ForEach(f => f.Dispose());
            Fields.Clear();
        }

        ITagField ITagBlock.this[int index]
        {
            get { return Fields[index]; }
        }
        int ITagBlock.Size => Size;
        int ITagBlock.MaximumElementCount => MaximumElementCount;
        int ITagBlock.Alignment => Alignment;
        string ITagBlock.BlockName => BlockName;
        string ITagBlock.DisplayName => DisplayName;
        void ITagBlock.Initialize()
        {
            Initialize();
        }
        void ITagBlock.PostWrite(BinaryWriter writer)
        {
            PostWrite(writer ?? throw new ArgumentNullException(nameof(writer)));
        }
        void IReadable.Read(BinaryReader reader)
        {
            Read(reader ?? throw new ArgumentNullException(nameof(reader)));
        }
        void IWritable.Write(BinaryWriter writer)
        {
            Write(writer ?? throw new ArgumentNullException(nameof(writer)));
        }
        IEnumerator<ITagField> IEnumerable<ITagField>.GetEnumerator()
        {
            foreach (var field in Fields)
                yield return field;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
