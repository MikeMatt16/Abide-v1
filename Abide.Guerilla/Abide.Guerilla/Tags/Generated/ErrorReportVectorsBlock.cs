using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct ErrorReportVectorsBlock
	{
		public Vector3 Position0;
		[Field("Node Index*", null)]
		public int NodeIndex2;
		[Field("Node Weight*", null)]
		public float NodeWeight5;
		[Field("Color*", null)]
		public ColorArgbF Color7;
		[Field("Normal*", null)]
		public Vector3 Normal8;
		[Field("Screen Length*", null)]
		public float ScreenLength9;
	}
}
#pragma warning restore CS1591
