using System;
using System.Collections.Generic;
using System.Text;

namespace VesaOS.Core.Windmill.Lib
{
    static class Output
    {
        public static void FindFunction(Windmill super)
        {
            super.index++;
            switch (super.program[super.index - 1] % 16)
            {
                case 0x00:
                    PrintChar(super);
                    break;
                case 0x01:
                    PrintString(super);
                    break;
                case 0x02:
                    SetForeGroundColor(super);
                    break;
                case 0x03:
                    SetBackGroundColor(super);
                    break;
            }
        }

        static void SetBackGroundColor(Windmill super)
        {
            byte type = super.program[super.index];
            Console.BackgroundColor = (ConsoleColor)type;
        }

        static void SetForeGroundColor(Windmill super)
        {
            byte type = super.program[super.index];
            Console.ForegroundColor = (ConsoleColor)type;
        }

        static void PrintChar(Windmill super)
        {
            int loc = Memory.GetRamLoc(super);
            Console.Write((char) super.ram[loc]);
        }

        static void PrintString(Windmill super)
        {
            int loc = Memory.GetRamLoc(super);
            string capture = "";

            for (; super.ram[loc] != 0; loc++)
            {
                capture += (char) super.ram[loc];
            }
            Console.Write(capture);
        }
    }
}
