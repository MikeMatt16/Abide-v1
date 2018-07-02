using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Decompiler
{
    public interface IDecompileHost : IProgress<float>
    {
        void Complete();
        void Fail();
    }
}
