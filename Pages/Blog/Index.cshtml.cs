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

        public IList<Article> Article { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Article != null)
            {
                Article = await _context.Article.ToListAsync();
            }
        }
    }
}
