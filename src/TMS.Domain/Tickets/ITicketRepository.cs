using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace TMS.Tickets;

public interface ITicketRepository : IRepository<Ticket, Guid>
{
    Task<List<Ticket>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter,
        string? title,
        string? description,
        PriorityType? priorityType,
        StatusType? statusType,
        Guid? ticketcategoryId,
        Guid? userId,
        Guid? assignedToUserId,
        Guid? selfAssignedByUserId,
        string? operatingSystem);

    Task<long> GetCountAsync(
        string? filter,
        string? title,
        string? description,
        PriorityType? priorityType,
        StatusType? statusType,
        Guid? ticketcategoryId,
        Guid? userId,
        Guid? assignedToUserId,
        Guid? selfAssignedByUserId,
        string? operatingSystem);
    Task<List<KeyLookUp>> GetAgentUsersLookupAsync();
    Task<List<KeyLookUp>> GetCustomerUsersLookupAsync();
    Task<List<KeyLookUp>> GetAdminUsersLookupAsync();
}
