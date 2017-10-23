using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(100, 4)]
	public unsafe struct BarrelFiringEffectBlock
	{
		[Field("shot count lower bound#the minimum number of times this firing effect will be used, once it has been chosen", null)]
		public short ShotCountLowerBound0;
		[Field("shot count upper bound#the maximum number of times this firing effect will be used, once it has been chosen", null)]
		public short ShotCountUpperBound1;
		[Field("firing effect^#this effect is used when the weapon is loaded and fired normally", null)]
		public TagReference FiringEffect2;
		[Field("misfire effect#this effect is used when the weapon is loaded but fired while overheated", null)]
		public TagReference MisfireEffect3;
		[Field("empty effect#this effect is used when the weapon is not loaded", null)]
		public TagReference EmptyEffect4;
		[Field("firing damage#this effect is used when the weapon is loaded and fired normally", null)]
		public TagReference FiringDamage5;
		[Field("misfire damage#this effect is used when the weapon is loaded but fired while overheated", null)]
		public TagReference MisfireDamage6;
		[Field("empty damage#this effect is used when the weapon is not loaded", null)]
		public TagReference EmptyDamage7;
	}
}
#pragma warning restore CS1591
