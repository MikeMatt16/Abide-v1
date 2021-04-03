using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Abide.Tag
{
    public class Block : ITagBlock, IEnumerable<Field>, IEquatable<Block>
    {
        private string name = string.Empty;
        private string displayName = string.Empty;
        private int alignment = 4;
        private int maximumElementCount = 0;

        public ObservableCollection<Field> Fields { get; } = new ObservableCollection<Field>();
        public long BlockAddress { get; private set; } = 0;
        public int Size => GetBlockSize();
        public int FieldCount => Fields.Count;
        public virtual int Alignment
        {
            get => alignment;
            private set => alignment = value;
        }
        public virtual int MaximumElementCount
        {
            get => maximumElementCount;
            private set => maximumElementCount = value;
        }
        public virtual string Name
        {
            get => name;
            private set => name = value;
        }
        public virtual string DisplayName
        {
            get => displayName;
            private set => displayName = value;
        }
        public string Label
        {
            get => GetLabel();
        }

        protected Block() { }
        public object Clone()
        {
            Block b = new Block()
            {
                name = Name,
                displayName = DisplayName,
                alignment = Alignment,
                maximumElementCount = MaximumElementCount,
                BlockAddress = BlockAddress,
            };

            foreach (var field in Fields)
            {
                b.Fields.Add((Field)field.Clone());
            }

            return b;
        }
        public bool Equals(Block other)
        {
            bool equals = Fields.Count == other.Fields.Count && Name == other.Name;

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
                                equals &= ((Block)f1.GetValue()).Equals((Block)f2.GetValue());
                                break;
                            case FieldType.FieldPad:
                                PadField pf1 = (PadField)f1;
                                PadField pf2 = (PadField)f2;
                                for (int j = 0; j < pf1.Length; j++)
                                    if (equals) equals &= pf1.Data[j] == pf2.Data[j];
                                break;
                            default:
                                if (f1.GetValue() == null && f2.GetValue() == null) continue;
                                else equals &= f1.GetValue().Equals(f2.GetValue());
                                break;
                        }
                }

            return equals;
        }
        public override string ToString()
        {
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
        protected virtual string GetLabel()
        {
            if (Fields.Any(f => f.IsBlockName))
            {
                return string.Join(", ", Fields
                    .Where(f => f.IsBlockName)
                    .Select(f => f.GetValueString()));
            }

            return DisplayName;
        }
        private int GetBlockSize()
        {
            int size = Fields.Sum(f => f.Size);
            return size;
        }
        public virtual IEnumerator<Field> GetEnumerator()
        {
            return Fields.GetEnumerator();
        }

        ITagField ITagBlock.this[int index]
        {
            get => Fields[index];
            set
            {
                if (value is Field field)
                {
                    Fields[index] = field;
                }

                throw new ArgumentException("Invalid field.", nameof(value));
            }
        }
        int ITagBlock.Size => Size;
        int ITagBlock.MaximumElementCount => MaximumElementCount;
        int ITagBlock.Alignment => Alignment;
        string ITagBlock.BlockName => Name;
        string ITagBlock.DisplayName => DisplayName;
        void ITagBlock.Initialize()
        {
            Initialize();
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
