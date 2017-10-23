using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("meter", "metr", "����", typeof(MeterBlock))]
	[FieldSet(172, 4)]
	public unsafe struct MeterBlock
	{
		public enum Flags0Options
		{
		}
		public enum InterpolateColors7Options
		{
			Linearly_0 = 0,
			FasterNearEmpty_1 = 1,
			FasterNearFull_2 = 2,
			ThroughRandomNoise_3 = 3,
		}
		public enum AnchorColors8Options
		{
			AtBothEnds_0 = 0,
			AtEmpty_1 = 1,
			AtFull_2 = 2,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("stencil bitmaps#two bitmaps specifying the mask and the meter levels", null)]
		public TagReference StencilBitmaps1;
		[Field("source bitmap#optional bitmap to draw into the unmasked regions of the meter (modulated by the colors below)", null)]
		public TagReference SourceBitmap2;
		[Field("stencil sequence index", null)]
		public short StencilSequenceIndex3;
		[Field("source sequence index", null)]
		public short SourceSequenceIndex4;
		[Field("", null)]
		public fixed byte _5[16];
		[Field("", null)]
		public fixed byte _6[4];
		[Field("interpolate colors...", typeof(InterpolateColors7Options))]
		public short InterpolateColors7;
		[Field("anchor colors...", typeof(AnchorColors8Options))]
		public short AnchorColors8;
		[Field("", null)]
		public fixed byte _9[8];
		[Field("empty color", null)]
		public ColorArgbF EmptyColor10;
		[Field("full color", null)]
		public ColorArgbF FullColor11;
		[Field("", null)]
		public fixed byte _12[20];
		[Field("unmask distance:meter units#fade from fully masked to fully unmasked this distance beyond full (and below empty)", null)]
		public float UnmaskDistance13;
		[Field("mask distance:meter units#fade from fully unmasked to fully masked this distance below full (and beyond empty)", null)]
		public float MaskDistance14;
		[Field("", null)]
		public fixed byte _15[20];
		[Field("encoded stencil", null)]
		[Data(65536)]
		public TagBlock EncodedStencil16;
	}
}
#pragma warning restore CS1591
