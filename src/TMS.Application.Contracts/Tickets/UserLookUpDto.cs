using System;
using Volo.Abp.Application.Dtos;

namespace TMS.Tickets;

public class UserLookUpDto : EntityDto<Guid>
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
}
