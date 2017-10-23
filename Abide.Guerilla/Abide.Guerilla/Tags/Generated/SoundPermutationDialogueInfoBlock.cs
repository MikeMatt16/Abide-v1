using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct SoundPermutationDialogueInfoBlock
	{
		[Field("mouth data offset", null)]
		public int MouthDataOffset0;
		[Field("mouth data length", null)]
		public int MouthDataLength1;
		[Field("lipsync data offset", null)]
		public int LipsyncDataOffset2;
		[Field("lipsync data length", null)]
		public int LipsyncDataLength3;
	}
}
#pragma warning restore CS1591
