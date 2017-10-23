using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(52, 4)]
	public unsafe struct TriggersBlock
	{
		public enum TriggerFlags2Options
		{
			LatchONWhenTriggered_0 = 1,
		}
		public enum CombinationRule3Options
		{
			OR_0 = 0,
			AND_1 = 1,
		}
		[Field("name^", null)]
		public String Name1;
		[Field("trigger flags", typeof(TriggerFlags2Options))]
		public int TriggerFlags2;
		[Field("combination rule", typeof(CombinationRule3Options))]
		public short CombinationRule3;
		[Field("", null)]
		public fixed byte _4[2];
		[Field("", null)]
		public fixed byte _5[24];
		[Field("conditions", null)]
		[Block("Order Completion Condition", 5, typeof(OrderCompletionCondition))]
		public TagBlock Conditions6;
	}
}
#pragma warning restore CS1591
