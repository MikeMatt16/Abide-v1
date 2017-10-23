using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(120, 4)]
	public unsafe struct HudBitmapWidgets
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
			FlipHorizontally_0 = 1,
			FlipVertically_1 = 2,
			ScopeMirrorHorizontally_2 = 4,
			ScopeMirrorVertically_3 = 8,
			ScopeStretch_4 = 16,
		}
		public enum SpecialHudType18Options
		{
			Unspecial_0 = 0,
			SBPlayerEmblem_1 = 1,
			SBOtherPlayerEmblem_2 = 2,
			SBPlayerScoreMeter_3 = 3,
			SBOtherPlayerScoreMeter_4 = 4,
			UnitShieldMeter_5 = 5,
			MotionSensor_6 = 6,
			TerritoryMeter_7 = 7,
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
		[Field("shader", null)]
		public TagReference Shader6;
		[Field("fullscreen sequence index", null)]
		public int FullscreenSequenceIndex7;
		[Field("halfscreen sequence index", null)]
		public int HalfscreenSequenceIndex8;
		[Field("quarterscreen sequence index", null)]
		public int QuarterscreenSequenceIndex9;
		[Field("", null)]
		public fixed byte _10[1];
		[Field("fullscreen offset", null)]
		public Vector2 FullscreenOffset11;
		[Field("halfscreen offset", null)]
		public Vector2 HalfscreenOffset12;
		[Field("quarterscreen offset", null)]
		public Vector2 QuarterscreenOffset13;
		[Field("fullscreen registration point", null)]
		public Vector2 FullscreenRegistrationPoint14;
		[Field("halfscreen registration point", null)]
		public Vector2 HalfscreenRegistrationPoint15;
		[Field("quarterscreen registration point", null)]
		public Vector2 QuarterscreenRegistrationPoint16;
		[Field("effect", null)]
		[Block("Hud Widget Effect Block", 1, typeof(HudWidgetEffectBlock))]
		public TagBlock Effect17;
		[Field("special hud type", typeof(SpecialHudType18Options))]
		public short SpecialHudType18;
		[Field("", null)]
		public fixed byte _19[2];
	}
}
#pragma warning restore CS1591
