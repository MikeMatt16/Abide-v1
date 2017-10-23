using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(60, 4)]
	public unsafe struct HudWaypointArrowBlock
	{
		[Field("bitmap", null)]
		public TagReference Bitmap0;
		[Field("shader", null)]
		public TagReference Shader1;
		[Field("sequence index", null)]
		public short SequenceIndex2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("smallest size", null)]
		public float SmallestSize4;
		[Field("smallest distance", null)]
		public float SmallestDistance5;
		[Field("border bitmap", null)]
		public TagReference BorderBitmap6;
	}
}
#pragma warning restore CS1591
