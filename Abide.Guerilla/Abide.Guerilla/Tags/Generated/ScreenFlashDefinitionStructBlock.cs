using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct ScreenFlashDefinitionStructBlock
	{
		public enum Type0Options
		{
			None_0 = 0,
			Lighten_1 = 1,
			Darken_2 = 2,
			Max_3 = 3,
			Min_4 = 4,
			Invert_5 = 5,
			Tint_6 = 6,
		}
		public enum Priority1Options
		{
			Low_0 = 0,
			Medium_1 = 1,
			High_2 = 2,
		}
		public enum FadeFunction4Options
		{
			Linear_0 = 0,
			Late_1 = 1,
			VeryLate_2 = 2,
			Early_3 = 3,
			VeryEarly_4 = 4,
			Cosine_5 = 5,
			Zero_6 = 6,
			One_7 = 7,
		}
		[Field("type", typeof(Type0Options))]
		public short Type0;
		[Field("priority", typeof(Priority1Options))]
		public short Priority1;
		[Field("", null)]
		public fixed byte _2[12];
		[Field("duration:seconds", null)]
		public float Duration3;
		[Field("fade function", typeof(FadeFunction4Options))]
		public short FadeFunction4;
		[Field("", null)]
		public fixed byte _5[2];
		[Field("", null)]
		public fixed byte _6[8];
		[Field("maximum intensity:[0,1]", null)]
		public float MaximumIntensity7;
		[Field("", null)]
		public fixed byte _8[4];
		[Field("color", null)]
		public ColorArgbF Color9;
	}
}
#pragma warning restore CS1591
