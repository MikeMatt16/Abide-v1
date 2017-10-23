using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct SoundEffectComponentBlock
	{
		public enum Flags2Options
		{
			DonTPlayAtStart_0 = 1,
			PlayOnStop_1 = 2,
			__2 = 4,
			PlayAlternate_3 = 8,
			__4 = 16,
			SyncWithOriginLoopingSound_5 = 32,
		}
		[Field("sound^", null)]
		public TagReference Sound0;
		[Field("gain:dB#additional attenuation to sound", null)]
		public float Gain1;
		[Field("flags", typeof(Flags2Options))]
		public int Flags2;
	}
}
#pragma warning restore CS1591
