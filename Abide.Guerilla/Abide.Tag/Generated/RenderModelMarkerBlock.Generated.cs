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
    
    /// <summary>
    /// Represents the generated render_model_marker_block tag block.
    /// </summary>
    public sealed class RenderModelMarkerBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderModelMarkerBlock"/> class.
        /// </summary>
        public RenderModelMarkerBlock()
        {
            this.Fields.Add(new CharIntegerField("region index*"));
            this.Fields.Add(new CharIntegerField("permutation index*"));
            this.Fields.Add(new CharIntegerField("node index*"));
            this.Fields.Add(new PadField("", 1));
            this.Fields.Add(new RealPoint3dField("translation*"));
            this.Fields.Add(new QuaternionField("rotation*"));
            this.Fields.Add(new RealField("scale"));
        }
        /// <summary>
        /// Gets and returns the name of the render_model_marker_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "render_model_marker_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the render_model_marker_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "marker";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the render_model_marker_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 256;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the render_model_marker_block tag block.
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
