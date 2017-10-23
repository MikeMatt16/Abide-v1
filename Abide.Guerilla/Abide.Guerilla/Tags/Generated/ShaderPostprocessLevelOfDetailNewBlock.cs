using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(6, 4)]
	public unsafe struct ShaderPostprocessLevelOfDetailNewBlock
	{
		[Field("available layer flags", null)]
		public int AvailableLayerFlags0;
		[Field("layers", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock Layers1;
	}
}
#pragma warning restore CS1591
