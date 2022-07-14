using ARKServerManager.Database;
using ARKServerManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ARKServerManager.Pages;

public class IndexModel : PageModel
{
    private readonly DatabaseContext _db;

    public List<Server> Servers { get; private set; }
    public IndexModel(DatabaseContext db)
    {
        _db = db;
    }
    public void OnGet()
    {
       Servers = _db.Server.ToList();
    }
}
