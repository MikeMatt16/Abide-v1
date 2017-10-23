using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(104, 4)]
	public unsafe struct WeaponHudCrosshairBlock
	{
		public enum CrosshairType0Options
		{
			Aim_0 = 0,
			Zoom_1 = 1,
			Charge_2 = 2,
			ShouldReload_3 = 3,
			FlashHeat_4 = 4,
			FlashInventoryAmmo_5 = 5,
			FlashBattery_6 = 6,
			ReloadOverheat_7 = 7,
			FlashWhenFiringAndNoAmmo_8 = 8,
			FlashWhenThrowingAndNoGrenade_9 = 9,
			LowAmmoAndNoneLeftToReload_10 = 10,
			ShouldReloadSecondaryTrigger_11 = 11,
			FlashSecondaryInventoryAmmo_12 = 12,
			FlashSecondaryReload_13 = 13,
			FlashWhenFiringSecondaryTriggerWithNoAmmo_14 = 14,
			LowSecondaryAmmoAndNoneLeftToReload_15 = 15,
			PrimaryTriggerReady_16 = 16,
			SecondaryTriggerReady_17 = 17,
			FlashWhenFiringWithDepletedBattery_18 = 18,
		}
		public enum CanUseOnMapType2Options
		{
			Any_0 = 0,
			Solo_1 = 1,
			Multiplayer_2 = 2,
		}
		[Field("crosshair type", typeof(CrosshairType0Options))]
		public short CrosshairType0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("can use on map type", typeof(CanUseOnMapType2Options))]
		public short CanUseOnMapType2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("", null)]
		public fixed byte _4[28];
		[Field("Crosshair bitmap", null)]
		public TagReference CrosshairBitmap5;
		[Field("Crosshair overlays", null)]
		[Block("Weapon Hud Crosshair Item Block", 16, typeof(WeaponHudCrosshairItemBlock))]
		public TagBlock CrosshairOverlays6;
		[Field("", null)]
		public fixed byte _7[40];
	}
}
#pragma warning restore CS1591
