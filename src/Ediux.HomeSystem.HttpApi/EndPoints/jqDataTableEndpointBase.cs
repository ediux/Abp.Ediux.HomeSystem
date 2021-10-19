
using Ediux.HomeSystem.Models.DTOs.jqDataTables;
using Ediux.HomeSystem.Models.jqDataTables;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;

namespace Ediux.HomeSystem.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class jqDataTableEndpointBase<TService, TDTO, TKey, TCreateRequestDTO, TUpdateRequestDTO> : AbpController where TDTO : AuditedEntityDto<TKey>
        where TCreateRequestDTO : AuditedEntityDto<TKey>
        where TUpdateRequestDTO : AuditedEntityDto<TKey>
        where TService : ICrudAppService<TDTO, TKey,jqDTSearchedResultRequestDto>
    {
        protected readonly TService crudService;

        public jqDataTableEndpointBase(TService crudAppService) : base()
        {
            crudService = crudAppService;
        }

        [HttpPost]
        [Route("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async virtual Task<OkObjectResult> Get([FromBody] jqDataTableRequest input)
        {
            string sorting = string.Join(",", input.order.Where(w => !string.IsNullOrWhiteSpace(w.columnName)).Select(s => s.columnName + " " + s.dir).ToArray());

            var result = await crudService.GetListAsync(new jqDTSearchedResultRequestDto() { Sorting = sorting, Search = input.search.value });
            return Ok(new jqDataTableResponse<TDTO>(input, result));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async virtual Task<OkObjectResult> Get([FromRoute] TKey id)
        {
            var data = await crudService.GetAsync(id);
            return Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async virtual Task<IActionResult> Create([FromForm] TCreateRequestDTO input)
        {
            try
            {
                input.CreatorId = CurrentUser.Id;
                input.CreationTime = DateTime.UtcNow;

                TDTO updatedData = await crudService.CreateAsync(ObjectMapper.Map<TCreateRequestDTO, TDTO>(input));

                if (updatedData != null)
                {
                    return CreatedAtAction("Get", new { updatedData.Id }, updatedData);
                }

                return BadRequest(new jqDataTableResponse<TDTO>(null, null) { error = "An error course." });

            }
            catch (Exception ex)
            {

                return BadRequest(new jqDataTableResponse<TDTO>(null, null) { error = ex.Message });
            }

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async virtual Task<IActionResult> Update([FromForm] TUpdateRequestDTO input)
        {
            try
            {
                input.LastModificationTime = DateTime.UtcNow;
                input.LastModifierId = CurrentUser.Id;

                await crudService.UpdateAsync(input.Id, ObjectMapper.Map<TUpdateRequestDTO, TDTO>(input));
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(new jqDataTableResponse<TDTO>(null, null) { error = ex.Message });
            }


        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async virtual Task<IActionResult> Delete([FromForm] TDTO input)
        {
            try
            {
                await crudService.DeleteAsync(input.Id);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(new jqDataTableResponse<TDTO>(null, null) { error = ex.Message });
            }


        }
    }
}
