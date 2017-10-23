using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct ObjectChangeColorFunction
	{
		public enum ScaleFlags1Options
		{
			BlendInHsvBlendsColorsInHsvRatherThanRgbSpace_0 = 1,
			MoreColorsBlendsColorsThroughMoreHuesGoesTheLongWayAroundTheColorWheel_1 = 2,
		}
		[Field("", null)]
		public fixed byte _0[4];
		[Field("scale flags", typeof(ScaleFlags1Options))]
		public int ScaleFlags1;
		[Field("color lower bound", null)]
		public ColorRgbF ColorLowerBound2;
		[Field("color upper bound", null)]
		public ColorRgbF ColorUpperBound3;
		[Field("darken by", null)]
		public StringId DarkenBy4;
		[Field("scale by", null)]
		public StringId ScaleBy5;
	}
}
#pragma warning restore CS1591
