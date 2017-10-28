using System.IO;

namespace Abide.Guerilla.Tags
{
    /// <summary>
    /// Represents a data structure with a fixed length.
    /// </summary>
    public interface IDataStructure
    {
        /// <summary>
        /// Gets and returns the size of the data structure.
        /// </summary>
        int Size { get; }
        /// <summary>
        /// Initializes all values within the data structure.
        /// </summary>
        void Initialize();
    }

    /// <summary>
    /// Represents a data structure that provides methods of reading the data from a stream.
    /// </summary>
    public interface IReadable : IDataStructure
    {
        /// <summary>
        /// Reads the data structure using the supplied binary reader.
        /// </summary>
        /// <param name="reader">The binary reader used to read the data structure from the underlying stream.</param>
        void Read(BinaryReader reader);
    }

    /// <summary>
    /// Represents a data structure that provides methods of writing the data to a stream.
    /// </summary>
    public interface IWritable : IDataStructure
    {
        /// <summary>
        /// Writes the data stucture using the supplied binary writer.
        /// </summary>
        /// <param name="writer">The binary writer used to write the data structure to the underlying stream.</param>
        void Write(BinaryWriter writer);
    }
}
