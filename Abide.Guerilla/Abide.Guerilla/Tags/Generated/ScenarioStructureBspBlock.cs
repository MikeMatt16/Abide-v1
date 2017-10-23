using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_structure_bsp", "sbsp", "����", typeof(ScenarioStructureBspBlock))]
	[FieldSet(792, 4)]
	public unsafe struct ScenarioStructureBspBlock
	{
		[Field("Import Info*", null)]
		[Block("Import Info", 1, typeof(GlobalTagImportInfoBlock))]
		public TagBlock ImportInfo0;
		[Field("", null)]
		public fixed byte _1[4];
		[Field("Collision Materials*", null)]
		[Block("Structure Collision Materials Block", 512, typeof(StructureCollisionMaterialsBlock))]
		public TagBlock CollisionMaterials2;
		[Field("Collision BSP*", null)]
		[Block("Global Collision Bsp Block", 1, typeof(GlobalCollisionBspBlock))]
		public TagBlock CollisionBSP3;
		[Field("Vehicle Floor:World Units#Height below which vehicles get pushed up by an unstoppable force.", null)]
		public float VehicleFloor4;
		[Field("Vehicle Ceiling:World Units#Height above which vehicles get pushed down by an unstoppable force.", null)]
		public float VehicleCeiling5;
		[Field("UNUSED nodes*", null)]
		[Block("UNUSED Structure Bsp Node Block", 131072, typeof(UNUSEDStructureBspNodeBlock))]
		public TagBlock UNUSEDNodes6;
		[Field("Leaves*", null)]
		[Block("Structure Bsp Leaf Block", 65536, typeof(StructureBspLeafBlock))]
		public TagBlock Leaves7;
		[Field("World Bounds x*", null)]
		public FloatBounds WorldBoundsX8;
		[Field("World Bounds y*", null)]
		public FloatBounds WorldBoundsY9;
		[Field("World Bounds z*", null)]
		public FloatBounds WorldBoundsZ10;
		[Field("Surface References*", null)]
		[Block("Structure Bsp Surface Reference Block", 262144, typeof(StructureBspSurfaceReferenceBlock))]
		public TagBlock SurfaceReferences11;
		[Field("Cluster Data*", null)]
		[Data(65536)]
		public TagBlock ClusterData12;
		[Field("Cluster Portals*", null)]
		[Block("Structure Bsp Cluster Portal Block", 512, typeof(StructureBspClusterPortalBlock))]
		public TagBlock ClusterPortals13;
		[Field("Fog Planes*", null)]
		[Block("Structure Bsp Fog Plane Block", 127, typeof(StructureBspFogPlaneBlock))]
		public TagBlock FogPlanes14;
		[Field("", null)]
		public fixed byte _15[24];
		[Field("Weather Palette*", null)]
		[Block("Structure Bsp Weather Palette Block", 32, typeof(StructureBspWeatherPaletteBlock))]
		public TagBlock WeatherPalette16;
		[Field("Weather Polyhedra*", null)]
		[Block("Structure Bsp Weather Polyhedron Block", 32, typeof(StructureBspWeatherPolyhedronBlock))]
		public TagBlock WeatherPolyhedra17;
		[Field("Detail Objects*", null)]
		[Block("Structure Bsp Detail Object Data Block", 1, typeof(StructureBspDetailObjectDataBlock))]
		public TagBlock DetailObjects18;
		[Field("Clusters*", null)]
		[Block("Structure Bsp Cluster Block", 512, typeof(StructureBspClusterBlock))]
		public TagBlock Clusters19;
		[Field("Materials*", null)]
		[Block("Material", 1024, typeof(GlobalGeometryMaterialBlock))]
		public TagBlock Materials20;
		[Field("Sky Owner Cluster*", null)]
		[Block("Structure Bsp Sky Owner Cluster Block", 32, typeof(StructureBspSkyOwnerClusterBlock))]
		public TagBlock SkyOwnerCluster21;
		[Field("Conveyor Surfaces*", null)]
		[Block("Structure Bsp Conveyor Surface Block", 512, typeof(StructureBspConveyorSurfaceBlock))]
		public TagBlock ConveyorSurfaces22;
		[Field("Breakable Surfaces*", null)]
		[Block("Structure Bsp Breakable Surface Block", 8448, typeof(StructureBspBreakableSurfaceBlock))]
		public TagBlock BreakableSurfaces23;
		[Field("Pathfinding Data", null)]
		[Block("Pathfinding Data Block", 16, typeof(PathfindingDataBlock))]
		public TagBlock PathfindingData24;
		[Field("Pathfinding Edges*", null)]
		[Block("Structure Bsp Pathfinding Edges Block", 262144, typeof(StructureBspPathfindingEdgesBlock))]
		public TagBlock PathfindingEdges25;
		[Field("Background Sound Palette*", null)]
		[Block("Structure Bsp Background Sound Palette Block", 64, typeof(StructureBspBackgroundSoundPaletteBlock))]
		public TagBlock BackgroundSoundPalette26;
		[Field("Sound Environment Palette*", null)]
		[Block("Structure Bsp Sound Environment Palette Block", 64, typeof(StructureBspSoundEnvironmentPaletteBlock))]
		public TagBlock SoundEnvironmentPalette27;
		[Field("Sound PAS Data*", null)]
		[Data(131072)]
		public TagBlock SoundPASData28;
		[Field("Markers*", null)]
		[Block("Structure Bsp Marker Block", 1024, typeof(StructureBspMarkerBlock))]
		public TagBlock Markers29;
		[Field("Runtime Decals*", null)]
		[Block("Structure Bsp Runtime Decal Block", 6144, typeof(StructureBspRuntimeDecalBlock))]
		public TagBlock RuntimeDecals30;
		[Field("Environment Object Palette*", null)]
		[Block("Structure Bsp Environment Object Palette Block", 100, typeof(StructureBspEnvironmentObjectPaletteBlock))]
		public TagBlock EnvironmentObjectPalette31;
		[Field("Environment Objects*", null)]
		[Block("Structure Bsp Environment Object Block", 16384, typeof(StructureBspEnvironmentObjectBlock))]
		public TagBlock EnvironmentObjects32;
		[Field("Lightmaps*", null)]
		[Block("Structure Bsp Lightmap Data Block", 128, typeof(StructureBspLightmapDataBlock))]
		public TagBlock Lightmaps33;
		[Field("", null)]
		public fixed byte _34[4];
		[Field("Leaf Map Leaves*", null)]
		[Block("Global Map Leaf Block", 65536, typeof(GlobalMapLeafBlock))]
		public TagBlock LeafMapLeaves35;
		[Field("Leaf Map Connections*", null)]
		[Block("Global Leaf Connection Block", 524288, typeof(GlobalLeafConnectionBlock))]
		public TagBlock LeafMapConnections36;
		[Field("Errors*", null)]
		[Block("Error Report Category", 64, typeof(GlobalErrorReportCategoriesBlock))]
		public TagBlock Errors37;
		[Field("Precomputed Lighting*", null)]
		[Block("Structure Bsp Precomputed Lighting Block", 350, typeof(StructureBspPrecomputedLightingBlock))]
		public TagBlock PrecomputedLighting38;
		[Field("Instanced Geometries Definitions*", null)]
		[Block("Structure Bsp Instanced Geometry Definition Block", 512, typeof(StructureBspInstancedGeometryDefinitionBlock))]
		public TagBlock InstancedGeometriesDefinitions39;
		[Field("Instanced Geometry Instances*", null)]
		[Block("Structure Bsp Instanced Geometry Instances Block", 1024, typeof(StructureBspInstancedGeometryInstancesBlock))]
		public TagBlock InstancedGeometryInstances40;
		[Field(")Ambience Sound Clusters", null)]
		[Block("Structure Bsp Sound Cluster Block", 512, typeof(StructureBspSoundClusterBlock))]
		public TagBlock AmbienceSoundClusters41;
		[Field(")Reverb Sound Clusters", null)]
		[Block("Structure Bsp Sound Cluster Block", 512, typeof(StructureBspSoundClusterBlock))]
		public TagBlock ReverbSoundClusters42;
		[Field("Transparent Planes*", null)]
		[Block("Transparent Planes Block", 32768, typeof(TransparentPlanesBlock))]
		public TagBlock TransparentPlanes43;
		[Field("", null)]
		public fixed byte _44[96];
		[Field("Vehicle Sperical Limit Radius#Distances this far and longer from limit origin will pull you back in.", null)]
		public float VehicleSpericalLimitRadius45;
		public Vector3 VehicleSpericalLimitCenter46;
		[Field("Debug Info*", null)]
		[Block("Structure Bsp Debug Info Block", 1, typeof(StructureBspDebugInfoBlock))]
		public TagBlock DebugInfo47;
		[Field("Decorators", null)]
		public TagReference Decorators48;
		[Field("structure_physics*", typeof(GlobalStructurePhysicsStructBlock))]
		[Block("Global Structure Physics Struct", 1, typeof(GlobalStructurePhysicsStructBlock))]
		public GlobalStructurePhysicsStructBlock StructurePhysics49;
		[Field("Water Definitions", null)]
		[Block("Global Water Definitions Block", 1, typeof(GlobalWaterDefinitionsBlock))]
		public TagBlock WaterDefinitions50;
		[Field(")portal=>device mapping", null)]
		[Block("Structure Portal Device Mapping Block", 1, typeof(StructurePortalDeviceMappingBlock))]
		public TagBlock PortalDeviceMapping51;
		[Field(")Audibility", null)]
		[Block("Structure Bsp Audibility Block", 1, typeof(StructureBspAudibilityBlock))]
		public TagBlock Audibility52;
		[Field(")Object Fake Lightprobes", null)]
		[Block("Structure Bsp Fake Lightprobes Block", 2048, typeof(StructureBspFakeLightprobesBlock))]
		public TagBlock ObjectFakeLightprobes53;
		[Field("Decorators", null)]
		[Block("Decorator Placement Definition Block", 1, typeof(DecoratorPlacementDefinitionBlock))]
		public TagBlock Decorators54;
	}
}
#pragma warning restore CS1591
