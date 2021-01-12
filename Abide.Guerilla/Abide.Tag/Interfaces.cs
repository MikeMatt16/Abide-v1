using Abide.HaloLibrary;
using System.Collections.Generic;
using System.IO;

namespace Abide.Tag
{
    /// <summary>
    /// Represents a tag group object.
    /// </summary>
    public interface ITagGroup : IReadWrite, IEnumerable<ITagBlock>
    {
        /// <summary>
        /// Gets and returns the number of <see cref="ITagBlock"/> elements within the <see cref="ITagGroup"/>.
        /// </summary>
        int TagBlockCount { get; }
        /// <summary>
        /// Gets or sets a tag block within the the <see cref="ITagGroup"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the tag block.</param>
        /// <returns>A <see cref="ITagBlock"/> instance if one is found at the given index, otherwise <see langword="null"/>.</returns>
        ITagBlock this[int index] { get; }
        /// <summary>
        /// Gets and returns the name of the <see cref="ITagGroup"/>.
        /// </summary>
        string GroupName { get; }
        /// <summary>
        /// Gets and returns the group tag of the <see cref="ITagGroup"/>.
        /// </summary>
        TagFourCc GroupTag { get; }
    }

    /// <summary>
    /// Represents a tag block object.
    /// </summary>
    public interface ITagBlock : IReadWrite, IEnumerable<ITagField>
    {
        /// <summary>
        /// Gets and returns the tag field at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the tag field.</param>
        /// <returns>A <see cref="ITagField"/> instance.</returns>
        ITagField this[int index] { get; }
        /// <summary>
        /// Gets and returns the number of fields within the tag block.
        /// </summary>
        int FieldCount { get; }
        /// <summary>
        /// Gets and returns the size of the <see cref="ITagBlock"/>.
        /// </summary>
        int Size { get; }
        /// <summary>
        /// Gets and returns the maximum element count of the <see cref="ITagBlock"/>.
        /// </summary>
        int MaximumElementCount { get; }
        /// <summary>
        /// Gets and returns the alignment of the <see cref="ITagBlock"/>.
        /// </summary>
        int Alignment { get; }
        /// <summary>
        /// Gets and returns the name of the <see cref="ITagBlock"/>.
        /// </summary>
        string BlockName { get; }
        /// <summary>
        /// Gets and returns the display name of the <see cref="ITagBlock"/>.
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// Initializes the <see cref="ITagBlock"/>.
        /// </summary>
        void Initialize();
        /// <summary>
        /// Writes any late data using the specified binary writer.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> used to post-write the tag block data to the underlying stream.</param>
        void PostWrite(BinaryWriter writer);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ITagField : IReadWrite
    {
        /// <summary>
        /// 
        /// </summary>
        FieldType Type { get; }
        /// <summary>
        /// 
        /// </summary>
        object Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 
        /// </summary>
        string Information { get; }
        /// <summary>
        /// 
        /// </summary>
        string Details { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsReadOnly { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsBlockName { get; }
        /// <summary>
        /// 
        /// </summary>
        int Size { get; }
        /// <summary>
        /// 
        /// </summary>
        object GetValue();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SetValue(object value);
    }

    /// <summary>
    /// Represents an object that can be read from a stream using a <see cref="BinaryReader"/> object.
    /// </summary>
    public interface IReadable
    {
        /// <summary>
        /// Reads the value of the object from the underlying stream using the specified binary reader.
        /// </summary>
        /// <param name="reader">The binary reader used to access the underlying stream.</param>
        void Read(BinaryReader reader);
    }
    /// <summary>
    /// Represents an object that can be written to a stream using a <see cref="BinaryWriter"/> object.
    /// </summary>
    public interface IWritable
    {
        /// <summary>
        /// Writes the value of the object to the underlying stream using the specified binary writer.
        /// </summary>
        /// <param name="writer">The binary writer used to access the underlying stream.</param>
        void Write(BinaryWriter writer);
    }
    /// <summary>
    /// Represents an object that can be both written to a stream using a <see cref="BinaryWriter"/> object,
    /// and read from a stream using a <see cref="BinaryReader"/> object.
    /// </summary>
    public interface IReadWrite : IReadable, IWritable { }
}
