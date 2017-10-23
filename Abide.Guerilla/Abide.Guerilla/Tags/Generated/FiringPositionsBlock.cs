using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct FiringPositionsBlock
	{
		public enum Flags3Options
		{
			Open_0 = 1,
			Partial_1 = 2,
			Closed_2 = 4,
			Mobile_3 = 8,
			WallLean_4 = 16,
			Perch_5 = 32,
			GroundPoint_6 = 64,
			DynamicCoverPoint_7 = 128,
		}
		public Vector3 PositionLocal1;
		[Field("reference frame", null)]
		public short ReferenceFrame2;
		[Field("flags*", typeof(Flags3Options))]
		public short Flags3;
		[Field("area^", null)]
		public short Area4;
		[Field("cluster index*", null)]
		public short ClusterIndex5;
		[Field("", null)]
		public fixed byte _6[4];
		[Field("normal", null)]
		public Vector2 Normal7;
	}
}
#pragma warning restore CS1591
