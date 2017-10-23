using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(52, 4)]
	public unsafe struct HudDashlightsBlock
	{
		public enum Flags3Options
		{
			DonTScaleWhenPulsing_0 = 1,
		}
		[Field("bitmap", null)]
		public TagReference Bitmap0;
		[Field("shader", null)]
		public TagReference Shader1;
		[Field("sequence index", null)]
		public short SequenceIndex2;
		[Field("flags", typeof(Flags3Options))]
		public short Flags3;
		[Field("sound", null)]
		public TagReference Sound4;
	}
}
#pragma warning restore CS1591
