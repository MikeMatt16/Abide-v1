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
    
    [Abide.Guerilla.Tags.FieldSetAttribute(24, 4)]
    public class ModelVariantObjectBlock
    {
        [Abide.Guerilla.Tags.FieldAttribute("parent marker^", typeof(StringId))]
        public StringId ParentMarker;
        [Abide.Guerilla.Tags.FieldAttribute("child marker", typeof(StringId))]
        public StringId ChildMarker;
        [Abide.Guerilla.Tags.FieldAttribute("child object", typeof(TagReference))]
        public TagReference ChildObject;
    }
}
#pragma warning restore CS1591