using System;
using System.ComponentModel;

namespace Abide.AddOnApi
{
    /// <summary>
    /// Defines a generalized AddOn host that a class implements to communcate with <see cref="IAddOn"/> instances.
    /// </summary>
    public interface IHost : ISynchronizeInvoke
    {
        /// <summary>
        /// Executes the specified delegate on the thread that owns the host's underlying window handle.
        /// </summary>
        /// <param name="method">A delegate that contains a method to be called in the control's thread context.</param>
        /// <returns>The return value from the delegate being invoked, or null if the delegate has no return value.</returns>
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
        /// Gets and returns the add-on host instance.
        /// </summary>
        public IHost Host { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="AddOnHostEventArgs"/> class using the specified add-on host.
        /// </summary>
        /// <param name="host">The add-on host instance.</param>
        public AddOnHostEventArgs(IHost host)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
        }
    }
}
