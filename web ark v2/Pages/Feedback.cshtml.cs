using ArkWeb.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArkWeb.Pages
{
    public class FeedbackInput
    {
        [BindProperty]
        [Required(ErrorMessage = "Не указано имя пользователя")]
        [Display(Name = "Выберите сервер")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Опишите возникшую проблему или предложение.")]
        public string Desc { get; set; }

        [BindProperty]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Укажите вашу эл/почту для связи")]
        public string Email { get; set; }

    }
    public class FeedbackModel : PageModel
    {
        private readonly ILogger<FeedbackModel> _logger;
        private readonly ApplicationContext _db;

        [BindProperty]
        public FeedbackInput FeedbackInput { get; set; }

        [BindProperty]
        public int FeedbackTypeId { get; set; }  

        [BindProperty]
        public List<SelectListItem> FeedbackTypeList { get; set; } = new();

        private readonly List<ArkFeedbackType> _feedbackTypes;

        public FeedbackModel(ILogger<FeedbackModel> logger, ApplicationContext db)
        {
            _logger = logger;
            _db = db;
            _feedbackTypes = _db.ArkFeedbackTypes.ToList();

        }

        public void OnGet()
        {
            FeedbackTypeList.AddRange(_feedbackTypes.Select(feedbackType => new SelectListItem() { Value = feedbackType.Id.ToString(), Text = feedbackType.Name }));
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                ArkFeedback arkFeedback = new()
                {
                    FeebackType = FeedbackTypeId,
                    Username = FeedbackInput.Name,
                    Email = FeedbackInput.Email,
                    Description = FeedbackInput.Desc
                };
                _db.ArkFeedback.Add(arkFeedback);
                _db.SaveChanges();
                return RedirectToPage("/result/feedback");
            }

            FeedbackTypeList.AddRange(_feedbackTypes.Select(feedbackType => new SelectListItem() { Value = feedbackType.Id.ToString(), Text = feedbackType.Name }));
            return Page();
        }


    }
}
