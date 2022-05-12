using BenMakesGames.PlayPlayMini.UI.Services;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BenMakesGames.PlayPlayMini.UI.UIElements;

public interface IUIElement
{
    UIService UI { get; }

    int X { get; }
    int Y { get; }
    int Width { get; }
    int Height { get; }
    bool Visible { get; }
    IReadOnlyList<IUIElement> Children { get; }
        
    ClickDelegate DoClick { get; }
    ClickDelegate DoDoubleClick { get; }
    MouseEnterDelegate DoMouseEnter { get; }
    MouseExitDelegate DoMouseExit { get; }

    void Draw(int xOffset, int yOffset, GameTime gameTime);

    public delegate void ClickDelegate(int x, int y);
    public delegate void MouseEnterDelegate();
    public delegate void MouseExitDelegate();
}