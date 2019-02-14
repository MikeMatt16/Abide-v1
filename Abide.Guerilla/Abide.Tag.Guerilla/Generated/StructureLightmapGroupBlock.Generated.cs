//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.Tag.Guerilla.Generated
{
    using Abide.Tag;
    
    /// <summary>
    /// Represents the generated structure_lightmap_group_block tag block.
    /// </summary>
    public sealed class StructureLightmapGroupBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StructureLightmapGroupBlock"/> class.
        /// </summary>
        public StructureLightmapGroupBlock()
        {
            this.Fields.Add(new EnumField("type", "normal"));
            this.Fields.Add(new WordFlagsField("flags", "unused"));
            this.Fields.Add(new LongIntegerField("structure checksum"));
            this.Fields.Add(new BlockField<StructureLightmapPaletteColorBlock>("section palette", 128));
            this.Fields.Add(new BlockField<StructureLightmapPaletteColorBlock>("writable palettes", 128));
            this.Fields.Add(new TagReferenceField("bitmap group", 1651078253));
            this.Fields.Add(new BlockField<LightmapGeometrySectionBlock>("clusters", 512));
            this.Fields.Add(new BlockField<LightmapGeometryRenderInfoBlock>("cluster render info", 1024));
            this.Fields.Add(new BlockField<LightmapGeometrySectionBlock>("poop definitions", 512));
            this.Fields.Add(new BlockField<StructureLightmapLightingEnvironmentBlock>("lighting environments*", 1024));
            this.Fields.Add(new BlockField<LightmapVertexBufferBucketBlock>("geometry buckets", 1024));
            this.Fields.Add(new BlockField<LightmapGeometryRenderInfoBlock>("instance render info", 1024));
            this.Fields.Add(new BlockField<LightmapInstanceBucketReferenceBlock>("instance bucket refs", 2000));
            this.Fields.Add(new BlockField<LightmapSceneryObjectInfoBlock>("scenery object info", 2000));
            this.Fields.Add(new BlockField<LightmapInstanceBucketReferenceBlock>("scenery object bucket refs", 2000));
        }
        /// <summary>
        /// Gets and returns the name of the structure_lightmap_group_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "structure_lightmap_group_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the structure_lightmap_group_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "structure_lightmap_group_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the structure_lightmap_group_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 256;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the structure_lightmap_group_block tag block.
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
