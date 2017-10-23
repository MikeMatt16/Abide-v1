using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct ShaderStateFillStateBlock
	{
		public enum Flags0Options
		{
			FlatShading_0 = 1,
			EdgeAntialiasing_1 = 2,
		}
		public enum FillMode1Options
		{
			Solid_0 = 0,
			Wireframe_1 = 1,
			Points_2 = 2,
		}
		public enum BackFillMode2Options
		{
			Solid_0 = 0,
			Wireframe_1 = 1,
			Points_2 = 2,
		}
		[Field("flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("fill mode", typeof(FillMode1Options))]
		public short FillMode1;
		[Field("back fill mode", typeof(BackFillMode2Options))]
		public short BackFillMode2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("line width", null)]
		public float LineWidth4;
	}
}
#pragma warning restore CS1591
