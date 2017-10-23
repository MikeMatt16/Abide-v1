using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(88, 4)]
	public unsafe struct ModelAnimationRuntimeDataStructBlock
	{
		[Field("inheritence list*|BBAAAA", null)]
		[Block("Inherited Animation Block", 8, typeof(InheritedAnimationBlock))]
		public TagBlock InheritenceListBBAAAA1;
		[Field("weapon list*|BBAAAA", null)]
		[Block("Weapon Class Lookup Block", 64, typeof(WeaponClassLookupBlock))]
		public TagBlock WeaponListBBAAAA2;
		[Field("", null)]
		public fixed byte _3[32];
		[Field("", null)]
		public fixed byte _4[32];
	}
}
#pragma warning restore CS1591
