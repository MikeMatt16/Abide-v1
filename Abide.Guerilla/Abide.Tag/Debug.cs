using System;
using System.Diagnostics;

namespace Abide.Tag
{
    internal static class Debug
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }
        public static void Log(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }
        public static void Break()
        {
            Debugger.Break();
        }
        public static void Attach()
        {
            Debugger.Launch();
        }
    }
}
