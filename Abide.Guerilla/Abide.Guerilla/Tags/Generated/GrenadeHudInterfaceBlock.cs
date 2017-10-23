using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("grenade_hud_interface", "grhi", "����", typeof(GrenadeHudInterfaceBlock))]
	[FieldSet(504, 4)]
	public unsafe struct GrenadeHudInterfaceBlock
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
		public enum FlashFlags59Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		public enum Flags64Options
		{
			ShowLeadingZeros_0 = 1,
			OnlyShowWhenZoomed_1 = 2,
			DrawATrailingM_2 = 4,
		}
		public enum Flags81Options
		{
			UseTextFromStringListInstead_0 = 1,
			OverrideDefaultColor_1 = 2,
			WidthOffsetIsAbsoluteIconWidth_2 = 4,
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
		[Field("default color", null)]
		public ColorArgb DefaultColor54;
		[Field("flashing color", null)]
		public ColorArgb FlashingColor55;
		[Field("flash period", null)]
		public float FlashPeriod56;
		[Field("flash delay#time between flashes", null)]
		public float FlashDelay57;
		[Field("number of flashes", null)]
		public short NumberOfFlashes58;
		[Field("flash flags", typeof(FlashFlags59Options))]
		public short FlashFlags59;
		[Field("flash length#time of each flash", null)]
		public float FlashLength60;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor61;
		[Field("", null)]
		public fixed byte _62[4];
		[Field("maximum number of digits", null)]
		public int MaximumNumberOfDigits63;
		[Field("flags", typeof(Flags64Options))]
		public byte Flags64;
		[Field("number of fractional digits", null)]
		public int NumberOfFractionalDigits65;
		[Field("", null)]
		public fixed byte _66[1];
		[Field("", null)]
		public fixed byte _67[12];
		[Field("flash cutoff", null)]
		public short FlashCutoff68;
		[Field("", null)]
		public fixed byte _69[2];
		[Field("Overlay bitmap", null)]
		public TagReference OverlayBitmap71;
		[Field("Overlays", null)]
		[Block("Grenade Hud Overlay Block", 16, typeof(GrenadeHudOverlayBlock))]
		public TagBlock Overlays72;
		[Field("Warning sounds", null)]
		[Block("Grenade Hud Sound Block", 12, typeof(GrenadeHudSoundBlock))]
		public TagBlock WarningSounds73;
		[Field("", null)]
		public fixed byte _74[68];
		[Field("sequence index#sequence index into the global hud icon bitmap", null)]
		public short SequenceIndex76;
		[Field("width offset#extra spacing beyond bitmap width for text alignment", null)]
		public short WidthOffset77;
		[Field("offset from reference corner", null)]
		public Vector2 OffsetFromReferenceCorner78;
		[Field("override icon color", null)]
		public ColorArgb OverrideIconColor79;
		[Field("frame rate [0,30]", null)]
		public int FrameRate03080;
		[Field("flags", typeof(Flags81Options))]
		public byte Flags81;
		[Field("text index", null)]
		public short TextIndex82;
		[Field("", null)]
		public fixed byte _83[48];
	}
}
#pragma warning restore CS1591
