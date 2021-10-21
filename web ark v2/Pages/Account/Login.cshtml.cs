using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ArkWeb.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<ArkUsers> _userManager;
        private readonly SignInManager<ArkUsers> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ArkUsers> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ArkUsers> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string Login { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Запомнить меня?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Очистите существующий внешний файл cookie, чтобы обеспечить чистый процесс входа в систему
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // Это не учитывает ошибки входа в систему для блокировки учетной записи
                // Чтобы включить сбой пароля для запуска блокировки учетной записи, установите lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Login, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Пользователь вошел в систему.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Пользователь заблокирован.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неверная попытка входа в систему.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        public async Task<IActionResult> OnGetByLogoutAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return LocalRedirect(returnUrl);
            //return RedirectToPage("../index");

        }
    }
}
