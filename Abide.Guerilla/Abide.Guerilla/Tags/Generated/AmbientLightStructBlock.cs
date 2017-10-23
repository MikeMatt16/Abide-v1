using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct AmbientLightStructBlock
	{
		[Field("Min lightmap sample", null)]
		public ColorRgbF MinLightmapSample1;
		[Field("Max lightmap sample", null)]
		public ColorRgbF MaxLightmapSample2;
		[Field("function", typeof(MappingFunctionBlock))]
		[Block("Mapping Function", 1, typeof(MappingFunctionBlock))]
		public MappingFunctionBlock Function5;
	}
}
#pragma warning restore CS1591
