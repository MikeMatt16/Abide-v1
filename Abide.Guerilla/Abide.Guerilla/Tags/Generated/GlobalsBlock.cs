using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("globals", "matg", "����", typeof(GlobalsBlock))]
	[FieldSet(760, 4)]
	public unsafe struct GlobalsBlock
	{
		public enum Language1Options
		{
			English_0 = 0,
			Japanese_1 = 1,
			German_2 = 2,
			French_3 = 3,
			Spanish_4 = 4,
			Italian_5 = 5,
			Korean_6 = 6,
			Chinese_7 = 7,
			Portuguese_8 = 8,
		}
		[Field("", null)]
		public fixed byte _0[172];
		[Field("language", typeof(Language1Options))]
		public int Language1;
		[Field("havok cleanup resources", null)]
		[Block("Havok Cleanup Resources Block", 1, typeof(HavokCleanupResourcesBlock))]
		public TagBlock HavokCleanupResources2;
		[Field("collision damage", null)]
		[Block("Collision Damage Block", 1, typeof(CollisionDamageBlock))]
		public TagBlock CollisionDamage3;
		[Field("sound globals", null)]
		[Block("Sound Globals Block", 1, typeof(SoundGlobalsBlock))]
		public TagBlock SoundGlobals4;
		[Field("ai globals", null)]
		[Block("Ai Globals Block", 1, typeof(AiGlobalsBlock))]
		public TagBlock AiGlobals5;
		[Field("damage table", null)]
		[Block("Game Globals Damage Block", 1, typeof(GameGlobalsDamageBlock))]
		public TagBlock DamageTable6;
		[Field("", null)]
		[Block("G Null Block", 0, typeof(GNullBlock))]
		public TagBlock _7;
		[Field("sounds", null)]
		[Block("Sound Block", 2, typeof(SoundBlock))]
		public TagBlock Sounds8;
		[Field("camera", null)]
		[Block("Camera Block", 1, typeof(CameraBlock))]
		public TagBlock Camera9;
		[Field("player control", null)]
		[Block("Player Control Block", 1, typeof(PlayerControlBlock))]
		public TagBlock PlayerControl10;
		[Field("difficulty", null)]
		[Block("Difficulty Block", 1, typeof(DifficultyBlock))]
		public TagBlock Difficulty11;
		[Field("grenades", null)]
		[Block("Grenades Block", 2, typeof(GrenadesBlock))]
		public TagBlock Grenades12;
		[Field("rasterizer data", null)]
		[Block("Rasterizer Data Block", 1, typeof(RasterizerDataBlock))]
		public TagBlock RasterizerData13;
		[Field("interface tags", null)]
		[Block("Interface Tag References", 1, typeof(InterfaceTagReferences))]
		public TagBlock InterfaceTags14;
		[Field("@weapon list (update _weapon_list enum in game_globals.h)", null)]
		[Block("Cheat Weapons Block", 20, typeof(CheatWeaponsBlock))]
		public TagBlock WeaponListUpdateWeaponListEnumInGameGlobalsH15;
		[Field("@cheat powerups", null)]
		[Block("Cheat Powerups Block", 20, typeof(CheatPowerupsBlock))]
		public TagBlock CheatPowerups16;
		[Field("@multiplayer information", null)]
		[Block("Multiplayer Information Block", 1, typeof(MultiplayerInformationBlock))]
		public TagBlock MultiplayerInformation17;
		[Field("@player information", null)]
		[Block("Player Information Block", 1, typeof(PlayerInformationBlock))]
		public TagBlock PlayerInformation18;
		[Field("@player representation", null)]
		[Block("Player Representation Block", 4, typeof(PlayerRepresentationBlock))]
		public TagBlock PlayerRepresentation19;
		[Field("falling damage", null)]
		[Block("Falling Damage Block", 1, typeof(FallingDamageBlock))]
		public TagBlock FallingDamage20;
		[Field("old materials", null)]
		[Block("Old Materials Block", 33, typeof(OldMaterialsBlock))]
		public TagBlock OldMaterials21;
		[Field("materials", null)]
		[Block("Materials Block", 256, typeof(MaterialsBlock))]
		public TagBlock Materials22;
		[Field("multiplayer UI", null)]
		[Block("Multiplayer Ui Block", 1, typeof(MultiplayerUiBlock))]
		public TagBlock MultiplayerUI23;
		[Field("profile colors", null)]
		[Block("Multiplayer Color Block", 32, typeof(MultiplayerColorBlock))]
		public TagBlock ProfileColors24;
		[Field("multiplayer globals", null)]
		public TagReference MultiplayerGlobals25;
		[Field("runtime level data", null)]
		[Block("Runtime Levels Definition Block", 1, typeof(RuntimeLevelsDefinitionBlock))]
		public TagBlock RuntimeLevelData26;
		[Field("ui level data", null)]
		[Block("Ui Levels Definition Block", 1, typeof(UiLevelsDefinitionBlock))]
		public TagBlock UiLevelData27;
		[Field("default global lighting", null)]
		public TagReference DefaultGlobalLighting29;
		[Field("", null)]
		public fixed byte _30[252];
	}
}
#pragma warning restore CS1591
