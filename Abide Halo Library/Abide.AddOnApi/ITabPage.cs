using System.Windows.Forms;

namespace Abide.AddOnApi
{
    /// <summary>
    /// Defines a generalized tab page AddOn that a class implements to extend the usage of a host application. 
    /// </summary>
    /// <typeparam name="TMap">The Halo Map type to be used by the interface.</typeparam>
    /// <typeparam name="TEntry">The Object Index Entry type to be used by the interface.</typeparam>
    /// <typeparam name="TXbox">The Debug Xbox type to be used by the interface.</typeparam>
    public interface ITabPage<TMap, TEntry, TXbox> : IAddOn, ITagFilter, IDebugXboxAddOn<TXbox>, IHaloAddOn<TMap, TEntry>
    {
        /// <summary>
        /// When implemented, gets and returns the User Interface control for this tab page.
        /// </summary>
        Control UserInterface { get; }
    }
}
