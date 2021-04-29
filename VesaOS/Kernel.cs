using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.HAL;
using Console = Cosmos.HAL.Terminal;

namespace VesaOS
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            VGADriverII.Initialize(VGAMode.Text90x60);
        }

        protected override void Run()
        {
            Console.Write("Input: ");
            var input = Console.ReadLine();
            Console.Write("Text typed: ");
            Console.WriteLine(input);
        }
    }
}
