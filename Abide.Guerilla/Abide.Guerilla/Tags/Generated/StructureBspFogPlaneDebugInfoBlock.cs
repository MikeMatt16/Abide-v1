using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(68, 4)]
	public unsafe struct StructureBspFogPlaneDebugInfoBlock
	{
		[Field("Fog Zone Index*", null)]
		public int FogZoneIndex0;
		[Field("", null)]
		public fixed byte _1[24];
		[Field("Connected Plane Designator*", null)]
		public int ConnectedPlaneDesignator2;
		[Field("Lines*", null)]
		[Block("Structure Bsp Debug Info Render Line Block", 32767, typeof(StructureBspDebugInfoRenderLineBlock))]
		public TagBlock Lines3;
		[Field("Intersected Cluster Indices*", null)]
		[Block("Structure Bsp Debug Info Indices Block", 32767, typeof(StructureBspDebugInfoIndicesBlock))]
		public TagBlock IntersectedClusterIndices4;
		[Field("Inf. Extent Cluster Indices*", null)]
		[Block("Structure Bsp Debug Info Indices Block", 32767, typeof(StructureBspDebugInfoIndicesBlock))]
		public TagBlock InfExtentClusterIndices5;
	}
}
#pragma warning restore CS1591
