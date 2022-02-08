using Ediux.HomeSystem.Models.DTOs.Files;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Plugins.HololivePages.HoloInforamtions
{
    public interface IHoloInformationAppService : IApplicationService, ITransientDependency
    {
        Task<PagedResultDto<HoloCompanyDto>> GetListByCompaniesAsync(HoloRequestDto input);

        Task<PagedResultDto<HoloBranchesDto>> GetListByBranchesAsync(HoloRequestDto input);

        Task<PagedResultDto<HoloDepartmentsDto>> GetListByDepartmentsAsync(HoloRequestDto input);

        Task<PagedResultDto<HoloMemberDto>> GetListByMembersAsync(HoloRequestDto input);

        Task<PagedResultDto<HoloMemberEventDto>> GetListByEventsAsync(HoloRequestDto input);

        Task<PagedResultDto<FileStoreDTO>> GetListByFilesAsync(HoloRequestDto input);

        Task<PagedResultDto<YTuberVideoDto>> GetListByVideosAsync(HoloRequestDto input);

        Task<HoloCompanyDto> GetCompanyByIdAsync(Guid companyId);

        Task<HoloBranchesDto> GetBranchByIdAsync(Guid branchId);

        Task<HoloCompanyDto> CreateAsync(HoloCompanyDto input);

        Task<HoloBranchesDto> CreateAsync(HoloBranchesDto input);

        Task<HoloDepartmentsDto> CreateAsync(HoloDepartmentsDto input);

        Task<HoloMemberDto> CreateAsync(HoloMemberDto input);

        Task<HoloMemberEventDto> CreateAsync(HoloMemberEventDto input);

        Task<YTuberVideoDto> CreateAsync(YTuberVideoDto input);
    }
}
