using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("hud_message_text", "hmt ", "����", typeof(HudMessageTextBlock))]
	[FieldSet(128, 4)]
	public unsafe struct HudMessageTextBlock
	{
		[Field("text data*", null)]
		[Data(65536)]
		public TagBlock TextData0;
		[Field("message elements*", null)]
		[Block("Hud Message Elements Block", 8192, typeof(HudMessageElementsBlock))]
		public TagBlock MessageElements1;
		[Field("messages*", null)]
		[Block("Hud Messages Block", 1024, typeof(HudMessagesBlock))]
		public TagBlock Messages2;
		[Field("", null)]
		public fixed byte _3[84];
	}
}
#pragma warning restore CS1591
