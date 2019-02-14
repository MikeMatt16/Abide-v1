//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.Tag.Cache.Generated
{
    using Abide.Tag;
    
    /// <summary>
    /// Represents the generated scenario_structure_bsp_block tag block.
    /// </summary>
    public sealed class ScenarioStructureBspBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioStructureBspBlock"/> class.
        /// </summary>
        public ScenarioStructureBspBlock()
        {
            this.Fields.Add(new BlockField<GlobalTagImportInfoBlock>("Import Info*", 1));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new BlockField<StructureCollisionMaterialsBlock>("Collision Materials*", 512));
            this.Fields.Add(new BlockField<GlobalCollisionBspBlock>("Collision BSP*", 1));
            this.Fields.Add(new RealField("Vehicle Floor:World Units#Height below which vehicles get pushed up by an unstopp" +
                        "able force."));
            this.Fields.Add(new RealField("Vehicle Ceiling:World Units#Height above which vehicles get pushed down by an uns" +
                        "toppable force."));
            this.Fields.Add(new BlockField<UNUSEDStructureBspNodeBlock>("UNUSED nodes*", 131072));
            this.Fields.Add(new BlockField<StructureBspLeafBlock>("Leaves*", 65536));
            this.Fields.Add(new RealBoundsField("World Bounds x*"));
            this.Fields.Add(new RealBoundsField("World Bounds y*"));
            this.Fields.Add(new RealBoundsField("World Bounds z*"));
            this.Fields.Add(new BlockField<StructureBspSurfaceReferenceBlock>("Surface References*", 262144));
            this.Fields.Add(new DataField("Cluster Data*", 1, 4));
            this.Fields.Add(new BlockField<StructureBspClusterPortalBlock>("Cluster Portals*", 512));
            this.Fields.Add(new BlockField<StructureBspFogPlaneBlock>("Fog Planes*", 127));
            this.Fields.Add(new PadField("", 24));
            this.Fields.Add(new BlockField<StructureBspWeatherPaletteBlock>("Weather Palette*", 32));
            this.Fields.Add(new BlockField<StructureBspWeatherPolyhedronBlock>("Weather Polyhedra*", 32));
            this.Fields.Add(new BlockField<StructureBspDetailObjectDataBlock>("Detail Objects*", 1));
            this.Fields.Add(new BlockField<StructureBspClusterBlock>("Clusters*", 512));
            this.Fields.Add(new BlockField<GlobalGeometryMaterialBlock>("Materials*", 1024));
            this.Fields.Add(new BlockField<StructureBspSkyOwnerClusterBlock>("Sky Owner Cluster*", 32));
            this.Fields.Add(new BlockField<StructureBspConveyorSurfaceBlock>("Conveyor Surfaces*", 512));
            this.Fields.Add(new BlockField<StructureBspBreakableSurfaceBlock>("Breakable Surfaces*", 8448));
            this.Fields.Add(new BlockField<PathfindingDataBlock>("Pathfinding Data", 16));
            this.Fields.Add(new BlockField<StructureBspPathfindingEdgesBlock>("Pathfinding Edges*", 262144));
            this.Fields.Add(new BlockField<StructureBspBackgroundSoundPaletteBlock>("Background Sound Palette*", 64));
            this.Fields.Add(new BlockField<StructureBspSoundEnvironmentPaletteBlock>("Sound Environment Palette*", 64));
            this.Fields.Add(new DataField("Sound PAS Data*", 1, 4));
            this.Fields.Add(new BlockField<StructureBspMarkerBlock>("Markers*", 1024));
            this.Fields.Add(new BlockField<StructureBspRuntimeDecalBlock>("Runtime Decals*", 6144));
            this.Fields.Add(new BlockField<StructureBspEnvironmentObjectPaletteBlock>("Environment Object Palette*", 100));
            this.Fields.Add(new BlockField<StructureBspEnvironmentObjectBlock>("Environment Objects*", 16384));
            this.Fields.Add(new BlockField<StructureBspLightmapDataBlock>("Lightmaps*", 128));
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new BlockField<GlobalMapLeafBlock>("Leaf Map Leaves*", 65536));
            this.Fields.Add(new BlockField<GlobalLeafConnectionBlock>("Leaf Map Connections*", 524288));
            this.Fields.Add(new BlockField<GlobalErrorReportCategoriesBlock>("Errors*", 64));
            this.Fields.Add(new BlockField<StructureBspPrecomputedLightingBlock>("Precomputed Lighting*", 350));
            this.Fields.Add(new BlockField<StructureBspInstancedGeometryDefinitionBlock>("Instanced Geometries Definitions*", 512));
            this.Fields.Add(new BlockField<StructureBspInstancedGeometryInstancesBlock>("Instanced Geometry Instances*", 1024));
            this.Fields.Add(new BlockField<StructureBspSoundClusterBlock>(")Ambience Sound Clusters", 512));
            this.Fields.Add(new BlockField<StructureBspSoundClusterBlock>(")Reverb Sound Clusters", 512));
            this.Fields.Add(new BlockField<TransparentPlanesBlock>("Transparent Planes*", 32768));
            this.Fields.Add(new PadField("", 96));
            this.Fields.Add(new RealField("Vehicle Sperical Limit Radius#Distances this far and longer from limit origin wil" +
                        "l pull you back in."));
            this.Fields.Add(new RealPoint3dField("Vehicle Sperical Limit Center#Center of space in which vehicle can move."));
            this.Fields.Add(new BlockField<StructureBspDebugInfoBlock>("Debug Info*", 1));
            this.Fields.Add(new TagReferenceField("Decorators", 1145389904));
            this.Fields.Add(new StructField<GlobalStructurePhysicsStructBlock>("structure_physics*"));
            this.Fields.Add(new BlockField<GlobalWaterDefinitionsBlock>("Water Definitions", 1));
            this.Fields.Add(new BlockField<StructurePortalDeviceMappingBlock>(")portal=>device mapping", 1));
            this.Fields.Add(new BlockField<StructureBspAudibilityBlock>(")Audibility", 1));
            this.Fields.Add(new BlockField<StructureBspFakeLightprobesBlock>(")Object Fake Lightprobes", 2048));
            this.Fields.Add(new BlockField<DecoratorPlacementDefinitionBlock>("Decorators", 1));
        }
        /// <summary>
        /// Gets and returns the name of the scenario_structure_bsp_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "scenario_structure_bsp_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the scenario_structure_bsp_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "scenario_structure_bsp";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the scenario_structure_bsp_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the scenario_structure_bsp_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
    }
}
