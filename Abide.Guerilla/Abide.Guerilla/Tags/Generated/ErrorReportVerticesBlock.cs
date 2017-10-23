using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(52, 4)]
	public unsafe struct ErrorReportVerticesBlock
	{
		public Vector3 Position0;
		[Field("Node Index*", null)]
		public int NodeIndex2;
		[Field("Node Weight*", null)]
		public float NodeWeight5;
		[Field("Color*", null)]
		public ColorArgbF Color7;
		[Field("Screen Size*", null)]
		public float ScreenSize8;
	}
}
#pragma warning restore CS1591
