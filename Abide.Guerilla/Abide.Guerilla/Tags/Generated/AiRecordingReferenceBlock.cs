using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct AiRecordingReferenceBlock
	{
		[Field("recording name^", null)]
		public String RecordingName0;
		[Field("", null)]
		public fixed byte _1[8];
	}
}
#pragma warning restore CS1591
