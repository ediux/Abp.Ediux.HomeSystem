using Ediux.HomeSystem.Models.DTOs.PassworkBook;
using Ediux.HomeSystem.PassworkBook;
using Ediux.HomeSystem.Web.Models.PasswordBook;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace Ediux.HomeSystem.Web.Pages.PasswordBook
{
    public class UpdateModel : HomeSystemPageModel
    {
        private readonly IPassworkBookService _passworkBookService;
       
        [BindProperty]
        public PasswordBookUpdateViewModel ViewModel { get; set; }

        [BindProperty]
        [DisplayName("新增欄位")]
        public string AddFieldName { get; set; }

        [BindProperty]
        [DisplayName("欄位內容")]
        public string NewFieldValue { get; set; }

        [TempData(Key = "TempExtraProperties")]
        public string TempExtraProperties { get; set; }


        public UpdateModel(IPassworkBookService passworkBookService)
        {
            _passworkBookService = passworkBookService;
            ViewModel = new PasswordBookUpdateViewModel();
        }


        public async Task<IActionResult> OnGetAsync(long? Id, PasswordBookUpdateViewModel model)
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

            var entity = await _passworkBookService.GetAsync(Id.Value);

            if (entity != null)
            {
                ViewModel = ObjectMapper.Map<PassworkBookDTO, PasswordBookUpdateViewModel>(entity);

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
        public async Task<IActionResult> OnPostAsync(long Id)
        {
            var dto = ObjectMapper.Map<PasswordBookUpdateViewModel, PassworkBookDTO>(ViewModel);
            ViewModel.MapExtraPropertiesTo(dto);
            await _passworkBookService.UpdateAsync(Id, dto);
            return RedirectToPage("./Index");
        }
    }
}
