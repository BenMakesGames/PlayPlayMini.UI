using BenMakesGames.PlayPlayMini.UI.UIElements;
using System;

namespace BenMakesGames.PlayPlayMini.UI.Model;

public struct Click
{
    public TimeSpan When { get; set; }
    public IUIElement What { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}