using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct ModelVariantRegionBlock
	{
		public enum SortOrderNegativeValuesMeanCloserToTheCamera5Options
		{
			NoSorting_0 = 0,
			_5Closest_1 = 1,
			_4_2 = 2,
			_3_3 = 3,
			_2_4 = 4,
			_1_5 = 5,
			_0SameAsModel_6 = 6,
			_1_7 = 7,
			_2_8 = 8,
			_3_9 = 9,
			_4_10 = 10,
			_5Farthest_11 = 11,
		}
		[Field("region name^:must match region name in render_model", null)]
		public StringId RegionName0;
		[Field("", null)]
		public fixed byte _1[1];
		[Field("", null)]
		public fixed byte _2[1];
		[Field("parent variant", null)]
		public short ParentVariant3;
		[Field("permutations", null)]
		[Block("Permutation", 32, typeof(ModelVariantPermutationBlock))]
		public TagBlock Permutations4;
		[Field("sort order#negative values mean closer to the camera", typeof(SortOrderNegativeValuesMeanCloserToTheCamera5Options))]
		public short SortOrder5;
		[Field("", null)]
		public fixed byte _6[2];
	}
}
#pragma warning restore CS1591
