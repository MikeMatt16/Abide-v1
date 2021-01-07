using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TextureEditor
{
    public sealed class BitmapSourcesToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int mipmapCount = 0;
            if (parameter is string s)
            {
                _ = int.TryParse(s, out mipmapCount);
            }

            if (value is IEnumerable<BitmapSource> mipmaps && targetType == typeof(ImageSource))
            {
                if (mipmapCount == 0)
                {
                    mipmapCount = mipmaps.Count();
                }

                var format = mipmaps.First().Format;
                var palette = mipmaps.First().Palette;
                int width = mipmaps.First().PixelWidth;
                int height = mipmaps.First().PixelHeight;

                for (int m = 1; m < mipmapCount; m++)
                {
                    height += mipmaps.ElementAt(m).PixelWidth;
                }

                WriteableBitmap bitmap = new WriteableBitmap(width, height, 96, 96, format, palette);

                unsafe
                {
                    bitmap.Lock();
                    bitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));

                    try
                    {
                        IntPtr pBackBufferDest = bitmap.BackBuffer;

                        for (int m = 0; m < mipmapCount; m++)
                        {
                            WriteableBitmap mipmap = mipmaps.ElementAt(m) as WriteableBitmap;

                            if (mipmap == null)
                            {
                                continue;
                            }

                            try
                            {
                                mipmap.Lock();

                                int diff = bitmap.BackBufferStride - mipmap.BackBufferStride;
                                IntPtr pBackBufferSrc = mipmap.BackBuffer;

                                byte[] data = new byte[mipmap.BackBufferStride * mipmap.PixelHeight];
                                Marshal.Copy(pBackBufferSrc, data, 0, data.Length);

                                for (int i = 0; i < mipmap.PixelHeight; i++)
                                {
                                    Marshal.Copy(data, i * mipmap.BackBufferStride, pBackBufferDest, mipmap.BackBufferStride);
                                    pBackBufferDest += diff + mipmap.BackBufferStride;
                                }
                            }
                            finally { mipmap.Unlock(); }
                        }
                    }
                    finally
                    {
                        bitmap.Unlock();
                    }
                }

                return bitmap;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
