using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct ObjectChangeColors
	{
		[Field("", null)]
		public fixed byte _0[32];
		[Field("initial permutations", null)]
		[Block("Object Change Color Initial Permutation", 32, typeof(ObjectChangeColorInitialPermutation))]
		public TagBlock InitialPermutations1;
		[Field("functions", null)]
		[Block("Object Change Color Function", 4, typeof(ObjectChangeColorFunction))]
		public TagBlock Functions2;
	}
}
#pragma warning restore CS1591
