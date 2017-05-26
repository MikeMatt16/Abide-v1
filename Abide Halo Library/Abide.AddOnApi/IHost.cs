using System;

namespace Abide.AddOnApi
{
    /// <summary>
    /// Defines a generalized AddOn host that a class implements to communcate with <see cref="IAddOn"/> instances.
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// Gets a value indicating whether the caller must call an invoke method when making method calls to the host because the host is on a different thread than the caller is on.
        /// </summary>
        bool InvokeRequired { get; }
        /// <summary>
        /// Executes the specified delegate on the thread that owns the host's underlying window handle.
        /// </summary>
        /// <param name="method">A delegate that contains a method to be called in the host's thread context. </param>
        /// <returns>The return value from the delegate being invoked, or <see cref="null"/> if the delegate has no return value.</returns>
        object Invoke(Delegate method);
        /// <summary>
        /// When implemented, handles a request from an <see cref="IAddOn"/> instance using a specified request string and supplied arguments.
        /// </summary>
        /// <param name="sender">The <see cref="IAddOn"/> instance that sent the request.</param>
        /// <param name="request">The request string.</param>
        /// <param name="args">The request arguments.</param>
        /// <returns>Varies based on the request.</returns>
        object Request(IAddOn sender, string request, params object[] args);
    }

    /// <summary>
    /// Provides data for the AddOn Host communication events.
    /// </summary>
    public class AddOnHostEventArgs : EventArgs
    {
        /// <summary>
        /// The AddOn host instance.
        /// </summary>
        public IHost Host
        {
            get { return host; }
        }
        
        private readonly IHost host;

        /// <summary>
        /// Initializes a new <see cref="AddOnHostEventArgs"/> instance.
        /// </summary>
        /// <param name="host">The AddOn Host instance.</param>
        public AddOnHostEventArgs(IHost host)
        {
            this.host = host;
        }
    }
}
