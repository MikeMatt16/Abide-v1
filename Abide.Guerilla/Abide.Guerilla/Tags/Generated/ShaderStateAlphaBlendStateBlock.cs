using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct ShaderStateAlphaBlendStateBlock
	{
		public enum BlendFunction0Options
		{
			Add_0 = 0,
			Subtract_1 = 1,
			ReverseSubtract_2 = 2,
			Min_3 = 3,
			Max_4 = 4,
			AddSigned_5 = 5,
			ReverseSubtractSigned_6 = 6,
			LogicOp_7 = 7,
		}
		public enum BlendSrcFactor1Options
		{
			Zero_0 = 0,
			One_1 = 1,
			Srccolor_2 = 2,
			SrccolorInverse_3 = 3,
			Srcalpha_4 = 4,
			SrcalphaInverse_5 = 5,
			Dstcolor_6 = 6,
			DstcolorInverse_7 = 7,
			Dstalpha_8 = 8,
			DstalphaInverse_9 = 9,
			SrcalphaSaturate_10 = 10,
			ConstantColor_11 = 11,
			ConstantColorInverse_12 = 12,
			ConstantAlpha_13 = 13,
			ConstantAlphaInverse_14 = 14,
		}
		public enum BlendDstFactor2Options
		{
			Zero_0 = 0,
			One_1 = 1,
			Srccolor_2 = 2,
			SrccolorInverse_3 = 3,
			Srcalpha_4 = 4,
			SrcalphaInverse_5 = 5,
			Dstcolor_6 = 6,
			DstcolorInverse_7 = 7,
			Dstalpha_8 = 8,
			DstalphaInverse_9 = 9,
			SrcalphaSaturate_10 = 10,
			ConstantColor_11 = 11,
			ConstantColorInverse_12 = 12,
			ConstantAlpha_13 = 13,
			ConstantAlphaInverse_14 = 14,
		}
		public enum LogicOpFlags5Options
		{
			Src0Dst0_0 = 1,
			Src0Dst1_1 = 2,
			Src1Dst0_2 = 4,
			Src1Dst1_3 = 8,
		}
		[Field("blend function", typeof(BlendFunction0Options))]
		public short BlendFunction0;
		[Field("blend src factor", typeof(BlendSrcFactor1Options))]
		public short BlendSrcFactor1;
		[Field("blend dst factor", typeof(BlendDstFactor2Options))]
		public short BlendDstFactor2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("blend color", null)]
		public ColorArgb BlendColor4;
		[Field("logic-op flags", typeof(LogicOpFlags5Options))]
		public short LogicOpFlags5;
		[Field("", null)]
		public fixed byte _6[2];
	}
}
#pragma warning restore CS1591
