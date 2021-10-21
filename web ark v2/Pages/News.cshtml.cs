using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ArkWeb.Pages
{
    public class NewsModel : PageModel
    {
        private readonly ApplicationContext _context;

        public NewsModel(ApplicationContext db)
        {
            _context = db;
        }
        public int Id { get; set; }
        public List<ArkNews> News { get; set; }

        public void OnGet(int id)
        {
            Id = id;
            News = _context.View_ArkNews.AsNoTracking().ToList();
            News = News.Where(x => x.Id == Id).ToList();
        }
    }
}
