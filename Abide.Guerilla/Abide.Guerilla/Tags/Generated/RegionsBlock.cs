using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct RegionsBlock
	{
		[Field("name^*", null)]
		public StringId Name0;
		[Field("permutations", null)]
		[Block("Permutations Block", 32, typeof(PermutationsBlock))]
		public TagBlock Permutations1;
	}
}
#pragma warning restore CS1591
