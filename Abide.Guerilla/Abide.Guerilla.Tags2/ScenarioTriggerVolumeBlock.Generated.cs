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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(68, 4)]
    public class ScenarioTriggerVolumeBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("Name^", typeof(StringId))]
        public StringId Name;
        [Abide.Guerilla.Tags.FieldAttribute("Object Name", typeof(Int16))]
        public Int16 ObjectName;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(2)]
        public Byte[] EmptyString;
        [Abide.Guerilla.Tags.FieldAttribute("Node Name", typeof(StringId))]
        public StringId NodeName;
        [Abide.Guerilla.Tags.FieldAttribute("EMPTY STRING", typeof(Byte[]))]
        public Byte[] EmptyString1;
        [Abide.Guerilla.Tags.FieldAttribute("EMPTY STRING", typeof(Single))]
        public Single EmptyString2;
        [Abide.Guerilla.Tags.FieldAttribute("Position", typeof(Vector3))]
        public Vector3 Position;
        [Abide.Guerilla.Tags.FieldAttribute("Extents", typeof(Vector3))]
        public Vector3 Extents;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(4)]
        public Byte[] EmptyString4;
        [Abide.Guerilla.Tags.FieldAttribute("~Kill Trigger Volume*", typeof(Int16))]
        public Int16 KillTriggerVolume;
        [Abide.Guerilla.Tags.FieldAttribute("", typeof(Byte[]))]
        [Abide.Guerilla.Tags.PaddingAttribute(2)]
        public Byte[] EmptyString5;
    }
}
#pragma warning restore CS1591