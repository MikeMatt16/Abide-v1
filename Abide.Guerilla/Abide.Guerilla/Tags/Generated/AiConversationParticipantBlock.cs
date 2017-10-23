using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(84, 4)]
	public unsafe struct AiConversationParticipantBlock
	{
		[Field("", null)]
		public fixed byte _0[8];
		[Field("use this object#if a unit with this name exists, we try to pick them to start the conversation", null)]
		public short UseThisObject1;
		[Field("set new name#once we pick a unit, we name it this", null)]
		public short SetNewName2;
		[Field("", null)]
		public fixed byte _3[12];
		[Field("", null)]
		public fixed byte _4[12];
		[Field("encounter name", null)]
		public String EncounterName5;
		[Field("", null)]
		public fixed byte _6[4];
		[Field("", null)]
		public fixed byte _7[12];
	}
}
#pragma warning restore CS1591
