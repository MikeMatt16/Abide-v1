using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenery", "scen", "obje", typeof(SceneryBlock))]
	[FieldSet(8, 4)]
	public unsafe struct SceneryBlock
	{
		public enum PathfindingPolicy1Options
		{
			PathfindingCUTOUT_0 = 0,
			PathfindingSTATIC_1 = 1,
			PathfindingDYNAMIC_2 = 2,
			PathfindingNONE_3 = 3,
		}
		public enum Flags2Options
		{
			PhysicallySimulatesStimulates_0 = 1,
		}
		public enum LightmappingPolicy4Options
		{
			PerVertex_0 = 0,
			PerPixelNotImplemented_1 = 1,
			Dynamic_2 = 2,
		}
		[Field("pathfinding policy", typeof(PathfindingPolicy1Options))]
		public short PathfindingPolicy1;
		[Field("flags", typeof(Flags2Options))]
		public short Flags2;
		[Field("lightmapping policy", typeof(LightmappingPolicy4Options))]
		public short LightmappingPolicy4;
		[Field("", null)]
		public fixed byte _5[2];
		[Field("", null)]
		public fixed byte _6[120];
	}
}
#pragma warning restore CS1591
