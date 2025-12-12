using System;
using Volo.Abp.Application.Dtos;

namespace TMS.Comments;

public class GetCommentListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { set; get; }

    public string? Detail { set; get; }

    public Guid? TicketId { set; get; }
}
