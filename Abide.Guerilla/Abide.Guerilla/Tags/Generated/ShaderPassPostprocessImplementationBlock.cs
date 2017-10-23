using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(502, 4)]
	public unsafe struct ShaderPassPostprocessImplementationBlock
	{
		[Field("GPU State", typeof(ShaderGpuStateStructBlock))]
		[Block("Shader Gpu State Struct", 1, typeof(ShaderGpuStateStructBlock))]
		public ShaderGpuStateStructBlock GPUState0;
		[Field("GPU Constant State", typeof(ShaderGpuStateReferenceStructBlock))]
		[Block("Shader Gpu State Reference Struct", 1, typeof(ShaderGpuStateReferenceStructBlock))]
		public ShaderGpuStateReferenceStructBlock GPUConstantState1;
		[Field("GPU Volatile State", typeof(ShaderGpuStateReferenceStructBlock))]
		[Block("Shader Gpu State Reference Struct", 1, typeof(ShaderGpuStateReferenceStructBlock))]
		public ShaderGpuStateReferenceStructBlock GPUVolatileState2;
		[Field("GPU default state", typeof(ShaderGpuStateReferenceStructBlock))]
		[Block("Shader Gpu State Reference Struct", 1, typeof(ShaderGpuStateReferenceStructBlock))]
		public ShaderGpuStateReferenceStructBlock GPUDefaultState3;
		[Field("vertex shader", null)]
		public TagReference VertexShader4;
		[Field("", null)]
		public fixed byte _5[8];
		[Field("", null)]
		public fixed byte _6[8];
		[Field("", null)]
		public fixed byte _7[4];
		[Field("", null)]
		public fixed byte _8[4];
		[Field("value externs", null)]
		[Block("Extern Reference Block", 1024, typeof(ExternReferenceBlock))]
		public TagBlock ValueExterns9;
		[Field("color externs", null)]
		[Block("Extern Reference Block", 1024, typeof(ExternReferenceBlock))]
		public TagBlock ColorExterns10;
		[Field("switch externs", null)]
		[Block("Extern Reference Block", 1024, typeof(ExternReferenceBlock))]
		public TagBlock SwitchExterns11;
		[Field("bitmap parameter count", null)]
		public short BitmapParameterCount12;
		[Field("", null)]
		public fixed byte _13[2];
		[Field("", null)]
		public fixed byte _14[240];
		[Field("pixel shader fragments", null)]
		[Block("Pixel Shader Fragment Block", 1024, typeof(PixelShaderFragmentBlock))]
		public TagBlock PixelShaderFragments15;
		[Field("pixel shader permutations", null)]
		[Block("Pixel Shader Permutation Block", 1024, typeof(PixelShaderPermutationBlock))]
		public TagBlock PixelShaderPermutations16;
		[Field("pixel shader combiners", null)]
		[Block("Pixel Shader Combiner Block", 1024, typeof(PixelShaderCombinerBlock))]
		public TagBlock PixelShaderCombiners17;
		[Field("pixel shader constants", null)]
		[Block("Pixel Shader Constant Block", 1024, typeof(PixelShaderConstantBlock))]
		public TagBlock PixelShaderConstants18;
		[Field("", null)]
		public fixed byte _19[4];
		[Field("", null)]
		public fixed byte _20[4];
	}
}
#pragma warning restore CS1591
