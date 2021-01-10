using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
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
        /// Represents the maximum length that can be used when using the setmem command.
        /// </summary>
        private const int MaxSetMemLength = 120;
        /// <summary>
        /// Represents the default timeout duration.
        /// </summary>
        private const int DefaultTimeout = 5000;

        private readonly EndPoint localEndPoint;
        private int sendBufferSize = 20 * 0x100000, receiveBufferSize = 20 * 0x100000;
        private Socket socket = null;

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
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler ConnectionStateChanged;
        /// <summary>
        /// Gets or sets a value that specifies the size of the send buffer of the underlying <see cref="Socket"/>.
        /// </summary>
        /// <exception cref="SocketException">An error occurred when attempting to access the socket.</exception>
        /// <exception cref="ObjectDisposedException">The <see cref="Socket"/> has been closed.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The value specified for a set operation is less than 0.</exception>
        public int SendBufferSize
        {
            get => socket?.SendBufferSize ?? sendBufferSize;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                sendBufferSize = value;
                if (socket != null) socket.SendBufferSize = sendBufferSize;
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
            get => socket?.ReceiveBufferSize ?? receiveBufferSize;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                receiveBufferSize = value;
                if (socket != null) socket.ReceiveBufferSize = receiveBufferSize;
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
        /// 
        /// </summary>
        public Stream MemoryStream { get; private set; }
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
            localEndPoint = new IPEndPoint(IPAddress.Any, 0);
        }
        /// <summary>
        /// Attempts to open a Remote Debugging and Control Protocol connection with the debug Xbox.
        /// </summary>
        public void Connect()
        {
            if (Connected) return;

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(localEndPoint);

                socket.Connect(RemoteEndPoint);
                socket.ReceiveBufferSize = sendBufferSize;
                socket.SendBufferSize = receiveBufferSize;
                socket.NoDelay = true;

                if (GetResponse().HasFlag(Status.OK))
                {
                    Connected = true;
                    MemoryStream = new XboxMemoryStream(this);

                    SendCommand("dmversion");
                    if (GetResponse(out string version) == Status.OK)
                        DebugMonitorVersion = new Version(version);
                    else DebugMonitorVersion = new Version();

                    ConnectionStateChanged?.Invoke(this, new EventArgs());
                }
            }
            catch { socket.Close(); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task ConnectAsync()
        {
            if (Connected) return;

            await new Task(() =>
            {
                Connect();
            });
        }
        /// <summary>
        /// Attempts to close the RDCP connection with the debug Xbox.
        /// </summary>
        /// <param name="sayBye">If <see langword="true"/>, sends the "bye" command to the debug Xbox, otherwise if <see langword=""="false"/>, closes the socket.</param>
        public void Disconnect(bool sayBye = true)
        {
            if (Connected)
            {
                //Check
                if (sayBye)
                {
                    //Send disconnect command
                    SendCommand("bye");

                    //Check response
                    if (GetResponse() == Status.OK)
                    { Close(); Dispose(); } //Close and cleanup
                }
                else
                { Close(); Dispose(); }
            }

            //Disconnect
            Connected = false;
            MemoryStream.Dispose();
            MemoryStream = null;

            //Raise ConnectionStateChanged event
            ConnectionStateChanged?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// Closes the underlying <see cref="Socket"/> used by the current instance of the <see cref="Xbox"/> class.
        /// </summary>
        public void Close()
        {
            socket.Close();
        }
        /// <summary>
        /// Releases all resources used by the current instance of the <see cref="Xbox"/> class.
        /// </summary>
        public void Dispose()
        {
            socket.Dispose();
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
        public string ReceiveLine(int timeout = DefaultTimeout)
        {
            //Wait for any amount of data
            WaitForData(timeout);

            //Prepare
            byte[] data = new byte[1024];
            string line = string.Empty;

            //Receive line
            int available = socket.Available;
            if (available < data.Length) socket.Receive(data, available, SocketFlags.Peek);
            else socket.Receive(data, data.Length, SocketFlags.Peek);
            line = Encoding.ASCII.GetString(data);
            line = line.Substring(0, line.IndexOf("\r\n") + 2);
            socket.Receive(data, line.Length, SocketFlags.None);

            //Return
            return line.Substring(0, line.Length - 2);
        }
        /// <summary>
        /// Attempts to ping the debug Xbox 
        /// </summary>
        /// <param name="timeout">The amount of time in milliseconds to wait until aborting the operation.</param>
        public bool Ping(int timeout = DefaultTimeout)
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
        public void Synchronize(int timeout = DefaultTimeout)
        {
            DateTime start = DateTime.Now;
            while ((start - DateTime.Now).TotalMilliseconds < timeout)
            {
                try
                {
                    WaitForData(timeout);
                    if (socket.Available == 0)
                    {
                        break;
                    }

                    byte[] buffer = new byte[socket.Available];
                    socket.Receive(buffer);
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
            socket.Send(commandData, SocketFlags.None);
        }
        /// <summary>
        /// Attempts to receive a response from the debug Xbox.
        /// </summary>
        public Status GetResponse()
        {
            Interpret(ReceiveLine(), out int status, out string message);
            Response?.Invoke(this, new ResponseEventArgs((Status)status, message));
            return (Status)status;
        }
        /// <summary>
        /// Attempts to receive a response from the debug Xbox.
        /// </summary>
        /// <param name="statusCode">The status code of the response.</param>
        /// <param name="message">The response message.</param>
        public Status GetResponse(out string message)
        {
            Interpret(ReceiveLine(), out int status, out message);
            Response?.Invoke(this, new ResponseEventArgs((Status)status, message));
            return (Status)status;
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
                if (GetResponse() == Status.BinaryResponseFollows)
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
                return GetResponse() == Status.OK;
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
            return GetResponse() == Status.OK;
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
                SendCommand("drivelist");
                Thread.Sleep(50);
                if (GetResponse(out string drivesString) == Status.OK)
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
                if (GetResponse(out string line) == Status.OK)
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
                if (GetResponse(out string line) == Status.MultilineResponseFollows)
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
                if (GetResponse(out string line) == Status.MultilineResponseFollows)
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
                bool success = GetResponse(out string msg) == Status.OK;
                if (success)
                {
                    Disconnect(false);
                }

                return success;
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
                return GetResponse(out string msg) == Status.OK;
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
                return GetResponse(out string msg) == Status.OK;
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
                return GetResponse(out string msg) == Status.OK;
            }

            //Return
            return false;
        }
        /// <summary>
        /// Attempts to take a screenshot of what the console is currently displaying.
        /// </summary>
        /// <param name="screenshot">If this function succeeds, <paramref name="screenshot"/> will contain a screenshot.</param>
        /// <returns><see langword="true"/> if the screenshot was taken successfully; otherwise <see langword="false"/>.</returns>
        public bool Screenshot(out Bitmap screenshot)
        {
            screenshot = null;
            byte[] headerData = new byte[97];

            if (Connected)
            {
                SendCommand("screenshot"); Thread.Sleep(10);
                var status = GetResponse(out string msg);
                if (status == Status.BinaryResponseFollows)
                {
                    WaitForData(97, 2000);
                    DownloadData(headerData, 97);
                    XboxScreenshotInformation info = XboxScreenshotInformation.FromResponse(Encoding.ASCII.GetString(headerData));
                    int bpp = info.FrameBufferSize / (info.Width * info.Height);
                    byte[] frameBuffer = new byte[info.FrameBufferSize];
                    byte[] decoded = new byte[info.FrameBufferSize];
                    WaitForData(info.FrameBufferSize, 2000);
                    DownloadData(frameBuffer, info.FrameBufferSize);

                    Bitmap bmp = new Bitmap(info.Width, info.Height, PixelFormat.Format32bppRgb);
                    BitmapData data = null;
                    switch (info.Format)
                    {
                        case 18:
                        case 30:
                            try
                            {
                                data = bmp.LockBits(new Rectangle(0, 0, info.Width, info.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                                for (int i = 0; i < info.Width * info.Height; i++)
                                {
                                    int a = i * 4;
                                    decoded[a + 0] = frameBuffer[a + 2];
                                    decoded[a + 1] = frameBuffer[a + 3];
                                    decoded[a + 2] = frameBuffer[a + 0];
                                    decoded[a + 3] = frameBuffer[a + 1];
                                }

                                Marshal.Copy(decoded, 0, data.Scan0, data.Stride * data.Height);
                            }
                            finally
                            {
                                if (data != null)
                                {
                                    bmp.UnlockBits(data);
                                }
                            }
                            break;
                    }

                    screenshot = bmp;
                }

                return status.HasFlag(Status.OK);
            }

            return false;
        }
        /// <summary>
        /// Attempts to download a file from the debug Xbox to a local file.
        /// These methods do not block the calling thread.
        /// </summary>
        /// <param name="remoteFileName">The name of the remote file from which to download.</param>
        /// <param name="fileName">The name of the local file that is to receive the data.</param>
        /// <param name="timeout">The amount of time in milliseconds to wait until aborting the operation.</param>
        public void GetFileAsync(string remoteFileName, string fileName, int timeout = DefaultTimeout)
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
                    var status = GetResponse(out string msg);

                    //Check status
                    if (status == Status.BinaryResponseFollows)
                    {
                        //Try
                        try
                        {
                            //Lock
                            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                                lock (socket)
                                {
                                    //Get data length
                                    length = GetLengthPrefix(timeout);
                                    iterations = length / socket.ReceiveBufferSize;
                                    remainder = length % socket.ReceiveBufferSize;

                                    //Download data in blocks, iterating over the max receive size
                                    buffer = new byte[socket.ReceiveBufferSize];
                                    for (int i = 0; i < iterations; i++)
                                    {
                                        //Wait
                                        WaitForData(socket.ReceiveBufferSize, timeout);
                                        socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);   //Download
                                        fs.Write(buffer, 0, buffer.Length);

                                        //Update progress
                                        progress += socket.ReceiveBufferSize;
                                        progressPercentage = (int)((float)progress / length * 100f);
                                        DownloadProgressChanged?.Invoke(this, new DownloadProgressChangedEventArgs(progress, length, progressPercentage, null));
                                    }

                                    //Wait for any remaining data
                                    WaitForData(remainder, timeout);
                                    buffer = new byte[remainder];
                                    socket.Receive(buffer, 0, remainder, SocketFlags.None); //Download
                                    fs.Write(buffer, 0, buffer.Length);

                                    //Update progress
                                    progress += remainder;
                                    progressPercentage = (int)((float)progress / length * 100f);
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
        public void GetDataAsync(string remoteFileName, int timeout = DefaultTimeout)
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

                    //Check status
                    if (GetResponse(out string msg) == Status.BinaryResponseFollows)
                    {
                        //Try
                        try
                        {
                            //Lock
                            lock (socket)
                            {
                                //Get data length
                                length = GetLengthPrefix(timeout);
                                iterations = length / socket.ReceiveBufferSize;
                                remainder = length % socket.ReceiveBufferSize;
                                buffer = new byte[length];

                                //Download data in blocks, iterating over the max receive size
                                for (int i = 0; i < iterations; i++)
                                {
                                    //Wait
                                    WaitForData(socket.ReceiveBufferSize, timeout);
                                    socket.Receive(buffer, i * socket.ReceiveBufferSize, socket.ReceiveBufferSize, SocketFlags.None);   //Download

                                    //Update progress
                                    progress += socket.ReceiveBufferSize;
                                    progressPercentage = (int)((float)progress / length * 100f);
                                    DownloadProgressChanged?.Invoke(this, new DownloadProgressChangedEventArgs(progress, length, progressPercentage, null));
                                }

                                //Wait for any remaining data
                                WaitForData(remainder, timeout);
                                socket.Receive(buffer, length - remainder, remainder, SocketFlags.None); //Download

                                //Update progress
                                progress += remainder;
                                progressPercentage = (int)(progress * 100d / length);
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
        public void SendFileAsync(string fileName, string remoteFileName, int timeout = DefaultTimeout)
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
                        iterations = length / socket.SendBufferSize;
                        remainder = length % socket.SendBufferSize;

                        //Send sendfile command
                        SendCommand("sendfile", new CommandArgument("name", remoteFileName), new CommandArgument("length", fs.Length));
                        if (GetResponse(out string msg) == Status.SendBinaryData)
                        {
                            try
                            {
                                //Upload in blocks
                                buffer = new byte[socket.SendBufferSize];
                                for (int i = 0; i < iterations; i++)
                                {
                                    //Upload
                                    fs.Read(buffer, 0, buffer.Length);
                                    socket.Send(buffer, buffer.Length, SocketFlags.None);

                                    //Update progress
                                    progress += socket.SendBufferSize;
                                    progressPercentage = (int)((float)progress / length * 100f);
                                    UploadProgressChanged?.Invoke(this, new UploadProgressChangedEventArgs(progress, socket.SendBufferSize, length, progressPercentage, null));
                                }

                                //Upload remainder
                                buffer = new byte[remainder];
                                fs.Read(buffer, 0, buffer.Length);
                                socket.Send(buffer, buffer.Length, SocketFlags.None);

                                //Update progress
                                progress += remainder;
                                progressPercentage = (int)((float)progress / length * 100f);
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
        public void SendDataAsync(byte[] data, string remoteFileName, int timeout = DefaultTimeout)
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
                        iterations = length / socket.SendBufferSize;
                        remainder = length % socket.SendBufferSize;

                        //Send sendfile command
                        SendCommand("sendfile", new CommandArgument("name", remoteFileName), new CommandArgument("length", ms.Length));
                        if (GetResponse(out string msg) == Status.SendBinaryData)
                        {
                            try
                            {
                                //Upload in blocks
                                buffer = new byte[socket.SendBufferSize];
                                for (int i = 0; i < iterations; i++)
                                {
                                    //Upload
                                    ms.Read(buffer, 0, buffer.Length);
                                    socket.Send(buffer, buffer.Length, SocketFlags.None);

                                    //Update progress
                                    progress += socket.SendBufferSize;
                                    progressPercentage = (int)((float)progress / length * 100f);
                                    UploadProgressChanged?.Invoke(this, new UploadProgressChangedEventArgs(progress, socket.SendBufferSize, length, progressPercentage, null));
                                }

                                //Upload remainder
                                buffer = new byte[remainder];
                                ms.Read(buffer, 0, buffer.Length);
                                socket.Send(buffer, buffer.Length, SocketFlags.None);

                                //Update progress
                                progress += remainder;
                                progressPercentage = (int)((float)progress / length * 100f);
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
                if (GetResponse(out string msg) == Status.BinaryResponseFollows)
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
                if (GetResponse() == Status.BinaryResponseFollows)
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
                    if (GetResponse() == Status.SendBinaryData)
                    {
                        //Send
                        byte[] buffer = new byte[socket.SendBufferSize];
                        for (int i = 0; i < fs.Length / socket.SendBufferSize; i++)
                        {
                            fs.Read(buffer, 0, buffer.Length);
                            socket.Send(buffer, buffer.Length, SocketFlags.None);
                        }
                        buffer = new byte[fs.Length % socket.SendBufferSize];
                        fs.Read(buffer, 0, buffer.Length);
                        socket.Send(buffer, buffer.Length, SocketFlags.None);
                    }
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
            if (Connected)
            {
                SendCommand("setfileattributes", new CommandArgument("name", fileName),
                    new CommandArgument("createhi", $"0x{created.ToFileTime() >> 32:X8}"),
                    new CommandArgument("createlo", $"0x{created.ToFileTime() & 0xffffffff:X8}"),
                    new CommandArgument("changehi", $"0x{modified.ToFileTime() >> 32:X8}"),
                    new CommandArgument("changelo", $"0x{modified.ToFileTime() & 0xffffffff:X8}"),
                    new CommandArgument("readonly", attributes.HasFlag(FileAttributes.Hidden) ? 1 : 0),
                    new CommandArgument("hidden", attributes.HasFlag(FileAttributes.Hidden) ? 1 : 0));

                return GetResponse().HasFlag(Status.OK);
            }

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
                if (GetResponse() == Status.MultilineResponseFollows)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xbeInfo"></param>
        /// <returns></returns>
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
                if (GetResponse().HasFlag(Status.OK))
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
        /// <param name="count"></param>
        /// <returns></returns>
        public int GetMemory(byte[] buffer, long address, int offset, int count, int timeout = DefaultTimeout)
        {
            int bytesRead = 0;
            if (Connected)
            {
                if (!CheckAddress(address, 1, timeout) || !CheckAddress(address + count - 1, 1, timeout))
                {
                    throw new IOException("Invalid address.");
                }

                SendCommand("getmem2", new CommandArgument("addr", $"0x{address:X8}"), new CommandArgument("length", count));
                if (GetResponse() == Status.BinaryResponseFollows)
                {
                    byte[] data = new byte[count];
                    DownloadData(data, count, timeout);
                    data.CopyTo(buffer, offset);
                    bytesRead += count;
                }
            }

            return bytesRead;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public bool SetMemory(byte[] buffer, long address, int offset, int length, int timeout = DefaultTimeout)
        {
            if (Connected)
            {
                if (!CheckAddress(address, 1, timeout) || !CheckAddress(address + length - 1, 1, timeout))
                {
                    throw new IOException("Invalid address.");
                }

                int iterations = length / MaxSetMemLength;
                int remainder = length % MaxSetMemLength;
                string data;

                for (int i = 0; i < iterations; i++)
                {
                    data = string.Concat(buffer
                        .Skip(offset)
                        .Take(MaxSetMemLength)
                        .Select(b => b.ToString("X2")));

                    SendCommand("setmem", new CommandArgument("addr", $"0x{address + offset:X8}"), new CommandArgument("data", data));
                    if (GetResponse() != Status.OK)
                    {
                        return false;
                    }

                    offset += MaxSetMemLength;
                }

                if (remainder > 0)
                {
                    data = string.Concat(buffer
                        .Skip(offset)
                        .Take(remainder)
                        .Select(b => b.ToString("X2")));

                    SendCommand("setmem", new CommandArgument("addr", $"0x{address + offset:X8}"), new CommandArgument("data", data));
                    if (GetResponse() != Status.OK)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memoryRegions"></param>
        /// <returns></returns>
        public bool WalkMemory(out XboxMemoryRegion[] memoryRegions)
        {
            List<XboxMemoryRegion> regions = new List<XboxMemoryRegion>();
            bool success = false;
            string line = string.Empty;

            if (Connected)
            {
                SendCommand("walkmem");
                if (GetResponse() == Status.MultilineResponseFollows)
                {
                    line = ReceiveLine();

                    do
                    {
                        regions.Add(XboxMemoryRegion.FromResponse(line));
                        line = ReceiveLine();
                    }
                    while (line != MultilineTerminator);

                    success = true;
                }
            }

            memoryRegions = regions.ToArray();
            return success;
        }
        private bool CheckAddress(long address, int count, int timeout = DefaultTimeout)
        {
            if (Connected)
            {
                SendCommand("getmem", new CommandArgument("addr", $"0x{address:X8}"), new CommandArgument("length", count));
                if (GetResponse() == Status.MultilineResponseFollows)
                {
                    bool valid = true;
                    var line = ReceiveLine(timeout);
                    do
                    {
                        if (line.Contains("?"))
                        {
                            valid = false;
                        }

                        line = ReceiveLine(timeout);
                    }
                    while (line != MultilineTerminator && valid);
                    return valid;
                }
            }

            return false;
        }
        private void WaitForData(int timeout = DefaultTimeout)
        {
            //Wait
            WaitForData(1, timeout);
        }
        private void WaitForData(int target, int timeout = DefaultTimeout)
        {
            if (target > 0)
            {
                if (!socket.Connected) return;
                if (socket.Available >= target) return;

                DateTime start = DateTime.Now;
                TimeSpan elapsed = new TimeSpan();
                while (socket.Available < target)
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
        }
        private int GetLengthPrefix(int timeout = DefaultTimeout)
        {
            //Wait
            WaitForData(4, timeout);

            //Prepare
            byte[] data = new byte[4];
            socket.Receive(data, 4, SocketFlags.None);
            return BitConverter.ToInt32(data, 0);
        }
        private void DownloadData(byte[] buffer, int size, int timeout = DefaultTimeout)
        {
            //Check
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (buffer.Length < size) throw new ArgumentOutOfRangeException(nameof(size));
            if (size == 0) return;

            //Prepare
            int iterations = size / socket.ReceiveBufferSize;
            int remainder = size % socket.ReceiveBufferSize;
            
            //Download data in blocks, iterating over the max receive size
            for (int i = 0; i < iterations; i++)
            {
                //Wait
                WaitForData(socket.ReceiveBufferSize, timeout);
                socket.Receive(buffer, i * socket.ReceiveBufferSize, socket.ReceiveBufferSize, SocketFlags.None);   //Download
            }

            //Wait for any remaining data
            if (remainder > 0)
            {
                WaitForData(remainder, timeout);
                socket.Receive(buffer, buffer.Length - remainder, remainder, SocketFlags.None); //Download
            }
        }
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
