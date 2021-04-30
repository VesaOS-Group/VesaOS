using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VesaOS.Win32
{
    class fileapi
    {
        public static FileStream CreateFileA(string lpFileName)
        {
            return File.Open(lpFileName, System.IO.FileMode.OpenOrCreate);
        }
        public static FileStream CreateFileW(string lpFileName)
        {
            return File.Open(lpFileName, System.IO.FileMode.Create);
        }
    }
}
