using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(60, 4)]
	public unsafe struct ShaderTemplatePostprocessDefinitionNewBlock
	{
		[Field("levels of detail", null)]
		[Block("Shader Template Postprocess Level Of Detail New Block", 1024, typeof(ShaderTemplatePostprocessLevelOfDetailNewBlock))]
		public TagBlock LevelsOfDetail0;
		[Field("layers", null)]
		[Block("Tag Block Index Block", 1024, typeof(TagBlockIndexBlock))]
		public TagBlock Layers1;
		[Field("passes", null)]
		[Block("Shader Template Postprocess Pass New Block", 1024, typeof(ShaderTemplatePostprocessPassNewBlock))]
		public TagBlock Passes2;
		[Field("implementations", null)]
		[Block("Shader Template Postprocess Implementation New Block", 1024, typeof(ShaderTemplatePostprocessImplementationNewBlock))]
		public TagBlock Implementations3;
		[Field("remappings", null)]
		[Block("Shader Template Postprocess Remapping New Block", 1024, typeof(ShaderTemplatePostprocessRemappingNewBlock))]
		public TagBlock Remappings4;
	}
}
#pragma warning restore CS1591
