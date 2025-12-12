using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TMS.EntityFrameworkCore;
using TMS.Tickets;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace TMS.Comments;

public class EfCoreCommentRepository : EfCoreRepository<TMSDbContext, Comment, Guid>, ICommentRepository
{
    public EfCoreCommentRepository(IDbContextProvider<TMSDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<long> GetCountAsync(string? filter, string? detail, Guid? ticketId)
    {
        var data = await GetFilteredQueryableAsync(filter, detail, ticketId);
        return await data.LongCountAsync();
    }

    public async Task<List<Comment>> GetListAsync(int skipCount, int maxResultCount, string sorting, string? filter, string? detail, Guid? ticketId)
    {
        var data = await GetFilteredQueryableAsync(filter, detail, ticketId);
        return await data.OrderBy(sorting).PageBy(skipCount, maxResultCount).OrderByDescending(x => x.CreationTime).ToListAsync();
    }

    public async Task<IQueryable<Comment>> GetFilteredQueryableAsync(string? filter, string? detail, Guid? ticketId)
    {
        var queryable = await GetQueryableAsync();
        var query = queryable.Include(x => x.Ticket).Include(x => x.User)
            .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Detail.ToLower().Contains(filter!.ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(detail), x => x.Detail.ToLower().Contains(detail!.ToLower()))
            .WhereIf(ticketId.HasValue, x => x.Ticket.Id == ticketId);

        return query;
    }

    public async Task<Comment> GetCommentsByIdAsync(Guid id)
    {
        var queryable = await GetQueryableAsync();
        var query = await queryable.Include(x => x.User).Where(x => x.Id == id).FirstOrDefaultAsync();
        return query!;
    }

    public async Task<Ticket> GetDetailOfTicketAsync(Guid ticketId)
    {
        var dbContext = await GetDbContextAsync();

        var ticket = await dbContext.
            Tickets.Include(t => t.Comments).
            Include(u => u.User).
            Include(c => c.TicketCategory).
            Include(s => s.SelfAssignedUser).
            Include(u => u.AssignedToUser).FirstOrDefaultAsync(x => x.Id == ticketId);
        return ticket!;
    }

    public async Task<List<Comment>> GetCommentsByTicketIdAsync(Guid ticketId)
    {
        var dbContext = await GetDbContextAsync();

        var comments = await dbContext.Comments
            .Include(x => x.Ticket)
            .Include(u => u.User)
            .Where(x => x.TicketId == ticketId).
            ToListAsync();

        return comments;
    }
}
