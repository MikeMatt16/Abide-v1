using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct SecondarySetTriggerBlock
	{
		public enum CombinationRule0Options
		{
			OR_0 = 0,
			AND_1 = 1,
		}
		public enum DialogueTypeWhenThisEndingIsTriggeredLaunchADialogueEventOfTheGivenType1Options
		{
			None_0 = 0,
			Advance_1 = 1,
			Charge_2 = 2,
			FallBack_3 = 3,
			Retreat_4 = 4,
			Moveone_5 = 5,
			Arrival_6 = 6,
			EnterVehicle_7 = 7,
			ExitVehicle_8 = 8,
			FollowPlayer_9 = 9,
			LeavePlayer_10 = 10,
			Support_11 = 11,
		}
		[Field("combination rule", typeof(CombinationRule0Options))]
		public short CombinationRule0;
		[Field("dialogue type#when this ending is triggered, launch a dialogue event of the given type", typeof(DialogueTypeWhenThisEndingIsTriggeredLaunchADialogueEventOfTheGivenType1Options))]
		public short DialogueType1;
		[Field("triggers", null)]
		[Block("Trigger References", 10, typeof(TriggerReferences))]
		public TagBlock Triggers2;
	}
}
#pragma warning restore CS1591
