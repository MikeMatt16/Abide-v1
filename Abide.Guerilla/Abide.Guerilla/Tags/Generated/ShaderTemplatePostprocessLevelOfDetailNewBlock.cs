using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(10, 4)]
	public unsafe struct ShaderTemplatePostprocessLevelOfDetailNewBlock
	{
		[Field("layers", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock Layers0;
		[Field("available layers", null)]
		public int AvailableLayers1;
		[Field("projected height percentage", null)]
		public float ProjectedHeightPercentage2;
	}
}
#pragma warning restore CS1591
