using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(324, 4)]
	public unsafe struct UnitHudAuxilaryPanelBlock
	{
		public enum Type0Options
		{
			IntegratedLight_0 = 0,
		}
		public enum ScalingFlags7Options
		{
			DonTScaleOffset_0 = 1,
			DonTScaleSize_1 = 2,
		}
		public enum FlashFlags16Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		public enum ScalingFlags28Options
		{
			DonTScaleOffset_0 = 1,
			DonTScaleSize_1 = 2,
		}
		public enum Flags36Options
		{
			UseMinMaxForStateChanges_0 = 1,
			InterpolateBetweenMinMaxFlashColorsAsStateChanges_1 = 2,
			InterpolateColorAlongHsvSpace_2 = 4,
			MoreColorsForHsvInterpolation_3 = 8,
			InvertInterpolation_4 = 16,
		}
		public enum Flags48Options
		{
			ShowOnlyWhenActive_0 = 1,
			FlashOnceIfActivatedWhileDisabled_1 = 2,
		}
		[Field("type", typeof(Type0Options))]
		public short Type0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("", null)]
		public fixed byte _2[16];
		[Field("anchor offset", null)]
		public Vector2 AnchorOffset4;
		[Field("width scale", null)]
		public float WidthScale5;
		[Field("height scale", null)]
		public float HeightScale6;
		[Field("scaling flags", typeof(ScalingFlags7Options))]
		public short ScalingFlags7;
		[Field("", null)]
		public fixed byte _8[2];
		[Field("", null)]
		public fixed byte _9[20];
		[Field("interface bitmap", null)]
		public TagReference InterfaceBitmap10;
		[Field("default color", null)]
		public ColorArgb DefaultColor11;
		[Field("flashing color", null)]
		public ColorArgb FlashingColor12;
		[Field("flash period", null)]
		public float FlashPeriod13;
		[Field("flash delay#time between flashes", null)]
		public float FlashDelay14;
		[Field("number of flashes", null)]
		public short NumberOfFlashes15;
		[Field("flash flags", typeof(FlashFlags16Options))]
		public short FlashFlags16;
		[Field("flash length#time of each flash", null)]
		public float FlashLength17;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor18;
		[Field("", null)]
		public fixed byte _19[4];
		[Field("sequence index", null)]
		public short SequenceIndex20;
		[Field("", null)]
		public fixed byte _21[2];
		[Field("multitex overlay", null)]
		[Block("Global Hud Multitexture Overlay Definition", 30, typeof(GlobalHudMultitextureOverlayDefinition))]
		public TagBlock MultitexOverlay22;
		[Field("", null)]
		public fixed byte _23[4];
		[Field("anchor offset", null)]
		public Vector2 AnchorOffset25;
		[Field("width scale", null)]
		public float WidthScale26;
		[Field("height scale", null)]
		public float HeightScale27;
		[Field("scaling flags", typeof(ScalingFlags28Options))]
		public short ScalingFlags28;
		[Field("", null)]
		public fixed byte _29[2];
		[Field("", null)]
		public fixed byte _30[20];
		[Field("meter bitmap", null)]
		public TagReference MeterBitmap31;
		[Field("color at meter minimum", null)]
		public ColorRgb ColorAtMeterMinimum32;
		[Field("color at meter maximum", null)]
		public ColorRgb ColorAtMeterMaximum33;
		[Field("flash color", null)]
		public ColorRgb FlashColor34;
		[Field("empty color", null)]
		public ColorArgb EmptyColor35;
		[Field("flags", typeof(Flags36Options))]
		public byte Flags36;
		[Field("minumum meter value", null)]
		public int MinumumMeterValue37;
		[Field("sequence index", null)]
		public short SequenceIndex38;
		[Field("alpha multiplier", null)]
		public int AlphaMultiplier39;
		[Field("alpha bias", null)]
		public int AlphaBias40;
		[Field("value scale#used for non-integral values, i.e. health and shields", null)]
		public short ValueScale41;
		[Field("opacity", null)]
		public float Opacity42;
		[Field("translucency", null)]
		public float Translucency43;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor44;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _45;
		[Field("", null)]
		public fixed byte _46[4];
		[Field("minimum fraction cutoff", null)]
		public float MinimumFractionCutoff47;
		[Field("flags", typeof(Flags48Options))]
		public int Flags48;
		[Field("", null)]
		public fixed byte _49[24];
		[Field("", null)]
		public fixed byte _50[64];
	}
}
#pragma warning restore CS1591
