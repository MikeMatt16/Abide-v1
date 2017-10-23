using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ShaderStateConstantBlock
	{
		public enum Constant2Options
		{
			ConstantBlendColor_0 = 0,
			ConstantBlendAlphaValue_1 = 1,
			AlphaTestRefValue_2 = 2,
			DepthBiasSlopeScaleValue_3 = 3,
			DepthBiasValue_4 = 4,
			LineWidthValue_5 = 5,
			FogColor_6 = 6,
		}
		[Field("source parameter", null)]
		public StringId SourceParameter0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("constant^", typeof(Constant2Options))]
		public short Constant2;
	}
}
#pragma warning restore CS1591
