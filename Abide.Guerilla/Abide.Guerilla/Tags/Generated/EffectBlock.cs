using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("effect", "effe", "����", typeof(EffectBlock))]
	[FieldSet(64, 4)]
	public unsafe struct EffectBlock
	{
		public enum Flags0Options
		{
			DeletedWhenAttachmentDeactivates_0 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("loop start event", null)]
		public short LoopStartEvent1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("", null)]
		public fixed byte _3[4];
		[Field("locations", null)]
		[Block("Effect Locations Block", 32, typeof(EffectLocationsBlock))]
		public TagBlock Locations4;
		[Field("events", null)]
		[Block("Effect Event Block", 32, typeof(EffectEventBlock))]
		public TagBlock Events5;
		[Field("", null)]
		public fixed byte _6[12];
		[Field("looping sound", null)]
		public TagReference LoopingSound8;
		[Field("location", null)]
		public short Location9;
		[Field("", null)]
		public fixed byte _10[2];
		[Field("always play distance", null)]
		public float AlwaysPlayDistance11;
		[Field("never play distance", null)]
		public float NeverPlayDistance12;
	}
}
#pragma warning restore CS1591
