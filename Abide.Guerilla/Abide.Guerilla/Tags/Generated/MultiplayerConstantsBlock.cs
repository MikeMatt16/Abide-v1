using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(384, 4)]
	public unsafe struct MultiplayerConstantsBlock
	{
		[Field("maximum random spawn bias", null)]
		public float MaximumRandomSpawnBias0;
		[Field("teleporter recharge time:seconds", null)]
		public float TeleporterRechargeTime1;
		[Field("grenade danger weight", null)]
		public float GrenadeDangerWeight2;
		[Field("grenade danger inner radius", null)]
		public float GrenadeDangerInnerRadius3;
		[Field("grenade danger outer radius", null)]
		public float GrenadeDangerOuterRadius4;
		[Field("grenade danger lead time:seconds", null)]
		public float GrenadeDangerLeadTime5;
		[Field("vehicle danger min speed:wu/sec", null)]
		public float VehicleDangerMinSpeed6;
		[Field("vehicle danger weight", null)]
		public float VehicleDangerWeight7;
		[Field("vehicle danger radius", null)]
		public float VehicleDangerRadius8;
		[Field("vehicle danger lead time:seconds", null)]
		public float VehicleDangerLeadTime9;
		[Field("vehicle nearby player dist#how nearby a player is to count a vehicle as 'occupied'", null)]
		public float VehicleNearbyPlayerDist10;
		[Field("", null)]
		public fixed byte _11[84];
		[Field("", null)]
		public fixed byte _12[32];
		[Field("", null)]
		public fixed byte _13[32];
		[Field("hill shader", null)]
		public TagReference HillShader14;
		[Field("", null)]
		public fixed byte _15[16];
		[Field("flag reset stop distance", null)]
		public float FlagResetStopDistance16;
		[Field("bomb explode effect", null)]
		public TagReference BombExplodeEffect17;
		[Field("bomb explode dmg effect", null)]
		public TagReference BombExplodeDmgEffect18;
		[Field("bomb defuse effect", null)]
		public TagReference BombDefuseEffect19;
		[Field("bomb defusal string", null)]
		public StringId BombDefusalString20;
		[Field("blocked teleporter string", null)]
		public StringId BlockedTeleporterString21;
		[Field("", null)]
		public fixed byte _22[4];
		[Field("", null)]
		public fixed byte _23[32];
		[Field("", null)]
		public fixed byte _24[32];
		[Field("", null)]
		public fixed byte _25[32];
	}
}
#pragma warning restore CS1591
