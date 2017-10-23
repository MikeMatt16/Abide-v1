using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("sound_classes", "sncl", "����", typeof(SoundClassesBlock))]
	[FieldSet(12, 4)]
	public unsafe struct SoundClassesBlock
	{
		[Field("sound classes", null)]
		[Block("Sound Class Block", 54, typeof(SoundClassBlock))]
		public TagBlock SoundClasses0;
	}
}
#pragma warning restore CS1591
