using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(84, 4)]
	public unsafe struct ShaderPostprocessPixelShader
	{
		[Field("pixel shader handle (runtime)", null)]
		public int PixelShaderHandleRuntime0;
		[Field("pixel shader handle (runtime)", null)]
		public int PixelShaderHandleRuntime1;
		[Field("pixel shader handle (runtime)", null)]
		public int PixelShaderHandleRuntime2;
		[Field("constant register defaults", null)]
		[Block("Shader Postprocess Pixel Shader Constant Defaults", 32, typeof(ShaderPostprocessPixelShaderConstantDefaults))]
		public TagBlock ConstantRegisterDefaults3;
		[Field("compiled shader", null)]
		[Data(32768)]
		public TagBlock CompiledShader4;
		[Field("compiled shader", null)]
		[Data(32768)]
		public TagBlock CompiledShader5;
		[Field("compiled shader", null)]
		[Data(32768)]
		public TagBlock CompiledShader6;
	}
}
#pragma warning restore CS1591
