using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(100, 4)]
	public unsafe struct StructureBspDebugInfoBlock
	{
		[Field("", null)]
		public fixed byte _0[64];
		[Field("Clusters*", null)]
		[Block("Structure Bsp Cluster Debug Info Block", 512, typeof(StructureBspClusterDebugInfoBlock))]
		public TagBlock Clusters1;
		[Field("Fog Planes*", null)]
		[Block("Structure Bsp Fog Plane Debug Info Block", 127, typeof(StructureBspFogPlaneDebugInfoBlock))]
		public TagBlock FogPlanes2;
		[Field("Fog Zones*", null)]
		[Block("Structure Bsp Fog Zone Debug Info Block", 127, typeof(StructureBspFogZoneDebugInfoBlock))]
		public TagBlock FogZones3;
	}
}
#pragma warning restore CS1591
