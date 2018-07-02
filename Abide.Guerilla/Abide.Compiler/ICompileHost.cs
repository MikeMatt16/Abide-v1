using System;

namespace Abide.Compiler
{
    public interface ICompileHost : IProgress<float>
    {
        void Log(string line);
        void Marquee();
        void Complete();
        void Fail();
    }
}
