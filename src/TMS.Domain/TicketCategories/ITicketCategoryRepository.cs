using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace TMS.TicketCategories;

public interface ITicketCategoryRepository : IRepository<TicketCategory, Guid>
{
    Task<TicketCategory?> FindByNameAsync(string name);

    Task<List<TicketCategory>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter,
        string? name,
        string? description);
    Task<long> GetCountAsync(string? filter, string? name, string? description);
}
