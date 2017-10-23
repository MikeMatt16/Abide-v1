using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct AnimationBlendScreenBlock
	{
		[Field("label^", null)]
		public StringId Label0;
		[Field("aiming screen*", typeof(AnimationAimingScreenStructBlock))]
		[Block("Animation Aiming Screen Struct", 1, typeof(AnimationAimingScreenStructBlock))]
		public AnimationAimingScreenStructBlock AimingScreen1;
	}
}
#pragma warning restore CS1591
