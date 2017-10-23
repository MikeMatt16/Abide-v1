using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(72, 4)]
	public unsafe struct StructureBspAudibilityBlock
	{
		[Field("door portal count", null)]
		public int DoorPortalCount0;
		[Field("cluster distance bounds", null)]
		public FloatBounds ClusterDistanceBounds1;
		[Field("encoded door pas", null)]
		[Block("Door Encoded Pas Block", 4096, typeof(DoorEncodedPasBlock))]
		public TagBlock EncodedDoorPas2;
		[Field("cluster door portal encoded pas", null)]
		[Block("Cluster Door Portal Encoded Pas Block", 2048, typeof(ClusterDoorPortalEncodedPasBlock))]
		public TagBlock ClusterDoorPortalEncodedPas3;
		[Field("ai deafening pas", null)]
		[Block("Ai Deafening Encoded Pas Block", 4088, typeof(AiDeafeningEncodedPasBlock))]
		public TagBlock AiDeafeningPas4;
		[Field("cluster distances", null)]
		[Block("Encoded Cluster Distances Block", 130816, typeof(EncodedClusterDistancesBlock))]
		public TagBlock ClusterDistances5;
		[Field("machine door mapping", null)]
		[Block("Occluder To Machine Door Mapping", 128, typeof(OccluderToMachineDoorMapping))]
		public TagBlock MachineDoorMapping6;
	}
}
#pragma warning restore CS1591
