using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(132, 4)]
	public unsafe struct PrismaticConstraintsBlock
	{
		[Field("constraint bodies*", typeof(ConstraintBodiesStructBlock))]
		[Block("Constraint Bodies Struct", 1, typeof(ConstraintBodiesStructBlock))]
		public ConstraintBodiesStructBlock ConstraintBodies0;
		[Field("", null)]
		public fixed byte _1[4];
		[Field("min_limit*", null)]
		public float MinLimit2;
		[Field("max_limit*", null)]
		public float MaxLimit3;
		[Field("max_friction_force*", null)]
		public float MaxFrictionForce4;
	}
}
#pragma warning restore CS1591
