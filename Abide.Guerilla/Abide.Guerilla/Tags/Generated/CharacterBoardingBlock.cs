using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct CharacterBoardingBlock
	{
		public enum Flags0Options
		{
			AirborneBoarding_0 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("max distance:wus#maximum distance from entry point that we will consider boarding", null)]
		public float MaxDistance1;
		[Field("abort distance:wus#give up trying to get in boarding seat if entry point is further away than this", null)]
		public float AbortDistance2;
		[Field("", null)]
		public fixed byte _3[12];
		[Field("max speed:wu/s#maximum speed at which we will consider boarding", null)]
		public float MaxSpeed4;
	}
}
#pragma warning restore CS1591
