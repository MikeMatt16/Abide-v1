using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(60, 4)]
	public unsafe struct LoopingSoundDetailBlock
	{
		public enum Flags5Options
		{
			DonTPlayWithAlternate_0 = 1,
			DonTPlayWithoutAlternate_1 = 2,
			StartImmediatelyWithLoop_2 = 4,
		}
		[Field("name^", null)]
		public StringId Name0;
		[Field("sound", null)]
		public TagReference Sound1;
		[Field("random period bounds:seconds#the time between successive playings of this sound will be randomly selected from this range.", null)]
		public FloatBounds RandomPeriodBounds3;
		[Field("", null)]
		public float _4;
		[Field("flags", typeof(Flags5Options))]
		public int Flags5;
		[Field("yaw bounds:degrees#the sound's position along the horizon will be randomly selected from this range.", null)]
		public FloatBounds YawBounds7;
		[Field("pitch bounds:degrees#the sound's position above (positive values) or below (negative values) the horizon will be randomly selected from this range.", null)]
		public FloatBounds PitchBounds8;
		[Field("distance bounds:world units#the sound's distance (from its spatialized looping sound or from the listener if the looping sound is stereo) will be randomly selected from this range.", null)]
		public FloatBounds DistanceBounds9;
	}
}
#pragma warning restore CS1591
