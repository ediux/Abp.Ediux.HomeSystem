using Ediux.HomeSystem.Miscellaneous;
using Ediux.HomeSystem.Models.DTOs.AutoSave;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Controllers
{
    [ApiController]
    [Route("api/autosave")]
    public class AutoSaveController : HomeSystemController
    {
        protected readonly IMiscellaneousAppService miscellaneousAppService;
        public AutoSaveController(IMiscellaneousAppService miscellaneousAppService)
        {
            this.miscellaneousAppService = miscellaneousAppService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async virtual Task<IActionResult> CreateAsync(AutoSaveDTO input)
        {
            try
            {
                var result = await miscellaneousAppService.CreateAsync(input);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(new { error = new { message = ex.Message } });
            }
         
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async virtual Task<IActionResult> DeleteAsync(AutoSaveDTO input)
        {
            await miscellaneousAppService.RemoveAutoSaveDataAsync(input);
            return NoContent();
        }

    }
}
