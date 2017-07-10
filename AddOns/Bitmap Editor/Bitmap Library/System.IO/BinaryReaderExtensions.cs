using System.Runtime.InteropServices;

namespace System.IO
{
    /// <summary>
    /// Contains helper functions related to the <see cref="System.IO"/> namespace.
    /// </summary>
    internal static class IONamespaceExtensions
    {
        /// <summary>
        /// Reads the specified structure from the underlying stream and advances the current position of the stream by the length of the structure.
        /// </summary>
        /// <typeparam name="T">The marshalable structure type.</typeparam>
        /// <param name="reader">The <see cref="BinaryReader"/> instance to read from the underlying stream.</param>
        /// <returns>A structure containing the read data.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal static T ReadStructure<T>(this BinaryReader reader)
        {
            //Prepare
            T obj = default(T);
            GCHandle handle = new GCHandle();
            byte[] data = null;

            //Get data...
            data = reader.ReadBytes(Marshal.SizeOf(typeof(T)));

            //Check
            if (data != null)
            {
                handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                obj = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
                handle.Free();
            }

            //Return
            return obj;
        }
        /// <summary>
        /// Writes the specified structure to the underlying stream and advances the current position of the stream by the length of the structure.
        /// </summary>
        /// <typeparam name="T">The marshalable structure type.</typeparam>
        /// <param name="writer">The <see cref="BinaryWriter"/> instance used to write to the underlying stream.</param>
        /// <param name="structure">The structure to write.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static void Write<T>(this BinaryWriter writer, T structure)
        {
            //Prepare
            int length = Marshal.SizeOf(structure);

            //Check
            if (length > 0)
            {
                //Allocate
                IntPtr addr = Marshal.AllocHGlobal(length);
                Marshal.StructureToPtr(structure, addr, true);

                //Copy
                byte[] data = new byte[length];
                Marshal.Copy(addr, data, 0, length);

                //Release
                Marshal.FreeHGlobal(addr);

                //Write
                writer.Write(data);
            }
        }
    }
}
