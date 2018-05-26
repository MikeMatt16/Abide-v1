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
    /// Represents the generated animation_pool_block tag block.
    /// </summary>
    public sealed class AnimationPoolBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationPoolBlock"/> class.
        /// </summary>
        public AnimationPoolBlock()
        {
            this.Fields.Add(new StringIdField("name*^"));
            this.Fields.Add(new LongIntegerField("node list checksum*"));
            this.Fields.Add(new LongIntegerField("production checksum*"));
            this.Fields.Add(new LongIntegerField("import_checksum*"));
            this.Fields.Add(new CharEnumField("type*", "base", "overlay", "replacement"));
            this.Fields.Add(new CharEnumField("frame info type*", "none", "dx,dy", "dx,dy,dyaw", "dx,dy,dz,dyaw"));
            this.Fields.Add(new CharBlockIndexField("blend screen"));
            this.Fields.Add(new CharIntegerField("node count*"));
            this.Fields.Add(new ShortIntegerField("frame count*"));
            this.Fields.Add(new ByteFlagsField("internal flags*", "<unused0>!", "world relative", "<unused1>!", "<unused2>!", "<unused3>!", "compression disabled", "old production checksum", "valid production checksum"));
            this.Fields.Add(new ByteFlagsField("production flags", "do not monitor changes", "verify sound events", "do not inherit for player graphs"));
            this.Fields.Add(new WordFlagsField("playback flags", "disable interpolation_in", "disable interpolation_out", "disable mode ik", "disable weapon ik", "disable weapon aim/1st person", "disable look screen", "disable transition adjustment"));
            this.Fields.Add(new CharEnumField("desired compression", "best score", "best compression", "best accuracy", "best fullframe", "best small keyframe", "best large keyframe"));
            this.Fields.Add(new CharEnumField("current compression*", "best score", "best compression", "best accuracy", "best fullframe", "best small keyframe", "best large keyframe"));
            this.Fields.Add(new RealField("weight"));
            this.Fields.Add(new LongIntegerField("parent graph index!"));
            this.Fields.Add(new LongIntegerField("parent graph block index!"));
            this.Fields.Add(new LongIntegerField("parent graph block offset!"));
            this.Fields.Add(new ShortIntegerField("parent graph starting point index!"));
            this.Fields.Add(new ShortIntegerField("loop frame index"));
            this.Fields.Add(new ShortBlockIndexField("parent animation*"));
            this.Fields.Add(new ShortBlockIndexField("next animation*"));
            this.Fields.Add(new DataField("animation data*"));
            this.Fields.Add(new StructField<PackedDataSizesStructBlock>("data sizes*"));
            this.Fields.Add(new BlockField<AnimationFrameEventBlock>("frame events|ABCDCC", 512));
            this.Fields.Add(new BlockField<AnimationSoundEventBlock>("sound events|ABCDCC", 512));
            this.Fields.Add(new BlockField<AnimationEffectEventBlock>("effect events|ABCDCC", 512));
            this.Fields.Add(new BlockField<ObjectSpaceNodeDataBlock>("object-space parent nodes|ABCDCC", 255));
        }
        /// <summary>
        /// Gets and returns the name of the animation_pool_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "animation_pool_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the animation_pool_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "animation_pool_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the animation_pool_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 2048;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the animation_pool_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the animation_pool_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the animation_pool_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<AnimationFrameEventBlock>)(this.Fields[24])).WriteChildren(writer);
            ((BlockField<AnimationSoundEventBlock>)(this.Fields[25])).WriteChildren(writer);
            ((BlockField<AnimationEffectEventBlock>)(this.Fields[26])).WriteChildren(writer);
            ((BlockField<ObjectSpaceNodeDataBlock>)(this.Fields[27])).WriteChildren(writer);
        }
    }
}
