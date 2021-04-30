using System;
using System.Collections.Generic;
using System.Text;

namespace VesaOS.Drivers.VirtualPartitions
{
    abstract class VirtualPartition
    {
        public static byte[] data;
        public static byte[] bootheader = new byte[256];
        public VirtualPartition(int size)
        {
            data = new byte[size];
        }
        public static void BootHeaderWrite(byte[] sect)
        {
            if (!(sect.Length == 256))
            {
                throw new Exception("Size of array is not 256!");
            }
            bootheader = sect;
        }

    }
}
