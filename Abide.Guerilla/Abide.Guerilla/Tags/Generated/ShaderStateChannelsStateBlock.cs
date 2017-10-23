using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct ShaderStateChannelsStateBlock
	{
		public enum Flags0Options
		{
			R_0 = 1,
			G_1 = 2,
			B_2 = 4,
			A_3 = 8,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("", null)]
		public fixed byte _1[2];
	}
}
#pragma warning restore CS1591
