using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct CharacterSwarmBlock
	{
		[Field("", null)]
		public fixed byte _0[48];
		[Field("scatter killed count#After the given number of deaths, the swarm scatters", null)]
		public short ScatterKilledCount1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("scatter radius#the distance from the target that the swarm scatters", null)]
		public float ScatterRadius3;
		[Field("scatter time#amount of time to remain scattered", null)]
		public float ScatterTime4;
		[Field("", null)]
		public fixed byte _5[16];
		[Field("hound min distance", null)]
		public float HoundMinDistance6;
		[Field("hound max distance", null)]
		public float HoundMaxDistance7;
		[Field("", null)]
		public fixed byte _8[16];
		[Field("perlin offset scale:[0-1]#amount of randomness added to creature's throttle", null)]
		public float PerlinOffsetScale9;
		[Field("offset period:s#how fast the creature changes random offset to throttle", null)]
		public FloatBounds OffsetPeriod10;
		[Field("perlin idle movement threshold:[0-1]#a random offset lower then given threshold is made 0. (threshold of 1 = no movement)", null)]
		public float PerlinIdleMovementThreshold11;
		[Field("perlin combat movement threshold:[0-1]#a random offset lower then given threshold is made 0. (threshold of 1 = no movement)", null)]
		public float PerlinCombatMovementThreshold12;
	}
}
#pragma warning restore CS1591
