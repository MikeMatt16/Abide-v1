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
    /// Represents the generated camera_track (trak) tag group.
    /// </summary>
    public class CameraTrack : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraTrack"/> class.
        /// </summary>
        public CameraTrack()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new CameraTrackBlock());
        }
        /// <summary>
        /// Gets and returns the name of the camera_track tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "camera_track";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the camera_track tag group.
        /// </summary>
        public override HaloTag GroupTag
        {
            get
            {
                return "trak";
            }
        }
    }
}
