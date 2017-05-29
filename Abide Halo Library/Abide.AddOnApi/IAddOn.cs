using System;

namespace Abide.AddOnApi
{
    /// <summary>
    /// Defines a generalized AddOn that a class implements to extend the usage of a host application.
    /// </summary>
    public interface IAddOn : IDisposable
    {
        /// <summary>
        /// When implemented, gets and returns the name of the AddOn.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// When implemented, gets and returns a description of the AddOn.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// When implemented, gets and returns the author's name.
        /// </summary>
        string Author { get; }
        /// <summary>
        /// When implemented, initializes the AddOn instance, and prepares communication with the host application.
        /// </summary>
        /// <param name="host">The AddOn's host instance.</param>
        void Initialize(IHost host);
    }
}
