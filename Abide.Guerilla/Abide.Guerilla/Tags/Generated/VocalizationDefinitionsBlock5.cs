using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(104, 4)]
	public unsafe struct VocalizationDefinitionsBlock5
	{
		public enum Priority3Options
		{
			None_0 = 0,
			Recall_1 = 1,
			Idle_2 = 2,
			Comment_3 = 3,
			IdleResponse_4 = 4,
			Postcombat_5 = 5,
			Combat_6 = 6,
			Status_7 = 7,
			Respond_8 = 8,
			Warn_9 = 9,
			Act_10 = 10,
			React_11 = 11,
			Involuntary_12 = 12,
			Scream_13 = 13,
			Scripted_14 = 14,
			Death_15 = 15,
		}
		public enum Flags4Options
		{
			Immediate_0 = 1,
			Interrupt_1 = 2,
			CancelLowPriority_2 = 4,
		}
		public enum GlanceBehaviorHowDoesTheSpeakerOfThisVocalizationDirectHisGaze5Options
		{
			NONE_0 = 0,
			GlanceSubjectShort_1 = 1,
			GlanceSubjectLong_2 = 2,
			GlanceCauseShort_3 = 3,
			GlanceCauseLong_4 = 4,
			GlanceFriendShort_5 = 5,
			GlanceFriendLong_6 = 6,
		}
		public enum GlanceRecipientBehaviorHowDoesSomeoneWhoHearsMeBehave6Options
		{
			NONE_0 = 0,
			GlanceSubjectShort_1 = 1,
			GlanceSubjectLong_2 = 2,
			GlanceCauseShort_3 = 3,
			GlanceCauseLong_4 = 4,
			GlanceFriendShort_5 = 5,
			GlanceFriendLong_6 = 6,
		}
		public enum PerceptionType7Options
		{
			None_0 = 0,
			Speaker_1 = 1,
			Listener_2 = 2,
		}
		public enum MaxCombatStatus8Options
		{
			Asleep_0 = 0,
			Idle_1 = 1,
			Alert_2 = 2,
			Active_3 = 3,
			Uninspected_4 = 4,
			Definite_5 = 5,
			Certain_6 = 6,
			Visible_7 = 7,
			ClearLos_8 = 8,
			Dangerous_9 = 9,
		}
		public enum AnimationImpulse9Options
		{
			None_0 = 0,
			Shakefist_1 = 1,
			Cheer_2 = 2,
			SurpriseFront_3 = 3,
			SurpriseBack_4 = 4,
			Taunt_5 = 5,
			Brace_6 = 6,
			Point_7 = 7,
			Hold_8 = 8,
			Wave_9 = 9,
			Advance_10 = 10,
			Fallback_11 = 11,
		}
		public enum OverlapPriority10Options
		{
			None_0 = 0,
			Recall_1 = 1,
			Idle_2 = 2,
			Comment_3 = 3,
			IdleResponse_4 = 4,
			Postcombat_5 = 5,
			Combat_6 = 6,
			Status_7 = 7,
			Respond_8 = 8,
			Warn_9 = 9,
			Act_10 = 10,
			React_11 = 11,
			Involuntary_12 = 12,
			Scream_13 = 13,
			Scripted_14 = 14,
			Death_15 = 15,
		}
		public enum SpeakerEmotion20Options
		{
			None_0 = 0,
			Asleep_1 = 1,
			Amorous_2 = 2,
			Happy_3 = 3,
			Inquisitive_4 = 4,
			Repulsed_5 = 5,
			Disappointed_6 = 6,
			Shocked_7 = 7,
			Scared_8 = 8,
			Arrogant_9 = 9,
			Annoyed_10 = 10,
			Angry_11 = 11,
			Pensive_12 = 12,
			Pain_13 = 13,
		}
		public enum ListenerEmotion21Options
		{
			None_0 = 0,
			Asleep_1 = 1,
			Amorous_2 = 2,
			Happy_3 = 3,
			Inquisitive_4 = 4,
			Repulsed_5 = 5,
			Disappointed_6 = 6,
			Shocked_7 = 7,
			Scared_8 = 8,
			Arrogant_9 = 9,
			Annoyed_10 = 10,
			Angry_11 = 11,
			Pensive_12 = 12,
			Pain_13 = 13,
		}
		[Field("vocalization^", null)]
		public StringId Vocalization0;
		[Field("parent vocalization", null)]
		public StringId ParentVocalization1;
		[Field("parent index*", null)]
		public short ParentIndex2;
		[Field("priority", typeof(Priority3Options))]
		public short Priority3;
		[Field("flags", typeof(Flags4Options))]
		public int Flags4;
		[Field("glance behavior#how does the speaker of this vocalization direct his gaze?", typeof(GlanceBehaviorHowDoesTheSpeakerOfThisVocalizationDirectHisGaze5Options))]
		public short GlanceBehavior5;
		[Field("glance recipient behavior#how does someone who hears me behave?", typeof(GlanceRecipientBehaviorHowDoesSomeoneWhoHearsMeBehave6Options))]
		public short GlanceRecipientBehavior6;
		[Field("perception type", typeof(PerceptionType7Options))]
		public short PerceptionType7;
		[Field("max combat status", typeof(MaxCombatStatus8Options))]
		public short MaxCombatStatus8;
		[Field("animation impulse", typeof(AnimationImpulse9Options))]
		public short AnimationImpulse9;
		[Field("overlap priority", typeof(OverlapPriority10Options))]
		public short OverlapPriority10;
		[Field("sound repetition delay:minutes#Minimum delay time between playing the same permutation", null)]
		public float SoundRepetitionDelay11;
		[Field("allowable queue delay:seconds#How long to wait to actually start the vocalization", null)]
		public float AllowableQueueDelay12;
		[Field("pre voc. delay:seconds#How long to wait to actually start the vocalization", null)]
		public float PreVocDelay13;
		[Field("notification delay:seconds#How long into the vocalization the AI should be notified", null)]
		public float NotificationDelay14;
		[Field("post voc. delay:seconds#How long speech is suppressed in the speaking unit after vocalizing", null)]
		public float PostVocDelay15;
		[Field("repeat delay:seconds#How long before the same vocalization can be repeated", null)]
		public float RepeatDelay16;
		[Field("weight:[0-1]#Inherent weight of this vocalization", null)]
		public float Weight17;
		[Field("speaker freeze time#speaker won't move for the given amount of time", null)]
		public float SpeakerFreezeTime18;
		[Field("listener freeze time#listener won't move for the given amount of time (from start of vocalization)", null)]
		public float ListenerFreezeTime19;
		[Field("speaker emotion", typeof(SpeakerEmotion20Options))]
		public short SpeakerEmotion20;
		[Field("listener emotion", typeof(ListenerEmotion21Options))]
		public short ListenerEmotion21;
		[Field("player skip fraction", null)]
		public float PlayerSkipFraction22;
		[Field("skip fraction", null)]
		public float SkipFraction23;
		[Field("Sample line", null)]
		public StringId SampleLine24;
		[Field("reponses", null)]
		[Block("Response Block", 20, typeof(ResponseBlock))]
		public TagBlock Reponses25;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _26;
	}
}
#pragma warning restore CS1591
