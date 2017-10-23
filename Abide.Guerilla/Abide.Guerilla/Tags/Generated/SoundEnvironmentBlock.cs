using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("sound_environment", "snde", "����", typeof(SoundEnvironmentBlock))]
	[FieldSet(72, 4)]
	public unsafe struct SoundEnvironmentBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("priority#when multiple listeners are in different sound environments in split screen, the combined environment will be the one with the highest priority.", null)]
		public short Priority1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("room intensity:dB#intensity of the room effect", null)]
		public float RoomIntensity3;
		[Field("room intensity hf:dB#intensity of the room effect above the reference high frequency", null)]
		public float RoomIntensityHf4;
		[Field("room rolloff (0 to 10)#how quickly the room effect rolls off, from 0.0 to 10.0", null)]
		public float RoomRolloff0To105;
		[Field("decay time (.1 to 20):seconds", null)]
		public float DecayTime1To206;
		[Field("decay hf ratio (.1 to 2)", null)]
		public float DecayHfRatio1To27;
		[Field("reflections intensity:dB[-100,10]", null)]
		public float ReflectionsIntensity8;
		[Field("reflections delay (0 to .3):seconds", null)]
		public float ReflectionsDelay0To39;
		[Field("reverb intensity:dB[-100,20]", null)]
		public float ReverbIntensity10;
		[Field("reverb delay (0 to .1):seconds", null)]
		public float ReverbDelay0To111;
		[Field("diffusion", null)]
		public float Diffusion12;
		[Field("density", null)]
		public float Density13;
		[Field("hf reference(20 to 20,000):Hz#for hf values, what frequency defines hf, from 20 to 20,000", null)]
		public float HfReference20To2000014;
		[Field("", null)]
		public fixed byte _15[16];
	}
}
#pragma warning restore CS1591
