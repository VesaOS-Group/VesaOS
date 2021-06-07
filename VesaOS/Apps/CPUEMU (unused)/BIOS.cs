using System;
using System.Collections.Generic;
using System.Text;

namespace VesaOS.Apps.CPUEMU
{
    class BIOS
    {
        public static void BootDisk()
        {
            if (HardDisk.GetByte(511) == 0xAA && HardDisk.GetByte(512) == 0x55)
            {
                Console.WriteLine("Disk is not bootable!");
            }
        }
    }
}
