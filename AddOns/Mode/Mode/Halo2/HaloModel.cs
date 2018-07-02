using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Mode.Halo2
{
    /// <summary>
    /// Represents a Halo 2 model.
    /// </summary>
    public sealed class HaloModel
    {
        public Mesh this[int region, int permutation, int lod]
        {
            get { return meshes[region][permutation][lod]; }
        }
        public int SectionCount
        {
            get { return tag.Sections.Length; }
        }
        public int RegionCount
        {
            get { return tag.Regions.Length; }
        }
        public int[] PermutationCount
        {
            get
            {
                int[] permutationCount = new int[tag.Regions.Length];
                for (int i = 0; i < tag.Regions.Length; i++)
                    permutationCount[i] = tag.Permutations[i].Length;
                return permutationCount;
            }
        }
        public int MaterialCount
        {
            get { return tag.Materials.Length; }
        }

        private readonly Mesh[][][] meshes;
        private readonly MapFile map;
        private readonly IndexEntry entry;
        private readonly ModelTag tag;
        
        private readonly CompressionInfoProperties compressionInfo;
        private readonly RegionProperties[] regions;
        private readonly PermutationProperties[][] permutations;
        private readonly SectionProperties[] sections;
        private readonly ResourceProperties[][] resources;

        /// <summary>
        /// Initializes a new instance of the <see cref="HaloModel"/> class.
        /// </summary>
        /// <param name="entry">The model Halo 2 index entry.</param>
        public HaloModel(MapFile map, IndexEntry entry)
        {
            //Check
            if (map == null) throw new ArgumentNullException(nameof(map));
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            else if (entry.Root != HaloTags.mode) throw new ArgumentException("Index entry is not render model.", nameof(entry));

            //Setup
            this.map = map;
            this.entry = entry;
            tag = new ModelTag(entry);
            meshes = new Mesh[tag.Regions.Length][][];

            //Setup property accessors
            compressionInfo = new CompressionInfoProperties(this);
            regions = new RegionProperties[tag.Regions.Length];
            for (int i = 0; i < tag.Regions.Length; i++)
                regions[i] = new RegionProperties(this, i);
            permutations = new PermutationProperties[tag.Regions.Length][];
            for (int i = 0; i < tag.Regions.Length; i++)
            {
                permutations[i] = new PermutationProperties[tag.Permutations[i].Length];
                for (int j = 0; j < tag.Permutations[i].Length; j++)
                    permutations[i][j] = new PermutationProperties(this, i, j);
            }
            sections = new SectionProperties[tag.Sections.Length];
            for (int i = 0; i < tag.Sections.Length; i++)
                sections[i] = new SectionProperties(this, i);
            resources = new ResourceProperties[tag.Sections.Length][];
            for (int i = 0; i < tag.Sections.Length; i++)
            {
                resources[i] = new ResourceProperties[tag.Resources[i].Length];
                for (int j = 0; j < tag.Resources[i].Length; j++)
                    resources[i][j] = new ResourceProperties(this, i, j);
            }            

            //Loop through regions
            for (int r = 0; r < tag.Regions.Length; r++)
            {
                //Setup
                ModelTagGroup.Region region = tag.Regions[r];
                meshes[r] = new Mesh[region.Permutations.Count][];

                //Loop through permutations
                for (int p = 0; p < region.Permutations.Count; p++)
                {
                    //Setup
                    ModelTagGroup.Region.Permutation permutation = tag.Permutations[r][p];
                    meshes[r][p] = new Mesh[6];

                    //Loop through LODs
                    byte[] sourceData = null;
                    for (int l = 0; l < 6; l++)
                    {
                        //Get source data
                        int sectionIndex = -1;
                        switch (l)
                        {
                            case 0: sectionIndex = permutation.L1SectionIndex; break;
                            case 1: sectionIndex = permutation.L2SectionIndex; break;
                            case 2: sectionIndex = permutation.L3SectionIndex; break;
                            case 3: sectionIndex = permutation.L4SectionIndex; break;
                            case 4: sectionIndex = permutation.L5SectionIndex; break;
                            case 5: sectionIndex = permutation.L6SectionIndex; break;
                        }

                        //Check
                        if(sectionIndex >= 0 && sectionIndex < sections.Length)
                        {
                            //Get properties
                            ModelTagGroup.Section section = tag.Sections[sectionIndex];

                            //Check
                            if (section.RawOffset != uint.MaxValue)
                            {
                                RawLocation rawLocation = (RawLocation)(section.RawOffset & 0xC0000000);
                                if (rawLocation == RawLocation.Local) sourceData = entry.Raws[RawSection.Model][(int)section.RawOffset].GetBuffer();
                                else
                                {
                                    string fileLocation = string.Empty;
                                    int rawOffset = (int)(section.RawOffset & (uint)RawLocation.LocalMask);
                                    switch (rawLocation)
                                    {
                                        case RawLocation.Shared:
                                            fileLocation = HaloSettings.SharedPath;
                                            break;
                                        case RawLocation.Mainmenu:
                                            fileLocation = HaloSettings.MainmenuPath;
                                            break;
                                        case RawLocation.SinglePlayerShared:
                                            fileLocation = HaloSettings.SingleplayerSharedPath;
                                            break;
                                    }

                                    //Check
                                    if (File.Exists(fileLocation))
                                        using (FileStream fs = new FileStream(fileLocation, FileMode.Open))
                                        using (BinaryReader mapReader = new BinaryReader(fs))
                                        {
                                            fs.Seek(rawOffset, SeekOrigin.Begin);
                                            sourceData = mapReader.ReadBytes((int)section.RawSize);
                                        }
                                }
                            }

                            //Set
                            if (sourceData.Length == 0)
                                continue;

                            //Dump
                            try
                            {
                                using (FileStream fs = new FileStream(@"G:\model.bin", FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                                    fs.Write(sourceData, 0, sourceData.Length);
                            }
                            catch { }

                            //Create Mesh
                            meshes[r][p][l] = Mesh.Halo2Mesh(resources[sectionIndex], sourceData);
                        }
                    }
                }
            }
        }
        
        public sealed class CompressionInfoProperties : TagblockProperties<HaloModel>
        {
            public Range X
            {
                get { return tagBlock.tag.CompressionInfo.X; }
                set { tagBlock.tag.CompressionInfo.X = value; }
            }
            public Range Y
            {
                get { return tagBlock.tag.CompressionInfo.Y; }
                set { tagBlock.tag.CompressionInfo.Y = value; }
            }
            public Range Z
            {
                get { return tagBlock.tag.CompressionInfo.Z; }
                set { tagBlock.tag.CompressionInfo.Z = value; }
            }
            public Range U
            {
                get { return tagBlock.tag.CompressionInfo.U; }
                set { tagBlock.tag.CompressionInfo.U = value; }
            }
            public Range V
            {
                get { return tagBlock.tag.CompressionInfo.V; }
                set { tagBlock.tag.CompressionInfo.V = value; }
            }
            public Range SecondaryU
            {
                get { return tagBlock.tag.CompressionInfo.SecondaryU; }
                set { tagBlock.tag.CompressionInfo.SecondaryU = value; }
            }
            public Range SecondaryV
            {
                get { return tagBlock.tag.CompressionInfo.SecondaryV; }
                set { tagBlock.tag.CompressionInfo.SecondaryV = value; }
            }

            public override uint Address
            {
                get { return tagBlock.tag.Header.CompressionInfos.Offset; }
            }
            
            public CompressionInfoProperties(HaloModel model) : base(model)
            {
                //Check
                if (model == null) throw new ArgumentNullException(nameof(model));
                if (model.tag.Header.CompressionInfos.Count < 1) throw new InvalidOperationException("Model must have a compression information structure.");
            }
        }
        public sealed class RegionProperties : TagblockProperties<HaloModel>
        {
            public string Name
            {
                get { return tagBlock.map.Strings[tagBlock.tag.Regions[index].Name.Index]; }
                set
                {
                    if (tagBlock.map.Strings.Contains(value))
                        tagBlock.tag.Regions[index].Name = tagBlock.map.Strings[value];
                }
            }
            public override uint Address
            {
                get { return (uint)(tagBlock.tag.Header.Regions.Offset + (index * Marshal.SizeOf(tagBlock.tag.Regions[index]))); }
            }

            private readonly int index;

            public RegionProperties(HaloModel model, int index) : base(model)
            {
                //Check
                if (model == null) throw new ArgumentNullException(nameof(model));
                if (index < 0 || model.RegionCount <= index) throw new ArgumentOutOfRangeException(nameof(index));

                //Setup
                this.index = index;
            }
            public override string ToString()
            {
                return $"Region {index}: {Name}";
            }
        }
        public sealed class PermutationProperties : TagblockProperties<HaloModel>
        {
            public string Name
            {
                get { return tagBlock.map.Strings[tagBlock.tag.Permutations[regionIndex][index].Name.Index]; }
                set
                {
                    if (tagBlock.map.Strings.Contains(value))
                        tagBlock.tag.Permutations[regionIndex][index].Name = tagBlock.map.Strings[value];
                }
            }
            public short L1SectionIndex
            {
                get { return tagBlock.tag.Permutations[regionIndex][index].L1SectionIndex; }
                set { tagBlock.tag.Permutations[regionIndex][index].L1SectionIndex = value; }
            }
            public short L2SectionIndex
            {
                get { return tagBlock.tag.Permutations[regionIndex][index].L2SectionIndex; }
                set { tagBlock.tag.Permutations[regionIndex][index].L2SectionIndex = value; }
            }
            public short L3SectionIndex
            {
                get { return tagBlock.tag.Permutations[regionIndex][index].L3SectionIndex; }
                set { tagBlock.tag.Permutations[regionIndex][index].L3SectionIndex = value; }
            }
            public short L4SectionIndex
            {
                get { return tagBlock.tag.Permutations[regionIndex][index].L4SectionIndex; }
                set { tagBlock.tag.Permutations[regionIndex][index].L4SectionIndex = value; }
            }
            public short L5SectionIndex
            {
                get { return tagBlock.tag.Permutations[regionIndex][index].L5SectionIndex; }
                set { tagBlock.tag.Permutations[regionIndex][index].L5SectionIndex = value; }
            }
            public short L6SectionIndex
            {
                get { return tagBlock.tag.Permutations[regionIndex][index].L6SectionIndex; }
                set { tagBlock.tag.Permutations[regionIndex][index].L6SectionIndex = value; }
            }
            public override uint Address
            {
                get { return (uint)(tagBlock.tag.Regions[regionIndex].Permutations.Offset + (index * Marshal.SizeOf(tagBlock.tag.Permutations[regionIndex][index]))); }
            }

            private readonly int regionIndex, index;

            public PermutationProperties(HaloModel model, int regionIndex, int index) : base(model)
            {
                //Check
                if (model == null) throw new ArgumentNullException(nameof(model));
                if (regionIndex < 0 || model.RegionCount <= regionIndex) throw new ArgumentOutOfRangeException(nameof(regionIndex));
                if (index < 0 || model.PermutationCount[regionIndex] <= index) throw new ArgumentOutOfRangeException(nameof(index));

                //Setup
                this.regionIndex = regionIndex;
                this.index = index;
            }
            public override string ToString()
            {
                return $"Permutation {index}: {tagBlock.map.Strings[tagBlock.tag.Regions[regionIndex].Name.Index]}, {Name}";
            }
        }
        public sealed class MaterialProperties : TagblockProperties<HaloModel>
        {
            public override uint Address
            {
                get { return (uint)(tagBlock.tag.Header.Materials.Offset + (index * Marshal.SizeOf(tagBlock.tag.Materials[index]))); }
            }

            private readonly int index;

            public MaterialProperties(HaloModel model, int index) : base(model)
            {
                //Check
                if (model == null) throw new ArgumentNullException(nameof(model));
                if (index < 0 || model.MaterialCount <= index) throw new ArgumentOutOfRangeException(nameof(index));

                //Setup
                this.index = index;
            }
        }
        public sealed class SectionProperties : TagblockProperties<HaloModel>
        {
            public override uint Address
            {
                get { return (uint)(tagBlock.tag.Header.Sections.Offset + (index * Marshal.SizeOf(tagBlock.tag.Sections[index]))); }
            }

            private readonly int index;

            public SectionProperties(HaloModel model, int index) : base(model)
            {
                //Check
                if (model == null) throw new ArgumentNullException(nameof(model));
                if (index < 0 || model.SectionCount <= index) throw new ArgumentOutOfRangeException(nameof(index));

                //Setup
                this.index = index;
            }
        }
        public sealed class ResourceProperties : TagblockProperties<HaloModel>
        {
            public override uint Address
            {
                get { return (uint)(tagBlock.tag.Sections[sectionIndex].Resources.Offset + (index * Marshal.SizeOf(tagBlock.tag.Resources[sectionIndex][index]))); }
            }
            public ResourceType Type
            {
                get { return (ResourceType)tagBlock.tag.Resources[sectionIndex][index].Type; }
                set { tagBlock.tag.Resources[sectionIndex][index].Type = (byte)value; }
            }
            public byte ResourceType
            {
                get { return tagBlock.tag.Resources[sectionIndex][index].ResourceType; }
                set { tagBlock.tag.Resources[sectionIndex][index].ResourceType = value; }
            }
            public short PrimaryLocator
            {
                get { return tagBlock.tag.Resources[sectionIndex][index].PrimaryLocator; }
                set { tagBlock.tag.Resources[sectionIndex][index].PrimaryLocator = value; }
            }
            public short SecondaryLocator
            {
                get { return tagBlock.tag.Resources[sectionIndex][index].SecondaryLocator; }
                set { tagBlock.tag.Resources[sectionIndex][index].SecondaryLocator = value; }
            }
            public int ResourceDataOffset
            {
                get { return tagBlock.tag.Resources[sectionIndex][index].ResourceDataOffset; }
                set { tagBlock.tag.Resources[sectionIndex][index].ResourceDataOffset = value; }
            }
            public int ResourceDataSize
            {
                get { return tagBlock.tag.Resources[sectionIndex][index].ResourceDataSize; }
                set { tagBlock.tag.Resources[sectionIndex][index].ResourceDataSize = value; }
            }
            
            private readonly int sectionIndex, index;

            public ResourceProperties(HaloModel model, int sectionIndex, int index) : base(model)
            {
                //Check
                if (model == null) throw new ArgumentNullException(nameof(model));
                if (sectionIndex < 0 || model.SectionCount <= sectionIndex) throw new ArgumentOutOfRangeException(nameof(sectionIndex));
                if (index < 0 || model.resources[sectionIndex].Length <= index) throw new ArgumentOutOfRangeException(nameof(index));

                //Setup
                this.sectionIndex = sectionIndex;
                this.index = index;
            }
        }

        private sealed class ModelTag
        {
            public ModelTagGroup Header;
            public ComponentCompression CompressionInfo;
            public ModelTagGroup.Region[] Regions;
            public ModelTagGroup.Region.Permutation[][] Permutations;
            public ModelTagGroup.Section[] Sections;
            public ModelTagGroup.Section.Resource[][] Resources;
            public ModelTagGroup.Material[] Materials;

            public ModelTag(IndexEntry entry)
            {
                //Prepare
                using (BinaryReader reader = entry.TagData.CreateReader())
                {
                    //Goto
                    entry.TagData.Seek(entry.Offset, SeekOrigin.Begin);

                    //Read Tag Header
                    Header = reader.Read<ModelTagGroup>();

                    //Read Compression Information
                    if (Header.CompressionInfos.Count > 0)
                    {
                        CompressionInfo = new ComponentCompression();
                        entry.TagData.Seek(Header.CompressionInfos.Offset, SeekOrigin.Begin);
                        CompressionInfo = reader.Read<ComponentCompression>();
                    }

                    //Read Regions
                    Regions = new ModelTagGroup.Region[Header.Regions.Count];
                    entry.TagData.Seek(Header.Regions.Offset, SeekOrigin.Begin);
                    for (int i = 0; i < Header.Regions.Count; i++)
                        Regions[i] = reader.Read<ModelTagGroup.Region>();

                    //Read Perumtations
                    Permutations = new ModelTagGroup.Region.Permutation[Header.Regions.Count][];
                    for (int i = 0; i < Header.Regions.Count; i++)
                    {
                        Permutations[i] = new ModelTagGroup.Region.Permutation[Regions[i].Permutations.Count];
                        entry.TagData.Seek(Regions[i].Permutations.Offset, SeekOrigin.Begin);
                        for (int j = 0; j < Regions[i].Permutations.Count; j++)
                            Permutations[i][j] = reader.Read<ModelTagGroup.Region.Permutation>();
                    }

                    //Read Sections
                    Sections = new ModelTagGroup.Section[Header.Sections.Count];
                    entry.TagData.Seek(Header.Sections.Offset, SeekOrigin.Begin);
                    for (int i = 0; i < Header.Sections.Count; i++)
                        Sections[i] = reader.Read<ModelTagGroup.Section>();

                    //Read Resources
                    Resources = new ModelTagGroup.Section.Resource[Header.Sections.Count][];
                    for (int i = 0; i < Header.Sections.Count; i++)
                    {
                        Resources[i] = new ModelTagGroup.Section.Resource[Sections[i].Resources.Count];
                        entry.TagData.Seek(Sections[i].Resources.Offset, SeekOrigin.Begin);
                        for (int j = 0; j < Sections[i].Resources.Count; j++)
                            Resources[i][j] = reader.Read<ModelTagGroup.Section.Resource>();
                    }

                    //Read Materials
                    Materials = new ModelTagGroup.Material[Header.Materials.Count];
                    entry.TagData.Seek(Header.Materials.Offset, SeekOrigin.Begin);
                    for (int i = 0; i < Header.Materials.Count; i++)
                        Materials[i] = reader.Read<ModelTagGroup.Material>();
                }
            }
        }

        private struct ModelTagGroup
        {
            public StringId Name
            {
                get { return name; }
            }
            public TagBlock ImportInfo
            {
                get { return importInfo; }
            }
            public TagBlock CompressionInfos
            {
                get { return compressionInfo; }
            }
            public TagBlock Regions
            {
                get { return regions; }
            }
            public TagBlock Sections
            {
                get { return sections; }
            }
            public TagBlock SectionGroups
            {
                get { return sectionGroups; }
            }
            public TagBlock Nodes
            {
                get { return nodes; }
            }
            public TagBlock Materials
            {
                get { return materials; }
            }
            public TagBlock MarkerGroups
            {
                get { return markerGroups; }
            }
            public TagBlock Errors
            {
                get { return errors; }
            }

            private StringId name;
            private ushort renderModelFlags;
            private ushort unnamed0;
            private uint unnamed1;
            private TagBlock importInfo;
            private TagBlock compressionInfo;
            private TagBlock regions;
            private TagBlock sections;
            private TagBlock invalidSectionPairBits;
            private TagBlock sectionGroups;
            private byte l1SectionGroupIndex, l2SectionGroupIndex, l3SectionGroupIndex, l4SectionGroupIndex, l5SectionGroupIndex, l6SectionGroupIndex;
            private ushort unnamed2;
            private int nodeListChecksum;
            private TagBlock nodes;
            private TagBlock nodeMapOld;
            private TagBlock markerGroups;
            private TagBlock materials;
            private TagBlock errors;
            private float dontDrawOverCameraCosineAngle;
            private TagBlock prtInfo;
            private TagBlock sectionRenderLeaves;

            public struct Region
            {
                public StringId Name
                {
                    get { return name; }
                    set { name = value; }
                }
                public TagBlock Permutations
                {
                    get { return permutations; }
                    set { permutations = value; }
                }

                private StringId name;
                private short nodeMapOffsetOld, nodeMapSizeOld;
                private TagBlock permutations;

                public struct Permutation
                {
                    public StringId Name
                    {
                        get { return name; }
                        set { name = value; }
                    }
                    public short L1SectionIndex
                    {
                        get { return l1SectionIndex; }
                        set { l1SectionIndex = value; }
                    }
                    public short L2SectionIndex
                    {
                        get { return l2SectionIndex; }
                        set { l2SectionIndex = value; }
                    }
                    public short L3SectionIndex
                    {
                        get { return l3SectionIndex; }
                        set { l3SectionIndex = value; }
                    }
                    public short L4SectionIndex
                    {
                        get { return l4SectionIndex; }
                        set { l4SectionIndex = value; }
                    }
                    public short L5SectionIndex
                    {
                        get { return l5SectionIndex; }
                        set { l5SectionIndex = value; }
                    }
                    public short L6SectionIndex
                    {
                        get { return l6SectionIndex; }
                        set { l6SectionIndex = value; }
                    }

                    private StringId name;
                    private short l1SectionIndex, l2SectionIndex, l3SectionIndex, l4SectionIndex, l5SectionIndex, l6SectionIndex;
                }
            }

            public struct Section
            {
                public TagBlock Resources
                {
                    get { return resources; }
                }
                public uint RawOffset
                {
                    get { return resourceDataOffset; }
                }
                public uint RawSize
                {
                    get { return resourceDataSize; }
                }

                private ushort globalGeometryClassificationEnumDefinition;
                private ushort unnamed0;
                private short totalVertexCount;
                private short totalTriangleCount;
                private short totalPartCount;
                private short shadowCastingTriangleCount;
                private short shadowCastingPartCount;
                private short opaquePointCount;
                private short opaqueVertexCount;
                private short opaquePartCount;
                private byte opaqueMaxNodesVertex;
                private byte transparentMaxNodesVertex;
                private short shadowCastingRigidTriangleCount;
                private ushort geometryClassification;
                private ushort geometryCompressionFlags;
                private TagBlock internalCompressionInfo;
                private byte hardwareNodeCount;
                private byte nodeMapSize;
                private short softwarePlaneCount;
                private short togalSubpartCount;
                private ushort sectionLightingFlags;
                private short rigidNode;
                private ushort sectionPostprocessFlags;
                private TagBlock sectionData;
                private uint resourceDataOffset;
                private uint resourceDataSize;
                private int sectionDataSize;
                private int resourceDataSize2;
                private TagBlock resources;
                private TagId geometrySelfReference;
                private ushort unused0;
                private uint unused1;

                public struct Resource
                {
                    public byte Type
                    {
                        get { return type; }
                        set { type = value; }
                    }
                    public byte ResourceType
                    {
                        get { return resourceType; }
                        set { resourceType = value; }
                    }
                    public short PrimaryLocator
                    {
                        get { return primaryLocator; }
                        set { primaryLocator = value; }
                    }
                    public short SecondaryLocator
                    {
                        get { return secondaryLocator; }
                        set { secondaryLocator = value; }
                    }
                    public int ResourceDataSize
                    {
                        get { return resourceDataSize; }
                        set { resourceDataSize = value; }
                    }
                    public int ResourceDataOffset
                    {
                        get { return resourceDataOffset; }
                        set { resourceDataOffset = value; }
                    }

                    private byte type;
                    private byte resourceType;
                    private short unused;
                    private short primaryLocator;
                    private short secondaryLocator;
                    private int resourceDataSize;
                    private int resourceDataOffset;
                }
            }

            public struct Node
            {
                private StringId name;
                private short firstChildNode;
                private short nextSiblingNode;
                private short importNodeIndex;
            }

            public struct Material
            {
                private TagFourCc oldShaderTag;
                private TagId oldShaderId;
                private TagFourCc shaderTag;
                private TagId shaderId;
                private TagBlock properties;
                private uint unused0;
                private byte breakableSurfaceIndex, unused1, unused2, unused3;

                public struct Properties
                {
                    private short type;
                    private short intValue;
                    private float realValue;
                }
            }
        }
    }

    /// <summary>
    /// Represents a polygon mesh consiting of vertices, edges and faces that make up a polyhedral object.
    /// </summary>
    [Serializable]
    public sealed class Mesh
    {
        /// <summary>
        /// Gets and returns the array of vertices of the mesh.
        /// </summary>
        public Vector3[] Vertices
        {
            get { return vertices; }
        }
        /// <summary>
        /// Gets and returns the array of texture coordinates of the mesh.
        /// </summary>
        public Vector2[] TextureCoords
        {
            get { return texCoods; }
        }
        /// <summary>
        /// Gets and returns the array of indices of the mesh.
        /// </summary>
        public int[] Indices
        {
            get { return indices; }
        }

        private readonly Vector3[] vertices, normals, tangets, bitangents;
        private readonly Vector2[] texCoods;
        private readonly int[] indices;

        public static Mesh Halo2Mesh(HaloModel.ResourceProperties[] resources, byte[] data)
        {
            //Prepare
            Mesh mesh = null;

            //Create Stream
            using (MemoryStream ms = new MemoryStream(data))
            using (BinaryReader reader = new BinaryReader(ms))
            {
                //Loop
                foreach (var resource in resources)
                    if (resource.Type == ResourceType.TagBlock)
                    {
                        //Get count
                        ms.Seek(8 + resource.PrimaryLocator, SeekOrigin.Begin);
                        int count = reader.ReadInt32();

                        //Goto
                        ms.Seek(resource.ResourceDataOffset, SeekOrigin.Begin);
                        switch (resource.PrimaryLocator)
                        {
                            case 0:
                                break;
                            case 32:
                                break;
                            case 56:
                                break;
                            case 100:
                                break;
                            default:
                                break;
                        }
                    }
            }

            //Return
            return mesh;
        }
    }

    /// <summary>
    /// Represents a resource type enumeration containing possible resource types.
    /// </summary>
    public enum ResourceType : byte
    {
        TagBlock = 0,
        TagData = 1,
        VertexBuffer = 2,
    };

    /// <summary>
    /// Represents the location of the raw.
    /// </summary>
    public enum RawLocation : uint
    {
        Local = 0,
        LocalMask = ~SinglePlayerShared,
        Shared = 0x80000000,
        Mainmenu = 0x40000000,
        SinglePlayerShared = Shared | Mainmenu,
    };
}
