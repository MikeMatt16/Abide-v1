using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Abide.DebugXbox
{
    internal class Helper
    {
        private static string[] GetCommand(string input)
        {
            char[] split = new char[] { ' ' };
            var result = input.Split('"').Select((s, i) => i % 2 == 0 ?
            s.Split(split, StringSplitOptions.RemoveEmptyEntries) :
            new string[] { s }).SelectMany(s => s).ToList();
            return result.ToArray();
        }

        public static Dictionary<string, string> GetParts(string line)
        {
            //Prepare
            Dictionary<string, string> parts = new Dictionary<string, string>();
            StringBuilder valueBuilder = new StringBuilder();
            StringBuilder nameBuilder = new StringBuilder();
            StringBuilder currentBuilder = nameBuilder;
            bool nested = false;

            //Loop
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '=' && !nested) currentBuilder = valueBuilder;
                else if (line[i] == '\"') nested = !nested;
                else if (line[i] == ' ' && !nested)
                {
                    parts.Add(nameBuilder.ToString(), valueBuilder.ToString());
                    currentBuilder = nameBuilder;
                    nameBuilder.Clear(); valueBuilder.Clear();
                }
                else currentBuilder.Append(line[i]);

                if (i + 1 == line.Length && nameBuilder.Length > 0)
                    parts.Add(nameBuilder.ToString(), valueBuilder.ToString());
            };

            //Return
            return parts;
        }
    }

    /// <summary>
    /// The exception that is thrown when a debug Xbox error occurs.
    /// </summary>
    public sealed class XboxException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XboxException"/> class using the specified message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public XboxException(string message) : base(message) { }
    }

    public sealed class XboxMemoryRegion
    {
        public uint BaseAddress { get; private set; }
        public int Size { get; private set; }
        public int Protect { get; private set; }
        internal static XboxMemoryRegion FromResponse(string region)
        {
            var parts = Helper.GetParts(region);
            return new XboxMemoryRegion()
            {
                BaseAddress = uint.Parse(parts["base"].Substring(2), NumberStyles.HexNumber),
                Size = int.Parse(parts["size"].Substring(2), NumberStyles.HexNumber),
                Protect = int.Parse(parts["protect"].Substring(2), NumberStyles.HexNumber),
            };
        }
    }

    public sealed class XboxExecutableInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime TimeStamp { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public uint Checksum { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public uint StackSize { get; private set; }
        /// <summary>
        /// Gets a new <see cref="XboxExecutableInfo"/> class using the specified xbe info line.
        /// </summary>
        /// <param name="timeAndChecksum">The line containing the time and checksum of the title.</param>
        /// <param name="fileName">The file name of the title.</param>
        /// <returns>A new <see cref="XboxExecutableInfo"/> class.</returns>
        internal static XboxExecutableInfo FromResponse(string timeAndChecksum, string fileName)
        {
            var parts = Helper.GetParts(string.Join(" ", timeAndChecksum, fileName));
            uint checksum = Convert.ToUInt32(parts["checksum"].Substring(2), 16);
            uint timestamp = Convert.ToUInt32(parts["timestamp"].Substring(2), 16);
            string name = parts["name"];
            return new XboxExecutableInfo()
            {
                FileName = name,
                Checksum = checksum,
                TimeStamp = new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, (int)timestamp)
            };
        }
    }

    /// <summary>
    /// Represents an item that contains information about a utility drive on the debug Xbox.
    /// </summary>
    public sealed class UtilityDriveInformation
    {
        /// <summary>
        /// Gets and returns the utility drive partition index.
        /// </summary>
        public int PartitionIndex { get; private set; }
        /// <summary>
        /// Gets and returns the title ID.
        /// </summary>
        public int TitleId { get; private set; }
        /// <summary>
        /// Gets and returns the last title ID.
        /// </summary>
        public int LastTitleId { get; private set; }
        /// <summary>
        /// Returns any title ID.
        /// </summary>
        /// <returns>A 32-bit signed integer containing the title ID.</returns>
        public int GetAnyTitleId()
        {
            return TitleId | LastTitleId;
        }
        /// <summary>
        /// Gets a new <see cref="UtilityDriveInformation"/> class using the specified drive information line.
        /// </summary>
        /// <param name="line">The line containing information about a drive.</param>
        /// <returns>A new <see cref="UtilityDriveInformation"/> class.</returns>
        internal static UtilityDriveInformation FromResponse(string info)
        {
            //Get Parts
            var parts = Helper.GetParts(info);
            
            //Prepare
            int partitionIndex = -1, titleId = 0, lastTitleId = 0;
            if (parts.ContainsKey("Part0_TitleId"))
            {
                partitionIndex = 0;
                titleId = int.Parse(parts["Part0_TitleId"].Substring(2), NumberStyles.HexNumber);
            }
            if (parts.ContainsKey("Part1_TitleId"))
            {
                partitionIndex = 1;
                titleId = int.Parse(parts["Part1_TitleId"].Substring(2), NumberStyles.HexNumber);
            }
            if (parts.ContainsKey("Part2_TitleId"))
            {
                partitionIndex = 2;
                titleId = int.Parse(parts["Part2_TitleId"].Substring(2), NumberStyles.HexNumber);
            }
            if (parts.ContainsKey("Part0_LastTitleId"))
            {
                partitionIndex = 0;
                lastTitleId = int.Parse(parts["Part0_LastTitleId"].Substring(2), NumberStyles.HexNumber);
            }
            if (parts.ContainsKey("Part1_LastTitleId"))
            {
                partitionIndex = 1;
                lastTitleId = int.Parse(parts["Part1_LastTitleId"].Substring(2), NumberStyles.HexNumber);
            }
            if (parts.ContainsKey("Part2_LastTitleId"))
            {
                partitionIndex = 2;
                lastTitleId = int.Parse(parts["Part2_LastTitleId"].Substring(2), NumberStyles.HexNumber);
            }

            //Return
            return new UtilityDriveInformation()
            {
                PartitionIndex = partitionIndex,
                TitleId = titleId,
                LastTitleId = lastTitleId
            };
        }
    }

    /// <summary>
    /// Represents an item that contains information about a drive on the debug Xbox.
    /// </summary>
    public sealed class DriveInformation
    {
        /// <summary>
        /// Gets and returns the size of the free space available to the user of the drive in bytes.
        /// </summary>
        public long FreeToCaller { get; private set; }
        /// <summary>
        /// Gets and retuns the capacity of the drive in bytes.
        /// </summary>
        public long TotalBytes { get; private set; }
        /// <summary>
        /// Gets and returns the size of the free space of the drive in bytes.
        /// </summary>
        public long TotalFreeBytes { get; private set; }
        /// <summary>
        /// Gets a new <see cref="DriveInformation"/> class using the specified drive information string.
        /// </summary>
        /// <param name="info">The string containing information about a drive.</param>
        /// <returns>A new <see cref="DriveInformation"/> class.</returns>
        internal static DriveInformation FromResponse(string info)
        {
            //Get Parts
            var parts = Helper.GetParts(info);

            //Prepare
            ulong freeToCaller = 0, totalBytes = 0, totalFreeBytes = 0;
            freeToCaller = ulong.Parse(parts["freetocallerlo"].Substring(2), NumberStyles.HexNumber);
            freeToCaller |= ulong.Parse(parts["freetocallerhi"].Substring(2), NumberStyles.HexNumber) << 32;
            totalBytes = ulong.Parse(parts["totalbyteslo"].Substring(2), NumberStyles.HexNumber);
            totalBytes |= ulong.Parse(parts["totalbyteshi"].Substring(2), NumberStyles.HexNumber) << 32;
            totalFreeBytes = ulong.Parse(parts["totalfreebyteslo"].Substring(2), NumberStyles.HexNumber);
            totalFreeBytes |= ulong.Parse(parts["totalfreebyteshi"].Substring(2), NumberStyles.HexNumber) << 32;

            //Return
            return new DriveInformation()
            {
                FreeToCaller = (long)freeToCaller,
                TotalBytes = (long)totalBytes,
                TotalFreeBytes = (long)totalFreeBytes
            };
        }
    }

    /// <summary>
    /// Represents an item that contains information about an item on the debug Xbox hard disk.
    /// </summary>
    public sealed class ItemInformation
    {
        /// <summary>
        /// Gets or sets the directory that this item resides in.m
        /// </summary>
        public string Directory { get; set; }
        /// <summary>
        /// Gets and returns the name of the item.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets and returns the size of the item.
        /// </summary>
        public long Size { get; private set; }
        /// <summary>
        /// Gets and returns the creation date time of the item.
        /// </summary>
        public DateTime Created { get; private set; }
        /// <summary>
        /// Gets and returns the modified date time of the item.
        /// </summary>
        public DateTime Modified { get; private set; }
        /// <summary>
        /// Gets and returns the file attributes of the item.
        /// </summary>
        public FileAttributes Attributes { get; private set; }
        /// <summary>
        /// Gets a new <see cref="ItemInformation"/> class using the specified item information string.
        /// </summary>
        /// <param name="info">The string containing information about a file or folder.</param>
        /// <returns>A new <see cref="ItemInformation"/> class.</returns>
        internal static ItemInformation FromResponse(string info)
        {
            //Get Parts
            var parts = Helper.GetParts(info);

            //Prepare
            ulong size = 0, createTime = 0, changeTime = 0;
            size = ulong.Parse(parts["sizelo"].Substring(2), NumberStyles.HexNumber);
            size |= ulong.Parse(parts["sizehi"].Substring(2), NumberStyles.HexNumber) << 32;
            createTime = ulong.Parse(parts["createlo"].Substring(2), NumberStyles.HexNumber);
            createTime |= ulong.Parse(parts["createhi"].Substring(2), NumberStyles.HexNumber) << 32;
            changeTime = ulong.Parse(parts["changelo"].Substring(2), NumberStyles.HexNumber);
            changeTime |= ulong.Parse(parts["changehi"].Substring(2), NumberStyles.HexNumber) << 32;

            //Get attributes
            FileAttributes attributes = 0;
            if (parts.ContainsKey("directory")) attributes |= FileAttributes.Directory;
            if (parts.ContainsKey("readonly")) attributes |= FileAttributes.ReadOnly;
            if (parts.ContainsKey("hidden")) attributes |= FileAttributes.Hidden;

            //Return
            return new ItemInformation()
            {
                Name = parts["name"],
                Size = (long)size,
                Created = DateTime.FromFileTime((long)createTime),
                Modified = DateTime.FromFileTime((long)changeTime),
                Attributes = attributes
            };
        }
    }
    
    /// <summary>
    /// Represents the method that will handle the <see cref="Xbox.DownloadProgressChanged"/> event of an <see cref="Xbox"/>.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="DownloadProgressChangedEventArgs"/> containing event data.</param>
    public delegate void DownloadProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs e);

    /// <summary>
    /// Provides data for the <see cref="Xbox.DownloadProgressChanged"/> event of an <see cref="Xbox"/>.
    /// </summary>
    public sealed class DownloadProgressChangedEventArgs : ProgressChangedEventArgs
    {
        /// <summary>
        /// Gets the number of bytes received.
        /// </summary>
        public int BytesReceived { get; }
        /// <summary>
        /// Gets the total number of bytes in a <see cref="Xbox"/> data download operation.
        /// </summary>
        public int TotalBytesToReceive { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadProgressChangedEventArgs"/> class.
        /// </summary>
        /// <param name="bytesReceived">The number of bytes received.</param>
        /// <param name="totalBytesToReceive">The total number of bytes in a <see cref="Xbox"/> data download operation.</param>
        /// <param name="progressPercentage">The percentage of an asynchronous task that has been completed.</param>
        /// <param name="userState">A unique user state.</param>
        public DownloadProgressChangedEventArgs(int bytesReceived, int totalBytesToReceive, int progressPercentage, object userState) :
            base(progressPercentage, userState)
        {
            BytesReceived = bytesReceived;
            TotalBytesToReceive = totalBytesToReceive;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void DownloadDataCompletedEventHandler(object sender, DownloadDataCompletedEventArgs e);
    
    /// <summary>
    /// 
    /// </summary>
    public class DownloadDataCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the data that is downloaded by a <see cref="Xbox.GetDataAsync(string, int)"/> method.
        /// </summary>
        public byte[] Data { get; }
        public Exception Error { get; }
        public bool Canceled { get; }
        public object UserState { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="error"></param>
        /// <param name="canceled"></param>
        /// <param name="userState"></param>
        public DownloadDataCompletedEventArgs(byte[] data, Exception error, bool canceled, object userState)
        {
            Data = data;
            Error = error;
            UserState = userState;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void UploadEventHandler(object sender, UploadCompletedEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    public class UploadCompletedEventArgs : EventArgs
    {
        public Exception Exception { get; }
        public bool Error { get; }
        public bool Canceled { get; }
        public object UserState { get; }

        public UploadCompletedEventArgs(Exception exception, bool error, bool canceled, object userState)
        {
            Exception = exception;
            Error = error;
            Canceled = canceled;
            UserState = userState;
        }
    }

    /// <summary>
    /// Represents the method that will handle the <see cref="Xbox.UploadProgressChanged"/> event of an <see cref="Xbox"/>.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="UploadProgressChangedEventArgs"/> containing event data.</param>
    public delegate void UploadProgressChangedEventHandler(object sender, UploadProgressChangedEventArgs e);
    
    /// <summary>
    /// 
    /// </summary>
    public sealed class UploadProgressChangedEventArgs : ProgressChangedEventArgs
    {
        /// <summary>
        /// Gets the total number of bytes sent.
        /// </summary>
        public int TotalBytesSent { get; }
        /// <summary>
        /// Gets the total number of bytes in a <see cref="Xbox"/> data upload operation.
        /// </summary>
        public int TotalBytesToSend { get; }
        /// <summary>
        /// Gets the number of bytes sent since the last update.
        /// </summary>
        public int BytesSent { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadProgressChangedEventArgs"/> class.
        /// </summary>
        /// <param name="totalBytesSent">The total number of bytes sent.</param>
        /// <param name="bytesSent">The number of bytes sent since the last update.</param>
        /// <param name="totalBytesToSend">The total number of bytes in a <see cref="Xbox"/> data upload operation.</param>
        /// <param name="progressPercentage">The percentage of an asynchronous task that has been completed.</param>
        /// <param name="userState">A unique user state.</param>
        public UploadProgressChangedEventArgs(int totalBytesSent, int bytesSent, int totalBytesToSend, int progressPercentage, object userState) :
            base(progressPercentage, userState)
        {
            TotalBytesSent = bytesSent;
            BytesSent = bytesSent;
            TotalBytesToSend = totalBytesToSend;
        }
    }
}
