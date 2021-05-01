using System;
using IL2CPU.API.Attribs;
using VesaOS;

namespace VesaOS_Plugs
{
    [Plug(Target = typeof(global::System.Environment))]
    public class EnviromentImpl
    {
        public static string CurrentDirectory {
            get { return Kernel.CurrentVol + ":\\" + Kernel.CurrentDir; }
            set { SetKernelVarsCurrentDir(value); } 
        }
        private static void SetKernelVarsCurrentDir(string value)
        {
            string[] split = value.Split("\\");
            string vol = split[0].Remove(1);
            Kernel.CurrentVol = vol;
            //TODO: set current dir
        }
    }
}
