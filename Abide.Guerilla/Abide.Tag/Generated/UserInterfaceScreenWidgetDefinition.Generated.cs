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
    /// Represents the generated user_interface_screen_widget_definition (wgit) tag group.
    /// </summary>
    public class UserInterfaceScreenWidgetDefinition : Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserInterfaceScreenWidgetDefinition"/> class.
        /// </summary>
        public UserInterfaceScreenWidgetDefinition()
        {
            // Add tag block to list.
            this.TagBlocks.Add(new UserInterfaceScreenWidgetDefinitionBlock());
        }
        /// <summary>
        /// Gets and returns the name of the user_interface_screen_widget_definition tag group.
        /// </summary>
        public override string Name
        {
            get
            {
                return "user_interface_screen_widget_definition";
            }
        }
        /// <summary>
        /// Gets and returns the group tag of the user_interface_screen_widget_definition tag group.
        /// </summary>
        public override HaloTag GroupTag
        {
            get
            {
                return "wgit";
            }
        }
    }
}
