using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Texture_Editor
{
    /// <summary>
    /// Represents a bitmap sheet containing smaller bitmaps.
    /// </summary>
    public sealed class BitmapSheet : IDisposable
    {
        /// <summary>
        /// Returns the bitmap sheet's spacer color. This color is R=255, G=0, B=255, A=255
        /// This value is read-only.
        /// </summary>
        public static readonly Color Spacer = Color.FromArgb(255, 255, 0, 255);

        /// <summary>
        /// Gets and returns the width of the bitmap sheet.
        /// </summary>
        public int Width
        {
            get { return sheet.Width; }
        }
        /// <summary>
        /// Gets or sets the height of the bitmap sheet.
        /// </summary>
        public int Height
        {
            get { return sheet.Height; }
        }
        /// <summary>
        /// Gets and returns the number of bitmaps found within the sheet.
        /// </summary>
        public int BitmapCount
        {
            get { return bitmaps.Length; }
        }
        
        private readonly Bitmap sheet;
        private readonly Rectangle[] bitmaps;
        private readonly int[] pixelsOffsets;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapSheet"/> class using the supplied image.
        /// </summary>
        /// <param name="sheet">The sheet image.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sheet"/> is null.</exception>
        public BitmapSheet(Bitmap sheet)
        {
            //Check
            if (sheet == null) throw new ArgumentNullException(nameof(sheet));
            if (sheet.PixelFormat != PixelFormat.Format32bppArgb) throw new ArgumentException("Format not supported.");

            //Prepare
            Rectangle bitmapRect = Rectangle.Empty;
            this.sheet = (Bitmap)sheet.Clone();
            List<Rectangle> bitmaps = new List<Rectangle>();
            List<int> pixelsOffsets = new List<int>();
            byte[] buffer = null;

            unsafe
            {
                //Get Data
                BitmapData data = this.sheet.LockBits(new Rectangle(0, 0, this.sheet.Width, this.sheet.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                buffer = new byte[data.Stride * data.Height];
                Marshal.Copy(data.Scan0, buffer, 0, data.Stride * data.Height);
                this.sheet.UnlockBits(data);
            }

            //Check
            if (buffer != null)
            {
                //Loop
                int x = 0, y = 0, a = 0;
                for (int i = 0; i < sheet.Width * sheet.Height; i++)
                {
                    //Setup variables
                    a = i * 4;
                    x = i % sheet.Width;
                    y = i / sheet.Width;

                    //Check pixel
                    if (Color.FromArgb(buffer[a + 3], buffer[a + 2], buffer[a + 1], buffer[a]) != Spacer)
                        if (!sheet_FoundBitmap(x, y, bitmaps.ToArray()))
                        { bitmapRect = sheet_FindBitmap(x, y, sheet.Width, buffer); bitmaps.Add(bitmapRect); pixelsOffsets.Add((bitmapRect.X + bitmapRect.Y * sheet.Width) * 4); }
                }

                //Set
                this.bitmaps = bitmaps.ToArray();
                this.pixelsOffsets = pixelsOffsets.ToArray();
            }
        }
        /// <summary>
        /// Gets a bitmap's rectangle from a given index.
        /// </summary>
        /// <param name="index">The zero-based index of the rectangle.</param>
        /// <returns>A bitmap rectangle.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is outside of valid range.</exception>
        public Rectangle GetBitmapRectangle(int index)
        {
            if (index < 0 || index >= bitmaps.Length) throw new ArgumentOutOfRangeException(nameof(index));
            return bitmaps[index];
        }
        /// <summary>
        /// Gets a bitmap from a given index.
        /// </summary>
        /// <param name="index">The zero-based index of the rectangle.</param>
        /// <returns>A bitmap image.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is outside of valid range.</exception>
        public Image GetBitmap(int index)
        {
            if (index < 0 || index >= bitmaps.Length) throw new ArgumentOutOfRangeException(nameof(index));
            Bitmap bitmap = new Bitmap(bitmaps[index].Width, bitmaps[index].Height, PixelFormat.Format32bppArgb);

            unsafe
            {
                //Lock
                BitmapData srcData = sheet.LockBits(new Rectangle(0, 0, sheet.Width, sheet.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                BitmapData dstData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                byte[] buffer = new byte[bitmap.Width * bitmap.Height * 4];

                //Prepare
                int dstIndex = 0;
                int srcIndex = bitmaps[index].X + (bitmaps[index].Y * sheet.Width);
                for (int i = 0; i < bitmaps[index].Height; i++)
                {
                    Marshal.Copy(srcData.Scan0 + (srcIndex * 4), buffer, dstIndex, dstData.Stride);
                    dstIndex += dstData.Stride; srcIndex += sheet.Width;
                }

                //Unlock
                bitmap.UnlockBits(dstData);
                sheet.UnlockBits(srcData);
            }

            //Return
            return bitmap;
        }
        /// <summary>
        /// Releases all resources used by this <see cref="BitmapSheet"/>.
        /// </summary>
        public void Dispose()
        {
            sheet.Dispose();
        }
        private bool sheet_FoundBitmap(int x, int y, Rectangle[] rects)
        {
            //Check
            if (rects == null) throw new ArgumentNullException(nameof(rects));
            if (rects.Length == 0) return false;

            foreach (var rect in rects)
                if (rect.Contains(x, y)) return true;

            return false;
        }
        private Rectangle sheet_FindBitmap(int ptX, int ptY, int bitmapWidth, byte[] buffer)
        {
            //Prepare
            int bitmapHeight = (buffer.Length / 4) / bitmapWidth;
            int x = ptX, y = ptY, cx = 0, cy = 0, i = 0, a = 0;

            i = y * bitmapWidth + (x - 1);
            while (x >= 0)
            {
                a = i * 4;
                if (Color.FromArgb(buffer[a + 3], buffer[a + 2], buffer[a + 1], buffer[a]) == Spacer) break;
                else { i--; x--; }
            }

            i = (y - 1) * bitmapWidth + x;
            while (y >= 0)
            {
                a = i * 4;
                if (Color.FromArgb(buffer[a + 3], buffer[a + 2], buffer[a + 1], buffer[a]) == Spacer) break;
                else { i -= bitmapWidth; y--; }
            }

            i = y * bitmapWidth + x;
            while (cx < (bitmapWidth - x))
            {
                a = i * 4;
                if (Color.FromArgb(buffer[a + 3], buffer[a + 2], buffer[a + 1], buffer[a]) == Spacer) break;
                else { cx++; i++; }
            }

            i = y * bitmapWidth + x;
            while (cy < (bitmapHeight - y))
            {
                a = i * 4;
                if (Color.FromArgb(buffer[a + 3], buffer[a + 2], buffer[a + 1], buffer[a]) == Spacer) break;
                else { cy++; i += bitmapWidth; }
            }

            //Return
            return new Rectangle(x, y, cx, cy);
        }
    }
}
