using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.HAL;
//using Console = Cosmos.HAL.Terminal;

namespace VesaOS
{
    public class Kernel : Sys.Kernel
    {
        public static List<int> pidstack { get; private set; }
        protected override void BeforeRun()
        {
            //VGADriverII.Initialize(VGAMode.Pixel320x200DB);
            pidstack.Add(0);
        }
        protected override void Run()
        {
            //VGADriverII.Display();
            Console.WriteLine(Environment.GetEnvironmentVariable("test"));
        }
        public static void RunProgram(int pid)
        {
            if (pid == 0)
            {
                throw new Exception("You cannot run the kernel!");
            }

        }
    }
}
