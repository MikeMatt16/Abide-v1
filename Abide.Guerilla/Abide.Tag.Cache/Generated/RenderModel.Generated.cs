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
    using HaloTag = Abide.HaloLibrary.TagFourCc;
    
    /// <summary>
    /// Represents the generated render_model (mode) tag group.
    /// </summary>
    public class RenderModel : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderModel"/> class.
        /// </summary>
        public RenderModel()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new RenderModelBlock());
        }
        /// <summary>
        /// Gets and returns the name of the render_model tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "render_model";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the render_model tag group.
        /// </summary>
        public override HaloTag GroupTag
        {
            get
            {
                return "mode";
            }
        }
    }
}