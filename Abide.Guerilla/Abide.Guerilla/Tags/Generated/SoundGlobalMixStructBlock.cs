using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(48, 4)]
	public unsafe struct SoundGlobalMixStructBlock
	{
		[Field("mono unspatialized gain:dB", null)]
		public float MonoUnspatializedGain0;
		[Field("stereo to 3d gain:dB", null)]
		public float StereoTo3dGain1;
		[Field("rear surround to front stereo gain:dB", null)]
		public float RearSurroundToFrontStereoGain2;
		[Field("front speaker gain:dB", null)]
		public float FrontSpeakerGain4;
		[Field("center speaker gain:dB", null)]
		public float CenterSpeakerGain5;
		[Field("front speaker gain:dB", null)]
		public float FrontSpeakerGain7;
		[Field("center speaker gain:dB", null)]
		public float CenterSpeakerGain8;
		[Field("stereo unspatialized gain:dB", null)]
		public float StereoUnspatializedGain10;
		[Field("solo player fade out delay: seconds", null)]
		public float SoloPlayerFadeOutDelay12;
		[Field("solo player fade out time: seconds", null)]
		public float SoloPlayerFadeOutTime13;
		[Field("solo player fade in time: seconds", null)]
		public float SoloPlayerFadeInTime14;
		[Field("game music fade out time: seconds", null)]
		public float GameMusicFadeOutTime15;
	}
}
#pragma warning restore CS1591
