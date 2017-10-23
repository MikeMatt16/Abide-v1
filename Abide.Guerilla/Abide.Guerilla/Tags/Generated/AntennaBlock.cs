using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("antenna", "ant!", "����", typeof(AntennaBlock))]
	[FieldSet(180, 4)]
	public unsafe struct AntennaBlock
	{
		[Field("bitmaps", null)]
		public TagReference Bitmaps1;
		[Field("physics", null)]
		public TagReference Physics2;
		[Field("", null)]
		public fixed byte _3[80];
		[Field("spring strength coefficient#strength of the spring (larger values make the spring stronger)", null)]
		public float SpringStrengthCoefficient4;
		[Field("falloff pixels", null)]
		public float FalloffPixels5;
		[Field("cutoff pixels", null)]
		public float CutoffPixels6;
		[Field("", null)]
		public fixed byte _7[40];
		[Field("vertices", null)]
		[Block("Antenna Vertex Block", 20, typeof(AntennaVertexBlock))]
		public TagBlock Vertices8;
	}
}
#pragma warning restore CS1591
