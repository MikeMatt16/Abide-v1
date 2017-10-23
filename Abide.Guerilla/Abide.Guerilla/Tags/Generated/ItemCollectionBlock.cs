using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("item_collection", "itmc", "����", typeof(ItemCollectionBlock))]
	[FieldSet(16, 4)]
	public unsafe struct ItemCollectionBlock
	{
		[Field("item permutations", null)]
		[Block("Item Permutation", 32, typeof(ItemPermutation))]
		public TagBlock ItemPermutations0;
		[Field("spawn time (in seconds, 0 = default)", null)]
		public short SpawnTimeInSeconds0Default1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("", null)]
		public fixed byte _3[76];
	}
}
#pragma warning restore CS1591
