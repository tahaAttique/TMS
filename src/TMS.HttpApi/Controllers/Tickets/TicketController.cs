using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using TMS.Tickets;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace TMS.Controllers.Tickets;

[RemoteService(IsEnabled = true)]
[ControllerName("Tickets")]
[Area("app")]
[Route("api/app/tickets")]

public class TicketController(ITicketAppService ticketAppService) : AbpController, ITicketAppService
{
    [HttpPost]
    public Task<TicketDto> CreateAsync(CreateTicketDto input)
    {
        return ticketAppService.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return ticketAppService.DeleteAsync(id);
    }

    [HttpGet("{id}")]
    public Task<TicketDto> GetAsync(Guid id)
    {
        return ticketAppService.GetAsync(id);
    }

    [HttpGet]
    public Task<PagedResultDto<TicketDto>> GetListAsync(GetTicketListDto input)
    {
        return ticketAppService.GetListAsync(input);
    }

    [HttpGet("get-ticket-category")]
    public Task<List<TicketCategoryLookUpDto>> GetTicketCategoryLookUpAsync()
    {
        return ticketAppService.GetTicketCategoryLookUpAsync();
    }

    [HttpPut("{id}")]
    public Task UpdateAsync(Guid id, UpdateTicketDto input)
    {
        return ticketAppService.UpdateAsync(id, input);
    }

    [HttpGet("get-agent")]
    public Task<List<KeyLookUpDto>> GetAgentKeyUpDtoAsync()
    {
        return ticketAppService.GetAgentKeyUpDtoAsync();
    }

    [HttpGet("get-customer")]
    public Task<List<KeyLookUpDto>> GetCustomerKeyLookUpDtoAsync()
    {
        return ticketAppService.GetCustomerKeyLookUpDtoAsync();
    }

    [HttpGet("get-admin")]
    public Task<List<KeyLookUpDto>> GetAdminKeyLookUpDtoAsync()
    {
        return ticketAppService.GetAdminKeyLookUpDtoAsync();
    }

    [HttpPost("assign-ticket")]
    public Task AssignTicketToUserAsync(Guid ticketId, Guid assignedToId, PriorityType priorityType)
    {
        return ticketAppService.AssignTicketToUserAsync(ticketId, assignedToId, priorityType);
    }

    [HttpGet("get-current-user")]
    public Task<CurrentUserLookUpDto> GetCurrentUser()
    {
        return ticketAppService.GetCurrentUser();
    }
}

