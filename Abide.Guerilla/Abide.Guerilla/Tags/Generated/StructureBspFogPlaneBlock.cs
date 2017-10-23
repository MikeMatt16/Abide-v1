using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct StructureBspFogPlaneBlock
	{
		public enum Flags3Options
		{
			ExtendInfinitelyWhileVisible_0 = 1,
			DoNotFloodfill_1 = 2,
			AggressiveFloodfill_2 = 4,
		}
		[Field("Scenario Planar Fog Index*", null)]
		public short ScenarioPlanarFogIndex0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("plane*", null)]
		public Plane3 Plane2;
		[Field("Flags*", typeof(Flags3Options))]
		public short Flags3;
		[Field("Priority*", null)]
		public short Priority4;
	}
}
#pragma warning restore CS1591
