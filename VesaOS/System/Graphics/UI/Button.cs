using Cosmos.System;
using Cosmos.System.Graphics;
using System;

namespace VesaOS.System.Graphics.UI
{
    public class Button : UIElement
    {
        private bool MousePreviouslyPressed = false;

        public override void Draw()
        {
            bool MouseOver = Intersect((int)MouseManager.X, (int)MouseManager.Y);

            if (MouseOver)
            {
                if (MouseManager.MouseState == MouseState.Left && !MousePreviouslyPressed)
                {
                    MousePreviouslyPressed = true;
                    VGAGraphics.DrawFilledRect(X, Y, Width, Height, ClickColor);
                    EventArgs eventArgs = new EventArgs();
                    OnMouseClick(eventArgs);
                } else
                {
                    VGAGraphics.DrawFilledRect(X, Y, Width, Height, HoverColor);
                }

                if (MouseManager.MouseState != MouseState.Left) MousePreviouslyPressed = false;
            }
            else
            {
                VGAGraphics.DrawFilledRect(X, Y, Width, Height, Color);
            }
        }

        private bool Intersect(int x, int y)
        {
            return (x > X && x < X + Width && y > Y && y < Y + Height);
        }

        public event EventHandler MouseClick;

        protected virtual void OnMouseClick(EventArgs e)
        {
            EventHandler handler = MouseClick;
            handler?.Invoke(this, e);
        }
    }
}
