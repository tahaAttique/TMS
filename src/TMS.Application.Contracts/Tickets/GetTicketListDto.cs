using System;
using Volo.Abp.Application.Dtos;

namespace TMS.Tickets;

public class GetTicketListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ExpectedBehaviour { get; set; }
    public string? ActualBehaviour { get; set; }
    public string? NonWorkRound { get; set; }
    public string? OperatingSystem { get; set; }
    public PriorityType? PriorityType { get; set; }
    public StatusType? StatusType { get; set; }
    public DateTime? ResolvedDate { get; set; }
    public Guid? TicketCategoryId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public Guid? SelfAssignedByUserId { get; set; }
}
