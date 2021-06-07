using System.Collections.Generic;
using System.Drawing;
using VesaOS.System.Graphics.UI;

namespace VesaOS.System.Graphics
{
    public class Window
    {
        public static Point Location;
        public static int Width;
        public static int Height;
        public static byte[] vram;
        public static bool Fullscreen = false;
        public List<UIElement> UIElements = new List<UIElement>();

        public virtual void Run()
        {
            foreach(UIElement element in UIElements)
            {
                element.Draw();
            }
        }
        public virtual void Draw()
        {

        }
        public virtual void Show(int w, int h, Point loc)
        {
            Width = w;
            Height = h;
            Location = loc;
            vram = new byte[w*h];
        }
        public virtual void Show()
        {
            if (!Fullscreen)
            {
                return;
            }
        }
        public virtual void Stop()
        {

        }
    }
}
