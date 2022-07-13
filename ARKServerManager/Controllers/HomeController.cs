using ARKServerManager.Database;
using ARKServerManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace ARKServerManager.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext _db;

        public HomeController(DatabaseContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            return View(GetServers());
        }
        private List<Server> GetServers()
        {
            return _db.Server.ToList();
        }
    }
}
