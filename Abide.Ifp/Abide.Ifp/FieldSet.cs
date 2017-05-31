using System.Collections.Generic;
using System.IO;

namespace Abide.Ifp
{
    /// <summary>
    /// Represents a set of IronForgePlugin fields.
    /// </summary>
    public sealed class FieldSet
    {
        /// <summary>
        /// Gets and returns the length of the field set.
        /// </summary>
        public int Length
        {
            get
            {
                int length = 0;
                fields.ForEach(f => length += f.Length - f.Offset);
                return length;
            }
        }

        private readonly List<IfpField> fields;

        /// <summary>
        /// Initializes a new <see cref="FieldSet"/> instance.
        /// </summary>
        public FieldSet()
        {
            //Initialize
            fields = new List<IfpField>();
        }
        /// <summary>
        /// Adds a <see cref="IfpNode"/> instance to the field set.
        /// </summary>
        /// <param name="node">Ifp node to add.</param>
        public void Add(IfpNode node)
        {
            //Filter type...
            switch (node.Type)
            {
                case IfpNodeType.TagBlock:
                case IfpNodeType.Buffer:
                case IfpNodeType.Byte:
                case IfpNodeType.SignedByte:
                case IfpNodeType.Short:
                case IfpNodeType.UnsignedShort:
                case IfpNodeType.Int:
                case IfpNodeType.UnsignedInt:
                case IfpNodeType.Long:
                case IfpNodeType.UnsignedLong:
                case IfpNodeType.Single:
                case IfpNodeType.Double:
                case IfpNodeType.Enumerator8:
                case IfpNodeType.Enumerator16:
                case IfpNodeType.Enumerator32:
                case IfpNodeType.Enumerator64:
                case IfpNodeType.Bitfield8:
                case IfpNodeType.Bitfield16:
                case IfpNodeType.Bitfield32:
                case IfpNodeType.Bitfield64:
                case IfpNodeType.String32:
                case IfpNodeType.String64:
                case IfpNodeType.Unicode128:
                case IfpNodeType.Unicode256:
                case IfpNodeType.Tag:
                case IfpNodeType.TagId:
                case IfpNodeType.StringId:
                    int offset = Length;
                    if (node.UseFieldOffset) offset -= node.FieldOffset;
                    IfpField field = new IfpField(node.Type, offset);
                    if (node.Type == IfpNodeType.Buffer) field.Length = node.Length;
                    fields.Add(field);
                    break;
            }
        }
        /// <summary>
        /// Reads the current field set and returns a byte array.
        /// </summary>
        /// <param name="reader">The reader to read the data.</param>
        /// <returns>A byte array to contain the field data.</returns>
        public byte[] ReadSet(BinaryReader reader)
        {
            //Prepare
            byte[] buffer = new byte[Length];

            //Create
            using (MemoryStream ms = new MemoryStream(buffer))
            using (BinaryWriter writer = new BinaryWriter(ms))
                foreach (var field in fields)
                {
                    ms.Seek(field.Offset, SeekOrigin.Current);
                    reader.BaseStream.Seek(field.Offset, SeekOrigin.Current);
                    writer.Write(reader.ReadBytes(field.Length));
                }

            //Return
            return buffer;
        }

        /// <summary>
        /// Represents a single Ifp field.
        /// </summary>
        private struct IfpField
        {
            /// <summary>
            /// Gets and returns the field type.
            /// </summary>
            public IfpNodeType Type
            {
                get { return type; }
            }
            /// <summary>
            /// Gets and returns the field offset.
            /// </summary>
            public int Offset
            {
                get { return offset; }
            }
            /// <summary>
            /// Gets and returns the length of the field.
            /// </summary>
            public int Length
            {
                get { return length; }
                set { length = value; }
            }

            private readonly IfpNodeType type;
            private readonly int offset;
            private int length;

            public IfpField(IfpNodeType type, int offset)
            {
                //Setup
                this.type = type;
                this.offset = offset;

                //Handle type
                switch (type)
                {
                    case IfpNodeType.Byte:
                    case IfpNodeType.SignedByte:
                    case IfpNodeType.Enumerator8:
                    case IfpNodeType.Bitfield8: length = 1; break;

                    case IfpNodeType.Short:
                    case IfpNodeType.UnsignedShort:
                    case IfpNodeType.Enumerator16:
                    case IfpNodeType.Bitfield16: length = 2; break;

                    case IfpNodeType.Int:
                    case IfpNodeType.UnsignedInt:
                    case IfpNodeType.Single:
                    case IfpNodeType.Enumerator32:
                    case IfpNodeType.Bitfield32:
                    case IfpNodeType.Tag:
                    case IfpNodeType.TagId:
                    case IfpNodeType.StringId: length = 4; break;

                    case IfpNodeType.Long:
                    case IfpNodeType.UnsignedLong:
                    case IfpNodeType.Enumerator64:
                    case IfpNodeType.Bitfield64:
                    case IfpNodeType.TagBlock:
                    case IfpNodeType.Double: length = 8; break;

                    case IfpNodeType.String32: length = 32; break;
                    case IfpNodeType.String64: length = 64; break;
                    case IfpNodeType.Unicode128: length = 128; break;
                    case IfpNodeType.Unicode256: length = 256; break;
                    default: length = 0; break;
                }
            }
            public IfpField(IfpNodeType type, int offset, int length)
            {
                //Setup
                this.type = type;
                this.offset = offset;

                //Handle type
                switch (type)
                {
                    case IfpNodeType.Byte:
                    case IfpNodeType.SignedByte:
                    case IfpNodeType.Enumerator8:
                    case IfpNodeType.Bitfield8: this.length = 1; break;

                    case IfpNodeType.Short:
                    case IfpNodeType.UnsignedShort:
                    case IfpNodeType.Enumerator16:
                    case IfpNodeType.Bitfield16: this.length = 2; break;

                    case IfpNodeType.Int:
                    case IfpNodeType.UnsignedInt:
                    case IfpNodeType.Single:
                    case IfpNodeType.Enumerator32:
                    case IfpNodeType.Bitfield32:
                    case IfpNodeType.Tag:
                    case IfpNodeType.TagId:
                    case IfpNodeType.StringId: this.length = 4; break;

                    case IfpNodeType.Long:
                    case IfpNodeType.UnsignedLong:
                    case IfpNodeType.Enumerator64:
                    case IfpNodeType.Bitfield64:
                    case IfpNodeType.TagBlock:
                    case IfpNodeType.Double: this.length = 8; break;

                    case IfpNodeType.String32: this.length = 32; break;
                    case IfpNodeType.String64: this.length = 64; break;
                    case IfpNodeType.Unicode128: this.length = 128; break;
                    case IfpNodeType.Unicode256: this.length = 256; break;
                    default: this.length = length; break;
                }
            }
            public override string ToString()
            {
                return $"{type} 0x{offset:X} {length * 8} bits";
            }
        }
    }
}
