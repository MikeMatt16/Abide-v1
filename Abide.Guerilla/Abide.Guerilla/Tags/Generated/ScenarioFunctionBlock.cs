using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(120, 4)]
	public unsafe struct ScenarioFunctionBlock
	{
		public enum Flags0Options
		{
			ScriptedLevelScriptWillSetThisValueOtherSettingsHereWillBeIgnored_0 = 1,
			InvertResultOfFunctionIs1MinusActualResult_1 = 2,
			Additive_2 = 4,
			AlwaysActiveFunctionDoesNotDeactivateWhenAtOrBelowLowerBound_3 = 8,
		}
		public enum Function4Options
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
		public enum WobbleFunctionCurveUsedForWobble6Options
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
		public enum MapTo11Options
		{
			Linear_0 = 0,
			Early_1 = 1,
			VeryEarly_2 = 2,
			Late_3 = 3,
			VeryLate_4 = 4,
			Cosine_5 = 5,
			One_6 = 6,
			Zero_7 = 7,
		}
		public enum BoundsModeControlsHowBoundsBelowAreUsed15Options
		{
			Clip_0 = 0,
			ClipAndNormalize_1 = 1,
			ScaleToFit_2 = 2,
		}
		[Field("Flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("Name^", null)]
		public String Name1;
		[Field("Period:Seconds#Period for above function (lower values make function oscillate quickly; higher values make it oscillate slowly).", null)]
		public float Period2;
		[Field("Scale Period By#Multiply this function by above period", null)]
		public short ScalePeriodBy3;
		[Field("Function", typeof(Function4Options))]
		public short Function4;
		[Field("Scale Function By#Multiply this function by result of above function.", null)]
		public short ScaleFunctionBy5;
		[Field("Wobble Function#Curve used for wobble.", typeof(WobbleFunctionCurveUsedForWobble6Options))]
		public short WobbleFunction6;
		[Field("Wobble Period:Seconds#Time it takes for magnitude of this function to complete a wobble.", null)]
		public float WobblePeriod7;
		[Field("Wobble Magnitude:Percent#Amount of random wobble in the magnitude.", null)]
		public float WobbleMagnitude8;
		[Field("Square Wave Threshold#If non-zero, all values above square wave threshold are snapped to 1.0, and all values below it are snapped to 0.0 to create a square wave.", null)]
		public float SquareWaveThreshold9;
		[Field("Step Count#Number of discrete values to snap to (e.g., step count of 5 snaps function to 0.00, 0.25, 0.50,0.75, or 1.00).", null)]
		public short StepCount10;
		[Field("Map to", typeof(MapTo11Options))]
		public short MapTo11;
		[Field("Sawtooth Count#Number of times this function should repeat (e.g., sawtooth count of 5 gives function value of 1.0 at each of 0.25, 0.50, and 0.75, as well as at 1.0).", null)]
		public short SawtoothCount12;
		[Field("", null)]
		public fixed byte _13[2];
		[Field("Scale Result by#Multiply this function (e.g., from a weapon, vehicle) final result of all of the above math.", null)]
		public short ScaleResultBy14;
		[Field("Bounds Mode#Controls how bounds, below, are used.", typeof(BoundsModeControlsHowBoundsBelowAreUsed15Options))]
		public short BoundsMode15;
		[Field("Bounds", null)]
		public FloatBounds Bounds16;
		[Field("", null)]
		public fixed byte _17[4];
		[Field("", null)]
		public fixed byte _18[2];
		[Field("Turn Off with#If specified function is off, so is this function.", null)]
		public short TurnOffWith19;
		[Field("", null)]
		public fixed byte _20[16];
		[Field("", null)]
		public fixed byte _21[16];
	}
}
#pragma warning restore CS1591
