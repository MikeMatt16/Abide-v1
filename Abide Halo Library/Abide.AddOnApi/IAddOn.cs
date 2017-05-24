namespace Abide.AddOnApi
{
    /// <summary>
    /// Defines a generalized AddOn that a class implements to extend the usage of a host application.
    /// </summary>
    public interface IAddOn
    {
        /// <summary>
        /// When implemented, initializes the AddOn instance, and prepares communication with the host application.
        /// </summary>
        /// <param name="host">The AddOn's host instance.</param>
        void Initialize(IHost host);
    }
}
