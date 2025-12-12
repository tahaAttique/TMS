using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace TMS.Tickets;

public class TicketManager(ICurrentUser currentUser) : DomainService
{
    private readonly ICurrentUser _currentUser = currentUser;
    public async Task<Ticket> CreateAsync(string title, string description, string expectedBehaviour, string actualBehaviour, string knownWorkRound, string? stepsToReproduce, string operatingSystem,
        PriorityType? priorityType, StatusType? statusType, DateTime? resolvedDate, Guid ticketCategoryId, Guid? userId, Guid? assignedToUserId, Guid? selfAssignedUserId)
    {
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.NotNullOrWhiteSpace(description, nameof(description));
        Check.NotNull(ticketCategoryId, nameof(ticketCategoryId));
        Check.NotNullOrWhiteSpace(operatingSystem, nameof(operatingSystem));

        statusType ??= StatusType.New;
        userId = _currentUser.Id;

        return new Ticket(GuidGenerator.Create(), title, description, expectedBehaviour, actualBehaviour, knownWorkRound, stepsToReproduce, operatingSystem, priorityType, statusType, resolvedDate,
             ticketCategoryId, userId, assignedToUserId, selfAssignedUserId);
    }

    public async Task ChangeTitleAsync(Ticket ticket, string title)
    {
        Check.NotNull(ticket, nameof(ticket));
        Check.NotNullOrWhiteSpace(title, nameof(title));

        ticket.ChangeTitle(title);
    }

}
