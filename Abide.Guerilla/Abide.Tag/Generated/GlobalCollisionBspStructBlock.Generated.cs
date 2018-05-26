//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.Tag.Generated
{
    using Abide.Tag;
    using System.IO;
    
    /// <summary>
    /// Represents the generated global_collision_bsp_struct_block tag block.
    /// </summary>
    public sealed class GlobalCollisionBspStructBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalCollisionBspStructBlock"/> class.
        /// </summary>
        public GlobalCollisionBspStructBlock()
        {
            this.Fields.Add(new BlockField<Bsp3dNodesBlock>("BSP 3D Nodes*", 131072));
            this.Fields.Add(new BlockField<PlanesBlock>("Planes*", 65536));
            this.Fields.Add(new BlockField<LeavesBlock>("Leaves*", 65536));
            this.Fields.Add(new BlockField<Bsp2dReferencesBlock>("BSP 2D References*", 131072));
            this.Fields.Add(new BlockField<Bsp2dNodesBlock>("BSP 2D Nodes*", 131072));
            this.Fields.Add(new BlockField<SurfacesBlock>("Surfaces*", 131072));
            this.Fields.Add(new BlockField<EdgesBlock>("Edges*", 262144));
            this.Fields.Add(new BlockField<VerticesBlock>("Vertices*", 131072));
        }
        /// <summary>
        /// Gets and returns the name of the global_collision_bsp_struct_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "global_collision_bsp_struct_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the global_collision_bsp_struct_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "global_collision_bsp_struct";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the global_collision_bsp_struct_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the global_collision_bsp_struct_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the global_collision_bsp_struct_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the global_collision_bsp_struct_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<Bsp3dNodesBlock>)(this.Fields[0])).WriteChildren(writer);
            ((BlockField<PlanesBlock>)(this.Fields[1])).WriteChildren(writer);
            ((BlockField<LeavesBlock>)(this.Fields[2])).WriteChildren(writer);
            ((BlockField<Bsp2dReferencesBlock>)(this.Fields[3])).WriteChildren(writer);
            ((BlockField<Bsp2dNodesBlock>)(this.Fields[4])).WriteChildren(writer);
            ((BlockField<SurfacesBlock>)(this.Fields[5])).WriteChildren(writer);
            ((BlockField<EdgesBlock>)(this.Fields[6])).WriteChildren(writer);
            ((BlockField<VerticesBlock>)(this.Fields[7])).WriteChildren(writer);
        }
    }
}
