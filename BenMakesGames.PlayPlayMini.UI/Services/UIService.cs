using BenMakesGames.PlayPlayMini.Attributes.DI;
using BenMakesGames.PlayPlayMini.Model;
using BenMakesGames.PlayPlayMini.Services;
using BenMakesGames.PlayPlayMini.UI.Model;
using BenMakesGames.PlayPlayMini.UI.UIElements;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BenMakesGames.PlayPlayMini.UI.Services
{
    [AutoRegister(Lifetime.PerDependency)]
    public class UIService
    {
        public GraphicsManager Graphics { get; }
        public UIThemeManager Theme { get; }
        private MouseManager Cursor { get; }

        public Font Font => Graphics.Fonts[Theme.Theme.FontName];

        public Canvas Canvas { get; }

        public bool DebugMode { get; set; } = false;

        private Click PreviousClick;
        private Click? MouseDown;

        public IUIElement Hovered { get; private set; } = null;

        public UIService(
            GraphicsManager gm, MouseManager cursor, UIThemeManager theme
        )
        {
            Graphics = gm;
            Cursor = cursor;
            Theme = theme;

            Canvas = new Canvas(this)
            {
                X = 0,
                Y = 0,
                Width = gm.Width,
                Height = gm.Height,
                UI = this
            };
        }

        public void AlwaysDraw(GameTime gameTime)
        {
            DrawElement(Canvas, 0, 0, gameTime);
        }

        private void DrawElement(IUIElement e, int xOffset, int yOffset, GameTime gameTime)
        {
            e.Draw(xOffset, yOffset, gameTime);

            foreach (IUIElement c in e.Children.Where(c => c.Visible))
                DrawElement(c, xOffset + e.X, yOffset + e.Y, gameTime);

            if (e == Hovered && DebugMode)
                Graphics.DrawRectangle(e.X + xOffset, e.Y + yOffset, e.Width, e.Height, Color.Red);
        }

        public void ActiveDraw(GameTime gameTime, bool showMouse = true)
        {
            if (showMouse)
                Cursor.ActiveDraw(gameTime);
        }

        public void ActiveUpdate(GameTime gameTime)
        {
            DoMouseOverElement(Canvas, Cursor.X, Cursor.Y);

            if (Cursor.LeftDown)
            {
                if (MouseDown == null)
                {
                    MouseDown = new Click()
                    {
                        X = Cursor.X,
                        Y = Cursor.Y,
                        When = gameTime.TotalGameTime,
                        What = Hovered
                    };
                }
            }
            else
            {
                if (MouseDown is Click click)
                {
                    if (PointsWithinDistance(click.X, click.Y, Cursor.X, Cursor.Y, 2))
                    {
                        bool isDoubleClick = (gameTime.TotalGameTime - PreviousClick.When).TotalMilliseconds <= 500;

                        if (isDoubleClick)
                        {
                            if (click.What == PreviousClick.What && PointsWithinDistance(click.X, click.Y, PreviousClick.X, PreviousClick.Y, 3))
                                Hovered.DoDoubleClick?.Invoke(click.X, click.Y);

                            PreviousClick = new Click();
                        }
                        else
                        {
                            Hovered.DoClick?.Invoke(click.X, click.Y);
                            PreviousClick = click;
                        }
                    }

                    MouseDown = null;
                }
            }
        }

        private bool PointsWithinDistance(int x1, int y1, int x2, int y2, int maxDistance)
        {
            return (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) <= maxDistance * maxDistance;
        }

        private void DoMouseOverElement(IUIElement e, int x, int y)
        {
            foreach (IUIElement c in e.Children.Where(c => c.Visible).Reverse())
            {
                if (x >= c.X && x < c.X + c.Width && y >= c.Y && y < c.Y + c.Height)
                {
                    DoMouseOverElement(c, x - c.X, y - c.Y);
                    return;
                }
            }

            if (e != Hovered && Hovered != null)
            {
                Hovered.DoMouseExit?.Invoke();
            }

            Hovered = e;

            Hovered.DoMouseEnter?.Invoke();
        }

        public Color GetContrastingBlackOrWhite(Color c)
        {
            float l = c.R * 0.2126f + c.G * 0.7152f + c.B * 0.0722f;

            return l < 96 ? Color.White : Color.Black;
        }

        public Font GetFont()
        {
            return Graphics.Fonts[Theme.Theme.FontName];
        }
    }
}
