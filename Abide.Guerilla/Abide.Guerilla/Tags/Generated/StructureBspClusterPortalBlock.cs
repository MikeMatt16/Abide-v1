using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct StructureBspClusterPortalBlock
	{
		public enum Flags5Options
		{
			AICannotHearThroughThis_0 = 1,
			OneWay_1 = 2,
			Door_2 = 4,
			NoWay_3 = 8,
			OneWayReversed_4 = 16,
			NoOneCanHearThroughThis_5 = 32,
		}
		[Field("Back Cluster*", null)]
		public short BackCluster0;
		[Field("Front Cluster*", null)]
		public short FrontCluster1;
		[Field("Plane Index*", null)]
		public int PlaneIndex2;
		public Vector3 Centroid3;
		[Field("Bounding Radius*", null)]
		public float BoundingRadius4;
		[Field("Flags*", typeof(Flags5Options))]
		public int Flags5;
		[Field("Vertices*", null)]
		[Block("Structure Bsp Cluster Portal Vertex Block", 128, typeof(StructureBspClusterPortalVertexBlock))]
		public TagBlock Vertices6;
	}
}
#pragma warning restore CS1591
