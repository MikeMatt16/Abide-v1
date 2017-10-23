using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ShaderStateAlphaTestStateBlock
	{
		public enum Flags0Options
		{
			AlphaTestEnabled_0 = 1,
			SampleAlphaToCoverage_1 = 2,
			SampleAlphaToOne_2 = 4,
		}
		public enum AlphaCompareFunction1Options
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
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("alpha compare function", typeof(AlphaCompareFunction1Options))]
		public short AlphaCompareFunction1;
		[Field("alpha-test ref:[0,255]", null)]
		public short AlphaTestRef2;
		[Field("", null)]
		public fixed byte _3[2];
	}
}
#pragma warning restore CS1591
