using Abide.DebugXbox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace XbExplorer
{
    public partial class FileUploadDialog : Form
    {
        /// <summary>
        /// Gets and returns the current Xbox instance.
        /// </summary>
        public Xbox Xbox { get; }

        private KeyValuePair<string, string> m_CurrentUpload;
        private Queue<KeyValuePair<string, string>> m_UploadQueue;
        private readonly LocalObject[] m_LocalObjects;
        private readonly string m_DestinationDirectoryName;
        private readonly int m_FileCount;
        private int m_CurrentFileCount;
        private int m_XboxSendBufferSize;
        private int m_TotalSentBytes;
        private float m_AverageUploadRate;
        private DateTime m_StartUploadTime;

        private FileUploadDialog()
        {
            InitializeComponent();
            
            //Initialize
            m_CurrentFileCount = 0;
            m_XboxSendBufferSize = 0;
            m_TotalSentBytes = 0;
            m_AverageUploadRate = 0f;
        }

        public FileUploadDialog(Xbox xbox, string destinationDirectoryName, params LocalObject[] objects) : this()
        {
            //Setup
            Xbox = xbox ?? throw new ArgumentNullException(nameof(xbox));
            m_LocalObjects = objects ?? throw new ArgumentNullException(nameof(objects));
            m_DestinationDirectoryName = destinationDirectoryName;

            //Connect
            xbox.Connect();

            //Loop
            foreach (LocalObject localObject in objects.Where(o => o.IsDirectory))
                foreach (string directory in localObject.NestedDirectories)
                {
                    //Get info
                    DirectoryInfo info = new DirectoryInfo(directory);
                    string targetPath = GetRemoteName(m_DestinationDirectoryName, localObject.Root, directory);

                    //Make directory
                    if (xbox.MakeDirectory(targetPath))
                        xbox.SetAttributes(targetPath, info.CreationTime, info.LastWriteTime, info.Attributes);
                }

            //Disconnect
            xbox.Disconnect();

            //Get file count
            m_FileCount = objects.Count(o => o.IsFile) + objects.SelectMany(o => o.NestedFiles).Count();

            //Setup
            xbox.UploadFileCompleted += Xbox_UploadFileCompleted;
            xbox.UploadProgressChanged += Xbox_UploadProgressChanged;
        }
        
        private void cancelButton_Click(object sender, EventArgs e)
        {
            //Check
            if(m_UploadQueue.Count == 0)
            {
                Xbox.Disconnect();
                DialogResult = DialogResult.Cancel;
            }

            //Clear queue
            m_UploadQueue.Clear();
            cancelButton.Text = "Cancelling...";
        }

        private void FileUploadDialog_Load(object sender, EventArgs e)
        {
            //Prepare file queue
            List<KeyValuePair<string, string>> queue = new List<KeyValuePair<string, string>>();
            foreach (LocalObject localObject in m_LocalObjects)    //Loop through objects
            {
                if (localObject.IsFile) queue.Add(new KeyValuePair<string, string>(localObject.Path, 
                    GetRemoteName(m_DestinationDirectoryName, localObject.Root, localObject.Path)));
                else if (localObject.IsDirectory)
                    foreach (string nestedFile in localObject.NestedFiles)
                        queue.Add(new KeyValuePair<string, string>(nestedFile, GetRemoteName(m_DestinationDirectoryName, localObject.Root, nestedFile)));
            }

            //Connect
            Xbox.Connect();
            m_XboxSendBufferSize = Xbox.SendBufferSize;

            //Setup
            m_UploadQueue = new Queue<KeyValuePair<string, string>>(queue);
            totalProgressBar.Value = (m_CurrentFileCount * 100) / m_FileCount;

            //Start
            m_StartUploadTime = DateTime.Now;
            m_CurrentUpload = m_UploadQueue.Dequeue();
            UploadFile(m_CurrentUpload.Key, m_CurrentUpload.Value);
        }

        private void Xbox_UploadFileCompleted(object sender, UploadCompletedEventArgs e)
        {
            //Invoke
            Invoke(new MethodInvoker(delegate
            {
                //Increment
                m_CurrentFileCount++;

                //Get attributes
                FileInfo info = new FileInfo(m_CurrentUpload.Key);

                //Set
                Xbox.SetAttributes(m_CurrentUpload.Value, info.CreationTime, info.LastWriteTime, info.Attributes);
                Xbox.SendBufferSize = m_XboxSendBufferSize;

                //Disconnect
                Xbox.Disconnect();

                //Check
                if (m_UploadQueue.Count > 0)
                {
                    //Connect
                    Xbox.Connect();

                    //Send next
                    m_CurrentUpload = m_UploadQueue.Dequeue();
                    UploadFile(m_CurrentUpload.Key, m_CurrentUpload.Value);
                }
                else DialogResult = DialogResult.OK;

                //Set progress
                totalProgressBar.Value = (m_CurrentFileCount * 100) / m_FileCount;
                fileProgressBar.Value = 0;
            }));
        }

        private void Xbox_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            //Update progress
            m_TotalSentBytes += e.BytesSent;

            //Setup
            TimeSpan elapsedTime = DateTime.Now - m_StartUploadTime;
            m_AverageUploadRate = (float)(m_TotalSentBytes / elapsedTime.TotalSeconds);

            //Invoke
            if (IsHandleCreated)
                Invoke(new MethodInvoker(delegate
                {
                    Text = $"Uploading... ({GetSizeString((int)Math.Ceiling(m_AverageUploadRate))}/ s)";
                    fileProgressBar.Value = e.ProgressPercentage;
                }));
        }

        private void FileUploadDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Remove events
            Xbox.UploadProgressChanged -= Xbox_UploadProgressChanged;
            Xbox.UploadFileCompleted -= Xbox_UploadFileCompleted;
        }

        private void UploadFile(string sourceFileName, string destinationFileName)
        {
            //Set
            Xbox.SendBufferSize = 0x80000;  //8 MiB

            //Setup
            localFileNameLabel.Text = GetCompactPath(sourceFileName, 50);
            targetFileNameLabel.Text = GetCompactPath(destinationFileName, 50);

            //Upload
            Xbox.SendFileAsync(sourceFileName, destinationFileName);
        }

        private static string GetRemoteName(string destinationRoot, string sourceRoot, string path)
        {
            return GetRemoteName(destinationRoot, GetLocalName(sourceRoot, path));
        }

        private static string GetRemoteName(string root, string localPath)
        {
            return Path.Combine(root, localPath);
        }

        private static string GetLocalName(string root, string path)
        {
            if (path.StartsWith(root))
            {
                path = path.Substring(root.Length);
                if (path.StartsWith("\\")) path = path.Substring(1);
                return path;
            }

            throw new ArgumentException();
        }

        private static string GetSizeString(long size)
        {
            StringBuilder sb = new StringBuilder(128);
            StrFormatByteSize(size, sb, sb.Capacity);
            return sb.ToString();
        }

        private const int MAX_CHAR = byte.MaxValue;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetShortPathName(
            [MarshalAs(UnmanagedType.LPWStr)] string path,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder shortPath,
            int shortPathLength);
        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        public static extern long StrFormatByteSize(
            long fileSize,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder buffer,
            int bufferSize);
        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        private static extern bool PathCompactPathEx(
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszOut,
            [MarshalAs(UnmanagedType.LPWStr)] string pszSrc,
            uint cchMax,
            uint dwFlags);

        /// <summary>
        /// Retrieves the short path string form of the specified path string.
        /// </summary>
        /// <param name="path">The file name to shorten.</param>
        /// <returns>The short form the the path that <paramref name="path"/> specifies.</returns>
        public static string GetShortPath(string path)
        {
            if (!File.Exists(path)) return path;
            StringBuilder builder = new StringBuilder(MAX_CHAR);
            GetShortPathName(path, builder, MAX_CHAR);
            return builder.ToString();
        }

        /// <summary>
        /// Truncates a path to fit within a certain number of characters by replacing path components with ellipses. 
        /// </summary>
        /// <param name="path">The path to be altered.</param>
        /// <param name="charCount">The maximum number of characters to be contained in the new string.</param>
        /// <returns>A string that has been altered.</returns>
        public static string GetCompactPath(string path, int charCount)
        {
            StringBuilder builder = new StringBuilder(MAX_CHAR);
            if (PathCompactPathEx(builder, path, (uint)charCount, 0)) return builder.ToString();
            else return path;
        }
    }
}
