using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(44, 4)]
	public unsafe struct STextValuePairBlocksBlockUNUSED
	{
		[Field("name", null)]
		public String Name1;
		[Field("text value pairs", null)]
		[Block("S Text Value Pair Reference Block UNUSED", 100, typeof(STextValuePairReferenceBlockUNUSED))]
		public TagBlock TextValuePairs2;
	}
}
#pragma warning restore CS1591
