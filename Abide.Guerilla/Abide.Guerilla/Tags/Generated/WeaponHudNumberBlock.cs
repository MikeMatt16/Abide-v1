using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(160, 4)]
	public unsafe struct WeaponHudNumberBlock
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
		public enum FlashFlags16Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		public enum Flags21Options
		{
			ShowLeadingZeros_0 = 1,
			OnlyShowWhenZoomed_1 = 2,
			DrawATrailingM_2 = 4,
		}
		public enum WeaponSpecificFlags25Options
		{
			DivideNumberByClipSize_0 = 1,
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
		[Field("default color", null)]
		public ColorArgb DefaultColor11;
		[Field("flashing color", null)]
		public ColorArgb FlashingColor12;
		[Field("flash period", null)]
		public float FlashPeriod13;
		[Field("flash delay#time between flashes", null)]
		public float FlashDelay14;
		[Field("number of flashes", null)]
		public short NumberOfFlashes15;
		[Field("flash flags", typeof(FlashFlags16Options))]
		public short FlashFlags16;
		[Field("flash length#time of each flash", null)]
		public float FlashLength17;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor18;
		[Field("", null)]
		public fixed byte _19[4];
		[Field("maximum number of digits", null)]
		public int MaximumNumberOfDigits20;
		[Field("flags", typeof(Flags21Options))]
		public byte Flags21;
		[Field("number of fractional digits", null)]
		public int NumberOfFractionalDigits22;
		[Field("", null)]
		public fixed byte _23[1];
		[Field("", null)]
		public fixed byte _24[12];
		[Field("weapon specific flags", typeof(WeaponSpecificFlags25Options))]
		public short WeaponSpecificFlags25;
		[Field("", null)]
		public fixed byte _26[2];
		[Field("", null)]
		public fixed byte _27[36];
	}
}
#pragma warning restore CS1591
