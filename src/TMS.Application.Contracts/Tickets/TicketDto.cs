using System;
using Volo.Abp.Application.Dtos;

namespace TMS.Tickets;

public class TicketDto : EntityDto<Guid>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public  string? ExpectedBehaviour { get; set; }
    public  string? ActualBehaviour { get; set; }
    public string? KnownWorkRound { get; set; }
    public string? StepsToReproduce { get; set; }
    public required string OperatingSystem { get; set; }
    public PriorityType? PriorityType { get; set; }
    public StatusType? StatusType { get; set; }
    public DateTime? ResolvedDate { get; set; }
    public required Guid TicketCategoryId { get; set; }
    public Guid? UserId { get; set; }
    public string? UserName { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public string? AssignedUserName { get; set; }
    public Guid? SelfAssignedByUserId { get; set; }
    public string? SelfAssignedUserName { get; set; }
    public string? TicketCategory { get; set; }
    public DateTime? CreatedDate { get; set; }
}
