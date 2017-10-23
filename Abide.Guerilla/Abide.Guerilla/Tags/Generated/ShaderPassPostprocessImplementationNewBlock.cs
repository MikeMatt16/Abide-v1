using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(350, 4)]
	public unsafe struct ShaderPassPostprocessImplementationNewBlock
	{
		[Field("textures", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock Textures0;
		[Field("render states", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock RenderStates1;
		[Field("texture states", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock TextureStates2;
		[Field("", null)]
		public fixed byte _3[240];
		[Field("ps fragments", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock PsFragments4;
		[Field("ps permutations", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock PsPermutations5;
		[Field("ps combiners", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock PsCombiners6;
		[Field("vertex shader", null)]
		public TagReference VertexShader7;
		[Field("", null)]
		public fixed byte _8[8];
		[Field("", null)]
		public fixed byte _9[8];
		[Field("", null)]
		public fixed byte _10[4];
		[Field("", null)]
		public fixed byte _11[4];
		[Field("default render states", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock DefaultRenderStates12;
		[Field("render state externs", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock RenderStateExterns13;
		[Field("texture state externs", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock TextureStateExterns14;
		[Field("pixel constant externs", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock PixelConstantExterns15;
		[Field("vertex constant externs", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock VertexConstantExterns16;
		[Field("ps constants", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock PsConstants17;
		[Field("vs constants", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock VsConstants18;
		[Field("pixel constant info", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock PixelConstantInfo19;
		[Field("vertex constant info", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock VertexConstantInfo20;
		[Field("render state info", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock RenderStateInfo21;
		[Field("texture state info", typeof(TagBlockIndexStructBlock))]
		[Block("Tag Block Index Struct", 1, typeof(TagBlockIndexStructBlock))]
		public TagBlockIndexStructBlock TextureStateInfo22;
		[Field("pixel shader", null)]
		[Block("Shader Postprocess Pixel Shader", 100, typeof(ShaderPostprocessPixelShader))]
		public TagBlock PixelShader23;
		[Field("pixel shader switch extern map", null)]
		[Block("Pixel Shader Extern Map Block", 6, typeof(PixelShaderExternMapBlock))]
		public TagBlock PixelShaderSwitchExternMap24;
		[Field("pixel shader index block", null)]
		[Block("Pixel Shader Index Block", 100, typeof(PixelShaderIndexBlock))]
		public TagBlock PixelShaderIndexBlock25;
	}
}
#pragma warning restore CS1591
