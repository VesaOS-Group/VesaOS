using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VesaOS.Core.VEXFormat
{
    class VexRunner
    {
        public static void StartProgram(string filename)
        {
            byte[] vex = File.ReadAllBytes(Kernel.GetFullPath(filename));
            switch (vex[3])
            {
                case 0:
                    byte[] prog = new byte[vex.Length - ((vex[4] + 4) - 1)];
                    for (int i = (vex[4] + 4) - 1; i < vex.Length - 1; i++)
                    {
                        prog[i - ((vex[4] + 4) - 1)] = vex[i];
                    }
                    Windmill.Windmill runner = new Windmill.Windmill(4096,prog);
                    for (; !runner.program[runner.index].Equals(0);)
                    {
                        runner.RunNext();
                    }
                    break;
                default:
                    break;
            }
            
        }
    }
}
