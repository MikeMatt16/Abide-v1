using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct ShaderTemplateCategoryBlock
	{
		[Field("Name^", null)]
		public StringId Name0;
		[Field("Parameters", null)]
		[Block("Parameter", 64, typeof(ShaderTemplateParameterBlock))]
		public TagBlock Parameters1;
	}
}
#pragma warning restore CS1591
