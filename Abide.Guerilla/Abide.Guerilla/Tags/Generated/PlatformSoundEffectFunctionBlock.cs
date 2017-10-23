using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct PlatformSoundEffectFunctionBlock
	{
		public enum Input0Options
		{
			Zero_0 = 0,
			Time_1 = 1,
			Scale_2 = 2,
			Rolloff_3 = 3,
		}
		public enum Range1Options
		{
			Zero_0 = 0,
			Time_1 = 1,
			Scale_2 = 2,
			Rolloff_3 = 3,
		}
		[Field("input", typeof(Input0Options))]
		public short Input0;
		[Field("range", typeof(Range1Options))]
		public short Range1;
		[Field("function", typeof(MappingFunctionBlock))]
		[Block("Mapping Function", 1, typeof(MappingFunctionBlock))]
		public MappingFunctionBlock Function2;
		[Field("time period: seconds", null)]
		public float TimePeriod3;
	}
}
#pragma warning restore CS1591
