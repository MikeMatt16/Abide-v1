using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("cellular_automata2d", "whip", "����", typeof(CellularAutomata2dBlock))]
	[FieldSet(556, 4)]
	public unsafe struct CellularAutomata2dBlock
	{
		public enum InterpolationFlags14Options
		{
			BlendInHsvBlendsColorsInHsvRatherThanRgbSpace_0 = 1,
			MoreColorsBlendsColorsThroughMoreHuesGoesTheLongWayAroundTheColorWheel_1 = 2,
		}
		[Field("updates per second:Hz", null)]
		public short UpdatesPerSecond1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("dead cell penalty", null)]
		public float DeadCellPenalty3;
		[Field("live cell bonus", null)]
		public float LiveCellBonus4;
		[Field("", null)]
		public fixed byte _5[80];
		[Field("width:cells", null)]
		public short Width7;
		[Field("height:cells", null)]
		public short Height8;
		[Field("cell width:world units", null)]
		public float CellWidth9;
		[Field("height:world units", null)]
		public float Height10;
		[Field("velocity:cells/update", null)]
		public Vector2 Velocity11;
		[Field("", null)]
		public fixed byte _12[28];
		[Field("interpolation flags", typeof(InterpolationFlags14Options))]
		public int InterpolationFlags14;
		[Field("base color", null)]
		public ColorRgbF BaseColor15;
		[Field("peak color", null)]
		public ColorRgbF PeakColor16;
		[Field("", null)]
		public fixed byte _17[76];
		[Field("width:cells", null)]
		public short Width19;
		[Field("height:cells", null)]
		public short Height20;
		[Field("cell width:world units", null)]
		public float CellWidth21;
		[Field("velocity:cells/update", null)]
		public Vector2 Velocity22;
		[Field("", null)]
		public fixed byte _23[48];
		[Field("texture width:cells", null)]
		public short TextureWidth25;
		[Field("", null)]
		public fixed byte _26[2];
		[Field("", null)]
		public fixed byte _27[48];
		[Field("texture", null)]
		public TagReference Texture28;
		[Field("", null)]
		public fixed byte _29[160];
		[Field("rules", null)]
		[Block("Rules Block", 16, typeof(RulesBlock))]
		public TagBlock Rules30;
	}
}
#pragma warning restore CS1591
