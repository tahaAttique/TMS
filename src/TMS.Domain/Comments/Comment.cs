using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TMS.Tickets;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace TMS.Comments;

public class Comment : FullAuditedAggregateRoot<Guid>
{
    [MaxLength(TicketConsts.commentDetailLength)]
    public string Detail { get; set; }

    public Guid TicketId { get; set; }

    [ForeignKey("TicketId")]
    [JsonIgnore]
    public virtual Ticket Ticket { get; set; }

    public Guid? UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual IdentityUser? User { get; set; }

    public Comment() { }

    internal Comment(Guid id, string detail, Guid ticketId, Guid? userId) : base(id)
    {
        Detail = detail;
        TicketId = ticketId;
        UserId = userId;
    }
}
