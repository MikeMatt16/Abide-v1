using Abide.DebugXbox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XbExplorer
{
    public partial class Main : Form
    {
        private enum IconSize
        {
            Small,
            Large
        };

        private enum ItemType
        {
            AddNewXbox,
            XboxItem,
            Drive,
            Directory,
            File,
        };

        private enum VirtualLocation
        {
            /// <summary>
            /// I don't know how you got here.
            /// </summary>
            None,
            /// <summary>
            /// Root of the browsing hierarchy, shows the available Xbox consoles.
            /// </summary>
            Root,
            /// <summary>
            /// Root of the Xbox console, shows the available drives.
            /// </summary>
            Xbox,
            /// <summary>
            /// Inside a folder on the Xbox console, shows child files and folders.
            /// </summary>
            Folder
        }

        private const string XbExplorerRootLocation = "XbExplorer";

        private Xbox CurrentXbox { get; set; } = null;
        private StringCollection SavedXboxNames
        {
            get { return Properties.Settings.Default.XboxNames; }
        }
        private VirtualLocation CurrentVirtualLocation { get; set; } = VirtualLocation.None;
        private string CurrentLocation { get; set; } = XbExplorerRootLocation;
        private List<string> LocationHistory { get; } = new List<string>();
        private List<string> ForwardHistory { get; } = new List<string>();
        
        private List<ItemInformation> DirectoryListing = new List<ItemInformation>();
        private Dictionary<string, int> FileIconIndex = new Dictionary<string, int>();
        
        public Main()
        {
            InitializeComponent();
            breadcrumbImageList.Images.Add(new Icon(Properties.Resources.folder_icon, 16, 16));
            breadcrumbImageList.Images.Add(new Icon(Properties.Resources.drive_icon, 16, 16));
            breadcrumbImageList.Images.Add(new Icon(Properties.Resources.xbox_icon, 16, 16));

            //Upgrade settings
            if (Properties.Settings.Default.UpgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeRequired = false;
                Properties.Settings.Default.Save();
            }
            if (Properties.Settings.Default.XboxNames == null)
            {
                Properties.Settings.Default.XboxNames = new StringCollection();
                Properties.Settings.Default.Save();
            }

            //Create theme for menu bar
            mainMenuStrip.Renderer = new XbExplorerMenuStripRenderer();
            var renderMode = mainMenuStrip.RenderMode;
        }

        private string GetLocalPath(string fullname)
        {
            //Check
            string[] nameParts = fullname.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length < 2) throw new ArgumentException("Invalid path", nameof(fullname));

            //Get location local to Xbox
            return Path.Combine($@"{nameParts[2]}:\", Path.Combine(nameParts.Where((p, i) => i > 2).ToArray()));
        }

        private void Navigate(string location, bool store = true)
        {
            //Check
            if (string.IsNullOrEmpty(location) || location == XbExplorerRootLocation)
                XbExplorerRoot(store);

            //Check
            if (!store) CurrentLocation = location;
            
            //Get parts
            string[] parts = location.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

            //Check
            if (parts.Length > 0)
            {
                if (parts[0] != XbExplorerRootLocation) return;

                //Check
                if (parts.Length == 2) XboxRoot(parts[1], store);

                //Check
                if (parts.Length > 2)
                {
                    //Prepare
                    Xbox xbox = null;

                    //Get Xbox name
                    string xboxName = parts[1];
                    if (xboxName != CurrentXbox?.Name)
                    {
                        if (IPAddress.TryParse(xboxName, out IPAddress xboxAddress))    //Check name for IP address
                            xbox = NameAnsweringProtocol.Discover(xboxAddress, 50);
                        else
                        {
                            //Discover and try to find xbox
                            Xbox[] foundXboxes = NameAnsweringProtocol.Discover(50);
                            if (foundXboxes.Any(x => x.Name == xboxName))
                                xbox = foundXboxes.First(x => x.Name == xboxName);
                        }
                    }
                    else xbox = CurrentXbox;

                    //Check Xbox
                    if (xbox == null)
                    {
                        MessageBox.Show("Unable to open specified Xbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //Setup
                    CurrentXbox = xbox;
                    xbox.Connect();

                    //Check
                    string path = Path.Combine($@"{parts[2]}:\", Path.Combine(parts.Where((p, i) => i > 2).ToArray()));
                    if (xbox.GetDirectoryList(path, out ItemInformation[] items))
                    {
                        //Disconnect
                        xbox.Disconnect();

                        //Navigate
                        Folder(location, store);
                    }
                }
            }

            //Update
            UpdateLocation();
        }
        
        private void SetVirtualLocation(VirtualLocation location)
        {
            //Set property
            CurrentVirtualLocation = location;

            //Handle location
            switch (location)
            {
                case VirtualLocation.Root: mainListView.ContextMenuStrip = rootMenuStrip; break;
                case VirtualLocation.Xbox: mainListView.ContextMenuStrip = xboxMenuStrip; break;
                case VirtualLocation.Folder: mainListView.ContextMenuStrip = folderMenuStrip; break;
                default: mainListView.ContextMenuStrip = null; break;
            }
        }

        private void ChangeLocation(string location, bool store = true)
        {
            //Check
            if (CurrentLocation == location) return;

            //Add
            if (store) LocationHistory.Add(CurrentLocation);
            CurrentLocation = location;

            //Check
            backToolStripMenuButton.Enabled = LocationHistory.Count > 0;
        }

        private void UpdateLocation()
        {
            //Get directory name?
            string currentLocation = string.Empty;
            if (CurrentLocation.Split('\\').Length == 3)
            {
                string driveLetter = Path.GetFileName(CurrentLocation);
                currentLocation = $"{GetDriveName(driveLetter)} ({driveLetter}:)";
            }
            else currentLocation = Path.GetFileName(CurrentLocation);
            if (!string.IsNullOrEmpty(currentLocation) && currentLocation != "XbExplorer") Text = $"XbExplorer - {currentLocation}";
            else Text = "XbExplorer";

            //Enable
            takeScreenshotToolStripButton.Enabled = upToolStripMenuButton.Enabled =
                CurrentLocation.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Length > 1;
            
            //Set location
            locationTextBox.Text = CurrentLocation;
        }

        private void AddNewXbox(bool store = true)
        {
            //Prepare
            string xboxName = null;
            bool found = false;

            //Create NewXboxDialog
            using (NewXboxDialog xbxDlg = new NewXboxDialog())
            {
                if(xbxDlg.ShowDialog() == DialogResult.OK)
                {
                    xboxName = xbxDlg.XboxName;
                    found = true;
                }
            }

            //Check
            if (found)
            {
                //Add and save
                SavedXboxNames.Add(xboxName);
                Properties.Settings.Default.Save();

                //Load root
                XbExplorerRoot();
            }
        }

        private void XbExplorerRoot(bool store = true)
        {
            //Prepare
            CurrentXbox = null;

            //Change location
            if (store) { ChangeLocation("XbExplorer"); ForwardHistory.Clear(); }
                SetVirtualLocation(VirtualLocation.Root);
            UpdateLocation();

            //Enable or disable
            forwardToolStripMenuButton.Enabled = ForwardHistory.Count > 0;
            backToolStripMenuButton.Enabled = LocationHistory.Count > 0;

            //Begin
            mainListView.BeginUpdate();
            mainListView.View = View.LargeIcon;
            mainListView.LabelEdit = false;
            mainListView.AllowDrop = false;
            mainListView.MultiSelect = false;
            mainListView.ContextMenuStrip = rootMenuStrip;
            mainListView.Items.Clear();

            //Prepare columns
            mainListView.Columns.Clear();
            mainListView.Columns.Add(ipAddressHeader);
            mainListView.ListViewItemSorter = new XboxSorter();

            //Prepare groups
            mainListView.Groups.Clear();

            //Add New Xbox item
            var newXboxItem = mainListView.Items.Add("New Xbox", "Add New Xbox", 1);
            newXboxItem.Tag = ItemType.AddNewXbox;

            //Create Xbox items
            ListViewItem xboxItem = null;
            foreach (string xboxName in SavedXboxNames)
            {
                //Create item
                xboxItem = mainListView.Items.Add(xboxName, xboxName, 2);
                xboxItem.Tag = ItemType.XboxItem;
                xboxItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Tag = null });

                //Check
                if (IPAddress.TryParse(xboxName, out IPAddress address))
                {
                    NameAnsweringProtocol.DiscoverAsync(address, 100).ContinueWith(NameAnsweringProtocol_XboxDiscovered);
                }
                else
                {
                    NameAnsweringProtocol.DiscoverAsync(xboxName, 100).ContinueWith(NameAnsweringProtocol_XboxesDiscovered);
                }
            }

            //End
            mainListView.Sort();
            mainListView.EndUpdate();
        }

        private void XboxRoot(string xboxName, bool store = true)
        {
            //Prepare
            ListViewItem driveItem = null;
            DriveInformation[] driveInfos = null;

            //Begin
            mainListView.BeginUpdate();
            mainListView.View = View.Tile;
            mainListView.LabelEdit = false;
            mainListView.AllowDrop = false;
            mainListView.MultiSelect = false;
            mainListView.Items.Clear();
            
            //Prepare headers
            mainListView.Columns.Clear();
            mainListView.Columns.Add(nameHeader);
            mainListView.Columns.Add(typeHeader);
            mainListView.Columns.Add(freeSpaceHeader);
            mainListView.Columns.Add(totalSizeHeader);
            mainListView.ListViewItemSorter = new DriveSorter();

            //Get xbox
            Xbox xbox = null;
            if (IPAddress.TryParse(xboxName, out IPAddress xboxAddr))
            {
                xbox = NameAnsweringProtocol.Discover(xboxAddr, 200);
            }
            else
            {
                Xbox[] foundXboxes = NameAnsweringProtocol.Discover(xboxName, 200);
                if (foundXboxes.Length > 0) xbox = foundXboxes[0];  //Choose first xbox
            }

            if (xbox == null)
            {
                MessageBox.Show("Unable to open specified Xbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                XbExplorerRoot();
                
                //End
                mainListView.EndUpdate();
                return;
            }

            try
            {
                //Connect
                xbox.Connect();

                //Change location
                if (store) { ChangeLocation(Path.Combine("XbExplorer", xboxName)); ForwardHistory.Clear(); }
                SetVirtualLocation(VirtualLocation.Xbox);
                UpdateLocation();

                //Enable or disable
                forwardToolStripMenuButton.Enabled = ForwardHistory.Count > 0;
                backToolStripMenuButton.Enabled = LocationHistory.Count > 0;

                //Get Drive list
                bool success = xbox.GetDrives(out string[] drives);
                driveInfos = new DriveInformation[drives.Length];
                for (int i = 0; i < drives.Length; i++)
                    success |= xbox.GetDriveInformation($"{drives[i]}:\\", out driveInfos[i]);

                //Get Utitlity Drive information
                Dictionary<string, int> utilityDictionary = new Dictionary<string, int>();
                if (xbox.GetUtilityDriveInformation(out UtilityDriveInformation[] utilInfos))
                    foreach (var info in utilInfos)
                        switch (info.PartitionIndex)
                        {
                            case 0: utilityDictionary.Add("R", info.GetAnyTitleId()); break;
                            case 1: utilityDictionary.Add("Q", info.GetAnyTitleId()); break;
                            case 2: utilityDictionary.Add("P", info.GetAnyTitleId()); break;
                        }

                //Disconnect
                xbox.Disconnect();

                //Loop
                if (success)
                    for (int i = 0; i < drives.Length; i++)
                    {
                        //Create item
                        int titleId = utilityDictionary.ContainsKey(drives[i]) ? utilityDictionary[drives[i]] : 0;
                        driveItem = mainListView.Items.Add($@"{CurrentLocation}\{drives[i]}", $"{GetDriveName(drives[i], titleId)} ({drives[i]}:)", 5);
                        driveItem.Tag = ItemType.Drive;
                        driveItem.SubItems.Add(GetDriveName(drives[i])).Tag = drives[i];
                        driveItem.SubItems.Add(GetSizeString(driveInfos[i].FreeToCaller)).Tag = driveInfos[i].FreeToCaller;
                        driveItem.SubItems.Add(GetSizeString(driveInfos[i].TotalBytes)).Tag = driveInfos[i].TotalBytes;
                    }

                //Set
                CurrentXbox = xbox;
            }
            catch
            {
                MessageBox.Show("Unable to open specified Xbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                XbExplorerRoot();
            }

            //End
            mainListView.EndUpdate();
        }

        private void Folder(string fullname, bool store = true)
        {
            //Get location local to Xbox
            string path = GetLocalPath(fullname);

            //Begin
            FileIconIndex.Clear();
            mainListView.BeginUpdate();
            mainListView.View = View.Details;
            mainListView.LabelEdit = true;
            mainListView.AllowDrop = true;
            mainListView.MultiSelect = true;
            mainListView.Items.Clear();
            mainListView.RefreshImageLists();

            //Connect
            CurrentXbox.Connect();
            if (CurrentXbox.GetDirectoryList($@"{path}", out ItemInformation[] itemInfos))
            {
                //Update location
                if (store) { ChangeLocation(fullname); ForwardHistory.Clear(); }
                SetVirtualLocation(VirtualLocation.Folder);
                UpdateLocation();

                //Enable or disable
                forwardToolStripMenuButton.Enabled = ForwardHistory.Count > 0;
                backToolStripMenuButton.Enabled = LocationHistory.Count > 0;

                //Get sizes
                int sWidth = mainListView.SmallImageList.ImageSize.Width;
                int sHeight = mainListView.SmallImageList.ImageSize.Height;
                int lWidth = mainListView.LargeImageList.ImageSize.Width;
                int lHeight = mainListView.LargeImageList.ImageSize.Height;

                //Prepare headers
                mainListView.Columns.Clear();
                mainListView.Columns.Add(nameHeader);
                mainListView.Columns.Add(dateModifiedHeader);
                mainListView.Columns.Add(typeHeader);
                mainListView.Columns.Add(sizeHeader);
                mainListView.ListViewItemSorter = new FileSorter();
                
                //Get Directory listing
                ListViewItem listItem = null;
                DirectoryListing = new List<ItemInformation>(itemInfos);
                foreach (ItemInformation item in itemInfos)
                {
                    //Check
                    if (item.Attributes.HasFlag(FileAttributes.Hidden)) continue;

                    //Create item
                    if ((item.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        listItem = mainListView.Items.Add($"{fullname}\\{item.Name}", item.Name, 0);
                        listItem.Tag = ItemType.Directory;
                        listItem.SubItems.Add(item.Modified.ToString("g")).Tag = item.Modified;
                        listItem.SubItems.Add("File Folder").Tag = "File Folder";
                    }
                    else
                    {
                        //Get info for extension
                        string extension = Path.GetExtension(item.Name);
                        var extInfo = Registry.GetInfo(extension);

                        //Get icon for file
                        int imageIndex = 8;
                        if (extension == ".exe")
                        {
                            imageIndex = 7;
                        }
                        else if (extension == ".xbe")
                        {
                            imageIndex = 6;
                            extInfo = new Registry.ExtensionInfo()
                            {
                                ProgId = "Xbox.xbe",
                                Type = "Xbox Executable",
                            };
                        }
                        else
                        {
                            //Add new extension
                            if (!FileIconIndex.ContainsKey(extension))
                            {
                                //Add to dictionary...
                                FileIconIndex.Add(extension, mainListView.SmallImageList.Images.Count);
                                using (var icon = GetDefaultIcon(extInfo.DefaultIcon, sWidth, sHeight))
                                    mainListView.SmallImageList.Images.Add(new Icon(icon, sWidth, sHeight));
                                using (var icon = GetDefaultIcon(extInfo.DefaultIcon, lWidth, lHeight))
                                    mainListView.LargeImageList.Images.Add(new Icon(icon, lWidth, lHeight));
                            }

                            //Get index
                            imageIndex = FileIconIndex[extension];
                        }

                        //Add item
                        listItem = mainListView.Items.Add($"{fullname}\\{item.Name}", item.Name, imageIndex);
                        listItem.Tag = ItemType.File;
                        listItem.SubItems.Add(item.Modified.ToString("g")).Tag = item.Modified;
                        listItem.SubItems.Add(GetFileType(extension, extInfo.Type)).Tag = extension;
                        listItem.SubItems.Add(GetSizeStringKb(item.Size)).Tag = item.Size;
                    }
                }
            }

            //Disconnect
            CurrentXbox.Disconnect();

            //End
            mainListView.Sort();
            mainListView.EndUpdate();
        }

        private Icon GetDefaultIcon(string defaultIcon)
        {
            //Prepare default
            string defaultPath = $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll")},0";
            if (string.IsNullOrEmpty(defaultIcon)) defaultIcon = defaultPath;
            Icon icon = null;

            //Load data from default icon string
            int commaIndex = defaultIcon.LastIndexOf(',');
            string path = commaIndex >= 0 ? defaultIcon.Substring(0, commaIndex) : defaultIcon;
            int index = commaIndex >= 0 ? int.Parse(defaultIcon.Substring(defaultIcon.LastIndexOf(',') + 1)) : 0;

            //Check
            IntPtr hIcon = Win32.ExtractIcon(Handle, path.Trim('\"'), (uint)index);
            if (index != -1)
                icon = Icon.FromHandle(hIcon);  //Load

            //Return
            return icon;
        }

        private Icon GetDefaultIcon(string defaultIcon, int width, int height)
        {
            //Prepare default
            string defaultPath = $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll")},0";
            if (string.IsNullOrEmpty(defaultIcon)) defaultIcon = defaultPath;
            Icon icon = null;

            //Load data from default icon string
            int commaIndex = defaultIcon.LastIndexOf(',');
            string path = commaIndex >= 0 ? defaultIcon.Substring(0, commaIndex) : defaultIcon;
            int index = commaIndex >= 0 ? int.Parse(defaultIcon.Substring(defaultIcon.LastIndexOf(',') + 1)) : 0;

            //Load
            uint iconCount = Win32.PrivateExtractIcons(path.Trim('\"'), index, width, height, out IntPtr hIcon, out IntPtr iconId, 1, 0);
            if (iconId != IntPtr.Zero && hIcon != IntPtr.Zero)
                icon = Icon.FromHandle(hIcon);

            //Return
            return icon;
        }

        private Icon GetDefaultIcon(string defaultIcon, IconSize size)
        {
            //Prepare default
            string defaultPath = $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll")},0";
            if (string.IsNullOrEmpty(defaultIcon)) defaultIcon = defaultPath;
            Icon icon = null;

            //Load data from default icon string
            int commaIndex = defaultIcon.LastIndexOf(',');
            string path = commaIndex >= 0 ? defaultIcon.Substring(0, commaIndex) : defaultIcon;
            int index = commaIndex >= 0 ? int.Parse(defaultIcon.Substring(defaultIcon.LastIndexOf(',') + 1)) : 0;
            
            //Load
            int iconCount = Win32.ExtractIconEx(path.Trim('\"'), index, out IntPtr hIconLarge, out IntPtr hIconSmall, 1);

            //Check
            switch (size)
            {
                case IconSize.Small:
                    icon = Icon.FromHandle(hIconSmall);
                    Win32.DestroyIcon(hIconLarge);
                    break;
                case IconSize.Large:
                    icon = Icon.FromHandle(hIconLarge);
                    Win32.DestroyIcon(hIconSmall);
                    break;
            }

            //Return
            return icon;
        }

        private string GetFileType(string extension, string typeDescription = null)
        {
            if (!string.IsNullOrEmpty(typeDescription)) return typeDescription;
            if (string.IsNullOrEmpty(extension)) return "File";
            else return $"{extension.Substring(1).ToUpper()} File";
        }
        
        private string GetSizeString(long size)
        {
            StringBuilder sb = new StringBuilder(128);
            Win32.StrFormatByteSize(size, sb, sb.Capacity);
            return sb.ToString();
        }

        private string GetSizeStringKb(long size)
        {
            StringBuilder sb = new StringBuilder(128);
            Win32.StrFormatKBSize(size, sb, sb.Capacity);
            return sb.ToString();
        }

        private string GetDriveName(string drive, int titleId = 0)
        {
            //Handle drive
            switch (drive[0])
            {
                case 'A': return "DVD-ROM Drive";
                case 'B': return "Volume";
                case 'C': return "Main Volume";
                case 'D': return "Active Title Media";
                case 'E': return "Game Development";
                case 'F': return "Memory Unit 1A";
                case 'G': return "Memory Unit 1B";
                case 'H': return "Memory Unit 2A";
                case 'I': return "Memory Unit 2B";
                case 'J': return "Memory Unit 3A";
                case 'K': return "Memory Unit 3B";
                case 'L': return "Memory Unit 4A";
                case 'M': return "Memory Unit 4B";
                case 'N': return "Secondary Active Utility Drive";
                case 'S': return "Persistent Data - All Titles";
                case 'T': return "Persistent Data - Active Title";
                case 'U': return "Saved Games - Active Title";
                case 'V': return "Saved Games - All Titles";
                case 'W': return "Persistant Data - Alternate Title";
                case 'X': return "Saved Games - Alternate Title";
                case 'Y': return "Xbox Dashboard Volume";
                case 'Z': return "Active Utility Drive";
                case 'P':
                case 'Q':
                case 'R': return $"Utility Drive for Title {titleId:X8}";
                default: return string.Empty;
            }
        }

        private void Execute(string fullname)
        {
            //Get location local to Xbox
            string fileName = GetLocalPath(fullname);

            //Connect
            CurrentXbox.Connect();

            //Get extension
            string ext = Path.GetExtension(fileName);
            if (ext == ".xbe") CurrentXbox.MagicBoot(fileName);
            else
            {
                //Get temp filename
                string tempFileName = Path.Combine(Path.GetTempPath(), Path.GetFileName(fileName));

                //Download
                using (FileProgressDialog progressDialog = new FileProgressDialog())
                {
                    //Setup
                    progressDialog.Text = "Downloading...";
                    progressDialog.SourceFileName = fileName;
                    progressDialog.TargetFileName = tempFileName;
                    CurrentXbox.DownloadFileCompleted += progressDialog.DownloadProgressCompleted;
                    CurrentXbox.DownloadProgressChanged += progressDialog.DownloadProgressChanged;

                    //Download
                    try
                    {
                        CurrentXbox.GetFileAsync(fileName, tempFileName);

                        //Show
                        progressDialog.ShowDialog();

                        //Check
                        if (progressDialog.Completed) Process.Start(tempFileName);
                    }
                    catch(Exception ex) { throw ex; }
                }
            }

            //Disconnect
            CurrentXbox.Disconnect();
        }

        private bool RecursiveDelete(string path)
        {
            //Prepare
            List<string> filesToDelete = new List<string>();
            List<string> directoriesToDelete = new List<string>();
            bool success = true;

            //Loop
            CurrentXbox.GetDirectoryList(path, out ItemInformation[] itemInfos);
            foreach (var item in itemInfos)
                if (item.Attributes.HasFlag(FileAttributes.Directory))
                    directoriesToDelete.Add(Path.Combine(path, item.Name));
                else filesToDelete.Add(Path.Combine(path, item.Name));

            //Loop through files
            foreach (string itemPath in filesToDelete)
                success &= CurrentXbox.Delete(itemPath);

            //Loop through folders
            foreach (string directory in directoriesToDelete)
                success &= RecursiveDelete(directory);

            //Remove folder
            return success & CurrentXbox.Delete(path, true);
        }
        
        private void Main_Load(object sender, EventArgs e)
        {
            //Load root
            XbExplorerRoot();
        }

        private void NameAnsweringProtocol_XboxDiscovered(Task<Xbox> task)
        {
            Invoke(new Action(() =>
            {
                foreach (ListViewItem xboxItem in mainListView.Items)
                {
                    if (xboxItem.Tag is ItemType type && type == ItemType.XboxItem)
                    {
                        Xbox xbox = task.Result;
                        if (IPAddress.TryParse(xboxItem.Text, out IPAddress ipAddress))
                        {
                            if (xbox.RemoteEndPoint.Address.Equals(ipAddress))
                            {
                                goto found;
                            }
                        }
                        else
                        {
                            if (xboxItem.Text == xbox.Name)
                            {
                                goto found;
                            }
                        }

                        continue;

                    found:
                        xboxItem.ImageIndex = 3;
                        xboxItem.Text = xbox.Name;
                        xboxItem.SubItems[1] = new ListViewItem.ListViewSubItem(xboxItem, xbox.RemoteEndPoint.Address.ToString())
                        {
                            Tag = xbox.RemoteEndPoint.Address
                        };
                    }
                }
            }));
        }

        private void NameAnsweringProtocol_XboxesDiscovered(Task<Xbox[]> task)
        {
            Invoke(new Action(() =>
            {
                foreach (ListViewItem xboxItem in mainListView.Items)
                {
                    var xboxes = task.Result;
                    if (xboxItem.Tag is ItemType type && type == ItemType.XboxItem)
                    {
                        //Check
                        Xbox xbox;
                        if (IPAddress.TryParse(xboxItem.Text, out IPAddress ipAddress))
                        {
                            xbox = xboxes.FirstOrDefault(x => x.RemoteEndPoint.Address.Equals(ipAddress));
                            if (xbox != null)
                            {
                                goto found;
                            }
                        }
                        else
                        {
                            xbox = xboxes.FirstOrDefault(x => x.Name == xboxItem.Text);
                            if (xbox != null)
                            {
                                goto found;
                            }
                        }

                        continue;

                    found:
                        //Setup item
                        xboxItem.ImageIndex = 3;
                        xboxItem.Text = xbox.Name;
                        xboxItem.SubItems[1] = new ListViewItem.ListViewSubItem(xboxItem, xbox.RemoteEndPoint.Address.ToString())
                        {
                            Tag = xbox.RemoteEndPoint.Address
                        };
                    }
                }
            }));
        }

        private void formatDriveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check
            if (mainListView.SelectedItems.Count > 0 && mainListView.SelectedItems[0] is ListViewItem selectedItem)
            {
                //Ask
                if (MessageBox.Show("Formatting this drive will destroy all data. " +
                    "Are you sure you want to continue?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //Connect
                    CurrentXbox.Connect();

                    //Format
                    CurrentXbox.FormatDrive(selectedItem.Name);

                    //Disconnect
                    CurrentXbox.Disconnect();
                }
            }
        }

        private void openXboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check
            if (mainListView.SelectedItems.Count > 0 && mainListView.SelectedItems[0] is ListViewItem selectedItem)
                mainListView_ItemActivate(sender, e);
        }

        private void openDriveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check
            if (mainListView.SelectedItems.Count > 0 && mainListView.SelectedItems[0] is ListViewItem selectedItem && selectedItem.Tag is ItemType.Drive)
                mainListView_ItemActivate(sender, e);
        }

        private void openItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check
            if (mainListView.SelectedItems.Count > 0 && mainListView.SelectedItems[0] is ListViewItem selectedItem &&
                (selectedItem.Tag is ItemType.File || selectedItem.Tag is ItemType.Directory))
                mainListView_ItemActivate(sender, e);
        }

        private void debuggingEnabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check
            if (mainListView.SelectedItems.Count > 0 && mainListView.SelectedItems[0] is ListViewItem selectedItem && selectedItem.Tag is ItemType.File)
            {
                //Get location local to Xbox
                string fileName = GetLocalPath(selectedItem.Name);

                //Boot
                CurrentXbox.Connect();
                if (!CurrentXbox.MagicBoot(fileName, true)) CurrentXbox.Disconnect();
            }
        }

        private void debuggingDisabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check
            if (mainListView.SelectedItems.Count > 0 && mainListView.SelectedItems[0] is ListViewItem selectedItem && selectedItem.Tag is ItemType.File)
            {
                //Get location local to Xbox
                string fileName = GetLocalPath(selectedItem.Name);

                //Boot
                CurrentXbox.Connect();
                if (!CurrentXbox.MagicBoot(fileName, false)) CurrentXbox.Disconnect();
                else XbExplorerRoot();
            }
        }

        private void deleteXboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check
            if (mainListView.SelectedItems.Count > 0 && mainListView.SelectedItems[0] is ListViewItem selectedItem)
            {
                //Remove
                SavedXboxNames.Remove(selectedItem.Name);
                Properties.Settings.Default.Save();

                //Reload
                XbExplorerRoot();
            }
        }

        private void warmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get selected item
            if (mainListView.SelectedItems.Count > 0 && mainListView.SelectedItems[0] is ListViewItem selectedItem &&
                selectedItem.Tag is ItemType type && type == ItemType.XboxItem)
            {
                //Connect
                Xbox xbox = null;
                Xbox[] foundXboxes = null;
                if (IPAddress.TryParse(selectedItem.Name, out IPAddress address)) foundXboxes = new Xbox[] { NameAnsweringProtocol.Discover(address, 100) };
                else foundXboxes = NameAnsweringProtocol.Discover(selectedItem.Name, 100);
                if (foundXboxes.Length > 0) xbox = foundXboxes[0];  //Choose first xbox
                try
                {
                    //Connect
                    xbox.Connect();

                    //Reboot
                    xbox.Reboot(BootType.Warm);

                    //Disconect
                    xbox.Disconnect(false);
                }
                catch { }
            }
        }

        private void warmToTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get selected item
            if (mainListView.SelectedItems.Count > 0 && mainListView.SelectedItems[0] is ListViewItem selectedItem &&
                selectedItem.Tag is ItemType type && type == ItemType.XboxItem)
            {
                //Connect
                Xbox xbox = null;
                Xbox[] foundXboxes = null;
                if (IPAddress.TryParse(selectedItem.Name, out IPAddress address)) foundXboxes = new Xbox[] { NameAnsweringProtocol.Discover(address, 100) };
                else foundXboxes = NameAnsweringProtocol.Discover(selectedItem.Name, 100);

                if (foundXboxes.Length > 0) xbox = foundXboxes[0];  //Choose first xbox
                try
                {
                    //Connect
                    xbox.Connect();

                    //Get xbe info
                    if (xbox.GetXbeInfo(out XboxExecutableInfo xbeInfo))
                        xbox.MagicBoot(xbeInfo.FileName, true);
                    else { xbox.Reboot(BootType.Warm); }

                    //Disconect
                    xbox.Disconnect(false);
                }
                catch { }
            }
        }

        private void coldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get selected item
            if (mainListView.SelectedItems.Count > 0 && mainListView.SelectedItems[0] is ListViewItem selectedItem &&
                selectedItem.Tag is ItemType type && type == ItemType.XboxItem)
            {
                //Connect
                if (IPAddress.TryParse(selectedItem.Name, out IPAddress address))
                {
                    var xbox = NameAnsweringProtocol.Discover(address, 10);
                    if (xbox != null)
                    {
                        try
                        {
                            xbox.Connect();
                            xbox.Reboot(BootType.Cold);
                            xbox.Disconnect(false);
                        }
                        catch { }
                    }
                }
            }
        }

        private void screenshotXboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Xbox xbox = CurrentXbox;

            if (CurrentXbox == null && mainListView.SelectedItems.Count > 0 && mainListView.SelectedItems[0] is ListViewItem selectedItem &&
                    selectedItem.Tag is ItemType type && type == ItemType.XboxItem)
            {
                Xbox[] foundXboxes = null;
                if (IPAddress.TryParse(selectedItem.Name, out IPAddress address)) foundXboxes = new Xbox[] { NameAnsweringProtocol.Discover(address, 100) };
                else foundXboxes = NameAnsweringProtocol.Discover(selectedItem.Name, 100);

                if (foundXboxes.Length > 0) xbox = foundXboxes[0];  //Choose first xbox
            }

            if (xbox != null)
            {
                xbox.Connect();

                if (xbox.Screenshot(out Bitmap screenshot) && screenshot != null)
                {
                    if (!Directory.Exists(Properties.Settings.Default.ScreenshotDirectory))
                        Directory.CreateDirectory(Properties.Settings.Default.ScreenshotDirectory);

                    var now = DateTime.Now;
                    string filepath = Path.Combine(Properties.Settings.Default.ScreenshotDirectory, $"Screenshot-{now.Month:d2}{now.Day:d2}{now.Year}{now.Hour}{now.Minute}{now.Second}.png");
                    screenshot.Save(filepath);
                    screenshot.Dispose();

                    Process.Start(filepath);
                }

                xbox.Disconnect(false);
            }
        }

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check
            if(CurrentVirtualLocation == VirtualLocation.Folder)
            {
                //Connect
                CurrentXbox.Connect();

                //Get file list
                string localPath = GetLocalPath(CurrentLocation);
                CurrentXbox.GetDirectoryList(localPath, out ItemInformation[] list);

                //Get Name
                string newFolderName = "New Folder";
                int folderIndex = 1;
                while (list.Any(i => i.Attributes.HasFlag(FileAttributes.Directory) && i.Name == newFolderName))
                {
                    folderIndex++;
                    newFolderName = $"New Folder ({folderIndex})";
                }

                //Make folder
                string path = Path.Combine(localPath, newFolderName);
                if (CurrentXbox.MakeDirectory(path))
                {
                    //Update list
                    CurrentXbox.GetDirectoryList(localPath, out list);
                    var item = list.First(i => i.Name == newFolderName);

                    //Setup listing
                    DirectoryListing = new List<ItemInformation>(list);

                    //Create List view item
                    ListViewItem listItem = mainListView.Items.Add($"{Path.Combine(CurrentLocation, newFolderName)}", newFolderName, 0);
                    listItem.Tag = ItemType.Directory;
                    listItem.SubItems.Add(item.Modified.ToString("g"));
                    listItem.SubItems.Add("Folder");

                    //Edit
                    listItem.BeginEdit();
                }

                //Disconnect
                CurrentXbox.Disconnect();
            }
        }

        private void renameItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get selected item
            if (mainListView.SelectedItems.Count > 0 && mainListView.SelectedItems[0] is ListViewItem selectedItem &&
                selectedItem.Tag is ItemType type && (type == ItemType.File || type == ItemType.Directory))
                selectedItem.BeginEdit();  //Rename
        }

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            List<string> filesToDelete = new List<string>();
            List<string> directoriesToDelete = new List<string>();

            //Get selected item
            foreach (ListViewItem selectedItem in mainListView.SelectedItems)
                if (selectedItem.Tag is ItemType type)
                    if (type == ItemType.File) filesToDelete.Add(selectedItem.Name);
                    else if (type == ItemType.Directory) directoriesToDelete.Add(selectedItem.Name);

            //Check
            bool delete = true;
            if ((filesToDelete.Count + directoriesToDelete.Count) > 1)
                delete = MessageBox.Show($"Are you sure you want to delete {filesToDelete.Count + directoriesToDelete.Count} items?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;

            //Check
            if (delete)
            {
                //Connect
                CurrentXbox.Connect();

                //Loop through files
                foreach (string file in filesToDelete)
                {
                    string filename = GetLocalPath(file);
                    if (CurrentXbox.Delete(filename))
                        mainListView.SelectedItems[file].Remove();
                }

                //Loop through folders
                foreach (string directory in directoriesToDelete)
                {
                    string dirname = GetLocalPath(directory);
                    if (RecursiveDelete(dirname))
                        mainListView.SelectedItems[directory].Remove();
                }

                //Disconnect
                CurrentXbox.Disconnect();
            }
        }

        private void copyItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare
            List<ItemInformation> infos = new List<ItemInformation>();

            //Loop
            foreach (ListViewItem item in mainListView.SelectedItems)
                if (DirectoryListing.Any(i => Path.Combine(CurrentLocation, i.Name) == item.Name))
                    infos.Add(DirectoryListing.First(i => Path.Combine(CurrentLocation, i.Name) == item.Name));

            //Set data
            RemoteItemObject remoteObj = new RemoteItemObject(CurrentXbox, infos.ToArray());
            remoteObj.SetData(RemoteItemObject.CFSTR_FILEDESCRIPTORW, null);
            remoteObj.SetData(RemoteItemObject.CFSTR_FILECONTENTS, null);
            remoteObj.SetData(RemoteItemObject.CFSTR_PERFORMEDDROPEFFECT, null);
            Clipboard.SetDataObject(remoteObj);
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.CreateAndShow();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SettingsDialog())
                dialog.ShowDialog();
        }
        
        private void homeToolStripButton_Click(object sender, EventArgs e)
        {
            //Goto Root
            XbExplorerRoot();
        }

        private void mainListView_ItemActivate(object sender, EventArgs e)
        {
            if (mainListView.SelectedItems.Count == 0) return;
            foreach (ListViewItem item in mainListView.SelectedItems)
                if (item.Tag is ItemType itemType)
                    switch (itemType)
                    {
                        case ItemType.AddNewXbox:
                            AddNewXbox(); break;
                        case ItemType.XboxItem:
                            XboxRoot(item.Name); break;
                        case ItemType.Drive:
                        case ItemType.Directory:
                            Folder(item.Name); break;
                        case ItemType.File:
                            Execute(item.Name); break;
                        default:
                            break;
                    }
        }

        private void mainListView_DragEnter(object sender, DragEventArgs e)
        {
            //Check
            if (CurrentVirtualLocation != VirtualLocation.Folder) e.Effect = DragDropEffects.None;
            else if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void mainListView_DragDrop(object sender, DragEventArgs e)
        {
            //Get string array containing the dropped files and/or folders
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            string localDirectory = GetLocalPath(CurrentLocation);

            using (FileUploadDialog uploadDialog = new FileUploadDialog(CurrentXbox, localDirectory, items.Select(i => new LocalObject(i)).ToArray()))
                uploadDialog.ShowDialog();

            //Reload
            Folder(CurrentLocation, false);
        }

        private void mainListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //Check
            if (CurrentVirtualLocation == VirtualLocation.Folder && e.Item is ListViewItem item)
            {
                ItemInformation currentItem = DirectoryListing.First(i => Path.Combine(CurrentLocation, i.Name) == item.Name);
                RemoteItemObject remoteObj = new RemoteItemObject(CurrentXbox, currentItem);
                remoteObj.SetData(RemoteItemObject.CFSTR_FILEDESCRIPTORW, null);
                remoteObj.SetData(RemoteItemObject.CFSTR_FILECONTENTS, null);
                remoteObj.SetData(RemoteItemObject.CFSTR_PERFORMEDDROPEFFECT, null);
                DoDragDrop(remoteObj, DragDropEffects.Copy);
            }
        }

        private void mainListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //Disable all
            openXboxToolStripMenuItem.Enabled = false;
            rebootXboxToolStripMenuItem.Enabled = false;
            deleteXboxToolStripMenuItem.Enabled = false;
            openDriveToolStripMenuItem.Enabled = false;
            formatDriveToolStripMenuItem.Enabled = false;
            openItemToolStripMenuItem.Enabled = false;
            copyItemToolStripMenuItem.Enabled = false;
            renameItemToolStripMenuItem.Enabled = false;
            deleteItemToolStripMenuItem.Enabled = false;
            takeScreenshotToolStripButton.Enabled = false;
            bootToolStripMenuItem.Visible = false;

            //Check
            if (e.IsSelected && e.Item.Tag is ItemType type)
            {
                switch (type)
                {
                    case ItemType.XboxItem:
                        openXboxToolStripMenuItem.Enabled = e.Item.ImageIndex == 3;
                        screenshotXboxToolStripMenuItem.Enabled = e.Item.ImageIndex == 3;
                        rebootXboxToolStripMenuItem.Enabled = e.Item.ImageIndex == 3;
                        deleteXboxToolStripMenuItem.Enabled = true;
                        takeScreenshotToolStripButton.Enabled = true;
                        break;
                    case ItemType.Drive:
                        openDriveToolStripMenuItem.Enabled = true;
                        formatDriveToolStripMenuItem.Enabled = true;
                        break;
                    case ItemType.File:
                        string ext = Path.GetExtension(e.Item.Name);
                        bootToolStripMenuItem.Visible = (Path.GetExtension(e.Item.Name) == ".xbe");
                        goto case ItemType.Directory;
                    case ItemType.Directory:
                        openItemToolStripMenuItem.Enabled = true;
                        copyItemToolStripMenuItem.Enabled = true;
                        pasteItemToolStripMenuItem.Enabled = true;
                        newFolderToolStripMenuItem.Enabled = true;
                        renameItemToolStripMenuItem.Enabled = true;
                        deleteItemToolStripMenuItem.Enabled = true;
                        break;
                }
            }

            if (CurrentVirtualLocation == VirtualLocation.Xbox)
            {
                takeScreenshotToolStripButton.Enabled = true;
            }

            //Check if we're in a folder
            if (CurrentVirtualLocation == VirtualLocation.Folder)
            {
                //Enable Paste and New Folder buttons
                pasteItemToolStripMenuItem.Enabled = true;
                newFolderToolStripMenuItem.Enabled = true;
                takeScreenshotToolStripButton.Enabled = true;
            }
        }
        
        private void mainListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            //Check
            if (string.IsNullOrEmpty(e.Label))
            {
                e.CancelEdit = true;
                return;
            }

            //Connect
            CurrentXbox.Connect();

            //Check
            if (sender is ListView listView && listView.Items[e.Item].Tag is ItemType type && (type == ItemType.Directory || type == ItemType.File))
            {
                //Get path of file or folder and get it's directory.
                string path = listView.Items[e.Item].Name;
                string directory = Path.GetDirectoryName(path);
                string target = Path.Combine(directory, e.Label);
                string name = Path.GetFileName(target);

                //Get local names
                string localPath = GetLocalPath(path);
                string localTarget = GetLocalPath(target);

                //Rename
                if (!CurrentXbox.Move(localPath, localTarget))
                    e.CancelEdit = true;
                else if(type == ItemType.Directory)
                {
                    //Update item
                    listView.Items[e.Item].Name = target;
                    listView.Items[e.Item].Text = name;
                }
                else if(type == ItemType.File)
                {
                    //Get info for extension
                    string extension = Path.GetExtension(target);
                    var extInfo = Registry.GetInfo(extension);

                    //Get sizes
                    int sWidth = mainListView.SmallImageList.ImageSize.Width;
                    int sHeight = mainListView.SmallImageList.ImageSize.Height;
                    int lWidth = mainListView.LargeImageList.ImageSize.Width;
                    int lHeight = mainListView.LargeImageList.ImageSize.Height;

                    //Get icon for file
                    int imageIndex = 6;

                    if (extension != ".xbe")
                    {
                        //Add new extension
                        if (!FileIconIndex.ContainsKey(extension))
                        {
                            //Add to dictionary...
                            FileIconIndex.Add(extension, mainListView.SmallImageList.Images.Count);
                            using (var icon = GetDefaultIcon(extInfo.DefaultIcon, sWidth, sHeight))
                                mainListView.SmallImageList.Images.Add(new Icon(icon, sWidth, sHeight));
                            using (var icon = GetDefaultIcon(extInfo.DefaultIcon, lWidth, lHeight))
                                mainListView.LargeImageList.Images.Add(new Icon(icon, lWidth, lHeight));
                        }

                        //Get index
                        imageIndex = FileIconIndex[extension];
                    }
                    else
                    {
                        extInfo = new Registry.ExtensionInfo()
                        {
                            ProgId = "Xbox.xbe",
                            Type = "Xbox Executable",
                        };
                    }

                    //Get name
                    string displayName = name;
                    if (!string.IsNullOrEmpty(extInfo.ProgId))
                        displayName = displayName.Replace(extension, string.Empty);

                    //Update item
                    listView.Items[e.Item].Name = target;
                    listView.Items[e.Item].Text = displayName;
                    listView.Items[e.Item].ImageIndex = imageIndex;
                    listView.Items[e.Item].SubItems[2].Text = GetFileType(extension, extInfo.Type);
                }
            }

            //Disconnect
            CurrentXbox.Disconnect();

            //Sort
            mainListView.Sort();
        }

        private void mainListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //Get column
            ColumnHeader header = null;
            if (e.Column >= 0 && e.Column < mainListView.Columns.Count)
                header = mainListView.Columns[e.Column];
            else return;

            //Handle
            switch (CurrentVirtualLocation)
            {
                case VirtualLocation.Root:
                    XboxSorter xboxSorter = null;
                    if (mainListView.ListViewItemSorter is XboxSorter)
                        xboxSorter = (XboxSorter)mainListView.ListViewItemSorter;
                    else mainListView.ListViewItemSorter = xboxSorter = new XboxSorter();
                    switch (e.Column)
                    {
                        case 0:
                            if (xboxSorter.SortBy == XboxSorter.SortingType.Name) xboxSorter.ToggleDirection();
                            else { xboxSorter.SortBy = XboxSorter.SortingType.Name; xboxSorter.Direction = Direction.Ascending; }
                            break;
                        case 1:
                            if (xboxSorter.SortBy == XboxSorter.SortingType.IPAddress) xboxSorter.ToggleDirection();
                            else { xboxSorter.SortBy = XboxSorter.SortingType.IPAddress; xboxSorter.Direction = Direction.Ascending; }
                            break;
                    }
                    break;
                case VirtualLocation.Xbox:
                    DriveSorter driveSorter = null;
                    if (mainListView.ListViewItemSorter is DriveSorter)
                        driveSorter = (DriveSorter)mainListView.ListViewItemSorter;
                    else mainListView.ListViewItemSorter = driveSorter = new DriveSorter();
                    switch (e.Column)
                    {
                        case 0:
                            if (driveSorter.SortBy == DriveSorter.SortingType.Name) driveSorter.ToggleDirection();
                            else { driveSorter.SortBy = DriveSorter.SortingType.Name; driveSorter.Direction = Direction.Ascending; }
                            break;
                        case 1:
                            if (driveSorter.SortBy == DriveSorter.SortingType.Type) driveSorter.ToggleDirection();
                            else { driveSorter.SortBy = DriveSorter.SortingType.Type; driveSorter.Direction = Direction.Ascending; }
                            break;
                        case 2:
                            if (driveSorter.SortBy == DriveSorter.SortingType.FreeSpace) driveSorter.ToggleDirection();
                            else { driveSorter.SortBy = DriveSorter.SortingType.FreeSpace; driveSorter.Direction = Direction.Ascending; }
                            break;
                        case 3:
                            if (driveSorter.SortBy == DriveSorter.SortingType.TotalSize) driveSorter.ToggleDirection();
                            else { driveSorter.SortBy = DriveSorter.SortingType.TotalSize; driveSorter.Direction = Direction.Ascending; }
                            break;
                    }
                    break;
                case VirtualLocation.Folder:
                    FileSorter fileSorter = null;
                    if (mainListView.ListViewItemSorter is FileSorter)
                        fileSorter = (FileSorter)mainListView.ListViewItemSorter;
                    else mainListView.ListViewItemSorter = fileSorter = new FileSorter();
                    switch (e.Column)
                    {
                        case 0:
                            if (fileSorter.SortBy == FileSorter.SortingType.Name) fileSorter.ToggleDirection();
                            else { fileSorter.SortBy = FileSorter.SortingType.Name; fileSorter.Direction = Direction.Ascending; }
                            break;
                        case 1:
                            if (fileSorter.SortBy == FileSorter.SortingType.DateModified) fileSorter.ToggleDirection();
                            else { fileSorter.SortBy = FileSorter.SortingType.DateModified; fileSorter.Direction = Direction.Ascending; }
                            break;
                        case 2:
                            if (fileSorter.SortBy == FileSorter.SortingType.Type) fileSorter.ToggleDirection();
                            else { fileSorter.SortBy = FileSorter.SortingType.Type; fileSorter.Direction = Direction.Ascending; }
                            break;
                        case 3:
                            if (fileSorter.SortBy == FileSorter.SortingType.Size) fileSorter.ToggleDirection();
                            else { fileSorter.SortBy = FileSorter.SortingType.Size; fileSorter.Direction = Direction.Ascending; }
                            break;
                    }
                    break;
            }

            //Sort
            mainListView.Sort();
        }

        private void locationTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //Check
            if (e.KeyCode == Keys.Return)
            {
                //Navigate
                Navigate(locationTextBox.Text);
                locationTextBox.Text = CurrentLocation; //Set current location
            }
        }

        private void backToolStripMenuButton_Click(object sender, EventArgs e)
        {
            //Get last location
            string last = LocationHistory.LastOrDefault();
            if (!string.IsNullOrEmpty(last))    //Check last location
            {
                //Add current to forward history
                ForwardHistory.Add(CurrentLocation);

                //Remove from navigation history
                LocationHistory.Remove(last);
                Navigate(last, false);
            }

            //Enable or disable
            forwardToolStripMenuButton.Enabled = ForwardHistory.Count > 0;
            backToolStripMenuButton.Enabled = LocationHistory.Count > 0;
        }

        private void forwardToolStripMenuButton_Click(object sender, EventArgs e)
        {
            //Get last location
            string last = ForwardHistory.LastOrDefault();
            if (!string.IsNullOrEmpty(last))    //Check last location
            {
                //Add to current to navigation history
                LocationHistory.Add(CurrentLocation);

                //Remove from forward history
                ForwardHistory.Remove(last);
                Navigate(last, false);
            }

            //Enable or disable
            forwardToolStripMenuButton.Enabled = ForwardHistory.Count > 0;
            backToolStripMenuButton.Enabled = LocationHistory.Count > 0;
        }

        private void upToolStripMenuButton_Click(object sender, EventArgs e)
        {
            //Get parent
            string parent = Path.GetDirectoryName(CurrentLocation);

            //Check
            if (!string.IsNullOrEmpty(parent))
                Navigate(parent);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (Program.MainWindow == this && Program.Windows.Count > 0)
                {
                    if (MessageBox.Show("Closing this window will also close all other XbExplorer windows. " +
                        "Are you sure you want to exit?", "Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        e.Cancel = true;
                    }
                }
                else if (Program.Windows.Contains(this))
                {
                    Program.Windows.Remove(this);
                }
            }
        }

        private enum Direction
        {
            Ascending,
            Descending
        }

        private abstract class ItemSorter
        {
            public Direction Direction { get; set; } = Direction.Ascending;

            public void ToggleDirection()
            {
                switch (Direction)
                {
                    case Direction.Ascending: Direction = Direction.Descending; break;
                    case Direction.Descending: Direction = Direction.Ascending; break;
                }
            }

            protected int CompareIPAddress(IPAddress ip1, IPAddress ip2)
            {
                return CompareText((ip1 ?? IPAddress.Any).ToString(), (ip2 ?? IPAddress.Any).ToString());
            }
            protected int CompareSize(long size1, long size2)
            {
                return size1.CompareTo(size2);
            }
            protected int CompareDateTime(DateTime d1, DateTime d2)
            {
                return d1.CompareTo(d2);
            }
            protected int CompareText(string str1, string str2)
            {
                return str1.CompareTo(str2);
            }
        }

        private sealed class XboxSorter : ItemSorter, IComparer
        {
            public SortingType SortBy { get; set; } = SortingType.IPAddress;

            public int Compare(object x, object y)
            {
                int result = 0;
                if (x is ListViewItem item1 && y is ListViewItem item2)
                    result = Compare(item1, item2);
                return Direction == Direction.Ascending ? result : result * -1;
            }

            private int Compare(ListViewItem item1, ListViewItem item2)
            {
                if (item1.Tag == null || item2.Tag == null) return 0;

                int result = 0;
                if (SortBy == SortingType.Name)
                {
                    if (item1.Tag is ItemType.AddNewXbox && item2.Tag is ItemType.XboxItem)
                        result = -1;
                    else if (item1.Tag is ItemType.XboxItem && item2.Tag is ItemType.AddNewXbox)
                        result = 1;
                    else result = CompareText(item1.Text, item2.Text);
                }
                else if (SortBy == SortingType.IPAddress)
                {
                    if (item1.Tag is ItemType.AddNewXbox && item2.Tag is ItemType.XboxItem)
                        result = -1;
                    else if (item1.Tag is ItemType.XboxItem && item2.Tag is ItemType.AddNewXbox)
                        result = 1;
                    else if (item1.SubItems[1].Tag is IPAddress && item2.SubItems[1].Tag == null)
                        result = -1;
                    else if (item1.SubItems[1].Tag == null && item2.SubItems[1].Tag is IPAddress)
                        result = 1;
                    else result = CompareIPAddress((IPAddress)item1.SubItems[1].Tag, (IPAddress)item2.SubItems[1].Tag);
                    if (result == 0) result = CompareText(item1.Text, item2.Text);
                }

                return result;
            }

            public enum SortingType : int
            {
                Name = 0,
                IPAddress = 1,
            }
        }

        private sealed class DriveSorter : ItemSorter, IComparer
        {
            public SortingType SortBy { get; set; } = SortingType.DriveLetter;

            public int Compare(object x, object y)
            {
                int result = 0;
                if (x is ListViewItem item1 && y is ListViewItem item2)
                    result = Compare(item1, item2);
                return Direction == Direction.Ascending ? result : result * -1;
            }

            private int Compare(ListViewItem item1, ListViewItem item2)
            {
                int result = 0;
                if (SortBy == SortingType.Name)
                    result = CompareText(item1.Text, item2.Text);
                else if (SortBy == SortingType.Type)
                    result = CompareText(item1.SubItems[1].Text, item2.SubItems[1].Text);
                else if (SortBy == SortingType.FreeSpace)
                    result = CompareSize((long)item1.SubItems[2].Tag, (long)item2.SubItems[2].Tag);
                else if (SortBy == SortingType.TotalSize)
                    result = CompareSize((long)item1.SubItems[3].Tag, (long)item2.SubItems[3].Tag);
                else if (SortBy == SortingType.DriveLetter)
                    result = CompareText(item1.Name, item2.Name);

                return result;
            }
            
            public enum SortingType : int
            {
                Name = 0,
                Type = 1,
                FreeSpace = 2,
                TotalSize = 3,
                DriveLetter = 4,
            };
        }

        private sealed class FileSorter : ItemSorter, IComparer
        {
            public SortingType SortBy { get; set; } = SortingType.Name;
            
            public int Compare(object x, object y)
            {
                int result = 0;
                if (x is ListViewItem item1 && y is ListViewItem item2)
                    result = Compare(item1, item2);
                return Direction == Direction.Ascending ? result : result * -1;
            }
            
            private int Compare(ListViewItem item1, ListViewItem item2)
            {
                int result = 0;
                if(SortBy == SortingType.Name)
                {
                    if (item1.Tag is ItemType.Directory && item2.Tag is ItemType.File)
                        result = -1;
                    else if (item1.Tag is ItemType.File && item2.Tag is ItemType.Directory)
                        result = 1;
                    else result = CompareText(item1.Text, item2.Text);
                }
                else if(SortBy == SortingType.DateModified)
                {
                    if (item1.Tag is ItemType.Directory && item2.Tag is ItemType.File)
                        result = -1;
                    else if (item1.Tag is ItemType.File && item2.Tag is ItemType.Directory)
                        result = 1;
                    else result = CompareDateTime((DateTime)item1.SubItems[1].Tag, (DateTime)item2.SubItems[1].Tag);
                    if (result == 0) result = CompareText(item1.Text, item2.Text);
                }
                else if(SortBy == SortingType.Type)
                {
                    if (item1.Tag is ItemType.Directory && item2.Tag is ItemType.File)
                        result = -1;
                    else if (item1.Tag is ItemType.File && item2.Tag is ItemType.Directory)
                        result = 1;
                    else result = CompareText(item1.SubItems[2].Text, item2.SubItems[2].Text);
                    if (result == 0) result = CompareText(item1.Text, item2.Text);
                }
                else if(SortBy == SortingType.Size)
                {
                    if (item1.Tag is ItemType.Directory && item2.Tag is ItemType.File)
                        result = -1;
                    else if (item1.Tag is ItemType.File && item2.Tag is ItemType.Directory)
                        result = 1;
                    else result = CompareSize((long)item1.SubItems[3].Tag, (long)item2.SubItems[3].Tag);
                    if (result == 0) result = CompareText(item1.Text, item2.Text);
                }
                return result;
            }
            
            public enum SortingType : int
            {
                Name = 0,
                Type = 1,
                DateModified = 2,
                Size = 3
            };
        }

        private sealed class XbExplorerMenuStripRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                base.OnRenderToolStripBackground(e);

                using (var brush = new SolidBrush(ColorTable.ToolStripContentPanelGradientEnd))
                    e.Graphics.FillRectangle(brush, e.AffectedBounds);
            }
        }
    }
}
