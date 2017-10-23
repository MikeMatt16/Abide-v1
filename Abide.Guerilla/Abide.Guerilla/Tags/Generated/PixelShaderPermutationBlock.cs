using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct PixelShaderPermutationBlock
	{
		public enum Flags1Options
		{
			HasFinalCombiner_0 = 1,
		}
		[Field("enum index", null)]
		public short EnumIndex0;
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("constants", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock Constants2;
		[Field("combiners", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock Combiners3;
		[Field("", null)]
		public fixed byte _4[4];
		[Field("", null)]
		public fixed byte _5[4];
	}
}
#pragma warning restore CS1591
