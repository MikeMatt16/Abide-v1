using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct AnimationModeBlock
	{
		[Field("label^", null)]
		public StringId Label0;
		[Field("weapon class|AABBCC", null)]
		[Block("Weapon Class Block", 64, typeof(WeaponClassBlock))]
		public TagBlock WeaponClassAABBCC1;
		[Field("mode ik|AABBCC", null)]
		[Block("Animation Ik Block", 8, typeof(AnimationIkBlock))]
		public TagBlock ModeIkAABBCC2;
	}
}
#pragma warning restore CS1591
