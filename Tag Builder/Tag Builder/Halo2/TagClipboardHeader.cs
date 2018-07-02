using Abide.Tag;
using System.Runtime.InteropServices;

namespace Abide.TagBuilder.Halo2
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct TagClipboardHeader
    {
        public int BlockCount
        {
            get { return blockCount; }
            set { blockCount = value; }
        }
        public string BlockName
        {
            get { return blockName.String; }
            set { blockName.String = value; }
        }
        public bool IsData
        {
            get { return isData == 1; }
            set { isData = value ? 1 : 0; }
        }
        public int ElementSize
        {
            get { return elementSize; }
            set { elementSize = value; }
        }

        private int blockCount;
        private int isData;
        private int elementSize;
        private int reserved3;
        private String256 blockName;
    }
}
