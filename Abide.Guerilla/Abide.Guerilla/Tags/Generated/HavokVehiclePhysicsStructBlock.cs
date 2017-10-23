using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(96, 4)]
	public unsafe struct HavokVehiclePhysicsStructBlock
	{
		public enum Flags0Options
		{
			Invalid_0 = 1,
		}
		[Field("flags*", typeof(Flags0Options))]
		public int Flags0;
		[Field("ground friction# for friction based vehicles only", null)]
		public float GroundFriction1;
		[Field("ground depth# for friction based vehicles only", null)]
		public float GroundDepth2;
		[Field("ground damp factor# for friction based vehicles only", null)]
		public float GroundDampFactor3;
		[Field("ground moving friction# for friction based vehicles only", null)]
		public float GroundMovingFriction4;
		[Field("ground maximum slope 0#degrees 0-90", null)]
		public float GroundMaximumSlope05;
		[Field("ground maximum slope 1#degrees 0-90.  and greater than slope 0", null)]
		public float GroundMaximumSlope16;
		[Field("", null)]
		public fixed byte _7[16];
		[Field("anti_gravity_bank_lift#lift per WU.", null)]
		public float AntiGravityBankLift8;
		[Field("steering_bank_reaction_scale#how quickly we bank when we steer ", null)]
		public float SteeringBankReactionScale9;
		[Field("gravity scale#value of 0 defaults to 1.  .5 is half gravity", null)]
		public float GravityScale10;
		[Field("radius*#generated from the radius of the hkConvexShape for this vehicle", null)]
		public float Radius11;
		[Field("anti gravity points", null)]
		[Block("Anti Gravity Point Definition Block", 16, typeof(AntiGravityPointDefinitionBlock))]
		public TagBlock AntiGravityPoints12;
		[Field("friction points", null)]
		[Block("Friction Point Definition Block", 16, typeof(FrictionPointDefinitionBlock))]
		public TagBlock FrictionPoints13;
		[Field("*shape phantom shape*", null)]
		[Block("Vehicle Phantom Shape Block", 1, typeof(VehiclePhantomShapeBlock))]
		public TagBlock ShapePhantomShape14;
	}
}
#pragma warning restore CS1591
