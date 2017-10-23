using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(116, 4)]
	public unsafe struct AiConversationBlock
	{
		public enum Flags1Options
		{
			StopIfDeathThisConversationWillBeAbortedIfAnyParticipantDies_0 = 1,
			StopIfDamagedAnActorWillAbortThisConversationIfTheyAreDamaged_1 = 2,
			StopIfVisibleEnemyAnActorWillAbortThisConversationIfTheySeeAnEnemy_2 = 4,
			StopIfAlertedToEnemyAnActorWillAbortThisConversationIfTheySuspectAnEnemy_3 = 8,
			PlayerMustBeVisibleThisConversationCannotTakePlaceUnlessTheParticipantsCanSeeANearbyPlayer_4 = 16,
			StopOtherActionsParticipantsStopDoingWhateverTheyWereDoingInOrderToPerformThisConversation_5 = 32,
			KeepTryingToPlayIfThisConversationFailsInitiallyItWillKeepTestingToSeeWhenItCanPlay_6 = 64,
			PlayerMustBeLookingThisConversationWillNotStartUntilThePlayerIsLookingAtOneOfTheParticipants_7 = 128,
		}
		[Field("name^", null)]
		public String Name0;
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("trigger distance:world units#distance the player must enter before the conversation can trigger", null)]
		public float TriggerDistance3;
		[Field("run-to-player dist:world units#if the 'involves player' flag is set, when triggered the conversation's participant(s) will run to within this distance of the player", null)]
		public float RunToPlayerDist4;
		[Field("", null)]
		public fixed byte _5[36];
		[Field("participants", null)]
		[Block("Ai Conversation Participant Block", 8, typeof(AiConversationParticipantBlock))]
		public TagBlock Participants6;
		[Field("lines", null)]
		[Block("Ai Conversation Line Block", 32, typeof(AiConversationLineBlock))]
		public TagBlock Lines7;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _8;
	}
}
#pragma warning restore CS1591
