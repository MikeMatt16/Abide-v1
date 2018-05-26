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
    /// Represents the generated scenario_ai_resource_block tag block.
    /// </summary>
    public sealed class ScenarioAiResourceBlock : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioAiResourceBlock"/> class.
        /// </summary>
        public ScenarioAiResourceBlock()
        {
            this.Fields.Add(new BlockField<StylePaletteBlock>("Style Palette", 50));
            this.Fields.Add(new BlockField<SquadGroupsBlock>("Squad Groups", 100));
            this.Fields.Add(new BlockField<SquadsBlock>("Squads", 335));
            this.Fields.Add(new BlockField<ZoneBlock>("Zones", 128));
            this.Fields.Add(new BlockField<CharacterPaletteBlock>("Character Palette", 64));
            this.Fields.Add(new BlockField<AiAnimationReferenceBlock>("AI Animation References", 128));
            this.Fields.Add(new BlockField<AiScriptReferenceBlock>("AI Script References", 128));
            this.Fields.Add(new BlockField<AiRecordingReferenceBlock>("AI Recording References", 128));
            this.Fields.Add(new BlockField<AiConversationBlock>("AI Conversations", 128));
            this.Fields.Add(new BlockField<CsScriptDataBlock>("Scripting Data", 1));
            this.Fields.Add(new BlockField<OrdersBlock>("Orders", 300));
            this.Fields.Add(new BlockField<TriggersBlock>("Triggers", 256));
            this.Fields.Add(new BlockField<ScenarioStructureBspReferenceBlock>("BSP Preferences", 16));
            this.Fields.Add(new BlockField<ScenarioWeaponPaletteBlock>("Weapon References", 256));
            this.Fields.Add(new BlockField<ScenarioVehiclePaletteBlock>("Vehicle References", 256));
            this.Fields.Add(new BlockField<ScenarioVehicleBlock>("Vehicle Datum References", 256));
            this.Fields.Add(new BlockField<AiSceneBlock>("Mission Dialogue Scenes", 100));
            this.Fields.Add(new BlockField<FlockDefinitionBlock>("Flocks", 20));
            this.Fields.Add(new BlockField<ScenarioTriggerVolumeBlock>("Trigger Volume References", 256));
        }
        /// <summary>
        /// Gets and returns the name of the scenario_ai_resource_block tag block.
        /// </summary>
        public override string Name
        {
            get
            {
                return "scenario_ai_resource_block";
            }
        }
        /// <summary>
        /// Gets and returns the display name of the scenario_ai_resource_block tag block.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "scenario_ai_resource";
            }
        }
        /// <summary>
        /// Gets and returns the maximum number of elements allowed of the scenario_ai_resource_block tag block.
        /// </summary>
        public override int MaximumElementCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets and returns the alignment of the scenario_ai_resource_block tag block.
        /// </summary>
        public override int Alignment
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// Writes the scenario_ai_resource_block tag block using the specified binary writer.
        /// </summary>
        // <param name="writer">The <see cref="BinaryWriter"/> used to write the scenario_ai_resource_block tag block.</param>
        public override void Write(BinaryWriter writer)
        {
            // Invoke base write procedure.
            base.Write(writer);
            // Post-write the tag blocks.
            ((BlockField<StylePaletteBlock>)(this.Fields[0])).WriteChildren(writer);
            ((BlockField<SquadGroupsBlock>)(this.Fields[1])).WriteChildren(writer);
            ((BlockField<SquadsBlock>)(this.Fields[2])).WriteChildren(writer);
            ((BlockField<ZoneBlock>)(this.Fields[3])).WriteChildren(writer);
            ((BlockField<CharacterPaletteBlock>)(this.Fields[4])).WriteChildren(writer);
            ((BlockField<AiAnimationReferenceBlock>)(this.Fields[5])).WriteChildren(writer);
            ((BlockField<AiScriptReferenceBlock>)(this.Fields[6])).WriteChildren(writer);
            ((BlockField<AiRecordingReferenceBlock>)(this.Fields[7])).WriteChildren(writer);
            ((BlockField<AiConversationBlock>)(this.Fields[8])).WriteChildren(writer);
            ((BlockField<CsScriptDataBlock>)(this.Fields[9])).WriteChildren(writer);
            ((BlockField<OrdersBlock>)(this.Fields[10])).WriteChildren(writer);
            ((BlockField<TriggersBlock>)(this.Fields[11])).WriteChildren(writer);
            ((BlockField<ScenarioStructureBspReferenceBlock>)(this.Fields[12])).WriteChildren(writer);
            ((BlockField<ScenarioWeaponPaletteBlock>)(this.Fields[13])).WriteChildren(writer);
            ((BlockField<ScenarioVehiclePaletteBlock>)(this.Fields[14])).WriteChildren(writer);
            ((BlockField<ScenarioVehicleBlock>)(this.Fields[15])).WriteChildren(writer);
            ((BlockField<AiSceneBlock>)(this.Fields[16])).WriteChildren(writer);
            ((BlockField<FlockDefinitionBlock>)(this.Fields[17])).WriteChildren(writer);
            ((BlockField<ScenarioTriggerVolumeBlock>)(this.Fields[18])).WriteChildren(writer);
        }
    }
}
