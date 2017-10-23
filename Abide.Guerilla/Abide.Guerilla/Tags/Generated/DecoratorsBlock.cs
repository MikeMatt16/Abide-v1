using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("decorators", "DECP", "����", typeof(DecoratorsBlock))]
	[FieldSet(64, 4)]
	public unsafe struct DecoratorsBlock
	{
		public Vector3 GridOrigin0;
		[Field("Cell Count per Dimension", null)]
		public int CellCountPerDimension1;
		[Field("Cache Blocks", null)]
		[Block("Decorator Cache Block Block", 4096, typeof(DecoratorCacheBlockBlock))]
		public TagBlock CacheBlocks2;
		[Field("Groups", null)]
		[Block("Decorator Group Block", 131072, typeof(DecoratorGroupBlock))]
		public TagBlock Groups3;
		[Field("Cells", null)]
		[Block("Decorator Cell Collection Block", 65535, typeof(DecoratorCellCollectionBlock))]
		public TagBlock Cells4;
		[Field("Decals", null)]
		[Block("Decorator Projected Decal Block", 32768, typeof(DecoratorProjectedDecalBlock))]
		public TagBlock Decals5;
	}
}
#pragma warning restore CS1591
