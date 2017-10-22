using Abide.Builder.Guerilla.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Builder.Guerilla.Example
{
    [TagGroupDefinition("bitmap", "bitm", 112, 0)]
    internal unsafe struct bitmapStruct
    {
        public short type;
        public short format;
        public short usage;
        public short format_;
        public float dtailFadeFactor;
        public float sharpenAmount;
        public float bumpHeight;
        public short spriteBudgetSize;
        public short spriteBudgetCount;
    }
}
