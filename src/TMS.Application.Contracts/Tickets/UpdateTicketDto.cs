using System;
using System.ComponentModel.DataAnnotations;

namespace TMS.Tickets;

public class UpdateTicketDto
{
    [Required]
    [StringLength(TicketConsts.MaxTitleLength)]
    public required string Title { get; set; }

    [Required]
    public required string Description { get; set; }

    public string? ExpectedBehaviour { get; set; }

    public string? ActualBehaviour { get; set; }

    public string? KnownWorkRound { get; set; }
    public string? StepsToReproduce { get; set; }

    [Required]
    public required string OperatingSystem { get; set; }
    public PriorityType? PriorityType { get; set; }
    public StatusType? StatusType { get; set; }
    public DateTime? ResolvedDate { get; set; }

    [Required]
    public Guid TicketCategoryId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public Guid? SelfAssignedByUserId { get; set; }
}
