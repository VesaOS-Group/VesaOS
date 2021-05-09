using System;
using System.IO;

namespace VesaOS.Apps.CPUEMU
{
    class HardDisk
    {
        public static byte[] hddbuf;
        public static byte GetByte(ulong addr)
        {
            ulong hddfilei = addr / 524288;
            string hddfile = "hdd-"+hddfilei.ToString()+".hdd";
            hddbuf = File.ReadAllBytes(Kernel.GetFullPath(hddfile));
            int hdaddr = (int)(addr % 524288);
            return hddbuf[hdaddr];
        }
    }
}
