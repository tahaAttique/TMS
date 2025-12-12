using System;
using System.ComponentModel.DataAnnotations;

namespace TMS.Comments;

public class UpdateCommentDto
{
    [Required]
    public string Detail { get; set; }

    [Required]
    public Guid TicketId { get; set; }
}
