using System;
using System.Collections.Generic;
using System.Text;

namespace VesaOS.Core.Windmill.Lib
{
    static class Math
    {
        public static void FindFunction(Windmill super)
        {
            super.index++;
            switch (super.program[super.index - 1] % 16)
            {
                case 0x00:
                    CalculateByte(super);
                    break;
                case 0x01:
                    CalculateInt(super);
                    break;
            }
        }

        private static void CalculateByte(Windmill super)
        {
            int op = super.program[super.index] % 16;
            super.index++;

            int loc1 = Memory.GetRamLoc(super);
            super.index++;
            int loc2 = Memory.GetRamLoc(super); 

            switch (op)
            {
                case 0x00:
                    super.ram[loc1] += super.ram[loc2];
                    break;
                case 0x01:
                    super.ram[loc1] -= super.ram[loc2];
                    break;
                case 0x02:
                    super.ram[loc1] *= super.ram[loc2];
                    break;
                case 0x03:
                    super.ram[loc1] /= super.ram[loc2];
                    break;
                case 0x04:
                    super.ram[loc1] %= super.ram[loc2];
                    break;
            }
        }

        private static void CalculateInt(Windmill super)
        {
            int op = super.program[super.index];

            super.index++;
            int finalLoc = Memory.GetRamLoc(super);
            byte[] rawVal1 = Memory.GetByteArray(super, finalLoc, 4);
            super.index++;
            byte[] rawVal2 = Memory.GetByteArray(super, Memory.GetRamLoc(super), 4);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(rawVal1); Array.Reverse(rawVal2);
            }
            
            int val1 = BitConverter.ToInt32(rawVal1); int val2 = BitConverter.ToInt32(rawVal2);
            switch (op)
            {
                case 0x00:
                    val1 += val2;
                    break;
                case 0x01:
                    val1 -= val2;
                    break;
                case 0x02:
                    val1 *= val2;
                    break;
                case 0x03:
                    val1 /= val2;
                    break;
                case 0x04:
                    val1 %= val2;
                    break;
            }

            byte[] rawResult = BitConverter.GetBytes(val1);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(rawResult);
            for (int i = 0; i < 4; i++)
            {
                super.ram[finalLoc + i] = rawResult[i];
            }
        }
    }
}
