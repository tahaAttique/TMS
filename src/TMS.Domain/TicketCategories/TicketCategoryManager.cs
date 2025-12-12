using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace TMS.TicketCategories;

public class TicketCategoryManager(ITicketCategoryRepository ticketCategoryRepository) : DomainService
{
    private readonly ITicketCategoryRepository _ticketCategoryRepository = ticketCategoryRepository;

    public async Task<TicketCategory> CreateAsync(string name, string? description)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
  
        var existingCategory = await _ticketCategoryRepository.FindByNameAsync(name);
        if (existingCategory != null)
        {
            throw new TicketCategoryAlreadyExistsException(name);
        }

        return new TicketCategory(GuidGenerator.Create(), name, description);
    }

    public async Task ChangeNameAsync(TicketCategory ticketCategory, string newName)
    {
        Check.NotNull(ticketCategory, nameof(ticketCategory));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingCategory = await _ticketCategoryRepository.FindByNameAsync(newName);
        if (existingCategory != null)
        {
            throw new TicketCategoryAlreadyExistsException(newName);
        }

        ticketCategory.ChangeName(newName);
    }
}
