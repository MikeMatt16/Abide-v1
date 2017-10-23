using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct CharacterFiringPatternBlock
	{
		[Field("rate of fire#how many times per second we pull the trigger (zero = continuously held down)", null)]
		public float RateOfFire0;
		[Field("target tracking:[0,1]#how well our bursts track moving targets. 0.0= fire at the position they were standing when we started the burst. 1.0= fire at current position", null)]
		public float TargetTracking1;
		[Field("target leading:[0,1]#how much we lead moving targets. 0.0= no prediction. 1.0= predict completely.", null)]
		public float TargetLeading2;
		[Field("burst origin radius:world units#how far away from the target the starting point is", null)]
		public float BurstOriginRadius4;
		[Field("burst origin angle:degrees#the range from the horizontal that our starting error can be", null)]
		public float BurstOriginAngle5;
		[Field("burst return length:world units#how far the burst point moves back towards the target (could be negative)", null)]
		public FloatBounds BurstReturnLength6;
		[Field("burst return angle:degrees#the range from the horizontal that the return direction can be", null)]
		public float BurstReturnAngle7;
		[Field("burst duration:seconds#how long each burst we fire is", null)]
		public FloatBounds BurstDuration8;
		[Field("burst separation:seconds#how long we wait between bursts", null)]
		public FloatBounds BurstSeparation9;
		[Field("weapon damage modifier#what fraction of its normal damage our weapon inflicts (zero = no modifier)", null)]
		public float WeaponDamageModifier10;
		[Field("projectile error:degrees#error added to every projectile we fire", null)]
		public float ProjectileError11;
		[Field("", null)]
		public fixed byte _12[12];
		[Field("burst angular velocity:degrees per second#the maximum rate at which we can sweep our fire (zero = unlimited)", null)]
		public float BurstAngularVelocity13;
		[Field("maximum error angle:degrees#cap on the maximum angle by which we will miss target (restriction on burst origin radius", null)]
		public float MaximumErrorAngle14;
	}
}
#pragma warning restore CS1591
