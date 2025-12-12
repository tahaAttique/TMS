using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMS.Tickets;
using Volo.Abp.Domain.Repositories;

namespace TMS.Comments;

public interface ICommentRepository : IRepository<Comment, Guid>
{
    Task<List<Comment>> GetListAsync(int skipCount, int maxResultCount, string sorting, string? filter, string? detail, Guid? ticketId);

    Task<long> GetCountAsync(string? filter, string? detail, Guid? ticketId);

    Task<Ticket> GetDetailOfTicketAsync(Guid ticketId);

    Task<List<Comment>> GetCommentsByTicketIdAsync(Guid ticketId);

    Task<Comment> GetCommentsByIdAsync(Guid id);
}
