using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct HudWidgetStateDefinitionStructBlock
	{
		public enum YUnitFlags1Options
		{
			Default_0 = 1,
			GrenadeTypeIsNONE_1 = 2,
			GrenadeTypeIsFrag_2 = 4,
			GrenadeTypeIsPlasma_3 = 8,
			UnitIsSingleWielding_4 = 16,
			UnitIsDualWielding_5 = 32,
			UnitIsUnzoomed_6 = 64,
			UnitIsZoomedLevel1_7 = 128,
			UnitIsZoomedLevel2_8 = 256,
			GrenadesDisabled_9 = 512,
			BinocularsEnabled_10 = 1024,
			MotionSensorEnabled_11 = 2048,
			ShieldEnabled_12 = 4096,
			Dervish_13 = 8192,
		}
		public enum YExtraFlags2Options
		{
			AutoaimFriendly_0 = 1,
			AutoaimPlasma_1 = 2,
			AutoaimHeadshot_2 = 4,
			AutoaimVulnerable_3 = 8,
			AutoaimInvincible_4 = 16,
		}
		public enum YWeaponFlags3Options
		{
			PrimaryWeapon_0 = 1,
			SecondaryWeapon_1 = 2,
			BackpackWeapon_2 = 4,
			AgeBelowCutoff_3 = 8,
			ClipBelowCutoff_4 = 16,
			TotalBelowCutoff_5 = 32,
			Overheated_6 = 64,
			OutOfAmmo_7 = 128,
			LockTargetAvailable_8 = 256,
			Locking_9 = 512,
			Locked_10 = 1024,
		}
		public enum YGameEngineStateFlags4Options
		{
			CampaignSolo_0 = 1,
			CampaignCoop_1 = 2,
			FreeForAll_2 = 4,
			TeamGame_3 = 8,
			UserLeading_4 = 16,
			UserNotLeading_5 = 32,
			TimedGame_6 = 64,
			UntimedGame_7 = 128,
			OtherScoreValid_8 = 256,
			OtherScoreInvalid_9 = 512,
			PlayerIsArmingBomb_10 = 1024,
			PlayerTalking_11 = 2048,
		}
		public enum NUnitFlags6Options
		{
			Default_0 = 1,
			GrenadeTypeIsNONE_1 = 2,
			GrenadeTypeIsFrag_2 = 4,
			GrenadeTypeIsPlasma_3 = 8,
			UnitIsSingleWielding_4 = 16,
			UnitIsDualWielding_5 = 32,
			UnitIsUnzoomed_6 = 64,
			UnitIsZoomedLevel1_7 = 128,
			UnitIsZoomedLevel2_8 = 256,
			GrenadesDisabled_9 = 512,
			BinocularsEnabled_10 = 1024,
			MotionSensorEnabled_11 = 2048,
			ShieldEnabled_12 = 4096,
			Dervish_13 = 8192,
		}
		public enum NExtraFlags7Options
		{
			AutoaimFriendly_0 = 1,
			AutoaimPlasma_1 = 2,
			AutoaimHeadshot_2 = 4,
			AutoaimVulnerable_3 = 8,
			AutoaimInvincible_4 = 16,
		}
		public enum NWeaponFlags8Options
		{
			PrimaryWeapon_0 = 1,
			SecondaryWeapon_1 = 2,
			BackpackWeapon_2 = 4,
			AgeBelowCutoff_3 = 8,
			ClipBelowCutoff_4 = 16,
			TotalBelowCutoff_5 = 32,
			Overheated_6 = 64,
			OutOfAmmo_7 = 128,
			LockTargetAvailable_8 = 256,
			Locking_9 = 512,
			Locked_10 = 1024,
		}
		public enum NGameEngineStateFlags9Options
		{
			CampaignSolo_0 = 1,
			CampaignCoop_1 = 2,
			FreeForAll_2 = 4,
			TeamGame_3 = 8,
			UserLeading_4 = 16,
			UserNotLeading_5 = 32,
			TimedGame_6 = 64,
			UntimedGame_7 = 128,
			OtherScoreValid_8 = 256,
			OtherScoreInvalid_9 = 512,
			PlayerIsArmingBomb_10 = 1024,
			PlayerTalking_11 = 2048,
		}
		[Field("[Y] unit flags", typeof(YUnitFlags1Options))]
		public short YUnitFlags1;
		[Field("[Y] extra flags", typeof(YExtraFlags2Options))]
		public short YExtraFlags2;
		[Field("[Y] weapon flags", typeof(YWeaponFlags3Options))]
		public short YWeaponFlags3;
		[Field("[Y] game engine state flags", typeof(YGameEngineStateFlags4Options))]
		public short YGameEngineStateFlags4;
		[Field("", null)]
		public fixed byte _5[8];
		[Field("[N] unit flags", typeof(NUnitFlags6Options))]
		public short NUnitFlags6;
		[Field("[N] extra flags", typeof(NExtraFlags7Options))]
		public short NExtraFlags7;
		[Field("[N] weapon flags", typeof(NWeaponFlags8Options))]
		public short NWeaponFlags8;
		[Field("[N] game engine state flags", typeof(NGameEngineStateFlags9Options))]
		public short NGameEngineStateFlags9;
		[Field("", null)]
		public fixed byte _10[8];
		[Field("age cutoff", null)]
		public int AgeCutoff11;
		[Field("clip cutoff", null)]
		public int ClipCutoff12;
		[Field("total cutoff", null)]
		public int TotalCutoff13;
		[Field("", null)]
		public fixed byte _14[1];
	}
}
#pragma warning restore CS1591
