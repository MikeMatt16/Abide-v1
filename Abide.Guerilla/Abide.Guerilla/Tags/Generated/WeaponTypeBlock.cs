using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(76, 4)]
	public unsafe struct WeaponTypeBlock
	{
		[Field("label^", null)]
		public StringId Label0;
		[Field("actions|AABBCC", null)]
		[Block("Animation Entry Block", 256, typeof(AnimationEntryBlock))]
		public TagBlock ActionsAABBCC1;
		[Field("overlays|AABBCC", null)]
		[Block("Animation Entry Block", 256, typeof(AnimationEntryBlock))]
		public TagBlock OverlaysAABBCC2;
		[Field("death and damage|AABBCC", null)]
		[Block("Damage Animation Block", 8, typeof(DamageAnimationBlock))]
		public TagBlock DeathAndDamageAABBCC3;
		[Field("transitions|AABBCC", null)]
		[Block("Animation Transition Block", 256, typeof(AnimationTransitionBlock))]
		public TagBlock TransitionsAABBCC4;
		[Field("high precache|CCCCC", null)]
		[Block("Precache List Block", 1024, typeof(PrecacheListBlock))]
		public TagBlock HighPrecacheCCCCC5;
		[Field("low precache|CCCCC", null)]
		[Block("Precache List Block", 1024, typeof(PrecacheListBlock))]
		public TagBlock LowPrecacheCCCCC6;
	}
}
#pragma warning restore CS1591
