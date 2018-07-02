using System;
using System.Collections.Generic;
using System.Text;

namespace Abide.DebugXbox.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Discover
            var xboxes = NameAnsweringProtocol.Discover();

            //Exit
            Exit();
        }

        private static void Exit()
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
