using System;

namespace TMS.Tickets;

public class CurrentUserLookUpDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
}
