using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct ResponseBlock
	{
		public enum Flags1Options
		{
			Nonexclusive_0 = 1,
			TriggerResponse_1 = 2,
		}
		public enum ResponseType3Options
		{
			Friend_0 = 0,
			Enemy_1 = 1,
			Listener_2 = 2,
			Joint_3 = 3,
			Peer_4 = 4,
		}
		[Field("vocalization name", null)]
		public StringId VocalizationName0;
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("vocalization index (post process)*", null)]
		public short VocalizationIndexPostProcess2;
		[Field("response type", typeof(ResponseType3Options))]
		public short ResponseType3;
		[Field("dialogue index (import)*", null)]
		public short DialogueIndexImport4;
	}
}
#pragma warning restore CS1591
