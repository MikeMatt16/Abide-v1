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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(40, 4)]
    [Abide.Guerilla.Tags.TagGroupAttribute("sound_dialogue_constants", 1936747297u, 4294967293u, typeof(SoundDialogueConstantsBlock))]
    public sealed class SoundDialogueConstantsBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
    {
        [Abide.Guerilla.Tags.FieldAttribute("almost never", typeof(Single))]
        public Single AlmostNever;
        [Abide.Guerilla.Tags.FieldAttribute("rarely", typeof(Single))]
        public Single Rarely;
        [Abide.Guerilla.Tags.FieldAttribute("somewhat", typeof(Single))]
        public Single Somewhat;
        [Abide.Guerilla.Tags.FieldAttribute("often", typeof(Single))]
        public Single Often;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(24)]
        public Byte[] EmptyString;
        public int Size
        {
            get
            {
                return 40;
            }
        }
        public void Initialize()
        {
        }
        public void Read(System.IO.BinaryReader reader)
        {
        }
        public void Write(System.IO.BinaryWriter writer)
        {
        }
    }
}
#pragma warning restore CS1591