using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("decorator_set", "DECR", "����", typeof(DecoratorSetBlock))]
	[FieldSet(140, 4)]
	public unsafe struct DecoratorSetBlock
	{
		[Field("shaders", null)]
		[Block("Decorator Shader Reference Block", 8, typeof(DecoratorShaderReferenceBlock))]
		public TagBlock Shaders0;
		[Field("lighting min scale#0.0 defaults to 0.4", null)]
		public float LightingMinScale1;
		[Field("lighting max scale#0.0 defaults to 2.0", null)]
		public float LightingMaxScale2;
		[Field("classes", null)]
		[Block("Decorator Classes Block", 8, typeof(DecoratorClassesBlock))]
		public TagBlock Classes3;
		[Field("models*", null)]
		[Block("Decorator Models Block", 256, typeof(DecoratorModelsBlock))]
		public TagBlock Models4;
		[Field("raw vertices*", null)]
		[Block("Decorator Model Vertices Block", 32768, typeof(DecoratorModelVerticesBlock))]
		public TagBlock RawVertices5;
		[Field("indices*", null)]
		[Block("Decorator Model Indices Block", 32768, typeof(DecoratorModelIndicesBlock))]
		public TagBlock Indices6;
		[Field("cached data", null)]
		[Block("Cached Data Block", 1, typeof(CachedDataBlock))]
		public TagBlock CachedData7;
		[Field("geometry section info", typeof(GlobalGeometryBlockInfoStructBlock))]
		[Block("Global Geometry Block Info Struct", 1, typeof(GlobalGeometryBlockInfoStructBlock))]
		public GlobalGeometryBlockInfoStructBlock GeometrySectionInfo8;
		[Field("", null)]
		public fixed byte _9[16];
		[Field("", null)]
		public fixed byte _10[4];
	}
}
#pragma warning restore CS1591
