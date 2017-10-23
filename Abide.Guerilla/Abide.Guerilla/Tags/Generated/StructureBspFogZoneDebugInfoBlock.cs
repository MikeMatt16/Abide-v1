using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(80, 4)]
	public unsafe struct StructureBspFogZoneDebugInfoBlock
	{
		[Field("Media Index:Scenario Fog Plane*", null)]
		public int MediaIndex0;
		[Field("Base Fog Plane Index*", null)]
		public int BaseFogPlaneIndex1;
		[Field("", null)]
		public fixed byte _2[24];
		[Field("Lines*", null)]
		[Block("Structure Bsp Debug Info Render Line Block", 32767, typeof(StructureBspDebugInfoRenderLineBlock))]
		public TagBlock Lines3;
		[Field("Immersed Cluster Indices*", null)]
		[Block("Structure Bsp Debug Info Indices Block", 32767, typeof(StructureBspDebugInfoIndicesBlock))]
		public TagBlock ImmersedClusterIndices4;
		[Field("Bounding Fog Plane Indices*", null)]
		[Block("Structure Bsp Debug Info Indices Block", 32767, typeof(StructureBspDebugInfoIndicesBlock))]
		public TagBlock BoundingFogPlaneIndices5;
		[Field("Collision Fog Plane Indices*", null)]
		[Block("Structure Bsp Debug Info Indices Block", 32767, typeof(StructureBspDebugInfoIndicesBlock))]
		public TagBlock CollisionFogPlaneIndices6;
	}
}
#pragma warning restore CS1591
