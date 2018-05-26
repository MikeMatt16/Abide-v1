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
    /// Represents the generated render_state_parameter_block tag block.
    /// </summary>
    public sealed class RenderStateParameterBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderStateParameterBlock"/> class.
        /// </summary>
        public RenderStateParameterBlock()
        {
            this.Fields.Add(new CharIntegerField("parameter index"));
            this.Fields.Add(new CharIntegerField("parameter type"));
            this.Fields.Add(new CharIntegerField("state index"));
        }
        /// <summary>
        /// Gets and returns the name of the render_state_parameter_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "render_state_parameter_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the render_state_parameter_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "render_state_parameter_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the render_state_parameter_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1024;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the render_state_parameter_block tag block.
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
