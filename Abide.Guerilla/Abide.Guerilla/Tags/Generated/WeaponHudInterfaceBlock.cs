using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("weapon_hud_interface", "wphi", "����", typeof(WeaponHudInterfaceBlock))]
	[FieldSet(380, 4)]
	public unsafe struct WeaponHudInterfaceBlock
	{
		public enum Flags2Options
		{
			UseParentHudFlashingParameters_0 = 1,
		}
		public enum Anchor10Options
		{
			TopLeft_0 = 0,
			TopRight_1 = 1,
			BottomLeft_2 = 2,
			BottomRight_3 = 3,
			Center_4 = 4,
			Crosshair_5 = 5,
		}
		public enum Flags29Options
		{
			UseTextFromStringListInstead_0 = 1,
			OverrideDefaultColor_1 = 2,
			WidthOffsetIsAbsoluteIconWidth_2 = 4,
		}
		[Field("child hud", null)]
		public TagReference ChildHud0;
		[Field("flags", typeof(Flags2Options))]
		public short Flags2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("inventory ammo cutoff", null)]
		public short InventoryAmmoCutoff4;
		[Field("loaded ammo cutoff", null)]
		public short LoadedAmmoCutoff5;
		[Field("heat cutoff", null)]
		public short HeatCutoff6;
		[Field("age cutoff", null)]
		public short AgeCutoff7;
		[Field("", null)]
		public fixed byte _8[32];
		[Field("anchor", typeof(Anchor10Options))]
		public short Anchor10;
		[Field("", null)]
		public fixed byte _11[2];
		[Field("", null)]
		public fixed byte _12[32];
		[Field("static elements", null)]
		[Block("Weapon Hud Static Block", 16, typeof(WeaponHudStaticBlock))]
		public TagBlock StaticElements13;
		[Field("meter elements", null)]
		[Block("Weapon Hud Meter Block", 16, typeof(WeaponHudMeterBlock))]
		public TagBlock MeterElements14;
		[Field("number elements", null)]
		[Block("Weapon Hud Number Block", 16, typeof(WeaponHudNumberBlock))]
		public TagBlock NumberElements15;
		[Field("crosshairs", null)]
		[Block("Weapon Hud Crosshair Block", 19, typeof(WeaponHudCrosshairBlock))]
		public TagBlock Crosshairs17;
		[Field("overlay elements", null)]
		[Block("Weapon Hud Overlays Block", 16, typeof(WeaponHudOverlaysBlock))]
		public TagBlock OverlayElements18;
		[Field("", null)]
		public fixed byte _19[4];
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _20;
		[Field("screen effect", null)]
		[Block("Global Hud Screen Effect Definition", 1, typeof(GlobalHudScreenEffectDefinition))]
		public TagBlock ScreenEffect21;
		[Field("", null)]
		public fixed byte _22[132];
		[Field("sequence index#sequence index into the global hud icon bitmap", null)]
		public short SequenceIndex24;
		[Field("width offset#extra spacing beyond bitmap width for text alignment", null)]
		public short WidthOffset25;
		[Field("offset from reference corner", null)]
		public Vector2 OffsetFromReferenceCorner26;
		[Field("override icon color", null)]
		public ColorArgb OverrideIconColor27;
		[Field("frame rate [0,30]", null)]
		public int FrameRate03028;
		[Field("flags", typeof(Flags29Options))]
		public byte Flags29;
		[Field("text index", null)]
		public short TextIndex30;
		[Field("", null)]
		public fixed byte _31[48];
	}
}
#pragma warning restore CS1591
