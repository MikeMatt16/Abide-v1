using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("dialogue", "udlg", "����", typeof(DialogueBlock))]
	[FieldSet(36, 4)]
	public unsafe struct DialogueBlock
	{
		public enum Flags1Options
		{
			Female_0 = 1,
		}
		[Field("global dialogue info", null)]
		public TagReference GlobalDialogueInfo0;
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("vocalizations", null)]
		[Block("Sound References Block", 500, typeof(SoundReferencesBlock))]
		public TagBlock Vocalizations2;
		[Field("mission dialogue designator#3-letter mission dialogue designator name", null)]
		public StringId MissionDialogueDesignator3;
	}
}
#pragma warning restore CS1591
