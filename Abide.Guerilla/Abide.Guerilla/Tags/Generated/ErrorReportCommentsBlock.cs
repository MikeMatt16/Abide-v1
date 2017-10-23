using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(68, 4)]
	public unsafe struct ErrorReportCommentsBlock
	{
		[Field("Text*", null)]
		[Data(8192)]
		public TagBlock Text0;
		public Vector3 Position1;
		[Field("Node Index*", null)]
		public int NodeIndex3;
		[Field("Node Weight*", null)]
		public float NodeWeight6;
		[Field("Color*", null)]
		public ColorArgbF Color8;
	}
}
#pragma warning restore CS1591
