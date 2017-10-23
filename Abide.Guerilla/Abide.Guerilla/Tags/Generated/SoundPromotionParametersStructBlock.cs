using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct SoundPromotionParametersStructBlock
	{
		[Field("promotion rules", null)]
		[Block("Sound Promotion Rule Block", 9, typeof(SoundPromotionRuleBlock))]
		public TagBlock PromotionRules0;
		[Field("", null)]
		[Block("Sound Promotion Runtime Timer Block", 9, typeof(SoundPromotionRuntimeTimerBlock))]
		public TagBlock _1;
		[Field("", null)]
		public fixed byte _2[12];
	}
}
#pragma warning restore CS1591
