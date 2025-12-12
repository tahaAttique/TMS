using System;
using System.Collections.Generic;
using TMS.Tickets;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace TMS.TicketCategories;

public class TicketCategory : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; }

    private TicketCategory() { }

    internal TicketCategory(Guid id, string name, string? description) : base(id)
    {
        SetName(name);
        Description = description;
        Tickets = new List<Ticket>();
    }

    internal TicketCategory ChangeName(string name)
    {
        SetName(name);
        return this;
    }

    private void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), maxLength: TicketCategoryConsts.MaxNameLength);
    }
}
