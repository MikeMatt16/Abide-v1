using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct DynamicSpawnZoneOverloadBlock
	{
		public enum OverloadType0Options
		{
			Enemy_0 = 0,
			Friend_1 = 1,
			EnemyVehicle_2 = 2,
			FriendlyVehicle_3 = 3,
			EmptyVehicle_4 = 4,
			OddballInclusion_5 = 5,
			OddballExclusion_6 = 6,
			HillInclusion_7 = 7,
			HillExclusion_8 = 8,
			LastRaceFlag_9 = 9,
			DeadAlly_10 = 10,
			ControlledTerritory_11 = 11,
		}
		[Field("Overload Type", typeof(OverloadType0Options))]
		public short OverloadType0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("Inner Radius", null)]
		public float InnerRadius2;
		[Field("Outer Radius", null)]
		public float OuterRadius3;
		[Field("Weight", null)]
		public float Weight4;
	}
}
#pragma warning restore CS1591
