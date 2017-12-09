using System;
using System.CodeDom;

namespace Abide.Guerilla.CodeDom
{
    /// <summary>
    /// Represents a reference to an attribute type.
    /// </summary>
    /// <typeparam name="TAttribute">The attribute type.</typeparam>
    internal sealed class CodeAttributeTypeReference<TAttribute> : CodeTypeReference where TAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeAttributeTypeReference{TAttribute}"/> class.
        /// </summary>
        public CodeAttributeTypeReference() : base()
        {
            BaseType = GetAttributeTypeName(typeof(TAttribute));
        }

        /// <summary>
        /// Gets the attribute type string.
        /// This method removes the "Attribute" string at the end of the type.
        /// </summary>
        /// <param name="attributeType">The attribute type.</param>
        /// <returns>A string.</returns>
        private static string GetAttributeTypeName(Type attributeType)
        {
            if (attributeType.Name.EndsWith("Attribute"))
                return attributeType.Name.Substring(0, attributeType.Name.LastIndexOf("Attribute"));
            else return attributeType.Name;
        }
    }
}
