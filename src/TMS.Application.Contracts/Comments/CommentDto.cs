using System;
using Volo.Abp.Application.Dtos;

namespace TMS.Comments;

public class CommentDto : EntityDto<Guid>
{
    public string Detail { get; set; }

    public Guid TicketId { get; set; }

    public Guid? UserId { get; set; }

    public string? UserName { get; set; }
}
