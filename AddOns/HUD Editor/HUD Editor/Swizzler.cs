namespace HUD_Editor
{
    internal static class Swizzler
    {
        public static byte[] Swizzle(byte[] raw, int pixOffset, int width, int height, int depth, int bitCount, bool deswizzle)
        {
            bitCount /= 8;
            int a = 0;
            int b = 0;
            byte[] dataArray = new byte[raw.Length]; //width * height * bitCount;

            MaskSet masks = new MaskSet(width, height, depth);
            pixOffset = 0;

            int x = 0, y = 0;
            for (int i = 0; i < height * width; i++)
            {
                x = i % width;
                y = i / width;

                if (deswizzle)
                {
                    a = ((y * width) + x) * bitCount;
                    b = (Swizzle(x, y, -1, masks)) * bitCount;
                }
                else
                {
                    b = ((y * width) + x) * bitCount;
                    a = (Swizzle(x, y, -1, masks)) * bitCount;
                }

                if (a < dataArray.Length && b < raw.Length)
                {
                    for (int j = pixOffset; j < bitCount + pixOffset; j++)
                        dataArray[a + j] = raw[b + j];
                }
                else return null;
            }
            return dataArray;
        }
        public static byte[] Swizzle(byte[] raw, int width, int height, int depth, int bitCount, bool deswizzle)
        {
            return Swizzle(raw, 0, width, height, depth, bitCount, deswizzle);
        }
        private static int Swizzle(int x, int y, int z, MaskSet masks)
        {
            return SwizzleAxis(x, masks.x) | SwizzleAxis(y, masks.y) | (z == -1 ? 0 : SwizzleAxis(z, masks.z));
        }
        private static int SwizzleAxis(int val, int mask)
        {
            int bit = 1;
            int result = 0;

            while (bit <= mask)
            {
                int tmp = mask & bit;

                if (tmp != 0) result |= (val & bit);
                else val <<= 1;

                bit <<= 1;
            }

            return result;
        }

        private struct MaskSet
        {
            public int x;
            public int y;
            public int z;

            public MaskSet(int w, int h, int d)
            {
                //Prepare
                int bit = 1;
                int index = 1;
                x = 0;
                y = 0;
                z = 0;

                //Loop
                while (bit < w || bit < h || bit < d)
                {
                    if (bit < w)
                    {
                        x |= index;
                        index <<= 1;
                    }
                    if (bit < h)
                    {
                        y |= index;
                        index <<= 1;
                    }
                    if (bit < d)
                    {
                        z |= index;
                        index <<= 1;
                    }
                    bit <<= 1;
                }
            }
        }
    }
}
