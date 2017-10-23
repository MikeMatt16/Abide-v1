using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 8)]
	public unsafe struct SurfacesBlock
	{
		public enum Flags2Options
		{
			TwoSided_0 = 1,
			Invisible_1 = 2,
			Climbable_2 = 4,
			Breakable_3 = 8,
			Invalid_4 = 16,
			Conveyor_5 = 32,
		}
		[Field("Plane*", null)]
		public short Plane0;
		[Field("First Edge*", null)]
		public short FirstEdge1;
		[Field("Flags*", typeof(Flags2Options))]
		public byte Flags2;
		[Field("Breakable Surface*", null)]
		public int BreakableSurface3;
		[Field("Material*", null)]
		public short Material4;
	}
}
#pragma warning restore CS1591
