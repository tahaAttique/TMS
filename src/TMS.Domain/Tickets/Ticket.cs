using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TMS.Comments;
using TMS.TicketCategories;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace TMS.Tickets;

public class Ticket : FullAuditedAggregateRoot<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ExpectedBehaviour { get; set; }
    public string? ActualBehaviour { get; set; }
    public string? KnownWorkRound { get; set; }
    public string? StepsToReproduce { get; set; }
    public string? OperatingSystem { get; set; }
    public PriorityType? PriorityType { get; set; }
    public StatusType? StatusType { get; set; }
    public DateTime? ResolvedDate { get; set; }

    public Guid TicketCategoryId { get; set; }

    [ForeignKey("TicketCategoryId")]
    [JsonIgnore]
    public virtual TicketCategory? TicketCategory { get; set; }

    public Guid? UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual IdentityUser? User { get; set; }

    public Guid? AssignedToUserId { get; set; }

    [ForeignKey("AssignedToUserId")]
    public virtual IdentityUser? AssignedToUser { get; set; }

    public Guid? SelfAssignedByUserId { get; set; }

    [ForeignKey("SelfAssignedByUserId")]
    public virtual IdentityUser? SelfAssignedUser { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public Ticket() { }

    internal Ticket(Guid id, string title, string description, string? expectedBehaviour, string? actualBehaviour, string? knownWorkRound, string? stepsToReproduce, string operatingSystem,
        PriorityType? priorityType, StatusType? statusType, DateTime? resolvedDate, Guid ticketCategoryId, Guid? userId, Guid? assignedToUserId, Guid? selfAssignedUserId) : base(id)
    {
        SetTitle(title);
        Description = description;
        ExpectedBehaviour = expectedBehaviour;
        ActualBehaviour = actualBehaviour;
        KnownWorkRound = KnownWorkRound;
        StepsToReproduce = stepsToReproduce;
        OperatingSystem = operatingSystem;
        PriorityType = priorityType;
        StatusType = statusType;
        ResolvedDate = resolvedDate;
        TicketCategoryId = ticketCategoryId;
        UserId = userId;
        AssignedToUserId = assignedToUserId;
        SelfAssignedByUserId = selfAssignedUserId;
        Comments = new List<Comment>();
    }

    internal Ticket ChangeTitle(string title)
    {
        SetTitle(title);
        return this;
    }

    private void SetTitle(string title)
    {
        Title = Check.NotNullOrWhiteSpace(title, nameof(title), maxLength: TicketConsts.MaxTitleLength);
    }
}
