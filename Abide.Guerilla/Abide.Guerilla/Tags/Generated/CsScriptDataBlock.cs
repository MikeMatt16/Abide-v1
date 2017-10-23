using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(132, 4)]
	public unsafe struct CsScriptDataBlock
	{
		[Field("point sets", null)]
		[Block("Cs Point Set Block", 200, typeof(CsPointSetBlock))]
		public TagBlock PointSets0;
		[Field("", null)]
		public fixed byte _1[120];
	}
}
#pragma warning restore CS1591
