using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TextureEditor
{
    /// <summary>
    /// Represents the collection of pixel formats supported by Halo.
    /// </summary>
    public static class HaloPixelFormats
    {
        public static PixelFormat A8R8G8B8 => PixelFormats.Bgra32;
        public static PixelFormat A8
        {
            get
            {
                PixelFormat format = PixelFormats.Gray8;
                for (int i = 0; i < format.Masks.Count; i++)
                {
                    var mask = format.Masks[i];
                }

                return format;
            }
        }
        public static PixelFormat Y8 => PixelFormats.Gray8;
        public static PixelFormat AY8 => PixelFormats.Gray8;
        public static PixelFormat A8Y8 => PixelFormats.Gray16;
        public static PixelFormat R5G6B5 => PixelFormats.Bgr565;
        public static PixelFormat A1R5G5B5 => PixelFormats.Gray16;
        public static PixelFormat A4R4G4B4 => PixelFormats.Bgra32;
    }
}
