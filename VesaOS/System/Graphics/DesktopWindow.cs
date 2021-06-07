using Cosmos.System.Graphics;

namespace VesaOS.System.Graphics
{
    class DesktopWindow : Window
    {
        public override void Run()
        {
            DrawWallpaper();
            DrawTaskbar();
        }
        public override void Draw()
        {
            DrawWallpaper();
            DrawTaskbar();
        }
        public override void Show()
        {
            Fullscreen = true;
            DrawWallpaper();
            DrawTaskbar();
        }
        private static void DrawWallpaper()
        {
            VGAGraphics.Clear(VGAColor.Cyan7);
        }
        private static void DrawTaskbar()
        {
            VGAGraphics.DrawFilledRect(0,300,200,20,VGAColor.Gray3);
        }
    }
}
