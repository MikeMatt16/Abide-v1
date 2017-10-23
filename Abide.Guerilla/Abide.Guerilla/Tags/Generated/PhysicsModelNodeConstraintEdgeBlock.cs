using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct PhysicsModelNodeConstraintEdgeBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("node a*", null)]
		public short NodeA1;
		[Field("node b*", null)]
		public short NodeB2;
		[Field("constraints*", null)]
		[Block("Physics Model Constraint Edge Constraint Block", 64, typeof(PhysicsModelConstraintEdgeConstraintBlock))]
		public TagBlock Constraints3;
		[Field("node a material#if you don't fill this out we will pluck the material from the first primitive, of the first rigid body attached to node a", null)]
		public StringId NodeAMaterial4;
		[Field("node b material#if you don't fill this out we will pluck the material from the first primitive, of the first rigid body attached to node b, if node b is none we use whatever material a has", null)]
		public StringId NodeBMaterial5;
	}
}
#pragma warning restore CS1591
