using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArkWeb.Models.Account
{
    [Authorize]
    public class InventoryModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
