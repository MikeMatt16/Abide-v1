using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(3, 4)]
	public unsafe struct PixelShaderFragmentBlock
	{
		[Field("switch parameter index", null)]
		public int SwitchParameterIndex0;
		[Field("permutations index", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock PermutationsIndex1;
	}
}
#pragma warning restore CS1591
