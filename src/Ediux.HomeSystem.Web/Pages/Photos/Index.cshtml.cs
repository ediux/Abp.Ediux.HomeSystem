using Ediux.HomeSystem.Files;
using Ediux.HomeSystem.Models.DTOs.Files;
using Ediux.HomeSystem.Web.Models.Photos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Web.Pages.Photos
{
    public class IndexModel : HomeSystemPageModel
    {
        private readonly IFileStoreAppService _fileStoreAppService;

        public List<PhotoViewModel> Photos { get; set; }

        public IndexModel(IFileStoreAppService fileStoreAppService)
        {
            _fileStoreAppService = fileStoreAppService;
            Photos = new List<PhotoViewModel>();
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var photos = await _fileStoreAppService.GetPhotosAsync(new FileStoreRequestDTO());
            if (photos != null && photos.Any())
            {
                foreach (var photo in photos)
                {
                    Photos.Add(new PhotoViewModel()
                    {
                        Creator = photo.Creator,
                        CreatorDate = photo.CreatorDate,
                        CreatorId = photo.CreatorId,
                        Description = photo.Description,
                        Id = photo.Id,
                        URL = $"/api/simpleupload/download/{photo.Id}"
                    });
                }
            }
            return Page();
        }
    }
}
