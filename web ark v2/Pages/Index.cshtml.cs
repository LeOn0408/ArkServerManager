using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArkWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext _context;

        public IndexModel(ApplicationContext db)
        {
            _context = db;
        }

        public List<ArkNews> News { get; set; }
        
        
        public string Title { get; set; }
        public string Desc { get; set; }
        public int ID { get; set; }
        public string Date { get; set; }




        public void OnGet()
        {
            

            News = _context.View_ArkNews.AsNoTracking().ToList();
            if (News.Count>0)
            {
                News = News.OrderByDescending(x => x.Id).ToList();//сортируем
                News = News.Skip(0).Take(5).ToList();//страницы 5 новостей на одну
                                                     //Извлекаем последнюю новость
                Title = News.FirstOrDefault().Title;
                Desc = News.FirstOrDefault().Description;
                ID = News.FirstOrDefault().Id;
                Date = News.FirstOrDefault().DateNew.ToString("yyyy-MM-dd");
                //Исключаем последнюю новость
                News = News.Where(x => x.Id != ID).ToList();

            }
            

            //Where(u => u.Id == u.Id.Max).ToList();
            
        }


        public void OnPost(string sum)
        {

        }

        
    }
}
