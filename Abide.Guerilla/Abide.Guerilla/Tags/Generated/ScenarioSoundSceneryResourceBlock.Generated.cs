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
    
    [FieldSetAttribute(76, 4)]
    [TagGroupAttribute("scenario_sound_scenery_resource", 712205157u, 4294967293u, typeof(ScenarioSoundSceneryResourceBlock))]
    public sealed class ScenarioSoundSceneryResourceBlock : AbideTagBlock
    {
        private TagBlockList<ScenarioObjectNamesBlock> namesList = new TagBlockList<ScenarioObjectNamesBlock>(640);
        private TagBlockList<DontUseMeScenarioEnvironmentObjectBlock> list = new TagBlockList<DontUseMeScenarioEnvironmentObjectBlock>(4096);
        private TagBlockList<ScenarioStructureBspReferenceBlock> structureReferencesList = new TagBlockList<ScenarioStructureBspReferenceBlock>(16);
        private TagBlockList<ScenarioSoundSceneryPaletteBlock> paletteList = new TagBlockList<ScenarioSoundSceneryPaletteBlock>(256);
        private TagBlockList<ScenarioSoundSceneryBlock> objectsList = new TagBlockList<ScenarioSoundSceneryBlock>(256);
        private TagBlockList<GScenarioEditorFolderBlock> editorFoldersList = new TagBlockList<GScenarioEditorFolderBlock>(32767);
        [FieldAttribute("Names", typeof(TagBlock))]
        [BlockAttribute("scenario_object_names_block", 640, typeof(ScenarioObjectNamesBlock))]
        public TagBlock Names;
        [FieldAttribute("*", typeof(TagBlock))]
        [BlockAttribute("dont_use_me_scenario_environment_object_block", 4096, typeof(DontUseMeScenarioEnvironmentObjectBlock))]
        public TagBlock EmptyString;
        [FieldAttribute("Structure References", typeof(TagBlock))]
        [BlockAttribute("scenario_structure_bsp_reference_block", 16, typeof(ScenarioStructureBspReferenceBlock))]
        public TagBlock StructureReferences;
        [FieldAttribute("Palette", typeof(TagBlock))]
        [BlockAttribute("scenario_sound_scenery_palette_block", 256, typeof(ScenarioSoundSceneryPaletteBlock))]
        public TagBlock Palette;
        [FieldAttribute("Objects", typeof(TagBlock))]
        [BlockAttribute("scenario_sound_scenery_block", 256, typeof(ScenarioSoundSceneryBlock))]
        public TagBlock Objects;
        [FieldAttribute("Next Object ID Salt*", typeof(Int32))]
        public Int32 NextObjectIdSalt;
        [FieldAttribute("Editor Folders*", typeof(TagBlock))]
        [BlockAttribute("g_scenario_editor_folder_block", 32767, typeof(GScenarioEditorFolderBlock))]
        public TagBlock EditorFolders;
        public TagBlockList<ScenarioObjectNamesBlock> NamesList
        {
            get
            {
                return this.namesList;
            }
        }
        public TagBlockList<DontUseMeScenarioEnvironmentObjectBlock> List
        {
            get
            {
                return this.list;
            }
        }
        public TagBlockList<ScenarioStructureBspReferenceBlock> StructureReferencesList
        {
            get
            {
                return this.structureReferencesList;
            }
        }
        public TagBlockList<ScenarioSoundSceneryPaletteBlock> PaletteList
        {
            get
            {
                return this.paletteList;
            }
        }
        public TagBlockList<ScenarioSoundSceneryBlock> ObjectsList
        {
            get
            {
                return this.objectsList;
            }
        }
        public TagBlockList<GScenarioEditorFolderBlock> EditorFoldersList
        {
            get
            {
                return this.editorFoldersList;
            }
        }
        public override int Size
        {
            get
            {
                return 76;
            }
        }
        public override void Initialize()
        {
            this.namesList.Clear();
            this.list.Clear();
            this.structureReferencesList.Clear();
            this.paletteList.Clear();
            this.objectsList.Clear();
            this.editorFoldersList.Clear();
            this.Names = TagBlock.Zero;
            this.EmptyString = TagBlock.Zero;
            this.StructureReferences = TagBlock.Zero;
            this.Palette = TagBlock.Zero;
            this.Objects = TagBlock.Zero;
            this.NextObjectIdSalt = 0;
            this.EditorFolders = TagBlock.Zero;
        }
        public override void Read(BinaryReader reader)
        {
            this.Names = reader.ReadInt64();
            this.namesList.Read(reader, this.Names);
            this.EmptyString = reader.ReadInt64();
            this.list.Read(reader, this.EmptyString);
            this.StructureReferences = reader.ReadInt64();
            this.structureReferencesList.Read(reader, this.StructureReferences);
            this.Palette = reader.ReadInt64();
            this.paletteList.Read(reader, this.Palette);
            this.Objects = reader.ReadInt64();
            this.objectsList.Read(reader, this.Objects);
            this.NextObjectIdSalt = reader.ReadInt32();
            this.EditorFolders = reader.ReadInt64();
            this.editorFoldersList.Read(reader, this.EditorFolders);
        }
        public override void Write(BinaryWriter writer)
        {
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
        [FieldSetAttribute(64, 4)]
        public sealed class DontUseMeScenarioEnvironmentObjectBlock : AbideTagBlock
        {
            [FieldAttribute("BSP*", typeof(Int16))]
            public Int16 Bsp;
            [FieldAttribute("EMPTY STRING", typeof(Int16))]
            public Int16 EmptyString;
            [FieldAttribute("Unique ID*", typeof(Int32))]
            public Int32 UniqueId;
            [FieldAttribute("", typeof(Byte[]))]
            [PaddingAttribute(4)]
            public Byte[] EmptyString1;
            [FieldAttribute("Object Definition Tag*", typeof(Tag))]
            public Tag ObjectDefinitionTag;
            [FieldAttribute("Object*^", typeof(Int32))]
            public Int32 Object;
            [FieldAttribute("", typeof(Byte[]))]
            [PaddingAttribute(44)]
            public Byte[] EmptyString2;
            public override int Size
            {
                get
                {
                    return 64;
                }
            }
            public override void Initialize()
            {
                this.Bsp = 0;
                this.EmptyString = 0;
                this.UniqueId = 0;
                this.EmptyString1 = new byte[4];
                this.ObjectDefinitionTag = "null";
                this.Object = 0;
                this.EmptyString2 = new byte[44];
            }
            public override void Read(BinaryReader reader)
            {
                this.Bsp = reader.ReadInt16();
                this.EmptyString = reader.ReadInt16();
                this.UniqueId = reader.ReadInt32();
                this.EmptyString1 = reader.ReadBytes(4);
                this.ObjectDefinitionTag = reader.Read<Tag>();
                this.Object = reader.ReadInt32();
                this.EmptyString2 = reader.ReadBytes(44);
            }
            public override void Write(BinaryWriter writer)
            {
            }
        }
        [FieldSetAttribute(84, 4)]
        public sealed class ScenarioStructureBspReferenceBlock : AbideTagBlock
        {
            [FieldAttribute("", typeof(Byte[]))]
            [PaddingAttribute(16)]
            public Byte[] EmptyString;
            [FieldAttribute("Structure BSP^", typeof(TagReference))]
            public TagReference StructureBsp;
            [FieldAttribute("Structure Lightmap^", typeof(TagReference))]
            public TagReference StructureLightmap;
            [FieldAttribute("", typeof(Byte[]))]
            [PaddingAttribute(4)]
            public Byte[] EmptyString1;
            [FieldAttribute("UNUSED radiance est. search distance", typeof(Single))]
            public Single UnusedRadianceEstSearchDistance;
            [FieldAttribute("", typeof(Byte[]))]
            [PaddingAttribute(4)]
            public Byte[] EmptyString2;
            [FieldAttribute("UNUSED luminels per world unit", typeof(Single))]
            public Single UnusedLuminelsPerWorldUnit;
            [FieldAttribute("UNUSED output white reference", typeof(Single))]
            public Single UnusedOutputWhiteReference;
            [FieldAttribute("", typeof(Byte[]))]
            [PaddingAttribute(8)]
            public Byte[] EmptyString3;
            [FieldAttribute("Flags", typeof(FlagsOptions))]
            [OptionsAttribute(typeof(FlagsOptions), true)]
            public FlagsOptions Flags;
            [FieldAttribute("", typeof(Byte[]))]
            [PaddingAttribute(2)]
            public Byte[] EmptyString4;
            [FieldAttribute("Default Sky", typeof(Int16))]
            public Int16 DefaultSky;
            [FieldAttribute("", typeof(Byte[]))]
            [PaddingAttribute(2)]
            public Byte[] EmptyString5;
            public override int Size
            {
                get
                {
                    return 84;
                }
            }
            public override void Initialize()
            {
                this.EmptyString = new byte[16];
                this.StructureBsp = TagReference.Null;
                this.StructureLightmap = TagReference.Null;
                this.EmptyString1 = new byte[4];
                this.UnusedRadianceEstSearchDistance = 0;
                this.EmptyString2 = new byte[4];
                this.UnusedLuminelsPerWorldUnit = 0;
                this.UnusedOutputWhiteReference = 0;
                this.EmptyString3 = new byte[8];
                this.Flags = ((FlagsOptions)(0));
                this.EmptyString4 = new byte[2];
                this.DefaultSky = 0;
                this.EmptyString5 = new byte[2];
            }
            public override void Read(BinaryReader reader)
            {
                this.EmptyString = reader.ReadBytes(16);
                this.StructureBsp = reader.Read<TagReference>();
                this.StructureLightmap = reader.Read<TagReference>();
                this.EmptyString1 = reader.ReadBytes(4);
                this.UnusedRadianceEstSearchDistance = reader.ReadSingle();
                this.EmptyString2 = reader.ReadBytes(4);
                this.UnusedLuminelsPerWorldUnit = reader.ReadSingle();
                this.UnusedOutputWhiteReference = reader.ReadSingle();
                this.EmptyString3 = reader.ReadBytes(8);
                this.Flags = ((FlagsOptions)(reader.ReadInt16()));
                this.EmptyString4 = reader.ReadBytes(2);
                this.DefaultSky = reader.ReadInt16();
                this.EmptyString5 = reader.ReadBytes(2);
            }
            public override void Write(BinaryWriter writer)
            {
            }
            public enum FlagsOptions : Int16
            {
                DefaultSkyEnabled = 1,
            }
        }
        [FieldSetAttribute(48, 4)]
        public sealed class ScenarioSoundSceneryPaletteBlock : AbideTagBlock
        {
            [FieldAttribute("Name^", typeof(TagReference))]
            public TagReference Name;
            [FieldAttribute("", typeof(Byte[]))]
            [PaddingAttribute(32)]
            public Byte[] EmptyString;
            public override int Size
            {
                get
                {
                    return 48;
                }
            }
            public override void Initialize()
            {
                this.Name = TagReference.Null;
                this.EmptyString = new byte[32];
            }
            public override void Read(BinaryReader reader)
            {
                this.Name = reader.Read<TagReference>();
                this.EmptyString = reader.ReadBytes(32);
            }
            public override void Write(BinaryWriter writer)
            {
            }
        }
        [FieldSetAttribute(80, 4)]
        public sealed class ScenarioSoundSceneryBlock : AbideTagBlock
        {
            [FieldAttribute("Type", typeof(Int16))]
            public Int16 Type;
            [FieldAttribute("Name^", typeof(Int16))]
            public Int16 Name;
            [FieldAttribute("Object Data", typeof(ScenarioObjectDatumStructBlock))]
            public ScenarioObjectDatumStructBlock ObjectData;
            [FieldAttribute("sound_scenery", typeof(SoundSceneryDatumStructBlock))]
            public SoundSceneryDatumStructBlock SoundScenery;
            public override int Size
            {
                get
                {
                    return 80;
                }
            }
            public override void Initialize()
            {
                this.Type = 0;
                this.Name = 0;
                this.ObjectData = new ScenarioObjectDatumStructBlock();
                this.SoundScenery = new SoundSceneryDatumStructBlock();
            }
            public override void Read(BinaryReader reader)
            {
                this.Type = reader.ReadInt16();
                this.Name = reader.ReadInt16();
                this.ObjectData = reader.ReadDataStructure<ScenarioObjectDatumStructBlock>();
                this.SoundScenery = reader.ReadDataStructure<SoundSceneryDatumStructBlock>();
            }
            public override void Write(BinaryWriter writer)
            {
            }
            [FieldSetAttribute(48, 4)]
            public sealed class ScenarioObjectDatumStructBlock : AbideTagBlock
            {
                [FieldAttribute("Placement Flags", typeof(PlacementFlagsOptions))]
                [OptionsAttribute(typeof(PlacementFlagsOptions), true)]
                public PlacementFlagsOptions PlacementFlags;
                [FieldAttribute("Position", typeof(Vector3))]
                public Vector3 Position;
                [FieldAttribute("Rotation", typeof(Vector3))]
                public Vector3 Rotation;
                [FieldAttribute("Scale", typeof(Single))]
                public Single Scale;
                [FieldAttribute(")Transform Flags", typeof(TransformFlagsOptions))]
                [OptionsAttribute(typeof(TransformFlagsOptions), true)]
                public TransformFlagsOptions TransformFlags;
                [FieldAttribute("Manual BSP Flags*", typeof(Int16))]
                public Int16 ManualBspFlags;
                [FieldAttribute("Object ID", typeof(ScenarioObjectIdStructBlock))]
                public ScenarioObjectIdStructBlock ObjectId;
                [FieldAttribute("BSP Policy", typeof(BspPolicyOptions))]
                [OptionsAttribute(typeof(BspPolicyOptions), false)]
                public BspPolicyOptions BspPolicy;
                [FieldAttribute("", typeof(Byte[]))]
                [PaddingAttribute(1)]
                public Byte[] EmptyString;
                [FieldAttribute("Editor Folder", typeof(Int16))]
                public Int16 EditorFolder;
                public override int Size
                {
                    get
                    {
                        return 48;
                    }
                }
                public override void Initialize()
                {
                    this.PlacementFlags = ((PlacementFlagsOptions)(0));
                    this.Position = Vector3.Zero;
                    this.Rotation = Vector3.Zero;
                    this.Scale = 0;
                    this.TransformFlags = ((TransformFlagsOptions)(0));
                    this.ManualBspFlags = 0;
                    this.ObjectId = new ScenarioObjectIdStructBlock();
                    this.BspPolicy = ((BspPolicyOptions)(0));
                    this.EmptyString = new byte[1];
                    this.EditorFolder = 0;
                }
                public override void Read(BinaryReader reader)
                {
                    this.PlacementFlags = ((PlacementFlagsOptions)(reader.ReadInt32()));
                    this.Position = reader.Read<Vector3>();
                    this.Rotation = reader.Read<Vector3>();
                    this.Scale = reader.ReadSingle();
                    this.TransformFlags = ((TransformFlagsOptions)(reader.ReadInt16()));
                    this.ManualBspFlags = reader.ReadInt16();
                    this.ObjectId = reader.ReadDataStructure<ScenarioObjectIdStructBlock>();
                    this.BspPolicy = ((BspPolicyOptions)(reader.ReadByte()));
                    this.EmptyString = reader.ReadBytes(1);
                    this.EditorFolder = reader.ReadInt16();
                }
                public override void Write(BinaryWriter writer)
                {
                }
                [FieldSetAttribute(8, 4)]
                public sealed class ScenarioObjectIdStructBlock : AbideTagBlock
                {
                    [FieldAttribute("Unique ID*", typeof(Int32))]
                    public Int32 UniqueId;
                    [FieldAttribute("Origin BSP Index*", typeof(Int16))]
                    public Int16 OriginBspIndex;
                    [FieldAttribute("Type*", typeof(TypeOptions))]
                    [OptionsAttribute(typeof(TypeOptions), false)]
                    public TypeOptions Type;
                    [FieldAttribute("Source*", typeof(SourceOptions))]
                    [OptionsAttribute(typeof(SourceOptions), false)]
                    public SourceOptions Source;
                    public override int Size
                    {
                        get
                        {
                            return 8;
                        }
                    }
                    public override void Initialize()
                    {
                        this.UniqueId = 0;
                        this.OriginBspIndex = 0;
                        this.Type = ((TypeOptions)(0));
                        this.Source = ((SourceOptions)(0));
                    }
                    public override void Read(BinaryReader reader)
                    {
                        this.UniqueId = reader.ReadInt32();
                        this.OriginBspIndex = reader.ReadInt16();
                        this.Type = ((TypeOptions)(reader.ReadByte()));
                        this.Source = ((SourceOptions)(reader.ReadByte()));
                    }
                    public override void Write(BinaryWriter writer)
                    {
                    }
                    public enum TypeOptions : Byte
                    {
                        Biped = 0,
                        Vehicle = 1,
                        Weapon = 2,
                        Equipment = 3,
                        Garbage = 4,
                        Projectile = 5,
                        Scenery = 6,
                        Machine = 7,
                        Control = 8,
                        LightFixture = 9,
                        SoundScenery = 10,
                        Crate = 11,
                        Creature = 12,
                    }
                    public enum SourceOptions : Byte
                    {
                        Structure = 0,
                        Editor = 1,
                        Dynamic = 2,
                        Legacy = 3,
                    }
                }
                public enum PlacementFlagsOptions : Int32
                {
                    NotAutomatically = 1,
                    Unused = 2,
                    Unused1 = 4,
                    Unused2 = 8,
                    LockTypeToEnvObject = 16,
                    LockTransformToEnvObject = 32,
                    NeverPlaced = 64,
                    LockNameToEnvObject = 128,
                    CreateAtRest = 256,
                }
                public enum TransformFlagsOptions : Int16
                {
                    Mirrored = 1,
                }
                public enum BspPolicyOptions : Byte
                {
                    Default = 0,
                    AlwaysPlaced = 1,
                    ManualBspPlacement = 2,
                }
            }
            [FieldSetAttribute(28, 4)]
            public sealed class SoundSceneryDatumStructBlock : AbideTagBlock
            {
                [FieldAttribute("Volume Type", typeof(VolumeTypeOptions))]
                [OptionsAttribute(typeof(VolumeTypeOptions), false)]
                public VolumeTypeOptions VolumeType;
                [FieldAttribute("Height", typeof(Single))]
                public Single Height;
                [FieldAttribute("Override Outer Cone Gain:dB", typeof(Single))]
                public Single OverrideOuterConeGain;
                public override int Size
                {
                    get
                    {
                        return 28;
                    }
                }
                public override void Initialize()
                {
                    this.VolumeType = ((VolumeTypeOptions)(0));
                    this.Height = 0;
                    this.OverrideOuterConeGain = 0;
                }
                public override void Read(BinaryReader reader)
                {
                    this.VolumeType = ((VolumeTypeOptions)(reader.ReadInt32()));
                    this.Height = reader.ReadSingle();
                    this.OverrideOuterConeGain = reader.ReadSingle();
                }
                public override void Write(BinaryWriter writer)
                {
                }
                public enum VolumeTypeOptions : Int32
                {
                    Sphere = 0,
                    VerticalCylinder = 1,
                }
            }
        }
        [FieldSetAttribute(260, 4)]
        public sealed class GScenarioEditorFolderBlock : AbideTagBlock
        {
            [FieldAttribute("parent folder", typeof(Int32))]
            public Int32 ParentFolder;
            [FieldAttribute("name^", typeof(String256))]
            public String256 Name;
            public override int Size
            {
                get
                {
                    return 260;
                }
            }
            public override void Initialize()
            {
                this.ParentFolder = 0;
                this.Name = String256.Empty;
            }
            public override void Read(BinaryReader reader)
            {
                this.ParentFolder = reader.ReadInt32();
                this.Name = reader.Read<String256>();
            }
            public override void Write(BinaryWriter writer)
            {
            }
        }
    }
}
#pragma warning restore CS1591
