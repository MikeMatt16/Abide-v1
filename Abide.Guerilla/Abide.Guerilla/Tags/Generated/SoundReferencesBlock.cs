using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct SoundReferencesBlock
	{
		public enum Flags0Options
		{
			NewVocalization_0 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("vocalization^", null)]
		public StringId Vocalization2;
		[Field("sound", null)]
		public TagReference Sound3;
	}
}
#pragma warning restore CS1591
