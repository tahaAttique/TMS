using System;
using Volo.Abp.Application.Dtos;

namespace TMS.Tickets;

public class TicketCategoryLookUpDto : EntityDto<Guid>
{
    public string Name { get; set; }
}
