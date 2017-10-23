using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct CharacterSearchBlock
	{
		public enum SearchFlags0Options
		{
			CrouchOnInvestigate_0 = 1,
			WalkOnPursuit_1 = 2,
		}
		[Field("Search flags", typeof(SearchFlags0Options))]
		public int SearchFlags0;
		[Field("", null)]
		public fixed byte _1[24];
		[Field("search time", null)]
		public FloatBounds SearchTime2;
		[Field("", null)]
		public fixed byte _3[24];
		[Field("Uncover distance bounds#Distance of uncover point from target. Hard lower limit, soft upper limit.", null)]
		public FloatBounds UncoverDistanceBounds5;
	}
}
#pragma warning restore CS1591
