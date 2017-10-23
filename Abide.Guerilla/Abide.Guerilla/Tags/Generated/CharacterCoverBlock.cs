using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct CharacterCoverBlock
	{
		public enum CoverFlags0Options
		{
			Flag1_0 = 1,
		}
		[Field("cover flags", typeof(CoverFlags0Options))]
		public int CoverFlags0;
		[Field("hide behind cover time:seconds#how long we stay behind cover after seeking cover", null)]
		public FloatBounds HideBehindCoverTime1;
		[Field("", null)]
		public fixed byte _2[4];
		[Field("Cover vitality threshold#When vitality drops below this level, possibly trigger a cover", null)]
		public float CoverVitalityThreshold3;
		[Field("Cover shield fraction#trigger cover when shield drops below this fraction (low shield cover impulse must be enabled)", null)]
		public float CoverShieldFraction4;
		[Field("Cover check delay#amount of time I will wait before trying again after covering", null)]
		public float CoverCheckDelay5;
		[Field("Emerge from cover when shield fraction reaches threshold#Emerge from cover when shield fraction reaches threshold", null)]
		public float EmergeFromCoverWhenShieldFractionReachesThreshold6;
		[Field("Cover danger threshold#Danger must be this high to cover. At a danger level of 'danger threshold', the chance of seeking cover is the cover chance lower bound (below)", null)]
		public float CoverDangerThreshold7;
		[Field("Danger upper threshold#At or above danger level of upper threshold, the chance of seeking cover is the cover chance upper bound (below)", null)]
		public float DangerUpperThreshold8;
		[Field("Cover chance#Bounds on the chances of seeking cover.", null)]
		public FloatBounds CoverChance10;
		[Field("Proximity self-preserve:wus#When the proximity_self_preservation impulse is enabled, triggers self-preservation when target within this distance", null)]
		public float ProximitySelfPreserve11;
		[Field("Disallow cover distance:world units#Disallow covering from visible target under the given distance away", null)]
		public float DisallowCoverDistance12;
		[Field("", null)]
		public fixed byte _13[16];
		[Field("proximity melee distance#When self preserving from a target less than given distance, causes melee attack (assuming proximity_melee_impulse is enabled)", null)]
		public float ProximityMeleeDistance14;
		[Field("", null)]
		public fixed byte _15[8];
		[Field("unreachable enemy danger threshold#When danger from an unreachable enemy surpasses threshold, actor cover (assuming unreachable_enemy_cover impulse is enabled)", null)]
		public float UnreachableEnemyDangerThreshold16;
		[Field("scary target threshold#When target is aware of me and surpasses the given scariness, self-preserve (scary_target_cover_impulse)", null)]
		public float ScaryTargetThreshold17;
	}
}
#pragma warning restore CS1591
