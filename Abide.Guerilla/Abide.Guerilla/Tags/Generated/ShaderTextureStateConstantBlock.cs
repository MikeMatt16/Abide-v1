using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ShaderTextureStateConstantBlock
	{
		public enum Constant2Options
		{
			MipmapBiasValue_0 = 0,
			ColorkeyColor_1 = 1,
			BorderColor_2 = 2,
			BorderAlphaValue_3 = 3,
			BumpenvMat00_4 = 4,
			BumpenvMat01_5 = 5,
			BumpenvMat10_6 = 6,
			BumpenvMat11_7 = 7,
			BumpenvLumScaleValue_8 = 8,
			BumpenvLumOffsetValue_9 = 9,
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
