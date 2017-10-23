using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(56, 4)]
	public unsafe struct GrenadeHudSoundBlock
	{
		public enum LatchedTo1Options
		{
			LowGrenadeCount_0 = 1,
			NoGrenadesLeft_1 = 2,
			ThrowOnNoGrenades_2 = 4,
		}
		[Field("sound^", null)]
		public TagReference Sound0;
		[Field("latched to", typeof(LatchedTo1Options))]
		public int LatchedTo1;
		[Field("scale", null)]
		public float Scale2;
		[Field("", null)]
		public fixed byte _3[32];
	}
}
#pragma warning restore CS1591
