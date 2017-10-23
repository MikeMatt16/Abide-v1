using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct HudMessagesBlock
	{
		[Field("name*", null)]
		public String Name0;
		[Field("start index into text blob*", null)]
		public short StartIndexIntoTextBlob1;
		[Field("start index of message block*", null)]
		public short StartIndexOfMessageBlock2;
		[Field("panel count*", null)]
		public int PanelCount3;
		[Field("", null)]
		public fixed byte _4[3];
		[Field("", null)]
		public fixed byte _5[24];
	}
}
#pragma warning restore CS1591
