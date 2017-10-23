using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(80, 4)]
	public unsafe struct UnitBoardingMeleeStructBlock
	{
		[Field("boarding melee damage", null)]
		public TagReference BoardingMeleeDamage0;
		[Field("boarding melee response", null)]
		public TagReference BoardingMeleeResponse1;
		[Field("landing melee damage", null)]
		public TagReference LandingMeleeDamage2;
		[Field("flurry melee damage", null)]
		public TagReference FlurryMeleeDamage3;
		[Field("obstacle smash damage", null)]
		public TagReference ObstacleSmashDamage4;
	}
}
#pragma warning restore CS1591
