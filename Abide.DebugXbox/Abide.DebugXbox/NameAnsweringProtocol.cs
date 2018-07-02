using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Abide.DebugXbox
{
    /// <summary>
    /// Represents a set of <see langword="static"/> (<see langword="Shared"/> in Visual Basic) methods for handling the debug Xbox Name Answering Protocol.
    /// </summary>
    public static class NameAnsweringProtocol
    {
        /// <summary>
        /// Represents the name answering protocol port.
        /// This value is constant.
        /// </summary>
        private const int NapPort = 731;
        /// <summary>
        /// Represents the socket will be used by the name answering protocol.
        /// This field is read-only.
        /// </summary>
        private static readonly Socket socket;
        /// <summary>
        /// Represents the discovery IP end point.
        /// This field is read-only.
        /// </summary>
        private static readonly IPEndPoint discoveryEndPoint = new IPEndPoint(IPAddress.Broadcast, NapPort);
        /// <summary>
        /// Represents the local end point of the name answering protocol server.
        /// </summary>
        private static readonly IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, NapPort);

        /// <summary>
        /// Initializes the static instance of the <see cref="NameAnsweringProtocol"/> class.
        /// </summary>
        static NameAnsweringProtocol()
        {
            //Initialize
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp) { EnableBroadcast = true };
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            socket.Bind(localEndPoint);
        }
        /// <summary>
        /// Attempts to discover debug Xbox consoles within a specified time limit.
        /// </summary>
        /// <param name="timeout">The amount of time in seconds to wait for responses before returning the results.
        /// Defaults to 3.</param>
        /// <returns>An array of <see cref="Xbox"/> elements.</returns>
        public static Xbox[] Discover(int timeout = 3)
        {
            //Check
            if (timeout < 1) throw new ArgumentException(nameof(timeout));

            //Prepare
            DateTime requestTime = new DateTime();
            List<Xbox> xboxes = new List<Xbox>();
            NapPacket response = new NapPacket();
            byte[] responsePacket = new byte[NapPacket.MaxLength];
            byte[] discoveryPacket = NapPacket.CreateDiscoveryPacket().GetPacket();
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, NapPort);
            AsyncCallback sendCallback = null, receiveCallback = null;

            //Setup send callback
            sendCallback = new AsyncCallback(ar =>
            {
                //Send
                socket.EndSendTo(ar);
            });

            //Setup receive callback
            receiveCallback = new AsyncCallback(ar =>
            {
                //Check for timeout
                if ((DateTime.Now - requestTime).TotalSeconds >= timeout)
                {
                    socket.EndReceiveFrom(ar, ref remoteEndPoint);
                    return;
                }

                //Check length
                if (NapPacket.MaxLength >= socket.EndReceiveFrom(ar, ref remoteEndPoint))
                {
                    //Get packet
                    response.SetPacket(responsePacket);
                    if (response.Type == NapType.Reply)
                    {
                        //Write
                        Console.WriteLine("Discovered {0} on {1}", response.Name, remoteEndPoint);
                    }
                }

                //Receive
                remoteEndPoint = new IPEndPoint(IPAddress.Any, NapPort);
                socket.BeginReceiveFrom(responsePacket, 0, responsePacket.Length, SocketFlags.None, ref remoteEndPoint, receiveCallback, null);
            });

            //Send
            socket.BeginReceiveFrom(responsePacket, 0, responsePacket.Length, SocketFlags.None, ref remoteEndPoint, receiveCallback, null);
            socket.BeginSendTo(discoveryPacket, 0, discoveryPacket.Length, SocketFlags.None, discoveryEndPoint, sendCallback, null);

            //Loop
            requestTime = DateTime.Now;
            while ((DateTime.Now - requestTime).TotalSeconds < timeout) { Thread.Sleep(1); }

            //Return
            return xboxes.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static Xbox[] Discover(string name, int timeout = 3)
        {
            //Check
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (name == string.Empty) throw new ArgumentException(nameof(name));
            if (timeout < 1) throw new ArgumentException(nameof(timeout));

            //Prepare
            List<Xbox> xboxes = new List<Xbox>();
            NapPacket response = new NapPacket();
            byte[] responsePacket = new byte[NapPacket.MaxLength];
            byte[] discoveryPacket = NapPacket.CreateLookupPacket(name).GetPacket();
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, NapPort);
            AsyncCallback sendCallback = null, receiveCallback = null;

            //Setup send callback
            sendCallback = new AsyncCallback(ar =>
            {
                //Send
                socket.EndSendTo(ar);
            });

            //Setup receive callback
            receiveCallback = new AsyncCallback(ar =>
            {
                //End
                if (NapPacket.MaxLength >= socket.EndReceiveFrom(ar, ref remoteEndPoint))
                {
                    //Get packet
                    response.SetPacket(responsePacket);
                    if (response.Type == NapType.Reply)
                    {

                        //Write
                        Console.WriteLine("Discovered {0} on {1}", response, remoteEndPoint);
                    }
                }

                //Receive
                remoteEndPoint = new IPEndPoint(IPAddress.Any, NapPort);
                socket.BeginReceiveFrom(responsePacket, 0, responsePacket.Length, SocketFlags.None, ref remoteEndPoint, receiveCallback, null);
            });

            //Send
            socket.BeginReceiveFrom(responsePacket, 0, responsePacket.Length, SocketFlags.None, ref remoteEndPoint, receiveCallback, null);
            socket.BeginSendTo(discoveryPacket, 0, discoveryPacket.Length, SocketFlags.Broadcast, discoveryEndPoint, sendCallback, null);

            //Loop
            DateTime requestTime = DateTime.Now;
            while ((DateTime.Now - requestTime).TotalSeconds < timeout) { Thread.Sleep(1); }

            //Return
            return xboxes.ToArray();
        }
    }

    /// <summary>
    /// Represents a name answering protocol packet.
    /// </summary>
    public class NapPacket : IPacket
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
            nameData = null;

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
