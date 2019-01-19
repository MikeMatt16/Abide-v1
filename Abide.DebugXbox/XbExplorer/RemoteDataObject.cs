using Abide.DebugXbox;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Permissions;

namespace XbExplorer
{
    using DataFormats = System.Windows.Forms.DataFormats;
    using DataObject = System.Windows.Forms.DataObject;
    using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;
    public sealed class RemoteItemObject : DataObject, IDataObject
    {
        private static readonly TYMED[] ALLOWED_TYMEDS = new TYMED[]
        {
            TYMED.TYMED_ENHMF,
            TYMED.TYMED_GDI,
            TYMED.TYMED_HGLOBAL,
            TYMED.TYMED_ISTREAM,
            TYMED.TYMED_MFPICT
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct FILEDESCRIPTOR
        {
            public uint dwFlags;
            public Guid clsid;
            public Size sizel;
            public Point pointl;
            public uint dwFileAttributes;
            public FILETIME ftCreationTime;
            public FILETIME ftLastAccessTime;
            public FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct ItemObject
        {
            public string Directory;
            public string Name;
            public long Size;
            public DateTime Created;
            public DateTime Modified;
            public uint Attributes;

            public override string ToString()
            {
                return $"{Path.Combine(Directory, Name)} Length: {Size}";
            }
        }

        private readonly IPAddress xboxAddress;
        private readonly ItemObject[] items;
        private int lIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteItemObject"/> class using the specified debug Xbox and items.
        /// </summary>
        /// <param name="xbox">The remote debug Xbox.</param>
        /// <param name="remoteItems">The remote items.</param>
        public RemoteItemObject(Xbox xbox, params ItemInformation[] remoteItems)
        {
            //Setup
            xboxAddress = (xbox ?? throw new ArgumentNullException(nameof(xbox))).RemoteEndPoint.Address;
            List<ItemObject> items = new List<ItemObject>();

            //Connect
            xbox.Connect();

            //Set items
            if (remoteItems != null && remoteItems.Length > 0)
            {
                //Add files
                foreach (var file in remoteItems.Where(i => !i.Attributes.HasFlag(FileAttributes.Directory)))
                    items.Add(new ItemObject()
                    {
                        Attributes = (uint)file.Attributes,
                        Created = file.Created,
                        Directory = file.Directory,
                        Modified = file.Modified,
                        Name = file.Name,
                        Size = file.Size,
                    });

                //Discover files in directories
                foreach (var directory in remoteItems.Where(i => i.Attributes.HasFlag(FileAttributes.Directory)))
                {
                    //Get items
                    var tempItems = DiscoverObjects(xbox, Path.Combine(directory.Directory, directory.Name));

                    //Localize
                    for (int i = 0; i < tempItems.Length; i++)
                    {
                        tempItems[i].Name = Path.Combine(tempItems[i].Directory, tempItems[i].Name);
                        tempItems[i].Name = tempItems[i].Name.Replace(directory.Directory, string.Empty);
                        tempItems[i].Directory = directory.Directory;
                    }

                    //Add
                    items.AddRange(tempItems);
                }
                
                //Set
                this.items = items.ToArray();
            }

            //Disconnect
            xbox.Disconnect();
        }

        private ItemObject[] DiscoverObjects(Xbox xbox, string directoryPath)
        {
            //Get items
            xbox.GetDirectoryList(directoryPath, out var nestedItems);
            List<ItemObject> items = new List<ItemObject>();

            //Add
            foreach (var file in nestedItems.Where(i => !i.Attributes.HasFlag(FileAttributes.Directory)))
                items.Add(new ItemObject()
                {
                    Attributes = (uint)file.Attributes,
                    Created = file.Created,
                    Directory = file.Directory,
                    Modified = file.Modified,
                    Name = file.Name,
                    Size = file.Size,
                });

            //Recursively access items
            foreach (var directory in nestedItems.Where(i => i.Attributes.HasFlag(FileAttributes.Directory)))
                items.AddRange(DiscoverObjects(xbox, Path.Combine(directory.Directory, directory.Name)));

            //Return
            return items.ToArray();
        }

        public override object GetData(string format, bool autoConvert)
        {
            if (string.Compare(format, CFSTR_FILEDESCRIPTORW, true) == 0 && items != null)
                SetData(CFSTR_FILEDESCRIPTORW, GetFileDescriptor(items));
            else if (string.Compare(format, CFSTR_FILECONTENTS, true) == 0 && items != null)
                SetData(CFSTR_FILECONTENTS, GetFileContents(items, lIndex));
            else if (string.Compare(format, CFSTR_PERFORMEDDROPEFFECT, true) == 0)
                GC.Collect();

            //Return
            return base.GetData(format, autoConvert);
        }

        private MemoryStream GetFileDescriptor(ItemObject[] items)
        {
            //Prepare
            MemoryStream ms = new MemoryStream();

            //Prepare
            FILEDESCRIPTOR fileDescriptor = new FILEDESCRIPTOR();

            //Write count
            ms.Write(BitConverter.GetBytes(items.Length), 0, 4);

            //Write items
            foreach (var item in items)
            {
                //Setup file descriptor
                fileDescriptor.cFileName = item.Name;
                fileDescriptor.ftLastWriteTime = ToFILETIME(item.Modified);
                fileDescriptor.ftCreationTime = ToFILETIME(item.Modified);
                fileDescriptor.nFileSizeHigh = (uint)(item.Size >> 32);
                fileDescriptor.nFileSizeLow = (uint)(item.Size & 0xffffffff);
                fileDescriptor.dwFlags = FD_WRITESTIME | FD_CREATETIME | FD_FILESIZE | FD_PROGRESSUI;
                fileDescriptor.dwFileAttributes = item.Attributes;

                //Marshal
                int length = Marshal.SizeOf(fileDescriptor);
                byte[] data = new byte[length];

                //Check
                if (length > 0)
                {
                    //Allocate
                    IntPtr addr = Marshal.AllocHGlobal(length);
                    Marshal.StructureToPtr(fileDescriptor, addr, true); //Copy into memory
                    Marshal.Copy(addr, data, 0, length);    //Copy to byte array
                    Marshal.FreeHGlobal(addr);  //Free memory

                    //Write data
                    ms.Write(data, 0, data.Length);
                }
            }

            //Return
            return ms;
        }

        private MemoryStream GetFileContents(ItemObject[] items, int index)
        {
            //Prepare
            MemoryStream ms = null;
            
            //Check
            if (items != null && index < items.Length)
            {
                //Prepare
                ms = new MemoryStream();
                ItemObject item = items[index];

                //Setup
                byte[] buffer = new byte[item.Size];

                //Find xbox
                Xbox xbox = NameAnsweringProtocol.Discover(xboxAddress);
                try
                {
                    //Connect
                    xbox.Connect();

                    //Download
                    if (!xbox.GetData(Path.Combine(item.Directory, item.Name), ref buffer))
                        buffer = new byte[1];

                    //Disconnect
                    xbox.Disconnect();
                }
                catch { buffer = new byte[1]; }
                
                //Check
                if (buffer.Length == 0) buffer = new byte[1];
                ms.Write(buffer, 0, buffer.Length);
            }

            //Return
            return ms;
        }

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        void IDataObject.GetData(ref FORMATETC formatetc, out STGMEDIUM medium)
        {
            if (formatetc.cfFormat == (short)DataFormats.GetFormat(CFSTR_FILECONTENTS).Id)
                lIndex = formatetc.lindex;
            medium = new STGMEDIUM();
            if (GetTymedUsable(formatetc.tymed))
            {
                if (formatetc.tymed.HasFlag(TYMED.TYMED_HGLOBAL))
                {
                    medium.tymed = TYMED.TYMED_HGLOBAL;
                    medium.unionmember = GlobalAlloc(GHND | GMEM_DDESHARE, 1);
                    if (medium.unionmember == IntPtr.Zero)
                        throw new OutOfMemoryException();
                    try
                    {
                        ((IDataObject)this).GetDataHere(ref formatetc, ref medium);
                        return;
                    }
                    catch
                    {
                        GlobalFree(new HandleRef(medium, medium.unionmember));
                        medium.unionmember = IntPtr.Zero;
                        throw;
                    }
                }
                medium.tymed = formatetc.tymed;
                ((IDataObject)this).GetDataHere(ref formatetc, ref medium);
            }
            else Marshal.ThrowExceptionForHR(DV_E_TYMED);
        }

        private bool GetTymedUsable(TYMED tymed)
        {
            //Loop through allowed tymeds
            for (int i = 0; i < ALLOWED_TYMEDS.Length; i++)
                if (tymed.HasFlag(ALLOWED_TYMEDS[i]))
                    return true;    //If tymed is allowed just return true

            //Return
            return false;
        }

        private FILETIME ToFILETIME(DateTime dateTime)
        {
            return ToFILETIME(dateTime.ToFileTimeUtc());
        }
        private FILETIME ToFILETIME(long ticks)
        {
            return new FILETIME
            {
                dwHighDateTime = (int)(ticks >> 32),
                dwLowDateTime = (int)(ticks & 0xFFFFFFFF)
            };
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GlobalAlloc(int uFlags, int dwBytes);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GlobalFree(HandleRef handle);

        public const string CFSTR_PREFERREDDROPEFFECT = "Preferred DropEffect";
        public const string CFSTR_PERFORMEDDROPEFFECT = "Performed DropEffect";
        public const string CFSTR_FILEDESCRIPTORW = "FileGroupDescriptorW";
        public const string CFSTR_FILECONTENTS = "FileContents";
        
        public const int FD_CLSID = 0x00000001;
        public const int FD_SIZEPOINT = 0x00000002;
        public const int FD_ATTRIBUTES = 0x00000004;
        public const int FD_CREATETIME = 0x00000008;
        public const int FD_ACCESSTIME = 0x00000010;
        public const int FD_WRITESTIME = 0x00000020;
        public const int FD_FILESIZE = 0x00000040;
        public const int FD_PROGRESSUI = 0x00004000;
        public const int FD_LINKUI = 0x00008000;
        
        public const int GMEM_MOVEABLE = 0x0002;
        public const int GMEM_ZEROINIT = 0x0040;
        public const int GHND = (GMEM_MOVEABLE | GMEM_ZEROINIT);
        public const int GMEM_DDESHARE = 0x2000;
        
        public const int DV_E_TYMED = unchecked((Int32)0x80040069);
    }
}
