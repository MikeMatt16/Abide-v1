using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(232, 4)]
	public unsafe struct MultiplayerInformationBlock
	{
		[Field("flag", null)]
		public TagReference Flag0;
		[Field("unit", null)]
		public TagReference Unit1;
		[Field("vehicles", null)]
		[Block("Vehicles Block", 20, typeof(VehiclesBlock))]
		public TagBlock Vehicles2;
		[Field("hill shader", null)]
		public TagReference HillShader3;
		[Field("flag shader", null)]
		public TagReference FlagShader4;
		[Field("ball", null)]
		public TagReference Ball5;
		[Field("sounds", null)]
		[Block("Sounds Block", 60, typeof(SoundsBlock))]
		public TagBlock Sounds6;
		[Field("in game text", null)]
		public TagReference InGameText7;
		[Field("", null)]
		public fixed byte _8[40];
		[Field("general events", null)]
		[Block("Game Engine General Event Block", 128, typeof(GameEngineGeneralEventBlock))]
		public TagBlock GeneralEvents9;
		[Field("slayer events", null)]
		[Block("Game Engine Slayer Event Block", 128, typeof(GameEngineSlayerEventBlock))]
		public TagBlock SlayerEvents10;
		[Field("ctf events", null)]
		[Block("Game Engine Ctf Event Block", 128, typeof(GameEngineCtfEventBlock))]
		public TagBlock CtfEvents11;
		[Field("oddball events", null)]
		[Block("Game Engine Oddball Event Block", 128, typeof(GameEngineOddballEventBlock))]
		public TagBlock OddballEvents12;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _13;
		[Field("king events", null)]
		[Block("Game Engine King Event Block", 128, typeof(GameEngineKingEventBlock))]
		public TagBlock KingEvents14;
	}
}
#pragma warning restore CS1591
