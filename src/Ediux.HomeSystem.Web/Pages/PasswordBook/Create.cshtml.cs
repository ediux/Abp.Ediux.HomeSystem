using Ediux.HomeSystem.Models.DTOs.PassworkBook;
using Ediux.HomeSystem.PassworkBook;
using Ediux.HomeSystem.Web.Models.PasswordBook;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.Threading.Tasks;
using Volo.Abp.Data;

namespace Ediux.HomeSystem.Web.Pages.PasswordBook
{
    public class CreateModel : HomeSystemPageModel
    {
        private readonly IPassworkBookService _passworkBookService;

        [BindProperty]
        public PasswordBookCreateViewModel ViewModel { get; set; }

        [BindProperty]
        [DisplayName("新增欄位")]
        public string AddFieldName { get; set; }

        [BindProperty]
        [DisplayName("欄位內容")]
        public string NewFieldValue { get; set; }

        [TempData(Key = "TempExtraProperties")]
        public string TempExtraProperties { get; set; }

        public CreateModel(IPassworkBookService passworkBookService)
        {
            _passworkBookService = passworkBookService;
            ViewModel = new PasswordBookCreateViewModel();
        }

        public IActionResult OnGet(PasswordBookCreateViewModel model)
        {
            string jsondata = TempExtraProperties;

            if (model != null)
            {
                ViewModel = model;
            }

            if (!string.IsNullOrWhiteSpace(jsondata))
            {
                if (!string.IsNullOrWhiteSpace(jsondata))
                {
                    var extdata = System.Text.Json.JsonSerializer.Deserialize<ExtraPropertyDictionary>(jsondata);

                    foreach (var key in extdata.Keys)
                    {
                        ViewModel.SetProperty(key, extdata[key]);
                    }
                }
            }

            return Page();
        }

        public IActionResult OnPostAddField()
        {
            ViewModel.ExtraProperties.Add(AddFieldName, NewFieldValue);
            TempExtraProperties = System.Text.Json.JsonSerializer.Serialize(ViewModel.ExtraProperties);
            return RedirectToPage(ViewModel);
        }

        public IActionResult OnPostRemoveField(string name)
        {
            ViewModel?.ExtraProperties.Remove(name);
            TempExtraProperties = System.Text.Json.JsonSerializer.Serialize(ViewModel.ExtraProperties);
            return RedirectToPage(ViewModel);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _passworkBookService.CreateAsync(ObjectMapper.Map<PasswordBookCreateViewModel, PassworkBookDTO>(ViewModel));

            return RedirectToPage("./Index");
        }
    }
}
