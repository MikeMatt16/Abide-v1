using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("vertex_shader", "vrtx", "����", typeof(VertexShaderBlock))]
	[FieldSet(20, 4)]
	public unsafe struct VertexShaderBlock
	{
		public enum Platform0Options
		{
			Pc_0 = 0,
			Xbox_1 = 1,
		}
		[Field("platform", typeof(Platform0Options))]
		public short Platform0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("geometry classifications", null)]
		[Block("Classification", 12, typeof(VertexShaderClassificationBlock))]
		public TagBlock GeometryClassifications2;
		[Field("output swizzles", null)]
		public int OutputSwizzles3;
	}
}
#pragma warning restore CS1591
