using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(92, 4)]
	public unsafe struct StructureBspClusterDebugInfoBlock
	{
		public enum Errors0Options
		{
			MultipleFogPlanes_0 = 1,
			FogZoneCollision_1 = 2,
			FogZoneImmersion_2 = 4,
		}
		public enum Warnings1Options
		{
			MultipleVisibleFogPlanes_0 = 1,
			VisibleFogClusterOmission_1 = 2,
			FogPlaneMissedRenderBSP_2 = 4,
		}
		[Field("Errors*", typeof(Errors0Options))]
		public short Errors0;
		[Field("Warnings*", typeof(Warnings1Options))]
		public short Warnings1;
		[Field("", null)]
		public fixed byte _2[28];
		[Field("Lines*", null)]
		[Block("Structure Bsp Debug Info Render Line Block", 32767, typeof(StructureBspDebugInfoRenderLineBlock))]
		public TagBlock Lines3;
		[Field("Fog Plane Indices*", null)]
		[Block("Structure Bsp Debug Info Indices Block", 32767, typeof(StructureBspDebugInfoIndicesBlock))]
		public TagBlock FogPlaneIndices4;
		[Field("Visible Fog Plane Indices*", null)]
		[Block("Structure Bsp Debug Info Indices Block", 32767, typeof(StructureBspDebugInfoIndicesBlock))]
		public TagBlock VisibleFogPlaneIndices5;
		[Field("Vis. Fog Omission Cluster Indices*", null)]
		[Block("Structure Bsp Debug Info Indices Block", 32767, typeof(StructureBspDebugInfoIndicesBlock))]
		public TagBlock VisFogOmissionClusterIndices6;
		[Field("Containing Fog Zone Indices*", null)]
		[Block("Structure Bsp Debug Info Indices Block", 32767, typeof(StructureBspDebugInfoIndicesBlock))]
		public TagBlock ContainingFogZoneIndices7;
	}
}
#pragma warning restore CS1591
