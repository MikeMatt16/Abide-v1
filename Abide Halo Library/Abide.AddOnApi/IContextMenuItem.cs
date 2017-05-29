using System.Drawing;

namespace Abide.AddOnApi
{
    /// <summary>
    /// Defines a generalized context menu item AddOn that a class implements to extend the usage of a host application. 
    /// </summary>
    /// <typeparam name="TMap">The Halo Map type to be used by the interface.</typeparam>
    /// <typeparam name="TEntry">The Object Index Entry type to be used by the interface.</typeparam>
    /// <typeparam name="TXbox">The Debug Xbox type to be used by the interface.</typeparam>
    public interface IContextMenuItem<TMap, TEntry, TXbox> : IAddOn, ITagFilter, IDebugXboxAddOn<TXbox>, IHaloAddOn<TMap, TEntry>
    {
        /// <summary>
        /// When implemented, gets and returns an icon for this tool. This value can be null.
        /// </summary>
        Image Icon { get; }
        /// <summary>
        /// When implemented, called by the host instance when the menu button is clicked.
        /// </summary>
        void OnClick();
    }
}
