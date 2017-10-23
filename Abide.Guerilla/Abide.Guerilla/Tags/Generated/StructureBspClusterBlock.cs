using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(216, 4)]
	public unsafe struct StructureBspClusterBlock
	{
		public enum Flags19Options
		{
			OneWayPortal_0 = 1,
			DoorPortal_1 = 2,
			PostprocessedGeometry_2 = 4,
			IsTheSky_3 = 8,
		}
		[Field("Section Info*", typeof(GlobalGeometrySectionInfoStructBlock))]
		[Block("Global Geometry Section Info Struct", 1, typeof(GlobalGeometrySectionInfoStructBlock))]
		public GlobalGeometrySectionInfoStructBlock SectionInfo0;
		[Field("Geometry Block Info*", typeof(GlobalGeometryBlockInfoStructBlock))]
		[Block("Global Geometry Block Info Struct", 1, typeof(GlobalGeometryBlockInfoStructBlock))]
		public GlobalGeometryBlockInfoStructBlock GeometryBlockInfo1;
		[Field("Cluster Data*", null)]
		[Block("Structure Bsp Cluster Data Block New", 1, typeof(StructureBspClusterDataBlockNew))]
		public TagBlock ClusterData2;
		[Field("Bounds x*", null)]
		public FloatBounds BoundsX4;
		[Field("Bounds y*", null)]
		public FloatBounds BoundsY5;
		[Field("Bounds z*", null)]
		public FloatBounds BoundsZ6;
		[Field("Scenario Sky Index*", null)]
		public int ScenarioSkyIndex7;
		[Field("Media Index*", null)]
		public int MediaIndex8;
		[Field("Scenario Visible Sky Index*", null)]
		public int ScenarioVisibleSkyIndex9;
		[Field("Scenario Atmospheric Fog Index*", null)]
		public int ScenarioAtmosphericFogIndex10;
		[Field("Planar Fog Designator*", null)]
		public int PlanarFogDesignator11;
		[Field("Visible Fog Plane Index*", null)]
		public int VisibleFogPlaneIndex12;
		[Field("Background Sound*", null)]
		public short BackgroundSound13;
		[Field("Sound Environment*", null)]
		public short SoundEnvironment14;
		[Field("Weather*", null)]
		public short Weather15;
		[Field("Transition Structure BSP", null)]
		public short TransitionStructureBSP16;
		[Field("", null)]
		public fixed byte _17[2];
		[Field("", null)]
		public fixed byte _18[4];
		[Field("Flags", typeof(Flags19Options))]
		public short Flags19;
		[Field("", null)]
		public fixed byte _20[2];
		[Field("Predicted Resources*", null)]
		[Block("Predicted Resource Block", 2048, typeof(PredictedResourceBlock))]
		public TagBlock PredictedResources21;
		[Field("Portals*", null)]
		[Block("Structure Bsp Cluster Portal Index Block", 512, typeof(StructureBspClusterPortalIndexBlock))]
		public TagBlock Portals22;
		[Field("Checksum from Structure*", null)]
		public int ChecksumFromStructure23;
		[Field("Instanced Geometry Indices*", null)]
		[Block("Structure Bsp Cluster Instanced Geometry Index Block", 1024, typeof(StructureBspClusterInstancedGeometryIndexBlock))]
		public TagBlock InstancedGeometryIndices24;
		[Field("Index Reorder Table*", null)]
		[Block("Index", 65535, typeof(GlobalGeometrySectionStripIndexBlock))]
		public TagBlock IndexReorderTable25;
		[Field("Collision mopp Code*", null)]
		[Data(1048576)]
		public TagBlock CollisionMoppCode26;
	}
}
#pragma warning restore CS1591
