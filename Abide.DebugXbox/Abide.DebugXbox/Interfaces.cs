namespace Abide.DebugXbox
{
    /// <summary>
    /// Represents objects that are packet types.
    /// </summary>
    public interface IPacket
    {
        /// <summary>
        /// Returns the packet data.
        /// </summary>
        /// <returns>An array of <see cref="byte"/> elements containing the packet data.</returns>
        byte[] GetPacket();
        /// <summary>
        /// Sets the packet value using the specified data.
        /// </summary>
        /// <param name="packet">The array of <see cref="byte"/> elements containing the packet data.</param>
        void SetPacket(byte[] packet);
    }
}
