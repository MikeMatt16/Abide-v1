using Abide.Guerilla.Tags;

namespace System.IO
{
    /// <summary>
    /// Me want more bananas.
    /// </summary>
    public static class GuerillaExtensions
    {
        /// <summary>
        /// Reads a data structure from the current stream and advances the position of the stream by the size of the data structure.
        /// </summary>
        /// <typeparam name="T">The readable data structure type.</typeparam>
        /// <param name="reader">The binary reader used to read the data structure from the underlying stream.</param>
        /// <returns>An instance of the data structure.</returns>
        public static T ReadDataStructure<T>(this BinaryReader reader) where T : IReadable, new()
        {
            //Prepare
            T dataStructure = new T();

            //Read
            dataStructure.Read(reader);

            //Return
            return dataStructure;
        }

        /// <summary>
        /// Writes a data structure to the current stream and advances the position of the stream by the size of the data structure.
        /// </summary>
        /// <typeparam name="T">The writable data structure type.</typeparam>
        /// <param name="writer">The binary writer used to write the data structure to the underlying stream.</param>
        /// <param name="dataStructure">The data structure to write to the stream.</param>
        public static void Write<T>(this BinaryWriter writer, T dataStructure) where T : IWritable
        {
            //Write
            dataStructure.Write(writer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="blockType"></param>
        public static void ReadBlock(this BinaryReader reader, Type blockType)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="block"></param>
        public static void ReadBlock(this BinaryReader reader, BlockAttribute block)
        {
        }
    }
}
