namespace Abide.AddOnApi
{
    /// <summary>
    /// Defines a generalized AddOn host that a class implements to communcate with <see cref="IAddOn"/> instances.
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// When implemented, handles a request from an <see cref="IAddOn"/> instance using a specified request string and supplied arguments.
        /// </summary>
        /// <param name="request">The request string.</param>
        /// <param name="args">The request arguments.</param>
        /// <returns>Varies based on the request.</returns>
        object Request(string request, object[] args = null);
    }
}
