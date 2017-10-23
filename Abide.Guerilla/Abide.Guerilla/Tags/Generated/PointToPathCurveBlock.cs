using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct PointToPathCurveBlock
	{
		[Field("name^*", null)]
		public StringId Name0;
		[Field("node index*", null)]
		public short NodeIndex1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("points*", null)]
		[Block("Point To Path Curve Point Block", 1024, typeof(PointToPathCurvePointBlock))]
		public TagBlock Points3;
	}
}
#pragma warning restore CS1591
