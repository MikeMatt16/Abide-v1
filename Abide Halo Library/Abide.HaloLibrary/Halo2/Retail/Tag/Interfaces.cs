using System;
using System.Collections.Generic;
using System.IO;

namespace Abide.HaloLibrary.Halo2.Retail.Tag
{
    internal interface ITagGroup : IReadWrite, IDisposable, IEnumerable<ITagBlock>
    {
        int TagBlockCount { get; }
        ITagBlock this[int index] { get; }
        string GroupName { get; }
        TagFourCc GroupTag { get; }
    }

    internal interface ITagBlock : IReadWrite, IDisposable, IEnumerable<ITagField>
    {
        ITagField this[int index] { get; }
        int FieldCount { get; }
        int Size { get; }
        int MaximumElementCount { get; }
        int Alignment { get; }
        string BlockName { get; }
        string DisplayName { get; }
        void Initialize();
        void PostWrite(BinaryWriter writer);
    }

    internal interface ITagField : IReadWrite, IDisposable
    {
        FieldType Type { get; }
        string Name { get; }
        string Information { get; }
        string Details { get; }
        bool IsReadOnly { get; }
        bool IsBlockName { get; }
        int Size { get; }
        object Value { get; set; }
    }

    internal interface IReadable
    {
        void Read(BinaryReader reader);
    }
    internal interface IWritable
    {
        void Write(BinaryWriter writer);
    }
    internal interface IReadWrite : IReadable, IWritable { }
}
