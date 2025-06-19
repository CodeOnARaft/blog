namespace JournalBlog.Models
{
    public class BlogPost
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public string Author { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
    }
}