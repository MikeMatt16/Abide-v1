using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(372, 4)]
	public unsafe struct AiGlobalsBlock
	{
		[Field("danger broadly facing", null)]
		public float DangerBroadlyFacing0;
		[Field("", null)]
		public fixed byte _1[4];
		[Field("danger shooting near", null)]
		public float DangerShootingNear2;
		[Field("", null)]
		public fixed byte _3[4];
		[Field("danger shooting at", null)]
		public float DangerShootingAt4;
		[Field("", null)]
		public fixed byte _5[4];
		[Field("danger extremely close", null)]
		public float DangerExtremelyClose6;
		[Field("", null)]
		public fixed byte _7[4];
		[Field("danger shield damage", null)]
		public float DangerShieldDamage8;
		[Field("danger exetended shield damage", null)]
		public float DangerExetendedShieldDamage9;
		[Field("danger body damage", null)]
		public float DangerBodyDamage10;
		[Field("danger extended body damage", null)]
		public float DangerExtendedBodyDamage11;
		[Field("", null)]
		public fixed byte _12[48];
		[Field("global dialogue tag", null)]
		public TagReference GlobalDialogueTag13;
		[Field("default mission dialogue sound effect", null)]
		public StringId DefaultMissionDialogueSoundEffect14;
		[Field("", null)]
		public fixed byte _15[20];
		[Field("jump down:wu/tick", null)]
		public float JumpDown16;
		[Field("jump step:wu/tick", null)]
		public float JumpStep17;
		[Field("jump crouch:wu/tick", null)]
		public float JumpCrouch18;
		[Field("jump stand:wu/tick", null)]
		public float JumpStand19;
		[Field("jump storey:wu/tick", null)]
		public float JumpStorey20;
		[Field("jump tower:wu/tick", null)]
		public float JumpTower21;
		[Field("max jump down height down:wu", null)]
		public float MaxJumpDownHeightDown22;
		[Field("max jump down height step:wu", null)]
		public float MaxJumpDownHeightStep23;
		[Field("max jump down height crouch:wu", null)]
		public float MaxJumpDownHeightCrouch24;
		[Field("max jump down height stand:wu", null)]
		public float MaxJumpDownHeightStand25;
		[Field("max jump down height storey:wu", null)]
		public float MaxJumpDownHeightStorey26;
		[Field("max jump down height tower:wu", null)]
		public float MaxJumpDownHeightTower27;
		[Field("hoist step:wus", null)]
		public FloatBounds HoistStep28;
		[Field("hoist crouch:wus", null)]
		public FloatBounds HoistCrouch29;
		[Field("hoist stand:wus", null)]
		public FloatBounds HoistStand30;
		[Field("", null)]
		public fixed byte _31[24];
		[Field("vault step:wus", null)]
		public FloatBounds VaultStep32;
		[Field("vault crouch:wus", null)]
		public FloatBounds VaultCrouch33;
		[Field("", null)]
		public fixed byte _34[48];
		[Field("gravemind properties", null)]
		[Block("Ai Globals Gravemind Block", 1, typeof(AiGlobalsGravemindBlock))]
		public TagBlock GravemindProperties35;
		[Field("", null)]
		public fixed byte _36[48];
		[Field("scary target threhold#A target of this scariness is offically considered scary (by combat dialogue, etc.)", null)]
		public float ScaryTargetThrehold37;
		[Field("scary weapon threhold#A weapon of this scariness is offically considered scary (by combat dialogue, etc.)", null)]
		public float ScaryWeaponThrehold38;
		[Field("player scariness", null)]
		public float PlayerScariness39;
		[Field("berserking actor scariness", null)]
		public float BerserkingActorScariness40;
	}
}
#pragma warning restore CS1591
