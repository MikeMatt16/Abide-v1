using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ModelPermutationBlock
	{
		public enum Flags1Options
		{
			CannotBeChosenRandomly_0 = 1,
		}
		[Field("name*^", null)]
		public StringId Name0;
		[Field("flags*", typeof(Flags1Options))]
		public byte Flags1;
		[Field("collision permutation index*", null)]
		public int CollisionPermutationIndex2;
		[Field("", null)]
		public fixed byte _3[2];
	}
}
#pragma warning restore CS1591
