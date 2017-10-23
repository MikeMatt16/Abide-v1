using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct OldSoundPermutationInfoBlock
	{
		[Field("", null)]
		public fixed byte _0[12];
		[Field("mouth data", null)]
		[Data(8192)]
		public TagBlock MouthData1;
	}
}
#pragma warning restore CS1591
