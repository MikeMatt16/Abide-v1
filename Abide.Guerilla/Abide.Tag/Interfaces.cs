using Abide.HaloLibrary;
using System;
using System.Collections.Generic;
using System.IO;

namespace Abide.Tag
{
    public interface ITagGroup : IReadWrite, IEnumerable<ITagBlock>
    {
        ITagBlock this[int index] { get; }
        int TagBlockCount { get; }
        string Name { get; }
        TagFourCc Tag { get; }
    }

    public interface ITagBlock : IReadWrite, IEnumerable<ITagField>, ICloneable
    {
        ITagField this[int index] { get; set; }
        int FieldCount { get; }
        int Size { get; }
        int MaximumElementCount { get; }
        int Alignment { get; }
        string BlockName { get; }
        string DisplayName { get; }
        void Initialize();
    }

    public interface ITagField : IReadWrite, ICloneable
    {
        FieldType Type { get; }
        object Value { get; set; }
        string Name { get; }
        object GetValue();
        bool SetValue(object value);
    }

    public interface IReadable
    {
        void Read(BinaryReader reader);
    }

    public interface IWritable
    {
        void Write(BinaryWriter writer);
    }

    public interface IReadWrite : IReadable, IWritable { }
}
