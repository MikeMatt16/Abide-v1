using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(112, 4)]
	public unsafe struct ProjectileMaterialResponseBlock
	{
		public enum Flags0Options
		{
			CannotBeOverpenetrated_0 = 1,
		}
		public enum Response2Options
		{
			ImpactDetonate_0 = 0,
			Fizzle_1 = 1,
			Overpenetrate_2 = 2,
			Attach_3 = 3,
			Bounce_4 = 4,
			BounceDud_5 = 5,
			FizzleRicochet_6 = 6,
		}
		public enum Response8Options
		{
			ImpactDetonate_0 = 0,
			Fizzle_1 = 1,
			Overpenetrate_2 = 2,
			Attach_3 = 3,
			Bounce_4 = 4,
			BounceDud_5 = 5,
			FizzleRicochet_6 = 6,
		}
		public enum Flags9Options
		{
			OnlyAgainstUnits_0 = 1,
			NeverAgainstUnits_1 = 2,
		}
		public enum ScaleEffectsBy16Options
		{
			Damage_0 = 0,
			Angle_1 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("response", typeof(Response2Options))]
		public short Response2;
		[Field("DO NOT USE (OLD effect)", null)]
		public TagReference DONOTUSEOLDEffect3;
		[Field("material name", null)]
		public StringId MaterialName4;
		[Field("", null)]
		public fixed byte _5[4];
		[Field("", null)]
		public fixed byte _6[8];
		[Field("response", typeof(Response8Options))]
		public short Response8;
		[Field("flags", typeof(Flags9Options))]
		public short Flags9;
		[Field("chance fraction:[0,1]", null)]
		public float ChanceFraction10;
		[Field("between:degrees", null)]
		public FloatBounds Between11;
		[Field("and:world units per second", null)]
		public FloatBounds And12;
		[Field("DO NOT USE (OLD effect)", null)]
		public TagReference DONOTUSEOLDEffect13;
		[Field("", null)]
		public fixed byte _14[16];
		[Field("scale effects by", typeof(ScaleEffectsBy16Options))]
		public short ScaleEffectsBy16;
		[Field("", null)]
		public fixed byte _17[2];
		[Field("angular noise:degrees#the angle of incidence is randomly perturbed by at most this amount to simulate irregularity.", null)]
		public float AngularNoise18;
		[Field("velocity noise:world units per second#the velocity is randomly perturbed by at most this amount to simulate irregularity.", null)]
		public float VelocityNoise19;
		[Field("DO NOT USE (OLD detonation effect)", null)]
		public TagReference DONOTUSEOLDDetonationEffect20;
		[Field("", null)]
		public fixed byte _21[24];
		[Field("initial friction#the fraction of the projectile's velocity lost on penetration", null)]
		public float InitialFriction23;
		[Field("maximum distance#the maximum distance the projectile can travel through on object of this material", null)]
		public float MaximumDistance24;
		[Field("parallel friction#the fraction of the projectile's velocity parallel to the surface lost on impact", null)]
		public float ParallelFriction26;
		[Field("perpendicular friction#the fraction of the projectile's velocity perpendicular to the surface lost on impact", null)]
		public float PerpendicularFriction27;
	}
}
#pragma warning restore CS1591
