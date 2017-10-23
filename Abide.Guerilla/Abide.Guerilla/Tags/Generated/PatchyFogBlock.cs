using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("patchy_fog", "fpch", "����", typeof(PatchyFogBlock))]
	[FieldSet(88, 4)]
	public unsafe struct PatchyFogBlock
	{
		public enum Flags1Options
		{
			SeparateLayerDepths_0 = 1,
			SortBehindTransparents_1 = 2,
		}
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("", null)]
		public fixed byte _3[60];
		[Field("rotation multiplier:[0,1]", null)]
		public float RotationMultiplier5;
		[Field("strafing multiplier:[0,1]", null)]
		public float StrafingMultiplier6;
		[Field("zoom multiplier:[0,1]", null)]
		public float ZoomMultiplier7;
		[Field("", null)]
		public fixed byte _8[32];
		[Field("noise map scale#0 defaults to 1", null)]
		public float NoiseMapScale10;
		[Field("noise map", null)]
		public TagReference NoiseMap11;
		[Field("noise vertical scale forward#0 defaults to 1", null)]
		public float NoiseVerticalScaleForward12;
		[Field("noise vertical scale up#0 defaults to 1", null)]
		public float NoiseVerticalScaleUp13;
		[Field("noise opacity scale up#0 defaults to 1", null)]
		public float NoiseOpacityScaleUp14;
		[Field("", null)]
		public fixed byte _15[20];
		[Field("animation period:seconds", null)]
		public float AnimationPeriod17;
		[Field("", null)]
		public fixed byte _18[4];
		[Field("wind velocity:world units per second", null)]
		public FloatBounds WindVelocity19;
		[Field("wind period:seconds#0 defaults to 1", null)]
		public FloatBounds WindPeriod20;
		[Field("wind acceleration weight:[0,1]", null)]
		public float WindAccelerationWeight21;
		[Field("wind perpendicular weight:[0,1]", null)]
		public float WindPerpendicularWeight22;
		[Field("wind constant velocity x:world units per second", null)]
		public float WindConstantVelocityX23;
		[Field("wind constant velocity y:world units per second", null)]
		public float WindConstantVelocityY24;
		[Field("wind constant velocity z:world units per second", null)]
		public float WindConstantVelocityZ25;
		[Field("", null)]
		public fixed byte _26[20];
	}
}
#pragma warning restore CS1591
