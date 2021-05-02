using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Cosmos.HAL;
using Cosmos.System.Graphics;
namespace VesaOS.System.Graphics
{
    class DesktopWindow : Window
    {
        public override void Run()
        {
            DrawWallpaper();
        }
        public override void Show()
        {
            Fullscreen = true;
            DrawWallpaper();
        }
        private static void DrawWallpaper()
        {
            VGAGraphics.Clear(VGAColor.Cyan7);
        }
    }
}
