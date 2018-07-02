using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2BetaMap;
using Bitmap_Library.Compression;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Texture_Editor.Halo2Beta
{
    /// <summary>
    /// Represents a Halo 2 Bitmap.
    /// </summary>
    public sealed class HaloBitmap : IDisposable
    {
        /// <summary>
        /// Gets and returns a bitmap using the supplied bitmap, level of detail, and mipmap index.
        /// </summary>
        /// <param name="bitmapIndex">The index of the bitmap to retrieve.</param>
        /// <param name="lodIndex">The index of the level of detail to retrieve.</param>
        /// <param name="mipmapIndex">The index of the mipmap to retrieve.</param>
        /// <returns>A bitmap.</returns>
        public Bitmap this[int bitmapIndex, int lodIndex, int mipmapIndex]
        {
            get
            {
                //Get
                if (bitmapIndex < maps.Length && maps[bitmapIndex] != null)
                    if (lodIndex < maps[bitmapIndex].Length && maps[bitmapIndex][lodIndex] != null)
                        if (mipmapIndex < maps[bitmapIndex][lodIndex].Length && maps[bitmapIndex][lodIndex][mipmapIndex] != null)
                            return maps[bitmapIndex][lodIndex][mipmapIndex];

                //Return null
                return null;
            }
        }
        /// <summary>
        /// Gets and returns the count of bitmaps.
        /// </summary>
        [Category("Tag Properties")]
        public int BitmapCount
        {
            get { return tag.Bitmaps.Length; }
        }
        /// <summary>
        /// Gets and returns the count of sequences.
        /// </summary>
        [Category("Tag Properties")]
        public int SequenceCount
        {
            get { return tag.Sequences.Length; }
        }
        [Category("Import Options")]
        public ImportType Type
        {
            get { return (ImportType)tag.Header.type; }
            set { tag.Header.type = (ushort)value; }
        }
        [Category("Import Options")]
        public ImportFormat Format
        {
            get { return (ImportFormat)tag.Header.format; }
            set { tag.Header.format = (ushort)value; }
        }
        [Category("Import Options")]
        public ImportUsage Usage
        {
            get { return (ImportUsage)tag.Header.usage; }
            set { tag.Header.usage = (ushort)value; }
        }
        [Category("Import Options"), Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
        public ImportFlags Flags
        {
            get { return (ImportFlags)tag.Header.flags; }
            set { tag.Header.flags = (ushort)value; }
        }
        [Category("Import Options")]
        public float DetailFadeFactor
        {
            get { return tag.Header.detailFadeFactor; }
            set { tag.Header.detailFadeFactor = value; }
        }
        [Category("Import Options")]
        public float SharpenAmount
        {
            get { return tag.Header.sharpenAmount; }
            set { tag.Header.sharpenAmount = value; }
        }
        [Category("Import Options")]
        public float BumpHeight
        {
            get { return tag.Header.bumpHeight; }
            set { tag.Header.bumpHeight = value; }
        }
        [Category("Import Options")]
        public ushort ColorPlateWidth
        {
            get { return tag.Header.colorPlateWidth; }
            set { tag.Header.colorPlateWidth = value; }
        }
        [Category("Import Options")]
        public ushort ColorPlateHeight
        {
            get { return tag.Header.colorPlateHeight; }
            set { tag.Header.colorPlateHeight = value; }
        }
        [Category("Import Options")]
        public float BlurFilterSize
        {
            get { return tag.Header.blurFilterSize; }
            set { tag.Header.blurFilterSize = value; }
        }
        [Category("Import Options")]
        public float AlphaBias
        {
            get { return tag.Header.alphaBias; }
            set { tag.Header.alphaBias = value; }
        }
        [Category("Import Options")]
        public ushort MipmapCount
        {
            get { return tag.Header.mipmapCount; }
            set { tag.Header.mipmapCount = value; }
        }
        [Category("Import Options")]
        public ushort SpriteUsage
        {
            get { return tag.Header.spriteUsage; }
            set { tag.Header.spriteUsage = value; }
        }
        [Category("Import Options")]
        public ushort SpriteSpacing
        {
            get { return tag.Header.spriteSpacing; }
            set { tag.Header.spriteSpacing = value; }
        }
        [Category("Import Options")]
        public ImportForceFormat ForceFormat
        {
            get { return (ImportForceFormat)tag.Header.forceFormat; }
            set { tag.Header.forceFormat = (ushort)value; }
        }
        [Browsable(false)]
        public BitmapProperties[] Bitmaps
        {
            get { return bitmaps; }
        }
        [Browsable(false)]
        public SequenceProperties[] Sequences
        {
            get { return sequences; }
        }
        [Browsable(false)]
        public IndexEntry Entry
        {
            get { return entry; }
        }

        private readonly Bitmap[][][] maps;
        private readonly IndexEntry entry;
        private readonly BitmapTag tag;

        private readonly SequenceProperties[] sequences;
        private readonly BitmapProperties[] bitmaps;

        /// <summary>
        /// Initializes a new <see cref="HaloBitmap"/> instance using the supplied object index entry.
        /// </summary>
        /// <param name="entry">The object index entry that contains the bitmap tag group data.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entry"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="entry"/> is not a bitmap.</exception>
        public HaloBitmap(IndexEntry entry)
        {
            //Check
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            else if (entry.Root != HaloTags.bitm) throw new ArgumentException("Index entry is not bitmap.", nameof(entry));

            //Setup
            this.entry = entry;
            tag = new BitmapTag(entry);
            maps = new Bitmap[tag.Bitmaps.Length][][];

            //Setup Property Accessors
            bitmaps = new BitmapProperties[tag.Bitmaps.Length];
            for (int i = 0; i < tag.Bitmaps.Length; i++)
                bitmaps[i] = new BitmapProperties(this, i);
            sequences = new SequenceProperties[tag.Sequences.Length];
            for (int i = 0; i < tag.Sequences.Length; i++)
                sequences[i] = new SequenceProperties(this, i);

            //Check
            if (tag == null) return;
            else if (tag.Bitmaps.Length == 0) return;

            //Loop through bitmaps
            for (int k = 0; k < tag.Bitmaps.Length; k++)
            {
                //Setup
                BitmapTagGroup.Bitmap bitmap = tag.Bitmaps[k];
                maps[k] = new Bitmap[6][];

                //Loop through LODs
                byte[] sourceData = null;
                for (int l = 0; l < 6; l++)
                {
                    //Get source data
                    if (bitmap.rawOffsets[l] != uint.MaxValue)
                    {
                        RawLocation rawLocation = (RawLocation)(bitmap.rawOffsets[l] & 0xC0000000);
                        if (rawLocation == RawLocation.Local) sourceData = entry.Raws[RawSection.Bitmap][(int)bitmap.rawOffsets[l]].GetBuffer();
                        else
                        {
                            string filelocation = string.Empty;
                            int rawOffset = (int)(bitmap.rawOffsets[l] & (uint)RawLocation.LocalMask);
                            switch (rawLocation)
                            {
                                case RawLocation.Mainmenu:
                                    filelocation = HaloSettings.MainmenuPath;
                                    break;
                                case RawLocation.Shared:
                                    filelocation = HaloSettings.SharedPath;
                                    break;
                            }

                            //Check
                            if (File.Exists(filelocation))
                                using (FileStream fs = new FileStream(filelocation, FileMode.Open))
                                using (BinaryReader mapReader = new BinaryReader(fs))
                                {
                                    fs.Seek(rawOffset, SeekOrigin.Begin);
                                    sourceData = mapReader.ReadBytes(bitmap.rawLengths[l]);
                                }
                        }
                    }

                    //Set
                    if (sourceData.Length == 0)
                        continue;

                    //Prepare
                    BitmapFlags flags = (BitmapFlags)bitmap.flags;
                    BitmapFormat format = (BitmapFormat)bitmap.format;
                    PixelFormat bitmapFormat = PixelFormat.Format32bppArgb;
                    int sourceBits = 32;
                    switch (format)
                    {
                        case BitmapFormat.A8:
                        case BitmapFormat.Y8:
                        case BitmapFormat.P8Bump:
                        case BitmapFormat.P8:
                        case BitmapFormat.Ay8: sourceBits = 8; break;
                        case BitmapFormat.A8y8:
                        case BitmapFormat.A1r5g5b5:
                        case BitmapFormat.A4r4g4b4:
                        case BitmapFormat.V8u8:
                        case BitmapFormat.G8b8:
                        case BitmapFormat.R5g6b5: sourceBits = 16; break;

                        case BitmapFormat.Dxt1: sourceBits = 4; break;
                        case BitmapFormat.Dxt5:
                        case BitmapFormat.Dxt3: sourceBits = 8; break;

                        case BitmapFormat.Argbfp32: sourceBits = 128; break;
                    }

                    //Handle
                    switch (format)
                    {
                        case BitmapFormat.R5g6b5: bitmapFormat = PixelFormat.Format16bppRgb565; break;
                        case BitmapFormat.A1r5g5b5: bitmapFormat = PixelFormat.Format16bppArgb1555; break;
                        case BitmapFormat.X8r8g8b8: bitmapFormat = PixelFormat.Format32bppRgb; break;
                        case BitmapFormat.P8Bump: bitmapFormat = PixelFormat.Format8bppIndexed; break;
                        case BitmapFormat.P8: bitmapFormat = PixelFormat.Format8bppIndexed; break;
                    }

                    //Prepare
                    int width = bitmap.width, height = bitmap.height;
                    if (flags.HasFlag(BitmapFlags.Linear))
                    {
                        width = (int)Math.Ceiling(width / 16f) * 16;
                    }

                    //Loop LOD
                    for (int i = 0; i < l; i++)
                    {
                        width /= 2;
                        height /= 2;
                    }

                    //Prepare
                    int mapWidth = width, mapHeight = height, location = 0;
                    int mipmapCount = bitmap.mipmapCount;
                    maps[k][l] = new Bitmap[mipmapCount + 1];
                    Size bitmapSize = Size.Empty;
                    byte[] mapData = null;

                    //Loop
                    for (int i = 1; i <= mipmapCount + 1; i++)
                    {
                        //Prepare
                        int mapIndex = i - 1;
                        mapWidth = width; mapHeight = height;
                        for (int j = 1; j < i; j++)
                        {
                            mapWidth /= 2;
                            mapHeight /= 2;
                        }

                        //Get Size
                        bitmapSize = new Size(mapWidth, mapHeight);

                        //Check
                        if (bitmapSize.Width == 0 || bitmapSize.Height == 0) continue;

                        //Create Map
                        int mapStride = mapWidth * sourceBits / 8;
                        int mapSize = mapStride * mapHeight;

                        //Ehh?
                        switch (format)
                        {
                            case BitmapFormat.Dxt1: mapSize = Math.Max(mapSize, 8); break;
                            case BitmapFormat.Dxt3:
                            case BitmapFormat.Dxt5: mapSize = Math.Max(mapSize, 16); break;
                            case BitmapFormat.P8:
                            case BitmapFormat.P8Bump: mapSize = Math.Max(mapSize, 16); break;
                            default: mapSize = Math.Max(mapSize, 1); break;
                        }

                        mapData = new byte[mapSize];
                        if (location + mapSize > sourceData.Length) continue;
                        Array.Copy(sourceData, location, mapData, 0, mapSize);

                        //Deswizzle?
                        if (format != BitmapFormat.Dxt1 && format != BitmapFormat.Dxt3 && format != BitmapFormat.Dxt5 && flags.HasFlag(BitmapFlags.PowTwoDimensions))
                            mapData = Swizzler.Swizzle(mapData, mapWidth, mapHeight, bitmap.depth, sourceBits, true);
                        if (mapData == null) mapData = new byte[mapSize];

                        using (Bitmap map = new Bitmap(mapWidth, mapHeight, bitmapFormat))
                        {
                            unsafe
                            {
                                //Lock Bits
                                BitmapData data = map.LockBits(new Rectangle(0, 0, mapWidth, mapHeight), ImageLockMode.ReadWrite, bitmapFormat);

                                //Prepare Buffer
                                byte[] bitmapData = new byte[data.Stride * data.Height];
                                int dataLength = Math.Min(bitmapData.Length, mapSize);

                                //Handle Format...
                                switch (format)
                                {
                                    case BitmapFormat.A8:
                                        for (int x = 0; x < mapWidth * mapHeight; x++)
                                        {
                                            bitmapData[x * 4 + 0] = 255;
                                            bitmapData[x * 4 + 1] = 255;
                                            bitmapData[x * 4 + 2] = 255;
                                            bitmapData[x * 4 + 3] = mapData[x];
                                        }
                                        break;
                                    case BitmapFormat.Y8:
                                        for (int x = 0; x < mapWidth * mapHeight; x++)
                                        {
                                            bitmapData[x * 4 + 0] = mapData[x];
                                            bitmapData[x * 4 + 1] = mapData[x];
                                            bitmapData[x * 4 + 2] = mapData[x];
                                            bitmapData[x * 4 + 3] = 255;
                                        }
                                        break;
                                    case BitmapFormat.Ay8:
                                        for (int x = 0; x < mapWidth * mapHeight; x++)
                                        {
                                            bitmapData[x * 4 + 0] = mapData[x];
                                            bitmapData[x * 4 + 1] = mapData[x];
                                            bitmapData[x * 4 + 2] = mapData[x];
                                            bitmapData[x * 4 + 3] = mapData[x];
                                        }
                                        break;
                                    case BitmapFormat.A8y8:
                                        for (int x = 0; x < mapWidth * mapHeight; x++)
                                        {
                                            bitmapData[x * 4 + 0] = mapData[x * 2];
                                            bitmapData[x * 4 + 1] = mapData[x * 2];
                                            bitmapData[x * 4 + 2] = mapData[x * 2];
                                            bitmapData[x * 4 + 3] = mapData[x * 2 + 1];
                                        }
                                        break;
                                    case BitmapFormat.A4r4g4b4:
                                        for (int x = 0; x < mapWidth * mapHeight; x++)
                                        {
                                            bitmapData[x * 4 + 0] = (byte)(mapData[x * 2 + 0] & 0xF0);
                                            bitmapData[x * 4 + 1] = (byte)(mapData[x * 2 + 0] & 0x0F);
                                            bitmapData[x * 4 + 2] = (byte)(mapData[x * 2 + 1] & 0xF0);
                                            bitmapData[x * 4 + 3] = (byte)(mapData[x * 2 + 1] & 0x0F);
                                        }
                                        break;
                                    case BitmapFormat.P8Bump:
                                    case BitmapFormat.P8: Array.Copy(mapData, 0, bitmapData, 0, dataLength); break;
                                    case BitmapFormat.R5g6b5: Array.Copy(mapData, 0, bitmapData, 0, dataLength); break;
                                    case BitmapFormat.A1r5g5b5: Array.Copy(mapData, 0, bitmapData, 0, dataLength); break;
                                    case BitmapFormat.X8r8g8b8: Array.Copy(mapData, 0, bitmapData, 0, dataLength); break;
                                    case BitmapFormat.A8r8g8b8: Array.Copy(mapData, 0, bitmapData, 0, dataLength); break;
                                    case BitmapFormat.Dxt1:
                                        if (mapWidth >= 4 && mapHeight >= 4)
                                            S3TC.DecompressDxt1(ref bitmapData, mapData, bitmapSize); break;
                                    case BitmapFormat.Dxt3:
                                        if (mapWidth >= 4 && mapHeight >= 4)
                                            S3TC.DecompressDxt3(ref bitmapData, mapData, bitmapSize); break;
                                    case BitmapFormat.Dxt5:
                                        if (mapWidth >= 4 && mapHeight >= 4)
                                            S3TC.DecompressDxt5(ref bitmapData, mapData, bitmapSize); break;
                                    case BitmapFormat.Argbfp32:
                                        for (int x = 0; x < mapWidth * mapHeight; x++)
                                        {
                                            bitmapData[x * 4 + 0] = (byte)Math.Min((BitConverter.ToSingle(mapData, x * 16 + 8) * 255f), 255f);
                                            bitmapData[x * 4 + 1] = (byte)Math.Min((BitConverter.ToSingle(mapData, x * 16 + 4) * 255f), 255f);
                                            bitmapData[x * 4 + 2] = (byte)Math.Min((BitConverter.ToSingle(mapData, x * 16) * 255f), 255f);
                                            bitmapData[x * 4 + 3] = (byte)Math.Min((BitConverter.ToSingle(mapData, x * 16 + 12) * 255f), 255f);
                                        }
                                        break;
                                    case BitmapFormat.Rgbfp32: break;
                                    case BitmapFormat.Rgbfp16: break;
                                    case BitmapFormat.V8u8:
                                        for (int x = 0; x < mapWidth * mapHeight; x++)
                                        {
                                            bitmapData[x * 4 + 0] = 255;
                                            bitmapData[x * 4 + 1] = (byte)(127 + (sbyte)mapData[x * 2 + 1]);
                                            bitmapData[x * 4 + 2] = (byte)(127 + (sbyte)mapData[x * 2]);
                                            bitmapData[x * 4 + 3] = 255;
                                        }
                                        break;
                                    case BitmapFormat.G8b8: break;
                                }

                                //Copy
                                Marshal.Copy(bitmapData, 0, data.Scan0, bitmapData.Length);
                                map.UnlockBits(data);

                                //Setup Palettes
                                if (format == BitmapFormat.P8Bump) map.SetNormalMapPalette();
                                else if (format == BitmapFormat.P8) map.SetGrayscalePalette();

                                //Set
                                location += mapSize;
                            }

                            //Draw into cropped image
                            maps[k][l][mapIndex] = new Bitmap(mapWidth, mapHeight, bitmapFormat);
                            using (Graphics g = Graphics.FromImage(maps[k][l][mapIndex])) g.DrawImage(map, Point.Empty);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Writes a this <see cref="HaloBitmap"/> instance to the tag's stream.
        /// </summary>
        /// <exception cref="IOException">An IO error occured.</exception>
        public void Write()
        {
            //Goto
            entry.TagData.Seek(entry.Offset, SeekOrigin.Begin);

            //Write Bitmap
            tag.Write(entry.TagData);
        }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            //Loop through each bitmap, each level of detail and each mipmap
            for (int b = 0; b < maps.Length; b++)
                if (maps[b] != null)
                    for (int l = 0; l < maps[b].Length; l++)
                        if (maps[b][l] != null)
                            for (int m = 0; m < maps[b][l].Length; m++)
                                if (maps[b][l][m] != null)
                                    maps[b][l][m].Dispose();
        }
        /// <summary>
        /// Gets and returns the <see cref="BitmapFormat"/> of the specified bitmap at a given index.
        /// </summary>
        /// <param name="bitmapIndex">The zero-based index of the bitmap.</param>
        /// <returns>A <see cref="BitmapFormat"/> value.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="bitmapIndex"/> is outside of valid range.</exception>
        public BitmapFormat GetFormat(int bitmapIndex)
        {
            //Check
            if (bitmapIndex < 0 || bitmapIndex > tag.Bitmaps.Length)
                throw new ArgumentOutOfRangeException(nameof(bitmapIndex));

            //Return
            return (BitmapFormat)tag.Bitmaps[bitmapIndex].format;
        }
        /// <summary>
        /// Gets and returns the Mipmap Count of the specified bitmap at a given index.
        /// </summary>
        /// <param name="bitmapIndex">The zero-based index of the bitmap.</param>
        /// <returns>The number of mipmaps in the specified bitmap.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="bitmapIndex"/> is outside of valid range.</exception>
        public int GetMipmapCount(int bitmapIndex)
        {
            //Check
            if (bitmapIndex < 0 || bitmapIndex > tag.Bitmaps.Length)
                throw new ArgumentOutOfRangeException(nameof(bitmapIndex));

            //Return
            return tag.Bitmaps[bitmapIndex].mipmapCount;
        }

        /// <summary>
        /// Represents a bitmap instance within a Halo 2 bitmap tag.
        /// </summary>
        public sealed class BitmapProperties
        {
            [Browsable(false)]
            public uint Address
            {
                get { return (uint)(bitmap.tag.Header.bitmaps.Offset + (Marshal.SizeOf(bitmap.tag.Bitmaps[index]) * index)); }
            }
            [Category("Bitmap Properties")]
            public ushort Width
            {
                get { return bitmap.tag.Bitmaps[index].width; }
                set { bitmap.tag.Bitmaps[index].width = value; }
            }
            [Category("Bitmap Properties")]
            public ushort Height
            {
                get { return bitmap.tag.Bitmaps[index].height; }
                set { bitmap.tag.Bitmaps[index].height = value; }
            }
            [Category("Bitmap Properties")]
            public byte Depth
            {
                get { return bitmap.tag.Bitmaps[index].depth; }
                set { bitmap.tag.Bitmaps[index].depth = value; }
            }
            [Category("Bitmap Properties"), Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
            public BitmapImportFlags ImportFlags
            {
                get { return (BitmapImportFlags)bitmap.tag.Bitmaps[index].importFlags; }
                set { bitmap.tag.Bitmaps[index].importFlags = (byte)value; }
            }
            [Category("Bitmap Properties")]
            public BitmapType Type
            {
                get { return (BitmapType)bitmap.tag.Bitmaps[index].type; }
                set { bitmap.tag.Bitmaps[index].type = (ushort)value; }
            }
            [Category("Bitmap Properties")]
            public BitmapFormat Format
            {
                get { return (BitmapFormat)bitmap.tag.Bitmaps[index].format; }
                set { bitmap.tag.Bitmaps[index].format = (ushort)value; }
            }
            [Category("Bitmap Properties"), Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
            public BitmapFlags Flags
            {
                get { return (BitmapFlags)bitmap.tag.Bitmaps[index].flags; }
                set { bitmap.tag.Bitmaps[index].flags = (ushort)value; }
            }
            [Category("Bitmap Properties")]
            public short RegistrationPointX
            {
                get { return bitmap.tag.Bitmaps[index].registrationPointX; }
                set { bitmap.tag.Bitmaps[index].registrationPointX = value; }
            }
            [Category("Bitmap Properties")]
            public short RegistrationPointY
            {
                get { return bitmap.tag.Bitmaps[index].registrationPointY; }
                set { bitmap.tag.Bitmaps[index].registrationPointY = value; }
            }
            [Category("Bitmap Properties")]
            public ushort MipmapCount
            {
                get { return bitmap.tag.Bitmaps[index].mipmapCount; }
                set { bitmap.tag.Bitmaps[index].mipmapCount = value; }
            }
            [Category("Bitmap Properties")]
            public ushort LowDetailMipmapCount
            {
                get { return bitmap.tag.Bitmaps[index].lowDetailMipmapCount; }
                set { bitmap.tag.Bitmaps[index].lowDetailMipmapCount = value; }
            }
            [Category("Bitmap Properties")]
            public int PixelsOffset
            {
                get { return bitmap.tag.Bitmaps[index].pixelsOffset; }
                set { bitmap.tag.Bitmaps[index].pixelsOffset = value; }
            }
            [Category("Raw Properties")]
            public uint[] RawOffsets
            {
                get { return bitmap.tag.Bitmaps[index].rawOffsets; }
            }
            [Category("Raw Properties")]
            public int[] RawLengths
            {
                get { return bitmap.tag.Bitmaps[index].rawLengths; }
            }

            private readonly HaloBitmap bitmap;
            private readonly int index;

            /// <summary>
            /// Initializes a new <see cref="BitmapProperties"/> instance using the supplied <see cref="HaloBitmap"/> instance and bitmap index.
            /// </summary>
            /// <param name="bitmap">The Halo 2 bitmap instance.</param>
            /// <param name="index">The bitmap index within the bitmap tag.</param>
            /// <exception cref="ArgumentNullException"><paramref name="bitmap"/> is null.</exception>
            /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> it outside of valid range.</exception>
            public BitmapProperties(HaloBitmap bitmap, int index)
            {
                //Check
                if (bitmap == null) throw new ArgumentNullException(nameof(bitmap));
                if (index < 0 || bitmap.BitmapCount <= index) throw new ArgumentOutOfRangeException(nameof(index));

                //Setup
                this.bitmap = bitmap;
                this.index = index;
            }
        }

        /// <summary>
        /// Represents a sequence instance within a Halo 2 bitmap tag.
        /// </summary>
        public sealed class SequenceProperties
        {
            [Category("Sequence Properties")]
            public string Name
            {
                get { return new string(bitmap.tag.Sequences[index].name).Trim('\0'); }
                set
                {
                    //Prepare
                    string name = value.Trim();
                    bitmap.tag.Sequences[index].name = new char[32];

                    //Set
                    for (int i = 0; i < Math.Min(name.Length, 32); i++)
                        bitmap.tag.Sequences[index].name[i] = name[i];
                }
            }
            [Category("Sequence Properties")]
            public short Index
            {
                get { return bitmap.tag.Sequences[index].firstBitmapIndex; }
                set { bitmap.tag.Sequences[index].firstBitmapIndex = value; }
            }
            [Category("Sequence Properties")]
            public ushort Count
            {
                get { return bitmap.tag.Sequences[index].bitmapCount; }
                set { bitmap.tag.Sequences[index].bitmapCount = value; }
            }
            [Browsable(false)]
            public uint Address
            {
                get { return (uint)(bitmap.tag.Header.sequences.Offset + (Marshal.SizeOf(bitmap.tag.Sequences[index]) * index)); }
            }

            private readonly HaloBitmap bitmap;
            private readonly int index;

            /// <summary>
            /// Initializes a new <see cref="SequenceProperties"/> instance using the supplied <see cref="HaloBitmap"/> instance and sequence index.
            /// </summary>
            /// <param name="bitmap">The Halo 2 bitmap instance.</param>
            /// <param name="index">The sequence index within the bitmap tag.</param>
            /// <exception cref="ArgumentNullException"><paramref name="bitmap"/> is null.</exception>
            /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> it outside of valid range.</exception>
            public SequenceProperties(HaloBitmap bitmap, int index)
            {
                //Check
                if (bitmap == null) throw new ArgumentNullException(nameof(bitmap));
                if (index < 0 || bitmap.SequenceCount <= index) throw new ArgumentOutOfRangeException(nameof(index));

                this.bitmap = bitmap;
                this.index = index;
            }

            /// <summary>
            /// Represents a sprite instance within a sequence instance within a Halo 2 bitmap tag.
            /// </summary>
            public sealed class SpriteProperties
            {
                [Category("Sprite Properties")]
                public short BitmapIndex
                {
                    get { return sequence.bitmap.tag.Sprites[sequence.index][index].bitmapIndex; }
                    set { sequence.bitmap.tag.Sprites[sequence.index][index].bitmapIndex = value; }
                }
                [Category("Sprite Properties")]
                public float Left
                {
                    get { return sequence.bitmap.tag.Sprites[sequence.index][index].left; }
                    set { sequence.bitmap.tag.Sprites[sequence.index][index].left = value; }
                }
                [Category("Sprite Properties")]
                public float Right
                {
                    get { return sequence.bitmap.tag.Sprites[sequence.index][index].right; }
                    set { sequence.bitmap.tag.Sprites[sequence.index][index].right = value; }
                }
                [Category("Sprite Properties")]
                public float Top
                {
                    get { return sequence.bitmap.tag.Sprites[sequence.index][index].top; }
                    set { sequence.bitmap.tag.Sprites[sequence.index][index].top = value; }
                }
                [Category("Sprite Properties")]
                public float Bottom
                {
                    get { return sequence.bitmap.tag.Sprites[sequence.index][index].bottom; }
                    set { sequence.bitmap.tag.Sprites[sequence.index][index].bottom = value; }
                }
                [Category("Sprite Properties")]
                public float RegistrationPointX
                {
                    get { return sequence.bitmap.tag.Sprites[sequence.index][index].registrationPointX; }
                    set { sequence.bitmap.tag.Sprites[sequence.index][index].registrationPointX = value; }
                }
                [Category("Sprite Properties")]
                public float RegistrationPointY
                {
                    get { return sequence.bitmap.tag.Sprites[sequence.index][index].registrationPointY; }
                    set { sequence.bitmap.tag.Sprites[sequence.index][index].registrationPointY = value; }
                }

                private readonly SequenceProperties sequence;
                private readonly int index;

                /// <summary>
                /// Initializes a new <see cref="SpriteProperties"/> instance using the supplied <see cref="SequenceProperties"/> instance and sequence index.
                /// </summary>
                /// <param name="sequence">The Halo 2 sequence instance.</param>
                /// <param name="index">The sprite index within the sequence.</param>
                /// <exception cref="ArgumentNullException"><paramref name="sequence"/> is null.</exception>
                /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> it outside of valid range.</exception>
                public SpriteProperties(SequenceProperties sequence, int index)
                {
                    //Check
                    if (sequence == null) throw new ArgumentNullException(nameof(bitmap));
                    if (index < 0 || sequence.bitmap.tag.Sprites[sequence.index].Length <= index) throw new ArgumentOutOfRangeException(nameof(index));

                    this.sequence = sequence;
                    this.index = index;
                }
            }
        }

        /// <summary>
        /// Represents a Halo 2 bitmap tag.
        /// </summary>
        private sealed class BitmapTag
        {
            public BitmapTagGroup Header;
            public BitmapTagGroup.Sequence[] Sequences;
            public BitmapTagGroup.Sequence.Sprite[][] Sprites;
            public BitmapTagGroup.Bitmap[] Bitmaps;

            /// <summary>
            /// Initializes a new <see cref="BitmapTag"/> using the supplied object index entry.
            /// </summary>
            /// <param name="entry">The bitmap object index entry.</param>
            public BitmapTag(IndexEntry entry)
            {
                using (BinaryReader reader = entry.TagData.CreateReader())
                {
                    //Goto
                    entry.TagData.Seek(entry.Offset, SeekOrigin.Begin);

                    //Read
                    Header = reader.Read<BitmapTagGroup>();

                    //Setup tag blocks
                    Sequences = new BitmapTagGroup.Sequence[Header.sequences.Count];
                    Sprites = new BitmapTagGroup.Sequence.Sprite[Header.sequences.Count][];
                    Bitmaps = new BitmapTagGroup.Bitmap[Header.bitmaps.Count];

                    //Loop
                    entry.TagData.Seek(Header.sequences.Offset, SeekOrigin.Begin);
                    for (int i = 0; i < Header.sequences.Count; i++)
                        Sequences[i] = reader.Read<BitmapTagGroup.Sequence>();
                    for (int i = 0; i < Header.sequences.Count; i++)
                    {
                        //Setup tag blocks
                        Sprites[i] = new BitmapTagGroup.Sequence.Sprite[Sequences[i].sprites.Count];

                        //Loop
                        entry.TagData.Seek(Sequences[i].sprites.Offset);
                        for (int j = 0; j < Sequences[i].sprites.Count; j++)
                            Sprites[i][j] = reader.Read<BitmapTagGroup.Sequence.Sprite>();
                    }

                    //Loop
                    entry.TagData.Seek(Header.bitmaps.Offset, SeekOrigin.Begin);
                    for (int i = 0; i < Header.bitmaps.Count; i++)
                        Bitmaps[i] = reader.Read<BitmapTagGroup.Bitmap>();
                }
            }
            /// <summary>
            /// Writes the <see cref="BitmapTag"/> to the supplied stream.
            /// </summary>
            /// <param name="outStream">The stream to write the bitmap tag group to.</param>
            /// <exception cref="ArgumentNullException"><paramref name="outStream"/> is null.</exception>
            /// <exception cref="ArgumentException"><paramref name="outStream"/> does not support seeking.</exception>
            /// <exception cref="IOException">An IO error occured.</exception>
            public void Write(Stream outStream)
            {
                //Check
                if (outStream == null) throw new ArgumentNullException(nameof(outStream));
                if (!outStream.CanWrite) throw new ArgumentException("Stream does not support writing.", nameof(outStream));

                //Create Writer
                using (BinaryWriter writer = new BinaryWriter(outStream))
                {
                    //Write Header
                    writer.Write(Header);

                    //Write Sequences
                    foreach (BitmapTagGroup.Sequence sequence in Sequences)
                        writer.Write(sequence);

                    //Write Sprites
                    for (int i = 0; i < Sequences.Length; i++)
                        foreach (BitmapTagGroup.Sequence.Sprite sprite in Sprites[i])
                            writer.Write(sprite);

                    //Write Bitmaps
                    foreach (BitmapTagGroup.Bitmap bitmap in Bitmaps)
                        writer.Write(bitmap);
                }
            }
        }

        /// <summary>
        /// Represents a Halo 2 Bitmap Tag Group structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct BitmapTagGroup
        {
            public ushort type;
            public ushort format;
            public ushort usage;
            public ushort flags;
            public float detailFadeFactor;
            public float sharpenAmount;
            public float bumpHeight;
            public ushort empty1;
            public ushort empty2;
            public ushort colorPlateWidth;
            public ushort colorPlateHeight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] unused1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] unused2;
            public float blurFilterSize;
            public float alphaBias;
            public ushort mipmapCount;
            public ushort spriteUsage;
            public ushort spriteSpacing;
            public ushort forceFormat;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] unused3;
            public TagBlock sequences;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] unused4;
            public TagBlock bitmaps;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] unused5;

            /// <summary>
            /// Represents a Halo 2 Bitmap Sequence structure
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct Sequence
            {
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
                public char[] name;
                public short firstBitmapIndex;
                public ushort bitmapCount;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
                public byte[] unused1;
                public TagBlock sprites;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
                public byte[] unused2;

                /// <summary>
                /// Represents a Halo 2 Bitmap Sprite structure
                /// </summary>
                [StructLayout(LayoutKind.Sequential)]
                public struct Sprite
                {
                    public short bitmapIndex;
                    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
                    public byte[] unused1;
                    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
                    public byte[] unused2;
                    public float left;
                    public float right;
                    public float top;
                    public float bottom;
                    public float registrationPointX;
                    public float registrationPointY;
                }
            }

            /// <summary>
            /// Represents a Halo 2 Bitmap structure
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct Bitmap
            {
                public TagFourCc signature;
                public ushort width;
                public ushort height;
                public byte depth;
                public byte importFlags;
                public ushort type;
                public ushort format;
                public ushort flags;
                public short registrationPointX;
                public short registrationPointY;
                public ushort mipmapCount;
                public ushort lowDetailMipmapCount;
                public int pixelsOffset;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
                public uint[] rawOffsets;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
                public int[] rawLengths;
                public TagId owner;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
                public byte[] unused1;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
                public byte[] unused2;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
                public byte[] unused3;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
                public byte[] unused4;
            }
        }

        /// <summary>
        /// Represents an enumeration containing the import type of the bitmap.
        /// </summary>
        public enum ImportType : ushort
        {
            Texture2D = 0,
            Texture3D = 1,
            CubeMap = 2,
            Spritesheet = 3,
            InterfaceBitmap = 4,
        }

        /// <summary>
        /// Represents an enumeration containing the import format of the bitmap.
        /// </summary>
        public enum ImportFormat : ushort
        {
            CompressedWithColorKeyTransparency = 0,
            CompressedWithExplicitAlpha = 1,
            CompressedWithInterpolatedAlpha = 2,
            Colors16bpp = 3,
            Colors32bpp = 4,
            Monochrome = 5,
        }

        /// <summary>
        /// Represents an enumeration containing the import usage of the bitmap.
        /// </summary>
        public enum ImportUsage : ushort
        {
            AlphaBlend = 0,
            Default = 1,
            HeightMap = 2,
            DetailMap = 3,
            LightMap = 4,
            VectorMap = 5,
            HeightMapBlue255 = 6,
            Embm = 7,
            HeightMapA8L8 = 8,
            HeightMapG8B8 = 9,
            HeightMapWithAlphaG8B8 = 10
        }

        /// <summary>
        /// Represents an enumeration containing the import flags of the bitmap.
        /// </summary>
        public enum ImportFlags : ushort
        {
            None = 0x0,
            EnableDiffusionDithering = 0x1,
            DisableHeightMapCompression = 0x2,
            UniformSpriteSequences = 0x4,
            FilthySpriteBugFix = 0x8,
            UseSharpBumpFilter = 0x10,
            UseClampedOrMirroredBumpFilter = 0x40,
            InvertDetailFade = 0x80,
            SwapXYVectorComponents = 0x100,
            ConvertFromSigned = 0x200,
            ConvertToSigned = 0x400,
            ImportMipmapChains = 0x800,
            IntentionallyTrueColor = 0x1000
        }

        /// <summary>
        /// Represents an enumeration containing the import bitmap force formats.
        /// </summary>
        public enum ImportForceFormat : ushort
        {
            Default = 0,
            G8B8 = 1,
            DXT1 = 2,
            DXT3 = 3,
            DXT5 = 4,
            AlphaLuminance8 = 5,
            ARGB4444 = 6
        }

        /// <summary>
        /// Represents an enumeration containing the bitmap import flags.
        /// </summary>
        [Flags]
        public enum BitmapImportFlags : byte
        {
            None = 0x0,
            DeleteFromCacheFile = 0x1,
            BitmapCreateAttempted = 0x2,
        }

        /// <summary>
        /// Represents an enumeration containing the bitmap type.
        /// </summary>
        public enum BitmapType : ushort
        {
            Texture2D = 0,
            Texture3D = 1,
            CubeMap = 2
        }

        /// <summary>
        /// Represents an enumeration containing bitmap formats.
        /// </summary>
        public enum BitmapFormat : ushort
        {
            A8 = 0,
            Y8 = 1,
            Ay8 = 2,
            A8y8 = 3,
            R5g6b5 = 6,
            A1r5g5b5 = 8,
            A4r4g4b4 = 9,
            X8r8g8b8 = 10,
            A8r8g8b8 = 11,
            Dxt1 = 14,
            Dxt3 = 15,
            Dxt5 = 16,
            P8Bump = 17,
            P8 = 18,
            Argbfp32 = 19,
            Rgbfp32 = 20,
            Rgbfp16 = 21,
            V8u8 = 22,
            G8b8 = 23,
            [Browsable(false)]
            Null = 65535
        };

        /// <summary>
        /// Represents an enumeration containing the bitmap flags.
        /// </summary>
        [Flags]
        public enum BitmapFlags : ushort
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
            [Browsable(false)]
            Unknown = 0x1000,
        };

        /// <summary>
        /// Represents the location of the raw.
        /// </summary>
        public enum RawLocation : uint
        {
            Local = 0,
            LocalMask = ~SinglePlayerShared,
            Shared = 0x80000000,
            Mainmenu = 0x40000000,
            SinglePlayerShared = 0xC0000000,
        };
    }
}
