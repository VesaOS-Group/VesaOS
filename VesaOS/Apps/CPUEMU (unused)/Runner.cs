using System;
using System.Collections.Generic;
using System.Text;

namespace VesaOS.Apps.CPUEMU
{
    class Runner
    {
        public static ProcessorEnum Processor { get; private set; }
        public static int ip = 0;
        public static byte[] ram;
        public static void RunInst(byte inst)
        {
            switch (inst)
            {
                case 0x90:
                    break;
                case 0xE9:
                    ip = BitConverter.ToInt32(ram, ip+1);
                    break;
                default:
                    break;
            }
        }
        public static void Reset()
        {
            ram = new byte[524288];
            BIOS.BootDisk();
        }
        public static void Halt()
        {
            Console.WriteLine("CPU Halted.");
        }
    }
}
