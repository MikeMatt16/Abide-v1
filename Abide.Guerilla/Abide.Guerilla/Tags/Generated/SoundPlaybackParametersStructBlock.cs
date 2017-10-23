using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(56, 4)]
	public unsafe struct SoundPlaybackParametersStructBlock
	{
		public enum Flags17Options
		{
			OverrideAzimuth_0 = 1,
			Override3dGain_1 = 2,
			OverrideSpeakerGain_2 = 4,
		}
		[Field("minimum distance:world units#the distance below which this sound no longer gets louder", null)]
		public float MinimumDistance2;
		[Field("maximum distance:world units#the distance beyond which this sound is no longer audible", null)]
		public float MaximumDistance3;
		[Field("skip fraction#fraction of requests to play this sound that will be ignored (0 means always play.)", null)]
		public float SkipFraction4;
		[Field("maximum bend per second:cents", null)]
		public float MaximumBendPerSecond5;
		[Field("gain base:dB#sound's random gain will start here", null)]
		public float GainBase8;
		[Field("gain variance:dB#sound's gain will be randomly modulated within this range", null)]
		public float GainVariance9;
		[Field("random pitch bounds:cents#the sound's pitch will be modulated randomly within this range.", null)]
		public FloatBounds RandomPitchBounds10;
		[Field("inner cone angle:degrees#within the cone defined by this angle and the sound's direction, the sound plays with a gain of 1.0.", null)]
		public float InnerConeAngle12;
		[Field("outer cone angle:degrees#outside the cone defined by this angle and the sound's direction, the sound plays with a gain of OUTER CONE GAIN. (0 means the sound does not attenuate with direction.)", null)]
		public float OuterConeAngle13;
		[Field("outer cone gain:dB#the gain to use when the sound is directed away from the listener", null)]
		public float OuterConeGain14;
		[Field("flags", typeof(Flags17Options))]
		public int Flags17;
		[Field("azimuth", null)]
		public float Azimuth18;
		[Field("positional gain:dB", null)]
		public float PositionalGain19;
		[Field("first person gain:dB", null)]
		public float FirstPersonGain20;
	}
}
#pragma warning restore CS1591
