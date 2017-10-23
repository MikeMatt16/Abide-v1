using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(644, 4)]
	public unsafe struct DifficultyBlock
	{
		[Field("easy enemy damage#enemy damage multiplier on easy difficulty", null)]
		public float EasyEnemyDamage1;
		[Field("normal enemy damage#enemy damage multiplier on normal difficulty", null)]
		public float NormalEnemyDamage2;
		[Field("hard enemy damage#enemy damage multiplier on hard difficulty", null)]
		public float HardEnemyDamage3;
		[Field("imposs. enemy damage#enemy damage multiplier on impossible difficulty", null)]
		public float ImpossEnemyDamage4;
		[Field("easy enemy vitality#enemy maximum body vitality scale on easy difficulty", null)]
		public float EasyEnemyVitality5;
		[Field("normal enemy vitality#enemy maximum body vitality scale on normal difficulty", null)]
		public float NormalEnemyVitality6;
		[Field("hard enemy vitality#enemy maximum body vitality scale on hard difficulty", null)]
		public float HardEnemyVitality7;
		[Field("imposs. enemy vitality#enemy maximum body vitality scale on impossible difficulty", null)]
		public float ImpossEnemyVitality8;
		[Field("easy enemy shield#enemy maximum shield vitality scale on easy difficulty", null)]
		public float EasyEnemyShield9;
		[Field("normal enemy shield#enemy maximum shield vitality scale on normal difficulty", null)]
		public float NormalEnemyShield10;
		[Field("hard enemy shield#enemy maximum shield vitality scale on hard difficulty", null)]
		public float HardEnemyShield11;
		[Field("imposs. enemy shield#enemy maximum shield vitality scale on impossible difficulty", null)]
		public float ImpossEnemyShield12;
		[Field("easy enemy recharge#enemy shield recharge scale on easy difficulty", null)]
		public float EasyEnemyRecharge13;
		[Field("normal enemy recharge#enemy shield recharge scale on normal difficulty", null)]
		public float NormalEnemyRecharge14;
		[Field("hard enemy recharge#enemy shield recharge scale on hard difficulty", null)]
		public float HardEnemyRecharge15;
		[Field("imposs. enemy recharge#enemy shield recharge scale on impossible difficulty", null)]
		public float ImpossEnemyRecharge16;
		[Field("easy friend damage#friend damage multiplier on easy difficulty", null)]
		public float EasyFriendDamage17;
		[Field("normal friend damage#friend damage multiplier on normal difficulty", null)]
		public float NormalFriendDamage18;
		[Field("hard friend damage#friend damage multiplier on hard difficulty", null)]
		public float HardFriendDamage19;
		[Field("imposs. friend damage#friend damage multiplier on impossible difficulty", null)]
		public float ImpossFriendDamage20;
		[Field("easy friend vitality#friend maximum body vitality scale on easy difficulty", null)]
		public float EasyFriendVitality21;
		[Field("normal friend vitality#friend maximum body vitality scale on normal difficulty", null)]
		public float NormalFriendVitality22;
		[Field("hard friend vitality#friend maximum body vitality scale on hard difficulty", null)]
		public float HardFriendVitality23;
		[Field("imposs. friend vitality#friend maximum body vitality scale on impossible difficulty", null)]
		public float ImpossFriendVitality24;
		[Field("easy friend shield#friend maximum shield vitality scale on easy difficulty", null)]
		public float EasyFriendShield25;
		[Field("normal friend shield#friend maximum shield vitality scale on normal difficulty", null)]
		public float NormalFriendShield26;
		[Field("hard friend shield#friend maximum shield vitality scale on hard difficulty", null)]
		public float HardFriendShield27;
		[Field("imposs. friend shield#friend maximum shield vitality scale on impossible difficulty", null)]
		public float ImpossFriendShield28;
		[Field("easy friend recharge#friend shield recharge scale on easy difficulty", null)]
		public float EasyFriendRecharge29;
		[Field("normal friend recharge#friend shield recharge scale on normal difficulty", null)]
		public float NormalFriendRecharge30;
		[Field("hard friend recharge#friend shield recharge scale on hard difficulty", null)]
		public float HardFriendRecharge31;
		[Field("imposs. friend recharge#friend shield recharge scale on impossible difficulty", null)]
		public float ImpossFriendRecharge32;
		[Field("easy infection forms#toughness of infection forms (may be negative) on easy difficulty", null)]
		public float EasyInfectionForms33;
		[Field("normal infection forms#toughness of infection forms (may be negative) on normal difficulty", null)]
		public float NormalInfectionForms34;
		[Field("hard infection forms#toughness of infection forms (may be negative) on hard difficulty", null)]
		public float HardInfectionForms35;
		[Field("imposs. infection forms#toughness of infection forms (may be negative) on impossible difficulty", null)]
		public float ImpossInfectionForms36;
		[Field("", null)]
		public fixed byte _37[16];
		[Field("easy rate of fire#enemy rate of fire scale on easy difficulty", null)]
		public float EasyRateOfFire39;
		[Field("normal rate of fire#enemy rate of fire scale on normal difficulty", null)]
		public float NormalRateOfFire40;
		[Field("hard rate of fire#enemy rate of fire scale on hard difficulty", null)]
		public float HardRateOfFire41;
		[Field("imposs. rate of fire#enemy rate of fire scale on impossible difficulty", null)]
		public float ImpossRateOfFire42;
		[Field("easy projectile error#enemy projectile error scale, as a fraction of their base firing error. on easy difficulty", null)]
		public float EasyProjectileError43;
		[Field("normal projectile error#enemy projectile error scale, as a fraction of their base firing error. on normal difficulty", null)]
		public float NormalProjectileError44;
		[Field("hard projectile error#enemy projectile error scale, as a fraction of their base firing error. on hard difficulty", null)]
		public float HardProjectileError45;
		[Field("imposs. projectile error#enemy projectile error scale, as a fraction of their base firing error. on impossible difficulty", null)]
		public float ImpossProjectileError46;
		[Field("easy burst error#enemy burst error scale; reduces intra-burst shot distance. on easy difficulty", null)]
		public float EasyBurstError47;
		[Field("normal burst error#enemy burst error scale; reduces intra-burst shot distance. on normal difficulty", null)]
		public float NormalBurstError48;
		[Field("hard burst error#enemy burst error scale; reduces intra-burst shot distance. on hard difficulty", null)]
		public float HardBurstError49;
		[Field("imposs. burst error#enemy burst error scale; reduces intra-burst shot distance. on impossible difficulty", null)]
		public float ImpossBurstError50;
		[Field("easy new target delay#enemy new-target delay scale factor. on easy difficulty", null)]
		public float EasyNewTargetDelay51;
		[Field("normal new target delay#enemy new-target delay scale factor. on normal difficulty", null)]
		public float NormalNewTargetDelay52;
		[Field("hard new target delay#enemy new-target delay scale factor. on hard difficulty", null)]
		public float HardNewTargetDelay53;
		[Field("imposs. new target delay#enemy new-target delay scale factor. on impossible difficulty", null)]
		public float ImpossNewTargetDelay54;
		[Field("easy burst separation#delay time between bursts scale factor for enemies. on easy difficulty", null)]
		public float EasyBurstSeparation55;
		[Field("normal burst separation#delay time between bursts scale factor for enemies. on normal difficulty", null)]
		public float NormalBurstSeparation56;
		[Field("hard burst separation#delay time between bursts scale factor for enemies. on hard difficulty", null)]
		public float HardBurstSeparation57;
		[Field("imposs. burst separation#delay time between bursts scale factor for enemies. on impossible difficulty", null)]
		public float ImpossBurstSeparation58;
		[Field("easy target tracking#additional target tracking fraction for enemies. on easy difficulty", null)]
		public float EasyTargetTracking59;
		[Field("normal target tracking#additional target tracking fraction for enemies. on normal difficulty", null)]
		public float NormalTargetTracking60;
		[Field("hard target tracking#additional target tracking fraction for enemies. on hard difficulty", null)]
		public float HardTargetTracking61;
		[Field("imposs. target tracking#additional target tracking fraction for enemies. on impossible difficulty", null)]
		public float ImpossTargetTracking62;
		[Field("easy target leading#additional target leading fraction for enemies. on easy difficulty", null)]
		public float EasyTargetLeading63;
		[Field("normal target leading#additional target leading fraction for enemies. on normal difficulty", null)]
		public float NormalTargetLeading64;
		[Field("hard target leading#additional target leading fraction for enemies. on hard difficulty", null)]
		public float HardTargetLeading65;
		[Field("imposs. target leading#additional target leading fraction for enemies. on impossible difficulty", null)]
		public float ImpossTargetLeading66;
		[Field("easy overcharge chance#overcharge chance scale factor for enemies. on easy difficulty", null)]
		public float EasyOverchargeChance67;
		[Field("normal overcharge chance#overcharge chance scale factor for enemies. on normal difficulty", null)]
		public float NormalOverchargeChance68;
		[Field("hard overcharge chance#overcharge chance scale factor for enemies. on hard difficulty", null)]
		public float HardOverchargeChance69;
		[Field("imposs. overcharge chance#overcharge chance scale factor for enemies. on impossible difficulty", null)]
		public float ImpossOverchargeChance70;
		[Field("easy special fire delay#delay between special-fire shots (overcharge, banshee bombs) scale factor for enemies. on easy difficulty", null)]
		public float EasySpecialFireDelay71;
		[Field("normal special fire delay#delay between special-fire shots (overcharge, banshee bombs) scale factor for enemies. on normal difficulty", null)]
		public float NormalSpecialFireDelay72;
		[Field("hard special fire delay#delay between special-fire shots (overcharge, banshee bombs) scale factor for enemies. on hard difficulty", null)]
		public float HardSpecialFireDelay73;
		[Field("imposs. special fire delay#delay between special-fire shots (overcharge, banshee bombs) scale factor for enemies. on impossible difficulty", null)]
		public float ImpossSpecialFireDelay74;
		[Field("easy guidance vs player#guidance velocity scale factor for all projectiles targeted on a player. on easy difficulty", null)]
		public float EasyGuidanceVsPlayer75;
		[Field("normal guidance vs player#guidance velocity scale factor for all projectiles targeted on a player. on normal difficulty", null)]
		public float NormalGuidanceVsPlayer76;
		[Field("hard guidance vs player#guidance velocity scale factor for all projectiles targeted on a player. on hard difficulty", null)]
		public float HardGuidanceVsPlayer77;
		[Field("imposs. guidance vs player#guidance velocity scale factor for all projectiles targeted on a player. on impossible difficulty", null)]
		public float ImpossGuidanceVsPlayer78;
		[Field("easy melee delay base#delay period added to all melee attacks, even when berserk. on easy difficulty", null)]
		public float EasyMeleeDelayBase79;
		[Field("normal melee delay base#delay period added to all melee attacks, even when berserk. on normal difficulty", null)]
		public float NormalMeleeDelayBase80;
		[Field("hard melee delay base#delay period added to all melee attacks, even when berserk. on hard difficulty", null)]
		public float HardMeleeDelayBase81;
		[Field("imposs. melee delay base#delay period added to all melee attacks, even when berserk. on impossible difficulty", null)]
		public float ImpossMeleeDelayBase82;
		[Field("easy melee delay scale#multiplier for all existing non-berserk melee delay times. on easy difficulty", null)]
		public float EasyMeleeDelayScale83;
		[Field("normal melee delay scale#multiplier for all existing non-berserk melee delay times. on normal difficulty", null)]
		public float NormalMeleeDelayScale84;
		[Field("hard melee delay scale#multiplier for all existing non-berserk melee delay times. on hard difficulty", null)]
		public float HardMeleeDelayScale85;
		[Field("imposs. melee delay scale#multiplier for all existing non-berserk melee delay times. on impossible difficulty", null)]
		public float ImpossMeleeDelayScale86;
		[Field("", null)]
		public fixed byte _87[16];
		[Field("easy grenade chance scale#scale factor affecting the desicions to throw a grenade. on easy difficulty", null)]
		public float EasyGrenadeChanceScale89;
		[Field("normal grenade chance scale#scale factor affecting the desicions to throw a grenade. on normal difficulty", null)]
		public float NormalGrenadeChanceScale90;
		[Field("hard grenade chance scale#scale factor affecting the desicions to throw a grenade. on hard difficulty", null)]
		public float HardGrenadeChanceScale91;
		[Field("imposs. grenade chance scale#scale factor affecting the desicions to throw a grenade. on impossible difficulty", null)]
		public float ImpossGrenadeChanceScale92;
		[Field("easy grenade timer scale#scale factor affecting the delay period between grenades thrown from the same encounter (lower is more often). on easy difficulty", null)]
		public float EasyGrenadeTimerScale93;
		[Field("normal grenade timer scale#scale factor affecting the delay period between grenades thrown from the same encounter (lower is more often). on normal difficulty", null)]
		public float NormalGrenadeTimerScale94;
		[Field("hard grenade timer scale#scale factor affecting the delay period between grenades thrown from the same encounter (lower is more often). on hard difficulty", null)]
		public float HardGrenadeTimerScale95;
		[Field("imposs. grenade timer scale#scale factor affecting the delay period between grenades thrown from the same encounter (lower is more often). on impossible difficulty", null)]
		public float ImpossGrenadeTimerScale96;
		[Field("", null)]
		public fixed byte _97[16];
		[Field("", null)]
		public fixed byte _98[16];
		[Field("", null)]
		public fixed byte _99[16];
		[Field("easy major upgrade (normal)#fraction of actors upgraded to their major variant. on easy difficulty", null)]
		public float EasyMajorUpgradeNormal101;
		[Field("normal major upgrade (normal)#fraction of actors upgraded to their major variant. on normal difficulty", null)]
		public float NormalMajorUpgradeNormal102;
		[Field("hard major upgrade (normal)#fraction of actors upgraded to their major variant. on hard difficulty", null)]
		public float HardMajorUpgradeNormal103;
		[Field("imposs. major upgrade (normal)#fraction of actors upgraded to their major variant. on impossible difficulty", null)]
		public float ImpossMajorUpgradeNormal104;
		[Field("easy major upgrade (few)#fraction of actors upgraded to their major variant when mix = normal. on easy difficulty", null)]
		public float EasyMajorUpgradeFew105;
		[Field("normal major upgrade (few)#fraction of actors upgraded to their major variant when mix = normal. on normal difficulty", null)]
		public float NormalMajorUpgradeFew106;
		[Field("hard major upgrade (few)#fraction of actors upgraded to their major variant when mix = normal. on hard difficulty", null)]
		public float HardMajorUpgradeFew107;
		[Field("imposs. major upgrade (few)#fraction of actors upgraded to their major variant when mix = normal. on impossible difficulty", null)]
		public float ImpossMajorUpgradeFew108;
		[Field("easy major upgrade (many)#fraction of actors upgraded to their major variant when mix = many. on easy difficulty", null)]
		public float EasyMajorUpgradeMany109;
		[Field("normal major upgrade (many)#fraction of actors upgraded to their major variant when mix = many. on normal difficulty", null)]
		public float NormalMajorUpgradeMany110;
		[Field("hard major upgrade (many)#fraction of actors upgraded to their major variant when mix = many. on hard difficulty", null)]
		public float HardMajorUpgradeMany111;
		[Field("imposs. major upgrade (many)#fraction of actors upgraded to their major variant when mix = many. on impossible difficulty", null)]
		public float ImpossMajorUpgradeMany112;
		[Field("easy player vehicle ram chance#Chance of deciding to ram the player in a vehicle on easy difficulty", null)]
		public float EasyPlayerVehicleRamChance114;
		[Field("normal player vehicle ram chance#Chance of deciding to ram the player in a vehicle on normal difficulty", null)]
		public float NormalPlayerVehicleRamChance115;
		[Field("hard player vehicle ram chance#Chance of deciding to ram the player in a vehicle on hard difficulty", null)]
		public float HardPlayerVehicleRamChance116;
		[Field("imposs. player vehicle ram chance#Chance of deciding to ram the player in a vehicle on impossible difficulty", null)]
		public float ImpossPlayerVehicleRamChance117;
		[Field("", null)]
		public fixed byte _118[16];
		[Field("", null)]
		public fixed byte _119[16];
		[Field("", null)]
		public fixed byte _120[16];
		[Field("", null)]
		public fixed byte _121[84];
	}
}
#pragma warning restore CS1591
