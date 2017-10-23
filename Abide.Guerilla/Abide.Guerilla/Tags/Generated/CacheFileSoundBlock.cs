using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("cache_file_sound", "$#!+", "����", typeof(CacheFileSoundBlock))]
	[FieldSet(20, 4)]
	public unsafe struct CacheFileSoundBlock
	{
		public enum Flags0Options
		{
			FitToAdpcmBlocksize_0 = 1,
			SplitLongSoundIntoPermutations_1 = 2,
			AlwaysSpatializeAlwaysPlayAs3dSoundEvenInFirstPerson_2 = 4,
			NeverObstructDisableOcclusionObstructionForThisSound_3 = 8,
			InternalDonTTouch_4 = 16,
			UseHugeSoundTransmission_5 = 32,
			LinkCountToOwnerUnit_6 = 64,
			PitchRangeIsLanguage_7 = 128,
			DonTUseSoundClassSpeakerFlag_8 = 256,
			DonTUseLipsyncData_9 = 512,
		}
		public enum SoundClass1Options
		{
			ProjectileImpact_0 = 0,
			ProjectileDetonation_1 = 1,
			ProjectileFlyby_2 = 2,
			__3 = 3,
			WeaponFire_4 = 4,
			WeaponReady_5 = 5,
			WeaponReload_6 = 6,
			WeaponEmpty_7 = 7,
			WeaponCharge_8 = 8,
			WeaponOverheat_9 = 9,
			WeaponIdle_10 = 10,
			WeaponMelee_11 = 11,
			WeaponAnimation_12 = 12,
			ObjectImpacts_13 = 13,
			ParticleImpacts_14 = 14,
			__15 = 15,
			__16 = 16,
			__17 = 17,
			UnitFootsteps_18 = 18,
			UnitDialog_19 = 19,
			UnitAnimation_20 = 20,
			__21 = 21,
			VehicleCollision_22 = 22,
			VehicleEngine_23 = 23,
			VehicleAnimation_24 = 24,
			__25 = 25,
			DeviceDoor_26 = 26,
			__27 = 27,
			DeviceMachinery_28 = 28,
			DeviceStationary_29 = 29,
			__30 = 30,
			__31 = 31,
			Music_32 = 32,
			AmbientNature_33 = 33,
			AmbientMachinery_34 = 34,
			__35 = 35,
			HugeAss_36 = 36,
			ObjectLooping_37 = 37,
			CinematicMusic_38 = 38,
			__39 = 39,
			__40 = 40,
			__41 = 41,
			__42 = 42,
			__43 = 43,
			__44 = 44,
			CortanaMission_45 = 45,
			CortanaCinematic_46 = 46,
			MissionDialog_47 = 47,
			CinematicDialog_48 = 48,
			ScriptedCinematicFoley_49 = 49,
			GameEvent_50 = 50,
			Ui_51 = 51,
			Test_52 = 52,
			MultilingualTest_53 = 53,
		}
		public enum SampleRate2Options
		{
			_22kHz_0 = 0,
			_44kHz_1 = 1,
			_32kHz_2 = 2,
		}
		public enum Encoding3Options
		{
			Mono_0 = 0,
			Stereo_1 = 1,
			Codec_2 = 2,
		}
		public enum Compression4Options
		{
			NoneBigEndian_0 = 0,
			XboxAdpcm_1 = 1,
			ImaAdpcm_2 = 2,
			NoneLittleEndian_3 = 3,
			Wma_4 = 4,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("sound class*", typeof(SoundClass1Options))]
		public byte SoundClass1;
		[Field("sample rate*", typeof(SampleRate2Options))]
		public byte SampleRate2;
		[Field("encoding*", typeof(Encoding3Options))]
		public byte Encoding3;
		[Field("compression*", typeof(Compression4Options))]
		public byte Compression4;
		[Field("playback index", null)]
		public short PlaybackIndex5;
		[Field("first pitch range index", null)]
		public short FirstPitchRangeIndex6;
		[Field("pitch range count", null)]
		public int PitchRangeCount7;
		[Field("scale index", null)]
		public int ScaleIndex8;
		[Field("promotion index", null)]
		public int PromotionIndex9;
		[Field("custom playback index", null)]
		public int CustomPlaybackIndex10;
		[Field("extra info index", null)]
		public short ExtraInfoIndex11;
		[Field("maximum play time:ms", null)]
		public int MaximumPlayTime12;
	}
}
#pragma warning restore CS1591
