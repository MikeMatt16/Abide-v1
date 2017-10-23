using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct AiSceneTriggerBlock
	{
		public enum CombinationRule0Options
		{
			OR_0 = 0,
			AND_1 = 1,
		}
		[Field("combination rule", typeof(CombinationRule0Options))]
		public short CombinationRule0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("triggers", null)]
		[Block("Trigger References", 10, typeof(TriggerReferences))]
		public TagBlock Triggers2;
	}
}
#pragma warning restore CS1591
