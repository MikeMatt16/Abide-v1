using System;

namespace Abide.AddOnApi
{
    /// <summary>
    /// Indicates that a class is an AddOn.
    /// This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AddOnAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddOnAttribute"/> class.
        /// </summary>
        public AddOnAttribute() { }
    }
}
