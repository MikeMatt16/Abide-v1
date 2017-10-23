using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ShaderStateMiscStateBlock
	{
		public enum Flags0Options
		{
			YUVToRGB_0 = 1,
			_16BitDither_1 = 2,
			_32BitDXT1Noise_2 = 4,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("fog color", null)]
		public ColorRgb FogColor2;
	}
}
#pragma warning restore CS1591
