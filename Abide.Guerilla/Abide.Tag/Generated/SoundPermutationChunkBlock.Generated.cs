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
    /// Represents the generated sound_permutation_chunk_block tag block.
    /// </summary>
    public sealed class SoundPermutationChunkBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundPermutationChunkBlock"/> class.
        /// </summary>
        public SoundPermutationChunkBlock()
        {
            this.Fields.Add(new LongIntegerField("file offset*"));
            this.Fields.Add(new LongIntegerField(""));
            this.Fields.Add(new LongIntegerField(""));
        }
        /// <summary>
        /// Gets and returns the name of the sound_permutation_chunk_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "sound_permutation_chunk_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the sound_permutation_chunk_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "sound_permutation_chunk_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the sound_permutation_chunk_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 32767;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the sound_permutation_chunk_block tag block.
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
