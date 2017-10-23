using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(156, 4)]
	public unsafe struct DecoratorCacheBlockDataBlock
	{
		[Field("Placements*", null)]
		[Block("Decorator Placement Block", 32768, typeof(DecoratorPlacementBlock))]
		public TagBlock Placements0;
		[Field("Decal Vertices*", null)]
		[Block("Decal Vertices Block", 65536, typeof(DecalVerticesBlock))]
		public TagBlock DecalVertices1;
		[Field("Decal Indices*", null)]
		[Block("Indices Block", 65536, typeof(IndicesBlock))]
		public TagBlock DecalIndices2;
		[Field("", null)]
		public fixed byte _4[16];
		[Field("Sprite Vertices*", null)]
		[Block("Sprite Vertices Block", 65536, typeof(SpriteVerticesBlock))]
		public TagBlock SpriteVertices5;
		[Field("Sprite Indices*", null)]
		[Block("Indices Block", 65536, typeof(IndicesBlock))]
		public TagBlock SpriteIndices6;
		[Field("", null)]
		public fixed byte _8[16];
	}
}
#pragma warning restore CS1591
