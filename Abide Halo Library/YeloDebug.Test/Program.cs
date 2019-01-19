using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YeloDebug.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Xbox xbox = new Xbox(Path.GetDirectoryName(Assembly.GetCallingAssembly().CodeBase));
            xbox.Connect();
        }
    }
}
