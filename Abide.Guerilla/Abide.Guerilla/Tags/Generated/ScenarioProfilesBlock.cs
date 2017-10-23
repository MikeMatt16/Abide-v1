using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(84, 4)]
	public unsafe struct ScenarioProfilesBlock
	{
		[Field("Name^", null)]
		public String Name0;
		[Field("Starting Health Damage:[0,1]", null)]
		public float StartingHealthDamage1;
		[Field("Starting Shield Damage:[0,1]", null)]
		public float StartingShieldDamage2;
		[Field("Primary Weapon", null)]
		public TagReference PrimaryWeapon3;
		[Field("Rounds Loaded", null)]
		public short RoundsLoaded4;
		[Field("Rounds Total", null)]
		public short RoundsTotal5;
		[Field("Secondary Weapon", null)]
		public TagReference SecondaryWeapon6;
		[Field("Rounds Loaded", null)]
		public short RoundsLoaded7;
		[Field("Rounds Total", null)]
		public short RoundsTotal8;
		[Field("Starting Fragmentation Grenade Count", null)]
		public int StartingFragmentationGrenadeCount9;
		[Field("Starting Plasma Grenade Count", null)]
		public int StartingPlasmaGrenadeCount10;
		[Field("Starting <unknown> Grenade Count", null)]
		public int StartingUnknownGrenadeCount11;
		[Field("Starting <unknown> Grenade Count", null)]
		public int StartingUnknownGrenadeCount12;
		[Field("", null)]
		public fixed byte _13[20];
	}
}
#pragma warning restore CS1591
