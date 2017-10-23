using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct PoweredSeatBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("driver powerup time:seconds", null)]
		public float DriverPowerupTime1;
		[Field("driver powerdown time:seconds", null)]
		public float DriverPowerdownTime2;
		[Field("", null)]
		public fixed byte _3[56];
	}
}
#pragma warning restore CS1591
