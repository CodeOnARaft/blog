using JournalBlog.Models;
using System.Text.Json;
using Markdig;
using System.Text.RegularExpressions;

namespace JournalBlog.Services
{
    public class BlogService
    {
        private readonly string _postsPath;
        private readonly MarkdownPipeline _markdownPipeline;

        public BlogService(IWebHostEnvironment env)
        {
            _postsPath = Path.Combine(env.ContentRootPath, "posts");
            _markdownPipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
        }

        public async Task<List<BlogPost>> GetAllPostsAsync()
        {
            var posts = new List<BlogPost>();
            
            if (!Directory.Exists(_postsPath))
                return posts;

            var markdownFiles = Directory.GetFiles(_postsPath, "*.md")
                .OrderByDescending(f => Path.GetFileNameWithoutExtension(f))
                .ToList();

            foreach (var file in markdownFiles)
            {
                try
                {
                    var post = await ParseMarkdownPostAsync(file);
                    if (post != null)
                    {
                        posts.Add(post);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading post {file}: {ex.Message}");
                }
            }

            return posts;
        }

        public async Task<List<BlogPost>> GetRecentPostsAsync(int count = 10)
        {
            var allPosts = await GetAllPostsAsync();
            return allPosts.Take(count).ToList();
        }

        public async Task<PagedResult<BlogPost>> GetPagedPostsAsync(int page = 1, int pageSize = 10)
        {
            var allPosts = await GetAllPostsAsync();
            var totalItems = allPosts.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            
            var pagedPosts = allPosts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<BlogPost>
            {
                Items = pagedPosts,
                CurrentPage = page,
                TotalPages = totalPages,
                TotalItems = totalItems,
                PageSize = pageSize
            };
        }

        public async Task<BlogPost?> GetPostByDateAsync(string date)
        {
            var filePath = Path.Combine(_postsPath, $"{date}.md");
            
            if (!File.Exists(filePath))
                return null;

            try
            {
                return await ParseMarkdownPostAsync(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading post {filePath}: {ex.Message}");
                return null;
            }
        }

        public async Task<List<string>> GetAllTagsAsync()
        {
            var posts = await GetAllPostsAsync();
            return posts.SelectMany(p => p.Tags)
                      .Distinct()
                      .OrderBy(t => t)
                      .ToList();
        }

        public async Task<List<BlogPost>> GetPostsByTagAsync(string tag)
        {
            var posts = await GetAllPostsAsync();
            return posts.Where(p => p.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
                       .ToList();
        }

        private async Task<BlogPost?> ParseMarkdownPostAsync(string filePath)
        {
            var content = await File.ReadAllTextAsync(filePath);
            
            // Parse front matter
            var frontMatterMatch = Regex.Match(content, @"^---\s*\n(.*?)\n---\s*\n(.*)", RegexOptions.Singleline);
            
            if (!frontMatterMatch.Success)
                return null;

            var frontMatter = frontMatterMatch.Groups[1].Value;
            var markdownContent = frontMatterMatch.Groups[2].Value;
            
            // Parse front matter fields
            var post = new BlogPost();
            
            foreach (var line in frontMatter.Split('\n'))
            {
                var colonIndex = line.IndexOf(':');
                if (colonIndex == -1) continue;
                
                var key = line.Substring(0, colonIndex).Trim();
                var value = line.Substring(colonIndex + 1).Trim();
                
                switch (key.ToLower())
                {
                    case "title":
                        post.Title = value.Trim('"', '\'');
                        break;
                    case "date":
                        if (DateTime.TryParse(value.Trim('"', '\''), out var date))
                            post.Date = date;
                        break;
                    case "author":
                        post.Author = value.Trim('"', '\'');
                        break;
                    case "summary":
                        post.Summary = value.Trim('"', '\'');
                        break;
                    case "tags":
                        // Parse array format ["tag1", "tag2", "tag3"]
                        var tagsMatch = Regex.Match(value, @"\[(.*?)\]");
                        if (tagsMatch.Success)
                        {
                            var tagsContent = tagsMatch.Groups[1].Value;
                            post.Tags = tagsContent.Split(',')
                                .Select(t => t.Trim().Trim('"', '\''))
                                .Where(t => !string.IsNullOrEmpty(t))
                                .ToList();
                        }
                        break;
                }
            }
            
            // Convert markdown to HTML
            post.Content = Markdown.ToHtml(markdownContent, _markdownPipeline);
            
            return post;
        }
    }
}