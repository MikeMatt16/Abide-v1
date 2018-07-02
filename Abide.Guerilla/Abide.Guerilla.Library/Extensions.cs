using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Abide.Guerilla.Library
{
    internal static class SystemIOExtensions
    {
        /// <summary>
        /// Reads the specified object from the underlying stream and advances the current position of the stream the length of the given type.
        /// <typeparamref name="T"/> must be a value type.
        /// </summary>
        /// <typeparam name="T">A value type structure type to read.</typeparam>
        /// <param name="reader">The <see cref="BinaryReader"/> instance to read the object from the underlying stream.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static T Read<T>(this BinaryReader reader) where T : struct
        {
            //Prepare
            GCHandle handle = new GCHandle();
            T instance = new T();
            byte[] data = null;
            int length = 0;
            try { length = Marshal.SizeOf(typeof(T)); }
            catch (ArgumentException ex) { throw new ArgumentException($"Unable to determine length of given type. ({typeof(T).FullName})", nameof(T), ex); }

            //Read
            data = reader.ReadBytes(length);

            //Check
            if (data != null)
            {
                try
                {
                    handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                    instance = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
                }
                finally { if (handle.IsAllocated) handle.Free(); }
            }

            //Return
            return instance;
        }
    }
}
