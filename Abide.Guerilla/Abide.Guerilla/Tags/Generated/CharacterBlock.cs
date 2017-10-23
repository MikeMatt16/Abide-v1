using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("character", "char", "����", typeof(CharacterBlock))]
	[FieldSet(372, 4)]
	public unsafe struct CharacterBlock
	{
		public enum CharacterFlags0Options
		{
			Flag1_0 = 1,
		}
		[Field("Character flags", typeof(CharacterFlags0Options))]
		public int CharacterFlags0;
		[Field("", null)]
		public fixed byte _1[24];
		[Field("parent character", null)]
		public TagReference ParentCharacter2;
		[Field("unit", null)]
		public TagReference Unit3;
		[Field("creature#Creature reference for swarm characters ONLY", null)]
		public TagReference Creature4;
		[Field("style", null)]
		public TagReference Style5;
		[Field("", null)]
		public fixed byte _6[16];
		[Field("major character", null)]
		public TagReference MajorCharacter7;
		[Field("", null)]
		public fixed byte _8[12];
		[Field("variants", null)]
		[Block("Character Variants Block", 64, typeof(CharacterVariantsBlock))]
		public TagBlock Variants9;
		[Field("", null)]
		public fixed byte _10[36];
		[Field("general properties", null)]
		[Block("Character General Block", 1, typeof(CharacterGeneralBlock))]
		public TagBlock GeneralProperties11;
		[Field("vitality properties", null)]
		[Block("Character Vitality Block", 1, typeof(CharacterVitalityBlock))]
		public TagBlock VitalityProperties12;
		[Field("placement properties", null)]
		[Block("Character Placement Block", 1, typeof(CharacterPlacementBlock))]
		public TagBlock PlacementProperties13;
		[Field("perception properties", null)]
		[Block("Character Perception Block", 4, typeof(CharacterPerceptionBlock))]
		public TagBlock PerceptionProperties14;
		[Field("look properties", null)]
		[Block("Character Look Block", 1, typeof(CharacterLookBlock))]
		public TagBlock LookProperties15;
		[Field("movement properties", null)]
		[Block("Character Movement Block", 1, typeof(CharacterMovementBlock))]
		public TagBlock MovementProperties16;
		[Field("swarm properties", null)]
		[Block("Character Swarm Block", 3, typeof(CharacterSwarmBlock))]
		public TagBlock SwarmProperties17;
		[Field("", null)]
		public fixed byte _18[36];
		[Field("ready properties", null)]
		[Block("Character Ready Block", 3, typeof(CharacterReadyBlock))]
		public TagBlock ReadyProperties19;
		[Field("engage properties", null)]
		[Block("Character Engage Block", 3, typeof(CharacterEngageBlock))]
		public TagBlock EngageProperties20;
		[Field("charge properties", null)]
		[Block("Character Charge Block", 3, typeof(CharacterChargeBlock))]
		public TagBlock ChargeProperties21;
		[Field("evasion properties", null)]
		[Block("Character Evasion Block", 3, typeof(CharacterEvasionBlock))]
		public TagBlock EvasionProperties23;
		[Field("cover properties", null)]
		[Block("Character Cover Block", 3, typeof(CharacterCoverBlock))]
		public TagBlock CoverProperties24;
		[Field("retreat properties", null)]
		[Block("Character Retreat Block", 3, typeof(CharacterRetreatBlock))]
		public TagBlock RetreatProperties25;
		[Field("search properties", null)]
		[Block("Character Search Block", 3, typeof(CharacterSearchBlock))]
		public TagBlock SearchProperties26;
		[Field("pre-search properties", null)]
		[Block("Character Presearch Block", 3, typeof(CharacterPresearchBlock))]
		public TagBlock PreSearchProperties27;
		[Field("idle properties", null)]
		[Block("Character Idle Block", 3, typeof(CharacterIdleBlock))]
		public TagBlock IdleProperties28;
		[Field("vocalization properties", null)]
		[Block("Character Vocalization Block", 1, typeof(CharacterVocalizationBlock))]
		public TagBlock VocalizationProperties29;
		[Field("boarding properties", null)]
		[Block("Character Boarding Block", 1, typeof(CharacterBoardingBlock))]
		public TagBlock BoardingProperties30;
		[Field("", null)]
		public fixed byte _31[12];
		[Field("boss properties", null)]
		[Block("Character Boss Block", 1, typeof(CharacterBossBlock))]
		public TagBlock BossProperties32;
		[Field("weapons properties", null)]
		[Block("Character Weapons Block", 100, typeof(CharacterWeaponsBlock))]
		public TagBlock WeaponsProperties33;
		[Field("firing pattern properties", null)]
		[Block("Character Firing Pattern Properties Block", 100, typeof(CharacterFiringPatternPropertiesBlock))]
		public TagBlock FiringPatternProperties34;
		[Field("", null)]
		public fixed byte _35[24];
		[Field("grenades properties", null)]
		[Block("Character Grenades Block", 10, typeof(CharacterGrenadesBlock))]
		public TagBlock GrenadesProperties36;
		[Field("", null)]
		public fixed byte _37[24];
		[Field("vehicle properties", null)]
		[Block("Character Vehicle Block", 100, typeof(CharacterVehicleBlock))]
		public TagBlock VehicleProperties38;
	}
}
#pragma warning restore CS1591
