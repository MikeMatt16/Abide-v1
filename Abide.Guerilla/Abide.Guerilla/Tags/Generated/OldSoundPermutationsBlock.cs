using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(124, 4)]
	public unsafe struct OldSoundPermutationsBlock
	{
		public enum Compression3Options
		{
			NoneBigEndian_0 = 0,
			XboxAdpcm_1 = 1,
			ImaAdpcm_2 = 2,
			NoneLittleEndian_3 = 3,
			Wma_4 = 4,
		}
		[Field("name*^#name of the file from which this sample was imported", null)]
		public String Name0;
		[Field("skip fraction#fraction of requests to play this permutation that are ignored (a different permutation is selected.)", null)]
		public float SkipFraction1;
		[Field("gain:dB#fraction of recorded volume to play at.", null)]
		public float Gain2;
		[Field("compression*", typeof(Compression3Options))]
		public short Compression3;
		[Field("next permutation index*", null)]
		public short NextPermutationIndex4;
		[Field("", null)]
		public fixed byte _5[20];
		[Field("samples#sampled sound data", null)]
		[Data(16777216)]
		public TagBlock Samples6;
		[Field("mouth data", null)]
		[Data(8192)]
		public TagBlock MouthData7;
		[Field("subtitle data", null)]
		[Data(512)]
		public TagBlock SubtitleData8;
	}
}
#pragma warning restore CS1591
