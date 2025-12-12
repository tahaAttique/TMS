using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TMS.TicketCategories;

public interface ITicketCategoryAppService : IApplicationService
{
    Task<TicketCategoryDto> GetAsync(Guid id);

    Task<PagedResultDto<TicketCategoryDto>> GetListAsync(GetTicketCategoryListDto input);

    Task<TicketCategoryDto> CreateAsync(CreateTicketCategoryDto input);

    Task UpdateAsync(Guid id, UpdateTicketCategoryDto input);

    Task DeleteAsync(Guid id);
}
