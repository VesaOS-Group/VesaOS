using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace VesaOS.System.Graphics.UI
{
    public class UIElement
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public VGAColor Color;
        public VGAColor HoverColor;
        public VGAColor ClickColor;

        public virtual void Draw()
        {
            
        }
    }
}
