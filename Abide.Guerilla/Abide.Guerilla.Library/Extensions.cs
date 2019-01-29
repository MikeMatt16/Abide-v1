using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Abide.Guerilla.Library
{
    public static class Enumerable
    {
        /// <summary>
        /// Gets the first index of an element in an enumerable.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> to check.</param>
        /// <returns>A zero-based index of the element if the source sequence contains an appropriate element; otherwise, -1.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            //Check
            if (source == null) throw new ArgumentNullException(nameof(source));

            int index = 0;
            foreach (TSource item in source)
            {
                if (predicate.Invoke(item)) return index;
                index++;
            }
            return -1;
        }
    }
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
