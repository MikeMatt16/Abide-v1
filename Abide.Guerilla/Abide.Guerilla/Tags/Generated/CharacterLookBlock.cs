using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(80, 4)]
	public unsafe struct CharacterLookBlock
	{
		[Field("maximum aiming deviation:degrees#how far we can turn our weapon", null)]
		public Vector2 MaximumAimingDeviation0;
		[Field("maximum looking deviation:degrees#how far we can turn our head", null)]
		public Vector2 MaximumLookingDeviation1;
		[Field("", null)]
		public fixed byte _2[16];
		[Field("noncombat look delta L:degrees#how far we can turn our head left away from our aiming vector when not in combat", null)]
		public float NoncombatLookDeltaL3;
		[Field("noncombat look delta R:degrees#how far we can turn our head right away from our aiming vector when not in combat", null)]
		public float NoncombatLookDeltaR4;
		[Field("combat look delta L:degrees#how far we can turn our head left away from our aiming vector when in combat", null)]
		public float CombatLookDeltaL5;
		[Field("combat look delta R:degrees#how far we can turn our head right away from our aiming vector when in combat", null)]
		public float CombatLookDeltaR6;
		[Field("noncombat idle looking:seconds#rate at which we change look around randomly when not in combat", null)]
		public FloatBounds NoncombatIdleLooking7;
		[Field("noncombat idle aiming:seconds#rate at which we change aiming directions when looking around randomly when not in combat", null)]
		public FloatBounds NoncombatIdleAiming8;
		[Field("combat idle looking:seconds#rate at which we change look around randomly when searching or in combat", null)]
		public FloatBounds CombatIdleLooking9;
		[Field("combat idle aiming:seconds#rate at which we change aiming directions when looking around randomly when searching or in combat", null)]
		public FloatBounds CombatIdleAiming10;
	}
}
#pragma warning restore CS1591
