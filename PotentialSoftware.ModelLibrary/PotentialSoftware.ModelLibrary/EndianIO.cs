using System;
using System.IO;

namespace PotentialSoftware.ModelLibrary
{
    /// <summary>
    /// Provides a set of <see langword="static"/> (<see langword="Shared"/> in Visual Basic) methods for reading and writing data with a different byte-order than the machine running the code.
    /// </summary>
    internal static class EndianIO
    {
        /// <summary>
        /// Returns 
        /// </summary>
        public static Endianness SystemEndianness
        {
            get { return BitConverter.IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian; }
        }
        /// <summary>
        /// Reads a 2-byte signed integer from the current stream using the specified byte order and advances the current position of the stream by two bytes.
        /// </summary>
        /// <param name="reader">The binary reader used to read the integer from the current stream.</param>
        /// <param name="byteOrder">The byte order of the data to read.</param>
        /// <returns>A signed 16-bit integer.</returns>
        public static short ReadInt16(this BinaryReader reader, Endianness byteOrder)
        {
            byte[] bytes = reader.ReadBytes(2);
            if (byteOrder != SystemEndianness)
                Array.Reverse(bytes);
            return Convert.ToInt16(bytes);
        }
        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream using the specified byte order and advances the current position of the stream by two bytes.
        /// </summary>
        /// <param name="reader">The binary reader used to read the integer from the current stream.</param>
        /// <param name="byteOrder">The byte order of the data to read.</param>
        /// <returns>An unsigned 16-bit integer.</returns>
        public static ushort ReadUInt16(this BinaryReader reader, Endianness byteOrder)
        {
            byte[] bytes = reader.ReadBytes(2);
            if (byteOrder != SystemEndianness)
                Array.Reverse(bytes);
            return Convert.ToUInt16(bytes);
        }
        /// <summary>
        /// Reads a 4-byte signed integer from the current stream using the specified byte order and advances the current position of the stream by four bytes.
        /// </summary>
        /// <param name="reader">The binary reader used to read the integer from the current stream.</param>
        /// <param name="byteOrder">The byte order of the data to read.</param>
        /// <returns>A signed 32-bit integer.</returns>
        public static int ReadInt32(this BinaryReader reader, Endianness byteOrder)
        {
            byte[] bytes = reader.ReadBytes(4);
            if (byteOrder != SystemEndianness)
                Array.Reverse(bytes);
            return Convert.ToInt32(bytes);
        }
        /// <summary>
        /// Reads a 4-byte unsigned integer from the current stream using the specified byte order and advances the current position of the stream by four bytes.
        /// </summary>
        /// <param name="reader">The binary reader used to read the integer from the current stream.</param>
        /// <param name="byteOrder">The byte order of the data to read.</param>
        /// <returns>An unsigned 32-bit integer.</returns>
        public static uint ReadUInt32(this BinaryReader reader, Endianness byteOrder)
        {
            byte[] bytes = reader.ReadBytes(4);
            if (byteOrder != SystemEndianness)
                Array.Reverse(bytes);
            return Convert.ToUInt32(bytes);
        }
        /// <summary>
        /// Reads an 8-byte signed integer from the current stream using the specified byte order and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <param name="reader">The binary reader used to read the integer from the current stream.</param>
        /// <param name="byteOrder">The byte order of the data to read.</param>
        /// <returns>A signed 64-bit integer.</returns>
        public static long ReadInt64(this BinaryReader reader, Endianness byteOrder)
        {
            byte[] bytes = reader.ReadBytes(8);
            if (byteOrder != SystemEndianness)
                Array.Reverse(bytes);
            return Convert.ToInt64(bytes);
        }
        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream using the specified byte order and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <param name="reader">The binary reader used to read the integer from the current stream.</param>
        /// <param name="byteOrder">The byte order of the data to read.</param>
        /// <returns>An unsigned 64-bit integer.</returns>
        public static ulong ReadUInt64(this BinaryReader reader, Endianness byteOrder)
        {
            byte[] bytes = reader.ReadBytes(8);
            if (byteOrder != SystemEndianness)
                Array.Reverse(bytes);
            return Convert.ToUInt64(bytes);
        }
        /// <summary>
        /// Reads a 4-byte floating point value from the current stream using the specified byte order and advances the current position of the stream by four bytes.
        /// </summary>
        /// <param name="reader">The binary reader used to read the floating point from the current stream.</param>
        /// <param name="byteOrder">The byte order of the data to read.</param>
        /// <returns>A single precision floating point number.</returns>
        public static float ReadSingle(this BinaryReader reader, Endianness byteOrder)
        {
            byte[] bytes = reader.ReadBytes(4);
            if (byteOrder != SystemEndianness)
                Array.Reverse(bytes);
            return Convert.ToSingle(bytes);
        }
        /// <summary>
        /// Reads an 8-byte floating point value from the current stream using the specified byte order and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <param name="reader">The binary reader used to read the floating point from the current stream.</param>
        /// <param name="byteOrder">The byte order of the data to read.</param>
        /// <returns>A double precision floating point number.</returns>
        public static double ReadDouble(this BinaryReader reader, Endianness byteOrder)
        {
            byte[] bytes = reader.ReadBytes(8);
            if (byteOrder != SystemEndianness)
                Array.Reverse(bytes);
            return Convert.ToDouble(bytes);
        }
    }

    /// <summary>
    /// Represents an enumeration containing possible endianess types.
    /// </summary>
    internal enum Endianness
    {
        /// <summary>
        /// Big endian byte order.
        /// 0xA0B0C0D0 -memory-> { 0xA0, 0xB0, 0xC0, 0xD0 }
        /// </summary>
        BigEndian,
        /// <summary>
        /// Little endian byte order.
        /// 0xA0B0C0D0 -memory-> { 0xD0, 0xC0, 0xB0, 0xA0 }
        /// </summary>
        LittleEndian
    }
}
