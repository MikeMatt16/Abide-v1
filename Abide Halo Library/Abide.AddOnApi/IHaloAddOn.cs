using Abide.HaloLibrary;

namespace Abide.AddOnApi
{
    /// <summary>
    /// Defines a generalized Halo AddOn that a class implements to communcate with a <see cref="IHost"/> instance.
    /// </summary>
    /// <typeparam name="T1">The Map type to be used by the interface.</typeparam>
    /// <typeparam name="T2">The Object Index Entry type to be used by the interface.</typeparam>
    public interface IHaloAddOn<T1, T2> : IAddOn
    {
        /// <summary>
        /// When implemented, gets and returns the <see cref="MapVersion"/> this Halo AddOn instance is compatible with.
        /// </summary>
        MapVersion Version { get; }
        /// <summary>
        /// When implemented, gets and returns the currently selected Halo Object Index Entry supplied from the <see cref="IHost"/> instance.
        /// This value can be null.
        /// </summary>
        T2 SelectedEntry { get; }
        /// <summary>
        /// When implemented, gets and returns the current Halo map instance supplied from the <see cref="IHost"/> instance.
        /// This value should never be null.
        /// </summary>
        T1 Map { get; }
        /// <summary>
        /// When implemented, will be called by the <see cref="IHost"/> instance when the Halo Map instance is either loaded, or reloaded.
        /// </summary>
        void OnMapLoad();
        /// <summary>
        /// When implemented, will be called by the <see cref="IHost"/> instance when the currently selected Halo Object Index Entry is changed.
        /// </summary>
        void OnSelectedEntryChanged();
    }
}
