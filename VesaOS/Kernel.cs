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
                Console.Write(Environment.CurrentDirectory + ">");
                System.Terminal.Shell.Exec(Console.ReadLine());
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
        public override string ToString()
        {
            return "VesaOS Kernel";
        }
        public static void Crash(Exception e)
        {
            Terminal.BackColor = ConsoleColor.Red;
            Terminal.TextColor = ConsoleColor.White;
            Terminal.Clear();
            Terminal.WriteLine("VesaOS ran into a problem and cannot continue.");
            Terminal.WriteLine(e.ToString());
            Terminal.WriteLine("");
            Terminal.WriteLine("If this is the first time you have seen this screen, try rebooting and trying what you were doing again.");
            Terminal.WriteLine("If you did that and it is the second time you see this, try making an issue on the github page and we will try to help fix your problem.");
            Terminal.WriteLine("If you want to fix it yourself, make a fork of the repository and edit the code to fix it. Make sure to make a pull request to the main repo afterwards.");
            while (true);
        }
        public static void WriteBootCode()
        {
            throw new NotImplementedException();
        }
    }
}
