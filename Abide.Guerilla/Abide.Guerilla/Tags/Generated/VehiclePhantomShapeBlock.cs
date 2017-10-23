using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(672, 16)]
	public unsafe struct VehiclePhantomShapeBlock
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
		public enum Flags13Options
		{
			HasAabbPhantom_0 = 1,
			OO1_1 = 2,
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
		[Field("multisphere count*", null)]
		public int MultisphereCount12;
		[Field("flags", typeof(Flags13Options))]
		public int Flags13;
		[Field("", null)]
		public fixed byte _14[8];
		[Field("x0", null)]
		public float X015;
		[Field("x1", null)]
		public float X116;
		[Field("y0", null)]
		public float Y017;
		[Field("y1", null)]
		public float Y118;
		[Field("z0", null)]
		public float Z019;
		[Field("z1", null)]
		public float Z120;
		[Field("", null)]
		public fixed byte _22[4];
		[Field("size*", null)]
		public short Size23;
		[Field("count*", null)]
		public short Count24;
		[Field("", null)]
		public fixed byte _25[4];
		[Field("num spheres*", null)]
		public int NumSpheres26;
		[Field("sphere*", null)]
		public Vector3 Sphere28;
		[Field("", null)]
		public fixed byte _29[4];
	}
}
#pragma warning restore CS1591
