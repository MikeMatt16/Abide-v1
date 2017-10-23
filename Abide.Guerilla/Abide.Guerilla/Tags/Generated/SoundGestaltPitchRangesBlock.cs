using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct SoundGestaltPitchRangesBlock
	{
		[Field("name^", null)]
		public short Name0;
		[Field("parameters", null)]
		public short Parameters1;
		[Field("encoded permutation data", null)]
		public short EncodedPermutationData2;
		[Field("first runtime permutation flag index", null)]
		public short FirstRuntimePermutationFlagIndex3;
		[Field("first permutation", null)]
		public short FirstPermutation4;
		[Field("permutation count", null)]
		public short PermutationCount5;
	}
}
#pragma warning restore CS1591
