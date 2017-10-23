using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(132, 4)]
	public unsafe struct LimitedHingeConstraintsBlock
	{
		[Field("constraint bodies*", typeof(ConstraintBodiesStructBlock))]
		[Block("Constraint Bodies Struct", 1, typeof(ConstraintBodiesStructBlock))]
		public ConstraintBodiesStructBlock ConstraintBodies0;
		[Field("", null)]
		public fixed byte _1[4];
		[Field("limit friction*", null)]
		public float LimitFriction2;
		[Field("limit min angle*", null)]
		public float LimitMinAngle3;
		[Field("limit max angle*", null)]
		public float LimitMaxAngle4;
	}
}
#pragma warning restore CS1591
