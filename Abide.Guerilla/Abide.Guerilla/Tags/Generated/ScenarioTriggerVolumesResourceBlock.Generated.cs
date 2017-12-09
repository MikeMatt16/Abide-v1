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
    
    [FieldSetAttribute(24, 4)]
    [TagGroupAttribute("scenario_trigger_volumes_resource", 1953654570u, 4294967293u, typeof(ScenarioTriggerVolumesResourceBlock))]
    public sealed class ScenarioTriggerVolumesResourceBlock : AbideTagBlock
    {
        private TagBlockList<ScenarioTriggerVolumeBlock> killTriggerVolumesList = new TagBlockList<ScenarioTriggerVolumeBlock>(256);
        private TagBlockList<ScenarioObjectNamesBlock> objectNamesList = new TagBlockList<ScenarioObjectNamesBlock>(640);
        [FieldAttribute("Kill Trigger Volumes", typeof(TagBlock))]
        [BlockAttribute("scenario_trigger_volume_block", 256, typeof(ScenarioTriggerVolumeBlock))]
        public TagBlock KillTriggerVolumes;
        [FieldAttribute("Object Names", typeof(TagBlock))]
        [BlockAttribute("scenario_object_names_block", 640, typeof(ScenarioObjectNamesBlock))]
        public TagBlock ObjectNames;
        public TagBlockList<ScenarioTriggerVolumeBlock> KillTriggerVolumesList
        {
            get
            {
                return this.killTriggerVolumesList;
            }
        }
        public TagBlockList<ScenarioObjectNamesBlock> ObjectNamesList
        {
            get
            {
                return this.objectNamesList;
            }
        }
        public override int Size
        {
            get
            {
                return 24;
            }
        }
        public override void Initialize()
        {
            this.killTriggerVolumesList.Clear();
            this.objectNamesList.Clear();
            this.KillTriggerVolumes = TagBlock.Zero;
            this.ObjectNames = TagBlock.Zero;
        }
        public override void Read(BinaryReader reader)
        {
            this.KillTriggerVolumes = reader.ReadInt64();
            this.killTriggerVolumesList.Read(reader, this.KillTriggerVolumes);
            this.ObjectNames = reader.ReadInt64();
            this.objectNamesList.Read(reader, this.ObjectNames);
        }
        public override void Write(BinaryWriter writer)
        {
        }
        [FieldSetAttribute(68, 4)]
        public sealed class ScenarioTriggerVolumeBlock : AbideTagBlock
        {
            [FieldAttribute("Name^", typeof(StringId))]
            public StringId Name;
            [FieldAttribute("Object Name", typeof(Int16))]
            public Int16 ObjectName;
            [FieldAttribute("", typeof(Byte[]))]
            [PaddingAttribute(2)]
            public Byte[] EmptyString;
            [FieldAttribute("Node Name", typeof(StringId))]
            public StringId NodeName;
            [FieldAttribute("EMPTY STRING", typeof(EmptyStringElement[]))]
            [ArrayAttribute(6, typeof(EmptyStringElement))]
            public EmptyStringElement[] EmptyString1;
            [FieldAttribute("Position", typeof(Vector3))]
            public Vector3 Position;
            [FieldAttribute("Extents", typeof(Vector3))]
            public Vector3 Extents;
            [FieldAttribute("", typeof(Byte[]))]
            [PaddingAttribute(4)]
            public Byte[] EmptyString2;
            [FieldAttribute("~Kill Trigger Volume*", typeof(Int16))]
            public Int16 KillTriggerVolume;
            [FieldAttribute("", typeof(Byte[]))]
            [PaddingAttribute(2)]
            public Byte[] EmptyString3;
            public override int Size
            {
                get
                {
                    return 68;
                }
            }
            public override void Initialize()
            {
                this.Name = StringId.Zero;
                this.ObjectName = 0;
                this.EmptyString = new byte[2];
                this.NodeName = StringId.Zero;
                this.EmptyString1 = new EmptyStringElement[6];
                this.Position = Vector3.Zero;
                this.Extents = Vector3.Zero;
                this.EmptyString2 = new byte[4];
                this.KillTriggerVolume = 0;
                this.EmptyString3 = new byte[2];
            }
            public override void Read(BinaryReader reader)
            {
                this.Name = reader.ReadInt32();
                this.ObjectName = reader.ReadInt16();
                this.EmptyString = reader.ReadBytes(2);
                this.NodeName = reader.ReadInt32();
                this.Position = reader.Read<Vector3>();
                this.Extents = reader.Read<Vector3>();
                this.EmptyString2 = reader.ReadBytes(4);
                this.KillTriggerVolume = reader.ReadInt16();
                this.EmptyString3 = reader.ReadBytes(2);
            }
            public override void Write(BinaryWriter writer)
            {
            }
            public sealed class EmptyStringElement : AbideTagBlock
            {
                [FieldAttribute("EMPTY STRING", typeof(Single))]
                public Single EmptyString;
                public override int Size
                {
                    get
                    {
                        return 0;
                    }
                }
                public override void Initialize()
                {
                    this.EmptyString = 0;
                }
                public override void Read(BinaryReader reader)
                {
                    this.EmptyString = reader.ReadSingle();
                }
                public override void Write(BinaryWriter writer)
                {
                }
            }
        }
        [FieldSetAttribute(36, 4)]
        public sealed class ScenarioObjectNamesBlock : AbideTagBlock
        {
            [FieldAttribute("Name^", typeof(String32))]
            public String32 Name;
            [FieldAttribute("EMPTY STRING", typeof(Int16))]
            public Int16 EmptyString;
            [FieldAttribute("EMPTY STRING", typeof(Int16))]
            public Int16 EmptyString1;
            public override int Size
            {
                get
                {
                    return 36;
                }
            }
            public override void Initialize()
            {
                this.Name = String32.Empty;
                this.EmptyString = 0;
                this.EmptyString1 = 0;
            }
            public override void Read(BinaryReader reader)
            {
                this.Name = reader.Read<String32>();
                this.EmptyString = reader.ReadInt16();
                this.EmptyString1 = reader.ReadInt16();
            }
            public override void Write(BinaryWriter writer)
            {
            }
        }
    }
}
#pragma warning restore CS1591
