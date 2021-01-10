using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Abide.DebugXbox.Test
{
    class Program
    {
        static Xbox currentXbox = null;
        static Xbox[] xboxes = new Xbox[0];

        static void WriteHeader()
        {
            Console.WriteLine("Abide Debug Xbox Library Test Application");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            //Welcome
            WriteHeader();
            xboxes = NameAnsweringProtocol.Discover(10);

            //Application loop
#if DEBUG
            Test();
#endif
            Application();
        }

        private static void Test()
        {
            Xbox xbox = NameAnsweringProtocol.Discover(15).FirstOrDefault();
            if (xbox != null)
            {
                xbox.Connect();
                if (xbox.Connected)
                {
                    if (xbox.Screenshot(out var screenshot))
                    {
                        screenshot.Save(@"F:\screenshot.png", System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
        }

        private static string[] GetCommand(string input)
        {
            char[] split = new char[] { ' ' };
            var result = input.Split('"').Select((s, i) => i % 2 == 0 ? 
            s.Split(split, StringSplitOptions.RemoveEmptyEntries) : 
            new string[] { s }).SelectMany(s => s).ToList();
            return result.ToArray();
        }

        private static void Application()
        {
            //Get input
            string input = string.Empty;

            //Loop
            do
            {
                //Write
                Console.Write("Abide.DebugXbox.Test>");
                input = Console.ReadLine();
                Console.WriteLine();

                //Get command
                string[] command = GetCommand(input);

                //Handle
                if (command.Length > 0)
                    HandleCommand(command);
            }
            while (input.ToUpper() != "EXIT");
            Console.WriteLine("Leaving application...");
        }

        private static void HandleCommand(params string[] cmd)
        {
            string command = cmd.First();
            string[] args = cmd.Where((s, i) => i > 0).ToArray();

            switch (command.ToUpper())
            {
                case "CONNECT":
                    //Check for current Xbox
                    if (currentXbox == null)
                    {
                        Console.WriteLine("There is no Xbox specified.");
                        Console.Write("Please select an Xbox and try again.");
                        break;
                    }

                    //Connect
                    currentXbox.Response += CurrentXbox_Response;
                    EnterRDCP(currentXbox);
                    currentXbox.Response -= CurrentXbox_Response;
                    break;
                case "SELECT":
                    switch ((args.FirstOrDefault() ?? string.Empty).ToUpper())
                    {
                        case "XBOX":
                            SelectXbox(args.Where((s, i) => i > 0).ToArray());
                            break;
                        default:
                            WriteHeader();
                            Console.WriteLine("XBOX\t- Shift the focus to a debug Xbox. For example SELECT XBOX.");
                            break;
                    }
                    break;
                case "REFRESH":
                    xboxes = NameAnsweringProtocol.Discover();
                    break;
                case "LIST":
                    switch ((args.FirstOrDefault() ?? string.Empty).ToUpper())
                    {
                        case "XBOX":
                            ListXbox();
                            break;
                        default:
                            WriteHeader();
                            Console.WriteLine("XBOX\t- Display a list of debug Xbox consoles. For example LIST XBOX.");
                            break;
                    }
                    break;
                default:
                    WriteHeader();
                    Console.WriteLine("LIST\t- Display a list of objects.");
                    Console.WriteLine("REFRESH\t- Discover all debug Xbox consoles on the network.");
                    Console.WriteLine("SELECT\t- Shift the focus to an object.");
                    Console.WriteLine("CONNECT\t- Connects to the selected Xbox");
                    Console.WriteLine("EXIT\t- Exit application.");
                    break;
            }

            //Write Line
            Console.WriteLine();
        }

        private static void CurrentXbox_Response(object sender, ResponseEventArgs e)
        {
            //Check
            if (sender is Xbox srcXbox)
                Console.WriteLine("{0}: {1}- {2}", srcXbox.Name, e.Status, e.Message);
        }

        private static void EnterRDCP(Xbox xbox)
        {
            //Get input
            string input = string.Empty;
            xbox.Connect();

            //Loop
            do
            {
                Console.Write("{0}>", xbox.Name);
                input = Console.ReadLine();
                string[] cmd = GetCommand(input);
                if(input == ".")
                {
                }
                else if (cmd.Length > 0 && cmd[0].ToUpper() != "BYE")
                {
                    xbox.SendCommand(cmd[0], cmd.Where((c, i) => i > 0).ToArray());
                    Thread.Sleep(5);
                    xbox.GetResponse();
                }
                else break; //If user types bye command exit the loop and disconnect
            }
            while (true);

            //Disconnect
            xbox.Disconnect();
        }

        private static void SelectXbox(params string[] args)
        {
            //Get argument
            string arg = args.FirstOrDefault() ?? string.Empty;
            Xbox foundXbox = null;

            //Loop through Xboxes
            foreach (Xbox xbox in xboxes)
            {
                //Check name and IP address
                if (xbox.Name == arg || xbox.RemoteEndPoint.Address.ToString() == arg)
                {
                    foundXbox = xbox;
                    break;
                }
            }

            //Check
            if (foundXbox == null) Console.WriteLine("The Xbox you specified is not valid.");
            else Console.WriteLine("{0} is now the selected Xbox.", foundXbox.Name);

            //Set
            if (foundXbox != null) currentXbox = foundXbox;
        }

        private static void ListXbox()
        {
            //Get widths
            int nameWidth = xboxes.Max(x => x.Name.Length);
            int addressWidth = xboxes.Max(x => x.RemoteEndPoint.Address.ToString().Length);
            int statusWidth = 12;
            if (nameWidth < 4) nameWidth = 4;
            if (addressWidth < 10) addressWidth = 10;
            
            Console.Write("   ");
            Console.Write("Name".PadRight(nameWidth) + " ");
            Console.Write("Status".PadRight(statusWidth) + " ");
            Console.WriteLine("IP Address".PadRight(addressWidth) + " ");
            Console.Write("   ");
            for (int i = 0; i < nameWidth; i++) Console.Write("-");
            Console.Write(" ");
            for (int i = 0; i < statusWidth; i++) Console.Write("-");
            Console.Write(" ");
            for (int i = 0; i < addressWidth; i++) Console.Write("-");
            Console.WriteLine(" ");
            foreach (Xbox xbox in xboxes)
            {
                Console.Write(xbox == currentXbox ? "*" : " ");
                Console.Write("  ");
                Console.WriteLine("{0} {1} {2}", xbox.Name.PadRight(nameWidth, ' '),
                    (xbox.Connected ? "Connected" : "Disconnected").PadRight(statusWidth, ' '),
                    xbox.RemoteEndPoint.Address.ToString().PadRight(addressWidth, ' '));
            }
        }
    }
}
