using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct SecondaryLightStructBlock
	{
		[Field("Min lightmap color", null)]
		public ColorRgbF MinLightmapColor1;
		[Field("Max lightmap color", null)]
		public ColorRgbF MaxLightmapColor2;
		[Field("Min diffuse sample", null)]
		public ColorRgbF MinDiffuseSample3;
		[Field("Max diffuse sample", null)]
		public ColorRgbF MaxDiffuseSample4;
		[Field("z axis rotation#degrees", null)]
		public float ZAxisRotation5;
		[Field("function", typeof(MappingFunctionBlock))]
		[Block("Mapping Function", 1, typeof(MappingFunctionBlock))]
		public MappingFunctionBlock Function8;
	}
}
#pragma warning restore CS1591
