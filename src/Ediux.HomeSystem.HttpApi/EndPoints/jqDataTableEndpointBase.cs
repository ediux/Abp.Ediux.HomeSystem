﻿
using Ediux.HomeSystem.Models.jqDataTables;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class jqDataTableEndpointBase<TService, TDTO, TKey, TCreateRequestDTO, TUpdateRequestDTO> : AbpController where TDTO : IEntityDto<TKey>
        where TCreateRequestDTO : IEntityDto<TKey>
        where TUpdateRequestDTO : IEntityDto<TKey>
        where TService : ICrudAppService<TDTO, TKey>
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

            var result = await crudService.GetListAsync(new PagedAndSortedResultRequestDto() { Sorting = sorting });
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
            TDTO updatedData = await crudService.CreateAsync(ObjectMapper.Map<TCreateRequestDTO, TDTO>(input));

            if (updatedData != null)
            {
                return CreatedAtAction("Get", new { updatedData.Id }, updatedData);
            }

            return BadRequest(new jqDataTableResponse<TDTO>(null, null) { error = "An error course." });

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async virtual Task<IActionResult> Update([FromForm] TUpdateRequestDTO input)
        {
            await crudService.UpdateAsync(input.Id, ObjectMapper.Map<TUpdateRequestDTO, TDTO>(input));
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async virtual Task<IActionResult> Delete([FromForm] TDTO input)
        {
            await crudService.DeleteAsync(input.Id);
            return NoContent();
        }

        //protected jqDataTableResponse<TDTO> NotImplemented
        //{
        //    get
        //    {
        //        var empty = new List<TDTO>();
        //        empty.Add(Activator.CreateInstance<TDTO>());
        //        return new jqDataTableResponse<TDTO>(empty, 0)
        //        {
        //            error = "This method has not been implemented yet!"
        //        };
        //    }


        //}


    }
}
