using Cosmos.System.Graphics;

namespace VesaOS.Apps
{
    class VesaOOBE : System.Graphics.Window
    {
        public override void Run()
        {
            DrawBackground();
        }
        public override void Show()
        {
            Fullscreen = true;
            DrawBackground();
        }
        private static void DrawBackground()
        {
            VGAGraphics.Clear(VGAColor.Blue3);
        }
        
    }
}
