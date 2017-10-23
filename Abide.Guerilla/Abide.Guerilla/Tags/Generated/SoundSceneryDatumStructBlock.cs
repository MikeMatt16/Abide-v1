using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct SoundSceneryDatumStructBlock
	{
		public enum VolumeType0Options
		{
			Sphere_0 = 0,
			VerticalCylinder_1 = 1,
		}
		[Field("Volume Type", typeof(VolumeType0Options))]
		public int VolumeType0;
		[Field("Height", null)]
		public float Height1;
		[Field("Override Distance Bounds", null)]
		public FloatBounds OverrideDistanceBounds2;
		[Field("Override Cone Angle Bounds", null)]
		public FloatBounds OverrideConeAngleBounds3;
		[Field("Override Outer Cone Gain:dB", null)]
		public float OverrideOuterConeGain4;
	}
}
#pragma warning restore CS1591
