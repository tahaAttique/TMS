using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TMS.Tickets;

public interface ITicketAppService : IApplicationService
{
    Task<TicketDto> GetAsync(Guid id);

    Task<PagedResultDto<TicketDto>> GetListAsync(GetTicketListDto input);

    Task<TicketDto> CreateAsync(CreateTicketDto input);

    Task UpdateAsync(Guid id, UpdateTicketDto input);

    Task DeleteAsync(Guid id);

    Task<List<TicketCategoryLookUpDto>> GetTicketCategoryLookUpAsync();

    Task<List<KeyLookUpDto>> GetAgentKeyUpDtoAsync();

    Task<List<KeyLookUpDto>> GetCustomerKeyLookUpDtoAsync();

    Task<List<KeyLookUpDto>> GetAdminKeyLookUpDtoAsync();

    Task AssignTicketToUserAsync(Guid ticketId, Guid assignedToId, PriorityType priorityType);

    Task<CurrentUserLookUpDto> GetCurrentUser();
}
