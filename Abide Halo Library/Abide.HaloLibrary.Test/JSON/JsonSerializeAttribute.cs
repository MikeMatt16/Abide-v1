using System;

namespace Donek.Core.JSON
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class JsonSerializeAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name to serialize this property or field with.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializeAttribute"/> class.
        /// </summary>
        public JsonSerializeAttribute()
        {
        }
    }
}
