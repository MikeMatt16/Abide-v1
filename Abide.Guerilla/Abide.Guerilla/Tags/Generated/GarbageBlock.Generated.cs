#pragma warning disable CS1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.Guerilla.Tags
{
    using Abide.Guerilla.Types;
    using Abide.HaloLibrary;
    using System;
    using System.IO;
    
    [FieldSetAttribute(168, 4)]
    [TagGroupAttribute("garbage", 1734439522u, 1769235821u, typeof(GarbageBlock))]
    public sealed class GarbageBlock : AbideTagBlock
    {
        [FieldAttribute("", typeof(Byte[]))]
        [PaddingAttribute(168)]
        public Byte[] EmptyString;
        public override int Size
        {
            get
            {
                return 168;
            }
        }
        public override void Initialize()
        {
            this.EmptyString = new byte[168];
        }
        public override void Read(BinaryReader reader)
        {
            this.EmptyString = reader.ReadBytes(168);
        }
        public override void Write(BinaryWriter writer)
        {
        }
    }
}
#pragma warning restore CS1591
