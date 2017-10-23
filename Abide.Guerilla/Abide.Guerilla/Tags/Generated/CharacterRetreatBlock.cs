using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(84, 4)]
	public unsafe struct CharacterRetreatBlock
	{
		public enum RetreatFlags0Options
		{
			ZigZagWhenFleeing_0 = 1,
			Unused1_1 = 2,
		}
		[Field("Retreat flags", typeof(RetreatFlags0Options))]
		public int RetreatFlags0;
		[Field("Shield threshold#When shield vitality drops below given amount, retreat is triggered by low_shield_retreat_impulse", null)]
		public float ShieldThreshold1;
		[Field("Scary target threshold#When confronting an enemy of over the given scariness, retreat is triggered by scary_target_retreat_impulse", null)]
		public float ScaryTargetThreshold2;
		[Field("Danger threshold#When perceived danger rises above the given threshold, retreat is triggered by danger_retreat_impulse", null)]
		public float DangerThreshold3;
		[Field("Proximity threshold#When enemy closer than given threshold, retreat is triggered by proximity_retreat_impulse", null)]
		public float ProximityThreshold4;
		[Field("", null)]
		public fixed byte _5[16];
		[Field("min/max forced cower time bounds#actor cowers for at least the given amount of time", null)]
		public FloatBounds MinMaxForcedCowerTimeBounds6;
		[Field("min/max cower timeout bounds#actor times out of cower after the given amount of time", null)]
		public FloatBounds MinMaxCowerTimeoutBounds7;
		[Field("", null)]
		public fixed byte _8[12];
		[Field("proximity ambush threshold#If target reaches is within the given proximity, an ambush is triggered by the proximity ambush impulse", null)]
		public float ProximityAmbushThreshold9;
		[Field("awareness ambush threshold#If target is less than threshold (0-1) aware of me, an ambush is triggered by the vulnerable enemy ambush impulse", null)]
		public float AwarenessAmbushThreshold10;
		[Field("", null)]
		public fixed byte _11[24];
		[Field("leader dead retreat chance#If leader-dead-retreat-impulse is active, gives the chance that we will flee when our leader dies within 4 world units of us", null)]
		public float LeaderDeadRetreatChance12;
		[Field("peer dead retreat chance#If peer-dead-retreat-impulse is active, gives the chance that we will flee when one of our peers (friend of the same race) dies within 4 world units of us", null)]
		public float PeerDeadRetreatChance13;
		[Field("second peer dead retreat chance#If peer-dead-retreat-impulse is active, gives the chance that we will flee when a second peer (friend of the same race) dies within 4 world units of us", null)]
		public float SecondPeerDeadRetreatChance14;
		[Field("", null)]
		public fixed byte _15[12];
		[Field("zig-zag angle:degrees#The angle from the intended destination direction that a zig-zag will cause", null)]
		public float ZigZagAngle16;
		[Field("zig-zag period:seconds#How long it takes to zig left and then zag right.", null)]
		public float ZigZagPeriod17;
		[Field("", null)]
		public fixed byte _18[8];
		[Field("retreat grenade chance#The likelihood of throwing down a grenade to cover our retreat", null)]
		public float RetreatGrenadeChance19;
		[Field("backup weapon#If I want to flee and I don't have flee animations with my current weapon, throw it away and try a ...", null)]
		public TagReference BackupWeapon20;
	}
}
#pragma warning restore CS1591
