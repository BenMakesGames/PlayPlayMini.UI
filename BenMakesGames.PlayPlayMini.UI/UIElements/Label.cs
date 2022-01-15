using BenMakesGames.PlayPlayMini.Model;
using BenMakesGames.PlayPlayMini.UI.Services;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenMakesGames.PlayPlayMini.UI.UIElements
{
    public class Label : IUIElement
    {
        public UIService UI { get; set; }

        public string Text { get; private set; }

        public int X { get; set; }
        public int Y { get; set; }
        public bool Visible { get; set; } = true;
        public virtual int Width => ForcedWidth ?? (Text.Length * UI.Font.CharacterWidth + 1);
        public virtual int Height => UI.Font.CharacterHeight;
        public Color Color { get; set; }

        protected int? ForcedWidth { get; }

        public IUIElement.ClickDelegate DoClick { get; set; }
        public IUIElement.ClickDelegate DoDoubleClick { get; set; }
        public IUIElement.MouseEnterDelegate DoMouseEnter { get; set; }
        public IUIElement.MouseExitDelegate DoMouseExit { get; set; }

        public IReadOnlyList<IUIElement> Children => new List<IUIElement>();

        public Label(UIService ui, int x, int y, string text, Color color)
        {
            UI = ui;

            X = x;
            Y = y;
            Text = text;
            Color = color;
        }

        public Label(UIService ui, int x, int y, string text, int width, Color color)
        {
            UI = ui;

            X = x;
            Y = y;
            Text = text;
            ForcedWidth = width;
            Color = color;
        }

        virtual public void Draw(int xOffset, int yOffset, GameTime gameTime)
        {
            UI.Graphics.DrawText(UI.Font, X + (Width - Text.Length * UI.Font.CharacterWidth) / 2 + xOffset, Y + yOffset, Text, Color);
        }
    }
}
