using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace TMS.Comments;

public class CommentManager(ICurrentUser currentUser) : DomainService
{
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Comment> CreateAsync(string detail, Guid ticketId)
    {
        Check.NotNullOrWhiteSpace(detail, nameof(detail));
        Check.NotNull(ticketId, nameof(ticketId));

        Guid? userId = _currentUser.Id;

        return new Comment(GuidGenerator.Create(), detail, ticketId, userId);
    }
}
