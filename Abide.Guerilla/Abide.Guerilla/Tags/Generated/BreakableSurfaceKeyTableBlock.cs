using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct BreakableSurfaceKeyTableBlock
	{
		[Field("*Instanced Geometry Index", null)]
		public short InstancedGeometryIndex0;
		[Field("*Breakable Surface Index", null)]
		public short BreakableSurfaceIndex1;
		[Field("*Seed Surface Index", null)]
		public int SeedSurfaceIndex2;
		[Field("*x0", null)]
		public float X03;
		[Field("*x1", null)]
		public float X14;
		[Field("*y0", null)]
		public float Y05;
		[Field("*y1", null)]
		public float Y16;
		[Field("*z0", null)]
		public float Z07;
		[Field("*z1", null)]
		public float Z18;
	}
}
#pragma warning restore CS1591
