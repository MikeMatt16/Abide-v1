using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct PlatformSoundOverrideMixbinsBlock
	{
		public enum Mixbin0Options
		{
			FrontLeft_0 = 0,
			FrontRight_1 = 1,
			BackLeft_2 = 2,
			BackRight_3 = 3,
			Center_4 = 4,
			LowFrequency_5 = 5,
			Reverb_6 = 6,
			_3dFrontLeft_7 = 7,
			_3dFrontRight_8 = 8,
			_3dBackLeft_9 = 9,
			_3dBackRight_10 = 10,
			DefaultLeftSpeakers_11 = 11,
			DefaultRightSpeakers_12 = 12,
		}
		[Field("mixbin", typeof(Mixbin0Options))]
		public int Mixbin0;
		[Field("gain:dB", null)]
		public float Gain1;
	}
}
#pragma warning restore CS1591
