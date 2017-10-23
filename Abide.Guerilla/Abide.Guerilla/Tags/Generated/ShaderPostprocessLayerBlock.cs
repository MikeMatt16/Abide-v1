using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(2, 4)]
	public unsafe struct ShaderPostprocessLayerBlock
	{
		[Field("passes", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock Passes0;
	}
}
#pragma warning restore CS1591
