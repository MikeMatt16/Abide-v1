using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct CharacterEngageBlock
	{
		public enum Flags0Options
		{
			EngagePerch_0 = 1,
			FightConstantMovement_1 = 2,
			FlightFightConstantMovement_2 = 4,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("", null)]
		public fixed byte _1[16];
		[Field("Crouch danger threshold#When danger rises above the threshold, the actor crouches", null)]
		public float CrouchDangerThreshold2;
		[Field("Stand danger threshold#When danger drops below this threshold, the actor can stand again.", null)]
		public float StandDangerThreshold3;
		[Field("Fight danger move threshold#When danger goes above given level, this actor switches firing positions", null)]
		public float FightDangerMoveThreshold4;
	}
}
#pragma warning restore CS1591
