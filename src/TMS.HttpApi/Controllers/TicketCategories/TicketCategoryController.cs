using System;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using TMS.TicketCategories;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace TMS.Controllers.TicketCategories;

[RemoteService(isEnabled: true)]
[ControllerName("TicketCategories")]
[Area("app")]
[Route("api/app/ticket-categories")]
public class TicketCategoryController(ITicketCategoryAppService ticketCategoryAppService) : AbpController, ITicketCategoryAppService
{
    [HttpPost]
    public Task<TicketCategoryDto> CreateAsync(CreateTicketCategoryDto input)
    {
        return ticketCategoryAppService.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return ticketCategoryAppService.DeleteAsync(id);
    }

    [HttpGet("{id}")]
    public Task<TicketCategoryDto> GetAsync(Guid id)
    {
        return ticketCategoryAppService.GetAsync(id);
    }

    [HttpGet]
    public Task<PagedResultDto<TicketCategoryDto>> GetListAsync(GetTicketCategoryListDto input)
    {
        return ticketCategoryAppService.GetListAsync(input);
    }

    [HttpPut("{id}")]
    public Task UpdateAsync(Guid id, UpdateTicketCategoryDto input)
    {
        return ticketCategoryAppService.UpdateAsync(id, input);
    }
}
