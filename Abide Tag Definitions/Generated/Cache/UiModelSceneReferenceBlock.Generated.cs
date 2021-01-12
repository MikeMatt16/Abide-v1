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
    using System;
    using Abide.HaloLibrary;
    using Abide.Tag;
    
    /// <summary>
    /// Represents the generated ui_model_scene_reference_block tag block.
    /// </summary>
    public sealed class UiModelSceneReferenceBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UiModelSceneReferenceBlock"/> class.
        /// </summary>
        public UiModelSceneReferenceBlock()
        {
            this.Fields.Add(new ExplanationField("NOTE on coordinate systems", @"Halo y-axis=ui z-axis, and Halo z-axis=ui y-axis.
As a convention, let's always place objects in the ui scenario such that
they are facing in the '-y' direction, and the camera such that is is
facing the '+y' direction. This way the ui animation for models (which
gets applied to the camera) will always be consisitent."));
            this.Fields.Add(new LongFlagsField("flags", "unused"));
            this.Fields.Add(new EnumField("animation index", "NONE", "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63"));
            this.Fields.Add(new ShortIntegerField("intro animation delay milliseconds"));
            this.Fields.Add(new ShortIntegerField("render depth bias"));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new BlockField<UiObjectReferenceBlock>("objects", 32));
            this.Fields.Add(new BlockField<UiLightReferenceBlock>("lights", 8));
            this.Fields.Add(new RealVector3dField("animation scale factor"));
            this.Fields.Add(new RealPoint3dField("camera position"));
            this.Fields.Add(new RealField("fov degress"));
            this.Fields.Add(new Rectangle2dField("ui viewport"));
            this.Fields.Add(new StringIdField("UNUSED intro anim"));
            this.Fields.Add(new StringIdField("UNUSED outro anim"));
            this.Fields.Add(new StringIdField("UNUSED ambient anim"));
        }
        /// <summary>
        /// Gets and returns the name of the ui_model_scene_reference_block tag block.
        /// </summary>
        public override string BlockName
        {
            get
            {
                return "ui_model_scene_reference_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the ui_model_scene_reference_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "ui_model_scene_reference_block";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the ui_model_scene_reference_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 32;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the ui_model_scene_reference_block tag block.
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