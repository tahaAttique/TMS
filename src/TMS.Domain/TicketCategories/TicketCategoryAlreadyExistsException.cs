using Volo.Abp;

namespace TMS.TicketCategories;

public class TicketCategoryAlreadyExistsException : BusinessException
{
    public TicketCategoryAlreadyExistsException(string name): base(TMSDomainErrorCodes.TicketCategoryAlreadyExists)
    {
        WithData("name", name);
    }
}
