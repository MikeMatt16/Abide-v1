using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct MissionDialogueLinesBlock
	{
		[Field("name^", null)]
		public StringId Name0;
		[Field("variants", null)]
		[Block("Mission Dialogue Variants Block", 10, typeof(MissionDialogueVariantsBlock))]
		public TagBlock Variants1;
		[Field("default sound effect", null)]
		public StringId DefaultSoundEffect2;
	}
}
#pragma warning restore CS1591
