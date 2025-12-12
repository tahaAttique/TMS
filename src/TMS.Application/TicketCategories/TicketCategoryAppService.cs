using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TMS.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace TMS.TicketCategories;

[RemoteService(IsEnabled = false)]
[Authorize(TMSPermissions.TicketCategories.Default)]
public class TicketCategoryAppService(ITicketCategoryRepository ticketCategoryRepository, TicketCategoryManager ticketCategoryManager) : TMSAppService, ITicketCategoryAppService
{
    private readonly ITicketCategoryRepository _ticketCategoryRepository = ticketCategoryRepository;
    private readonly TicketCategoryManager _ticketCategoryManager = ticketCategoryManager;

    [Authorize(TMSPermissions.TicketCategories.Create)]
    public async Task<TicketCategoryDto> CreateAsync(CreateTicketCategoryDto input)
    {
        var category = await _ticketCategoryManager.CreateAsync(input.Name, input.Description);

        await _ticketCategoryRepository.InsertAsync(category);

        return ObjectMapper.Map<TicketCategory, TicketCategoryDto>(category);
    }

    [Authorize(TMSPermissions.TicketCategories.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _ticketCategoryRepository.DeleteAsync(id);
    }

    public async Task<TicketCategoryDto> GetAsync(Guid id)
    {
        var category = await _ticketCategoryRepository.GetAsync(id);
        return ObjectMapper.Map<TicketCategory, TicketCategoryDto>(category);
    }

    public async Task<PagedResultDto<TicketCategoryDto>> GetListAsync(GetTicketCategoryListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(TicketCategory.Name);
        }

        var categories = await _ticketCategoryRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter, input.Name, input.Description);
        var totalCount = await _ticketCategoryRepository.GetCountAsync(input.Filter, input.Name, input.Description);

        return new PagedResultDto<TicketCategoryDto>(totalCount, ObjectMapper.Map<List<TicketCategory>, List<TicketCategoryDto>>(categories));
    }

    [Authorize(TMSPermissions.TicketCategories.Edit)]
    public async Task UpdateAsync(Guid id, UpdateTicketCategoryDto input)
    {
        var category = await _ticketCategoryRepository.GetAsync(id);

        if (category.Name != input.Name)
        {
            await _ticketCategoryManager.ChangeNameAsync(category, input.Name);
        }

        category.Description = input.Description;

        await _ticketCategoryRepository.UpdateAsync(category);
    }
}
