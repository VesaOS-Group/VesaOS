using Cosmos.System.Graphics;

namespace VesaOS.System.Graphics.UI
{
    public class UIElement
    {
        public int X = 0;
        public int Y = 0;
        public int Width = 100;
        public int Height = 50;
        public VGAColor Color = VGAColor.Gray;
        public VGAColor HoverColor = VGAColor.Gray;
        public VGAColor ClickColor = VGAColor.Gray;

        public virtual void Draw()
        {
            
        }
        public virtual void Run()
        {

        }
    }
}
