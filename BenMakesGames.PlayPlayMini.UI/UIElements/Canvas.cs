using BenMakesGames.PlayPlayMini.UI.Services;
using Microsoft.Xna.Framework;

namespace BenMakesGames.PlayPlayMini.UI.UIElements;

public class Canvas: UIContainer, IUIElement
{
    public UIService UI { get; set; }

    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool Visible { get; set; } = true;

    public Color BackgroundColor { get; set; } = Color.Transparent;

    public IUIElement.ClickDelegate DoClick { get; set; }
    public IUIElement.ClickDelegate DoDoubleClick { get; set; }
    public IUIElement.MouseEnterDelegate DoMouseEnter { get; set; }
    public IUIElement.MouseExitDelegate DoMouseExit { get; set; }

    public Canvas(UIService ui) : base()
    {
        UI = ui;
    }

    public void Draw(int xOffset, int yOffset, GameTime gameTime)
    {
        if(BackgroundColor.A > 0)
            UI.Graphics.DrawFilledRectangle(X, Y, Width, Height, BackgroundColor);
    }
}