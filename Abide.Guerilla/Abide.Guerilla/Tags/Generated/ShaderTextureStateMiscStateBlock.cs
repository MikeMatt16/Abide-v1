using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ShaderTextureStateMiscStateBlock
	{
		public enum ComponentSignFlags0Options
		{
			RSigned_0 = 1,
			GSigned_1 = 2,
			BSigned_2 = 4,
			ASigned_3 = 8,
		}
		[Field("component sign flags", typeof(ComponentSignFlags0Options))]
		public short ComponentSignFlags0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("border color", null)]
		public ColorArgb BorderColor2;
	}
}
#pragma warning restore CS1591
