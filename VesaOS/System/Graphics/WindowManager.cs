using System.Collections.Generic;
using Cosmos.HAL;
using Cosmos.System;
using Cosmos.System.Graphics;
using VesaOS.System.Graphics.UI;

namespace VesaOS.System.Graphics
{
    class WindowManager
    {
        private static List<Window> windows = new List<Window>();
        private static List<Window> windows1 = new List<Window>();
        private static List<Window> windows2 = new List<Window>();
        private static List<Window> windows3 = new List<Window>();
        private static List<Window> windows4 = new List<Window>();
        private static int CurrentDesktop = 1;
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
            foreach (var window in windows)
            {
                window.Draw();
            }
            if (windows.Count != 0)
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
            RunningIndex = windows.Count - 1;
            windows[windows.Count - 1].Show();
        }
        public static void ShowDesktop(int d)
        {
            switch (CurrentDesktop)
            {
                case 1:
                    windows1 = windows;
                    break;
                case 2:
                    windows2 = windows;
                    break;
                case 3:
                    windows3 = windows;
                    break;
                case 4:
                    windows4 = windows;
                    break;
                default:
                    windows1 = windows;
                    break;
            }
            CurrentDesktop = d;
            switch (CurrentDesktop)
            {
                case 1:
                    windows = windows1;
                    break;
                case 2:
                    windows = windows2;
                    break;
                case 3:
                    windows = windows3;
                    break;
                case 4:
                    windows = windows4;
                    break;
                default:
                    windows = windows1;
                    break;
            }
        }
        public static void ResetGraphicsModeFlag() {
            GraphicsMode = false;
        }
    }
}
