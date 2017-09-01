using Abide.AddOnApi.Halo2;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Bitmap_Editor.Halo2.Dialogs;
using Bitmap_Library.DirectDraw;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Bitmap_Editor.Halo2
{
    public partial class TextureEditor : AbideTool
    {
        private HaloBitmap bitmap;

        public TextureEditor()
        {
            InitializeComponent();
        }

        private void BitmapEditor_SelectedEntryChanged(object sender, EventArgs e)
        {
            //Filter bitmaps
            if (SelectedEntry.Root == HaloTags.bitm)
            {
                //Dispose?
                bitmap?.Dispose();
                bitmap = new HaloBitmap(SelectedEntry);
                importProperties.SelectedObject = bitmap;

                //Reset
                lodUpDown.Value = 1;
                bitmapUpDown.Value = 0;
                bitmapUpDown.Maximum = bitmap.BitmapCount;

                //Load
                if (bitmap.BitmapCount > 0) bitmap_Load(0, 0);
            }
        }
        
        private void indexUpDown_ValueChanged(object sender, EventArgs e)
        {
            //Check
            if (bitmap != null) bitmap_Load((int)bitmapUpDown.Value, (int)lodUpDown.Value - 1);
        }

        private void exportToolStripButton_Click(object sender, EventArgs e)
        {
            //Prepare
            string filename = string.Empty;
            bool save = false;

            //Get Filename
            string[] parts = this.bitmap.Entry.Filename.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0) filename = parts[parts.Length - 1];

            //Initialize
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                //Setup
                saveDlg.Filter = "DirectDraw Surface Files (*.dds)|*.dds";
                saveDlg.Title = "Save texture as...";
                saveDlg.FileName = filename;
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    filename = saveDlg.FileName;
                    save = true;
                }
            }

            //Prepare
            HaloBitmap.BitmapProperties bitmap = null;
            int bitmapIndex = (int)bitmapUpDown.Value;

            //Check
            if (save)
            {
                //Get Bitmap
                bitmap = this.bitmap.Bitmaps[bitmapIndex];
                uint width = bitmap.Width, height = bitmap.Height, depth = bitmap.Depth;
                bool swizzled = (bitmap.Flags & HaloBitmap.BitmapFlags.Swizzled) == HaloBitmap.BitmapFlags.Swizzled;
                if ((bitmap.Flags & HaloBitmap.BitmapFlags.PowTwoDimensions) == 0 && bitmap.MipmapCount == 0)
                    width = width + (16 - (width % 16) == 16 ? 0 : 16 - (width % 16));  //Pad width to 16 for some ungodly reason

                //Setup DDS file
                DdsFile file = new DdsFile();
                file.Width = width;
                file.Height = height;
                file.MipmapCount = bitmap.MipmapCount;
                file.DefinitionFlags |= DirectDrawSurfaceDefinitionFlags.PixelFormat | DirectDrawSurfaceDefinitionFlags.Width | DirectDrawSurfaceDefinitionFlags.Height | DirectDrawSurfaceDefinitionFlags.Caps;
                file.Caps |= DirectDrawCaps.Texture;
                file.PitchOrLinearSize = (uint)bitmap_GetLength(bitmap.Format, bitmap.Width, bitmap.Height);
                file.DefinitionFlags |= DirectDrawSurfaceDefinitionFlags.LinearSize;
                if (file.MipmapCount > 0) { file.Caps |= DirectDrawCaps.Mipmap | DirectDrawCaps.Complex; file.DefinitionFlags |= DirectDrawSurfaceDefinitionFlags.MipmapCount; }

                //Setup Pixel Format
                switch (bitmap.Format)
                {
                    case HaloBitmap.BitmapFormat.Dxt1: file.RgbBitCount = 0; file.FourCC = "DXT1"; file.PixelFormatFlags |= DirectDrawPixelFormatFlags.FourCC; break;
                    case HaloBitmap.BitmapFormat.Dxt3: file.RgbBitCount = 0; file.FourCC = "DXT3"; file.PixelFormatFlags |= DirectDrawPixelFormatFlags.FourCC; break;
                    case HaloBitmap.BitmapFormat.Dxt5: file.RgbBitCount = 0; file.FourCC = "DXT5"; file.PixelFormatFlags |= DirectDrawPixelFormatFlags.FourCC; break;
                    case HaloBitmap.BitmapFormat.V8u8: file.RgbBitCount = 16; file.PixelFormatFlags = DirectDrawPixelFormatFlags.VU; break;
                    default: file.RgbBitCount = (uint)bitmapFormat_GetBitCount(bitmap.Format); break;
                }
                uint aMask = 0, rMask = 0, gMask = 0, bMask = 0;
                bitmapFormat_GetBitMask(bitmap.Format, out aMask, out rMask, out bMask, out gMask);
                file.AlphaBitmask = aMask; file.RedBitmask = rMask; file.GreenBitmask = gMask; file.BlueBitmask = bMask;

                //Setup Bits
                switch (bitmap.Format)
                {
                    case HaloBitmap.BitmapFormat.A8: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.AlphaPixels; break;
                    case HaloBitmap.BitmapFormat.Y8: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.Luminance; break;
                    case HaloBitmap.BitmapFormat.Ay8: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.AlphaPixels; break;

                    case HaloBitmap.BitmapFormat.G8b8: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.AlphaLuminance; break;
                    case HaloBitmap.BitmapFormat.A8y8: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.AlphaLuminance; break;
                    case HaloBitmap.BitmapFormat.R5g6b5: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.Rgb; break;
                    case HaloBitmap.BitmapFormat.A4r4g4b4: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.Argb; break;

                    case HaloBitmap.BitmapFormat.X8r8g8b8: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.Rgb; break;
                    case HaloBitmap.BitmapFormat.A8r8g8b8: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.Argb; break;
                }

                //Prepare File Data
                int mapWidth = (int)width, mapHeight = (int)height;
                file.Data = new byte[bitmap.RawLengths[0]];
                using (MemoryStream ds = new MemoryStream(file.Data))
                using (MemoryStream ms = new MemoryStream(bitmap_LoadData(bitmapIndex, 0)))
                using (BinaryWriter writer = new BinaryWriter(ds))
                using (BinaryReader reader = new BinaryReader(ms))
                    for (int i = 0; i <= bitmap.MipmapCount; i++)
                    {
                        byte[] data = reader.ReadBytes(bitmap_GetLength(bitmap.Format, mapWidth, mapHeight));
                        if (data.Length > 0) writer.Write(swizzled ? Swizzler.Swizzle(data, mapWidth, mapHeight, (int)depth, (int)file.RgbBitCount, swizzled) : data);
                        mapWidth /= 2; mapHeight /= 2;
                    }

                //Save
                file.Save(filename);
            }
        }

        private void dumpTexturesToolStripButton_Click(object sender, EventArgs e)
        {
            //Check
            if (bitmap == null) return;

            //Prepare
            string directory = string.Empty;
            bool ok = false;

            //Initialize
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                //Setup
                folderDlg.Description = "Select directory to dump textures.";

                //Show
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    directory = folderDlg.SelectedPath;
                    ok = true;
                }
            }

            //Check
            if (ok)
            {
                //Prepare
                HaloBitmap.BitmapProperties bitmap = null;
                string[] parts = this.bitmap.Entry.Filename.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                string filename = Path.Combine(directory, $"{parts[parts.Length - 1]}_{{0}}.dds");

                //Loop
                for (int b = 0; b < this.bitmap.BitmapCount; b++)
                {
                    //Get Bitmap
                    bitmap = this.bitmap.Bitmaps[b];
                    uint width = bitmap.Width, height = bitmap.Height, depth = bitmap.Depth;
                    bool swizzled = (bitmap.Flags & HaloBitmap.BitmapFlags.Swizzled) == HaloBitmap.BitmapFlags.Swizzled;
                    if ((bitmap.Flags & HaloBitmap.BitmapFlags.PowTwoDimensions) == 0 && bitmap.MipmapCount == 0)
                        width = (uint)Math.Ceiling(width / 16f) * 16;    //Pad width to 16 for some ungodly reason

                    //Setup DDS file
                    DdsFile file = new DdsFile();
                    file.Width = width;
                    file.Height = height;
                    file.MipmapCount = bitmap.MipmapCount;
                    file.DefinitionFlags |= DirectDrawSurfaceDefinitionFlags.PixelFormat | DirectDrawSurfaceDefinitionFlags.Width | DirectDrawSurfaceDefinitionFlags.Height | DirectDrawSurfaceDefinitionFlags.Caps;
                    file.Caps |= DirectDrawCaps.Texture;
                    file.PitchOrLinearSize = (uint)bitmap_GetLength(bitmap.Format, bitmap.Width, bitmap.Height);
                    file.DefinitionFlags |= DirectDrawSurfaceDefinitionFlags.LinearSize;
                    if (file.MipmapCount > 0) { file.Caps |= DirectDrawCaps.Mipmap | DirectDrawCaps.Complex; file.DefinitionFlags |= DirectDrawSurfaceDefinitionFlags.MipmapCount; }

                    //Setup Pixel Format
                    switch (bitmap.Format)
                    {
                        case HaloBitmap.BitmapFormat.Dxt1: file.RgbBitCount = 0; file.FourCC = "DXT1"; file.PixelFormatFlags |= DirectDrawPixelFormatFlags.FourCC; break;
                        case HaloBitmap.BitmapFormat.Dxt3: file.RgbBitCount = 0; file.FourCC = "DXT3"; file.PixelFormatFlags |= DirectDrawPixelFormatFlags.FourCC; break;
                        case HaloBitmap.BitmapFormat.Dxt5: file.RgbBitCount = 0; file.FourCC = "DXT5"; file.PixelFormatFlags |= DirectDrawPixelFormatFlags.FourCC; break;
                        case HaloBitmap.BitmapFormat.V8u8: file.RgbBitCount = 16; file.PixelFormatFlags = DirectDrawPixelFormatFlags.VU; break;
                        default: file.RgbBitCount = (uint)bitmapFormat_GetBitCount(bitmap.Format); break;
                    }
                    uint aMask = 0, rMask = 0, gMask = 0, bMask = 0;
                    bitmapFormat_GetBitMask(bitmap.Format, out aMask, out rMask, out bMask, out gMask);
                    file.AlphaBitmask = aMask; file.RedBitmask = rMask; file.GreenBitmask = gMask; file.BlueBitmask = bMask;

                    //Setup Bits
                    switch (bitmap.Format)
                    {
                        case HaloBitmap.BitmapFormat.A8: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.AlphaPixels; break;
                        case HaloBitmap.BitmapFormat.Y8: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.Luminance; break;
                        case HaloBitmap.BitmapFormat.Ay8: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.AlphaPixels; break;

                        case HaloBitmap.BitmapFormat.G8b8: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.AlphaLuminance; break;
                        case HaloBitmap.BitmapFormat.A8y8: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.AlphaLuminance; break;
                        case HaloBitmap.BitmapFormat.R5g6b5: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.Rgb; break;
                        case HaloBitmap.BitmapFormat.A4r4g4b4: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.Argb; break;

                        case HaloBitmap.BitmapFormat.X8r8g8b8: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.Rgb; break;
                        case HaloBitmap.BitmapFormat.A8r8g8b8: file.PixelFormatFlags |= DirectDrawPixelFormatFlags.Argb; break;
                    }

                    //Prepare File Data
                    int mapWidth = (int)width, mapHeight = (int)height;
                    file.Data = new byte[bitmap.RawLengths[0]];
                    using (MemoryStream ds = new MemoryStream(file.Data))
                    using (MemoryStream ms = new MemoryStream(bitmap_LoadData(b, 0)))
                    using (BinaryWriter writer = new BinaryWriter(ds))
                    using (BinaryReader reader = new BinaryReader(ms))
                        for (int i = 0; i <= bitmap.MipmapCount; i++)
                        {
                            byte[] data = reader.ReadBytes(bitmap_GetLength(bitmap.Format, mapWidth, mapHeight));
                            if (data.Length > 0) writer.Write(swizzled ? Swizzler.Swizzle(data, mapWidth, mapHeight, (int)depth, (int)file.RgbBitCount, swizzled) : data);
                            mapWidth /= 2; mapHeight /= 2;
                        }

                    //Save
                    file.Save(string.Format(filename, b));
                }
            }
        }

        private void importToolStripButton_Click(object sender, EventArgs e)
        {
            //Prepare
            int filterIndex = 0;
            string filename = string.Empty;
            bool open = false;

            //Initialize
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                //Setup
                openDlg.Filter = "DirectDraw Surface Files (*.dds)|*.dds|Image Files (*.bmp;*.png;*.jpg;*.tiff)|*.bmp;*.png;*.jpg;*.tiff";
                openDlg.Title = "Open texture...";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    filterIndex = openDlg.FilterIndex;
                    filename = openDlg.FileName;
                    open = true;
                }
            }

            //Check
            if (open)
            {
                //Get Indices
                int bitmapIndex = (int)bitmapUpDown.Value;

                //Which filter?
                switch (filterIndex)
                {
                    case 1:     //DDS file
                        DdsFile ddsFile = new DdsFile();
                        ddsFile.Load(filename);

                        //Import
                        if (bitmap_ImportDds(ddsFile, bitmapIndex)) tag_Reload();
                        break;

                    case 2:     //Image File
                        using (Image image = Image.FromFile(filename))
                            if (bitmap_ImportImage(image, bitmapIndex)) tag_Reload();
                        break;
                }
            }
        }

        private void TextureEditor_DragEnter(object sender, DragEventArgs e)
        {
            //Check for file
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void TextureEditor_DragDrop(object sender, DragEventArgs e)
        {
            //Prepare
            FileInfo info = null;
            string[] filenames = null;
            bool success = false;
            int bitmapIndex = (int)bitmapUpDown.Value;

            //Get File names
            filenames = e.Data.GetData(DataFormats.FileDrop) as string[];

            //Check
            if (filenames != null && filenames.Length > 0)
            {
                //Get File Info
                info = new FileInfo(filenames[0]);
                if (info.Exists && info.Extension == ".dds")
                {
                    //Load DDS File
                    DdsFile ddsFile = new DdsFile();
                    ddsFile.Load(info.FullName);

                    //Import
                    success |= bitmap_ImportDds(ddsFile, bitmapIndex);
                }
                else if (info.Exists && (info.Extension == ".png" || info.Extension == ".bmp" || info.Extension == ".jpg" || info.Extension == ".tiff"))
                {
                    //Load Image
                    using (Image image = Image.FromFile(info.FullName))
                        success |= bitmap_ImportImage(image, bitmapIndex);
                }
            }

            //Check
            if (success) tag_Reload();
        }

        private void bitmapProperties_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            //Save
            bitmap.Write();
            tag_Reload();
        }

        public void tag_Reload()
        {
            //Load
            if (bitmap != null) bitmap.Dispose();
            bitmap = new HaloBitmap(SelectedEntry);
            importProperties.SelectedObject = bitmap;

            //Load
            bitmap_Load((int)bitmapUpDown.Value, (int)lodUpDown.Value - 1);
        }
        
        private void bitmap_Load(int bitmapIndex, int lodIndex)
        {
            //Check
            if (bitmap[bitmapIndex, lodIndex, 0] == null) return;
            formatLabel.Text = Enum.GetName(typeof(HaloBitmap.BitmapFormat), bitmap.GetFormat(bitmapIndex));
            locationLabel.Text = Enum.GetName(typeof(HaloBitmap.RawLocation), bitmap.Bitmaps[bitmapIndex].RawOffsets[lodIndex] & (uint)~HaloBitmap.RawLocation.LocalMask);

            //Create Bitmap
            Point mapPosition = new Point(bitmap[bitmapIndex, lodIndex, 0].Width, 0);
            Bitmap fullMap = new Bitmap(bitmap[bitmapIndex, lodIndex, 0].Width + (bitmap[bitmapIndex, lodIndex, 0].Width / 2), bitmap[bitmapIndex, lodIndex, 0].Height);
            using (Graphics g = Graphics.FromImage(fullMap))
            {
                //Draw Main
                if (bitmap[bitmapIndex, lodIndex, 0] != null)
                    g.DrawImage(bitmap[bitmapIndex, lodIndex, 0], Point.Empty);

                //Draw mipmaps
                for (int i = 1; i < bitmap.GetMipmapCount(bitmapIndex); i++)
                {
                    if (bitmap[bitmapIndex, lodIndex, i] == null) continue;
                    g.DrawImage(bitmap[bitmapIndex, lodIndex, i], mapPosition);
                    mapPosition.Y += bitmap[bitmapIndex, lodIndex, i].Height;
                }
            }

            //Set Bitmap Properties
            bitmapProperties.SelectedObject = bitmap.Bitmaps[bitmapIndex];

            //Set
            if (bitmapBox.Image != null)
                bitmapBox.Image.Dispose();
            bitmapBox.Image = fullMap;
            bitmapBox.Refresh();
        }

        private bool bitmap_ImportImage(Image image, int bitmapIndex)
        {
            //Check
            if (bitmapIndex >= bitmap.BitmapCount) return false;

            //Prepare
            bool deleteLods, swizzle, linear;

            //Create Data Buffer
            byte[] buffer = image_CreateBuffer(image, out linear, out swizzle, out deleteLods);
            if (buffer == null) return false;

            //Check
            if (deleteLods)
                for (int i = 0; i < 6; i++)
                    if (bitmap.Bitmaps[bitmapIndex].RawOffsets[i] != uint.MaxValue)
                    {
                        SelectedEntry.Raws[RawSection.Bitmap].Delete((int)bitmap.Bitmaps[bitmapIndex].RawOffsets[i]);
                        bitmap.Bitmaps[bitmapIndex].RawOffsets[i] = uint.MaxValue;
                        bitmap.Bitmaps[bitmapIndex].RawLengths[i] = 0;
                    }

            //Setup Bitmap Header
            bitmap.MipmapCount = 0;
            bitmap.ColorPlateWidth = (ushort)image.Width;
            bitmap.ColorPlateHeight = (ushort)image.Height;
            bitmap.Format = HaloBitmap.ImportFormat.Colors32bpp;

            //Setup Flags
            HaloBitmap.BitmapFlags flags = HaloBitmap.BitmapFlags.AlwaysOn;
            if (!linear && Math.Log(image.Width, 2) % 1 == 0 && Math.Log(image.Height, 2) % 1 == 0) flags |= HaloBitmap.BitmapFlags.PowTwoDimensions;
            if (linear) { flags |= HaloBitmap.BitmapFlags.Linear | HaloBitmap.BitmapFlags.PeferStutter; }
            if (swizzle) flags |= HaloBitmap.BitmapFlags.Swizzled;

            //Setup Bitmap
            bitmap.Bitmaps[bitmapIndex].Format = HaloBitmap.BitmapFormat.A8r8g8b8;
            bitmap.Bitmaps[bitmapIndex].Width = (ushort)image.Width;
            bitmap.Bitmaps[bitmapIndex].Height = (ushort)image.Height;
            bitmap.Bitmaps[bitmapIndex].MipmapCount = 0;
            bitmap.Bitmaps[bitmapIndex].Flags = flags;

            //Get Raw Offset
            int offset = (int)bitmap.Bitmaps[bitmapIndex].RawOffsets[0];

            //Check if raw exists, if so swap the buffer.
            if (bitmap.Entry.Raws[RawSection.Bitmap].ContainsRawOffset(offset) && offset != -1)
                bitmap.Entry.Raws[RawSection.Bitmap].SwapBuffer(offset, buffer);
            else
            {
                //Make a raw offset
                int rawOffset = (int)(bitmap.Bitmaps[bitmapIndex].Address + 28 - SelectedEntry.TagData.MemoryAddress);

                //Add Raw?
                if (bitmap.Entry.Raws[RawSection.Bitmap].Add(new RawStream(buffer, rawOffset)))
                {
                    bitmap.Bitmaps[bitmapIndex].RawOffsets[0] = (uint)rawOffset;
                    SelectedEntry.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(bitmap.Bitmaps[bitmapIndex].Address + 28);
                    SelectedEntry.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(bitmap.Bitmaps[bitmapIndex].Address + 52);
                }
            }

            //Set Length
            bitmap.Bitmaps[bitmapIndex].RawLengths[0] = buffer.Length;

            //Save
            bitmap.Write();

            //Return
            return true;
        }

        private bool bitmap_ImportDds(DdsFile ddsFile, int bitmapIndex)
        {
            //Check
            if (bitmapIndex >= bitmap.BitmapCount) return false;

            //Prepare
            bool deleteLods, swizzle, linear;
            HaloBitmap.BitmapFormat format;
            int lodLevels = 0;

            //Get existing properties
            for (int i = 0; i < 6; i++) if (bitmap.Bitmaps[bitmapIndex].RawOffsets[i] != uint.MaxValue) lodLevels += 1;
            format = bitmap.Bitmaps[bitmapIndex].Format;

            //Create Data Buffers
            byte[][] buffers = ddsFile_CreateBuffer(ddsFile, out format, ref lodLevels, out linear, out swizzle, out deleteLods);
            if (buffers == null) return false;

            //Check
            if (deleteLods)
                for (int i = 0; i < 6; i++)
                    if (bitmap.Bitmaps[bitmapIndex].RawOffsets[i] != uint.MaxValue)
                    {
                        SelectedEntry.Raws[RawSection.Bitmap].Delete((int)bitmap.Bitmaps[bitmapIndex].RawOffsets[i]);
                        bitmap.Bitmaps[bitmapIndex].RawOffsets[i] = uint.MaxValue;
                        bitmap.Bitmaps[bitmapIndex].RawLengths[i] = 0;
                    }

            //Setup Bitmap Header
            bitmap.ColorPlateWidth = (ushort)ddsFile.Width;
            bitmap.ColorPlateHeight = (ushort)ddsFile.Height;
            bitmap.MipmapCount = (ushort)ddsFile.MipmapCount;

            //Get Import Format
            if (ddsFile.PixelFormatFlags.HasFlag(DirectDrawPixelFormatFlags.FourCC))
            {
                switch (ddsFile.FourCC)
                {
                    case "DXT1": bitmap.Format = HaloBitmap.ImportFormat.CompressedWithColorKeyTransparency; break;
                    case "DXT2":
                    case "DXT3": bitmap.Format = HaloBitmap.ImportFormat.CompressedWithExplicitAlpha; break;
                    case "DXT4":
                    case "DXT5": bitmap.Format = HaloBitmap.ImportFormat.CompressedWithInterpolatedAlpha; break;
                }
                switch (ddsFile.DwordFourCC)
                {
                    case 116: bitmap.Format = HaloBitmap.ImportFormat.Colors32bpp; break;
                }
            }
            else
                switch (ddsFile.RgbBitCount)
                {
                    case 8: bitmap.Format = HaloBitmap.ImportFormat.Monochrome; break;
                    case 16: bitmap.Format = HaloBitmap.ImportFormat.Colors16bpp; break;
                    case 32: bitmap.Format = HaloBitmap.ImportFormat.Colors32bpp; break;
                }

            //Setup Flags
            HaloBitmap.BitmapFlags flags = HaloBitmap.BitmapFlags.AlwaysOn;
            if (!linear && Math.Log(ddsFile.Width, 2) % 1 == 0 && Math.Log(ddsFile.Height, 2) % 1 == 0) flags |= HaloBitmap.BitmapFlags.PowTwoDimensions;
            if (linear) { flags |= HaloBitmap.BitmapFlags.Linear | HaloBitmap.BitmapFlags.PeferStutter; }
            if (swizzle) flags |= HaloBitmap.BitmapFlags.Swizzled;

            //Check for compression
            if (ddsFile.PixelFormatFlags.HasFlag(DirectDrawPixelFormatFlags.FourCC))
                switch (ddsFile.FourCC)
                {
                    case "DXT1":
                    case "DXT2":
                    case "DXT3":
                    case "DXT4":
                    case "DXT5": flags |= HaloBitmap.BitmapFlags.Compressed; break;
                }

            //Setup Bitmap
            bitmap.Bitmaps[bitmapIndex].Format = format;
            bitmap.Bitmaps[bitmapIndex].Width = (ushort)ddsFile.Width;
            bitmap.Bitmaps[bitmapIndex].Height = (ushort)ddsFile.Height;
            bitmap.Bitmaps[bitmapIndex].MipmapCount = (ushort)ddsFile.MipmapCount;
            bitmap.Bitmaps[bitmapIndex].Flags = flags;

            //Check
            for (int l = 0; l < lodLevels; l++)
            {
                //Get Raw Offset
                int offset = (int)bitmap.Bitmaps[bitmapIndex].RawOffsets[l];

                //Check if raw exists, if so swap the buffer.
                if (SelectedEntry.Raws[RawSection.Bitmap].ContainsRawOffset(offset) && offset != -1)
                    SelectedEntry.Raws[RawSection.Bitmap].SwapBuffer(offset, buffers[l]);
                else
                {
                    //Make a raw offset
                    int rawOffset = (int)(bitmap.Bitmaps[bitmapIndex].Address + 28 + (l * 4) - SelectedEntry.TagData.MemoryAddress);

                    //Add Raw?
                    if (SelectedEntry.Raws[RawSection.Bitmap].Add(new RawStream(buffers[l], rawOffset)))
                    {
                        bitmap.Bitmaps[bitmapIndex].RawOffsets[l] = (uint)rawOffset;
                        SelectedEntry.Raws[RawSection.Bitmap][rawOffset].OffsetAddresses.Add(bitmap.Bitmaps[bitmapIndex].Address + 28 + (l * 4));
                        SelectedEntry.Raws[RawSection.Bitmap][rawOffset].LengthAddresses.Add(bitmap.Bitmaps[bitmapIndex].Address + 52 + (l * 4));
                    }
                }

                //Set Length
                bitmap.Bitmaps[bitmapIndex].RawLengths[l] = buffers[l].Length;
            }

            //Save
            bitmap.Write();

            //Return
            return true;
        }

        private byte[] bitmap_LoadData(int bitmapIndex, int lodIndex)
        {
            //Get source data
            byte[] sourceData = null; RawLocation rawLocation = (RawLocation)(bitmap.Bitmaps[bitmapIndex].RawOffsets[lodIndex] & 0xC0000000);
            if (rawLocation == RawLocation.Local) sourceData = SelectedEntry.Raws[RawSection.Bitmap][(int)bitmap.Bitmaps[bitmapIndex].RawOffsets[lodIndex]].GetBuffer();
            else
            {
                string filelocation = string.Empty;
                int rawOffset = (int)(bitmap.Bitmaps[bitmapIndex].RawOffsets[lodIndex] & (uint)RawLocation.LocalMask);
                switch (rawLocation)
                {
                    case RawLocation.Mainmenu:
                        filelocation = HaloSettings.MainmenuPath;
                        break;
                    case RawLocation.Shared:
                        filelocation = HaloSettings.SharedPath;
                        break;
                    case RawLocation.SinglePlayerShared:
                        filelocation = HaloSettings.SingleplayerSharedPath;
                        break;
                }

                //Check
                if (File.Exists(filelocation))
                    using (FileStream fs = new FileStream(filelocation, FileMode.Open))
                    using (BinaryReader mapReader = new BinaryReader(fs))
                    {
                        fs.Seek(rawOffset, SeekOrigin.Begin);
                        sourceData = mapReader.ReadBytes(bitmap.Bitmaps[bitmapIndex].RawLengths[lodIndex]);
                    }
            }

            //Return
            return sourceData;
        }

        private int bitmap_GetLength(HaloBitmap.BitmapFormat format, int width, int height)
        {
            //Return
            int bpp = bitmapFormat_GetBitCount(format);
            int length = width * height * bpp / 8;
            switch (format)
            {
                case HaloBitmap.BitmapFormat.Dxt1: return Math.Max(length, 8);
                case HaloBitmap.BitmapFormat.Dxt3:
                case HaloBitmap.BitmapFormat.Dxt5: return Math.Max(length, 16);
                default: return Math.Max(length, 1);
            }
        }

        private int bitmapFormat_GetBitCount(HaloBitmap.BitmapFormat format)
        {
            //Handle Bitmap Format
            switch (format)
            {
                case HaloBitmap.BitmapFormat.P8Bump:
                case HaloBitmap.BitmapFormat.P8:
                case HaloBitmap.BitmapFormat.A8:
                case HaloBitmap.BitmapFormat.Y8:
                case HaloBitmap.BitmapFormat.Ay8: return 8;

                case HaloBitmap.BitmapFormat.V8u8:
                case HaloBitmap.BitmapFormat.G8b8:
                case HaloBitmap.BitmapFormat.A8y8:
                case HaloBitmap.BitmapFormat.R5g6b5:
                case HaloBitmap.BitmapFormat.A4r4g4b4: return 16;

                case HaloBitmap.BitmapFormat.X8r8g8b8:
                case HaloBitmap.BitmapFormat.A8r8g8b8: return 32;

                case HaloBitmap.BitmapFormat.Dxt1: return 4;

                case HaloBitmap.BitmapFormat.Dxt3:
                case HaloBitmap.BitmapFormat.Dxt5: return 8;

                case HaloBitmap.BitmapFormat.Argbfp32: return 128;
                case HaloBitmap.BitmapFormat.Rgbfp32: return 96;
                case HaloBitmap.BitmapFormat.Rgbfp16: return 48;
                default: return 0;
            }
        }

        private void bitmapFormat_GetBitMask(HaloBitmap.BitmapFormat format, out uint alpha, out uint red, out uint blue, out uint green)
        {
            //Prepare
            uint eightMask = byte.MaxValue;
            uint fourMask = 15u;
            uint fiveMask = 31u;
            uint sixMask = 63u;

            //Setup
            alpha = 0; red = 0; blue = 0; green = 0;

            //Handle Bitmap Format
            switch (format)
            {
                case HaloBitmap.BitmapFormat.A8: alpha = eightMask; break;
                case HaloBitmap.BitmapFormat.Y8: red = eightMask; break;
                case HaloBitmap.BitmapFormat.Ay8: alpha = eightMask; break;

                case HaloBitmap.BitmapFormat.V8u8: green = eightMask << 8; red = eightMask; break;
                case HaloBitmap.BitmapFormat.G8b8: green = eightMask << 8; blue = eightMask; break;
                case HaloBitmap.BitmapFormat.A8y8: alpha = eightMask << 8; red = eightMask; break;
                case HaloBitmap.BitmapFormat.R5g6b5: red = fiveMask << 11; green = sixMask << 5; blue = fiveMask; break;
                case HaloBitmap.BitmapFormat.A4r4g4b4: alpha = fourMask << 12; red = fourMask << 8; green = fourMask << 4; blue = fourMask; break;

                case HaloBitmap.BitmapFormat.X8r8g8b8: red = eightMask << 16; green = eightMask << 8; blue = eightMask; break;
                case HaloBitmap.BitmapFormat.A8r8g8b8: alpha = eightMask << 24; red = eightMask << 16; green = eightMask << 8; blue = eightMask; break;
            }
        }

        private byte[] image_CreateBuffer(Image image, out bool linear, out bool swizzle, out bool deleteLods)
        {
            //Prepare
            linear = false;
            swizzle = false;
            deleteLods = true;
            byte[] buffer = new byte[image.Width * image.Height * 4];

            //Get Color Data
            Color pixel = Color.Black;
            using (Bitmap bitmap = new Bitmap(image))
                for (int y = 0; y < bitmap.Height; y++)
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        //Get Pixel
                        pixel = bitmap.GetPixel(x, y);
                        int a = y * bitmap.Width + x;

                        //Copy into buffer
                        buffer[a * 4] = pixel.B;
                        buffer[a * 4 + 1] = pixel.G;
                        buffer[a * 4 + 2] = pixel.R;
                        buffer[a * 4 + 3] = pixel.A;
                    }

            //Confirm
            using (TextureImportOptionsDialog texDlg = new TextureImportOptionsDialog())
            {
                //Setup
                texDlg.OriginalHeight = image.Height;
                texDlg.OriginalWidth = image.Width;
                texDlg.MaxLodLevels = 1;
                texDlg.LodLevels = 1;
                texDlg.Format = HaloBitmap.BitmapFormat.A8r8g8b8;

                //Show
                if (texDlg.ShowDialog(this) == DialogResult.OK)
                {
                    deleteLods = texDlg.DeleteLods;
                    swizzle = texDlg.Swizzle;
                }
                else return null;
            }

            //Get Linear
            linear = !swizzle;

            //Check
            if (swizzle) buffer = Swizzler.Swizzle(buffer, image.Width, image.Height, 1, 32, false);

            //Return
            return buffer;
        }

        private byte[][] ddsFile_CreateBuffer(DdsFile ddsFile, out HaloBitmap.BitmapFormat format, ref int lodLevels, out bool linear, out bool swizzle, out bool deleteLods)
        {
            //Prepare
            format = HaloBitmap.BitmapFormat.Null;
            deleteLods = true;
            swizzle = false;
            linear = false;

            //Get Format
            if (ddsFile.PixelFormatFlags.HasFlag(DirectDrawPixelFormatFlags.FourCC))    //Check for FourCC flag...
            {
                switch (ddsFile.FourCC)         //Check FourCC string
                {
                    case "DXT1": format = HaloBitmap.BitmapFormat.Dxt1; break;
                    case "DXT2":
                    case "DXT3": format = HaloBitmap.BitmapFormat.Dxt3; break;
                    case "DXT4":
                    case "DXT5": format = HaloBitmap.BitmapFormat.Dxt5; break;
                    case "ATI2": format = HaloBitmap.BitmapFormat.P8Bump; break;
                }
                switch (ddsFile.DwordFourCC)    //Check FourCC int
                {
                    case 116: format = HaloBitmap.BitmapFormat.Argbfp32; break;
                }
            }
            else
            {
                if (ddsFile.PixelFormatFlags.HasFlag(DirectDrawPixelFormatFlags.Argb))                      //ARGB
                    switch (ddsFile.RgbBitCount)
                    {
                        case 16:                                                                            //16bpp
                            if (ddsFile.AlphaBitmask == 0x8000) format = HaloBitmap.BitmapFormat.A1r5g5b5; //A1R5G5B5
                            else format = HaloBitmap.BitmapFormat.A4r4g4b4;                                //A4R4G4B4
                            break;
                        case 32: format = HaloBitmap.BitmapFormat.A8r8g8b8; break;                         //A8R8G8B8
                    }
                else if (ddsFile.PixelFormatFlags.HasFlag(DirectDrawPixelFormatFlags.Rgb))                  //RGB
                    switch (ddsFile.RgbBitCount)
                    {
                        case 16: format = HaloBitmap.BitmapFormat.R5g6b5; break;                           //R5G6B5
                        case 32: format = HaloBitmap.BitmapFormat.X8r8g8b8; break;                         //X8R8G8B8
                    }
                else if (ddsFile.PixelFormatFlags.HasFlag(DirectDrawPixelFormatFlags.AlphaLuminance))       //AL
                    switch (ddsFile.RgbBitCount)
                    {
                        case 16: format = HaloBitmap.BitmapFormat.A8y8; break;                             //A8L8
                    }
                else if (ddsFile.PixelFormatFlags.HasFlag(DirectDrawPixelFormatFlags.Luminance))            //L
                    switch (ddsFile.RgbBitCount)
                    {
                        case 8: format = HaloBitmap.BitmapFormat.Y8; break;                                //L8
                    }
                else if (ddsFile.PixelFormatFlags.HasFlag(DirectDrawPixelFormatFlags.Argb))                 //A
                    switch (ddsFile.RgbBitCount)
                    {
                        case 8: format = HaloBitmap.BitmapFormat.A8; break;                                //A8
                    }
                else if (ddsFile.PixelFormatFlags.HasFlag(DirectDrawPixelFormatFlags.VU))                   //VU
                    switch (ddsFile.RgbBitCount)
                    {
                        case 16: format = HaloBitmap.BitmapFormat.V8u8; break;                             //V8U8
                    }
            }

            //Check
            if (format == HaloBitmap.BitmapFormat.Null)
                switch (ddsFile.RgbBitCount)
                {
                    case 8: format = HaloBitmap.BitmapFormat.Y8; break;
                    case 16: format = HaloBitmap.BitmapFormat.A8y8; break;
                    case 32: format = HaloBitmap.BitmapFormat.A8r8g8b8; break;
                    default: throw new FormatException("DirectDraw Surface texture format is not supported.");
                }

            //Confirm
            using (TextureImportOptionsDialog texDlg = new TextureImportOptionsDialog())
            {
                //Setup
                int mapCount = 1 + (int)ddsFile.MipmapCount;
                texDlg.OriginalHeight = (int)ddsFile.Height;
                texDlg.OriginalWidth = (int)ddsFile.Width;
                texDlg.MaxLodLevels = mapCount;
                texDlg.LodLevels = lodLevels;
                texDlg.Format = format;

                //Show
                if (texDlg.ShowDialog(this) == DialogResult.OK)
                {
                    lodLevels = texDlg.LodLevels;
                    format = texDlg.Format;
                    deleteLods = texDlg.DeleteLods;
                    swizzle = texDlg.Swizzle;
                }
                else return null;
            }

            //Prepare buffers
            byte[][] buffers = new byte[lodLevels][];

            //Get BPP
            uint bitsPerPixel = 0;
            if (ddsFile.DefinitionFlags.HasFlag(DirectDrawSurfaceDefinitionFlags.LinearSize))
                bitsPerPixel = ddsFile.PitchOrLinearSize * 8u / ddsFile.Width / ddsFile.Height;
            else bitsPerPixel = ddsFile.RgbBitCount;

            //Get Data Length
            long length = 0; uint mipWidth = ddsFile.Width, mipHeight = ddsFile.Height;
            for (int j = 0; j <= ddsFile.MipmapCount; j++)
            {
                length += mipWidth * mipHeight * bitsPerPixel / 8;
                mipWidth /= 2; mipHeight /= 2;
            }

            //Get Linear?
            if (ddsFile.RgbBitCount > 0)
                linear = !swizzle;

            //Get Data Lengths
            uint width = ddsFile.Width, height = ddsFile.Height; int position = 0;
            for (int i = 0; i < lodLevels; i++)
            {
                //Prepare
                int lodLength = 0; mipWidth = width; mipHeight = height;

                //Loop
                for (int j = i; j <= ddsFile.MipmapCount; j++)
                {
                    lodLength += (int)(mipWidth * mipHeight * bitsPerPixel / 8);
                    mipWidth /= 2; mipHeight /= 2;
                }

                //Prepare
                int mipPosition = 0;
                buffers[i] = new byte[lodLength]; mipWidth = width; mipHeight = height;
                for (int j = i; j <= ddsFile.MipmapCount; j++)
                {
                    //Get Mip Data
                    byte[] data = new byte[mipWidth * mipHeight * bitsPerPixel / 8];
                    Array.Copy(ddsFile.Data, mipPosition + position, data, 0, data.Length);

                    //Swizzle?
                    if (swizzle) data = Swizzler.Swizzle(data, (int)mipWidth, (int)mipHeight, 1, (int)bitsPerPixel, !swizzle);

                    //Copy
                    Array.Copy(data, 0, buffers[i], mipPosition, data.Length);

                    //Increment
                    mipPosition += (int)(mipWidth * mipHeight * bitsPerPixel / 8);
                    mipWidth /= 2; mipHeight /= 2;
                }

                //Increment
                position += (int)(width * height * bitsPerPixel / 8);

                //Get next LOD
                width /= 2; height /= 2;
            }

            //Return
            return buffers;
        }

        [Flags]
        private enum BitmapFlags : ushort
        {
            None = 0x0,
            PowTwoDimensions = 0x01,
            Compressed = 0x02,
            Palettized = 0x04,
            Swizzled = 0x08,
            Linear = 0x10,
            v16u16 = 0x20,
            HudBitmap = 0x40,
            AlwaysOn = 0x80,
            Interlaced = 0x100,
        };


        private enum RawLocation : uint
        {
            Local = 0,
            LocalMask = ~SinglePlayerShared,
            Shared = 0x80000000,
            Mainmenu = 0x40000000,
            SinglePlayerShared = Shared | Mainmenu,
        };
    }
}
