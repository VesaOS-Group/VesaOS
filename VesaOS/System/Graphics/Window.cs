﻿using Cosmos.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace VesaOS.System.Graphics
{
    abstract class Window
    {
        public static Point Location;
        public static int Width;
        public static int Height;
        public static byte[] vram;
        
        public virtual void Run()
        {

        }
        public virtual void Draw()
        {

        }
        public virtual void Init(int w, int h, Point loc)
        {
            Width = w;
            Height = h;
            Location = loc;
            vram = new byte[w*h];
        }
    }
}