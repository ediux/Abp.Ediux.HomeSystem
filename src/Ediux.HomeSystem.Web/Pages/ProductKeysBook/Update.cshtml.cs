using Ediux.HomeSystem.Models.DTOs.ProductKeysBook;
using Ediux.HomeSystem.ProductKeysBook;
using Ediux.HomeSystem.Web.Models.ProductKeysBook;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace Ediux.HomeSystem.Web.Pages.ProductKeysBook
{
    public class UpdateModel : HomeSystemPageModel
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
        public ProductKeysBookUpdateViewModel ViewModel { get; set; }

        [BindProperty]
        [DisplayName("新增欄位")]
        public string AddFieldName { get; set; }

        [BindProperty]
        [DisplayName("欄位內容")]
        public string NewFieldValue { get; set; }

        [TempData(Key = "TempExtraProperties")]
        public string TempExtraProperties { get; set; }


        public UpdateModel(IProductKeysBookService productKeysBookService)
        {
            _productKeysBookService = productKeysBookService;
            ViewModel = new ProductKeysBookUpdateViewModel();
        }


        public async Task<IActionResult> OnGetAsync(Guid? Id, ProductKeysBookUpdateViewModel model)
        {
            string jsondata = TempExtraProperties;

            if (Id.HasValue == false)
            {
                if (model != null)
                {
                    ViewModel = model;
                    if (!string.IsNullOrWhiteSpace(jsondata))
                    {
                        var extdata = System.Text.Json.JsonSerializer.Deserialize<ExtraPropertyDictionary>(jsondata);

                        foreach (var key in extdata.Keys)
                        {
                            ViewModel.SetProperty(key, extdata[key]);
                        }
                    }
                    return Page();
                }
                else
                {
                    return BadRequest(new { error = new { message = $"'{nameof(Id)}' can't be null or empty." } });
                }
            }

            var entity = await _productKeysBookService.GetAsync(Id.Value);

            if (entity != null)
            {
                ViewModel = ObjectMapper.Map<ProductKeysBookDTO, ProductKeysBookUpdateViewModel>(entity);

                if (!string.IsNullOrWhiteSpace(jsondata))
                {
                    var extdata = System.Text.Json.JsonSerializer.Deserialize<ExtraPropertyDictionary>(jsondata);

                    foreach (var key in extdata.Keys)
                    {
                        ViewModel.SetProperty(key, extdata[key]);
                    }
                }
                else
                {
                    entity.MapExtraPropertiesTo(ViewModel, MappingPropertyDefinitionChecks.None);
                }
                return Page();
            }

            return NotFound();
        }

        public IActionResult OnPostAddField()
        {
            ViewModel.ExtraProperties.Add(AddFieldName, NewFieldValue);
            TempExtraProperties = System.Text.Json.JsonSerializer.Serialize(ViewModel.ExtraProperties);
            return RedirectToPage();
        }
        public IActionResult OnPostRemoveField(string name)
        {
            ViewModel?.ExtraProperties.Remove(name);
            TempExtraProperties = System.Text.Json.JsonSerializer.Serialize(ViewModel.ExtraProperties);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostAsync(Guid Id)
        {
            var dto = ObjectMapper.Map<ProductKeysBookUpdateViewModel, ProductKeysBookDTO>(ViewModel);
            ViewModel.MapExtraPropertiesTo(dto);
            await _productKeysBookService.UpdateAsync(Id, dto);
            return RedirectToPage("./Index");
        }
    }
}
