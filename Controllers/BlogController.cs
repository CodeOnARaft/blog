using JournalBlog.Services;
using Microsoft.AspNetCore.Mvc;

namespace JournalBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var pagedPosts = await _blogService.GetPagedPostsAsync(page, 10);
            return View(pagedPosts);
        }

        public async Task<IActionResult> Post(string date, int page = 1)
        {
            if (string.IsNullOrEmpty(date))
                return NotFound();

            var post = await _blogService.GetPostByDateAsync(date);
            if (post == null)
                return NotFound();

            ViewBag.ReturnPage = page;
            return View(post);
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Tag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
                return NotFound();

            var posts = await _blogService.GetPostsByTagAsync(tag);
            ViewBag.Tag = tag;
            return View("Index", posts);
        }
    }
}