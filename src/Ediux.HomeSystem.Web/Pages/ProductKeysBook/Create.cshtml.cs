using Ediux.HomeSystem.Models.DTOs.ProductKeysBook;
using Ediux.HomeSystem.ProductKeysBook;
using Ediux.HomeSystem.Web.Models.ProductKeysBook;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Volo.Abp.Data;

namespace Ediux.HomeSystem.Web.Pages.ProductKeysBook
{
    public class CreateModel : HomeSystemPageModel
    {
        private readonly IProductKeysBookService _productKeysBookService;

        public List<SelectListItem> SharedOptions
        {
            get
            {
                return new List<SelectListItem> {
                    new SelectListItem { Value = "Y", Text = "公開"},
                    new SelectListItem { Value = "N", Text = "私人"},
                };
            }
        }

        [BindProperty]
        public ProductKeysBookCreateViewModel ViewModel { get; set; }

        [BindProperty]
        [DisplayName("新增欄位")]
        public string AddFieldName { get; set; }

        [BindProperty]
        [DisplayName("欄位內容")]
        public string NewFieldValue { get; set; }

        [TempData(Key = "TempExtraProperties")]
        public string TempExtraProperties { get; set; }

        public CreateModel(IProductKeysBookService productKeysBookService)
        {
            _productKeysBookService = productKeysBookService;
            ViewModel = new ProductKeysBookCreateViewModel();
        }

        public IActionResult OnGet(ProductKeysBookCreateViewModel model)
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
            await _productKeysBookService.CreateAsync(ObjectMapper.Map<ProductKeysBookCreateViewModel, ProductKeysBookDTO>(ViewModel));

            return RedirectToPage("./Index");
        }
    }
}
