using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("shader_light_response", "slit", "����", typeof(ShaderLightResponseBlock))]
	[FieldSet(28, 4)]
	public unsafe struct ShaderLightResponseBlock
	{
		[Field("categories", null)]
		[Block("Category", 16, typeof(ShaderTemplateCategoryBlock))]
		public TagBlock Categories0;
		[Field("shader LODs", null)]
		[Block("Shader Template Level Of Detail Block", 8, typeof(ShaderTemplateLevelOfDetailBlock))]
		public TagBlock ShaderLODs1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("", null)]
		public fixed byte _3[2];
	}
}
#pragma warning restore CS1591
