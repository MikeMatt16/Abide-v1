using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(132, 4)]
	public unsafe struct ShaderPassPostprocessDefinitionNewBlock
	{
		[Field("implementations", null)]
		[Block("Shader Pass Postprocess Implementation New Block", 1024, typeof(ShaderPassPostprocessImplementationNewBlock))]
		public TagBlock Implementations0;
		[Field("textures", null)]
		[Block("Shader Pass Postprocess Texture New Block", 1024, typeof(ShaderPassPostprocessTextureNewBlock))]
		public TagBlock Textures1;
		[Field("render states", null)]
		[Block("Render State Block", 1024, typeof(RenderStateBlock))]
		public TagBlock RenderStates2;
		[Field("texture states", null)]
		[Block("Shader Pass Postprocess Texture State Block", 1024, typeof(ShaderPassPostprocessTextureStateBlock))]
		public TagBlock TextureStates3;
		[Field("ps fragments", null)]
		[Block("Pixel Shader Fragment Block", 1024, typeof(PixelShaderFragmentBlock))]
		public TagBlock PsFragments4;
		[Field("ps permutations", null)]
		[Block("Pixel Shader Permutation New Block", 1024, typeof(PixelShaderPermutationNewBlock))]
		public TagBlock PsPermutations5;
		[Field("ps combiners", null)]
		[Block("Pixel Shader Combiner Block", 1024, typeof(PixelShaderCombinerBlock))]
		public TagBlock PsCombiners6;
		[Field("externs", null)]
		[Block("Shader Pass Postprocess Extern New Block", 1024, typeof(ShaderPassPostprocessExternNewBlock))]
		public TagBlock Externs7;
		[Field("constants", null)]
		[Block("Shader Pass Postprocess Constant New Block", 1024, typeof(ShaderPassPostprocessConstantNewBlock))]
		public TagBlock Constants8;
		[Field("constant info", null)]
		[Block("Shader Pass Postprocess Constant Info New Block", 1024, typeof(ShaderPassPostprocessConstantInfoNewBlock))]
		public TagBlock ConstantInfo9;
		[Field("old implementations", null)]
		[Block("Shader Pass Postprocess Implementation Block", 1024, typeof(ShaderPassPostprocessImplementationBlock))]
		public TagBlock OldImplementations10;
	}
}
#pragma warning restore CS1591
