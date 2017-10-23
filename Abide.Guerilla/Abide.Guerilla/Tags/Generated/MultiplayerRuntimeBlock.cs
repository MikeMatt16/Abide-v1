using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(1640, 4)]
	public unsafe struct MultiplayerRuntimeBlock
	{
		[Field("flag", null)]
		public TagReference Flag0;
		[Field("ball", null)]
		public TagReference Ball1;
		[Field("unit", null)]
		public TagReference Unit2;
		[Field("flag shader", null)]
		public TagReference FlagShader3;
		[Field("hill shader", null)]
		public TagReference HillShader4;
		[Field("head", null)]
		public TagReference Head5;
		[Field("juggernaut powerup", null)]
		public TagReference JuggernautPowerup6;
		[Field("da bomb", null)]
		public TagReference DaBomb7;
		[Field("", null)]
		public TagReference _8;
		[Field("", null)]
		public TagReference _9;
		[Field("", null)]
		public TagReference _10;
		[Field("", null)]
		public TagReference _11;
		[Field("", null)]
		public TagReference _12;
		[Field("weapons", null)]
		[Block("Weapons Block", 20, typeof(WeaponsBlock))]
		public TagBlock Weapons13;
		[Field("vehicles", null)]
		[Block("Vehicles Block", 20, typeof(VehiclesBlock))]
		public TagBlock Vehicles14;
		[Field("arr!", typeof(GrenadeAndPowerupStructBlock))]
		[Block("Grenade And Powerup Struct", 1, typeof(GrenadeAndPowerupStructBlock))]
		public GrenadeAndPowerupStructBlock Arr15;
		[Field("in game text", null)]
		public TagReference InGameText16;
		[Field("sounds", null)]
		[Block("Sounds Block", 60, typeof(SoundsBlock))]
		public TagBlock Sounds17;
		[Field("general events", null)]
		[Block("Game Engine General Event Block", 128, typeof(GameEngineGeneralEventBlock))]
		public TagBlock GeneralEvents18;
		[Field("flavor events", null)]
		[Block("Game Engine Flavor Event Block", 128, typeof(GameEngineFlavorEventBlock))]
		public TagBlock FlavorEvents19;
		[Field("slayer events", null)]
		[Block("Game Engine Slayer Event Block", 128, typeof(GameEngineSlayerEventBlock))]
		public TagBlock SlayerEvents20;
		[Field("ctf events", null)]
		[Block("Game Engine Ctf Event Block", 128, typeof(GameEngineCtfEventBlock))]
		public TagBlock CtfEvents21;
		[Field("oddball events", null)]
		[Block("Game Engine Oddball Event Block", 128, typeof(GameEngineOddballEventBlock))]
		public TagBlock OddballEvents22;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _23;
		[Field("king events", null)]
		[Block("Game Engine King Event Block", 128, typeof(GameEngineKingEventBlock))]
		public TagBlock KingEvents24;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _25;
		[Field("juggernaut events", null)]
		[Block("Game Engine Juggernaut Event Block", 128, typeof(GameEngineJuggernautEventBlock))]
		public TagBlock JuggernautEvents26;
		[Field("territories events", null)]
		[Block("Game Engine Territories Event Block", 128, typeof(GameEngineTerritoriesEventBlock))]
		public TagBlock TerritoriesEvents27;
		[Field("invasion events", null)]
		[Block("Game Engine Assault Event Block", 128, typeof(GameEngineAssaultEventBlock))]
		public TagBlock InvasionEvents28;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _29;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _30;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _31;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _32;
		[Field("default item collection 1", null)]
		public TagReference DefaultItemCollection133;
		[Field("default item collection 2", null)]
		public TagReference DefaultItemCollection234;
		[Field("default frag grenade count", null)]
		public int DefaultFragGrenadeCount35;
		[Field("default plasma grenade count", null)]
		public int DefaultPlasmaGrenadeCount36;
		[Field("", null)]
		public fixed byte _37[40];
		[Field("dynamic zone upper height", null)]
		public float DynamicZoneUpperHeight39;
		[Field("dynamic zone lower height", null)]
		public float DynamicZoneLowerHeight40;
		[Field("", null)]
		public fixed byte _41[40];
		[Field("enemy inner radius", null)]
		public float EnemyInnerRadius43;
		[Field("enemy outer radius", null)]
		public float EnemyOuterRadius44;
		[Field("enemy weight", null)]
		public float EnemyWeight45;
		[Field("", null)]
		public fixed byte _46[16];
		[Field("friend inner radius", null)]
		public float FriendInnerRadius48;
		[Field("friend outer radius", null)]
		public float FriendOuterRadius49;
		[Field("friend weight", null)]
		public float FriendWeight50;
		[Field("", null)]
		public fixed byte _51[16];
		[Field("enemy vehicle inner radius", null)]
		public float EnemyVehicleInnerRadius53;
		[Field("enemy vehicle outer radius", null)]
		public float EnemyVehicleOuterRadius54;
		[Field("enemy vehicle weight", null)]
		public float EnemyVehicleWeight55;
		[Field("", null)]
		public fixed byte _56[16];
		[Field("friendly vehicle inner radius", null)]
		public float FriendlyVehicleInnerRadius58;
		[Field("friendly vehicle outer radius", null)]
		public float FriendlyVehicleOuterRadius59;
		[Field("friendly vehicle weight", null)]
		public float FriendlyVehicleWeight60;
		[Field("", null)]
		public fixed byte _61[16];
		[Field("empty vehicle inner radius", null)]
		public float EmptyVehicleInnerRadius63;
		[Field("empty vehicle outer radius", null)]
		public float EmptyVehicleOuterRadius64;
		[Field("empty vehicle weight", null)]
		public float EmptyVehicleWeight65;
		[Field("", null)]
		public fixed byte _66[16];
		[Field("oddball inclusion inner radius", null)]
		public float OddballInclusionInnerRadius68;
		[Field("oddball inclusion outer radius", null)]
		public float OddballInclusionOuterRadius69;
		[Field("oddball inclusion weight", null)]
		public float OddballInclusionWeight70;
		[Field("", null)]
		public fixed byte _71[16];
		[Field("oddball exclusion inner radius", null)]
		public float OddballExclusionInnerRadius73;
		[Field("oddball exclusion outer radius", null)]
		public float OddballExclusionOuterRadius74;
		[Field("oddball exclusion weight", null)]
		public float OddballExclusionWeight75;
		[Field("", null)]
		public fixed byte _76[16];
		[Field("hill inclusion inner radius", null)]
		public float HillInclusionInnerRadius78;
		[Field("hill inclusion outer radius", null)]
		public float HillInclusionOuterRadius79;
		[Field("hill inclusion weight", null)]
		public float HillInclusionWeight80;
		[Field("", null)]
		public fixed byte _81[16];
		[Field("hill exclusion inner radius", null)]
		public float HillExclusionInnerRadius83;
		[Field("hill exclusion outer radius", null)]
		public float HillExclusionOuterRadius84;
		[Field("hill exclusion weight", null)]
		public float HillExclusionWeight85;
		[Field("", null)]
		public fixed byte _86[16];
		[Field("last race flag inner radius", null)]
		public float LastRaceFlagInnerRadius88;
		[Field("last race flag outer radius", null)]
		public float LastRaceFlagOuterRadius89;
		[Field("last race flag weight", null)]
		public float LastRaceFlagWeight90;
		[Field("", null)]
		public fixed byte _91[16];
		[Field("dead ally inner radius", null)]
		public float DeadAllyInnerRadius93;
		[Field("dead ally outer radius", null)]
		public float DeadAllyOuterRadius94;
		[Field("dead ally weight", null)]
		public float DeadAllyWeight95;
		[Field("", null)]
		public fixed byte _96[16];
		[Field("controlled territory inner radius", null)]
		public float ControlledTerritoryInnerRadius98;
		[Field("controlled territory outer radius", null)]
		public float ControlledTerritoryOuterRadius99;
		[Field("controlled territory weight", null)]
		public float ControlledTerritoryWeight100;
		[Field("", null)]
		public fixed byte _101[16];
		[Field("", null)]
		public fixed byte _102[560];
		[Field("", null)]
		public fixed byte _103[48];
		[Field("multiplayer constants", null)]
		[Block("Multiplayer Constants Block", 1, typeof(MultiplayerConstantsBlock))]
		public TagBlock MultiplayerConstants104;
		[Field("state responses", null)]
		[Block("Game Engine Status Response Block", 32, typeof(GameEngineStatusResponseBlock))]
		public TagBlock StateResponses105;
		[Field("scoreboard hud definition", null)]
		public TagReference ScoreboardHudDefinition106;
		[Field("scoreboard emblem shader", null)]
		public TagReference ScoreboardEmblemShader107;
		[Field("scoreboard emblem bitmap", null)]
		public TagReference ScoreboardEmblemBitmap108;
		[Field("scoreboard dead emblem shader", null)]
		public TagReference ScoreboardDeadEmblemShader109;
		[Field("scoreboard dead emblem bitmap", null)]
		public TagReference ScoreboardDeadEmblemBitmap110;
	}
}
#pragma warning restore CS1591
