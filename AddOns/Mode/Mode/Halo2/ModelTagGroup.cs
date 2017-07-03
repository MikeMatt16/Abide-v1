using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using System.IO;
using System.Runtime.InteropServices;

namespace Mode.Halo2
{
    public class ModelTagGroup
    {
        private ModeTagGroup mode;
        private ModeTagGroup.Section[] sections;
        private ModeTagGroup.Section.Resource[][] resources;

        public ModelTagGroup(IndexEntry selectedEntry)
        {
            //Prepare
            using (BinaryReader reader = new BinaryReader(selectedEntry.TagData))
            {
                //Goto
                selectedEntry.TagData.Seek(selectedEntry.Offset, SeekOrigin.Begin);

                //Read Tag Header
                mode = reader.ReadStructure<ModeTagGroup>();

                //Read Sections
                sections = new ModeTagGroup.Section[mode.Sections.Count];
                selectedEntry.TagData.Seek(mode.Sections.Offset, SeekOrigin.Begin);
                for (int i = 0; i < mode.Sections.Count; i++)
                    sections[i] = reader.ReadStructure<ModeTagGroup.Section>();

                //Read Resources
                resources = new ModeTagGroup.Section.Resource[mode.Sections.Count][];
                for (int i = 0; i < mode.Sections.Count; i++)
                {
                    resources[i] = new ModeTagGroup.Section.Resource[sections[i].Resources.Count];
                    selectedEntry.TagData.Seek(sections[i].Resources.Offset, SeekOrigin.Begin);
                    for (int j = 0; j < sections[i].Resources.Count; j++)
                        resources[i][j] = reader.ReadStructure<ModeTagGroup.Section.Resource>();
                }
            }
        }

        private struct ModeTagGroup
        {
            public static readonly int SerializedSize = Marshal.SizeOf(typeof(ModeTagGroup));

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
            
            public struct Section
            {
                public static readonly int SerializedSize = Marshal.SizeOf(typeof(Section));

                public TagBlock Resources
                {
                    get { return resources; }
                }
                public int ResourceOffset
                {
                    get { return (int)resourceDataOffset; }
                }
                public int ResourceDataSize
                {
                    get { return (int)resourceDataSize; }
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
                    public static readonly int SerializedSize = Marshal.SizeOf(typeof(Resource));

                    public ResourceType Type
                    {
                        get { return (ResourceType)type; }
                        set { type = (byte)value; }
                    }

                    private byte type;
                    private byte resourceType;
                    private short headerSize;
                    private short primaryLocator;
                    private short secondaryLocator;
                    private int resourceDataSize;
                    private int resourceDataOffset;
                }
            }

            public struct CompressionInfo
            {
                public static readonly int SerializedSize = Marshal.SizeOf(typeof(CompressionInfo));

                public ComponentCompression Compression
                {
                    get { return compression; }
                }

                private ComponentCompression compression;
                private float secTexCoordBoundsXFrom, secTexCoordBoundsXTo;
                private float secTexCoordBoundsYFrom, secTexCoordBoundsYTo;
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
                private Tag oldShaderTag;
                private TagId oldShaderId;
                private Tag shaderTag;
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

        public enum ResourceType : byte
        {
            TagBlock = 0,
            TagData = 1,
            VertexBuffer = 2,
        };
    }
}
