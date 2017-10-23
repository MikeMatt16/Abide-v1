using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(188, 4)]
	public unsafe struct LightVolumeVolumeBlock
	{
		public enum Flags1Options
		{
			ForceLinearRadiusFunction_0 = 1,
			ForceLinearOffset_1 = 2,
			ForceDifferentialEvaluation_2 = 4,
			Fuzzy_3 = 8,
			NotScaledByEventDuration_4 = 16,
			ScaledByMarker_5 = 32,
		}
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("", null)]
		public fixed byte _2[16];
		[Field("bitmap", null)]
		public TagReference Bitmap3;
		[Field("sprite count:[4,256]", null)]
		public int SpriteCount4;
		[Field("", null)]
		public fixed byte _5[32];
		[Field("offset function", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock OffsetFunction7;
		[Field("radius function", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock RadiusFunction9;
		[Field("brightness function", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock BrightnessFunction11;
		[Field("color function", typeof(ColorFunctionStructBlock))]
		[Block("Color Function Struct", 1, typeof(ColorFunctionStructBlock))]
		public ColorFunctionStructBlock ColorFunction13;
		[Field("", null)]
		public fixed byte _14[64];
		[Field("facing function", typeof(ScalarFunctionStructBlock))]
		[Block("Scalar Function Struct", 1, typeof(ScalarFunctionStructBlock))]
		public ScalarFunctionStructBlock FacingFunction16;
		[Field("", null)]
		public fixed byte _17[64];
		[Field("aspect", null)]
		[Block("Aspect", 1, typeof(LightVolumeAspectBlock))]
		public TagBlock Aspect18;
		[Field("", null)]
		public fixed byte _19[64];
		[Field("radius frac min*:[0.00390625, 1.0]", null)]
		public float RadiusFracMin21;
		[Field("DEPRECATED!! x-step exponent*:[0.5, 0.875]", null)]
		public float DEPRECATEDXStepExponent22;
		[Field("DEPRECATED!! x-buffer length*:[32, 512]", null)]
		public int DEPRECATEDXBufferLength23;
		[Field("x-buffer spacing*:[1, 256]", null)]
		public int XBufferSpacing24;
		[Field("x-buffer min iterations*:[1, 256]", null)]
		public int XBufferMinIterations25;
		[Field("x-buffer max iterations*:[1, 256]", null)]
		public int XBufferMaxIterations26;
		[Field("x-delta max error*:[0.001, 0.1]", null)]
		public float XDeltaMaxError27;
		[Field("", null)]
		public fixed byte _28[48];
		[Field("", null)]
		public fixed byte _29[4];
		[Field("*", null)]
		[Block("Light Volume Runtime Offset Block", 256, typeof(LightVolumeRuntimeOffsetBlock))]
		public TagBlock Empty30;
		[Field("", null)]
		public fixed byte _31[48];
	}
}
#pragma warning restore CS1591
