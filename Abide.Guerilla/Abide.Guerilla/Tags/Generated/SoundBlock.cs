using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(164, 4)]
	public unsafe struct SoundBlock
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
		public enum Class1Options
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
		public enum OutputEffect12Options
		{
			None_0 = 0,
			OutputFrontSpeakers_1 = 1,
			OutputRearSpeakers_2 = 2,
			OutputCenterSpeakers_3 = 3,
		}
		public enum ImportType13Options
		{
			Unknown_0 = 0,
			SingleShot_1 = 1,
			SingleLayer_2 = 2,
			MultiLayer_3 = 3,
		}
		public enum Encoding23Options
		{
			Mono_0 = 0,
			Stereo_1 = 1,
			Codec_2 = 2,
		}
		public enum Compression24Options
		{
			NoneBigEndian_0 = 0,
			XboxAdpcm_1 = 1,
			ImaAdpcm_2 = 2,
			NoneLittleEndian_3 = 3,
			Wma_4 = 4,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("class", typeof(Class1Options))]
		public short Class1;
		[Field("sample rate*", typeof(SampleRate2Options))]
		public short SampleRate2;
		[Field("minimum distance:world units#the distance below which this sound no longer gets louder", null)]
		public float MinimumDistance3;
		[Field("maximum distance:world units#the distance beyond which this sound is no longer audible", null)]
		public float MaximumDistance4;
		[Field("skip fraction#fraction of requests to play this sound that will be ignored (0 means always play.)", null)]
		public float SkipFraction5;
		[Field("random pitch bounds:cents#the sound's pitch will be modulated randomly within this range.", null)]
		public FloatBounds RandomPitchBounds6;
		[Field("inner cone angle:degrees#within the cone defined by this angle and the sound's direction, the sound plays with a gain of 1.0.", null)]
		public float InnerConeAngle7;
		[Field("outer cone angle:degrees#outside the cone defined by this angle and the sound's direction, the sound plays with a gain of OUTER CONE GAIN. (0 means the sound does not attenuate with direction.)", null)]
		public float OuterConeAngle8;
		[Field("outer cone gain:dB#the gain to use when the sound is directed away from the listener", null)]
		public float OuterConeGain9;
		[Field("gain modifier:dB", null)]
		public float GainModifier10;
		[Field("maximum bend per second:cents", null)]
		public float MaximumBendPerSecond11;
		[Field("output effect", typeof(OutputEffect12Options))]
		public short OutputEffect12;
		[Field("import type*", typeof(ImportType13Options))]
		public short ImportType13;
		[Field("", null)]
		public fixed byte _14[8];
		[Field("skip fraction modifier", null)]
		public float SkipFractionModifier15;
		[Field("gain modifier:dB", null)]
		public float GainModifier16;
		[Field("pitch modifier:cents", null)]
		public float PitchModifier17;
		[Field("", null)]
		public fixed byte _18[12];
		[Field("skip fraction modifier", null)]
		public float SkipFractionModifier19;
		[Field("gain modifier:dB", null)]
		public float GainModifier20;
		[Field("pitch modifier:cents", null)]
		public float PitchModifier21;
		[Field("", null)]
		public fixed byte _22[12];
		[Field("encoding*", typeof(Encoding23Options))]
		public short Encoding23;
		[Field("compression*", typeof(Compression24Options))]
		public short Compression24;
		[Field("promotion sound", null)]
		public TagReference PromotionSound25;
		[Field("promotion count#when there are this many instances of the sound, promote to the new sound.", null)]
		public short PromotionCount26;
		[Field("", null)]
		public fixed byte _27[2];
		[Field("", null)]
		public fixed byte _28[20];
		[Field("pitch ranges#pitch ranges allow multiple samples to represent the same sound at different pitches", null)]
		[Block("Old Sound Pitch Range Block", 9, typeof(OldSoundPitchRangeBlock))]
		public TagBlock PitchRanges29;
	}
}
#pragma warning restore CS1591
