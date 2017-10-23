using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(10, 4)]
	public unsafe struct SoundGestaltPitchRangeParametersBlock
	{
		[Field("natural pitch:cents", null)]
		public short NaturalPitch0;
		[Field("bend bounds:cents#the range of pitches that will be represented using this sample.", null)]
		public FloatBounds BendBounds1;
		[Field("max gain pitch bounds:cents", null)]
		public FloatBounds MaxGainPitchBounds2;
	}
}
#pragma warning restore CS1591
