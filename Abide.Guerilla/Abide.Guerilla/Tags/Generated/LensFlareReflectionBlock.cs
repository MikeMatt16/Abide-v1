using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(48, 4)]
	public unsafe struct LensFlareReflectionBlock
	{
		public enum Flags0Options
		{
			AlignRotationWithScreenCenter_0 = 1,
			RadiusNOTScaledByDistance_1 = 2,
			RadiusScaledByOcclusionFactor_2 = 4,
			OccludedBySolidObjects_3 = 8,
			IgnoreLightColor_4 = 16,
			NotAffectedByInnerOcclusion_5 = 32,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("bitmap index", null)]
		public short BitmapIndex2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("", null)]
		public fixed byte _4[20];
		[Field("position:along flare axis#0 is on top of light, 1 is opposite light, 0.5 is the center of the screen, etc.", null)]
		public float Position5;
		[Field("rotation offset:degrees", null)]
		public float RotationOffset6;
		[Field("", null)]
		public fixed byte _7[4];
		[Field("radius:world units#interpolated by external input", null)]
		public FloatBounds Radius8;
		[Field("", null)]
		public fixed byte _9[4];
		[Field("brightness:[0,1]#interpolated by external input", null)]
		public FloatBounds Brightness10;
		[Field("", null)]
		public fixed byte _11[4];
		[Field("modulation factor:[0,1]", null)]
		public float ModulationFactor13;
		[Field("color", null)]
		public ColorRgbF Color14;
		[Field("", null)]
		public fixed byte _15[48];
	}
}
#pragma warning restore CS1591
