using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct SoundGestaltPermutationsBlock
	{
		[Field("name^", null)]
		public short Name0;
		[Field("encoded skip fraction", null)]
		public short EncodedSkipFraction1;
		[Field("encoded gain:dB", null)]
		public int EncodedGain2;
		[Field("permutation info index", null)]
		public int PermutationInfoIndex3;
		[Field("language neutral time:ms", null)]
		public short LanguageNeutralTime4;
		[Field("sample size", null)]
		public int SampleSize5;
		[Field("first chunk", null)]
		public short FirstChunk6;
		[Field("chunk count", null)]
		public short ChunkCount7;
	}
}
#pragma warning restore CS1591
