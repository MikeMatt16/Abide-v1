using Abide.HaloLibrary.Halo2Map;
using System;
using System.IO;

namespace Abide.HaloLibrary.Builder.Test
{
    static class Program
    {
        static IProgram currentProgram = null;
        static MapFile selectedMap = null;
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

        private static void HandleLine(string input, params string[] args)
        {
            if (currentProgram != null) //Check program
            {
                switch (input)
                {
                    case "exit":    //Exit program
                    case "stop":
                    case "quit":
                        //Exit
                        currentProgram.Exit();
                        currentProgram = null;

                        //Clear
                        Console.Clear();
                        Console.WriteLine("Welcome to Abide Command Line");
                        break;

                    default: currentProgram.OnInput(input, args); break;    //Program handle input
                }
            }
            else
            {
                switch (input)   //Handle input
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
                        Program.input = false;
                        break;

                    case "open":
                        Console.WriteLine("Drag map file... (Or type path)");
                        string filename = Console.ReadLine();
                        OpenMap(filename);
                        break;

                    case "map":
                        if (selectedMap == null) { Console.WriteLine("No selected map."); return; }
                        Console.WriteLine($"Selected map is {selectedMap.Name} ({selectedMap.Build})");
                        Console.WriteLine("Would you like to go to the editor? (Y/n)");
                        if (Console.ReadKey().Key == ConsoleKey.Y)
                        { currentProgram = new MapEditor(selectedMap); currentProgram.Start(); }
                        break;

                    default:
                        Console.WriteLine($"Unknown input: \"{input}\"");
                        break;
                }
            }
        }

        private static void OpenMap(string filename)
        {
            //Check for quotation marks
            string safeFileName = filename;
            if ((safeFileName.StartsWith("\"") || safeFileName.StartsWith("\'")) && (safeFileName.EndsWith("\"") || safeFileName.EndsWith("\'")))
                safeFileName = filename.Substring(1, filename.Length - 2);
            else if (safeFileName.StartsWith("file:\\") && filename.Length >= 7)
                safeFileName = filename.Substring(7);

            //Check
            if (!File.Exists(safeFileName))
            {
                Console.WriteLine("Unable to find file.");
                return;
            }

            //Open
            try
            {
                //Dispose
                if (selectedMap != null) selectedMap.Dispose();
                selectedMap = new MapFile();

                //Load
                selectedMap.Load(safeFileName);

                //Write
                Console.WriteLine($"{selectedMap.Name} is now the selected map.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error opening {filename}:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
