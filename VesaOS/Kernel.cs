using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.HAL;
//using Console = Cosmos.HAL.Terminal;
using VesaOS.Drivers.VirtualPartitions;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using Cosmos.HAL.BlockDevice;

namespace VesaOS
{
    public delegate void KernelEvent();
    
    public class Kernel : Sys.Kernel
    {
        public static List<int> pidstack { get; private set; }
        public static event KernelEvent BootFinished;
        public static string CurrentDir = "";
        public static string CurrentVol = "0";
        public static Dictionary<string, string> config;
        public static Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        public static VirtualPartition ramdisk;
        private static List<Partition> mPartitions = new List<Partition>();

        protected override void BeforeRun()
        {
            try
            {
                VGADriverII.Initialize(VGAMode.Text90x60);
                Console.WriteLine("VesaOSPE is starting...");
                Console.WriteLine("Initializing ramdisk...");
                /*ramdisk = new VirtualPartition();
                BootFinished?.Invoke();*/
                Console.WriteLine("Initializing filesystem...");
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
                for (int i = 0; i < BlockDevice.Devices.Count; i++)
                {
                    if (BlockDevice.Devices[i] is Partition)
                    {
                        mPartitions.Add((Partition)BlockDevice.Devices[i]);
                    }
                }
                Console.WriteLine("Checking drive 0 is accessible...");
                try
                {
                    fs.GetDirectoryListing(@"0:\");
                }
                catch (Exception)
                {
                    Console.WriteLine("WARNING: Could not access drive 0!");
                }
                Console.WriteLine("Initializing network...");
                VesaOS.System.Network.NTPClient.Init();
                Console.WriteLine("Boot finished.");
                pidstack.Add(0);
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
                Crash(e);
            }
            
        }
        
        public static void RunProgram(int pid)
        {
            if (pid == 0)
            {
                throw new Exception("You cannot run the kernel!");
            }

        }
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
    }
}
