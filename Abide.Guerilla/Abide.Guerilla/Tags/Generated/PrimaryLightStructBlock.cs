using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct PrimaryLightStructBlock
	{
		[Field("Min lightmap color", null)]
		public ColorRgbF MinLightmapColor1;
		[Field("Max lightmap color", null)]
		public ColorRgbF MaxLightmapColor2;
		[Field("exclusion angle from up#degrees from up the direct light cannot be", null)]
		public float ExclusionAngleFromUp3;
		[Field("function", typeof(MappingFunctionBlock))]
		[Block("Mapping Function", 1, typeof(MappingFunctionBlock))]
		public MappingFunctionBlock Function6;
	}
}
#pragma warning restore CS1591
