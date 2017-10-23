using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(132, 4)]
	public unsafe struct UnitHudAuxilaryOverlayBlock
	{
		public enum ScalingFlags3Options
		{
			DonTScaleOffset_0 = 1,
			DonTScaleSize_1 = 2,
		}
		public enum FlashFlags12Options
		{
			ReverseDefaultFlashingColors_0 = 1,
		}
		public enum Type20Options
		{
			TeamIcon_0 = 0,
		}
		public enum Flags21Options
		{
			UseTeamColor_0 = 1,
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
		[Field("interface bitmap", null)]
		public TagReference InterfaceBitmap6;
		[Field("default color", null)]
		public ColorArgb DefaultColor7;
		[Field("flashing color", null)]
		public ColorArgb FlashingColor8;
		[Field("flash period", null)]
		public float FlashPeriod9;
		[Field("flash delay#time between flashes", null)]
		public float FlashDelay10;
		[Field("number of flashes", null)]
		public short NumberOfFlashes11;
		[Field("flash flags", typeof(FlashFlags12Options))]
		public short FlashFlags12;
		[Field("flash length#time of each flash", null)]
		public float FlashLength13;
		[Field("disabled color", null)]
		public ColorArgb DisabledColor14;
		[Field("", null)]
		public fixed byte _15[4];
		[Field("sequence index", null)]
		public short SequenceIndex16;
		[Field("", null)]
		public fixed byte _17[2];
		[Field("multitex overlay", null)]
		[Block("Global Hud Multitexture Overlay Definition", 30, typeof(GlobalHudMultitextureOverlayDefinition))]
		public TagBlock MultitexOverlay18;
		[Field("", null)]
		public fixed byte _19[4];
		[Field("type", typeof(Type20Options))]
		public short Type20;
		[Field("flags", typeof(Flags21Options))]
		public short Flags21;
		[Field("", null)]
		public fixed byte _22[24];
	}
}
#pragma warning restore CS1591
