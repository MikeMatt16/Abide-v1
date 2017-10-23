using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(212, 4)]
	public unsafe struct PlayerRepresentationBlock
	{
		[Field("first person hands", null)]
		public TagReference FirstPersonHands0;
		[Field("first person body", null)]
		public TagReference FirstPersonBody1;
		[Field("", null)]
		public fixed byte _2[40];
		[Field("", null)]
		public fixed byte _3[120];
		[Field("third person unit", null)]
		public TagReference ThirdPersonUnit4;
		[Field("third person variant", null)]
		public StringId ThirdPersonVariant5;
	}
}
#pragma warning restore CS1591
