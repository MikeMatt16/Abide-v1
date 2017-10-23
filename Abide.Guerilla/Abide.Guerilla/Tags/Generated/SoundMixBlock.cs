using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("sound_mix", "snmx", "����", typeof(SoundMixBlock))]
	[FieldSet(88, 4)]
	public unsafe struct SoundMixBlock
	{
		[Field("left stereo gain:dB", null)]
		public float LeftStereoGain1;
		[Field("right stereo gain:dB", null)]
		public float RightStereoGain2;
		[Field("left stereo gain:dB", null)]
		public float LeftStereoGain4;
		[Field("right stereo gain:dB", null)]
		public float RightStereoGain5;
		[Field("left stereo gain:dB", null)]
		public float LeftStereoGain7;
		[Field("right stereo gain:dB", null)]
		public float RightStereoGain8;
		[Field("front speaker gain:dB", null)]
		public float FrontSpeakerGain10;
		[Field("rear speaker gain:dB", null)]
		public float RearSpeakerGain11;
		[Field("front speaker gain:dB", null)]
		public float FrontSpeakerGain13;
		[Field("rear speaker gain:dB", null)]
		public float RearSpeakerGain14;
		[Field("global mix", typeof(SoundGlobalMixStructBlock))]
		[Block("Sound Global Mix Struct", 1, typeof(SoundGlobalMixStructBlock))]
		public SoundGlobalMixStructBlock GlobalMix16;
	}
}
#pragma warning restore CS1591
