using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct ShaderTextureStateFilterStateBlock
	{
		public enum MagFilter0Options
		{
			None_0 = 0,
			PointSampled_1 = 1,
			Linear_2 = 2,
			Anisotropic_3 = 3,
			Quincunx_4 = 4,
			GaussianCubic_5 = 5,
		}
		public enum MinFilter1Options
		{
			None_0 = 0,
			PointSampled_1 = 1,
			Linear_2 = 2,
			Anisotropic_3 = 3,
			Quincunx_4 = 4,
			GaussianCubic_5 = 5,
		}
		public enum MipFilter2Options
		{
			None_0 = 0,
			PointSampled_1 = 1,
			Linear_2 = 2,
			Anisotropic_3 = 3,
			Quincunx_4 = 4,
			GaussianCubic_5 = 5,
		}
		public enum Anisotropy6Options
		{
			NonAnisotropic_0 = 0,
			_2Tap_1 = 1,
			_3Tap_2 = 2,
			_4Tap_3 = 3,
		}
		[Field("mag filter", typeof(MagFilter0Options))]
		public short MagFilter0;
		[Field("min filter", typeof(MinFilter1Options))]
		public short MinFilter1;
		[Field("mip filter", typeof(MipFilter2Options))]
		public short MipFilter2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("mipmap bias", null)]
		public float MipmapBias4;
		[Field("max mipmap index#0 means all mipmap levels are used", null)]
		public short MaxMipmapIndex5;
		[Field("anisotropy", typeof(Anisotropy6Options))]
		public short Anisotropy6;
	}
}
#pragma warning restore CS1591
