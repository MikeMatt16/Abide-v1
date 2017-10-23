using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("sound_cache_file_gestalt", "ugh!", "����", typeof(SoundCacheFileGestaltBlock))]
	[FieldSet(132, 4)]
	public unsafe struct SoundCacheFileGestaltBlock
	{
		[Field("playbacks", null)]
		[Block("Sound Gestalt Playback Block", 32767, typeof(SoundGestaltPlaybackBlock))]
		public TagBlock Playbacks0;
		[Field("scales", null)]
		[Block("Sound Gestalt Scale Block", 32767, typeof(SoundGestaltScaleBlock))]
		public TagBlock Scales1;
		[Field("import names", null)]
		[Block("Sound Gestalt Import Names Block", 32767, typeof(SoundGestaltImportNamesBlock))]
		public TagBlock ImportNames2;
		[Field("pitch range parameters", null)]
		[Block("Sound Gestalt Pitch Range Parameters Block", 32767, typeof(SoundGestaltPitchRangeParametersBlock))]
		public TagBlock PitchRangeParameters3;
		[Field("pitch ranges", null)]
		[Block("Sound Gestalt Pitch Ranges Block", 32767, typeof(SoundGestaltPitchRangesBlock))]
		public TagBlock PitchRanges4;
		[Field("permutations", null)]
		[Block("Sound Gestalt Permutations Block", 32767, typeof(SoundGestaltPermutationsBlock))]
		public TagBlock Permutations5;
		[Field("custom playbacks", null)]
		[Block("Sound Gestalt Custom Playback Block", 32767, typeof(SoundGestaltCustomPlaybackBlock))]
		public TagBlock CustomPlaybacks6;
		[Field("runtime permutation flags", null)]
		[Block("Sound Gestalt Runtime Permutation Bit Vector Block", 32767, typeof(SoundGestaltRuntimePermutationBitVectorBlock))]
		public TagBlock RuntimePermutationFlags7;
		[Field("chunks", null)]
		[Block("Sound Permutation Chunk Block", 32767, typeof(SoundPermutationChunkBlock))]
		public TagBlock Chunks8;
		[Field("promotions", null)]
		[Block("Sound Gestalt Promotions Block", 32767, typeof(SoundGestaltPromotionsBlock))]
		public TagBlock Promotions9;
		[Field("extra infos", null)]
		[Block("Sound Gestalt Extra Info Block", 32767, typeof(SoundGestaltExtraInfoBlock))]
		public TagBlock ExtraInfos10;
	}
}
#pragma warning restore CS1591
