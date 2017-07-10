using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Bitmap_Library.DirectDraw
{
    /// <summary>
    /// Represents a DirectDraw Surface file.
    /// </summary>
    public class DdsFile
    {
        /// <summary>
        /// Represents the 'DDS ' FourCC.
        /// </summary>
        private const int DdsFourCC = 542327876;
        
        public DirectDrawSurfaceDefinitionFlags DefinitionFlags
        {
            get { return header.Flags; }
            set { header.Flags = value; }
        }
        public int HeaderSize
        {
            get { return header.Size;}
            set { header.Size = value; }
        }
        public int PixelFormatSize
        {
            get { return header.PixelFormatSize; }
            set { header.PixelFormatSize = value; }
        }
        public uint Height
        {
            get { return header.Height; }
            set { header.Height = value; }
        }
        public uint Width
        {
            get { return header.Width; }
            set { header.Width = value; }
        }
        public uint PitchOrLinearSize
        {
            get { return header.PitchOrLinearSize; }
            set { header.PitchOrLinearSize = value; }
        }
        public uint Depth
        {
            get { return header.Depth; }
            set { header.Depth = value; }
        }
        public uint MipmapCount
        {
            get { return header.MipmapCount; }
            set { header.MipmapCount = value; }
        }
        public DirectDrawCaps Caps
        {
            get { return header.Caps; }
            set { header.Caps = value; }
        }
        public DirectDrawCapsTwo CapsTwo
        {
            get { return header.CapsTwo; }
            set { header.CapsTwo = value; }
        }
        public DirectDrawPixelFormatFlags PixelFormatFlags
        {
            get { return header.PixelFormatFlags; }
            set { header.PixelFormatFlags = value; }
        }
        public uint DwordFourCC
        {
            get { return header.DwordFourCC; }
            set { header.DwordFourCC = value; }
        }
        public string FourCC
        {
            get { return header.FourCC; }
            set { header.FourCC = value; }
        }
        public uint RgbBitCount
        {
            get { return header.RgbBitCount; }
            set { header.RgbBitCount = value; }
        }
        public uint RedBitmask
        {
            get { return header.RedBitmask; }
            set { header.RedBitmask = value; }
        }
        public uint BlueBitmask
        {
            get { return header.BlueBitmask; }
            set { header.BlueBitmask = value; }
        }
        public uint GreenBitmask
        {
            get { return header.GreenBitmask; }
            set { header.GreenBitmask = value; }
        }
        public uint AlphaBitmask
        {
            get { return header.AlphaBitmask; }
            set { header.AlphaBitmask = value; }
        }
        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }

        private DdsHeader header;
        private byte[] data;

        /// <summary>
        /// Initializes a new DirectDraw Surface file.
        /// </summary>
        public DdsFile()
        {
            //Prepare
            header = new DdsHeader();

            //Set Sizes
            header.Size = Marshal.SizeOf(typeof(DdsHeader));
            header.PixelFormatSize = Marshal.SizeOf(typeof(DdsPixelFormat));
        }
        /// <summary>
        /// Loads a DirectDraw Surface file from the specified file path.
        /// </summary>
        /// <param name="filename">The file path of the DirectDraw Surface file.</param>
        public void Load(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                Load(fs);
        }
        /// <summary>
        /// Loads a DirectDraw Surface file from the specified stream.
        /// </summary>
        /// <param name="inStream">The stream containing the DirectDraw surface file.</param>
        /// <exception cref="ArgumentException"><paramref name="inStream"/> does not support reading or seeking.</exception>
        public void Load(Stream inStream)
        {
            //Check
            if (!inStream.CanRead) throw new ArgumentException("Stream does not support reading.", nameof(inStream));
            if (!inStream.CanSeek) throw new ArgumentException("Stream does not support seeking.", nameof(inStream));

            //Check
            if (inStream.Length > 4)
                using (BinaryReader reader = new BinaryReader(inStream))
                {
                    //Read FourCC
                    if (reader.ReadInt32() == DdsFourCC)
                    {
                        //Read Header
                        header = reader.ReadStructure<DdsHeader>();
                        uint bitsPerPixel = 0;

                        //Calculate Data length(s)
                        if ((header.Flags & DirectDrawSurfaceDefinitionFlags.LinearSize) == DirectDrawSurfaceDefinitionFlags.LinearSize)
                            bitsPerPixel = header.PitchOrLinearSize * 8u / header.Width / header.Height;
                        if (bitsPerPixel == 0) throw new InvalidOperationException();

                        //Read Data
                        data = reader.ReadBytes((int)(inStream.Length - 128));
                    }
                }
        }
        /// <summary>
        /// Writes a DirectDraw Surface file to the specified file path.
        /// </summary>
        /// <param name="filename">The file path to write the DirectDraw Surface to.</param>
        public void Save(string filename)
        {
            //Create
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                Save(fs);
        }
        /// <summary>
        /// Writes a DirectDraw Surface file to the specified stream.
        /// </summary>
        /// <param name="outStream">The stream to write the DirectDraw Surface to.</param>
        public void Save(Stream outStream)
        {
            //Check
            if (outStream == null) throw new ArgumentNullException(nameof(outStream));
            if (!outStream.CanWrite) throw new ArgumentException("Stream does not support writing.", nameof(outStream));

            //Create Writer
            using(BinaryWriter writer = new BinaryWriter(outStream))
            {
                //Write FourCC
                writer.Write(DdsFourCC);

                //Write DDS Header
                writer.Write(header);

                //Write Data
                writer.Write(data);
            }
        }
    }

    /// <summary>
    /// Represents a single texture in a DirectDraw Surface file.
    /// </summary>
    public class DdsTexture
    {
        public uint Width
        {
            get { return width; }
        }
        public uint Height
        {
            get { return height; }
        }
        public byte[] Data
        {
            get { return (byte[])buffer.Clone(); }
        }
        
        private readonly uint width, height;
        private byte[] buffer;

        /// <summary>
        /// Initializes a new <see cref="DdsTexture"/> using the supplied width, height, and data buffer values.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="buffer"></param>
        public DdsTexture(uint width, uint height, byte[] buffer)
        {
            this.width = width;
            this.height = height;
            this.buffer = buffer;
        }
        /// <summary>
        /// Sets the surface's data buffer to the supplied value. The supplied value's length must match the source data's length.
        /// </summary>
        /// <param name="buffer">The data buffer to set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is null.</exception>
        /// <exception cref="ArgumentException">Length of <paramref name="buffer"/> is not the expected value.</exception>
        public void SetData(byte[] buffer)
        {
            //Check
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));

            //Set
            this.buffer = (byte[])buffer.Clone();
        }
        public override string ToString()
        {
            return $"{width}x{height} Length: {buffer.LongLength}";
        }
    }
}