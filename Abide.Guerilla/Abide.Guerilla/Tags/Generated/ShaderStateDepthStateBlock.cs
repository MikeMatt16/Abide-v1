using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct ShaderStateDepthStateBlock
	{
		public enum Mode0Options
		{
			UseZ_0 = 0,
			UseW_1 = 1,
		}
		public enum DepthCompareFunction1Options
		{
			Never_0 = 0,
			Less_1 = 1,
			Equal_2 = 2,
			LessOrEqual_3 = 3,
			Greater_4 = 4,
			NotEqual_5 = 5,
			GreaterOrEqual_6 = 6,
			Always_7 = 7,
		}
		public enum Flags2Options
		{
			DepthWrite_0 = 1,
			OffsetPoints_1 = 2,
			OffsetLines_2 = 4,
			OffsetTriangles_3 = 8,
			ClipControlDonTCullPrimitive_4 = 16,
			ClipControlClamp_5 = 32,
			ClipControlIgnoreWSign_6 = 64,
		}
		[Field("mode", typeof(Mode0Options))]
		public short Mode0;
		[Field("depth compare function", typeof(DepthCompareFunction1Options))]
		public short DepthCompareFunction1;
		[Field("flags", typeof(Flags2Options))]
		public short Flags2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("depth bias slope scale", null)]
		public float DepthBiasSlopeScale4;
		[Field("depth bias", null)]
		public float DepthBias5;
	}
}
#pragma warning restore CS1591
