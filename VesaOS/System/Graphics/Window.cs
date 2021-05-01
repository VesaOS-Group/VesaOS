using Cosmos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace VesaOS.System.Graphics
{
    abstract class Window
    {
        public static ushort Width { get; private set; }
        public static ushort Height { get; private set; }
        //public static unsafe byte* Buffer;
        private static MemoryBlock BackBuffer = new MemoryBlock(0x60000, 0x10000);
        public virtual void Create()
        {

        }
    }
}
