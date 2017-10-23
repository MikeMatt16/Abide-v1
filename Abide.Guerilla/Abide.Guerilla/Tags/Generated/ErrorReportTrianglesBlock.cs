using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(112, 4)]
	public unsafe struct ErrorReportTrianglesBlock
	{
		public Vector3 Position1;
		[Field("Node Index*", null)]
		public int NodeIndex3;
		[Field("Node Weight*", null)]
		public float NodeWeight6;
		[Field("Color*", null)]
		public ColorArgbF Color9;
	}
}
#pragma warning restore CS1591
