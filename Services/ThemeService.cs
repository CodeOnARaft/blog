using JournalBlog.Models;

namespace JournalBlog.Services
{
    public class ThemeService
    {
        private static readonly List<ThemeInfo> _themes = new()
        {
            new ThemeInfo { Theme = Theme.Light, Name = "Light", CssClass = "theme-light", Icon = "☀️" },
            new ThemeInfo { Theme = Theme.Dark, Name = "Dark", CssClass = "theme-dark", Icon = "🌙" },
            new ThemeInfo { Theme = Theme.Terminal, Name = "Terminal", CssClass = "theme-terminal", Icon = "💻" },
            new ThemeInfo { Theme = Theme.Cyberpunk, Name = "Cyberpunk", CssClass = "theme-cyberpunk", Icon = "🌆" }
        };

        public List<ThemeInfo> GetAllThemes()
        {
            return _themes;
        }

        public ThemeInfo GetThemeInfo(Theme theme)
        {
            return _themes.FirstOrDefault(t => t.Theme == theme) ?? _themes.First();
        }

        public ThemeInfo GetDefaultTheme()
        {
            return _themes.First(); // Light theme
        }
    }
}