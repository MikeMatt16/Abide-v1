using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("detail_object_collection", "dobc", "����", typeof(DetailObjectCollectionBlock))]
	[FieldSet(128, 4)]
	public unsafe struct DetailObjectCollectionBlock
	{
		public enum CollectionType0Options
		{
			ScreenFacing_0 = 0,
			ViewerFacing_1 = 1,
		}
		[Field("Collection Type", typeof(CollectionType0Options))]
		public short CollectionType0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("Global z Offset: Applied to all detail objects in this collection so they don't float above the ground.", null)]
		public float GlobalZOffset2;
		[Field("", null)]
		public fixed byte _3[44];
		[Field("Sprite Plate", null)]
		public TagReference SpritePlate4;
		[Field("Types", null)]
		[Block("Detail Object Type Block", 16, typeof(DetailObjectTypeBlock))]
		public TagBlock Types5;
		[Field("", null)]
		public fixed byte _6[48];
	}
}
#pragma warning restore CS1591
