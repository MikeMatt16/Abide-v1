using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(104, 4)]
	public unsafe struct WeaponHudOverlaysBlock
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
		[Field("Overlay bitmap", null)]
		public TagReference OverlayBitmap5;
		[Field("Overlays", null)]
		[Block("Weapon Hud Overlay Block", 16, typeof(WeaponHudOverlayBlock))]
		public TagBlock Overlays6;
		[Field("", null)]
		public fixed byte _7[40];
	}
}
#pragma warning restore CS1591
