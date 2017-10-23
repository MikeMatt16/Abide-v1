using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(96, 4)]
	public unsafe struct HudTextWidgets
	{
		public enum Anchor3Options
		{
			HealthAndShield_0 = 0,
			WeaponHud_1 = 1,
			MotionSensor_2 = 2,
			Scoreboard_3 = 3,
			Crosshair_4 = 4,
			LockOnTarget_5 = 5,
		}
		public enum Flags5Options
		{
			StringIsANumber_0 = 1,
			Force2DigitNumber_1 = 2,
			Force3DigitNumber_2 = 4,
			TalkingPlayerHack_3 = 8,
		}
		public enum Justification8Options
		{
			Left_0 = 0,
			Center_1 = 1,
			Right_2 = 2,
		}
		public enum FullscreenFontIndex11Options
		{
			Defualt_0 = 0,
			NumberFont_1 = 1,
		}
		public enum HalfscreenFontIndex12Options
		{
			Defualt_0 = 0,
			NumberFont_1 = 1,
		}
		public enum QuarterscreenFontIndex13Options
		{
			Defualt_0 = 0,
			NumberFont_1 = 1,
		}
		[Field("name", null)]
		public StringId Name0;
		[Field("", typeof(HudWidgetInputsStructBlock))]
		[Block("Hud Widget Inputs Struct", 1, typeof(HudWidgetInputsStructBlock))]
		public HudWidgetInputsStructBlock _1;
		[Field("", typeof(HudWidgetStateDefinitionStructBlock))]
		[Block("Hud Widget State Definition Struct", 1, typeof(HudWidgetStateDefinitionStructBlock))]
		public HudWidgetStateDefinitionStructBlock _2;
		[Field("anchor", typeof(Anchor3Options))]
		public short Anchor3;
		[Field("flags", typeof(Flags5Options))]
		public short Flags5;
		[Field("shader", null)]
		public TagReference Shader6;
		[Field("string", null)]
		public StringId String7;
		[Field("justification", typeof(Justification8Options))]
		public short Justification8;
		[Field("", null)]
		public fixed byte _9[2];
		[Field("", null)]
		public fixed byte _10[12];
		[Field("fullscreen font index", typeof(FullscreenFontIndex11Options))]
		public byte FullscreenFontIndex11;
		[Field("halfscreen font index", typeof(HalfscreenFontIndex12Options))]
		public byte HalfscreenFontIndex12;
		[Field("quarterscreen font index", typeof(QuarterscreenFontIndex13Options))]
		public byte QuarterscreenFontIndex13;
		[Field("", null)]
		public fixed byte _14[1];
		[Field("fullscreen scale", null)]
		public float FullscreenScale15;
		[Field("halfscreen scale", null)]
		public float HalfscreenScale16;
		[Field("quarterscreen scale", null)]
		public float QuarterscreenScale17;
		[Field("fullscreen offset", null)]
		public Vector2 FullscreenOffset18;
		[Field("halfscreen offset", null)]
		public Vector2 HalfscreenOffset19;
		[Field("quarterscreen offset", null)]
		public Vector2 QuarterscreenOffset20;
		[Field("effect", null)]
		[Block("Hud Widget Effect Block", 1, typeof(HudWidgetEffectBlock))]
		public TagBlock Effect21;
	}
}
#pragma warning restore CS1591
