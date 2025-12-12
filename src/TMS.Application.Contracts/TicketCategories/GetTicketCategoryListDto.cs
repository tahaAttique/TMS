using Volo.Abp.Application.Dtos;

namespace TMS.TicketCategories;

public class GetTicketCategoryListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}
