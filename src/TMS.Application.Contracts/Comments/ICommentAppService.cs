using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMS.Tickets;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TMS.Comments;

public interface ICommentAppService : IApplicationService
{
    Task<CommentDto> GetAsync(Guid id);

    Task<PagedResultDto<CommentDto>> GetListAsync(GetCommentListDto input);

    Task<CommentDto> CreateAsync(CreateCommentDto input);

    Task DeleteAsync(Guid id);

    Task UpdateAsync(Guid id, UpdateCommentDto input);

    Task<TicketDto> GetDetailOfTicketAsync(Guid ticketId);
    Task<List<CommentDto>> GetCommentsByTicketIdAsync(Guid ticketId);

    Task UpdateStatusAsync(Guid ticketId, StatusType statusType);
}
