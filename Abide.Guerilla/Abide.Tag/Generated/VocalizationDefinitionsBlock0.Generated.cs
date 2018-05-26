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
    /// Represents the generated vocalization_definitions_block_0 tag block.
    /// </summary>
    public sealed class VocalizationDefinitionsBlock0 : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VocalizationDefinitionsBlock0"/> class.
        /// </summary>
        public VocalizationDefinitionsBlock0()
        {
            this.Fields.Add(new StringIdField("vocalization^"));
            this.Fields.Add(new StringIdField("parent vocalization"));
            this.Fields.Add(new ShortIntegerField("parent index*"));
            this.Fields.Add(new EnumField("priority", "none", "recall", "idle", "comment", "idle_response", "postcombat", "combat", "status", "respond", "warn", "act", "react", "involuntary", "scream", "scripted", "death"));
            this.Fields.Add(new LongFlagsField("flags", "immediate", "interrupt", "cancel low priority"));
            this.Fields.Add(new EnumField("glance behavior#how does the speaker of this vocalization direct his gaze?", "NONE", "glance subject short", "glance subject long", "glance cause short", "glance cause long", "glance friend short", "glance friend long"));
            this.Fields.Add(new EnumField("glance recipient behavior#how does someone who hears me behave?", "NONE", "glance subject short", "glance subject long", "glance cause short", "glance cause long", "glance friend short", "glance friend long"));
            this.Fields.Add(new EnumField("perception type", "none", "speaker", "listener"));
            this.Fields.Add(new EnumField("max combat status", "asleep", "idle", "alert", "active", "uninspected", "definite", "certain", "visible", "clear_los", "dangerous"));
            this.Fields.Add(new EnumField("animation impulse", "none", "shakefist", "cheer", "surprise-front", "surprise-back", "taunt", "brace", "point", "hold", "wave", "advance", "fallback"));
            this.Fields.Add(new EnumField("overlap priority", "none", "recall", "idle", "comment", "idle_response", "postcombat", "combat", "status", "respond", "warn", "act", "react", "involuntary", "scream", "scripted", "death"));
            this.Fields.Add(new RealField("sound repetition delay:minutes#Minimum delay time between playing the same permut" +
                        "ation"));
            this.Fields.Add(new RealField("allowable queue delay:seconds#How long to wait to actually start the vocalization" +
                        ""));
            this.Fields.Add(new RealField("pre voc. delay:seconds#How long to wait to actually start the vocalization"));
            this.Fields.Add(new RealField("notification delay:seconds#How long into the vocalization the AI should be notifi" +
                        "ed"));
            this.Fields.Add(new RealField("post voc. delay:seconds#How long speech is suppressed in the speaking unit after " +
                        "vocalizing"));
            this.Fields.Add(new RealField("repeat delay:seconds#How long before the same vocalization can be repeated"));
            this.Fields.Add(new RealField("weight:[0-1]#Inherent weight of this vocalization"));
            this.Fields.Add(new RealField("speaker freeze time#speaker won\'t move for the given amount of time"));
            this.Fields.Add(new RealField("listener freeze time#listener won\'t move for the given amount of time (from start" +
                        " of vocalization)"));
            this.Fields.Add(new EnumField("speaker emotion", "none", "asleep", "amorous", "happy", "inquisitive", "repulsed", "disappointed", "shocked", "scared", "arrogant", "annoyed", "angry", "pensive", "pain"));
            this.Fields.Add(new EnumField("listener emotion", "none", "asleep", "amorous", "happy", "inquisitive", "repulsed", "disappointed", "shocked", "scared", "arrogant", "annoyed", "angry", "pensive", "pain"));
            this.Fields.Add(new RealField("player skip fraction"));
            this.Fields.Add(new RealField("skip fraction"));
            this.Fields.Add(new StringIdField("Sample line"));
            this.Fields.Add(new BlockField<ResponseBlock>("reponses", 20));
            this.Fields.Add(new BlockField<VocalizationDefinitionsBlock1>("children", 500));
        }
        /// <summary>
        /// Gets and returns the name of the vocalization_definitions_block_0 tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "vocalization_definitions_block_0";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the vocalization_definitions_block_0 tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "vocalization_definitions_block_0";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the vocalization_definitions_block_0 tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 500;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the vocalization_definitions_block_0 tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the vocalization_definitions_block_0 tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the vocalization_definitions_block_0 tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<ResponseBlock>)(this.Fields[25])).WriteChildren(writer);
            ((BlockField<VocalizationDefinitionsBlock1>)(this.Fields[26])).WriteChildren(writer);
        }
    }
}
