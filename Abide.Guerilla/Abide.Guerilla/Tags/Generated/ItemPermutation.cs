using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct ItemPermutation
	{
		[Field("", null)]
		public fixed byte _0[32];
		[Field("weight#relatively how likely this item will be chosen", null)]
		public float Weight1;
		[Field("item^#which item to ", null)]
		public TagReference Item2;
		[Field("variant name", null)]
		public StringId VariantName3;
		[Field("", null)]
		public fixed byte _4[28];
	}
}
#pragma warning restore CS1591
