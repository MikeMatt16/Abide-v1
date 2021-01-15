using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abide.DebugXbox
{
    /// <summary>
    /// Represents a set of <see langword="static"/> (<see langword="Shared"/> in Visual Basic) methods for handling the debug Xbox Name Answering Protocol.
    /// </summary>
    public static class NameAnsweringProtocol
    {
        private const int NapPort = 731;
        private static readonly IPEndPoint discoveryEndPoint = new IPEndPoint(IPAddress.Broadcast, NapPort);
        private static readonly IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, NapPort);
        
        /// <summary>
        /// Attempts to discover debug Xbox consoles within a specified time limit.
        /// </summary>
        /// <param name="timeout">The amount of time in milliseconds to wait for responses before returning the results.</param>
        /// <returns>An array of discovered <see cref="Xbox"/> elements.</returns>
        public static Xbox[] Discover(int timeout = 1000)
        {
            DateTime requestTime = new DateTime();
            NapPacket response = new NapPacket();
            byte[] responsePacket = new byte[NapPacket.MaxLength];
            byte[] discoveryPacket = NapPacket.CreateDiscoveryPacket().GetPacket();
            EndPoint remoteEndPoint = localEndPoint;
            List<Socket> sockets = new List<Socket>();
            List<Xbox> xboxes = new List<Xbox>();

            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation ua in networkInterface.GetIPProperties().UnicastAddresses)
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp) { EnableBroadcast = true };

                    try
                    {
                        socket.Bind(new IPEndPoint(ua.Address, 0));
                        socket.SendTo(discoveryPacket, discoveryEndPoint);
                        sockets.Add(socket);
                    }
                    catch { }
                }
            }

            requestTime = DateTime.Now;
            do
            {
                Thread.Sleep(1);
                foreach (Socket socket in sockets)
                {
                    while (socket.Available > 0)
                    {
                        responsePacket = new byte[socket.Available];
                        socket.ReceiveFrom(responsePacket, ref remoteEndPoint);
                        response.SetPacket(responsePacket);
                        if (response.Type == NapType.Reply)
                        {
                            xboxes.Add(CreateXbox(response, remoteEndPoint));
                        }
                    }
                }
            }
            while ((DateTime.Now - requestTime).TotalMilliseconds < timeout);

            sockets.ForEach(s => s.Close());
            sockets.Clear();
            return xboxes.ToArray();
        }
        /// <summary>
        /// Attempts to discover debug Xbox consoles with the specified name within a specified time limit.
        /// </summary>
        /// <param name="name">The name of the debug Xbox console or domain name.</param>
        /// <param name="timeout">The amount of time in milliseconds to wait for responses before returning the results.</param>
        /// <returns>An array of discovered <see cref="Xbox"/> elements.</returns>
        public static Xbox[] Discover(string name, int timeout = 1000)
        {
            //Check
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (name == string.Empty) throw new ArgumentException(nameof(name));
            if (timeout < 1) throw new ArgumentException(nameof(timeout));
            
            //Prepare
            DateTime requestTime = new DateTime();
            List<Xbox> xboxes = new List<Xbox>();
            NapPacket response = new NapPacket();
            byte[] responsePacket = new byte[NapPacket.MaxLength];
            byte[] discoveryPacket = NapPacket.CreateLookupPacket(name).GetPacket();
            EndPoint remoteEndPoint = discoveryEndPoint;
            List<Socket> sockets = new List<Socket>();

            //Prepare
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
                foreach (UnicastIPAddressInformation ua in networkInterface.GetIPProperties().UnicastAddresses)
                {
                    //Create Socket
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp) { EnableBroadcast = true };

                    //Attempt to bind and send
                    try
                    {
                        socket.Bind(new IPEndPoint(ua.Address, 0));
                        socket.SendTo(discoveryPacket, discoveryEndPoint);

                        //Add socket to list
                        sockets.Add(socket);
                    }
                    catch { }
                }

            //Loop
            requestTime = DateTime.Now;
            do
            {
                //Sleep
                Thread.Sleep(1);
                foreach (Socket socket in sockets)
                    while (socket.Available > 0)
                    {
                        responsePacket = new byte[socket.Available];
                        socket.ReceiveFrom(responsePacket, ref remoteEndPoint);
                        response.SetPacket(responsePacket);
                        if (response.Type == NapType.Reply)
                            xboxes.Add(CreateXbox(response, remoteEndPoint));
                    }
            }
            while ((DateTime.Now - requestTime).TotalMilliseconds < timeout);

            //Close
            sockets.ForEach(s => s.Close());
            sockets.Clear();

            //Return
            return xboxes.ToArray();
        }
        /// <summary>
        /// Attempts to discover a debug Xbox at a specified end point within a specified time limit.
        /// </summary>
        /// <param name="remoteAddress">The remote end point of the debug Xbox console.</param>
        /// <param name="timeout">The amount of time in milliseconds to wait for responses before returning the results.</param>
        /// <returns>The </returns>
        public static Xbox Discover(IPAddress remoteAddress, int timeout = 1000)
        {
            //Check
            if (remoteAddress == null) throw new ArgumentNullException(nameof(remoteAddress));
            if (timeout < 1) throw new ArgumentException(nameof(timeout));

            //Prepare
            Xbox xbox = null;
            DateTime requestTime = new DateTime();
            NapPacket response = new NapPacket();
            byte[] responsePacket = new byte[NapPacket.MaxLength];
            byte[] discoveryPacket = NapPacket.CreateDiscoveryPacket().GetPacket();
            EndPoint remoteEndPoint = new IPEndPoint(remoteAddress, NapPort);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp) { EnableBroadcast = true };
            socket.Bind(localEndPoint);

            //Send
            socket.SendTo(discoveryPacket, remoteEndPoint);

            //Loop
            requestTime = DateTime.Now;
            do
            {
                //Sleep
                Thread.Sleep(1);
                while (socket.Available > 0)
                {
                    responsePacket = new byte[socket.Available];
                    socket.ReceiveFrom(responsePacket, ref remoteEndPoint);
                    response.SetPacket(responsePacket);
                    if (response.Type == NapType.Reply)
                    {
                        //Close
                        socket.Close();
                        return CreateXbox(response, remoteEndPoint);
                    }
                }
            }
            while ((DateTime.Now - requestTime).TotalMilliseconds < timeout);

            //Close
            socket.Close();
            return xbox;
        }
        /// <summary>
        /// Attempts to discover debug Xbox consoles within a specified time limit.
        /// These methods do not block the calling thread.
        /// </summary>
        /// <param name="timeout">The amount of time in milliseconds to wait for responses before returning the results.</param>
        public static async Task<Xbox[]> DiscoverAsync(int timeout = 1000)
        {
            if (timeout < 1) throw new ArgumentException(nameof(timeout));

            return await Task.Run(() =>
            {
                return Discover(timeout);
            });
        }
        /// <summary>
        /// Attempts to discover debug Xbox consoles with the specified name within a specified time limit.
        /// These methods do not block the calling thread.
        /// </summary>
        /// <param name="name">The name of the debug Xbox console or domain name.</param>
        /// <param name="timeout">The amount of time in milliseconds to wait for responses before returning the results.</param>
        public static async Task<Xbox[]> DiscoverAsync(string name, int timeout = 1000)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (name.Length == 0) throw new ArgumentException(nameof(name));
            if (timeout < 1) throw new ArgumentException(nameof(timeout));

            return await Task.Run(() =>
            {
                return Discover(name, timeout);
            });
        }
        /// <summary>
        /// Attempts to discover a debug Xbox at a specified end point within a specified time limit.
        /// These methods do not block the calling thread.
        /// </summary>
        /// <param name="remoteAddress">The remote end point of the debug Xbox console.</param>
        /// <param name="timeout">The amount of time in milliseconds to wait for responses before returning the results.</param>
        public static async Task<Xbox> DiscoverAsync(IPAddress remoteAddress, int timeout = 1000)
        {
            //Check
            if (timeout < 1) throw new ArgumentException(nameof(timeout));

            return await Task.Run(() =>
            {
                return Discover(remoteAddress, timeout);
            });
        }
        private static Xbox CreateXbox(NapPacket packet, EndPoint remoteEndPoint)
        {
            //Check
            if (packet == null) throw new ArgumentNullException(nameof(packet));
            if (remoteEndPoint == null) throw new ArgumentNullException(nameof(remoteEndPoint));
            if (packet.Type != NapType.Reply) throw new ArgumentException("Invalid packet.", nameof(packet));

            //Create
            Xbox xbox = new Xbox() { Name = packet.Name, RemoteEndPoint = (IPEndPoint)remoteEndPoint };

            //Return
            return xbox;
        }
    }

    /// <summary>
    /// Represents the method that will handle the <see cref="Xbox.DownloadProgressChanged"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="NameAnsweringProtocolEventArgs"/> containing event data.</param>
    public delegate void NameAnsweringProtocolEventHandler(NameAnsweringProtocolEventArgs e);

    /// <summary>
    /// Provides data for the <see cref="Xbox.DownloadProgressChanged"/> event.
    /// </summary>
    public sealed class NameAnsweringProtocolEventArgs : EventArgs
    {
        /// <summary>
        /// Returns the found Xbox.
        /// </summary>
        public Xbox Result { get; } = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="NameAnsweringProtocolEventArgs"/> class using the specified Xbox instance.
        /// </summary>
        /// <param name="xbox">The Xbox to pass to the event arguments. This value can be <see langword="null"/>.</param>
        public NameAnsweringProtocolEventArgs(Xbox xbox)
        {
            //Set
            Result = xbox;
        }
    }

    /// <summary>
    /// Represents a name answering protocol packet.
    /// </summary>
    public sealed class NapPacket : IPacket
    {
        /// <summary>
        /// Represents the maximum length of a name answering protocol packet.
        /// </summary>
        public const int MaxLength = 257;
        /// <summary>
        /// Represents the minimum length of a name answering protocol packet.
        /// </summary>
        public const int MinLength = 2;
        /// <summary>
        /// Gets or sets the name of the console.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the name answering protocol type.
        /// </summary>
        public NapType Type { get; set; } = NapType.None;
        /// <summary>
        /// Creates a new <see cref="NapPacket"/> for console discovery.
        /// </summary>
        /// <returns>A new instance of the <see cref="NapPacket"/> class.</returns>
        /// <remarks>Send packet to UDP 255.255.255.255 on port 731, each debug console will respond with a NAP packet with 
        /// <see cref="Type"/> being equal to <see cref="NapType.Reply"/> and <see cref="Name"/> being equal to the responding console's name.</remarks>
        public static NapPacket CreateDiscoveryPacket()
        {
            return new NapPacket() { Type = NapType.Wildcard };
        }
        /// <summary>
        /// Creates a new <see cref="NapPacket"/> for console lookup.
        /// </summary>
        /// <param name="name">The name of the console to lookup.</param>
        /// <returns>A new instance of the <see cref="NapPacket"/> class.</returns>
        /// <remarks>Send packet to UDP 255.255.255.255 on port 731, each console whose name matches the supplied name will respond with a NAP packet with
        /// <see cref="Type"/> being equal to <see cref="NapType.Reply"/> and <see cref="Name"/> being equal to the responding console's name.</remarks>
        public static NapPacket CreateLookupPacket(string name)
        {
            return new NapPacket() { Type = NapType.Lookup, Name = name };
        }
        /// <summary>
        /// Returns the NAP packet data.
        /// </summary>
        /// <returns>An array of <see cref="byte"/> elements containing the packet data.</returns>
        public byte[] GetPacket()
        {
            //Prepare
            byte[] nameData = Encoding.ASCII.GetBytes(Name.Substring(0, Math.Min(Name.Length, 255)));
            byte[] packet = new byte[nameData.Length + 2];

            //Populate packet
            packet[0] = (byte)Type;
            packet[1] = (byte)nameData.Length;
            Array.Copy(nameData, 0, packet, 2, nameData.Length);

            //Return
            return packet;
        }
        /// <summary>
        /// Sets the NAP packet using the specified data.
        /// </summary>
        /// <param name="packet">The array of <see cref="byte"/> elements containing the packet data.</param>
        public void SetPacket(byte[] packet)
        {
            if (packet == null || packet.Length < 2 || packet.Length > 257) return; //Ignore invalid packets

            //Get type
            Type = (NapType)packet[0];

            //Get name
            int length = packet[1];
            Name = Encoding.ASCII.GetString(packet, 2, length);
        }
        /// <summary>
        /// Returns a string representation of this packet.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{Enum.GetName(typeof(NapType), Type)} - \"{Name}\"";
        }
    }

    /// <summary>
    /// Represents an enumerator that contains name answering protocol packet types.
    /// </summary>
    public enum NapType : byte
    {
        None = 0,
        Lookup = 1,
        Reply = 2,
        Wildcard = 3
    };
}
