namespace JournalBlog.Models
{
    public enum Theme
    {
        Light,
        Dark,
        Terminal,
        Cyberpunk
    }

    public class ThemeInfo
    {
        public Theme Theme { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CssClass { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}