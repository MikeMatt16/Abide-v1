using Abide.Wpf.Modules.Win32;
using System.IO;

namespace Abide.Wpf.Modules.Halo2
{
    public static class Helper
    {
        public static byte[] ReadExternalData(long offset, int length)
        {
            long location = ((uint)offset) >> 30;
            long address = ((uint)offset) & 0x3FFFFFFF;
            byte[] data = null;

            switch (location)
            {
                case 1:
                    if (File.Exists(AbideRegistry.Halo2Mainmenu))
                    {
                        using (FileStream fs = File.OpenRead(AbideRegistry.Halo2Mainmenu))
                        {
                            _ = fs.Seek(address, SeekOrigin.Begin);
                            data = new byte[length];
                            _ = fs.Read(data, 0, length);
                        }
                    }

                    break;
                case 2:
                    if (File.Exists(AbideRegistry.Halo2Shared))
                    {
                        using (FileStream fs = File.OpenRead(AbideRegistry.Halo2Shared))
                        {
                            _ = fs.Seek(address, SeekOrigin.Begin);
                            data = new byte[length];
                            _ = fs.Read(data, 0, length);
                        }
                    }

                    break;
                case 3:
                    if (File.Exists(AbideRegistry.Halo2SpShared))
                    {
                        using (FileStream fs = File.OpenRead(AbideRegistry.Halo2SpShared))
                        {
                            _ = fs.Seek(address, SeekOrigin.Begin);
                            data = new byte[length];
                            _ = fs.Read(data, 0, length);
                        }
                    }

                    break;
            }

            return data;
        }
    }
}
