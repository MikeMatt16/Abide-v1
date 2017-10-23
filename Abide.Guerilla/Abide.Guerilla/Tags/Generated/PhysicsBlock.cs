using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("physics", "phys", "����", typeof(PhysicsBlock))]
	[FieldSet(128, 4)]
	public unsafe struct PhysicsBlock
	{
		[Field("radius#positive uses old inferior physics, negative uses new improved physics", null)]
		public float Radius0;
		[Field("moment scale", null)]
		public float MomentScale1;
		[Field("mass", null)]
		public float Mass2;
		public Vector3 CenterOfMass3;
		[Field("density", null)]
		public float Density4;
		[Field("gravity scale", null)]
		public float GravityScale5;
		[Field("ground friction", null)]
		public float GroundFriction6;
		[Field("ground depth", null)]
		public float GroundDepth7;
		[Field("ground damp fraction", null)]
		public float GroundDampFraction8;
		[Field("ground normal k1", null)]
		public float GroundNormalK19;
		[Field("ground normal k0", null)]
		public float GroundNormalK010;
		[Field("", null)]
		public fixed byte _11[4];
		[Field("water friction", null)]
		public float WaterFriction12;
		[Field("water depth", null)]
		public float WaterDepth13;
		[Field("water density", null)]
		public float WaterDensity14;
		[Field("", null)]
		public fixed byte _15[4];
		[Field("air friction", null)]
		public float AirFriction16;
		[Field("", null)]
		public fixed byte _17[4];
		[Field("xx moment", null)]
		public float XxMoment18;
		[Field("yy moment", null)]
		public float YyMoment19;
		[Field("zz moment", null)]
		public float ZzMoment20;
		[Field("inertial matrix and inverse*", null)]
		[Block("Inertial Matrix Block", 2, typeof(InertialMatrixBlock))]
		public TagBlock InertialMatrixAndInverse21;
		[Field("powered mass points", null)]
		[Block("Powered Mass Point Block", 32, typeof(PoweredMassPointBlock))]
		public TagBlock PoweredMassPoints22;
		[Field("mass points", null)]
		[Block("Mass Point Block", 32, typeof(MassPointBlock))]
		public TagBlock MassPoints23;
	}
}
#pragma warning restore CS1591
