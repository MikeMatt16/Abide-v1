using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("hud_globals", "hudg", "����", typeof(HudGlobalsBlock))]
	[FieldSet(1364, 4)]
	public unsafe struct HudGlobalsBlock
	{
		public enum Anchor1Options
		{
			TopLeft_0 = 0,
			TopRight_1 = 1,
			BottomLeft_2 = 2,
			BottomRight_3 = 3,
			Center_4 = 4,
			Crosshair_5 = 5,
		}
		public enum ScalingFlags7Options
		{
			DonTScaleOffset_0 = 1,
			DonTScaleSize_1 = 2,
		}
		public enum FlashFlags27Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		public enum FlashFlags39Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		public enum FlashFlags81Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		public enum FlashFlags91Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		[Field("anchor", typeof(Anchor1Options))]
		public short Anchor1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("", null)]
		public fixed byte _3[32];
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
		[Field("obsolete1", null)]
		public TagReference Obsolete110;
		[Field("obsolete2", null)]
		public TagReference Obsolete211;
		[Field("up time", null)]
		public float UpTime12;
		[Field("fade time", null)]
		public float FadeTime13;
		[Field("icon color", null)]
		public ColorArgbF IconColor14;
		[Field("text color", null)]
		public ColorArgbF TextColor15;
		[Field("text spacing", null)]
		public float TextSpacing16;
		[Field("item message text", null)]
		public TagReference ItemMessageText17;
		[Field("icon bitmap", null)]
		public TagReference IconBitmap18;
		[Field("alternate icon text", null)]
		public TagReference AlternateIconText19;
		[Field("button icons", null)]
		[Block("Hud Button Icon Block", 18, typeof(HudButtonIconBlock))]
		public TagBlock ButtonIcons20;
		[Field("default color", null)]
		public ColorArgb DefaultColor22;
		[Field("flashing color", null)]
		public ColorArgb FlashingColor23;
		[Field("flash period", null)]
		public float FlashPeriod24;
		[Field("flash delay#time between flashes", null)]
		public float FlashDelay25;
		[Field("number of flashes", null)]
		public short NumberOfFlashes26;
		[Field("flash flags", typeof(FlashFlags27Options))]
		public short FlashFlags27;
		[Field("flash length#time of each flash", null)]
		public float FlashLength28;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor29;
		[Field("", null)]
		public fixed byte _30[4];
		[Field("hud messages", null)]
		public TagReference HudMessages32;
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
		[Field("uptime ticks", null)]
		public short UptimeTicks42;
		[Field("fade ticks", null)]
		public short FadeTicks43;
		[Field("top offset", null)]
		public float TopOffset45;
		[Field("bottom offset", null)]
		public float BottomOffset46;
		[Field("left offset", null)]
		public float LeftOffset47;
		[Field("right offset", null)]
		public float RightOffset48;
		[Field("", null)]
		public fixed byte _49[32];
		[Field("arrow bitmap", null)]
		public TagReference ArrowBitmap50;
		[Field("waypoint arrows", null)]
		[Block("Hud Waypoint Arrow Block", 16, typeof(HudWaypointArrowBlock))]
		public TagBlock WaypointArrows51;
		[Field("", null)]
		public fixed byte _52[80];
		[Field("hud scale in multiplayer", null)]
		public float HudScaleInMultiplayer54;
		[Field("", null)]
		public fixed byte _55[256];
		[Field("", null)]
		public fixed byte _57[16];
		[Field("motion sensor range", null)]
		public float MotionSensorRange58;
		[Field("motion sensor velocity sensitivity#how fast something moves to show up on the motion sensor", null)]
		public float MotionSensorVelocitySensitivity59;
		[Field("motion sensor scale [DON'T TOUCH EVER]*", null)]
		public float MotionSensorScaleDONTTOUCHEVER60;
		[Field("default chapter title bounds", null)]
		public Rectangle2 DefaultChapterTitleBounds61;
		[Field("", null)]
		public fixed byte _62[44];
		[Field("top offset", null)]
		public short TopOffset64;
		[Field("bottom offset", null)]
		public short BottomOffset65;
		[Field("left offset", null)]
		public short LeftOffset66;
		[Field("right offset", null)]
		public short RightOffset67;
		[Field("", null)]
		public fixed byte _68[32];
		[Field("indicator bitmap", null)]
		public TagReference IndicatorBitmap69;
		[Field("sequence index", null)]
		public short SequenceIndex70;
		[Field("multiplayer sequence index", null)]
		public short MultiplayerSequenceIndex71;
		[Field("color", null)]
		public ColorArgb Color72;
		[Field("", null)]
		public fixed byte _73[16];
		[Field("default color", null)]
		public ColorArgb DefaultColor76;
		[Field("flashing color", null)]
		public ColorArgb FlashingColor77;
		[Field("flash period", null)]
		public float FlashPeriod78;
		[Field("flash delay#time between flashes", null)]
		public float FlashDelay79;
		[Field("number of flashes", null)]
		public short NumberOfFlashes80;
		[Field("flash flags", typeof(FlashFlags81Options))]
		public short FlashFlags81;
		[Field("flash length#time of each flash", null)]
		public float FlashLength82;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor83;
		[Field("", null)]
		public fixed byte _84[4];
		[Field("default color", null)]
		public ColorArgb DefaultColor86;
		[Field("flashing color", null)]
		public ColorArgb FlashingColor87;
		[Field("flash period", null)]
		public float FlashPeriod88;
		[Field("flash delay#time between flashes", null)]
		public float FlashDelay89;
		[Field("number of flashes", null)]
		public short NumberOfFlashes90;
		[Field("flash flags", typeof(FlashFlags91Options))]
		public short FlashFlags91;
		[Field("flash length#time of each flash", null)]
		public float FlashLength92;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor93;
		[Field("", null)]
		public fixed byte _94[4];
		[Field("", null)]
		public fixed byte _95[40];
		[Field("carnage report bitmap", null)]
		public TagReference CarnageReportBitmap96;
		[Field("loading begin text", null)]
		public short LoadingBeginText98;
		[Field("loading end text", null)]
		public short LoadingEndText99;
		[Field("checkpoint begin text", null)]
		public short CheckpointBeginText100;
		[Field("checkpoint end text", null)]
		public short CheckpointEndText101;
		[Field("checkpoint sound", null)]
		public TagReference CheckpointSound102;
		[Field("", null)]
		public fixed byte _103[96];
		[Field("new globals", typeof(GlobalNewHudGlobalsStructBlock))]
		[Block("Global New Hud Globals Struct", 1, typeof(GlobalNewHudGlobalsStructBlock))]
		public GlobalNewHudGlobalsStructBlock NewGlobals104;
	}
}
#pragma warning restore CS1591
