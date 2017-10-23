using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct SoundScaleModifiersStructBlock
	{
		[Field("gain modifier:dB", null)]
		public FloatBounds GainModifier1;
		[Field("pitch modifier:cents", null)]
		public FloatBounds PitchModifier2;
		[Field("skip fraction modifier", null)]
		public FloatBounds SkipFractionModifier3;
	}
}
#pragma warning restore CS1591
