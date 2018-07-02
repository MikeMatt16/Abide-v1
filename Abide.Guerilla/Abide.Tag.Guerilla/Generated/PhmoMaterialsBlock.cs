//------------------------------------------------------------------------------
// <hand-written>
//     This code was hand written because Mike is an idiot.
// </hand-written>
//------------------------------------------------------------------------------

namespace Abide.Tag.Guerilla.Generated
{
    using Abide.Tag;
    /// <summary>
    /// Represents the generated phmo_materials_block tag block.
    /// </summary>
    public sealed class PhmoMaterialsBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhmoMaterialsBlock"/> class.
        /// </summary>
        public PhmoMaterialsBlock()
        {
            this.Fields.Add(new StringIdField("name"));
            this.Fields.Add(new StringIdField("global material name"));
            this.Fields.Add(new ShortBlockIndexField("phantom type"));
            this.Fields.Add(new WordFlagsField("flags", "none", "does not collide with fixed bodies"));
        }
        /// <summary>
        /// Gets and returns the name of the phmo_materials_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "physics_model_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the phmo_materials_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "physics_model";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the phmo_materials_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1024;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the phmo_materials_block tag block.
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
