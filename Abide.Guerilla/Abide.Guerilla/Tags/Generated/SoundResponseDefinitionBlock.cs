using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(152, 4)]
	public unsafe struct SoundResponseDefinitionBlock
	{
		public enum SoundFlags0Options
		{
			AnnouncerSound_0 = 1,
		}
		[Field("sound flags", typeof(SoundFlags0Options))]
		public short SoundFlags0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("english sound^", null)]
		public TagReference EnglishSound2;
		[Field("extra sounds", typeof(SoundResponseExtraSoundsStructBlock))]
		[Block("Sound Response Extra Sounds Struct", 1, typeof(SoundResponseExtraSoundsStructBlock))]
		public SoundResponseExtraSoundsStructBlock ExtraSounds3;
		[Field("probability", null)]
		public float Probability4;
	}
}
#pragma warning restore CS1591
