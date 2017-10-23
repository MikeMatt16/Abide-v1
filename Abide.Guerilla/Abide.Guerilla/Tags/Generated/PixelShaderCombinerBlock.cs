using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct PixelShaderCombinerBlock
	{
		[Field("", null)]
		public fixed byte _0[16];
		[Field("constant color0", null)]
		public ColorArgb ConstantColor01;
		[Field("constant color1", null)]
		public ColorArgb ConstantColor12;
		[Field("color A register ptr index", null)]
		public int ColorARegisterPtrIndex3;
		[Field("color B register ptr index", null)]
		public int ColorBRegisterPtrIndex4;
		[Field("color C register ptr index", null)]
		public int ColorCRegisterPtrIndex5;
		[Field("color D register ptr index", null)]
		public int ColorDRegisterPtrIndex6;
		[Field("alpha A register ptr index", null)]
		public int AlphaARegisterPtrIndex7;
		[Field("alpha B register ptr index", null)]
		public int AlphaBRegisterPtrIndex8;
		[Field("alpha C register ptr index", null)]
		public int AlphaCRegisterPtrIndex9;
		[Field("alpha D register ptr index", null)]
		public int AlphaDRegisterPtrIndex10;
	}
}
#pragma warning restore CS1591
