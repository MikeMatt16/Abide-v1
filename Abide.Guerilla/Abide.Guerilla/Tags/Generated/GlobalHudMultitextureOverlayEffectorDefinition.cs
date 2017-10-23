using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(220, 4)]
	public unsafe struct GlobalHudMultitextureOverlayEffectorDefinition
	{
		public enum DestinationType2Options
		{
			Tint01_0 = 0,
			HorizontalOffset_1 = 1,
			VerticalOffset_2 = 2,
			Fade01_3 = 3,
		}
		public enum Destination3Options
		{
			GeometryOffset_0 = 0,
			PrimaryMap_1 = 1,
			SecondaryMap_2 = 2,
			TertiaryMap_3 = 3,
		}
		public enum Source4Options
		{
			PlayerPitch_0 = 0,
			PlayerPitchTangent_1 = 1,
			PlayerYaw_2 = 2,
			WeaponRoundsLoaded_3 = 3,
			WeaponRoundsInventory_4 = 4,
			WeaponHeat_5 = 5,
			ExplicitUsesLowBound_6 = 6,
			WeaponZoomLevel_7 = 7,
		}
		public enum PeriodicFunction14Options
		{
			One_0 = 0,
			Zero_1 = 1,
			Cosine_2 = 2,
			CosineVariablePeriod_3 = 3,
			DiagonalWave_4 = 4,
			DiagonalWaveVariablePeriod_5 = 5,
			Slide_6 = 6,
			SlideVariablePeriod_7 = 7,
			Noise_8 = 8,
			Jitter_9 = 9,
			Wander_10 = 10,
			Spark_11 = 11,
		}
		[Field("", null)]
		public fixed byte _0[64];
		[Field("destination type", typeof(DestinationType2Options))]
		public short DestinationType2;
		[Field("destination", typeof(Destination3Options))]
		public short Destination3;
		[Field("source", typeof(Source4Options))]
		public short Source4;
		[Field("", null)]
		public fixed byte _5[2];
		[Field("in bounds:source units", null)]
		public FloatBounds InBounds7;
		[Field("out bounds:pixels", null)]
		public FloatBounds OutBounds8;
		[Field("", null)]
		public fixed byte _9[64];
		[Field("tint color lower bound", null)]
		public ColorRgbF TintColorLowerBound11;
		[Field("tint color upper bound", null)]
		public ColorRgbF TintColorUpperBound12;
		[Field("periodic function", typeof(PeriodicFunction14Options))]
		public short PeriodicFunction14;
		[Field("", null)]
		public fixed byte _15[2];
		[Field("function period:seconds", null)]
		public float FunctionPeriod16;
		[Field("function phase:seconds", null)]
		public float FunctionPhase17;
		[Field("", null)]
		public fixed byte _18[32];
	}
}
#pragma warning restore CS1591
