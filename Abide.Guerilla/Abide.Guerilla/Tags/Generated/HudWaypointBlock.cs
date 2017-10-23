using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct HudWaypointBlock
	{
		[Field("bitmap", null)]
		public TagReference Bitmap0;
		[Field("shader", null)]
		public TagReference Shader1;
		[Field("onscreen sequence index", null)]
		public short OnscreenSequenceIndex2;
		[Field("occluded sequence index", null)]
		public short OccludedSequenceIndex3;
		[Field("offscreen sequence index", null)]
		public short OffscreenSequenceIndex4;
		[Field("", null)]
		public fixed byte _5[2];
	}
}
#pragma warning restore CS1591
