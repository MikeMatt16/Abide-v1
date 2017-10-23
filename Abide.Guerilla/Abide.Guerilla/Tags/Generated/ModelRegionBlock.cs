using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct ModelRegionBlock
	{
		[Field("name*^", null)]
		public StringId Name0;
		[Field("collision region index*", null)]
		public int CollisionRegionIndex1;
		[Field("physics region index*", null)]
		public int PhysicsRegionIndex2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("permutations*", null)]
		[Block("Model Permutation Block", 32, typeof(ModelPermutationBlock))]
		public TagBlock Permutations4;
	}
}
#pragma warning restore CS1591
