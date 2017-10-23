using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct WeaponClassBlock
	{
		[Field("label^", null)]
		public StringId Label0;
		[Field("weapon type|AABBCC", null)]
		[Block("Weapon Type Block", 64, typeof(WeaponTypeBlock))]
		public TagBlock WeaponTypeAABBCC1;
		[Field("weapon ik|AABBCC", null)]
		[Block("Animation Ik Block", 8, typeof(AnimationIkBlock))]
		public TagBlock WeaponIkAABBCC2;
	}
}
#pragma warning restore CS1591
