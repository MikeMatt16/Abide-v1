using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct RenderModelSectionGroupBlock
	{
		public enum DetailLevels0Options
		{
			L1SuperLow_0 = 1,
			L2Low_1 = 2,
			L3Medium_2 = 4,
			L4High_3 = 8,
			L5SuperHigh_4 = 16,
			L6Hollywood_5 = 32,
		}
		[Field("detail levels*", typeof(DetailLevels0Options))]
		public short DetailLevels0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("compound nodes*", null)]
		[Block("Compound Node", 255, typeof(RenderModelCompoundNodeBlock))]
		public TagBlock CompoundNodes2;
	}
}
#pragma warning restore CS1591
