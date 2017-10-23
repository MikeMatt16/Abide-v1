using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct AnimationDestinationStateStructBlock
	{
		public enum FrameEventLinkWhichFrameEventToLinkTo1Options
		{
			NOKEYFRAME_0 = 0,
			KEYFRAMETYPEA_1 = 1,
			KEYFRAMETYPEB_2 = 2,
			KEYFRAMETYPEC_3 = 3,
			KEYFRAMETYPED_4 = 4,
		}
		[Field("state name*#name of the state", null)]
		public StringId StateName0;
		[Field("frame event link*#which frame event to link to", typeof(FrameEventLinkWhichFrameEventToLinkTo1Options))]
		public byte FrameEventLink1;
		[Field("", null)]
		public fixed byte _2[1];
		[Field("index a*#first level sub-index into state", null)]
		public int IndexA3;
		[Field("index b*#second level sub-index into state", null)]
		public int IndexB4;
	}
}
#pragma warning restore CS1591
