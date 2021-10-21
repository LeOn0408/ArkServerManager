using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ArkWeb.Pages
{
    [Authorize]
    public class IssuesModel : PageModel
    {
        private readonly ILogger<IssuesModel> _logger;



        public IssuesModel(ILogger<IssuesModel> logger)
        {
            _logger = logger;

        }

        //public IActionResult Index()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return Content(User.Identity.Name);
        //    }
        //    return Content("�� ����������������");
        //}
        public string Message { get; set; }
        [BindProperty]
        public Issue Issue { get; set; }
        public void OnGet()
        {
            Message = "������� ��������";
        }
        public void OnPost()
        {
            Message = $"���: {Issue.Name}  ��������: {Issue.Problem}";
        }
    }
}
