using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace VesaOS.Drivers.Video.VESA
{
    class VBEDriver
    {
        private static VBECanvas canvas = new VBECanvas();
        public static void Initialize()
        {
            canvas.Clear();
        }
    }
}
 