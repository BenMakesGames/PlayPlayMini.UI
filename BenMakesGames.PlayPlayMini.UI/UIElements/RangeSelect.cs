﻿using BenMakesGames.PlayPlayMini.UI.Services;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BenMakesGames.PlayPlayMini.UI.UIElements;

public class RangeSelect: IUIElement
{
    public UIService UI { get; set; }

    public string Label => Labels[Value];

    public int X { get; set; }
    public int Y { get; set; }
    public bool Visible { get; set; } = true;
    public int Width { get;}
    public int Height => Math.Max(
        UI.Graphics.SpriteSheets[UI.Theme.Theme.ButtonSpriteSheetName].SpriteHeight,
        UI.Font.CharacterHeight
    );

    private bool _enabled = true;

    public bool Enabled
    {
        get
        {
            return _enabled;
        }

        set
        {
            _enabled = value;
            EnableOrDisableButtons();
        }
    }

    private bool ValueWrap { get; set; } = false;

    public int Value { get; private set; }
        
    private IList<string> Labels { get; }

    public IUIElement.ClickDelegate DoClick { get; set; }
    public IUIElement.ClickDelegate DoDoubleClick { get; set; }
    public IUIElement.MouseEnterDelegate DoMouseEnter { get; set; }
    public IUIElement.MouseExitDelegate DoMouseExit { get; set; }

    private RangeChangeDelegate ChangeHandler { get; set; }

    public IReadOnlyList<IUIElement> Children { get; }

    public delegate int RangeChangeDelegate(int proposedValue);

    private Button Decrement { get; }
    private Button Increment { get; }

    public RangeSelect(UIService ui, int x, int y, int width, int initialValue, IList<string> valueLabels, RangeChangeDelegate changeHandler)
    {
        UI = ui;
        X = x;
        Y = y;
        Width = width;
        Labels = valueLabels;
        Value = initialValue;

        ChangeHandler = changeHandler;

        Decrement = new Button(UI, 0, 0, "<", 16, DecrementValue);
        Increment = new Button(UI, Width - 16, 0, ">", 16, IncrementValue);

        EnableOrDisableButtons();

        Children = new List<IUIElement>() { Increment, Decrement };
    }

    public void EnableValueWrap(bool wrap)
    {
        ValueWrap = wrap;
        EnableOrDisableButtons();
    }

    public void ForceValue(int value)
    {
        Value = value;
        EnableOrDisableButtons();
    }

    public void Draw(int xOffset, int yOffset, GameTime gameTime)
    {
        UI.Graphics.DrawText(UI.GetFont(), X + 18 + xOffset, Y + 4 + yOffset, Label, Color.Black);
    }

    private void EnableOrDisableButtons()
    {
        Decrement.Enabled = Enabled && (ValueWrap || Value > 0);
        Increment.Enabled = Enabled && (ValueWrap || Value < Labels.Count - 1);
    }

    private void DecrementValue(int x, int y)
    {
        if(Value > 0)
        {
            Value = ChangeHandler(Value - 1);
        }
        else if(ValueWrap)
        {
            Value = ChangeHandler(Labels.Count - 1);
        }

        EnableOrDisableButtons();
    }

    private void IncrementValue(int x, int y)
    {
        if(Value < Labels.Count - 1)
        {
            Value = ChangeHandler(Value + 1);
        }
        else if(ValueWrap)
        {
            Value = ChangeHandler(0);
        }

        EnableOrDisableButtons();
    }
}