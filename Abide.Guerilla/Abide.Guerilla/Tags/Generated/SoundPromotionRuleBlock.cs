using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct SoundPromotionRuleBlock
	{
		[Field("pitch range^", null)]
		public short PitchRange0;
		[Field("maximum playing count", null)]
		public short MaximumPlayingCount1;
		[Field("suppression time:seconds#time from when first permutation plays to when another sound from an equal or lower promotion can play", null)]
		public float SuppressionTime2;
		[Field("", null)]
		public fixed byte _3[8];
	}
}
#pragma warning restore CS1591
