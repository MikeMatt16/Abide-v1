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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(12, 4)]
    [Abide.Guerilla.Tags.TagGroupAttribute("multiplayer_scenario_description", 1836084345u, 4294967293u, typeof(MultiplayerScenarioDescriptionBlock))]
    public sealed class MultiplayerScenarioDescriptionBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
    {
        [Abide.Guerilla.Tags.FieldAttribute("multiplayer scenarios", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("scenario_description_block", 32, typeof(ScenarioDescriptionBlock))]
        public TagBlock MultiplayerScenarios;
        public int Size
        {
            get
            {
                return 12;
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
        [Abide.Guerilla.Tags.FieldSetAttribute(68, 4)]
        public sealed class ScenarioDescriptionBlock : Abide.Guerilla.Tags.IReadable, Abide.Guerilla.Tags.IWritable
        {
            [Abide.Guerilla.Tags.FieldAttribute("descriptive bitmap", typeof(TagReference))]
            public TagReference DescriptiveBitmap;
            [Abide.Guerilla.Tags.FieldAttribute("displayed map name", typeof(TagReference))]
            public TagReference DisplayedMapName;
            [Abide.Guerilla.Tags.FieldAttribute("scenario tag directory path#this is the path to the directory containing the scen" +
                "ario tag file of the same name", typeof(String32))]
            public String32 ScenarioTagDirectoryPath;
            [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
            [Abide.Guerilla.Tags.PaddingAttribute(4)]
            public Byte[] EmptyString;
            public int Size
            {
                get
                {
                    return 68;
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
}
#pragma warning restore CS1591