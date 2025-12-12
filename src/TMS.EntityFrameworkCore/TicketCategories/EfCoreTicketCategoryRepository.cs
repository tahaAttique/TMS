using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TMS.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace TMS.TicketCategories;

public class EfCoreTicketCategoryRepository : EfCoreRepository<TMSDbContext, TicketCategory, Guid>, ITicketCategoryRepository
{
    public EfCoreTicketCategoryRepository(IDbContextProvider<TMSDbContext> dbContextProvider) : base(dbContextProvider)
    { }

    public async Task<TicketCategory?> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
    }

    public async Task<long> GetCountAsync(string? filter, string? name, string? description)
    {
        var data = await GetFilteredQueryableAsync(filter, name, description);
        return await data.LongCountAsync();
    }

    public async Task<List<TicketCategory>> GetListAsync(int skipCount, int maxResultCount, string sorting, string? filter, string? name, string? description)
    {
        var data = await GetFilteredQueryableAsync(filter, name, description);
        return await data.OrderBy(sorting).PageBy(skipCount, maxResultCount).ToListAsync();
    }

    public async Task<IQueryable<TicketCategory>> GetFilteredQueryableAsync(string? filter, string? name, string? description)
    {
        var queryable = await GetQueryableAsync();

        var query = queryable.AsQueryable()
            .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Name.ToLower().Contains(filter!.ToLower()) || x.Description.ToLower().Contains(filter!.ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name.ToLower().Contains(name!.ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(description), x => x.Description.ToLower().Contains(description!.ToLower()));

        return query;
    }

}
