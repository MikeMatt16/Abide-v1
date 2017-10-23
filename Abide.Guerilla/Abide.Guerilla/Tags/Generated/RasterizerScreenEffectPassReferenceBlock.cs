using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(192, 4)]
	public unsafe struct RasterizerScreenEffectPassReferenceBlock
	{
		public enum Stage0Mode10Options
		{
			Default_0 = 0,
			ViewportNormalized_1 = 1,
			ViewportRelative_2 = 2,
			FramebufferRelative_3 = 3,
			Zero_4 = 4,
		}
		public enum Stage1Mode11Options
		{
			Default_0 = 0,
			ViewportNormalized_1 = 1,
			ViewportRelative_2 = 2,
			FramebufferRelative_3 = 3,
			Zero_4 = 4,
		}
		public enum Stage2Mode12Options
		{
			Default_0 = 0,
			ViewportNormalized_1 = 1,
			ViewportRelative_2 = 2,
			FramebufferRelative_3 = 3,
			Zero_4 = 4,
		}
		public enum Stage3Mode13Options
		{
			Default_0 = 0,
			ViewportNormalized_1 = 1,
			ViewportRelative_2 = 2,
			FramebufferRelative_3 = 3,
			Zero_4 = 4,
		}
		public enum Target16Options
		{
			Framebuffer_0 = 0,
			Texaccum_1 = 1,
			TexaccumSmall_2 = 2,
			TexaccumTiny_3 = 3,
			CopyFbToTexaccum_4 = 4,
		}
		[Field("explanation", null)]
		[Data(65535)]
		public TagBlock Explanation0;
		[Field("layer pass index*:leave as -1 unless debugging", null)]
		public short LayerPassIndex2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("primary=0 and secondary=0:implementation index", null)]
		public int Primary0AndSecondary04;
		[Field("primary>0 and secondary=0:implementation index", null)]
		public int Primary0AndSecondary05;
		[Field("primary=0 and secondary>0:implementation index", null)]
		public int Primary0AndSecondary06;
		[Field("primary>0 and secondary>0:implementation index", null)]
		public int Primary0AndSecondary07;
		[Field("", null)]
		public fixed byte _8[64];
		[Field("stage 0 mode", typeof(Stage0Mode10Options))]
		public short Stage0Mode10;
		[Field("stage 1 mode", typeof(Stage1Mode11Options))]
		public short Stage1Mode11;
		[Field("stage 2 mode", typeof(Stage2Mode12Options))]
		public short Stage2Mode12;
		[Field("stage 3 mode", typeof(Stage3Mode13Options))]
		public short Stage3Mode13;
		[Field("advanced control", null)]
		[Block("Advanced Control", 1, typeof(RasterizerScreenEffectTexcoordGenerationAdvancedControlBlock))]
		public TagBlock AdvancedControl14;
		[Field("target", typeof(Target16Options))]
		public short Target16;
		[Field("", null)]
		public fixed byte _17[2];
		[Field("", null)]
		public fixed byte _18[64];
		[Field("convolution", null)]
		[Block("Convolution", 2, typeof(RasterizerScreenEffectConvolutionBlock))]
		public TagBlock Convolution19;
	}
}
#pragma warning restore CS1591
