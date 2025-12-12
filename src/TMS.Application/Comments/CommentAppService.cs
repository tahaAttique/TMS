using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using TMS.EmailSendingJob;
using TMS.Notification;
using TMS.Permissions;
using TMS.Tickets;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Uow;

namespace TMS.Comments;

[RemoteService(IsEnabled = false)]

public class CommentAppService(ICommentRepository commentRepository, CommentManager commentManager, ITicketRepository ticketRepository,
    IUnitOfWorkManager unitOfWorkManager, IHubContext<NotificationHub> hubContext, IBackgroundJobManager backgroundJobManager) : TMSAppService, ICommentAppService
{
    private readonly ICommentRepository _commentRepository = commentRepository;
    private readonly ITicketRepository _ticketRepository = ticketRepository;
    private readonly CommentManager _commentManager = commentManager;
    private readonly IUnitOfWorkManager _unitOfWorkManager = unitOfWorkManager;
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;
    private readonly IBackgroundJobManager _backgroundManager = backgroundJobManager;

    [Authorize(TMSPermissions.Comments.Create)]
    public async Task<CommentDto> CreateAsync(CreateCommentDto input)
    {
        var comment = await _commentManager.CreateAsync(input.Detail, input.TicketId);

        var item = await _commentRepository.InsertAsync(comment);

        await _unitOfWorkManager.Current!.SaveChangesAsync();

        if (item != null)
        {
            await SendEmailAccordingToCommentAsync(item.TicketId);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification",
                $"A new comment has been added in this ticket '{comment.Ticket.Title}'");
        }

        var data = await _commentRepository.GetCommentsByIdAsync(item!.Id);

        var comments = ObjectMapper.Map<Comment, CommentDto>(data);
        return comments;
    }

    [Authorize(TMSPermissions.Comments.Edit)]
    public async Task DeleteAsync(Guid id)
    {
        await _commentRepository.DeleteAsync(id);
    }

    public async Task<TicketDto> GetDetailOfTicketAsync(Guid ticketId)
    {
        var tickets = await _commentRepository.GetDetailOfTicketAsync(ticketId);
        return ObjectMapper.Map<Ticket, TicketDto>(tickets);
    }

    public async Task<List<CommentDto>> GetCommentsByTicketIdAsync(Guid ticketId)
    {
        var comments = await _commentRepository.GetCommentsByTicketIdAsync(ticketId);

        return ObjectMapper.Map<List<Comment>, List<CommentDto>>(comments);
    }

    public async Task<CommentDto> GetAsync(Guid id)
    {
        var comment = await _commentRepository.GetAsync(id);
        return ObjectMapper.Map<Comment, CommentDto>(comment);
    }

    public async Task<PagedResultDto<CommentDto>> GetListAsync(GetCommentListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Comment.CreationTime);
        }

        var comments = await _commentRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter, input.Detail, input.TicketId);
        var totalCount = await _commentRepository.GetCountAsync(input.Filter, input.Detail, input.TicketId);

        return new PagedResultDto<CommentDto>(totalCount, ObjectMapper.Map<List<Comment>, List<CommentDto>>(comments));
    }

    [Authorize(TMSPermissions.Comments.Edit)]
    public async Task UpdateAsync(Guid id, UpdateCommentDto input)
    {
        var comment = await _commentRepository.GetAsync(id);

        comment.Detail = input.Detail;

        await _commentRepository.UpdateAsync(comment);
    }

    [Authorize(TMSPermissions.Comments.Status)]
    public async Task UpdateStatusAsync(Guid ticketId, StatusType statusType)
    {
        var ticket = await _ticketRepository.GetAsync(ticketId);

        if (ticket == null)
        {
            throw new UserFriendlyException("Ticket not found.");
        }

        ticket.StatusType = statusType;

        await _ticketRepository.UpdateAsync(ticket);
    }

    public async Task SendEmailAccordingToCommentAsync(Guid ticketId)
    {
        var ticket = await _ticketRepository.GetAsync(ticketId);
        if (ticket == null)
        {
            throw new UserFriendlyException("Ticket not found");
        }

        var comments = await _commentRepository.GetCommentsByTicketIdAsync(ticketId);
        var ticketDetail = await _commentRepository.GetDetailOfTicketAsync(ticketId);

        var agentName = ticketDetail.AssignedToUser?.UserName ?? throw new UserFriendlyException("Assigned user not found for this ticket.");
        var agentEmail = ticketDetail.AssignedToUser?.Email ?? throw new UserFriendlyException("Assigned user's email not found for this ticket.");

        if (!comments.Any())
        {
            throw new UserFriendlyException("No comments found for this ticket.");
        }

        var commentEmails = comments
            .Where(c => !string.IsNullOrWhiteSpace(c.User?.Email))
            .Select(c => new
            {
                Email = c.User?.Email ?? "",
                UserName = c.User?.UserName ?? ""
            })
            .Distinct()
            .ToList();

        if (!commentEmails.Any())
        {
            throw new UserFriendlyException("No valid email addresses found for comments.");
        }

        var distinctRecipients = commentEmails
            .Append(new { Email = agentEmail, UserName = agentName })
            .GroupBy(r => r.Email)
            .Select(g => g.First())
            .ToList();

        var subject = $"New Comment Added to Ticket: {ticket.Title}";
        var body = $@"
                <p>We would like to inform you that a new comment has been added to the ticket you are associated with. Below are the details:</p>
                <ul>
                    <li><strong>Ticket Title:</strong> {ticket.Title}</li>
                    <li><strong>Status:</strong> {ticket.StatusType}</li>
                    <li><strong>Created By:</strong> {comments.FirstOrDefault()?.User?.UserName ?? "Unknown"}</li>
                </ul>
                <p>Please review the comments and take the necessary actions if needed.</p>
                <p>Thank you for your attention.</p>
                <p style='color:red;'><strong>Note:</strong> Please do not reply to this email as it is auto-generated.</p>";

        foreach (var recipient in distinctRecipients)
        {
            var emailArgs = new EmailSendingArgs
            {
                EmailAddress = recipient.Email,
                Name = recipient.UserName,
                Subject = subject,
                Body = body
            };

            await _backgroundManager.EnqueueAsync(emailArgs);
        }
    }
}
