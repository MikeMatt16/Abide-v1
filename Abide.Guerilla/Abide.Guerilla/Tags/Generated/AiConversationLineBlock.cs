using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(124, 4)]
	public unsafe struct AiConversationLineBlock
	{
		public enum Flags0Options
		{
			AddresseeLookAtSpeaker_0 = 1,
			EveryoneLookAtSpeaker_1 = 2,
			EveryoneLookAtAddressee_2 = 4,
			WaitAfterUntilToldToAdvance_3 = 8,
			WaitUntilSpeakerNearby_4 = 16,
			WaitUntilEveryoneNearby_5 = 32,
		}
		public enum Addressee2Options
		{
			None_0 = 0,
			Player_1 = 1,
			Participant_2 = 2,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("participant", null)]
		public short Participant1;
		[Field("addressee", typeof(Addressee2Options))]
		public short Addressee2;
		[Field("addressee participant#this field is only used if the addressee type is 'participant'", null)]
		public short AddresseeParticipant3;
		[Field("", null)]
		public fixed byte _4[4];
		[Field("line delay time", null)]
		public float LineDelayTime5;
		[Field("", null)]
		public fixed byte _6[12];
		[Field("variant 1", null)]
		public TagReference Variant17;
		[Field("variant 2", null)]
		public TagReference Variant28;
		[Field("variant 3", null)]
		public TagReference Variant39;
		[Field("variant 4", null)]
		public TagReference Variant410;
		[Field("variant 5", null)]
		public TagReference Variant511;
		[Field("variant 6", null)]
		public TagReference Variant612;
	}
}
#pragma warning restore CS1591
