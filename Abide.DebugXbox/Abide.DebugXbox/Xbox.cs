using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abide.DebugXbox
{
    /// <summary>
    /// Represents a debug Xbox connection.
    /// </summary>
    public sealed partial class Xbox : IDisposable
    {
        /// <summary>
        /// Represents the multiline terminator when reading multiline responses.
        /// </summary>
        private const string MultilineTerminator = ".";
        /// <summary>
        /// Occurs when the Xbox sends a response.
        /// </summary>
        public event EventHandler<ResponseEventArgs> Response;
        /// <summary>
        /// Occurs when an asynchronous download operation successfully transfers some or all of the data.
        /// </summary>
        public event DownloadProgressChangedEventHandler DownloadProgressChanged;
        /// <summary>
        /// Occurs when an asynchronous upload operation successfully transfers some or all of the data.
        /// </summary>
        public event UploadProgressChangedEventHandler UploadProgressChanged;
        /// <summary>
        /// Occurs when an asynchronous data download operation completes.
        /// </summary>
        public event DownloadDataCompletedEventHandler DownloadDataCompleted;
        /// <summary>
        /// Occurs when an asynchronous file download operation completes.
        /// </summary>
        public event AsyncCompletedEventHandler DownloadFileCompleted;
        /// <summary>
        /// Occurs when an asynchronous file upload operation completes.
        /// </summary>
        public event UploadEventHandler UploadFileCompleted;
        /// <summary>
        /// Occurs when an asynchronous data upload operation completes.
        /// </summary>
        public event UploadEventHandler UploadDataCompleted;

        private int m_SendBufferSize = 20 * 0x100000, m_ReceiveBufferSize = 20 * 0x100000;
        private readonly EndPoint m_LocalEndPoint;
        private Socket m_Socket = null;

        /// <summary>
        /// Gets or sets a value that specifies the size of the send buffer of the underlying <see cref="Socket"/>.
        /// </summary>
        /// <exception cref="SocketException">An error occurred when attempting to access the socket.</exception>
        /// <exception cref="ObjectDisposedException">The <see cref="Socket"/> has been closed.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The value specified for a set operation is less than 0.</exception>
        public int SendBufferSize
        {
            get { return m_Socket?.SendBufferSize ?? m_SendBufferSize; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                m_SendBufferSize = value;
                if (m_Socket != null) m_Socket.SendBufferSize = m_SendBufferSize;
            }
        }
        /// <summary>
        /// Gets or sets a value that specifies the size of the receive buffer of the underlying <see cref="Socket"/>.
        /// </summary>
        /// <exception cref="SocketException">An error occurred when attempting to access the socket.</exception>
        /// <exception cref="ObjectDisposedException">The <see cref="Socket"/> has been closed.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The value specified for a set operation is less than 0.</exception>
        public int ReceiveBufferSize
        {
            get { return m_Socket?.ReceiveBufferSize ?? m_ReceiveBufferSize; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                m_ReceiveBufferSize = value;
                if (m_Socket != null) m_Socket.ReceiveBufferSize = m_ReceiveBufferSize;
            }
        }
        /// <summary>
        /// Gets and returns true if a RDCP connection is open with the debug Xbox.
        /// </summary>
        public bool Connected
        {
            get; private set;
        }
        /// <summary>
        /// Gets and returns the debug monitor version that is running on the remote debug Xbox.
        /// </summary>
        public Version DebugMonitorVersion { get; private set; }
        /// <summary>
        /// Gets and returns the name of the debug Xbox console.
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// Gets and returns the IP end point of the debug Xbox console.
        /// </summary>
        public IPEndPoint RemoteEndPoint { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Xbox"/> class.
        /// </summary>
        public Xbox()
        {
            //Prepare End Point
            m_LocalEndPoint = new IPEndPoint(IPAddress.Any, 0);
        }
        /// <summary>
        /// Attempts to open a Remote Debugging and Control Protocol connection with the debug Xbox.
        /// </summary>
        public void Connect()
        {
            //Check
            if (Connected) return;

            try
            {
                //Initialize Socket
                m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_Socket.Bind(m_LocalEndPoint);

                //Attempt to connect
                m_Socket.Connect(RemoteEndPoint);
                m_Socket.ReceiveBufferSize = m_SendBufferSize;
                m_Socket.SendBufferSize = m_ReceiveBufferSize;
                m_Socket.NoDelay = true;

                //Get response
                GetResponse(out Status statusCode, out string message);
                if ((statusCode & Status.OK) == Status.OK)
                {
                    //Setup
                    Connected = true;

                    //Get version
                    Version dmVersion = new Version();
                    SendCommand("dmversion");
                    GetResponse(out statusCode, out string version);
                    if ((statusCode & Status.OK) == Status.OK)
                        dmVersion = new Version(version);
                }
            }
            catch { }
        }
        /// <summary>
        /// Attempts to close the RDCP connection with the debug Xbox.
        /// </summary>
        /// <param name="sayBye">If <see langword="true"/>, sends the "bye" command to the debug Xbox, otherwise if <see langword=""="false"/>, closes the socket.</param>
        public void Disconnect(bool sayBye = true)
        {
            //Check if connected
            if (Connected)
            {
                //Check
                if (sayBye)
                {
                    //Send disconnect command
                    SendCommand("bye");
                    GetResponse(out Status statusCode, out string message);

                    //Check response
                    if ((statusCode & Status.OK) == Status.OK)
                    { Close(); Dispose(); } //Close and cleanup
                }
                else
                { Close(); Dispose(); }
            }

            //Disconnect
            Connected = false;
        }
        /// <summary>
        /// Closes the underlying <see cref="Socket"/> used by the current instance of the <see cref="Xbox"/> class.
        /// </summary>
        public void Close()
        {
            //Close
            m_Socket.Close();
        }
        /// <summary>
        /// Releases all resources used by the current instance of the <see cref="Xbox"/> class.
        /// </summary>
        public void Dispose()
        {
            //Dispose
            m_Socket.Dispose();
        }
        /// <summary>
        /// Returns a string that represents the current <see cref="Xbox"/>.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"{(Connected ? "Connected" : "Disconnected")}: {Name} - {RemoteEndPoint.Address}";
        }
        /// <summary>
        /// Attempts to read a response line from the debug Xbox.
        /// </summary>
        /// <param name="timeout">The amount of time in milliseconds to wait until aborting the operation.</param>
        /// <returns>A line of text from the debug Xbox.</returns>
        public string ReceiveLine(int timeout = 10000)
        {
            //Wait for any amount of data
            WaitForData(timeout);

            //Prepare
            byte[] data = new byte[1024];
            string line = string.Empty;

            //Receive line
            int available = m_Socket.Available;
            if (available < data.Length) m_Socket.Receive(data, available, SocketFlags.Peek);
            else m_Socket.Receive(data, data.Length, SocketFlags.Peek);
            line = Encoding.ASCII.GetString(data);
            line = line.Substring(0, line.IndexOf("\r\n") + 2);
            m_Socket.Receive(data, line.Length, SocketFlags.None);

            //Return
            return line.Substring(0, line.Length - 2);
        }
        /// <summary>
        /// Attempts to ping the debug Xbox 
        /// </summary>
        /// <param name="timeout">The amount of time in milliseconds to wait until aborting the operation.</param>
        public bool Ping(int timeout = 10000)
        {
            //Prepare
            bool successful = false;

            if (Connected)
            {
                try
                {
                    //Send new line, and receive an error
                    SendCommand(Environment.NewLine);
                    ReceiveLine(timeout);

                    //Set
                    successful = true;
                }
                catch { successful = false; }
            }

            return successful;
        }
        /// <summary>
        /// Attempts to synchronize the communication with the debug Xbox.
        /// Desynchroniziation can occur when the debug Xbox is sending data that isn't handled by this API.
        /// In that case we may start sending requests but the debug Xbox is still waiting for us to accept the data that is being sent.
        /// </summary>
        /// <param name="timeout">The amount of time in milliseconds to wait until aborting the operation.</param>
        public void Synchronize(int timeout = 1000)
        {
            //Prepare
            byte[] buffer = null;
            DateTime start = DateTime.Now;

            while ((start - DateTime.Now).TotalMilliseconds < timeout)
            {
                try
                {
                    //Wait for data
                    WaitForData(timeout);

                    //Receive
                    buffer = new byte[m_Socket.Available];
                    m_Socket.Receive(buffer);

                    //Change start time
                    start = DateTime.Now;
                }
                catch { break; }
            }
        }
        /// <summary>
        /// Attempts to send a command to the debug Xbox.
        /// </summary>
        /// <param name="cmd">The command string.</param>
        /// <param name="args">The command arguments.</param>
        public void SendCommand(string cmd, params object[] args)
        {
            //Check
            if (!Connected) return;
            if (string.IsNullOrEmpty(cmd)) return;

            //Create command line
            string commandLine = string.Join(" ", cmd, string.Join(" ", args)) + "\r\n";
            byte[] commandData = Encoding.ASCII.GetBytes(commandLine);

            //Write
            m_Socket.Send(commandData, SocketFlags.None);
        }
        /// <summary>
        /// Attempts to receive a response from the debug Xbox.
        /// </summary>
        public void GetResponse()
        {
            GetResponse(out Status status, out string message);
        }
        /// <summary>
        /// Attempts to receive a response from the debug Xbox.
        /// </summary>
        /// <param name="statusCode">The status code of the response.</param>
        /// <param name="message">The response message.</param>
        public void GetResponse(out Status statusCode, out string message)
        {
            //Interpret
            Interpret(ReceiveLine(), out int status, out message);
            statusCode = (Status)status;

            //Invoke response event
            Response?.Invoke(this, new ResponseEventArgs(statusCode, message));
        }
        /// <summary>
        /// Attempts to get the Direct3D state of the debug Xbox.
        /// </summary>
        /// <param name="data">The output data buffer.</param>
        /// <returns></returns>
        public bool GetDirect3dState(out byte[] data)
        {
            //Check
            if (Connected)
            {
                data = null;
                return false;

                //Send rename command
                SendCommand("getd3dstate"); Thread.Sleep(10);
                GetResponse(out Status status, out string msg);
                if (status == Status.BinaryResponseFollows)
                {
                    //Prepare
                    data = new byte[0];
                    DownloadData(data, 0);    //Download

                    //Return
                    return true;
                }
            }

            //Return
            data = new byte[0];
            return false;

        }
        /// <summary>
        /// Attempts to reboot the debug Xbox using the specified boot type.
        /// By default, the Xbox will be cold rebooted.
        /// </summary>
        /// <param name="type">The boot type.</param>
        /// <returns><see langword="true"/> if the debug Xbox is being rebooted; otherwise <see langword="false"/>.</returns>
        public bool Reboot(BootType type = BootType.Cold)
        {
            if(Connected)
            {
                //Handle boot type
                switch (type)
                {
                    case BootType.Warm:
                        SendCommand("reboot", "warm"); break;
                    case BootType.NoDebug:
                        SendCommand("reboot", "nodebug"); break;
                    case BootType.Wait:
                        SendCommand("reboot", "wait"); break;
                    case BootType.Stop:
                        SendCommand("reboot", "stop"); break;
                    default:
                        SendCommand("reboot"); break;
                }

                //Get response
                GetResponse(out Status status, out string msg);
                return status == Status.OK;
            }

            return false;
        }
        /// <summary>
        /// Attempts to reboot the debug Xbox.
        /// </summary>
        /// <param name="warm">If <see langword="true"/> then the debug Xbox will reboot to the default dash; otherwise if <see langword="false"/> the debug Xbox will cold reboot.</param>
        /// <returns><see langword="true"/> if the debug Xbox is being rebooted.</returns>
        [Obsolete("Deprecated use Reboot(BootType) instead.")]
        public bool Reboot(bool warm = false)
        {
            //Send reboot command with optional warm argument
            SendCommand("reboot", $"{(warm ? "warm" : string.Empty)}"); Thread.Sleep(10);
            GetResponse(out Status status, out string msg);
            return status == Status.OK;
        }
        /// <summary>
        /// Attempts to get the debug Xbox's drive paths.
        /// </summary>
        /// <param name="drives">When this method returns, contains an array of strings containing the letter of the drives.</param>
        /// <returns><see langword="true"/> if the the method succeeds; otherwise <see langword="false"/>.</returns>
        public bool GetDrives(out string[] drives)
        {
            //Prepare
            List<string> driveList = new List<string>();
            bool successful = false;

            //Check
            if (Connected)
            {
                //Send drivelist command
                SendCommand("drivelist"); Thread.Sleep(10);
                GetResponse(out Status status, out string drivesString);
                if (status == Status.OK)
                {
                    driveList = drivesString.Select(c => c.ToString()).ToList();
                    successful = true;
                }
            }

            drives = driveList.ToArray();
            return successful;
        }
        /// <summary>
        /// Attempts to get information about any mounted utitlity drive partitions.
        /// </summary>
        /// <param name="infos"></param>
        /// <returns><see langword="true"/> if the the utility drive information was successfully received; otherwise <see langword="false"/>.</returns>
        public bool GetUtilityDriveInformation(out UtilityDriveInformation[] infos)
        {
            //Prepare
            List<UtilityDriveInformation> infoList = new List<UtilityDriveInformation>();

            //Check
            if (Connected)
            {
                //Send getutildrvinfo command
                SendCommand("getutildrvinfo");
                GetResponse(out Status status, out string line);
                if (status == Status.OK)
                {
                    //Loop through parts
                    foreach (string info in line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                        infoList.Add(UtilityDriveInformation.FromResponse(info));

                    //Return
                    infos = infoList.ToArray();
                    return true;
                }
            }

            //Return
            infos = new UtilityDriveInformation[0];
            return false;
        }
        /// <summary>
        /// Attempts to gets the information associated with a single drive on the debug Xbox.
        /// </summary>
        /// <param name="drivePath">The path to the drive.</param>
        /// <returns><see langword="true"/> if the the drive information was successfully received; otherwise <see langword="false"/>.</returns>
        public bool GetDriveInformation(string drivePath, out DriveInformation info)
        {
            //Prepare
            bool successful = false;
            string driveInfoLine = string.Empty;
            info = new DriveInformation();

            //Check
            if (Connected)
            {
                //Send drivelist command
                SendCommand("drivefreespace", new CommandArgument("name", drivePath));
                GetResponse(out Status status, out string line);
                if (status == Status.MultilineResponseFollows)
                {
                    //Read line(s) (even though it seems like it's just one)
                    do
                    {
                        driveInfoLine = line;
                        line = ReceiveLine();
                    }
                    while (line != MultilineTerminator);

                    //Parse
                    info = DriveInformation.FromResponse(driveInfoLine);

                    //Set
                    successful = true;
                }
            }

            return successful;
        }
        /// <summary>
        /// Attempts to format the specified drive on the debug Xbox.
        /// </summary>
        /// <param name="name">The path of the drive.</param>
        public bool FormatDrive(string name)
        {
            //Prepare
            bool successful = false;

            //Check
            if (Connected)
            {
                //Send fmtfat command
                SendCommand("fmtfat"); Thread.Sleep(10);
                GetResponse(out Status status, out string line);
                if (status == Status.MultilineResponseFollows)
                    while ((line = ReceiveLine()) != MultilineTerminator)
                        Thread.Sleep(1);
            }

            return successful;
        }
        /// <summary>
        /// Attempts to boot the debug Xbox into another title located at the specified filename with debugging enabled by default.
        /// </summary>
        /// <param name="fileName">The path on the remote console to the title.</param>
        /// <param name="debug">If <see langword="true"/> then the Xbox Debug Monitor will remain loaded while the title is ran; otherwise <see langword="false"/>.</param>
        /// <returns><see langoword="true"/> if the debug Xbox loaded the specified title; otherwise <see langword="false"/>.</returns>
        /// <remarks>If <see cref="MagicBoot(string, bool)"/> returns <see langword="true"/> the debug connection is forcibly closed by the remote console, 
        /// otherwise if it returns <see langword="false"/> the connection has to be closed by the caller.</remarks>
        public bool MagicBoot(string fileName, bool debug = true)
        {
            //Check
            if (Connected)
            {
                //Send magicboot command with optional debug argument
                SendCommand("magicboot", $"title=\"{fileName}\" {(debug ? "debug" : string.Empty)}");
                GetResponse(out Status status, out string msg);
                if (status == Status.OK) Disconnect(false);
                return status == Status.OK;
            }

            //Return
            return false;
        }
        /// <summary>
        /// Attempts to create a new directory on the debug Xbox.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        /// <returns><see langword="true"/> if the directory creation was successful; otherwise <see langword="false"/>.</returns>
        public bool MakeDirectory(string path)
        {
            //Check
            if (Connected)
            {
                //Send rename command
                SendCommand("mkdir", new CommandArgument("name", path)); Thread.Sleep(10);
                GetResponse(out Status status, out string msg);
                return status == Status.OK;
            }

            //Return
            return false;

        }
        /// <summary>
        /// Attempts to delete a specified file or directory on the debug Xbox.
        /// </summary>
        /// <param name="path">The path to the file or directory.</param>
        /// <param name="directory"><see langword="true"/> if <paramref name="path"/> points to a directory; otherwise <see langword="false"/>.</param>
        /// <returns><see langword="true"/> if the deletion was successful; otherwise <see langword="false"/>.</returns>
        public bool Delete(string path, bool directory = false)
        {
            //Check
            if (Connected)
            {
                //Send delete command
                if (!directory) SendCommand("delete", new CommandArgument("name", path));
                else SendCommand("delete", new CommandArgument("name", path), new CommandArgument("dir"));
                Thread.Sleep(10);
                GetResponse(out Status status, out string msg);
                return status == Status.OK;
            }

            //Return
            return false;
        }
        /// <summary>
        /// Attempts to move a specified file or directory to a new location on the debug Xbox.
        /// </summary>
        /// <param name="sourceFileName">The name of the file or fodler to move.</param>
        /// <param name="destFileName">New path and name for the file or directory.</param>
        /// <returns><see langword="true"/> if the move was successful; otherwise <see langword="false"/>.</returns>
        public bool Move(string sourceFileName, string destFileName)
        {
            //Check
            if (Connected)
            {
                //Send rename command
                SendCommand("rename", new CommandArgument("name", sourceFileName), new CommandArgument("newname", destFileName)); Thread.Sleep(10);
                GetResponse(out Status status, out string msg);
                return status == Status.OK;
            }

            //Return
            return false;
        }
        /// <summary>
        /// Attempts to take a screenshot of what the console is currently displaying.
        /// </summary>
        /// <param name="screenshot">If this function succeeds, <paramref name="screenshot"/> will contain a screenshot.</param>
        /// <returns><see langword="true"/> if the screenshot was taken successfully; otherwise <see langword="false"/>.</returns>
        public bool Screenshot(out Image screenshot)
        {
            //Prepare
            screenshot = null;
            byte[] headerData = new byte[97];

            //Check
            if (Connected)
            {
                //Send rename command
                SendCommand("screenshot"); Thread.Sleep(10);
                GetResponse(out Status status, out string msg);
                if (status == Status.BinaryResponseFollows)
                {
                    //Wait for header
                    WaitForData(97, 5000);
                    DownloadData(headerData, 97);
                    string headerString = Encoding.ASCII.GetString(headerData);
                }

                //Return
                return status.HasFlag(Status.OK);
            }

            //Return
            return false;
        }
        /// <summary>
        /// Attempts to download a file from the debug Xbox to a local file.
        /// These methods do not block the calling thread.
        /// </summary>
        /// <param name="remoteFileName">The name of the remote file from which to download.</param>
        /// <param name="fileName">The name of the local file that is to receive the data.</param>
        /// <param name="timeout">The amount of time in milliseconds to wait until aborting the operation.</param>
        public void GetFileAsync(string remoteFileName, string fileName, int timeout = 10000)
        {
            //Check
            if (Connected)
            {
                //Download
                int length = 0;
                byte[] buffer = null;
                int iterations = 0, remainder = 0, progress = 0;
                int progressPercentage = 0;
                Exception error = null;

                //Check status
                new Task(new Action(delegate
                {
                    //Send getfile command
                    SendCommand("getfile", new CommandArgument("name", remoteFileName));
                    GetResponse(out Status status, out string msg);

                    //Check status
                    if (status == Status.BinaryResponseFollows)
                    {
                        //Try
                        try
                        {
                            //Lock
                            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                                lock (m_Socket)
                                {
                                    //Get data length
                                    length = GetLengthPrefix(timeout);
                                    iterations = length / m_Socket.ReceiveBufferSize;
                                    remainder = length % m_Socket.ReceiveBufferSize;

                                    //Download data in blocks, iterating over the max receive size
                                    buffer = new byte[m_Socket.ReceiveBufferSize];
                                    for (int i = 0; i < iterations; i++)
                                    {
                                        //Wait
                                        WaitForData(m_Socket.ReceiveBufferSize, timeout);
                                        m_Socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);   //Download
                                        fs.Write(buffer, 0, buffer.Length);

                                        //Update progress
                                        progress += m_Socket.ReceiveBufferSize;
                                        progressPercentage = (int)(((float)progress / length) * 100f);
                                        DownloadProgressChanged?.Invoke(this, new DownloadProgressChangedEventArgs(progress, length, progressPercentage, null));
                                    }

                                    //Wait for any remaining data
                                    WaitForData(remainder, timeout);
                                    buffer = new byte[remainder];
                                    m_Socket.Receive(buffer, 0, remainder, SocketFlags.None); //Download
                                    fs.Write(buffer, 0, buffer.Length);

                                    //Update progress
                                    progress += remainder;
                                    progressPercentage = (int)(((float)progress / length) * 100f);
                                    DownloadProgressChanged?.Invoke(this, new DownloadProgressChangedEventArgs(progress, length, progressPercentage, null));
                                }
                        }
                        catch (Exception ex) { error = ex; }
                    }
                    else throw new XboxException(msg);
                    
                    //Completed
                    DownloadFileCompleted?.Invoke(this, new AsyncCompletedEventArgs(error, false, null));
                })).Start();
            }
        }
        /// <summary>
        /// Attempts to download a file from the debug Xbox as a <see cref="byte"/> array.
        /// These methods do not block the calling thread.
        /// </summary>
        /// <param name="remoteFileName">The name of the remote file from which to download.</param>
        /// <param name="timeout">The amount of time in milliseconds to wait until aborting the operation.</param>
        public void GetDataAsync(string remoteFileName, int timeout = 10000)
        {
            //Check
            if (Connected)
            {
                //Download
                int length = 0;
                byte[] buffer = null;
                int iterations = 0, remainder = 0, progress = 0;
                int progressPercentage = 0;
                Exception error = null;

                //Start task
                new Task(new Action(delegate
                {
                    //Send getfile command
                    SendCommand("getfile", new CommandArgument("name", remoteFileName));
                    GetResponse(out Status status, out string msg);

                    //Check status
                    if (status == Status.BinaryResponseFollows)
                    {
                        //Try
                        try
                        {
                            //Lock
                            lock (m_Socket)
                            {
                                //Get data length
                                length = GetLengthPrefix(timeout);
                                iterations = length / m_Socket.ReceiveBufferSize;
                                remainder = length % m_Socket.ReceiveBufferSize;
                                buffer = new byte[length];

                                //Download data in blocks, iterating over the max receive size
                                for (int i = 0; i < iterations; i++)
                                {
                                    //Wait
                                    WaitForData(m_Socket.ReceiveBufferSize, timeout);
                                    m_Socket.Receive(buffer, i * m_Socket.ReceiveBufferSize, m_Socket.ReceiveBufferSize, SocketFlags.None);   //Download

                                    //Update progress
                                    progress += m_Socket.ReceiveBufferSize;
                                    progressPercentage = (int)(((float)progress / length) * 100f);
                                    DownloadProgressChanged?.Invoke(this, new DownloadProgressChangedEventArgs(progress, length, progressPercentage, null));
                                }

                                //Wait for any remaining data
                                WaitForData(remainder, timeout);
                                m_Socket.Receive(buffer, length - remainder, remainder, SocketFlags.None); //Download

                                //Update progress
                                progress += remainder;
                                progressPercentage = (int)(((float)progress / length) * 100f);
                                DownloadProgressChanged?.Invoke(this, new DownloadProgressChangedEventArgs(progress, length, progressPercentage, null));
                            }
                        }
                        catch (Exception ex) { error = ex; }
                    }
                    else throw new XboxException(msg);

                    //Completed
                    DownloadDataCompleted?.Invoke(this, new DownloadDataCompletedEventArgs(buffer, error, false, null));
                })).Start();
            }
        }
        /// <summary>
        /// Attempts to upload a file to the debug Xbox.
        /// These methods do not block the calling thread.
        /// </summary>
        /// <param name="fileName">The file to send to the debug Xbox.</param>
        /// <param name="remoteFileName">The name of the remote file to receive the file data.</param>
        /// <param name="timeout">The amount of time in milliseconds to wait until aborting the operation.</param>
        public void SendFileAsync(string fileName, string remoteFileName, int timeout = 10000)
        {
            //Check
            if (Connected)
            {
                //Download
                int length = 0;
                byte[] buffer = null;
                int iterations = 0, remainder = 0, progress = 0;
                int progressPercentage = 0;
                Exception error = null;

                //Create task
                new Task(new Action(delegate
                {
                    //Open file
                    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        //Setup
                        length = (int)fs.Length;
                        iterations = length / m_Socket.SendBufferSize;
                        remainder = length % m_Socket.SendBufferSize;

                        //Send sendfile command
                        SendCommand("sendfile", new CommandArgument("name", remoteFileName), new CommandArgument("length", fs.Length));
                        GetResponse(out Status status, out string msg);

                        if (status == Status.SendBinaryData)
                        {
                            try
                            {
                                //Upload in blocks
                                buffer = new byte[m_Socket.SendBufferSize];
                                for (int i = 0; i < iterations; i++)
                                {
                                    //Upload
                                    fs.Read(buffer, 0, buffer.Length);
                                    m_Socket.Send(buffer, buffer.Length, SocketFlags.None);

                                    //Update progress
                                    progress += m_Socket.SendBufferSize;
                                    progressPercentage = (int)(((float)progress / length) * 100f);
                                    UploadProgressChanged?.Invoke(this, new UploadProgressChangedEventArgs(progress, m_Socket.SendBufferSize, length, progressPercentage, null));
                                }

                                //Upload remainder
                                buffer = new byte[remainder];
                                fs.Read(buffer, 0, buffer.Length);
                                m_Socket.Send(buffer, buffer.Length, SocketFlags.None);

                                //Update progress
                                progress += remainder;
                                progressPercentage = (int)(((float)progress / length) * 100f);
                                UploadProgressChanged?.Invoke(this, new UploadProgressChangedEventArgs(progress, remainder, length, progressPercentage, null));
                            }
                            catch (Exception ex) { error = ex; }
                        }
                        else error = new XboxException(msg);

                        //Completed
                        UploadFileCompleted?.Invoke(this, new UploadCompletedEventArgs(error, error != null, false, null));
                    }
                })).Start();
            }
        }
        /// <summary>
        /// Attempts to upload a data buffer to the debug Xbox.
        /// These methods do not block the calling thread.
        /// </summary>
        /// <param name="data">The data buffer to send to the debug Xbox.</param>
        /// <param name="remoteFileName">The name of the remote file to receive the data.</param>
        /// <param name="timeout">The amount of time in milliseconds to wait until aborting the operation.</param>
        public void SendDataAsync(byte[] data, string remoteFileName, int timeout = 10000)
        {
            //Check
            if (Connected)
            {
                //Download
                int length = 0;
                byte[] buffer = null;
                int iterations = 0, remainder = 0, progress = 0;
                int progressPercentage = 0;
                Exception error = null;

                //Create task
                new Task(new Action(delegate
                {
                    //Open file
                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        //Setup
                        length = (int)ms.Length;
                        iterations = length / m_Socket.SendBufferSize;
                        remainder = length % m_Socket.SendBufferSize;

                        //Send sendfile command
                        SendCommand("sendfile", new CommandArgument("name", remoteFileName), new CommandArgument("length", ms.Length));
                        GetResponse(out Status status, out string msg);

                        if (status == Status.SendBinaryData)
                        {
                            try
                            {
                                //Upload in blocks
                                buffer = new byte[m_Socket.SendBufferSize];
                                for (int i = 0; i < iterations; i++)
                                {
                                    //Upload
                                    ms.Read(buffer, 0, buffer.Length);
                                    m_Socket.Send(buffer, buffer.Length, SocketFlags.None);

                                    //Update progress
                                    progress += m_Socket.SendBufferSize;
                                    progressPercentage = (int)((float)progress / length * 100f);
                                    UploadProgressChanged?.Invoke(this, new UploadProgressChangedEventArgs(progress, m_Socket.SendBufferSize, length, progressPercentage, null));
                                }

                                //Upload remainder
                                buffer = new byte[remainder];
                                ms.Read(buffer, 0, buffer.Length);
                                m_Socket.Send(buffer, buffer.Length, SocketFlags.None);

                                //Update progress
                                progress += remainder;
                                progressPercentage = (int)(((float)progress / length) * 100f);
                                UploadProgressChanged?.Invoke(this, new UploadProgressChangedEventArgs(progress, remainder, length, progressPercentage, null));
                            }
                            catch (Exception ex) { error = ex; }
                        }
                        else throw new XboxException(msg);

                        //Completed
                        UploadDataCompleted?.Invoke(this, new UploadCompletedEventArgs(error, error != null, false, null));
                    }
                })).Start();
            }
        }
        /// <summary>
        /// Attempts to download a file from the debug Xbox to a local file.
        /// </summary>
        /// <param name="remoteFileName">The name of the remote file from which to download.</param>
        /// <param name="fileName">The name of the local file that is to receive the data.</param>
        /// <returns><see langword="true"/> if the file was downloaded successfully; otherwise <see langword="false"/>.</returns>
        public bool GetFile(string remoteFileName, string fileName)
        {
            //Prepare
            bool success = false;
            byte[] buffer = null;

            //Check
            if (Connected)
            {
                //Send rename command
                SendCommand("getfile", new CommandArgument("name", remoteFileName));
                GetResponse(out Status status, out string msg);

                //Check status
                if (status == Status.BinaryResponseFollows)
                {
                    //Prepare
                    int length = GetLengthPrefix();
                    buffer = new byte[length];

                    //Download
                    DownloadData(buffer, length);

                    //Write
                    using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                        fs.Write(buffer, 0, length);

                    //Succeeded
                    success = true;
                }
            }

            //Return
            return success;
        }
        /// <summary>
        /// Attempts to download a file from the debug Xbox as a <see cref="byte"/> array.
        /// </summary>
        /// <param name="remoteFileName">The path to the source file on the debug Xbox.</param>
        /// <param name="buffer">The buffer to download the file to.</param>
        /// <returns><see langword="true"/> if the file was downloaded successfully; otherwise <see langword="false"/>.</returns>
        public bool GetData(string remoteFileName, ref byte[] buffer)
        {
            //Check
            if (Connected)
            {
                //Send rename command
                SendCommand("getfile", new CommandArgument("name", remoteFileName));
                GetResponse(out Status status, out string msg);

                //Check status
                if (status == Status.BinaryResponseFollows)
                {
                    //Download
                    int length = GetLengthPrefix();
                    DownloadData(buffer, length);

                    //Return
                    return true;
                }
            }

            //Return
            return false;
        }
        /// <summary>
        /// Attempts to upload a file to the debug Xbox.
        /// </summary>
        /// <param name="fileName">The path to the source file.</param>
        /// <param name="remoteFileName">The path to the destination file on the debug Xbox.</param>
        /// <returns><see langword="true"/> if the upload was completed successfully; otherwise <see langword="false"/>.</returns>
        public bool SendFile(string fileName, string remoteFileName)
        {
            //Check
            if (Connected)
            {
                //Open file
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    //Send sendfile command
                    SendCommand("sendfile", new CommandArgument("name", remoteFileName), new CommandArgument("length", fs.Length));
                    GetResponse(out Status status, out string msg);

                    //Send
                    byte[] buffer = new byte[m_Socket.SendBufferSize];
                    for (int i = 0; i < fs.Length / m_Socket.SendBufferSize; i++)
                    {
                        fs.Read(buffer, 0, buffer.Length);
                        m_Socket.Send(buffer, buffer.Length, SocketFlags.None);
                    }
                    buffer = new byte[fs.Length % m_Socket.SendBufferSize];
                    fs.Read(buffer, 0, buffer.Length);
                    m_Socket.Send(buffer, buffer.Length, SocketFlags.None);
                }

                //Return
                return true;
            }
            return false;
        }
        /// <summary>
        /// Attempts to set attributes to a specified file or directory.
        /// </summary>
        /// <param name="fileName">The path to the file or directory to have it's attributes set on the debug Xbox.</param>
        /// <param name="created">The creation date of the file or directory.</param>
        /// <param name="modified">The last modified date of the file or directory.</param>
        /// <param name="attributes">The file or directory attributes.</param>
        /// <returns><see langword="true"/> if <paramref name="fileName"/> had it's attributes successfully set; otherwise, <see langword="false"/>.</returns>
        public bool SetAttributes(string fileName, DateTime created, DateTime modified, FileAttributes attributes)
        {
            //Check
            if (Connected)
            {
                //Send setfileattributes command
                SendCommand("setfileattributes", new CommandArgument("name", fileName),
                    new CommandArgument("createhi", $"0x{created.ToFileTime() >> 32:X8}"),
                    new CommandArgument("createlo", $"0x{created.ToFileTime() & 0xffffffff:X8}"),
                    new CommandArgument("changehi", $"0x{modified.ToFileTime() >> 32:X8}"),
                    new CommandArgument("changelo", $"0x{modified.ToFileTime() & 0xffffffff:X8}"),
                    new CommandArgument("readonly", attributes.HasFlag(FileAttributes.Hidden) ? 1 : 0),
                    new CommandArgument("hidden", attributes.HasFlag(FileAttributes.Hidden) ? 1 : 0));
                GetResponse(out Status status, out string msg);

                //Return
                return status.HasFlag(Status.OK);
            }

            //Return
            return false;
        }
        /// <summary>
        /// Attempts to get the directory listing of a specified directory on the debug Xbox.
        /// </summary>
        /// <param name="directory">The absolute path of the directory to query.</param>
        /// <param name="itemInfos">When this method returns, is an array of <see cref="ItemInformation"/> objects that contain information about the listings in <paramref name="directory"/>.
        /// This parameter is passed uninitialized; any value originally supplied in <paramref name="itemInfos"/> will be overwritten.</param>
        /// <returns><see langword="true"/> if <paramref name="directory"/> was successfully queried; otherwise, <see langword="false"/>.</returns>
        public bool GetDirectoryList(string directory, out ItemInformation[] itemInfos)
        {
            //Prepare
            List<ItemInformation> itemList = new List<ItemInformation>();

            //Check
            if (Connected)
            {
                //Send dirlist command
                SendCommand("dirlist", new CommandArgument("name", directory));
                GetResponse(out Status status, out string msg);

                //Check status
                if (status == Status.MultilineResponseFollows)
                {
                    //Prepare
                    string line = string.Empty;

                    //Receive infos
                    while ((line = ReceiveLine()) != MultilineTerminator)
                    {
                        ItemInformation itemInfo = ItemInformation.FromResponse(line);
                        itemInfo.Directory = directory;
                        itemList.Add(itemInfo);
                    }

                    //Return
                    itemInfos = itemList.ToArray();
                    return true;
                }
            }

            //Return
            itemInfos = new ItemInformation[0];
            return false;
        }
        public bool GetXbeInfo(out XboxExecutableInfo xbeInfo)
        {
            //Prepare
            xbeInfo = new XboxExecutableInfo();
            bool successful = false;

            //Check
            if (Connected)
            {
                //Send drivelist command
                SendCommand("xbeinfo running"); Thread.Sleep(10);
                GetResponse(out Status status, out string xbeInfoString);
                if (status.HasFlag(Status.OK))
                {
                    //Receive data
                    string timeAndChecksum = ReceiveLine();
                    string filename = ReceiveLine();

                    ReceiveLine();
                    xbeInfo = XboxExecutableInfo.FromResponse(timeAndChecksum, filename);
                    successful = true;
                }
            }
            
            return successful;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public bool GetMemory(byte[] buffer, long address, int length, int timeout = 10000)
        {
            //Prepare
            bool successful = false;

            //Check
            if (Connected)
            {
                //Send getmem2 command
                SendCommand("getmem2", new CommandArgument("addr", address), new CommandArgument("length", length));
                GetResponse(out Status status, out string message);
                if (status == Status.BinaryResponseFollows)
                    DownloadData(buffer, length, timeout);
                return status == Status.BinaryResponseFollows;
            }

            //Return
            return successful;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public bool SetMemory(long address, byte[] buffer, int length)
        {
            //Prepare
            bool successful = false;

            //Check
            if (Connected)
            {
                //Prepare
                List<object> commands = new List<object>() { new CommandArgument("addr", address) };
                foreach (byte b in buffer)
                    commands.Add(b.ToString("X2"));

                //Send getmem2 command
                SendCommand("setmem", commands.ToArray());
                GetResponse(out Status status, out string message);
                return status.HasFlag(Status.OK);
            }

            //Return
            return successful;
        }
        /// <summary>
        /// Waits for any amount of data to be avaliable.
        /// </summary>
        /// <param name="timeout">The amount of time in milliseconds to wait until aborting the operation.</param>
        private void WaitForData(int timeout = 10000)
        {
            //Wait
            WaitForData(1, timeout);
        }
        /// <summary>
        /// Waits for a specified amount of data to be available.
        /// </summary>
        /// <param name="target">The amount of data needing to be available.</param>
        /// <param name="timeout">The amount of time in milliseconds to wait until aborting the operation.</param>
        private void WaitForData(int target, int timeout = 10000)
        {
            if (!m_Socket.Connected) return;
            if (m_Socket.Available >= target) return;

            DateTime start = DateTime.Now;
            TimeSpan elapsed = new TimeSpan();
            while (m_Socket.Available < target)
            {
                Thread.Sleep(0);
                elapsed = DateTime.Now - start;
                if (elapsed.TotalMilliseconds >= timeout)
                {
                    Disconnect(false);
                    throw new TimeoutException();
                }
            }
        }
        /// <summary>
        /// Attempts to download a 32-bit signed integer containing the length of an incomming amount of data.
        /// </summary>
        /// <param name="timeout">The amount of time in milliseconds to wait until aborting the operation.</param>
        /// <returns>A 32-bit signed integer.</returns>
        private int GetLengthPrefix(int timeout = 10000)
        {
            //Wait
            WaitForData(4, timeout);

            //Prepare
            byte[] data = new byte[4];
            m_Socket.Receive(data, 4, SocketFlags.None);
            return BitConverter.ToInt32(data, 0);
        }
        /// <summary>
        /// Attempts to download the specified number of bytes into a download buffer.
        /// </summary>
        /// <param name="buffer">The array of type <see cref="byte"/> that is the storage location of the downloaded data.</param>
        /// <param name="size">The number of bytes to receive.</param>
        /// <param name="timeout">The amount of time in milliseconds to wait until aborting the operation.</param>
        private void DownloadData(byte[] buffer, int size, int timeout = 10000)
        {
            //Check
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (buffer.Length < size) throw new ArgumentOutOfRangeException(nameof(size));
            if (size == 0) return;

            //Prepare
            int iterations = buffer.Length / m_Socket.ReceiveBufferSize;
            int remainder = buffer.Length % m_Socket.ReceiveBufferSize;
            
            //Download data in blocks, iterating over the max receive size
            for (int i = 0; i < iterations; i++)
            {
                //Wait
                WaitForData(m_Socket.ReceiveBufferSize, timeout);
                m_Socket.Receive(buffer, i * m_Socket.ReceiveBufferSize, m_Socket.ReceiveBufferSize, SocketFlags.None);   //Download
            }

            //Wait for any remaining data
            WaitForData(remainder, timeout);
            m_Socket.Receive(buffer, buffer.Length - remainder, remainder, SocketFlags.None); //Download
        }
        /// <summary>
        /// Interprets a response line.
        /// </summary>
        /// <param name="line">The response line.</param>
        /// <param name="statusCode">When this function returns, contains the status code of the response.</param>
        /// <param name="message">When this function returns, contains the message of the response.</param>
        private void Interpret(string line, out int statusCode, out string message)
        {
            //Check
            if (string.IsNullOrEmpty(line) || line.Length < 5) { statusCode = 0; message = string.Empty; return; };

            //Interpret
            int.TryParse(line.Substring(0, 3), out statusCode);
            message = line.Substring(5);
        }
    }

    /// <summary>
    /// Represents the object that contains event data for the Xbox Response event.
    /// </summary>
    public class ResponseEventArgs : EventArgs
    {
        /// <summary>
        /// Gets and returns the status code of the response.
        /// </summary>
        public Status Status { get; private set; }
        /// <summary>
        /// Gets and returns the message text of the response.
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseEventArgs"/> class using the specified status and message.
        /// </summary>
        /// <param name="status">The status code of the response.</param>
        /// <param name="message">The message text of the response.</param>
        public ResponseEventArgs(Status status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
