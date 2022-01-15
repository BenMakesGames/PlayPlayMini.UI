﻿using BenMakesGames.PlayPlayMini.Model;
using BenMakesGames.PlayPlayMini.UI.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenMakesGames.PlayPlayMini.UI.UIElements
{
    public class LabelWithIcon: IUIElement
    {
        public UIService UI { get; set; }

        public string Label { get; private set; }

        public int X { get; set; }
        public int Y { get; set; }
        public bool Visible { get; set; } = true;
        public int Width => Text.Length * UI.Font.CharacterWidth + 1 + 2 + SpriteRectangle.Width;
        public int Height => Math.Max(SpriteRectangle.Height, UI.Font.CharacterHeight);
        public IReadOnlyList<IUIElement> Children => new List<IUIElement>();

        private Texture2D Texture { get; }
        private Rectangle SpriteRectangle { get; }
        public string Text { get; set; }
        public Color TextColor { get; set; }

        public IUIElement.ClickDelegate DoClick { get; set; }
        public IUIElement.ClickDelegate DoDoubleClick { get; set; }
        public IUIElement.MouseEnterDelegate DoMouseEnter { get; set; }
        public IUIElement.MouseExitDelegate DoMouseExit { get; set; }

        public LabelWithIcon(UIService ui, int x, int y, string text, Color textColor, Texture2D texture, Rectangle spriteRectangle)
        {
            UI = ui;
            X = x;
            Y = y;
            Texture = texture;
            SpriteRectangle = spriteRectangle;
            Text = text;
            TextColor = textColor;
        }

        public LabelWithIcon(UIService ui, int x, int y, string text, Color textColor, SpriteSheet ss, int spriteIndex)
        {
            UI = ui;
            X = x;
            Y = y;
            Texture = ss.Texture;
            SpriteRectangle = new Rectangle((spriteIndex % ss.Columns) * ss.SpriteWidth, (spriteIndex / ss.Columns) * ss.SpriteHeight, ss.SpriteWidth, ss.SpriteHeight);
            Text = text;
            TextColor = textColor;
        }

        public void Draw(int xOffset, int yOffset, GameTime gameTime)
        {
            UI.Graphics.DrawPicture(Texture, X + xOffset, Y + yOffset, SpriteRectangle);
            UI.Graphics.DrawText(UI.Font, X + xOffset + SpriteRectangle.Width + 2, Y + yOffset + (SpriteRectangle.Height - UI.Font.CharacterHeight) / 2, Text, TextColor);
        }
    }
}
