using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(92, 4)]
	public unsafe struct SoundClassBlock
	{
		public enum InternalFlags3Options
		{
			Valid_0 = 1,
			IsSpeech_1 = 2,
			Scripted_2 = 4,
			StopsWithObject_3 = 8,
			Unused_4 = 16,
			ValidDopplerFactor_5 = 32,
			ValidObstructionFactor_6 = 64,
			Multilingual_7 = 128,
		}
		public enum Flags4Options
		{
			PlaysDuringPause_0 = 1,
			DryStereoMix_1 = 2,
			NoObjectObstruction_2 = 4,
			UseCenterSpeakerUnspatialized_3 = 8,
			SendMonoToLfe_4 = 16,
			Deterministic_5 = 32,
			UseHugeTransmission_6 = 64,
			AlwaysUseSpeakers_7 = 128,
			DonTStripFromMainMenu_8 = 256,
			IgnoreStereoHeadroom_9 = 512,
			LoopFadeOutIsLinear_10 = 1024,
			StopWhenObjectDies_11 = 2048,
			AllowCacheFileEditing_12 = 4096,
		}
		public enum CacheMissMode6Options
		{
			Discard_0 = 0,
			Postpone_1 = 1,
		}
		public enum StereoPlaybackType21Options
		{
			FirstPerson_0 = 0,
			Ambient_1 = 1,
		}
		[Field("max sounds per tag [1,16]#maximum number of sounds playing per individual sound tag", null)]
		public short MaxSoundsPerTag1160;
		[Field("max sounds per object [1,16]#maximum number of sounds of this type playing on an object", null)]
		public short MaxSoundsPerObject1161;
		[Field("preemption time:ms#replaces other instances after this many milliseconds", null)]
		public int PreemptionTime2;
		[Field("internal flags*", typeof(InternalFlags3Options))]
		public short InternalFlags3;
		[Field("flags", typeof(Flags4Options))]
		public short Flags4;
		[Field("priority*", null)]
		public short Priority5;
		[Field("cache miss mode*", typeof(CacheMissMode6Options))]
		public short CacheMissMode6;
		[Field("reverb gain:dB#how much reverb applies to this sound class", null)]
		public float ReverbGain7;
		[Field("override speaker gain:dB", null)]
		public float OverrideSpeakerGain8;
		[Field("distance bounds", null)]
		public FloatBounds DistanceBounds9;
		[Field("gain bounds:dB", null)]
		public FloatBounds GainBounds11;
		[Field("cutscene ducking:dB", null)]
		public float CutsceneDucking12;
		[Field("cutscene ducking fade in time:seconds", null)]
		public float CutsceneDuckingFadeInTime13;
		[Field("cutscene ducking sustain time:seconds#how long this lasts after the cutscene ends", null)]
		public float CutsceneDuckingSustainTime14;
		[Field("cutscene ducking fade out time:seconds", null)]
		public float CutsceneDuckingFadeOutTime15;
		[Field("scripted dialog ducking:dB", null)]
		public float ScriptedDialogDucking16;
		[Field("scripted dialog ducking fade in time:seconds", null)]
		public float ScriptedDialogDuckingFadeInTime17;
		[Field("scripted dialog ducking sustain time:seconds#how long this lasts after the scripted dialog ends", null)]
		public float ScriptedDialogDuckingSustainTime18;
		[Field("scripted dialog ducking fade out time:seconds", null)]
		public float ScriptedDialogDuckingFadeOutTime19;
		[Field("doppler factor", null)]
		public float DopplerFactor20;
		[Field("stereo playback type", typeof(StereoPlaybackType21Options))]
		public byte StereoPlaybackType21;
		[Field("", null)]
		public fixed byte _22[3];
		[Field("transmission multiplier", null)]
		public float TransmissionMultiplier23;
		[Field("obstruction max bend", null)]
		public float ObstructionMaxBend24;
		[Field("occlusion max bend", null)]
		public float OcclusionMaxBend25;
	}
}
#pragma warning restore CS1591
