namespace Abide.AddOnApi
{
    /// <summary>
    /// Defines a generalized Debug Xbox AddOn that a class implements to communcate with a host application.
    /// This type of AddOn recieves triggers from the host application when changes are made with a remote debug console.
    /// </summary>
    /// <typeparam name="TXbox">The Debug Xbox type to be used by the interface.</typeparam>
    public interface IDebugXboxAddOn<TXbox> : IAddOn
    {
        /// <summary>
        /// When implemented, gets and returns the Debug Xbox supplied from the <see cref="IHost"/> instance.
        /// This value can be null.
        /// </summary>
        TXbox Xbox { get; }
        /// <summary>
        /// When implemented, will be called by the <see cref="IHost"/> instance when the Debug Xbox instance is connected or disconnected.
        /// </summary>
        void DebugXboxChanged();
    }
}
