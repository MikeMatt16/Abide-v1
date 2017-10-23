using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ListShapesBlock
	{
		public enum ShapeType0Options
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
		[Field("shape type*", typeof(ShapeType0Options))]
		public short ShapeType0;
		[Field("shape*", null)]
		public short Shape1;
		[Field("collision filter*", null)]
		public int CollisionFilter2;
	}
}
#pragma warning restore CS1591
