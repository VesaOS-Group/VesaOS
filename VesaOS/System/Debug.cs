using System;
using System.Collections.Generic;
using System.Text;

namespace VesaOS.System
{
    class Debug
    {
        public static bool Enabled { get; private set; }
        public static void Init()
        {
            Kernel.BootFinished += BootFinished;
        }

        private static void BootFinished()
        {
            Console.WriteLine("Debug messages disabled!");
            Enabled = false;
        }
    }
}
