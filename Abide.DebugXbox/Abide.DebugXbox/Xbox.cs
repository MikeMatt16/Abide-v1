using System;
using System.Net;
using System.Net.Sockets;

namespace Abide.DebugXbox
{
    /// <summary>
    /// Represents a debug Xbox console.
    /// </summary>
    public sealed partial class Xbox : IDisposable
    {
        private Socket tcpSocket = null;

        /// <summary>
        /// Gets and returns the name of the debug Xbox console.
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// Gets and returns the end point of the debug Xbox console.
        /// </summary>
        public EndPoint RemoteEndPoint { get; internal set; }
        /// <summary>
        /// Attempts to open a Remote Debugging and Control Protocol TCP connection.
        /// </summary>
        public void Connect()
        {

        }
        public void Dispose()
        {
            //Dispose
            tcpSocket.Dispose();
        }
    }
}
