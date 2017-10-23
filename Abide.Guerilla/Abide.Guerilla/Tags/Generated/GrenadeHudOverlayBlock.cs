using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(136, 4)]
	public unsafe struct GrenadeHudOverlayBlock
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
		public enum Type17Options
		{
			ShowOnFlashing_0 = 1,
			ShowOnEmpty_1 = 2,
			ShowOnDefault_2 = 4,
			ShowAlways_3 = 8,
		}
		public enum Flags18Options
		{
			FlashesWhenActive_0 = 1,
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
		public float FrameRate15;
		[Field("sequence index", null)]
		public short SequenceIndex16;
		[Field("type", typeof(Type17Options))]
		public short Type17;
		[Field("flags", typeof(Flags18Options))]
		public int Flags18;
		[Field("", null)]
		public fixed byte _19[16];
		[Field("", null)]
		public fixed byte _20[40];
	}
}
#pragma warning restore CS1591
