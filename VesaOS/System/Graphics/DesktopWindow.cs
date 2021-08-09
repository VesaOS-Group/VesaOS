using Cosmos.HAL;
using Cosmos.System.Graphics;
using VesaOS.System.Graphics.UI;

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
            Button button = new Button();
            button.Width = 100;
            button.Height = 25;
            button.Color = VGAColor.White;
            button.HoverColor = VGAColor.Gray;
            button.ClickColor = VGAColor.Blue;
            button.X = 0;
            button.Y = 0;
            button.Text = "Exit GUI";
            //button.MouseClick += GUIExitButton_MouseClick;
            UIElements.Add(button);
        }

        private void GUIExitButton_MouseClick(object sender, global::System.EventArgs e)
        {
            VGADriverII.SetMode(VGAMode.Text90x60);
            VGADriverII.SetColorPalette(VGADriverII.Palette16);
            WindowManager.ResetGraphicsModeFlag();
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
