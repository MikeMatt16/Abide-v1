#pragma warning disable CS1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.Guerilla.Tags
{
    using Abide.Guerilla.Types;
    using Abide.HaloLibrary;
    using System;
    using System.IO;
    
    [FieldSetAttribute(48, 4)]
    [TagGroupAttribute("text_value_pair_definition", 1936288889u, 4294967293u, typeof(TextValuePairDefinitionBlock))]
    public sealed class TextValuePairDefinitionBlock : AbideTagBlock
    {
        private TagBlockList<TextValuePairReferenceBlock> textValuePairsList = new TagBlockList<TextValuePairReferenceBlock>(32);
        [FieldAttribute("parameter", typeof(ParameterOptions))]
        [OptionsAttribute(typeof(ParameterOptions), false)]
        public ParameterOptions Parameter;
        [FieldAttribute("", typeof(Byte[]))]
        [PaddingAttribute(4)]
        public Byte[] EmptyString;
        [FieldAttribute("string list", typeof(TagReference))]
        public TagReference StringList;
        [FieldAttribute("title text", typeof(StringId))]
        public StringId TitleText;
        [FieldAttribute("header text", typeof(StringId))]
        public StringId HeaderText;
        [FieldAttribute("description text", typeof(StringId))]
        public StringId DescriptionText;
        [FieldAttribute("text value pairs", typeof(TagBlock))]
        [BlockAttribute("text_value_pair_reference_block", 32, typeof(TextValuePairReferenceBlock))]
        public TagBlock TextValuePairs;
        public TagBlockList<TextValuePairReferenceBlock> TextValuePairsList
        {
            get
            {
                return this.textValuePairsList;
            }
        }
        public override int Size
        {
            get
            {
                return 48;
            }
        }
        public override void Initialize()
        {
            this.textValuePairsList.Clear();
            this.Parameter = ((ParameterOptions)(0));
            this.EmptyString = new byte[4];
            this.StringList = TagReference.Null;
            this.TitleText = StringId.Zero;
            this.HeaderText = StringId.Zero;
            this.DescriptionText = StringId.Zero;
            this.TextValuePairs = TagBlock.Zero;
        }
        public override void Read(BinaryReader reader)
        {
            this.Parameter = ((ParameterOptions)(reader.ReadInt32()));
            this.EmptyString = reader.ReadBytes(4);
            this.StringList = reader.Read<TagReference>();
            this.TitleText = reader.ReadInt32();
            this.HeaderText = reader.ReadInt32();
            this.DescriptionText = reader.ReadInt32();
            this.TextValuePairs = reader.ReadInt64();
            this.textValuePairsList.Read(reader, this.TextValuePairs);
        }
        public override void Write(BinaryWriter writer)
        {
        }
        [FieldSetAttribute(12, 4)]
        public sealed class TextValuePairReferenceBlock : AbideTagBlock
        {
            [FieldAttribute("flags", typeof(FlagsOptions))]
            [OptionsAttribute(typeof(FlagsOptions), true)]
            public FlagsOptions Flags;
            [FieldAttribute("value", typeof(Int32))]
            public Int32 Value;
            [FieldAttribute("label string id", typeof(StringId))]
            public StringId LabelStringId;
            public override int Size
            {
                get
                {
                    return 12;
                }
            }
            public override void Initialize()
            {
                this.Flags = ((FlagsOptions)(0));
                this.Value = 0;
                this.LabelStringId = StringId.Zero;
            }
            public override void Read(BinaryReader reader)
            {
                this.Flags = ((FlagsOptions)(reader.ReadInt32()));
                this.Value = reader.ReadInt32();
                this.LabelStringId = reader.ReadInt32();
            }
            public override void Write(BinaryWriter writer)
            {
            }
            public enum FlagsOptions : Int32
            {
                DefaultSetting = 1,
            }
        }
        public enum ParameterOptions : Int32
        {
            MatchRoundSetting = 0,
            MatchCtfScoreToWin = 1,
            MatchSlayerScoreToWinRound = 2,
            MatchOddballScoreToWinRound = 3,
            MatchKingScoreToWinRound = 4,
            MatchRaceScoreToWinRound = 5,
            MatchHeadhunterScoreToWinRound = 6,
            MatchJuggernautScoreToWinRound = 7,
            MatchTerritoriesScoreToWinRound = 8,
            MatchAssaultScoreToWinRound = 9,
            MatchRoundTimeLimit = 10,
            MatchRoundsResetMap = 11,
            MatchTieResolution = 12,
            MatchObservers = 13,
            MatchJoinInProgress = 14,
            MaximumPlayers = 15,
            LivesPerRound = 16,
            RespawnTime = 17,
            SuicidePenalty = 18,
            Shields = 19,
            MotionSensor = 20,
            Invisibility = 21,
            TeamChanging = 22,
            TeamScoring = 23,
            FriendlyFire = 24,
            TeamRespawnSetting = 25,
            BetrayalRespawnPenalty = 26,
            TeamKillerManagement = 27,
            SlayerBonusPoints = 28,
            SlayerSuicidePointLoss = 29,
            SlayerDeathPointLoss = 30,
            HeadhunterMovingHeadBin = 31,
            HeadhunterPointMultiplier = 32,
            HeadhunterSuicidePointLoss = 33,
            HeadhunterDeathPointLoss = 34,
            HeadhunterUncontestedBin = 35,
            HeadhunterSpeedWithHeads = 36,
            HeadhunterMaxHeadsCarried = 37,
            KingUncontestedHill = 38,
            KingTeamTimeMultiplier = 39,
            KingMovingHill = 40,
            KingExtraDamageOnHill = 41,
            KingDmgResistanceOnHill = 42,
            OddballBallSpawnCount = 43,
            OddballBallHitDamage = 44,
            OddballSpeedWithBall = 45,
            OddballDrivingGunningWithBall = 46,
            OddballWaypointToBall = 47,
            RaceRandomTrack = 48,
            RaceUncontestedFlag = 49,
            CtfGameType = 50,
            CtfSuddenDeath = 51,
            CtfFlagMayBeReturned = 52,
            CtfFlagAtHomeToScore = 53,
            CtfFlagResetTime = 54,
            CtfSpeedWithFlag = 55,
            CtfFlagHitDamage = 56,
            CtfDrivingGunningWithFlag = 57,
            CtfWaypointToOwnFlag = 58,
            AssaultGameType = 59,
            AssaultSuddenDeath = 60,
            AssaultDetonationTime = 61,
            AssaultBombAtHomeToScore = 62,
            AssaultArmingTime = 63,
            AssaultSpeedWithBomb = 64,
            AssaultBombHitDamage = 65,
            AssaultDrivingGunningWithBomb = 66,
            AssaultWaypointToOwnBomb = 67,
            JuggernautBetrayalPointLoss = 68,
            JuggernautJuggyExtraDamage = 69,
            JuggernautJuggyInfiniteAmmo = 70,
            JuggernautJuggyOvershields = 71,
            JuggernautJuggyActiveCamo = 72,
            JuggernautJuggyMotionSensor = 73,
            TerritoriesTerritoryCount = 74,
            VehRespawn = 75,
            VehPrimaryLightLand = 76,
            VehSecondaryLightLand = 77,
            VehPrimaryHeavyLand = 78,
            VehPrimaryFlying = 79,
            VehSecondaryHeavyLand = 80,
            VehPrimaryTurret = 81,
            VehSecondaryTurret = 82,
            EquipWeaponsOnMap = 83,
            EquipOvershieldsOnMap = 84,
            EquipActiveCamoOnMap = 85,
            EquipGrenadesOnMap = 86,
            EquipWeaponRespawnTimes = 87,
            EquipStartingGrenades = 88,
            EquipPrimaryStartingEquipment = 89,
            UnsMaxLivingPlayers = 90,
            UnsTeamsEnabled = 91,
            UnsAssaultBombMayBeReturned = 92,
            UnsMaxTeams = 93,
            UnsEquipSecondaryStartingEquipment = 94,
            UnsAssaultFuseTime = 95,
            UnsJuggyMovement = 96,
            UnsStickyFuse = 97,
            UnsTerrContestTime = 98,
            UnsTerrControlTime = 99,
            UnsOddbCarrInvis = 100,
            UnsKingInvisInHill = 101,
            UnsBallCarrDmgResis = 102,
            UnsKingDmgResInHill = 103,
            UnsPlayersExDmg = 104,
            UnsPlayersDmgResis = 105,
            UnsCtfCarrDmgResis = 106,
            UnsCtfCarrInvis = 107,
            UnsJuggyDmgResis = 108,
            UnsBombCarrDmgResis = 109,
            UnsBombCarrInvis = 110,
            UnsForceEvenTeams = 111,
        }
    }
}
#pragma warning restore CS1591
