using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(88, 4)]
	public unsafe struct VisibilityStructBlock
	{
		[Field("Projection Count*", null)]
		public short ProjectionCount0;
		[Field("Cluster Count*", null)]
		public short ClusterCount1;
		[Field("Volume Count*", null)]
		public short VolumeCount2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("Projections*", null)]
		[Data(2664)]
		public TagBlock Projections4;
		[Field("Visibility Clusters*", null)]
		[Data(3328)]
		public TagBlock VisibilityClusters5;
		[Field("Cluster Remap Table*", null)]
		[Data(512)]
		public TagBlock ClusterRemapTable6;
		[Field("Visibility Volumes*", null)]
		[Data(135168)]
		public TagBlock VisibilityVolumes7;
	}
}
#pragma warning restore CS1591
