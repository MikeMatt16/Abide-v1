using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(68, 4)]
	public unsafe struct SoundGlobalsBlock
	{
		[Field("sound classes", null)]
		public TagReference SoundClasses0;
		[Field("sound effects", null)]
		public TagReference SoundEffects1;
		[Field("sound mix", null)]
		public TagReference SoundMix2;
		[Field("sound combat dialogue constants", null)]
		public TagReference SoundCombatDialogueConstants3;
		[Field("", null)]
		public int _4;
	}
}
#pragma warning restore CS1591
