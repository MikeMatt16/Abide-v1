using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct CharacterIdleBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("", null)]
		public fixed byte _1[24];
		[Field("idle pose delay time:seconds#time range for delays between idle poses", null)]
		public FloatBounds IdlePoseDelayTime2;
	}
}
#pragma warning restore CS1591
