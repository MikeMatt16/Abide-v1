using System;

namespace Abide.Decompiler
{
    public interface IDecompileReporter : IProgress<float>
    {
        void Complete();
        void Fail();
    }
}
