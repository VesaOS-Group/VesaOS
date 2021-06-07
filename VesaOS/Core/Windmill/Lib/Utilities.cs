using System;
using System.Collections.Generic;
using System.Text;

namespace VesaOS.Core.Windmill.Lib
{
    static class Utilities
    {
        public static void FindFunction(Windmill super)
        {
            super.index++;
            switch (super.program[super.index - 1] % 16)
            {
                case 0x00:
                    ConvertByte(super);
                    break;
                case 0x01:
                    ConvertInt(super);
                    break;
            }
        }        

        private static void ConvertByte(Windmill super)
        {
            int loc = Memory.GetRamLoc(super);
            byte val = super.ram[loc];

            char[] charArr = val.ToString().ToCharArray();
            for (int i = 0; i < charArr.Length; i++)
            {
                super.ram[loc + i] = (byte)charArr[i];
            }
            super.ram[loc + charArr.Length] = 0;
        }

        private static void ConvertInt(Windmill super)
        {
            int loc = Memory.GetRamLoc(super);
            byte[] rawVal = Memory.GetByteArray(super, loc, 4);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(rawVal);
            int val1 = BitConverter.ToInt32(rawVal,0);

            char[] charArr = val1.ToString().ToCharArray();
            for (int i = 0; i < charArr.Length; i++)
            {
                super.ram[loc + i] = (byte) charArr[i];
            }
            super.ram[loc + charArr.Length] = 0;
        }
    }
}
