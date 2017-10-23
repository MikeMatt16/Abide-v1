using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(128, 4)]
	public unsafe struct AntennaVertexBlock
	{
		[Field("spring strength coefficient#strength of the spring (larger values make the spring stronger)", null)]
		public float SpringStrengthCoefficient0;
		[Field("", null)]
		public fixed byte _1[24];
		[Field("angles#direction toward next vertex", null)]
		public Vector2 Angles2;
		[Field("length:world units#distance between this vertex and the next", null)]
		public float Length3;
		[Field("sequence index#bitmap group sequence index for this vertex's texture", null)]
		public short SequenceIndex4;
		[Field("", null)]
		public fixed byte _5[2];
		[Field("color#color at this vertex", null)]
		public ColorArgbF Color6;
		[Field("LOD color#color at this vertex for the low-LOD line primitives", null)]
		public ColorArgbF LODColor7;
		[Field("", null)]
		public fixed byte _8[40];
		[Field("", null)]
		public fixed byte _9[12];
	}
}
#pragma warning restore CS1591
