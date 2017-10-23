using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(56, 4)]
	public unsafe struct ListsBlock
	{
		public enum ShapeType8Options
		{
			Sphere_0 = 0,
			Pill_1 = 1,
			Box_2 = 2,
			Triangle_3 = 3,
			Polyhedron_4 = 4,
			MultiSphere_5 = 5,
			Unused0_6 = 6,
			Unused1_7 = 7,
			Unused2_8 = 8,
			Unused3_9 = 9,
			Unused4_10 = 10,
			Unused5_11 = 11,
			Unused6_12 = 12,
			Unused7_13 = 13,
			List_14 = 14,
			Mopp_15 = 15,
		}
		[Field("", null)]
		public fixed byte _0[4];
		[Field("size*", null)]
		public short Size1;
		[Field("count*", null)]
		public short Count2;
		[Field("", null)]
		public fixed byte _3[4];
		[Field("", null)]
		public fixed byte _4[4];
		[Field("child shapes size*", null)]
		public int ChildShapesSize5;
		[Field("child shapes capacity*", null)]
		public int ChildShapesCapacity6;
		[Field("shape type*", typeof(ShapeType8Options))]
		public short ShapeType8;
		[Field("shape*", null)]
		public short Shape9;
		[Field("collision filter*", null)]
		public int CollisionFilter10;
	}
}
#pragma warning restore CS1591
