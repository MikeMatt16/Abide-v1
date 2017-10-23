using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(124, 4)]
	public unsafe struct StiffSpringConstraintsBlock
	{
		[Field("constraint bodies*", typeof(ConstraintBodiesStructBlock))]
		[Block("Constraint Bodies Struct", 1, typeof(ConstraintBodiesStructBlock))]
		public ConstraintBodiesStructBlock ConstraintBodies0;
		[Field("", null)]
		public fixed byte _1[4];
		[Field("spring_length*", null)]
		public float SpringLength2;
	}
}
#pragma warning restore CS1591
