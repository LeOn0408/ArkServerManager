using ARKServerManager.Database;
using ARKServerManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ARKServerManager.Pages;

public class IndexModel : PageModel
{
    public IndexModel(DatabaseContext db)
    {
        
    }
    
}
