using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct DecoratorGroupBlock
	{
		public enum DecoratorType1Options
		{
			Model_0 = 0,
			FloatingDecal_1 = 1,
			ProjectedDecal_2 = 2,
			ScreenFacingQuad_3 = 3,
			AxisRotatingQuad_4 = 4,
			CrossQuad_5 = 5,
		}
		[Field("Decorator Set*", null)]
		public byte DecoratorSet0;
		[Field("Decorator Type", typeof(DecoratorType1Options))]
		public byte DecoratorType1;
		[Field("Shader Index*", null)]
		public int ShaderIndex2;
		[Field("Compressed Radius*", null)]
		public int CompressedRadius3;
		[Field("Cluster*", null)]
		public short Cluster4;
		[Field("Cache Block*", null)]
		public short CacheBlock5;
		[Field("Decorator Start Index*", null)]
		public short DecoratorStartIndex6;
		[Field("Decorator Count*", null)]
		public short DecoratorCount7;
		[Field("Vertex Start Offset*", null)]
		public short VertexStartOffset8;
		[Field("Vertex Count*", null)]
		public short VertexCount9;
		[Field("Index Start Offset*", null)]
		public short IndexStartOffset10;
		[Field("Index Count*", null)]
		public short IndexCount11;
		[Field("Compressed Bounding Center*", null)]
		public int CompressedBoundingCenter12;
	}
}
#pragma warning restore CS1591
