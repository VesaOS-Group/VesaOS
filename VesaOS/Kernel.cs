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
            
            //VGADriverII.Initialize(VGAMode.Pixel320x200DB);
            pidstack.Add(0);
        }
        protected override void Run()
        {
            Console.Write(Environment.CurrentDirectory + ">");
            System.Terminal.Shell.Exec(Console.ReadLine());
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
        public static void WriteBootCode()
        {
            throw new NotImplementedException();
        }
    }
}
