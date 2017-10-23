using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct CollisionModelPathfindingSphereBlock
	{
		public enum Flags1Options
		{
			RemainsWhenOpen_0 = 1,
			VehicleOnly_1 = 2,
			WithSectors_2 = 4,
		}
		[Field("node*", null)]
		public short Node0;
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("", null)]
		public fixed byte _2[12];
		public Vector3 Center3;
		[Field("radius*", null)]
		public float Radius4;
	}
}
#pragma warning restore CS1591
