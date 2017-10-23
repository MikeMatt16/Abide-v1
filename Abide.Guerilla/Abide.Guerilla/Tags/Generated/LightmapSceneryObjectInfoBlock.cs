using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct LightmapSceneryObjectInfoBlock
	{
		[Field("unique ID", null)]
		public int UniqueID0;
		[Field("origin BSP index", null)]
		public short OriginBSPIndex1;
		[Field("type", null)]
		public int Type2;
		[Field("source", null)]
		public int Source3;
		[Field("render model checksum", null)]
		public int RenderModelChecksum4;
	}
}
#pragma warning restore CS1591
