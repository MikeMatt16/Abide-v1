using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(6, 4)]
	public unsafe struct PixelShaderPermutationNewBlock
	{
		[Field("enum index", null)]
		public short EnumIndex0;
		[Field("flags", null)]
		public short Flags1;
		[Field("combiners", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock Combiners2;
	}
}
#pragma warning restore CS1591
