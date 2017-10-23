using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(52, 4)]
	public unsafe struct SoundGestaltExtraInfoBlock
	{
		[Field("encoded permutation section", null)]
		[Block("Sound Encoded Dialogue Section Block", 1, typeof(SoundEncodedDialogueSectionBlock))]
		public TagBlock EncodedPermutationSection0;
		[Field("geometry block info", typeof(GlobalGeometryBlockInfoStructBlock))]
		[Block("Global Geometry Block Info Struct", 1, typeof(GlobalGeometryBlockInfoStructBlock))]
		public GlobalGeometryBlockInfoStructBlock GeometryBlockInfo1;
	}
}
#pragma warning restore CS1591
