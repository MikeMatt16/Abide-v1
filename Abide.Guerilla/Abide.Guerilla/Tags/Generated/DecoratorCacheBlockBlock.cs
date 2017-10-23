using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(60, 4)]
	public unsafe struct DecoratorCacheBlockBlock
	{
		[Field("Geometry Block Info*", typeof(GlobalGeometryBlockInfoStructBlock))]
		[Block("Global Geometry Block Info Struct", 1, typeof(GlobalGeometryBlockInfoStructBlock))]
		public GlobalGeometryBlockInfoStructBlock GeometryBlockInfo0;
		[Field("Cache Block Data*", null)]
		[Block("Decorator Cache Block Data Block", 1, typeof(DecoratorCacheBlockDataBlock))]
		public TagBlock CacheBlockData1;
		[Field("", null)]
		public fixed byte _2[4];
		[Field("", null)]
		public fixed byte _3[4];
	}
}
#pragma warning restore CS1591
