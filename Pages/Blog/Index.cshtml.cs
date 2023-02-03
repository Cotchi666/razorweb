using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace razorwebef.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly ArticleContext _context;

        public IndexModel(ArticleContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public async Task OnGetAsync()
        {

            // Truy vấn lấy các Article
            var articles = from a in _context.Article orderby a.PublishDate descending select a;
            if (!string.IsNullOrEmpty(SearchString))
            {
                Console.WriteLine(SearchString);
                // Truy vấn lọc các Article mà tiêu đề chứa chuỗi tìm kiếm
                Article = articles.Where(article => article.Title.Contains(SearchString)).ToList();
            }
            else
            {
                Article = await articles.ToListAsync();
            }
            // Đọc (nạp) Article

        }
    }
}
