using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct MissionDialogueVariantsBlock
	{
		[Field("variant designation#3-letter designation for the character^", null)]
		public StringId VariantDesignation0;
		[Field("sound", null)]
		public TagReference Sound1;
		[Field("sound effect", null)]
		public StringId SoundEffect2;
	}
}
#pragma warning restore CS1591
