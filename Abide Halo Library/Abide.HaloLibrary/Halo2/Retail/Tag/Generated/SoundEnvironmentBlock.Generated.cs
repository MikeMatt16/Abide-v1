//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.HaloLibrary.Halo2.Retail.Tag.Generated
{
    using System;
    using Abide.HaloLibrary;
    using Abide.HaloLibrary.Halo2.Retail.Tag;
    
    /// <summary>
    /// Represents the generated sound_environment_block tag block.
    /// </summary>
    internal sealed class SoundEnvironmentBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundEnvironmentBlock"/> class.
        /// </summary>
        public SoundEnvironmentBlock()
        {
            this.Fields.Add(new PadField("", 4));
            this.Fields.Add(new ShortIntegerField("priority#when multiple listeners are in different sound environments in split scr" +
                        "een, the combined environment will be the one with the highest priority."));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new RealField("room intensity:dB#intensity of the room effect"));
            this.Fields.Add(new RealField("room intensity hf:dB#intensity of the room effect above the reference high freque" +
                        "ncy"));
            this.Fields.Add(new RealField("room rolloff (0 to 10)#how quickly the room effect rolls off, from 0.0 to 10.0"));
            this.Fields.Add(new RealField("decay time (.1 to 20):seconds"));
            this.Fields.Add(new RealField("decay hf ratio (.1 to 2)"));
            this.Fields.Add(new RealField("reflections intensity:dB[-100,10]"));
            this.Fields.Add(new RealField("reflections delay (0 to .3):seconds"));
            this.Fields.Add(new RealField("reverb intensity:dB[-100,20]"));
            this.Fields.Add(new RealField("reverb delay (0 to .1):seconds"));
            this.Fields.Add(new RealField("diffusion"));
            this.Fields.Add(new RealField("density"));
            this.Fields.Add(new RealField("hf reference(20 to 20,000):Hz#for hf values, what frequency defines hf, from 20 t" +
                        "o 20,000"));
            this.Fields.Add(new PadField("", 16));
        }
        /// <summary>
        /// Gets and returns the name of the sound_environment_block tag block.
        /// </summary>
        public override string BlockName
        {
            get
            {
                return "sound_environment_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the sound_environment_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "sound_environment";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the sound_environment_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the sound_environment_block tag block.
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