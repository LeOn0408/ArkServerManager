using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace ArkWeb.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ArkUsers> _userManager;
        private readonly SignInManager<ArkUsers> _signInManager;
        

        public RegisterModel(UserManager<ArkUsers> userManager, SignInManager<ArkUsers> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public ArkUsers ArkUser { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync(string Pass)
        {
            if (ModelState.IsValid)
            {
                ArkUsers user = new ArkUsers { Email = ArkUser.Email, UserName = ArkUser.UserName};
                

                // добавляем пользователя
                if (Pass == ArkUser.UsPass)
                {
                    var result = await _userManager.CreateAsync(user, ArkUser.UsPass);
                    if (result.Succeeded)
                    {
                        // установка куки
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                
            }
            return Page();
        }
    }
}
