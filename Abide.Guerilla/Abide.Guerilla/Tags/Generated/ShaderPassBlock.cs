using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("shader_pass", "spas", "����", typeof(ShaderPassBlock))]
	[FieldSet(60, 4)]
	public unsafe struct ShaderPassBlock
	{
		[Field("Documentation", null)]
		[Data(65535)]
		public TagBlock Documentation0;
		[Field("Parameters", null)]
		[Block("Parameter", 64, typeof(ShaderPassParameterBlock))]
		public TagBlock Parameters1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("", null)]
		public fixed byte _3[2];
		[Field("Implementations", null)]
		[Block("Implementation", 32, typeof(ShaderPassImplementationBlock))]
		public TagBlock Implementations4;
		[Field("Postprocess Definition*", null)]
		[Block("Shader Pass Postprocess Definition New Block", 1, typeof(ShaderPassPostprocessDefinitionNewBlock))]
		public TagBlock PostprocessDefinition5;
	}
}
#pragma warning restore CS1591
