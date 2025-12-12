using System;
using System.ComponentModel.DataAnnotations;
using TMS.Tickets;

namespace TMS.Comments;

public class CreateCommentDto
{
    [Required]
    [MaxLength(TicketConsts.commentDetailLength)]
    public string Detail { get; set; }

    [Required]
    public Guid TicketId { get; set; }
}
