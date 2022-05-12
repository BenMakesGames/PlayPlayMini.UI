using BenMakesGames.PlayPlayMini.UI.Services;
using Microsoft.Xna.Framework;

namespace BenMakesGames.PlayPlayMini.UI.UIElements;

public class Window: UIContainer, IUIElement
{
    public UIService UI { get; set; }

    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool Visible { get; set; } = true;

    public string Title { get; set; }

    public IUIElement.ClickDelegate DoClick { get; set; }
    public IUIElement.ClickDelegate DoDoubleClick { get; set; }
    public IUIElement.MouseEnterDelegate DoMouseEnter { get; set; }
    public IUIElement.MouseExitDelegate DoMouseExit { get; set; }

    public Window(UIService ui, int x, int y, int w, int h, string title): base()
    {
        UI = ui;

        X = x;
        Y = y;
        Width = w;
        Height = h;
        Title = title;
    }

    public void Draw(int xOffset, int yOffset, GameTime gameTime)
    {
        UI.Graphics.DrawFilledRectangle(X, Y, Width, 12, UI.Theme.Theme.WindowColor, Color.Black);

        UI.Graphics.DrawText(UI.GetFont(), X + 4, Y + 2, Title, UI.GetContrastingBlackOrWhite(UI.Theme.Theme.WindowColor));

        UI.Graphics.DrawFilledRectangle(X, Y + 11, Width, Height - 11, Color.White, Color.Black);
    }
}