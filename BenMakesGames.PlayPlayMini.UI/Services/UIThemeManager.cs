using BenMakesGames.PlayPlayMini.Attributes.DI;
using BenMakesGames.PlayPlayMini.UI.Model;

namespace BenMakesGames.PlayPlayMini.UI.Services
{
    [AutoRegister(Lifetime.Singleton)]
    public class UIThemeManager
    {
        public ITheme Theme { get; set; }
    }
}
