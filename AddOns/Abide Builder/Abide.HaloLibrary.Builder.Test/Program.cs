using System;
using System.IO;

namespace Abide.HaloLibrary.Builder.Test
{
    static class Program
    {
        static volatile bool input = true;

        [STAThread]
        static int Main(string[] args)
        {
            //Prepare
            string line = null;
            string[] parts = null;

            //Welcome
            Console.WriteLine("Welcome to Abide Command Line");

            //Loop
            while(input)
            {
                //Check
                if (!string.IsNullOrEmpty(line = Console.ReadLine()))
                {
                    //Split
                    parts = line.Split(' ');

                    //Handle
                    HandleLine(parts[0], parts);
                }
            }

            //Wait..
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            
            //Return
            return 0;
        }

        private static void HandleLine(string line, params string[] args)
        {
            switch (line)
            {
                case "clear":
                    Console.Clear();
                    Console.WriteLine("Welcome to Abide Command Line");
                    break;

                case "help":
                    Console.WriteLine("Yeah uhh no thanks.");
                    break;

                case "exit":
                case "stop":
                case "quit":
                    input = false;
                    break;

                case "open":
                    Console.WriteLine("Drag map file... (Or type path)");
                    string filename = Console.ReadLine();
                    OpenMap(filename);
                    break;

                default:
                    Console.WriteLine($"Unknown input: \"{line}\"");
                    break;
            }
        }

        private static void OpenMap(string filename)
        {
            //Check
            if (!File.Exists(filename))
            {
                Console.WriteLine("Unable to find file.");
                return;
            }

        }
    }
}
