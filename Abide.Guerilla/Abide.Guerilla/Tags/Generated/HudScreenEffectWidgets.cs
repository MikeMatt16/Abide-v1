using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(112, 4)]
	public unsafe struct HudScreenEffectWidgets
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
		public enum Flags4Options
		{
			Unused_0 = 1,
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
		[Field("flags", typeof(Flags4Options))]
		public short Flags4;
		[Field("bitmap", null)]
		public TagReference Bitmap5;
		[Field("fullscreen screen effect", null)]
		public TagReference FullscreenScreenEffect6;
		[Field("waa", typeof(ScreenEffectBonusStructBlock))]
		[Block("Screen Effect Bonus Struct", 1, typeof(ScreenEffectBonusStructBlock))]
		public ScreenEffectBonusStructBlock Waa7;
		[Field("fullscreen sequence index", null)]
		public int FullscreenSequenceIndex8;
		[Field("halfscreen sequence index", null)]
		public int HalfscreenSequenceIndex9;
		[Field("quarterscreen sequence index", null)]
		public int QuarterscreenSequenceIndex10;
		[Field("", null)]
		public fixed byte _11[1];
		[Field("fullscreen offset", null)]
		public Vector2 FullscreenOffset12;
		[Field("halfscreen offset", null)]
		public Vector2 HalfscreenOffset13;
		[Field("quarterscreen offset", null)]
		public Vector2 QuarterscreenOffset14;
	}
}
#pragma warning restore CS1591
