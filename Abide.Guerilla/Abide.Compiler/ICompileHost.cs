using System;

namespace Abide.Compiler
{
    public interface ICompileHost : IProgress<float>
    {
        object Invoke(Delegate method);
        void Log(string line);
        void Complete();
        void Fail();
    }
}
