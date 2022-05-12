using BenMakesGames.PlayPlayMini.Model;
using BenMakesGames.PlayPlayMini.UI.Services;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BenMakesGames.PlayPlayMini.UI.UIElements;

public class Button : IUIElement
{
    public UIService UI { get; private set; }

    public string Label { get; set; }

    public int X { get; set; }
    public int Y { get; set; }
    public bool Visible { get; set; } = true;
    public bool Enabled { get; set; } = true;
    public int Width => ForcedWidth ?? (Label.Length * UI.Font.CharacterWidth + 6);
    public int Height => UI.Graphics.SpriteSheets[UI.Theme.Theme.ButtonSpriteSheetName].SpriteHeight;

    private int? ForcedWidth { get; }

    public IUIElement.ClickDelegate ClickHandler { get; set; }

    public IUIElement.ClickDelegate DoClick => (clickX, clickY) => {
        if (Enabled && ClickHandler != null)
            ClickHandler(clickX, clickY);
    };

    public IUIElement.ClickDelegate DoDoubleClick { get; set; }

    public IUIElement.MouseEnterDelegate DoMouseEnter { get; set; }
    public IUIElement.MouseExitDelegate DoMouseExit { get; set; }

    public IReadOnlyList<IUIElement> Children => new List<IUIElement>();

    public Button(UIService ui, int x, int y, string label, IUIElement.ClickDelegate clickHandler)
    {
        UI = ui;
        X = x;
        Y = y;
        Label = label;
        ClickHandler = clickHandler;
    }

    public Button(UIService ui, int x, int y, string label, int width, IUIElement.ClickDelegate clickHandler)
    {
        UI = ui;
        X = x;
        Y = y;
        Label = label;
        ForcedWidth = width;
        ClickHandler = clickHandler;
    }

    public void Draw(int xOffset, int yOffset, GameTime gameTime)
    {
        int spiteIndexOffset = Enabled ? 0 : 3;

        SpriteSheet button = UI.Graphics.SpriteSheets[UI.Theme.Theme.ButtonSpriteSheetName];

        // left edge
        UI.Graphics.DrawSprite(button, X + xOffset, Y + yOffset, spiteIndexOffset + 0);

        // middle
        UI.Graphics.DrawSpriteStretched(button, X + button.SpriteWidth + xOffset, Y + yOffset, Width - button.SpriteWidth * 2, button.SpriteHeight, spiteIndexOffset + 1);

        // right edge
        UI.Graphics.DrawSprite(button, X + Width - button.SpriteWidth + xOffset, Y + yOffset, spiteIndexOffset + 2);

        UI.Graphics.DrawText(UI.Font, X + (Width - Label.Length * UI.Font.CharacterWidth) / 2 + xOffset, Y + 4 + yOffset, Label, Enabled ? UI.Theme.Theme.ButtonLabelColor : UI.Theme.Theme.ButtonLabelDisabledColor);
    }
}