using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(180, 4)]
	public unsafe struct WeaponHudMeterBlock
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
		public enum Flags16Options
		{
			UseMinMaxForStateChanges_0 = 1,
			InterpolateBetweenMinMaxFlashColorsAsStateChanges_1 = 2,
			InterpolateColorAlongHsvSpace_2 = 4,
			MoreColorsForHsvInterpolation_3 = 8,
			InvertInterpolation_4 = 16,
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
		[Field("meter bitmap", null)]
		public TagReference MeterBitmap11;
		[Field("color at meter minimum", null)]
		public ColorRgb ColorAtMeterMinimum12;
		[Field("color at meter maximum", null)]
		public ColorRgb ColorAtMeterMaximum13;
		[Field("flash color", null)]
		public ColorRgb FlashColor14;
		[Field("empty color", null)]
		public ColorArgb EmptyColor15;
		[Field("flags", typeof(Flags16Options))]
		public byte Flags16;
		[Field("minumum meter value", null)]
		public int MinumumMeterValue17;
		[Field("sequence index", null)]
		public short SequenceIndex18;
		[Field("alpha multiplier", null)]
		public int AlphaMultiplier19;
		[Field("alpha bias", null)]
		public int AlphaBias20;
		[Field("value scale#used for non-integral values, i.e. health and shields", null)]
		public short ValueScale21;
		[Field("opacity", null)]
		public float Opacity22;
		[Field("translucency", null)]
		public float Translucency23;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor24;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _25;
		[Field("", null)]
		public fixed byte _26[4];
		[Field("", null)]
		public fixed byte _27[40];
	}
}
#pragma warning restore CS1591
