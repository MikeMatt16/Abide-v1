using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2;
using Abide.HaloLibrary.Halo2.Retail;
using Abide.HaloLibrary.Halo2.Retail.Tag;
using Abide.HaloLibrary.Halo2.Retail.Tag.Generated;
using Abide.Wpf.Modules.ViewModel;
using Abide.Wpf.Modules.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TextureEditor
{
    public sealed class HaloBitmap : BaseViewModel
    {
        private HaloTag tag;
        private BitmapInformation[] bitmapInfo = null;
        private Dictionary<long, byte[]> resources = null;
        private Bitmap bitmapTagGroup = null;
        private string status = string.Empty;

        public ObservableCollection<BitmapContainer> BitmapContainers { get; } = new ObservableCollection<BitmapContainer>();
        public string Status
        {
            get => status;
            private set
            {
                if (status != value)
                {
                    status = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public HaloTag Tag
        {
            get => tag;
            private set
            {
                if (tag != value)
                {
                    tag = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public HaloBitmap(HaloTag tag)
        {
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));

            if (tag.GroupTag != HaloTags.bitm) throw new ArgumentException("Specified tag is not a bitmap.", nameof(tag));
        }
        public void Load()
        {
            resources = tag.Map.GetResourcesForTag(Tag);
            using (var data = tag.Map.ReadTagData(Tag))
            using (var reader = data.Stream.CreateReader())
            {
                _ = reader.BaseStream.Seek(Tag.MemoryAddress, SeekOrigin.Begin);
                bitmapTagGroup = new Bitmap();
                bitmapTagGroup.Read(reader);

                BlockField bitmapsBlock = (BlockField)bitmapTagGroup.TagBlocks[0].Fields[29];
                bitmapInfo = new BitmapInformation[bitmapsBlock.BlockList.Count];
                for (int i = 0; i < bitmapsBlock.BlockList.Count; i++)
                {
                    Block bitmapDataBlock = bitmapsBlock.BlockList[i];

                    TagBlock bitmapsTagBlock = (TagBlock)bitmapsBlock.Value;
                    _ = reader.BaseStream.Seek(bitmapsTagBlock.Offset + (i * 116) + 28, SeekOrigin.Begin);
                    LodInformation offsets = reader.Read<LodInformation>();
                    LodInformation sizes = reader.Read<LodInformation>();

                    if (!resources.ContainsKey(offsets.Lod1) && offsets.Lod1 != -1)
                        resources.Add(offsets.Lod1, ReadExternalData(offsets.Lod1, sizes.Lod1));

                    if (!resources.ContainsKey(offsets.Lod2) && offsets.Lod2 != -1)
                        resources.Add(offsets.Lod2, ReadExternalData(offsets.Lod2, sizes.Lod2));

                    if (!resources.ContainsKey(offsets.Lod3) && offsets.Lod3 != -1)
                        resources.Add(offsets.Lod3, ReadExternalData(offsets.Lod3, sizes.Lod3));

                    bitmapInfo[i] = new BitmapInformation()
                    {
                        Width = (short)bitmapDataBlock.Fields[1].Value,
                        Height = (short)bitmapDataBlock.Fields[2].Value,
                        Depth = (byte)bitmapDataBlock.Fields[3].Value,
                        Type = (short)bitmapDataBlock.Fields[5].Value,
                        Format = (short)bitmapDataBlock.Fields[6].Value,
                        Flags = (short)bitmapDataBlock.Fields[7].Value,
                        RegistrationPoint = (Point2)bitmapDataBlock.Fields[8].Value,
                        MipmapCount = (short)bitmapDataBlock.Fields[9].Value,
                        Offsets = offsets,
                        Sizes = sizes,
                    };
                }
            }
        }
        public void Process()
        {
            BitmapContainers.Clear();

            for (int i = 0; i < bitmapInfo.Length; i++)
            {
                BitmapInformation bitmap = bitmapInfo[i];
                int width = bitmap.Width;
                int height = bitmap.Height;
                BitmapFlags flags = (BitmapFlags)bitmap.Flags;
                int lodCount = 0;
                if (resources.ContainsKey(bitmap.Offsets.Lod1))
                    lodCount++;
                if (resources.ContainsKey(bitmap.Offsets.Lod2))
                    lodCount++;
                if (resources.ContainsKey(bitmap.Offsets.Lod3))
                    lodCount++;

                if (lodCount == 0)
                    continue;

                if (flags.HasFlag(BitmapFlags.Linear))
                    width = (int)Math.Ceiling(width / 16f) * 16;

                for (int j = 0; j < lodCount; j++)
                {
                    BitmapContainer container = new BitmapContainer(i, j);

                    for (int k = 0; k <= bitmap.MipmapCount + 1; k++)
                    {
                        BitmapSource source = LoadMipmap(bitmap, j, k, width, height);

                        if (source != null)
                            container.Mipmaps.Add(source);
                    }

                    BitmapContainers.Add(container);
                }
            }
        }
        private WriteableBitmap LoadMipmap(BitmapInformation bitmap, int lodNum, int mipmapNum, int width, int height)
        {
            DpiScale dpi = TextureEditor.CurrentDpiScale;
            int depth = bitmap.Depth;
            BitmapType type = (BitmapType)bitmap.Type;
            HaloBitmapFormat format = (HaloBitmapFormat)bitmap.Format;
            BitmapFlags flags = (BitmapFlags)bitmap.Flags;
            PixelFormat pixelFormat = PixelFormats.Bgra32;
            BitmapPalette palette = null;

            int sourceBits;
            switch (format)
            {
                case HaloBitmapFormat.A8:
                case HaloBitmapFormat.Y8:
                case HaloBitmapFormat.P8Bump:
                case HaloBitmapFormat.P8:
                case HaloBitmapFormat.AY8:
                    sourceBits = 8;
                    break;

                case HaloBitmapFormat.A8Y8:
                case HaloBitmapFormat.A1R5G5B5:
                case HaloBitmapFormat.A4R4G4B4:
                case HaloBitmapFormat.V8U8:
                case HaloBitmapFormat.G8B8:
                case HaloBitmapFormat.R5G6B5:
                    sourceBits = 16;
                    break;

                case HaloBitmapFormat.DXT1:
                    sourceBits = 4;
                    break;

                case HaloBitmapFormat.DXT3:
                case HaloBitmapFormat.DXT5:
                    sourceBits = 8;
                    break;

                case HaloBitmapFormat.Argbfp32:
                    sourceBits = 128;
                    break;

                default:
                    sourceBits = 32;
                    break;
            }

            switch (format)
            {
                case HaloBitmapFormat.R5G6B5: pixelFormat = PixelFormats.Bgr565; break;
                case HaloBitmapFormat.A1R5G5B5: pixelFormat = PixelFormats.Bgra32; break;
                case HaloBitmapFormat.X8R8G8B8: pixelFormat = PixelFormats.Bgr32; break;
                case HaloBitmapFormat.P8Bump: pixelFormat = PixelFormats.Indexed8; break;
                case HaloBitmapFormat.P8: pixelFormat = PixelFormats.Indexed8; break;
            }

            int bitmapWidth = width;
            int bitmapHeight = height;

            for (int k = 0; k < lodNum; k++)
            {
                bitmapWidth /= 2;
                bitmapHeight /= 2;
            }

            int dataOffset = 0;
            for (int k = 0; k < mipmapNum; k++)
            {
                dataOffset += bitmapWidth * bitmapHeight * sourceBits / 8;
                bitmapWidth /= 2;
                bitmapHeight /= 2;
            }

            if (bitmapWidth == 0 || bitmapHeight == 0) return null;
            int bitmapStride = bitmapWidth * sourceBits / 8;
            int bitmapSize = bitmapStride * bitmapHeight;

            // Certain formats fail if their length is too short. So we'll check that here
            switch (format)
            {
                case HaloBitmapFormat.DXT1:
                    bitmapSize = Math.Max(bitmapSize, 8); break;

                case HaloBitmapFormat.DXT3:
                case HaloBitmapFormat.DXT5:
                    bitmapSize = Math.Max(bitmapSize, 16); break;

                case HaloBitmapFormat.P8:
                case HaloBitmapFormat.P8Bump:
                    bitmapSize = Math.Max(bitmapSize, 16); break;

                default:
                    bitmapSize = Math.Max(bitmapSize, 1); break;
            }

            long resourceOffset;
            switch (lodNum)
            {
                case 1:
                    resourceOffset = bitmap.Offsets.Lod2;
                    break;
                case 2:
                    resourceOffset = bitmap.Offsets.Lod3;
                    break;
                case 3:
                    resourceOffset = bitmap.Offsets.Lod4;
                    break;
                case 4:
                    resourceOffset = bitmap.Offsets.Lod5;
                    break;
                case 5:
                    resourceOffset = bitmap.Offsets.Lod6;
                    break;
                default:
                    resourceOffset = bitmap.Offsets.Lod1;
                    break;
            }

            byte[] bitmapData = new byte[bitmapSize];
            byte[] resource = resources[resourceOffset];
            if (dataOffset + bitmapSize <= resource.Length)
                Array.Copy(resource, dataOffset, bitmapData, 0, bitmapSize);

            if (flags.HasFlag(BitmapFlags.Swizzled))
                bitmapData = TextureHelper.Swizzle(bitmapData, bitmapWidth, bitmapHeight, depth, sourceBits, true);

            if (bitmapData == null)
                bitmapData = new byte[bitmapSize];

            if (format == HaloBitmapFormat.P8Bump)
                palette = new BitmapPalette(bumpmapPalette);

            WriteableBitmap lod = new WriteableBitmap(bitmapWidth, bitmapHeight, dpi.PixelsPerInchX, dpi.PixelsPerInchY, pixelFormat, palette);

            try
            {
                lod.Lock();

                unsafe
                {
                    // Sometimes the Marshal.Copy() method fails, this is likely because the length of the data is longer than the allowed length
                    // it might also be because of invalid data. It needs further investigation

                    byte[] decoded = new byte[lod.BackBufferStride * lod.PixelHeight];
                    DecodeData(bitmapData, decoded, bitmapWidth, bitmapHeight, format);

                    // check
                    int length = decoded.Length > (lod.BackBufferStride * lod.PixelHeight) ? (lod.BackBufferStride * lod.PixelHeight) : decoded.Length;
                    Marshal.Copy(decoded, 0, lod.BackBuffer, length);
                    lod.AddDirtyRect(new Int32Rect(0, 0, lod.PixelWidth, lod.PixelHeight));
                }
            }
            finally
            {
                lod.Unlock();
            }

            return lod;
        }
        private void DecodeData(byte[] sourceData, byte[] destination, int width, int height, HaloBitmapFormat format)
        {
            int length = sourceData.Length;
            if (sourceData.Length > destination.Length)
                length = destination.Length;

            switch (format)
            {
                case HaloBitmapFormat.A8:
                    for (int x = 0; x < width * height; x++)
                    {
                        destination[x * 4] = 255;
                        destination[x * 4 + 1] = 255;
                        destination[x * 4 + 2] = 255;
                        destination[x * 4 + 3] = sourceData[x];
                    }
                    break;

                case HaloBitmapFormat.Y8:
                    for (int x = 0; x < width * height; x++)
                    {
                        destination[x * 4] = sourceData[x];
                        destination[x * 4 + 1] = sourceData[x];
                        destination[x * 4 + 2] = sourceData[x];
                        destination[x * 4 + 3] = 255;
                    }
                    break;

                case HaloBitmapFormat.AY8:
                    for (int x = 0; x < width * height; x++)
                    {
                        destination[x * 4] = sourceData[x];
                        destination[x * 4 + 1] = sourceData[x];
                        destination[x * 4 + 2] = sourceData[x];
                        destination[x * 4 + 3] = sourceData[x];
                    }
                    break;

                case HaloBitmapFormat.A8Y8:
                    for (int x = 0; x < width * height; x++)
                    {
                        destination[x * 4] = sourceData[x * 2];
                        destination[x * 4 + 1] = sourceData[x * 2];
                        destination[x * 4 + 2] = sourceData[x * 2];
                        destination[x * 4 + 3] = sourceData[x * 2 + 1];
                    }
                    break;

                case HaloBitmapFormat.V8U8:
                    for (int x = 0; x < width * height; x++)
                    {
                        destination[x * 4] = 255;
                        destination[x * 4 + 1] = (byte)(127 + (sbyte)sourceData[x * 2 + 1]);
                        destination[x * 4 + 2] = (byte)(127 + (sbyte)sourceData[x * 2]);
                        destination[x * 4 + 3] = 255;
                    }
                    break;

                case HaloBitmapFormat.A4R4G4B4:
                    for (int x = 0; x < width * height; x++)
                    {
                        destination[x * 4 + 3] = (byte)(sourceData[x * 2] & 0xf0);
                        destination[x * 4 + 2] = (byte)(sourceData[x * 2] & 0x0f);
                        destination[x * 4 + 1] = (byte)(sourceData[x * 2 + 1] & 0xf0);
                        destination[x * 4] = (byte)(sourceData[x * 2 + 1] & 0x0f);
                    }
                    break;

                case HaloBitmapFormat.DXT1:
                    if (width >= 4 && height >= 4)
                        TextureHelper.DecompressDxt1(destination, sourceData, width, height);
                    break;

                case HaloBitmapFormat.DXT3:
                    if (width >= 4 && height >= 4)
                        TextureHelper.DecompressDxt3(destination, sourceData, width, height);
                    break;

                case HaloBitmapFormat.DXT5:
                    if (width >= 4 && height >= 4)
                        TextureHelper.DecompressDxt5(destination, sourceData, width, height);
                    break;

                case HaloBitmapFormat.P8:
                case HaloBitmapFormat.P8Bump:
                case HaloBitmapFormat.A1R5G5B5:
                case HaloBitmapFormat.X8R8G8B8:
                case HaloBitmapFormat.A8R8G8B8:
                case HaloBitmapFormat.R5G6B5:
                    Array.Copy(sourceData, 0, destination, 0, length);
                    break;

                default: 
                    throw new NotImplementedException();
            }
        }
        private byte[] ReadExternalData(long offset, int length)
        {
            long location = ((uint)offset) >> 30;
            long address = ((uint)offset) & 0x3FFFFFFF;
            byte[] data = null;

            switch (location)
            {
                case 1:
                    if (File.Exists(AbideRegistry.Halo2Mainmenu))
                        using (FileStream fs = File.OpenRead(AbideRegistry.Halo2Mainmenu))
                        {
                            _ = fs.Seek(address, SeekOrigin.Begin);
                            data = new byte[length];
                            _ = fs.Read(data, 0, length);
                        }
                    break;
                case 2:
                    if (File.Exists(AbideRegistry.Halo2Shared))
                        using (FileStream fs = File.OpenRead(AbideRegistry.Halo2Shared))
                        {
                            _ = fs.Seek(address, SeekOrigin.Begin);
                            data = new byte[length];
                            _ = fs.Read(data, 0, length);
                        }
                    break;
                case 3:
                    if (File.Exists(AbideRegistry.Halo2SpShared))
                        using (FileStream fs = File.OpenRead(AbideRegistry.Halo2SpShared))
                        {
                            _ = fs.Seek(address, SeekOrigin.Begin);
                            data = new byte[length];
                            _ = fs.Read(data, 0, length);
                        }
                    break;
            }

            return data;
        }

        private enum BitmapType : short
        {
            Texture2D = 0,
            Texture3D = 1,
            CubeMap = 2
        }

        [Flags]
        private enum BitmapFlags : short
        {
            None = 0x0,
            PowTwoDimensions = 0x1,
            Compressed = 0x2,
            Palettized = 0x4,
            Swizzled = 0x8,
            Linear = 0x10,
            v16u16 = 0x20,
            MipmapDebugLevel = 0x40,
            PeferStutter = 0x80,
            AlwaysOn = 0x200,
            Unknown = 0x1000,
        }

        #region bumpmap palette
        private readonly Color[] bumpmapPalette = new Color[]
        {
            Color.FromArgb(255, 126, 126, 255),
            Color.FromArgb(255, 126, 127, 255),
            Color.FromArgb(255, 126, 128, 255),
            Color.FromArgb(255, 126, 129, 255),
            Color.FromArgb(255, 127, 126, 255),
            Color.FromArgb(255, 127, 127, 255),
            Color.FromArgb(255, 127, 128, 255),
            Color.FromArgb(255, 127, 129, 255),
            Color.FromArgb(255, 128, 126, 255),
            Color.FromArgb(255, 128, 127, 255),
            Color.FromArgb(255, 128, 128, 255),
            Color.FromArgb(255, 128, 129, 255),
            Color.FromArgb(255, 129, 126, 255),
            Color.FromArgb(255, 129, 127, 255),
            Color.FromArgb(255, 129, 128, 255),
            Color.FromArgb(255, 129, 129, 255),
            Color.FromArgb(255, 127, 130, 255),
            Color.FromArgb(255, 131, 127, 255),
            Color.FromArgb(255, 125, 127, 255),
            Color.FromArgb(255, 129, 131, 255),
            Color.FromArgb(255, 129, 124, 255),
            Color.FromArgb(255, 124, 130, 255),
            Color.FromArgb(255, 132, 129, 255),
            Color.FromArgb(255, 125, 124, 255),
            Color.FromArgb(255, 127, 133, 255),
            Color.FromArgb(255, 132, 125, 255),
            Color.FromArgb(255, 122, 128, 255),
            Color.FromArgb(255, 132, 132, 255),
            Color.FromArgb(255, 128, 122, 255),
            Color.FromArgb(255, 124, 133, 255),
            Color.FromArgb(255, 135, 127, 255),
            Color.FromArgb(255, 122, 124, 255),
            Color.FromArgb(255, 130, 136, 255),
            Color.FromArgb(255, 132, 121, 255),
            Color.FromArgb(255, 120, 131, 255),
            Color.FromArgb(255, 136, 132, 255),
            Color.FromArgb(255, 124, 119, 255),
            Color.FromArgb(255, 125, 137, 255),
            Color.FromArgb(255, 137, 123, 255),
            Color.FromArgb(255, 118, 125, 255),
            Color.FromArgb(255, 134, 137, 255),
            Color.FromArgb(255, 130, 117, 255),
            Color.FromArgb(255, 119, 135, 255),
            Color.FromArgb(255, 140, 129, 255),
            Color.FromArgb(255, 120, 119, 255),
            Color.FromArgb(255, 128, 141, 255),
            Color.FromArgb(255, 137, 119, 255),
            Color.FromArgb(255, 115, 129, 255),
            Color.FromArgb(255, 139, 136, 255),
            Color.FromArgb(255, 126, 114, 255),
            Color.FromArgb(255, 120, 140, 255),
            Color.FromArgb(255, 142, 124, 255),
            Color.FromArgb(255, 115, 121, 255),
            Color.FromArgb(255, 133, 142, 255),
            Color.FromArgb(255, 134, 113, 255),
            Color.FromArgb(255, 113, 135, 254),
            Color.FromArgb(255, 144, 133, 254),
            Color.FromArgb(255, 120, 113, 254),
            Color.FromArgb(255, 124, 145, 254),
            Color.FromArgb(255, 142, 118, 254),
            Color.FromArgb(255, 110, 126, 254),
            Color.FromArgb(255, 140, 142, 254),
            Color.FromArgb(255, 129, 109, 254),
            Color.FromArgb(255, 114, 142, 254),
            Color.FromArgb(255, 147, 127, 254),
            Color.FromArgb(255, 113, 115, 254),
            Color.FromArgb(255, 131, 148, 254),
            Color.FromArgb(255, 140, 111, 254),
            Color.FromArgb(255, 107, 133, 254),
            Color.FromArgb(255, 147, 139, 254),
            Color.FromArgb(255, 121, 107, 254),
            Color.FromArgb(255, 119, 148, 254),
            Color.FromArgb(255, 149, 119, 253),
            Color.FromArgb(255, 106, 120, 253),
            Color.FromArgb(255, 139, 149, 253),
            Color.FromArgb(255, 134, 105, 253),
            Color.FromArgb(255, 108, 141, 253),
            Color.FromArgb(255, 152, 132, 253),
            Color.FromArgb(255, 113, 108, 253),
            Color.FromArgb(255, 126, 153, 253),
            Color.FromArgb(255, 147, 111, 253),
            Color.FromArgb(255, 102, 128, 253),
            Color.FromArgb(255, 147, 146, 253),
            Color.FromArgb(255, 126, 101, 253),
            Color.FromArgb(255, 111, 150, 253),
            Color.FromArgb(255, 155, 123, 252),
            Color.FromArgb(255, 104, 113, 252),
            Color.FromArgb(255, 135, 155, 252),
            Color.FromArgb(255, 141, 103, 252),
            Color.FromArgb(255, 101, 138, 252),
            Color.FromArgb(255, 155, 139, 252),
            Color.FromArgb(255, 115, 101, 252),
            Color.FromArgb(255, 119, 157, 252),
            Color.FromArgb(255, 155, 113, 252),
            Color.FromArgb(255, 98, 121, 252),
            Color.FromArgb(255, 146, 154, 252),
            Color.FromArgb(255, 132, 96, 251),
            Color.FromArgb(255, 103, 149, 251),
            Color.FromArgb(255, 161, 129, 251),
            Color.FromArgb(255, 105, 105, 251),
            Color.FromArgb(255, 129, 161, 251),
            Color.FromArgb(255, 150, 102, 251),
            Color.FromArgb(255, 94, 132, 251),
            Color.FromArgb(255, 156, 148, 251),
            Color.FromArgb(255, 120, 94, 251),
            Color.FromArgb(255, 110, 159, 251),
            Color.FromArgb(255, 162, 117, 250),
            Color.FromArgb(255, 95, 113, 250),
            Color.FromArgb(255, 142, 162, 250),
            Color.FromArgb(255, 141, 93, 250),
            Color.FromArgb(255, 95, 145, 250),
            Color.FromArgb(255, 164, 138, 250),
            Color.FromArgb(255, 108, 96, 250),
            Color.FromArgb(255, 121, 166, 250),
            Color.FromArgb(255, 159, 104, 249),
            Color.FromArgb(255, 89, 125, 249),
            Color.FromArgb(255, 155, 157, 249),
            Color.FromArgb(255, 128, 88, 249),
            Color.FromArgb(255, 101, 158, 249),
            Color.FromArgb(255, 169, 124, 249),
            Color.FromArgb(255, 95, 103, 249),
            Color.FromArgb(255, 135, 169, 248),
            Color.FromArgb(255, 151, 92, 248),
            Color.FromArgb(255, 87, 139, 248),
            Color.FromArgb(255, 166, 148, 248),
            Color.FromArgb(255, 113, 87, 248),
            Color.FromArgb(255, 111, 168, 248),
            Color.FromArgb(255, 168, 109, 248),
            Color.FromArgb(255, 86, 115, 247),
            Color.FromArgb(255, 150, 167, 247),
            Color.FromArgb(255, 138, 84, 247),
            Color.FromArgb(255, 91, 154, 247),
            Color.FromArgb(255, 174, 134, 247),
            Color.FromArgb(255, 98, 92, 247),
            Color.FromArgb(255, 126, 175, 247),
            Color.FromArgb(255, 162, 94, 246),
            Color.FromArgb(255, 80, 130, 246),
            Color.FromArgb(255, 165, 159, 246),
            Color.FromArgb(255, 122, 80, 246),
            Color.FromArgb(255, 100, 168, 246),
            Color.FromArgb(255, 176, 117, 246),
            Color.FromArgb(255, 85, 103, 245),
            Color.FromArgb(255, 143, 176, 245),
            Color.FromArgb(255, 149, 82, 245),
            Color.FromArgb(255, 81, 148, 245),
            Color.FromArgb(255, 176, 146, 245),
            Color.FromArgb(255, 104, 82, 244),
            Color.FromArgb(255, 114, 178, 244),
            Color.FromArgb(255, 172, 100, 244),
            Color.FromArgb(255, 76, 119, 244),
            Color.FromArgb(255, 161, 170, 244),
            Color.FromArgb(255, 133, 74, 244),
            Color.FromArgb(255, 88, 165, 243),
            Color.FromArgb(255, 183, 128, 243),
            Color.FromArgb(255, 87, 91, 243),
            Color.FromArgb(255, 133, 183, 243),
            Color.FromArgb(255, 162, 84, 243),
            Color.FromArgb(255, 73, 138, 242),
            Color.FromArgb(255, 176, 158, 242),
            Color.FromArgb(255, 113, 73, 242),
            Color.FromArgb(255, 101, 179, 242),
            Color.FromArgb(255, 182, 108, 242),
            Color.FromArgb(255, 74, 106, 241),
            Color.FromArgb(255, 153, 181, 241),
            Color.FromArgb(255, 146, 72, 241),
            Color.FromArgb(255, 76, 158, 241),
            Color.FromArgb(255, 187, 141, 240),
            Color.FromArgb(255, 93, 79, 240),
            Color.FromArgb(255, 120, 188, 240),
            Color.FromArgb(255, 175, 89, 240),
            Color.FromArgb(255, 66, 125, 240),
            Color.FromArgb(255, 172, 172, 239),
            Color.FromArgb(255, 125, 66, 239),
            Color.FromArgb(255, 88, 176, 239),
            Color.FromArgb(255, 191, 120, 239),
            Color.FromArgb(255, 76, 92, 238),
            Color.FromArgb(255, 142, 191, 238),
            Color.FromArgb(255, 160, 72, 238),
            Color.FromArgb(255, 66, 148, 238),
            Color.FromArgb(255, 187, 156, 237),
            Color.FromArgb(255, 103, 67, 237),
            Color.FromArgb(255, 105, 190, 237),
            Color.FromArgb(255, 187, 97, 237),
            Color.FromArgb(255, 63, 111, 237),
            Color.FromArgb(255, 164, 185, 236),
            Color.FromArgb(255, 140, 61, 236),
            Color.FromArgb(255, 74, 170, 236),
            Color.FromArgb(255, 196, 134, 235),
            Color.FromArgb(255, 81, 77, 235),
            Color.FromArgb(255, 128, 197, 235),
            Color.FromArgb(255, 175, 77, 235),
            Color.FromArgb(255, 58, 134, 234),
            Color.FromArgb(255, 184, 171, 234),
            Color.FromArgb(255, 116, 58, 234),
            Color.FromArgb(255, 90, 188, 234),
            Color.FromArgb(255, 197, 109, 233),
            Color.FromArgb(255, 64, 95, 233),
            Color.FromArgb(255, 153, 196, 233),
            Color.FromArgb(255, 156, 61, 233),
            Color.FromArgb(255, 62, 159, 232),
            Color.FromArgb(255, 198, 150, 232),
            Color.FromArgb(255, 91, 64, 232),
            Color.FromArgb(255, 112, 201, 231),
            Color.FromArgb(255, 189, 85, 231),
            Color.FromArgb(255, 53, 118, 231),
            Color.FromArgb(255, 177, 186, 231),
            Color.FromArgb(255, 131, 52, 230),
            Color.FromArgb(255, 74, 182, 230),
            Color.FromArgb(255, 205, 125, 230),
            Color.FromArgb(255, 69, 78, 229),
            Color.FromArgb(255, 138, 205, 229),
            Color.FromArgb(255, 173, 64, 229),
            Color.FromArgb(255, 51, 145, 228),
            Color.FromArgb(255, 196, 167, 228),
            Color.FromArgb(255, 104, 52, 228),
            Color.FromArgb(255, 94, 200, 227),
            Color.FromArgb(255, 202, 97, 227),
            Color.FromArgb(255, 52, 101, 227),
            Color.FromArgb(255, 165, 200, 227),
            Color.FromArgb(255, 149, 49, 226),
            Color.FromArgb(255, 59, 172, 226),
            Color.FromArgb(255, 209, 142, 226),
            Color.FromArgb(255, 78, 63, 225),
            Color.FromArgb(255, 121, 211, 225),
            Color.FromArgb(255, 189, 72, 225),
            Color.FromArgb(255, 44, 128, 224),
            Color.FromArgb(255, 190, 185, 224),
            Color.FromArgb(255, 121, 44, 224),
            Color.FromArgb(255, 76, 195, 223),
            Color.FromArgb(255, 212, 113, 223),
            Color.FromArgb(255, 56, 82, 223),
            Color.FromArgb(255, 150, 211, 222),
            Color.FromArgb(255, 168, 51, 222),
            Color.FromArgb(255, 47, 158, 221),
            Color.FromArgb(255, 209, 161, 221),
            Color.FromArgb(255, 91, 49, 221),
            Color.FromArgb(255, 102, 212, 220),
            Color.FromArgb(255, 204, 84, 220),
            Color.FromArgb(255, 41, 109, 220),
            Color.FromArgb(255, 179, 201, 219),
            Color.FromArgb(255, 140, 39, 219),
            Color.FromArgb(255, 59, 186, 219),
            Color.FromArgb(255, 218, 132, 218),
            Color.FromArgb(255, 64, 64, 218),
            Color.FromArgb(255, 132, 219, 217),
            Color.FromArgb(255, 187, 58, 217),
            Color.FromArgb(255, 37, 140, 217),
            Color.FromArgb(255, 203, 181, 216),
            Color.FromArgb(255, 108, 38, 216),
            Color.FromArgb(255, 82, 208, 216),
            Color.FromArgb(255, 217, 100, 215),
            Color.FromArgb(255, 43, 89, 215),
            Color.FromArgb(255, 164, 215, 214),
            Color.FromArgb(255, 160, 39, 214),
            Color.FromArgb(255, 44, 172, 214),
            Color.FromArgb(0, 0, 0, 0)
        };
#endregion
    }

    public enum HaloBitmapFormat : short
    {
        A8 = 0,
        Y8 = 1,
        AY8 = 2,
        A8Y8 = 3,
        R5G6B5 = 6,
        A1R5G5B5 = 8,
        A4R4G4B4 = 9,
        X8R8G8B8 = 10,
        A8R8G8B8 = 11,
        DXT1 = 14,
        DXT3 = 15,
        DXT5 = 16,
        P8Bump = 17,
        P8 = 18,
        Argbfp32 = 19,
        Rgbfp32 = 20,
        Rgbfp16 = 21,
        V8U8 = 22,
        G8B8 = 23,
    }

    public struct LodInformation
    {
        public int Lod1 { get; set; }
        public int Lod2 { get; set; }
        public int Lod3 { get; set; }
        public int Lod4 { get; set; }
        public int Lod5 { get; set; }
        public int Lod6 { get; set; }
    }

    public class BitmapContainer : BaseAddOnViewModel
    {
        public int BitmapIndex { get; } = 0;
        public int LodIndex { get; } = 0;
        public ObservableCollection<BitmapSource> Mipmaps { get; } = new ObservableCollection<BitmapSource>();

        public BitmapContainer(int bitmapIndex, int lodIndex)
        {
            LodIndex = lodIndex;
            BitmapIndex = bitmapIndex;
        }
        public override string ToString()
        {
            return $"Bitmap {BitmapIndex} LOD #{LodIndex + 1}";
        }
        public void ImportFile(string filename)
        {
            if (!File.Exists(filename)) 
                throw new FileNotFoundException("File not found.", filename);

            throw new NotImplementedException();
        }
    }

    public class BitmapInformation
    {
        public short Width { get; set; }
        public short Height { get; set; }
        public byte Depth { get; set; }
        public short Type { get; set; }
        public short Format { get; set; }
        public short Flags { get; set; }
        public Point2 RegistrationPoint { get; set; }
        public short MipmapCount { get; set; }
        public LodInformation Offsets { get; set; }
        public LodInformation Sizes { get; set; }
    }
}
