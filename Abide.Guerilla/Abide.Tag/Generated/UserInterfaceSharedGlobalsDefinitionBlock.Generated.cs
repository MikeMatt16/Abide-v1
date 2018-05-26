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
    /// Represents the generated user_interface_shared_globals_definition_block tag block.
    /// </summary>
    public sealed class UserInterfaceSharedGlobalsDefinitionBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserInterfaceSharedGlobalsDefinitionBlock"/> class.
        /// </summary>
        public UserInterfaceSharedGlobalsDefinitionBlock()
        {
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new PadField("", 2));
            this.Fields.Add(new PadField("", 16));
            this.Fields.Add(new PadField("", 8));
            this.Fields.Add(new PadField("", 8));
            this.Fields.Add(new PadField("", 16));
            this.Fields.Add(new PadField("", 8));
            this.Fields.Add(new PadField("", 8));
            this.Fields.Add(new ExplanationField("UI Rendering Globals", null));
            this.Fields.Add(new RealField("overlayed screen alpha mod"));
            this.Fields.Add(new ShortIntegerField("inc. text update period:milliseconds"));
            this.Fields.Add(new ShortIntegerField("inc. text block character:ASCII code"));
            this.Fields.Add(new RealField("callout text scale"));
            this.Fields.Add(new RealArgbColorField("progress bar color"));
            this.Fields.Add(new RealField("near clip plane distance:objects closer than this are not drawn"));
            this.Fields.Add(new RealField("projection plane distance:distance at which objects are rendered when z=0 (normal" +
                        " size)"));
            this.Fields.Add(new RealField("far clip plane distance:objects farther than this are not drawn"));
            this.Fields.Add(new ExplanationField("Overlayed UI Color", null));
            this.Fields.Add(new RealArgbColorField("overlayed interface color"));
            this.Fields.Add(new PadField("", 12));
            this.Fields.Add(new ExplanationField("Displayed Errors", null));
            this.Fields.Add(new BlockField<UiErrorCategoryBlock>("errors", 100));
            this.Fields.Add(new ExplanationField("Cursor Sound", null));
            this.Fields.Add(new TagReferenceField("sound tag"));
            this.Fields.Add(new ExplanationField("Selection Sound", null));
            this.Fields.Add(new TagReferenceField("sound tag"));
            this.Fields.Add(new ExplanationField("Error Sound", null));
            this.Fields.Add(new TagReferenceField("sound tag"));
            this.Fields.Add(new ExplanationField("Advancing Sound", null));
            this.Fields.Add(new TagReferenceField("sound tag"));
            this.Fields.Add(new ExplanationField("Retreating Sound", null));
            this.Fields.Add(new TagReferenceField("sound tag"));
            this.Fields.Add(new ExplanationField("Initial Login Sound", null));
            this.Fields.Add(new TagReferenceField("sound tag"));
            this.Fields.Add(new ExplanationField("VKBD Cursor Sound", null));
            this.Fields.Add(new TagReferenceField("sound tag"));
            this.Fields.Add(new ExplanationField("VKBD Character Insertion Sound", null));
            this.Fields.Add(new TagReferenceField("sound tag"));
            this.Fields.Add(new ExplanationField("Online Notification Sound", null));
            this.Fields.Add(new TagReferenceField("sound tag"));
            this.Fields.Add(new ExplanationField("Tabbed View Pane Tabbing Sound", null));
            this.Fields.Add(new TagReferenceField("sound tag"));
            this.Fields.Add(new ExplanationField("Pregame Countdown Timer Sound", null));
            this.Fields.Add(new TagReferenceField("sound tag"));
            this.Fields.Add(new TagReferenceField(""));
            this.Fields.Add(new ExplanationField("Matchmaking Advance Sound", null));
            this.Fields.Add(new TagReferenceField("sound tag"));
            this.Fields.Add(new TagReferenceField(""));
            this.Fields.Add(new TagReferenceField(""));
            this.Fields.Add(new TagReferenceField(""));
            this.Fields.Add(new ExplanationField("Global Bitmaps", null));
            this.Fields.Add(new TagReferenceField("global bitmaps tag"));
            this.Fields.Add(new ExplanationField("Global Text Strings", null));
            this.Fields.Add(new TagReferenceField("unicode string list tag"));
            this.Fields.Add(new ExplanationField("Screen Animations", null));
            this.Fields.Add(new BlockField<AnimationReferenceBlock>("screen animations", 64));
            this.Fields.Add(new ExplanationField("Polygonal Shape Groups", null));
            this.Fields.Add(new BlockField<ShapeGroupReferenceBlock>("shape groups", 64));
            this.Fields.Add(new ExplanationField("Persistant Background Animations", null));
            this.Fields.Add(new BlockField<PersistentBackgroundAnimationBlock>("animations", 100));
            this.Fields.Add(new ExplanationField("List Skins", null));
            this.Fields.Add(new BlockField<ListSkinReferenceBlock>("list item skins", 32));
            this.Fields.Add(new ExplanationField("Additional UI Strings", null));
            this.Fields.Add(new TagReferenceField("button key type strings"));
            this.Fields.Add(new TagReferenceField("game type strings"));
            this.Fields.Add(new TagReferenceField(""));
            this.Fields.Add(new ExplanationField("Skill to rank mapping table", null));
            this.Fields.Add(new BlockField<SkillToRankMappingBlock>("skill mappings", 65535));
            this.Fields.Add(new ExplanationField("WINDOW PARAMETERS", null));
            this.Fields.Add(new EnumField("full screen header text font", "terminal", "body text", "title", "super large font", "large body text", "split screen hud message", "full screen hud message", "english body text", "HUD number text", "subtitle font", "main menu font", "text chat font"));
            this.Fields.Add(new EnumField("large dialog header text font", "terminal", "body text", "title", "super large font", "large body text", "split screen hud message", "full screen hud message", "english body text", "HUD number text", "subtitle font", "main menu font", "text chat font"));
            this.Fields.Add(new EnumField("half dialog header text font", "terminal", "body text", "title", "super large font", "large body text", "split screen hud message", "full screen hud message", "english body text", "HUD number text", "subtitle font", "main menu font", "text chat font"));
            this.Fields.Add(new EnumField("qtr dialog header text font", "terminal", "body text", "title", "super large font", "large body text", "split screen hud message", "full screen hud message", "english body text", "HUD number text", "subtitle font", "main menu font", "text chat font"));
            this.Fields.Add(new RealArgbColorField("default text color"));
            this.Fields.Add(new Rectangle2dField("full screen header text bounds"));
            this.Fields.Add(new Rectangle2dField("full screen button key text bounds"));
            this.Fields.Add(new Rectangle2dField("large dialog header text bounds"));
            this.Fields.Add(new Rectangle2dField("large dialog button key text bounds"));
            this.Fields.Add(new Rectangle2dField("half dialog header text bounds"));
            this.Fields.Add(new Rectangle2dField("half dialog button key text bounds"));
            this.Fields.Add(new Rectangle2dField("qtr dialog header text bounds"));
            this.Fields.Add(new Rectangle2dField("qtr dialog button key text bounds"));
            this.Fields.Add(new ExplanationField("Main menu music", null));
            this.Fields.Add(new TagReferenceField("main menu music"));
            this.Fields.Add(new LongIntegerField("music fade time:milliseconds"));
        }
        /// <summary>
        /// Gets and returns the name of the user_interface_shared_globals_definition_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "user_interface_shared_globals_definition_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the user_interface_shared_globals_definition_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "user_interface_shared_globals_definition";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the user_interface_shared_globals_definition_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the user_interface_shared_globals_definition_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the user_interface_shared_globals_definition_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the user_interface_shared_globals_definition_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<UiErrorCategoryBlock>)(this.Fields[21])).WriteChildren(writer);
            ((BlockField<AnimationReferenceBlock>)(this.Fields[55])).WriteChildren(writer);
            ((BlockField<ShapeGroupReferenceBlock>)(this.Fields[57])).WriteChildren(writer);
            ((BlockField<PersistentBackgroundAnimationBlock>)(this.Fields[59])).WriteChildren(writer);
            ((BlockField<ListSkinReferenceBlock>)(this.Fields[61])).WriteChildren(writer);
            ((BlockField<SkillToRankMappingBlock>)(this.Fields[67])).WriteChildren(writer);
        }
    }
}
