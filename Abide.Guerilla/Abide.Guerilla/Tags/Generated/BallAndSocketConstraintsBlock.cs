using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(120, 4)]
	public unsafe struct BallAndSocketConstraintsBlock
	{
		[Field("constraint bodies*", typeof(ConstraintBodiesStructBlock))]
		[Block("Constraint Bodies Struct", 1, typeof(ConstraintBodiesStructBlock))]
		public ConstraintBodiesStructBlock ConstraintBodies0;
		[Field("", null)]
		public fixed byte _1[4];
	}
}
#pragma warning restore CS1591
