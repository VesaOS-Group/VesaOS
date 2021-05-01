using System;
using System.Collections.Generic;
using System.Text;

namespace VesaOS.Drivers.VirtualPartitions
{
    public class VirtualPartition
    {
        //public static byte[] data;
        public static Dictionary<string, byte[]> files;
        public static List<string> directoryentry = new List<string>();
        public static byte[] bootheader = new byte[256];
        public VirtualPartition()
        {
            byte[] osimage = new byte[] { };
            files.Add("os.cza",osimage);
            directoryentry.Add("os.cza");
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
