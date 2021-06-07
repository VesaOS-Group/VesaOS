using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VesaOS.System
{
    class VesaUpdate
    {
        public static void InstallBundle(string filename)
        {
            Directory.CreateDirectory(@"0:\system\updatetemp");
            Tar.ExtractTar(filename,@"0:\system\updatetemp");
            foreach (var upd in Directory.GetFiles(@"0:\system\updatetemp"))
            {
                Install(upd);
            }
        }
        public static void Install(string filename)
        {
            //this will be replaced with a lot of code later
            Tar.ExtractTar(filename,@"0:\");
        }
    }
}
