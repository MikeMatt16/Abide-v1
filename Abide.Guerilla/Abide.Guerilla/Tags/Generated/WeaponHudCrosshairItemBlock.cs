using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(108, 4)]
	public unsafe struct WeaponHudCrosshairItemBlock
	{
		public enum ScalingFlags3Options
		{
			DonTScaleOffset_0 = 1,
			DonTScaleSize_1 = 2,
		}
		public enum FlashFlags11Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		public enum Flags17Options
		{
			FlashesWhenActive_0 = 1,
			NotASprite_1 = 2,
			ShowOnlyWhenZoomed_2 = 4,
			ShowSniperData_3 = 8,
			HideAreaOutsideReticle_4 = 16,
			OneZoomLevel_5 = 32,
			DonTShowWhenZoomed_6 = 64,
		}
		[Field("anchor offset", null)]
		public Vector2 AnchorOffset0;
		[Field("width scale", null)]
		public float WidthScale1;
		[Field("height scale", null)]
		public float HeightScale2;
		[Field("scaling flags", typeof(ScalingFlags3Options))]
		public short ScalingFlags3;
		[Field("", null)]
		public fixed byte _4[2];
		[Field("", null)]
		public fixed byte _5[20];
		[Field("default color", null)]
		public ColorArgb DefaultColor6;
		[Field("flashing color", null)]
		public ColorArgb FlashingColor7;
		[Field("flash period", null)]
		public float FlashPeriod8;
		[Field("flash delay#time between flashes", null)]
		public float FlashDelay9;
		[Field("number of flashes", null)]
		public short NumberOfFlashes10;
		[Field("flash flags", typeof(FlashFlags11Options))]
		public short FlashFlags11;
		[Field("flash length#time of each flash", null)]
		public float FlashLength12;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor13;
		[Field("", null)]
		public fixed byte _14[4];
		[Field("frame rate", null)]
		public short FrameRate15;
		[Field("sequence index", null)]
		public short SequenceIndex16;
		[Field("flags", typeof(Flags17Options))]
		public int Flags17;
		[Field("", null)]
		public fixed byte _18[32];
	}
}
#pragma warning restore CS1591
