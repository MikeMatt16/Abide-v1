#pragma warning disable CS1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abide.Guerilla.Tags2
{
    using Abide.Guerilla.Types;
    using Abide.HaloLibrary;
    using System;
    
    [Abide.Guerilla.Tags.FieldSetAttribute(60, 4)]
    [Abide.Guerilla.Tags.TagGroupAttribute("new_hud_definition", "nhdt", "����", typeof(NewHudDefinitionBlock))]
    public class NewHudDefinitionBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("DO NOT USE", typeof(TagReference))]
        public TagReference DoNotUse;
        [Abide.Guerilla.Tags.FieldAttribute("bitmap widgets", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Hud Bitmap Widgets", 256, typeof(HudBitmapWidgets))]
        public TagBlock BitmapWidgets;
        [Abide.Guerilla.Tags.FieldAttribute("text widgets", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Hud Text Widgets", 256, typeof(HudTextWidgets))]
        public TagBlock TextWidgets;
        [Abide.Guerilla.Tags.FieldAttribute("dashlight data", typeof(NewHudDashlightDataStructBlock))]
        public NewHudDashlightDataStructBlock DashlightData;
        [Abide.Guerilla.Tags.FieldAttribute("screen effect widgets", typeof(TagBlock))]
        [Abide.Guerilla.Tags.BlockAttribute("Hud Screen Effect Widgets", 4, typeof(HudScreenEffectWidgets))]
        public TagBlock ScreenEffectWidgets;
    }
}
#pragma warning restore CS1591