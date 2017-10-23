using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(44, 4)]
	public unsafe struct GlobalGeometrySectionInfoStructBlock
	{
		public enum GeometryClassification12Options
		{
			Worldspace_0 = 0,
			Rigid_1 = 1,
			RigidBoned_2 = 2,
			Skinned_3 = 3,
			UnsupportedReimport_4 = 4,
		}
		public enum GeometryCompressionFlags13Options
		{
			CompressedPosition_0 = 1,
			CompressedTexcoord_1 = 2,
			CompressedSecondaryTexcoord_2 = 4,
		}
		public enum SectionLightingFlags19Options
		{
			HasLmTexcoords_0 = 1,
			HasLmIncRad_1 = 2,
			HasLmColors_2 = 4,
			HasLmPrt_3 = 8,
		}
		[Field("Total Vertex Count*", null)]
		public short TotalVertexCount1;
		[Field("Total Triangle Count*", null)]
		public short TotalTriangleCount2;
		[Field("Total Part Count*", null)]
		public short TotalPartCount3;
		[Field("Shadow-Casting Triangle Count*", null)]
		public short ShadowCastingTriangleCount4;
		[Field("Shadow-Casting Part Count*", null)]
		public short ShadowCastingPartCount5;
		[Field("Opaque Point Count*", null)]
		public short OpaquePointCount6;
		[Field("Opaque Vertex Count*", null)]
		public short OpaqueVertexCount7;
		[Field("Opaque Part Count*", null)]
		public short OpaquePartCount8;
		[Field("Opaque Max Nodes/Vertex*", null)]
		public int OpaqueMaxNodesVertex9;
		[Field("Transparent Max Nodes/Vertex*", null)]
		public int TransparentMaxNodesVertex10;
		[Field("Shadow-Casting Rigid Triangle Count*", null)]
		public short ShadowCastingRigidTriangleCount11;
		[Field("Geometry Classification*", typeof(GeometryClassification12Options))]
		public short GeometryClassification12;
		[Field("Geometry Compression Flags*", typeof(GeometryCompressionFlags13Options))]
		public short GeometryCompressionFlags13;
		[Field("EMPTY STRING", null)]
		[Block("Compression Info", 1, typeof(GlobalGeometryCompressionInfoBlock))]
		public TagBlock EMPTYSTRING14;
		[Field("Hardware Node Count*", null)]
		public int HardwareNodeCount15;
		[Field("Node Map Size*", null)]
		public int NodeMapSize16;
		[Field("Software Plane Count*", null)]
		public short SoftwarePlaneCount17;
		[Field("total subpart_cont*", null)]
		public short TotalSubpartCont18;
		[Field("Section Lighting Flags*", typeof(SectionLightingFlags19Options))]
		public short SectionLightingFlags19;
	}
}
#pragma warning restore CS1591
