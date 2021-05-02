using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;
using Cosmos.System;
using Cosmos.System.Graphics;

namespace VesaOS.System.Graphics
{
    class WindowManager
    {
        private static List<Window> windows = new List<Window>();
        private static int RunningIndex = 0;
        private static readonly byte[] CursorData = new byte[6 * 10]
        {
            0x00, 0x9F, 0x9F, 0x9F, 0x9F, 0x9F,
            0x00, 0x00, 0x9F, 0x9F, 0x9F, 0x9F,
            0x00, 0xFF, 0x00, 0x9F, 0x9F, 0x9F,
            0x00, 0xFF, 0xFF, 0x00, 0x9F, 0x9F,
            0x00, 0xFF, 0xFF, 0xFF, 0x00, 0x9F,
            0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0x00,
            0x00, 0xFF, 0xFF, 0xFF, 0x00, 0x9F,
            0x00, 0xFF, 0x00, 0xFF, 0x00, 0x9F,
            0x00, 0x00, 0x9F, 0x00, 0xFF, 0x00,
            0x9F, 0x9F, 0x9F, 0x9F, 0x00, 0x9F,
        };
        public static VGAImage ImgCursor = new VGAImage(6,10,CursorData);
        public static bool GraphicsMode { get; private set; }
        public static void Run()
        {
            VGAGraphics.Clear(VGAColor.Black);
            //draw current window (windows are always fullscreen for now)
            if (!(windows.Count == 0))
            {
                windows[RunningIndex].Run();
            }
            //draw mouse
            VGAGraphics.DrawImage((int)MouseManager.X, (int)MouseManager.Y, VGAColor.Magenta, ImgCursor);
            VGADriverII.Display();
        }
        public static void Init()
        {
            VGADriverII.SetMode(VGAMode.Pixel320x200DB);
            GraphicsMode = true;
            MouseManager.ScreenHeight = 200;
            MouseManager.ScreenWidth = 320;
            windows.Add(new DesktopWindow());
            windows[0].Show();
        }
        public static void ShowWindow(Window w)
        {
            windows.Add(w);
            windows[windows.Count - 1].Show();
        }
    }
}
