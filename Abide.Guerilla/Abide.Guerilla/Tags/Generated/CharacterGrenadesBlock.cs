using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(60, 4)]
	public unsafe struct CharacterGrenadesBlock
	{
		public enum GrenadesFlags0Options
		{
			Flag1_0 = 1,
		}
		public enum GrenadeTypeTypeOfGrenadesThatWeThrow1Options
		{
			HumanFragmentation_0 = 0,
			CovenantPlasma_1 = 1,
		}
		public enum TrajectoryTypeHowWeThrowOurGrenades2Options
		{
			Toss_0 = 0,
			Lob_1 = 1,
			Bounce_2 = 2,
		}
		[Field("grenades flags", typeof(GrenadesFlags0Options))]
		public int GrenadesFlags0;
		[Field("grenade type#type of grenades that we throw^", typeof(GrenadeTypeTypeOfGrenadesThatWeThrow1Options))]
		public short GrenadeType1;
		[Field("trajectory type#how we throw our grenades", typeof(TrajectoryTypeHowWeThrowOurGrenades2Options))]
		public short TrajectoryType2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("minimum enemy count#how many enemies must be within the radius of the grenade before we will consider throwing there", null)]
		public short MinimumEnemyCount4;
		[Field("enemy radius:world units#we consider enemies within this radius when determining where to throw", null)]
		public float EnemyRadius5;
		[Field("grenade ideal velocity:world units per second#how fast we LIKE to throw our grenades", null)]
		public float GrenadeIdealVelocity6;
		[Field("grenade velocity:world units per second#the fastest we can possibly throw our grenades", null)]
		public float GrenadeVelocity7;
		[Field("grenade ranges:world units#ranges within which we will consider throwing a grenade", null)]
		public FloatBounds GrenadeRanges8;
		[Field("collateral damage radius:world units#we won't throw if there are friendlies around our target within this range", null)]
		public float CollateralDamageRadius9;
		[Field("grenade chance:[0,1]#how likely we are to throw a grenade in one second", null)]
		public float GrenadeChance10;
		[Field("grenade throw delay:seconds#How long we have to wait after throwing a grenade before we can throw another one", null)]
		public float GrenadeThrowDelay11;
		[Field("", null)]
		public fixed byte _12[4];
		[Field("grenade uncover chance:[0,1]#how likely we are to throw a grenade to flush out a target in one second", null)]
		public float GrenadeUncoverChance13;
		[Field("", null)]
		public fixed byte _14[4];
		[Field("anti-vehicle grenade chance:[0,1]#how likely we are to throw a grenade against a vehicle", null)]
		public float AntiVehicleGrenadeChance15;
		[Field("", null)]
		public fixed byte _16[4];
		[Field("grenade count#number of grenades that we start with", null)]
		public FloatBounds GrenadeCount18;
		[Field("dont drop grenades chance:[0,1]#how likely we are not to drop any grenades when we die, even if we still have some", null)]
		public float DontDropGrenadesChance19;
	}
}
#pragma warning restore CS1591
