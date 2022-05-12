using Microsoft.Xna.Framework;

namespace BenMakesGames.PlayPlayMini.UI.Model;

public interface ITheme
{
    Color WindowColor { get; }
        
    string FontName { get; }
        
    string ButtonSpriteSheetName { get; }
    Color ButtonLabelColor { get; }
    Color ButtonLabelDisabledColor { get; }
        
    string CheckboxSpriteSheetName { get; }
}