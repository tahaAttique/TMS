using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TMS.Comments;
using TMS.EntityFrameworkCore;
using TMS.Identity;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace TMS.Tickets;

public class EfCoreTicketRepository : EfCoreRepository<TMSDbContext, Ticket, Guid>, ITicketRepository
{
    public EfCoreTicketRepository(IDbContextProvider<TMSDbContext> dbContextProvider) : base(dbContextProvider) { }

    public async Task<long> GetCountAsync(string? filter, string? title, string? description, PriorityType? priorityType, StatusType? statusType, Guid? ticketcategoryId, Guid? userId, Guid? assignedToUserId, Guid? selfAssignedByUserId, string? operatingSystem)
    {
        var data = await GetFilteredQueryableAsync(filter, title, description, priorityType, statusType, ticketcategoryId, userId, assignedToUserId, selfAssignedByUserId, operatingSystem);
        return await data.LongCountAsync();
    }

    public async Task<List<Ticket>> GetListAsync(int skipCount, int maxResultCount, string sorting, string? filter, string? title, string? description, PriorityType? priorityType, StatusType? statusType, Guid? ticketcategoryId, Guid? userId, Guid? assignedToUserId, Guid? selfAssignedByUserId, string? operatingSystem)
    {
        var data = await GetFilteredQueryableAsync(filter, title, description, priorityType, statusType, ticketcategoryId, userId, assignedToUserId, selfAssignedByUserId, operatingSystem);

        return await data.OrderBy(sorting).PageBy(skipCount, maxResultCount).OrderByDescending(x => x.CreationTime).ToListAsync();
    }

    public async Task<IQueryable<Ticket>> GetFilteredQueryableAsync(string? filter, string? title, string? description, PriorityType? priorityType, StatusType? statusType,
        Guid? ticketcategoryId, Guid? userId, Guid? assignedToUserId, Guid? selfAssignedByUserId, string? operatingSystem)
    {
        var queryable = await GetQueryableAsync();
        var query = queryable.Include(x => x.TicketCategory).Include(x => x.User).Include(x => x.AssignedToUser)
            .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Title.ToLower().Contains(filter!.ToLower()) ||
                                                              x.Description.ToLower().Contains(filter!.ToLower()) ||
                                                              x.User!.Name.ToLower().Contains(filter!.ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(title), x => x.Title.ToLower().Contains(title!.ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(description), x => x.Description.ToLower().Contains(description!.ToLower()))
            .WhereIf(priorityType.HasValue, x => x.PriorityType! == priorityType)
            .WhereIf(statusType.HasValue, x => x.StatusType! == statusType)
            .WhereIf(ticketcategoryId.HasValue, x => x.TicketCategory!.Id == ticketcategoryId)
            .WhereIf(userId.HasValue, x => x.User!.Id == userId)
            .WhereIf(assignedToUserId.HasValue, x => x.AssignedToUserId == assignedToUserId)
            .WhereIf(selfAssignedByUserId.HasValue, x => x.User!.Id == selfAssignedByUserId)
            .WhereIf(!string.IsNullOrWhiteSpace(operatingSystem), x => x.OperatingSystem.ToLower().Contains(operatingSystem!.ToLower()));

        return query;
    }

    public async Task<List<KeyLookUp>> GetAgentUsersLookupAsync()
    {
        var dbContext = await GetDbContextAsync();

        var agentRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.Name == CustomIdentityUserConsts.IdentityUserRoleNames.Agent);

        if (agentRole == null)
        {
            return new List<KeyLookUp>();
        }

        var agentUsers = await dbContext.Users
            .Where(u => u.Roles.Any(r => r.RoleId == agentRole.Id))
            .Select(x => new KeyLookUp
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
            })
            .ToListAsync();

        return agentUsers;
    }

    public async Task<List<KeyLookUp>> GetCustomerUsersLookupAsync()
    {
        var dbContext = await GetDbContextAsync();

        var customerRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.Name == CustomIdentityUserConsts.IdentityUserRoleNames.Customer);
        if (customerRole == null)
        {
            return new List<KeyLookUp>();
        }

        var customerRoles = await dbContext.Users
            .Where(u => u.Roles.Any(r => r.RoleId == customerRole.Id))
            .Select(x => new KeyLookUp
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email
            }).ToListAsync();

        return customerRoles;
    }

    public async Task<List<KeyLookUp>> GetAdminUsersLookupAsync()
    {
        var dbContext = await GetDbContextAsync();

        var adminRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.Name == CustomIdentityUserConsts.IdentityUserRoleNames.Admin);
        if (adminRole == null)
        {
            return new List<KeyLookUp>();
        }

        var adminRoles = await dbContext.Users
            .Where(u => u.Roles.Any(r => r.RoleId == adminRole.Id))
            .Select(x => new KeyLookUp
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email
            }).ToListAsync();
        
        return adminRoles;
    }

}
