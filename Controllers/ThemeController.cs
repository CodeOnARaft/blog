using JournalBlog.Models;
using JournalBlog.Services;
using Microsoft.AspNetCore.Mvc;

namespace JournalBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThemeController : ControllerBase
    {
        private readonly ThemeService _themeService;

        public ThemeController(ThemeService themeService)
        {
            _themeService = themeService;
        }

        [HttpGet]
        public ActionResult<List<ThemeInfo>> GetThemes()
        {
            return _themeService.GetAllThemes();
        }

        [HttpGet("default")]
        public ActionResult<ThemeInfo> GetDefaultTheme()
        {
            return _themeService.GetDefaultTheme();
        }
    }
}