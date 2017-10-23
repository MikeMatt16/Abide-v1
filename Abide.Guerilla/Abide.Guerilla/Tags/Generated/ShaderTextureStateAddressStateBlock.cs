using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ShaderTextureStateAddressStateBlock
	{
		public enum UAddressMode0Options
		{
			Wrap_0 = 0,
			Mirror_1 = 1,
			Clamp_2 = 2,
			Border_3 = 3,
			ClampToEdge_4 = 4,
		}
		public enum VAddressMode1Options
		{
			Wrap_0 = 0,
			Mirror_1 = 1,
			Clamp_2 = 2,
			Border_3 = 3,
			ClampToEdge_4 = 4,
		}
		public enum WAddressMode2Options
		{
			Wrap_0 = 0,
			Mirror_1 = 1,
			Clamp_2 = 2,
			Border_3 = 3,
			ClampToEdge_4 = 4,
		}
		[Field("U address mode", typeof(UAddressMode0Options))]
		public short UAddressMode0;
		[Field("V address mode", typeof(VAddressMode1Options))]
		public short VAddressMode1;
		[Field("W address mode", typeof(WAddressMode2Options))]
		public short WAddressMode2;
		[Field("", null)]
		public fixed byte _3[2];
	}
}
#pragma warning restore CS1591
