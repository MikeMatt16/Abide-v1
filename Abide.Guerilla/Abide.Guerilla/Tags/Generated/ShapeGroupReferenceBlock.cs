using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct ShapeGroupReferenceBlock
	{
		[Field("shapes", null)]
		[Block("Shape Block Reference Block", 32, typeof(ShapeBlockReferenceBlock))]
		public TagBlock Shapes1;
		[Field("model scene blocks", null)]
		[Block("Ui Model Scene Reference Block", 32, typeof(UiModelSceneReferenceBlock))]
		public TagBlock ModelSceneBlocks3;
		[Field("bitmap blocks", null)]
		[Block("Bitmap Block Reference Block", 64, typeof(BitmapBlockReferenceBlock))]
		public TagBlock BitmapBlocks5;
	}
}
#pragma warning restore CS1591
