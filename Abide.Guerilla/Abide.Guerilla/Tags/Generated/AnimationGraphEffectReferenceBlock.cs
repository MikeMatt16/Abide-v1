using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct AnimationGraphEffectReferenceBlock
	{
		public enum Flags1Options
		{
			AllowOnPlayer_0 = 1,
			LeftArmOnly_1 = 2,
			RightArmOnly_2 = 4,
			FirstPersonOnly_3 = 8,
			ForwardOnly_4 = 16,
			ReverseOnly_5 = 32,
		}
		[Field("effect^", null)]
		public TagReference Effect0;
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("", null)]
		public fixed byte _2[2];
	}
}
#pragma warning restore CS1591
