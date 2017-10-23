using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(180, 4)]
	public unsafe struct WeaponHudStaticBlock
	{
		public enum StateAttachedTo0Options
		{
			InventoryAmmo_0 = 0,
			LoadedAmmo_1 = 1,
			Heat_2 = 2,
			Age_3 = 3,
			SecondaryWeaponInventoryAmmo_4 = 4,
			SecondaryWeaponLoadedAmmo_5 = 5,
			DistanceToTarget_6 = 6,
			ElevationToTarget_7 = 7,
		}
		public enum CanUseOnMapType2Options
		{
			Any_0 = 0,
			Solo_1 = 1,
			Multiplayer_2 = 2,
		}
		public enum ScalingFlags8Options
		{
			DonTScaleOffset_0 = 1,
			DonTScaleSize_1 = 2,
		}
		public enum FlashFlags17Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		[Field("state attached to", typeof(StateAttachedTo0Options))]
		public short StateAttachedTo0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("can use on map type", typeof(CanUseOnMapType2Options))]
		public short CanUseOnMapType2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("", null)]
		public fixed byte _4[28];
		[Field("anchor offset", null)]
		public Vector2 AnchorOffset5;
		[Field("width scale", null)]
		public float WidthScale6;
		[Field("height scale", null)]
		public float HeightScale7;
		[Field("scaling flags", typeof(ScalingFlags8Options))]
		public short ScalingFlags8;
		[Field("", null)]
		public fixed byte _9[2];
		[Field("", null)]
		public fixed byte _10[20];
		[Field("interface bitmap", null)]
		public TagReference InterfaceBitmap11;
		[Field("default color", null)]
		public ColorArgb DefaultColor12;
		[Field("flashing color", null)]
		public ColorArgb FlashingColor13;
		[Field("flash period", null)]
		public float FlashPeriod14;
		[Field("flash delay#time between flashes", null)]
		public float FlashDelay15;
		[Field("number of flashes", null)]
		public short NumberOfFlashes16;
		[Field("flash flags", typeof(FlashFlags17Options))]
		public short FlashFlags17;
		[Field("flash length#time of each flash", null)]
		public float FlashLength18;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor19;
		[Field("", null)]
		public fixed byte _20[4];
		[Field("sequence index", null)]
		public short SequenceIndex21;
		[Field("", null)]
		public fixed byte _22[2];
		[Field("multitex overlay", null)]
		[Block("Global Hud Multitexture Overlay Definition", 30, typeof(GlobalHudMultitextureOverlayDefinition))]
		public TagBlock MultitexOverlay23;
		[Field("", null)]
		public fixed byte _24[4];
		[Field("", null)]
		public fixed byte _25[40];
	}
}
#pragma warning restore CS1591
