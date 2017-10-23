using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(84, 4)]
	public unsafe struct OldSoundPitchRangeBlock
	{
		[Field("name*#the name of the imported pitch range directory", null)]
		public String Name0;
		[Field("natural pitch:cents#the apparent pitch when these samples are played at their recorded pitch.", null)]
		public float NaturalPitch2;
		[Field("bend bounds:cents#the range of pitches that will be represented using this sample. this should always contain the natural pitch.", null)]
		public FloatBounds BendBounds3;
		[Field("actual permutation count*", null)]
		public short ActualPermutationCount4;
		[Field("", null)]
		public fixed byte _5[2];
		[Field("", null)]
		public fixed byte _6[12];
		[Field("permutations#permutations represent equivalent variations of this sound.", null)]
		[Block("Old Sound Permutations Block", 1024, typeof(OldSoundPermutationsBlock))]
		public TagBlock Permutations7;
		[Field("permutation info", null)]
		[Block("Old Sound Permutation Info Block", 32, typeof(OldSoundPermutationInfoBlock))]
		public TagBlock PermutationInfo8;
	}
}
#pragma warning restore CS1591
