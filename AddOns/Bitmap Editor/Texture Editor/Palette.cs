using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Texture_Editor
{
    /// <summary>
    /// Provides functions and tools for bitmap palette manipulation.
    /// </summary>
    internal static class Palette
    {
        private readonly static Color[] normalMapPalette;
        private readonly static Dictionary<Color, byte> normalMapPaletteLookup;

        public static byte[] InterpolateBitmap(this Bitmap bitmap, Color[] palette)
        {
            //Check...
            if (palette.Length > byte.MaxValue + 1)
                throw new ArgumentException("Color palette contains too many colors.", nameof(palette));
            else if (bitmap == null) throw new ArgumentNullException(nameof(bitmap));
            else if (palette == null) throw new ArgumentNullException(nameof(palette));
            else if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ArgumentException("Only 32bpp bitmaps are supported.", nameof(bitmap));

            //Read
            byte[] pixels = new byte[bitmap.Width * bitmap.Height];
            unsafe
            {
                Color pixel = Color.Transparent;
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                byte[] pixelBuffer = new byte[data.Stride * data.Height];
                Marshal.Copy(data.Scan0, pixelBuffer, 0, pixelBuffer.Length);
                using (MemoryStream ms = new MemoryStream(pixelBuffer))
                using (BinaryReader reader = new BinaryReader(ms))
                    for (int i = 0; i < data.Stride / data.Width; i++)
                    {
                        pixel = Color.FromArgb(reader.ReadInt32());
                        var distances = palette.Select((x, j) => new { Index = (byte)j, Distance = GetDistance(x, pixel) }).ToList();
                        var min = distances.Min(x => x.Distance);
                        pixels[i] = distances.Find(x => x.Distance == min).Index;
                    }
            }

            return pixels;
        }
        public static void SetGrayscalePalette(this Bitmap bitmap)
        {
            //Get Palette
            ColorPalette palette = bitmap.Palette;

            //Check
            if (palette.Entries.Length == 256)
                for (int i = 0; i < 256; i++)
                    palette.Entries[i] = Color.FromArgb(255, i, i, i);

            //Set
            bitmap.Palette = palette;
        }
        public static void SetNormalMapPalette(this Bitmap bitmap)
        {
            //Get Palette
            ColorPalette palette = bitmap.Palette;

            //Check
            if (palette.Entries.Length == 256)
                for (int i = 0; i < 256; i++)
                    palette.Entries[i] = normalMapPalette[i];

            //Set
            bitmap.Palette = palette;
        }
        public static Color[] GetNormalMapPalette()
        {
            return (Color[])normalMapPalette.Clone();
        }
        public static byte[] GetNormalMapPaletteBuffer()
        {
            byte[] buffer = new byte[(32 * normalMapPalette.Length) / 8];
            using (MemoryStream ms = new MemoryStream(buffer))
            using (BinaryWriter writer = new BinaryWriter(ms))
                foreach (Color c in normalMapPalette)
                    writer.Write(c.ToArgb());

            return buffer;
        }

        private static double GetDistance(Color color1, Color color2)
        {
            int a = color1.A - color2.A, r = color1.R - color2.R, g = color1.G - color2.G, b = color1.B - color2.B;
            return Math.Sqrt(a * a + r * r + b * b + g * g + b * b);
        }

        static Palette()
        {
            //Setup Palette
            #region Normal Map Palette
            normalMapPalette = new Color[] { Color.FromArgb(255, 126, 126, 255),
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
            Color.FromArgb(0, 0, 0, 0) };
            #endregion

            //Create Lookup
            normalMapPaletteLookup = new Dictionary<Color, byte>();
            for (int i = 0; i < 256; i++)
                normalMapPaletteLookup.Add(normalMapPalette[i], (byte)i);
        }
    }
}
