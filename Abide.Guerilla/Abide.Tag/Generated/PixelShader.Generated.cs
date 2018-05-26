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
    using HaloTag = Abide.HaloLibrary.Tag;
    
    /// <summary>
    /// Represents the generated pixel_shader (pixl) tag group.
    /// </summary>
    public class PixelShader : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PixelShader"/> class.
        /// </summary>
        public PixelShader()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new PixelShaderBlock());
        }
        /// <summary>
        /// Gets and returns the name of the pixel_shader tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "pixel_shader";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the pixel_shader tag group.
        /// </summary>
        public override HaloTag GroupTag
        {
            get
            {
                return "pixl";
            }
        }
    }
}
