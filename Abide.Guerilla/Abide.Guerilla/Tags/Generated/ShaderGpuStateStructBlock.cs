using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(84, 4)]
	public unsafe struct ShaderGpuStateStructBlock
	{
		[Field("render states", null)]
		[Block("Render State Block", 1024, typeof(RenderStateBlock))]
		public TagBlock RenderStates0;
		[Field("texture stage states", null)]
		[Block("Texture Stage State Block", 1024, typeof(TextureStageStateBlock))]
		public TagBlock TextureStageStates1;
		[Field("render state parameters", null)]
		[Block("Render State Parameter Block", 1024, typeof(RenderStateParameterBlock))]
		public TagBlock RenderStateParameters2;
		[Field("texture stage parameters", null)]
		[Block("Texture Stage State Parameter Block", 1024, typeof(TextureStageStateParameterBlock))]
		public TagBlock TextureStageParameters3;
		[Field("textures", null)]
		[Block("Texture Block", 1024, typeof(TextureBlock))]
		public TagBlock Textures4;
		[Field("Vn Constants", null)]
		[Block("Vertex Shader Constant Block", 1024, typeof(VertexShaderConstantBlock))]
		public TagBlock VnConstants5;
		[Field("Cn Constants", null)]
		[Block("Vertex Shader Constant Block", 1024, typeof(VertexShaderConstantBlock))]
		public TagBlock CnConstants6;
	}
}
#pragma warning restore CS1591
