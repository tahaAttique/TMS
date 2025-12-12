using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using TMS.Comments;
using TMS.Tickets;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace TMS.Controllers.Comments;

[RemoteService(isEnabled: true)]
[ControllerName("Comments")]
[Area("app")]
[Route("api/app/comments")]
public class CommentController(ICommentAppService commentAppService) : AbpController, ICommentAppService
{
    [HttpPost]
    public Task<CommentDto> CreateAsync(CreateCommentDto input)
    {
            return commentAppService.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return commentAppService.DeleteAsync(id);
    }

    [HttpGet("{id}")]
    public Task<CommentDto> GetAsync(Guid id)
    {
        return commentAppService.GetAsync(id);
    }

    [HttpGet("get-comment-list-by-ticketId/{ticketId}")]
    public Task<List<CommentDto>> GetCommentsByTicketIdAsync(Guid ticketId)
    {
        return commentAppService.GetCommentsByTicketIdAsync(ticketId);
    }

    [HttpGet("ticketDetail/{ticketId}")]
    public Task<TicketDto> GetDetailOfTicketAsync(Guid ticketId)
    {
       return commentAppService.GetDetailOfTicketAsync(ticketId);
    }

    [HttpGet]
    public Task<PagedResultDto<CommentDto>> GetListAsync(GetCommentListDto input)
    {
        return commentAppService.GetListAsync(input);
    }

    [HttpPut("{id}")]
    public Task UpdateAsync(Guid id, UpdateCommentDto input)
    {
        return commentAppService.UpdateAsync(id, input);
    }

    [HttpPut("update-status/{ticketId}")]
    public Task UpdateStatusAsync(Guid ticketId, StatusType statusType)
    {
        return commentAppService.UpdateStatusAsync(ticketId, statusType);
    }
}
