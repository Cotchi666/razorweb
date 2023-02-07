using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace razorwebef.Pages.Blog
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ArticleContext _context;

        public IndexModel(ArticleContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get; set; } 

        public const int ITEMS_PER_PAGE = 1;
        // cho phep lay data tu parameter voi tham so la "p"
        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }
        public int countPages { get; set; }

        public string SearchString { get; set; }
        public async Task OnGetAsync()
        {
            System.Console.WriteLine("vao get");
            int totalArticle = await _context.Article.CountAsync();
            countPages = (int)Math.Ceiling((double)totalArticle / ITEMS_PER_PAGE);
            System.Console.WriteLine("vao get countPages" + countPages);
            if (currentPage < 1)
                currentPage = 1;
            if (currentPage > countPages)
                currentPage = countPages;

            // Truy vấn lấy các Article
            var articles = (from a in _context.Article
                            orderby a.PublishDate descending
                            select a)
                                .Skip((currentPage - 1) * ITEMS_PER_PAGE)
                             .Take(ITEMS_PER_PAGE);

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
