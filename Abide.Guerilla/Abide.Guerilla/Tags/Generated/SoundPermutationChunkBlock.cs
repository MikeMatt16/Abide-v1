using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct SoundPermutationChunkBlock
	{
		[Field("file offset*", null)]
		public int FileOffset0;
		[Field("", null)]
		public int _1;
		[Field("", null)]
		public int _2;
	}
}
#pragma warning restore CS1591
