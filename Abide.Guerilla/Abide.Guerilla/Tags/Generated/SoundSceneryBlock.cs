using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("sound_scenery", "ssce", "obje", typeof(SoundSceneryBlock))]
	[FieldSet(16, 4)]
	public unsafe struct SoundSceneryBlock
	{
		[Field("", null)]
		public fixed byte _0[16];
		[Field("", null)]
		public fixed byte _1[112];
	}
}
#pragma warning restore CS1591
