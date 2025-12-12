using System;
using Volo.Abp.Application.Dtos;

namespace TMS.TicketCategories;

public class TicketCategoryDto : EntityDto<Guid>
{
    public required string Name { get; set; }

    public string? Description { get; set; }
}
