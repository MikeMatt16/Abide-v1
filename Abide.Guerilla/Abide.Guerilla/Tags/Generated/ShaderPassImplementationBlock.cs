using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(184, 4)]
	public unsafe struct ShaderPassImplementationBlock
	{
		public enum Flags0Options
		{
			DeleteFromCacheFile_0 = 1,
			Critical_1 = 2,
		}
		public enum Channels10Options
		{
			All_0 = 0,
			ColorOnly_1 = 1,
			AlphaOnly_2 = 2,
			Custom_3 = 3,
		}
		public enum AlphaBlend11Options
		{
			Disabled_0 = 0,
			Add_1 = 1,
			Multiply_2 = 2,
			AddSrcTimesDstalpha_3 = 3,
			AddSrcTimesSrcalpha_4 = 4,
			AddDstTimesSrcalphaInverse_5 = 5,
			AlphaBlend_6 = 6,
			Custom_7 = 7,
		}
		public enum Depth12Options
		{
			Disabled_0 = 0,
			DefaultOpaque_1 = 1,
			DefaultOpaqueWrite_2 = 2,
			DefaultTransparent_3 = 3,
			Custom_4 = 4,
		}
		[Field("Flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("Textures", null)]
		[Block("Texture Stage", 8, typeof(ShaderPassTextureBlock))]
		public TagBlock Textures2;
		[Field("Vertex Shader", null)]
		public TagReference VertexShader4;
		[Field("vs Constants", null)]
		[Block("Vs Constant", 32, typeof(ShaderPassVertexShaderConstantBlock))]
		public TagBlock VsConstants5;
		[Field("", null)]
		public fixed byte _6[4];
		[Field("Pixel Shader Code [NO LONGER USED]", null)]
		[Data(65535)]
		public TagBlock PixelShaderCodeNOLONGERUSED7;
		[Field("", null)]
		public fixed byte _8[12];
		[Field("channels", typeof(Channels10Options))]
		public short Channels10;
		[Field("alpha-blend", typeof(AlphaBlend11Options))]
		public short AlphaBlend11;
		[Field("depth", typeof(Depth12Options))]
		public short Depth12;
		[Field("", null)]
		public fixed byte _13[2];
		[Field("channel state", null)]
		[Block("Channels", 1, typeof(ShaderStateChannelsStateBlock))]
		public TagBlock ChannelState14;
		[Field("alpha-blend state", null)]
		[Block("Alpha-blend State", 1, typeof(ShaderStateAlphaBlendStateBlock))]
		public TagBlock AlphaBlendState15;
		[Field("alpha-test state", null)]
		[Block("Alpha-test State", 1, typeof(ShaderStateAlphaTestStateBlock))]
		public TagBlock AlphaTestState16;
		[Field("depth state", null)]
		[Block("Depth State", 1, typeof(ShaderStateDepthStateBlock))]
		public TagBlock DepthState17;
		[Field("cull state", null)]
		[Block("Cull State", 1, typeof(ShaderStateCullStateBlock))]
		public TagBlock CullState18;
		[Field("fill state", null)]
		[Block("Fill State", 1, typeof(ShaderStateFillStateBlock))]
		public TagBlock FillState19;
		[Field("misc state", null)]
		[Block("Misc State", 1, typeof(ShaderStateMiscStateBlock))]
		public TagBlock MiscState20;
		[Field("constants", null)]
		[Block("Render State Constant", 7, typeof(ShaderStateConstantBlock))]
		public TagBlock Constants21;
		[Field("Pixel Shader", null)]
		public TagReference PixelShader22;
		[Field("", null)]
		public fixed byte _23[224];
	}
}
#pragma warning restore CS1591
