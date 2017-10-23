using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("unit_hud_interface", "unhi", "����", typeof(UnitHudInterfaceBlock))]
	[FieldSet(1404, 4)]
	public unsafe struct UnitHudInterfaceBlock
	{
		public enum Anchor2Options
		{
			TopLeft_0 = 0,
			TopRight_1 = 1,
			BottomLeft_2 = 2,
			BottomRight_3 = 3,
			Center_4 = 4,
			Crosshair_5 = 5,
		}
		public enum ScalingFlags9Options
		{
			DonTScaleOffset_0 = 1,
			DonTScaleSize_1 = 2,
		}
		public enum FlashFlags18Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		public enum ScalingFlags30Options
		{
			DonTScaleOffset_0 = 1,
			DonTScaleSize_1 = 2,
		}
		public enum FlashFlags39Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		public enum ScalingFlags51Options
		{
			DonTScaleOffset_0 = 1,
			DonTScaleSize_1 = 2,
		}
		public enum Flags59Options
		{
			UseMinMaxForStateChanges_0 = 1,
			InterpolateBetweenMinMaxFlashColorsAsStateChanges_1 = 2,
			InterpolateColorAlongHsvSpace_2 = 4,
			MoreColorsForHsvInterpolation_3 = 8,
			InvertInterpolation_4 = 16,
		}
		public enum ScalingFlags79Options
		{
			DonTScaleOffset_0 = 1,
			DonTScaleSize_1 = 2,
		}
		public enum FlashFlags88Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		public enum ScalingFlags100Options
		{
			DonTScaleOffset_0 = 1,
			DonTScaleSize_1 = 2,
		}
		public enum Flags108Options
		{
			UseMinMaxForStateChanges_0 = 1,
			InterpolateBetweenMinMaxFlashColorsAsStateChanges_1 = 2,
			InterpolateColorAlongHsvSpace_2 = 4,
			MoreColorsForHsvInterpolation_3 = 8,
			InvertInterpolation_4 = 16,
		}
		public enum ScalingFlags127Options
		{
			DonTScaleOffset_0 = 1,
			DonTScaleSize_1 = 2,
		}
		public enum FlashFlags136Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		public enum ScalingFlags148Options
		{
			DonTScaleOffset_0 = 1,
			DonTScaleSize_1 = 2,
		}
		public enum FlashFlags157Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		public enum ScalingFlags170Options
		{
			DonTScaleOffset_0 = 1,
			DonTScaleSize_1 = 2,
		}
		public enum Anchor174Options
		{
			TopLeft_0 = 0,
			TopRight_1 = 1,
			BottomLeft_2 = 2,
			BottomRight_3 = 3,
			Center_4 = 4,
			Crosshair_5 = 5,
		}
		[Field("anchor", typeof(Anchor2Options))]
		public short Anchor2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("", null)]
		public fixed byte _4[32];
		[Field("anchor offset", null)]
		public Vector2 AnchorOffset6;
		[Field("width scale", null)]
		public float WidthScale7;
		[Field("height scale", null)]
		public float HeightScale8;
		[Field("scaling flags", typeof(ScalingFlags9Options))]
		public short ScalingFlags9;
		[Field("", null)]
		public fixed byte _10[2];
		[Field("", null)]
		public fixed byte _11[20];
		[Field("interface bitmap", null)]
		public TagReference InterfaceBitmap12;
		[Field("default color", null)]
		public ColorArgb DefaultColor13;
		[Field("flashing color", null)]
		public ColorArgb FlashingColor14;
		[Field("flash period", null)]
		public float FlashPeriod15;
		[Field("flash delay#time between flashes", null)]
		public float FlashDelay16;
		[Field("number of flashes", null)]
		public short NumberOfFlashes17;
		[Field("flash flags", typeof(FlashFlags18Options))]
		public short FlashFlags18;
		[Field("flash length#time of each flash", null)]
		public float FlashLength19;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor20;
		[Field("", null)]
		public fixed byte _21[4];
		[Field("sequence index", null)]
		public short SequenceIndex22;
		[Field("", null)]
		public fixed byte _23[2];
		[Field("multitex overlay", null)]
		[Block("Global Hud Multitexture Overlay Definition", 30, typeof(GlobalHudMultitextureOverlayDefinition))]
		public TagBlock MultitexOverlay24;
		[Field("", null)]
		public fixed byte _25[4];
		[Field("anchor offset", null)]
		public Vector2 AnchorOffset27;
		[Field("width scale", null)]
		public float WidthScale28;
		[Field("height scale", null)]
		public float HeightScale29;
		[Field("scaling flags", typeof(ScalingFlags30Options))]
		public short ScalingFlags30;
		[Field("", null)]
		public fixed byte _31[2];
		[Field("", null)]
		public fixed byte _32[20];
		[Field("interface bitmap", null)]
		public TagReference InterfaceBitmap33;
		[Field("default color", null)]
		public ColorArgb DefaultColor34;
		[Field("flashing color", null)]
		public ColorArgb FlashingColor35;
		[Field("flash period", null)]
		public float FlashPeriod36;
		[Field("flash delay#time between flashes", null)]
		public float FlashDelay37;
		[Field("number of flashes", null)]
		public short NumberOfFlashes38;
		[Field("flash flags", typeof(FlashFlags39Options))]
		public short FlashFlags39;
		[Field("flash length#time of each flash", null)]
		public float FlashLength40;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor41;
		[Field("", null)]
		public fixed byte _42[4];
		[Field("sequence index", null)]
		public short SequenceIndex43;
		[Field("", null)]
		public fixed byte _44[2];
		[Field("multitex overlay", null)]
		[Block("Global Hud Multitexture Overlay Definition", 30, typeof(GlobalHudMultitextureOverlayDefinition))]
		public TagBlock MultitexOverlay45;
		[Field("", null)]
		public fixed byte _46[4];
		[Field("anchor offset", null)]
		public Vector2 AnchorOffset48;
		[Field("width scale", null)]
		public float WidthScale49;
		[Field("height scale", null)]
		public float HeightScale50;
		[Field("scaling flags", typeof(ScalingFlags51Options))]
		public short ScalingFlags51;
		[Field("", null)]
		public fixed byte _52[2];
		[Field("", null)]
		public fixed byte _53[20];
		[Field("meter bitmap", null)]
		public TagReference MeterBitmap54;
		[Field("color at meter minimum", null)]
		public ColorRgb ColorAtMeterMinimum55;
		[Field("color at meter maximum", null)]
		public ColorRgb ColorAtMeterMaximum56;
		[Field("flash color", null)]
		public ColorRgb FlashColor57;
		[Field("empty color", null)]
		public ColorArgb EmptyColor58;
		[Field("flags", typeof(Flags59Options))]
		public byte Flags59;
		[Field("minumum meter value", null)]
		public int MinumumMeterValue60;
		[Field("sequence index", null)]
		public short SequenceIndex61;
		[Field("alpha multiplier", null)]
		public int AlphaMultiplier62;
		[Field("alpha bias", null)]
		public int AlphaBias63;
		[Field("value scale#used for non-integral values, i.e. health and shields", null)]
		public short ValueScale64;
		[Field("opacity", null)]
		public float Opacity65;
		[Field("translucency", null)]
		public float Translucency66;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor67;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _68;
		[Field("", null)]
		public fixed byte _69[4];
		[Field("overcharge minimum color", null)]
		public ColorRgb OverchargeMinimumColor70;
		[Field("overcharge maximum color", null)]
		public ColorRgb OverchargeMaximumColor71;
		[Field("overcharge flash color", null)]
		public ColorRgb OverchargeFlashColor72;
		[Field("overcharge empty color", null)]
		public ColorRgb OverchargeEmptyColor73;
		[Field("", null)]
		public fixed byte _74[16];
		[Field("anchor offset", null)]
		public Vector2 AnchorOffset76;
		[Field("width scale", null)]
		public float WidthScale77;
		[Field("height scale", null)]
		public float HeightScale78;
		[Field("scaling flags", typeof(ScalingFlags79Options))]
		public short ScalingFlags79;
		[Field("", null)]
		public fixed byte _80[2];
		[Field("", null)]
		public fixed byte _81[20];
		[Field("interface bitmap", null)]
		public TagReference InterfaceBitmap82;
		[Field("default color", null)]
		public ColorArgb DefaultColor83;
		[Field("flashing color", null)]
		public ColorArgb FlashingColor84;
		[Field("flash period", null)]
		public float FlashPeriod85;
		[Field("flash delay#time between flashes", null)]
		public float FlashDelay86;
		[Field("number of flashes", null)]
		public short NumberOfFlashes87;
		[Field("flash flags", typeof(FlashFlags88Options))]
		public short FlashFlags88;
		[Field("flash length#time of each flash", null)]
		public float FlashLength89;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor90;
		[Field("", null)]
		public fixed byte _91[4];
		[Field("sequence index", null)]
		public short SequenceIndex92;
		[Field("", null)]
		public fixed byte _93[2];
		[Field("multitex overlay", null)]
		[Block("Global Hud Multitexture Overlay Definition", 30, typeof(GlobalHudMultitextureOverlayDefinition))]
		public TagBlock MultitexOverlay94;
		[Field("", null)]
		public fixed byte _95[4];
		[Field("anchor offset", null)]
		public Vector2 AnchorOffset97;
		[Field("width scale", null)]
		public float WidthScale98;
		[Field("height scale", null)]
		public float HeightScale99;
		[Field("scaling flags", typeof(ScalingFlags100Options))]
		public short ScalingFlags100;
		[Field("", null)]
		public fixed byte _101[2];
		[Field("", null)]
		public fixed byte _102[20];
		[Field("meter bitmap", null)]
		public TagReference MeterBitmap103;
		[Field("color at meter minimum", null)]
		public ColorRgb ColorAtMeterMinimum104;
		[Field("color at meter maximum", null)]
		public ColorRgb ColorAtMeterMaximum105;
		[Field("flash color", null)]
		public ColorRgb FlashColor106;
		[Field("empty color", null)]
		public ColorArgb EmptyColor107;
		[Field("flags", typeof(Flags108Options))]
		public byte Flags108;
		[Field("minumum meter value", null)]
		public int MinumumMeterValue109;
		[Field("sequence index", null)]
		public short SequenceIndex110;
		[Field("alpha multiplier", null)]
		public int AlphaMultiplier111;
		[Field("alpha bias", null)]
		public int AlphaBias112;
		[Field("value scale#used for non-integral values, i.e. health and shields", null)]
		public short ValueScale113;
		[Field("opacity", null)]
		public float Opacity114;
		[Field("translucency", null)]
		public float Translucency115;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor116;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _117;
		[Field("", null)]
		public fixed byte _118[4];
		[Field("medium health left color", null)]
		public ColorRgb MediumHealthLeftColor119;
		[Field("max color health fraction cutoff", null)]
		public float MaxColorHealthFractionCutoff120;
		[Field("min color health fraction cutoff", null)]
		public float MinColorHealthFractionCutoff121;
		[Field("", null)]
		public fixed byte _122[20];
		[Field("anchor offset", null)]
		public Vector2 AnchorOffset124;
		[Field("width scale", null)]
		public float WidthScale125;
		[Field("height scale", null)]
		public float HeightScale126;
		[Field("scaling flags", typeof(ScalingFlags127Options))]
		public short ScalingFlags127;
		[Field("", null)]
		public fixed byte _128[2];
		[Field("", null)]
		public fixed byte _129[20];
		[Field("interface bitmap", null)]
		public TagReference InterfaceBitmap130;
		[Field("default color", null)]
		public ColorArgb DefaultColor131;
		[Field("flashing color", null)]
		public ColorArgb FlashingColor132;
		[Field("flash period", null)]
		public float FlashPeriod133;
		[Field("flash delay#time between flashes", null)]
		public float FlashDelay134;
		[Field("number of flashes", null)]
		public short NumberOfFlashes135;
		[Field("flash flags", typeof(FlashFlags136Options))]
		public short FlashFlags136;
		[Field("flash length#time of each flash", null)]
		public float FlashLength137;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor138;
		[Field("", null)]
		public fixed byte _139[4];
		[Field("sequence index", null)]
		public short SequenceIndex140;
		[Field("", null)]
		public fixed byte _141[2];
		[Field("multitex overlay", null)]
		[Block("Global Hud Multitexture Overlay Definition", 30, typeof(GlobalHudMultitextureOverlayDefinition))]
		public TagBlock MultitexOverlay142;
		[Field("", null)]
		public fixed byte _143[4];
		[Field("anchor offset", null)]
		public Vector2 AnchorOffset145;
		[Field("width scale", null)]
		public float WidthScale146;
		[Field("height scale", null)]
		public float HeightScale147;
		[Field("scaling flags", typeof(ScalingFlags148Options))]
		public short ScalingFlags148;
		[Field("", null)]
		public fixed byte _149[2];
		[Field("", null)]
		public fixed byte _150[20];
		[Field("interface bitmap", null)]
		public TagReference InterfaceBitmap151;
		[Field("default color", null)]
		public ColorArgb DefaultColor152;
		[Field("flashing color", null)]
		public ColorArgb FlashingColor153;
		[Field("flash period", null)]
		public float FlashPeriod154;
		[Field("flash delay#time between flashes", null)]
		public float FlashDelay155;
		[Field("number of flashes", null)]
		public short NumberOfFlashes156;
		[Field("flash flags", typeof(FlashFlags157Options))]
		public short FlashFlags157;
		[Field("flash length#time of each flash", null)]
		public float FlashLength158;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor159;
		[Field("", null)]
		public fixed byte _160[4];
		[Field("sequence index", null)]
		public short SequenceIndex161;
		[Field("", null)]
		public fixed byte _162[2];
		[Field("multitex overlay", null)]
		[Block("Global Hud Multitexture Overlay Definition", 30, typeof(GlobalHudMultitextureOverlayDefinition))]
		public TagBlock MultitexOverlay163;
		[Field("", null)]
		public fixed byte _164[4];
		[Field("", null)]
		public fixed byte _165[32];
		[Field("anchor offset", null)]
		public Vector2 AnchorOffset167;
		[Field("width scale", null)]
		public float WidthScale168;
		[Field("height scale", null)]
		public float HeightScale169;
		[Field("scaling flags", typeof(ScalingFlags170Options))]
		public short ScalingFlags170;
		[Field("", null)]
		public fixed byte _171[2];
		[Field("", null)]
		public fixed byte _172[20];
		[Field("anchor", typeof(Anchor174Options))]
		public short Anchor174;
		[Field("", null)]
		public fixed byte _175[2];
		[Field("", null)]
		public fixed byte _176[32];
		[Field("overlays", null)]
		[Block("Unit Hud Auxilary Overlay Block", 16, typeof(UnitHudAuxilaryOverlayBlock))]
		public TagBlock Overlays177;
		[Field("", null)]
		public fixed byte _178[16];
		[Field("sounds", null)]
		[Block("Unit Hud Sound Block", 12, typeof(UnitHudSoundBlock))]
		public TagBlock Sounds180;
		[Field("meters", null)]
		[Block("Unit Hud Auxilary Panel Block", 16, typeof(UnitHudAuxilaryPanelBlock))]
		public TagBlock Meters182;
		[Field("new hud", null)]
		public TagReference NewHud184;
		[Field("", null)]
		public fixed byte _185[356];
		[Field("", null)]
		public fixed byte _186[48];
	}
}
#pragma warning restore CS1591
