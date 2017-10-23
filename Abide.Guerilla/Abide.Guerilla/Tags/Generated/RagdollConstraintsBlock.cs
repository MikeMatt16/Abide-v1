using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(148, 4)]
	public unsafe struct RagdollConstraintsBlock
	{
		[Field("constraint bodies*", typeof(ConstraintBodiesStructBlock))]
		[Block("Constraint Bodies Struct", 1, typeof(ConstraintBodiesStructBlock))]
		public ConstraintBodiesStructBlock ConstraintBodies0;
		[Field("", null)]
		public fixed byte _1[4];
		[Field("min twist*", null)]
		public float MinTwist2;
		[Field("max twist*", null)]
		public float MaxTwist3;
		[Field("min cone*", null)]
		public float MinCone4;
		[Field("max cone*", null)]
		public float MaxCone5;
		[Field("min plane*", null)]
		public float MinPlane6;
		[Field("max plane*", null)]
		public float MaxPlane7;
		[Field("max friciton torque*", null)]
		public float MaxFricitonTorque8;
	}
}
#pragma warning restore CS1591
