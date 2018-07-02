using System;
using System.Drawing;
using System.IO;

namespace Bitmap_Library.Bitmap
{
    public class BitmapFile
    {
        public const int FileHeaderSize = 14;
        public const int InfoHeaderSize = 40;
        private BITMAPFILEHEADER fileHeader;
        private BITMAPINFOHEADER infoHeader;
        private byte[] raw;

        public BITMAPFILEHEADER FileHeader
        {
            get { return fileHeader; }
        }
        public BITMAPINFOHEADER InfoHeader
        {
            get { return infoHeader; }
        }
        public Color[] Pixels
        {
            get
            {
                Color[] pix = new Color[this.InfoHeader.biWidth * this.InfoHeader.biHeight * (int)(this.InfoHeader.biBitCount / 8)];
                for (int i = 0; i < pix.Length; i++)
                {
                    ushort biBitCount = this.InfoHeader.biBitCount;
                    if (biBitCount != 24)
                    {
                        if (biBitCount != 32)
                        {
                            string arg_DD_0 = "Only 24bpp and 32bpp Bitmap files are supported by this operation.\r\nAttempted BitCount: ";
                            ushort biBitCount2 = this.InfoHeader.biBitCount;
                            throw new NotImplementedException(arg_DD_0 + biBitCount2.ToString());
                        }
                        pix[i] = Color.FromArgb((int)this.raw[i * 4], (int)this.raw[i * 4 + 1], (int)this.raw[i * 4 + 2], (int)this.raw[i * 4 + 3]);
                    }
                    else
                    {
                        pix[i] = Color.FromArgb(255, (int)this.raw[i * 3], (int)this.raw[i * 3 + 1], (int)this.raw[i * 3 + 2]);
                    }
                }
                return pix;
            }
        }
        public byte[] Image
        {
            get { return raw; }
        }

        public BitmapFile()
        {
        }
        public BitmapFile(string FileLocation)
        {
            try
            {
                //Load Headers...
                fileHeader = LoadFileHeader(FileLocation);
                infoHeader = LoadInfoHeader(FileLocation);

                //Read Raw data
                using (Stream BitmapFileStream = new FileStream(FileLocation, FileMode.Open))
                using (BinaryReader Reader = new BinaryReader(BitmapFileStream))
                {
                    BitmapFileStream.Position = 54L;
                    raw = Reader.ReadBytes((int)infoHeader.biSizeImage);
                }
            }
            catch (IOException)
            {
                throw new IOException("Unable to read Bitmap File. File might be corrupt, or being used in another location");
            }
        }
        private static BITMAPFILEHEADER LoadFileHeader(string FileLocation)
        {
            BITMAPFILEHEADER Structure;
            using (Stream BitmapFileStream = new FileStream(FileLocation, FileMode.Open))
            using (BinaryReader Reader = new BinaryReader(BitmapFileStream))
            {
                Reader.BaseStream.Position = 0L;
                Structure = new BITMAPFILEHEADER();
                Structure.bfType = Reader.ReadUInt16();
                Structure.bfSize = Reader.ReadUInt32();
                Structure.bfReserved1 = Reader.ReadUInt16();
                Structure.bfReserved2 = Reader.ReadUInt16();
                Structure.bfOffBits = Reader.ReadUInt32();
            }

            return Structure;
        }
        private static BITMAPINFOHEADER LoadInfoHeader(string FileLocation)
        {
            BITMAPINFOHEADER Structure;
            using (Stream BitmapFileStream = new FileStream(FileLocation, FileMode.Open))
            using (BinaryReader Reader = new BinaryReader(BitmapFileStream))
            {
                Reader.BaseStream.Position = 14L;
                Structure = new BITMAPINFOHEADER();
                Structure.biSize = Reader.ReadUInt32();
                Structure.biWidth = Reader.ReadInt32();
                Structure.biHeight = Reader.ReadInt32();
                Structure.biPlanes = Reader.ReadUInt16();
                Structure.biBitCount = Reader.ReadUInt16();
                Structure.biCompression = Reader.ReadUInt32();
                Structure.biSizeImage = Reader.ReadUInt32();
                Structure.biXPelsPerMeter = Reader.ReadInt32();
                Structure.biYPelsPerMeter = Reader.ReadInt32();
                Structure.biClrUsed = Reader.ReadUInt32();
                Structure.biClrImportant = Reader.ReadUInt32();
            }

            return Structure;
        }
    }
}
