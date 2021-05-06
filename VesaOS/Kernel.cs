using System;
using System.Collections.Generic;
using Sys = Cosmos.System;
using Cosmos.HAL;
//using Console = Cosmos.HAL.Terminal;
using VesaOS.Drivers.VirtualPartitions;
using VesaOS.System;
using VesaOS.System.Users;

namespace VesaOS
{
    public delegate void KernelEvent();
    
    public class Kernel : Sys.Kernel
    {
        public static List<int> pidstack { get; private set; }
        public static List<string> RunningServices { get; private set; }
        public static Core.Registry.IniFile config;
        public static event KernelEvent BootFinished;
        public static string CurrentDir = "";
        public static string CurrentVol = "0";
        public static Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        public static VirtualPartition ramdisk;

        protected override void BeforeRun()
        {
            try
            {
                mDebugger.Send("VesaOS is starting!");
                pidstack.Add(0);
                VGADriverII.Initialize(VGAMode.Text90x60);
                Terminal.BackColor = ConsoleColor.DarkGreen;
                Terminal.ClearSlow(ConsoleColor.DarkGreen);
                Terminal.SetCursorPos(39,30);
                Terminal.Write("VesaOS");
                /*Console.WriteLine("VesaOSPE is starting...");
                Console.WriteLine("Initializing ramdisk...");
                ramdisk = new VirtualPartition();
                BootFinished?.Invoke();*/
                //Console.WriteLine("Initializing filesystem...");
                mDebugger.Send("Filesystem init");
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
                Terminal.ClearSlow(ConsoleColor.DarkGreen);
                Terminal.SetCursorPos(39, 30);
                Terminal.Write("VesaOS");
                mDebugger.Send("Checking drive 0 is accessible...");
                try
                {
                    fs.GetDirectoryListing(@"0:\");
                }
                catch (Exception)
                {
                    mDebugger.Send("WARNING: Could not access drive 0!");
                }
                mDebugger.Send("Reading registry...");
                config = new Core.Registry.IniFile("0:\\config.ini");
                if (config.GetBoolean("Boot", "NetworkEnabled"))
                {
                    mDebugger.Send("Initializing network...");
                    VesaOS.System.Network.NTPClient.Init();
                }
                if (!config.GetBoolean("Setup","SetupCompleted"))
                {
                    /*System.Graphics.WindowManager.Init();
                    System.Graphics.Window OOBE = new Apps.VesaOOBE();
                    System.Graphics.WindowManager.ShowWindow(OOBE);
                    while (true) { System.Graphics.WindowManager.Run(); }*/
                    Terminal.BackColor = ConsoleColor.Black;
                    Terminal.ClearSlow(ConsoleColor.Black);
                    Apps.VesaOOBEText.UserAccountSetup();
                }
                mDebugger.Send("Initializing shell...");
                Terminal.InitHistory();
                //Console.WriteLine("Starting services...");
                //StartService("ukms");
                //Console.WriteLine("Boot finished.");
                pidstack.Add(1);
                Terminal.BackColor = ConsoleColor.Black;
                Terminal.ClearSlow(ConsoleColor.Black);
                Console.Write("Username: ");
                string un = Console.ReadLine();
                Console.Write("Password: ");
                string psk = Console.ReadLine();
                /*while (!UserProfileSystem.Login(un,psk))
                {
                    Console.Write("Username: ");
                    un = Console.ReadLine();
                    Console.Write("Password: ");
                    psk = Console.ReadLine();
                }*/
            }
            catch (Exception e)
            {
                Crash(e);
            }
        }
        protected override void Run()
        {
            try
            {
                if (!System.Graphics.WindowManager.GraphicsMode)
                {
                    
                    Console.Write(Environment.CurrentDirectory + ">");
                    System.Terminal.Shell.Exec(Console.ReadLine());
                }
                else
                {
                    System.Graphics.WindowManager.Run();
                }
                
            }
            catch (Exception e)
            {
                if (e is FatalException)
                {
                    Crash(e);
                } else
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            
        }
        
        /*public static void StartService(string sname)
        {
            switch (sname.ToLower())
            {
                case "ukms":
                    if (!RunningServices.Contains("ukms"))
                    {
                        RunningServices.Add("ukms");
                        Core.Services.UKMS.Start();
                    }
                    break;
                default:
                    break;
            }
        }*/
        /// <summary>
        /// Get the full path of a file.
        /// </summary>
        /// <param name="name">The filename.</param>
        /// <returns>The full path.</returns>
        public static string GetFullPath(string name)
        {
            if (CurrentDir == "")
            {
                return Kernel.CurrentVol + @":\" + name;
            }
            else
            {
                return Kernel.CurrentVol + @":\" + Kernel.CurrentDir + "\\" + name;
            }
        }
        private void Crash(Exception e)
        {
            Terminal.TextColor = ConsoleColor.White;
            Terminal.BackColor = ConsoleColor.DarkBlue;
            Terminal.ClearSlow(ConsoleColor.DarkBlue);
            Terminal.WriteLine("Your PC has run into a problem and has been shut down to prevent damage to the system.");
            Terminal.WriteLine("");
            Terminal.WriteLine("Error:");
            Terminal.WriteLine(e.ToString());
            Terminal.WriteLine("\nPlease report this issue to the developers!\nhttps://github.com/TheRealEli310/VesaOS");
            int y = Terminal.CursorY;
            Terminal.SetCursorPos(0, 59); Terminal.Write("@Vesa Systems - 2021");

            Terminal.SetCursorPos(0, y);
            Terminal.WriteLine("\nPress enter to reboot, press delete to shut down: ");

            while (true)
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    Sys.Power.Reboot();
                else if (Console.ReadKey(true).Key == ConsoleKey.Delete)
                    Sys.Power.Shutdown();
        }
        public override string ToString()
        {
            return "VesaOS Kernel";
        }
        public static void WriteBootCode()
        {
            throw new NotImplementedException();
        }
        public static void Reboot()
        {
            config.SetValue("Setup", "SetupCompleted", "true");
            config.Push("0:\\config.ini");
            Sys.Power.Reboot();
        }
    }
}
