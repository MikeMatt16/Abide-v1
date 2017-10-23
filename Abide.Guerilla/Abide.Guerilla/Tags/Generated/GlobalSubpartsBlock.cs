using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct GlobalSubpartsBlock
	{
		[Field("indices_start_index*", null)]
		public short IndicesStartIndex0;
		[Field("indices_length*", null)]
		public short IndicesLength1;
		[Field("visibility_bounds_index*", null)]
		public short VisibilityBoundsIndex2;
		[Field("Part Index*", null)]
		public short PartIndex3;
	}
}
#pragma warning restore CS1591
