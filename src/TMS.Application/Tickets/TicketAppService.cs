using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TMS.Comments;
using TMS.EmailSendingJob;
using TMS.Notification;
using TMS.Permissions;
using TMS.TicketCategories;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace TMS.Tickets;

[RemoteService(IsEnabled = false)]
//[Authorize(TMSPermissions.Tickets.Default)]
public class TicketAppService(ITicketRepository ticketRepository, ITicketCategoryRepository ticketCategoryRepository, TicketManager ticketManager, ICurrentUser currentUser,
    IUnitOfWorkManager unitOfWorkManager, ICommentRepository commentRepository, IHubContext<NotificationHub> hubContext, IBackgroundJobManager backgroundJobManager)
    : TMSAppService, ITicketAppService
{
    private readonly ITicketRepository _ticketRepository = ticketRepository;
    private readonly TicketManager _ticketManager = ticketManager; 
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IUnitOfWorkManager _unitOfWorkManager = unitOfWorkManager;
    private readonly ICommentRepository _commentRepository = commentRepository;
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;
    private readonly IBackgroundJobManager _backgroundJobManager = backgroundJobManager;

    [Authorize(TMSPermissions.Tickets.Create)]
    public async Task<TicketDto> CreateAsync(CreateTicketDto input)
    {
        var ticket = await _ticketManager.CreateAsync(
            input.Title,
            input.Description,
            input.ExpectedBehaviour,
            input.ActualBehaviour,
            input.KnownWorkRound,
            input.StepsToReproduce,
            input.OperatingSystem,
            input.PriorityType,
            input.StatusType,
            input.ResolvedDate,
            input.TicketCategoryId,
            input.UserId,
            input.AssignedToUserId,
            input.SelfAssignedByUserId);

        var item = await _ticketRepository.InsertAsync(ticket);
        await _unitOfWorkManager.Current!.SaveChangesAsync();

        if (item != null)
        {
            await SendTicketEmailToAdminAndAgentAsync(item.Id);

            await _hubContext.Clients.All.SendAsync("ReceiveNotification",
            $"A new ticket '{ticket.Title}' has been Created. Please check your system.");
        }

        return ObjectMapper.Map<Ticket, TicketDto>(ticket);
    }

    [Authorize(TMSPermissions.Tickets.Delete)]

    public async Task DeleteAsync(Guid id)
    {
        await _ticketRepository.DeleteAsync(id);
    }

    public async Task<TicketDto> GetAsync(Guid id)
    {
        var ticket = await _ticketRepository.GetAsync(id);
        return ObjectMapper.Map<Ticket, TicketDto>(ticket);
    }

    public async Task<PagedResultDto<TicketDto>> GetListAsync(GetTicketListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Ticket.Title);
        }

        var tickets = await _ticketRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter,
            input.Title,
            input.Description,
            input.PriorityType,
            input.StatusType,
            input.TicketCategoryId,
            input.UserId,
            input.AssignedToUserId,
            input.SelfAssignedByUserId,
            input.OperatingSystem);

        var totalCount = await _ticketRepository.GetCountAsync(
            input.Filter,
            input.Title,
            input.Description,
            input.PriorityType,
            input.StatusType,
            input.TicketCategoryId,
            input.UserId,
            input.AssignedToUserId,
            input.SelfAssignedByUserId,
            input.OperatingSystem);

        return new PagedResultDto<TicketDto>(totalCount, ObjectMapper.Map<List<Ticket>, List<TicketDto>>(tickets));
    }

    [Authorize(TMSPermissions.Tickets.Edit)]
    public async Task UpdateAsync(Guid id, UpdateTicketDto input)
    {
        var ticket = await _ticketRepository.GetAsync(id);

        if (ticket.Title != input.Title)
        {
            await _ticketManager.ChangeTitleAsync(ticket, input.Title);
        }

        ticket.Description = input.Description;
        ticket.ExpectedBehaviour = input.ExpectedBehaviour;
        ticket.ActualBehaviour = input.ActualBehaviour;
        ticket.KnownWorkRound = input.KnownWorkRound;
        ticket.StepsToReproduce = input.StepsToReproduce;
        ticket.OperatingSystem = input.OperatingSystem;
        ticket.PriorityType = input.PriorityType;
        ticket.StatusType = input.StatusType;
        ticket.ResolvedDate = input.ResolvedDate;
        ticket.TicketCategoryId = input.TicketCategoryId;
        ticket.UserId = input.UserId;
        ticket.AssignedToUserId = input.AssignedToUserId;
        ticket.SelfAssignedByUserId = input.SelfAssignedByUserId;

        await _ticketRepository.UpdateAsync(ticket);
    }

    public async Task<List<TicketCategoryLookUpDto>> GetTicketCategoryLookUpAsync()
    {
        var ticketCategory = await ticketCategoryRepository.GetListAsync();
        return ticketCategory.Select(e => new TicketCategoryLookUpDto { Id = e.Id, Name = e.Name }).ToList();
    }

    public async Task<List<KeyLookUpDto>> GetAgentKeyUpDtoAsync()
    {
        var agentUser = await _ticketRepository.GetAgentUsersLookupAsync();
        return agentUser.Select(e => new KeyLookUpDto { Id = e.Id, UserName = e.UserName }).ToList();
    }

    public async Task<List<KeyLookUpDto>> GetCustomerKeyLookUpDtoAsync()
    {
        var customerUser = await _ticketRepository.GetCustomerUsersLookupAsync();
        return customerUser.Select(e => new KeyLookUpDto { Id = e.Id, UserName = e.UserName }).ToList();
    }

    public async Task<List<KeyLookUpDto>> GetAdminKeyLookUpDtoAsync()
    {
        var adminUser = await _ticketRepository.GetAdminUsersLookupAsync();
        return adminUser.Select(e => new KeyLookUpDto { Id = e.Id, UserName = e.UserName }).ToList();
    }

    public async Task AssignTicketToUserAsync(Guid ticketId, Guid assignedToId, PriorityType priorityType)
    {
        Check.NotNull(ticketId, nameof(ticketId));
        Check.NotNull(assignedToId, nameof(assignedToId));
        Check.NotNull(priorityType, nameof(priorityType));

        var ticket = await _ticketRepository.GetAsync(ticketId);

        ticket.AssignedToUserId = assignedToId;
        ticket.PriorityType = priorityType;

        await _ticketRepository.UpdateAsync(ticket);
        await _unitOfWorkManager.Current!.SaveChangesAsync();

        var ticketDetail = await _commentRepository.GetDetailOfTicketAsync(ticketId);

        await SendTicketEmailToAgent(ticketDetail.Id);

        _hubContext.Clients.All.SendAsync("ReceiveNotification",
        $"A new ticket '{ticket.Title}' has assigned to '{ticketDetail.AssignedToUser?.UserName ?? "UnKnown"}'. Please check your dashboard.");
    }


    public Task<CurrentUserLookUpDto> GetCurrentUser()
    {
        if (!_currentUser.IsAuthenticated)
        {
            throw new UserFriendlyException("User is not authenticated.");
        }

        return Task.FromResult(new CurrentUserLookUpDto
        {
            UserId = (Guid)_currentUser.Id!,
            Name = _currentUser.UserName!
        });
    }

    public async Task SendTicketEmailToAdminAndAgentAsync(Guid ticketId)
    {
        var ticket = await _ticketRepository.GetAsync(ticketId);
        if (ticket == null)
        {
            throw new UserFriendlyException("Ticket not found");
        }

        var currentUserEmail = _currentUser.Email;
        var currentUserName = _currentUser.UserName;

        if (string.IsNullOrWhiteSpace(currentUserEmail))
        {
            throw new UserFriendlyException("Current user email not available");
        }

        // Fetch Admin & Agent Users
        var adminUsers = await _ticketRepository.GetAdminUsersLookupAsync();
        var agentUsers = await _ticketRepository.GetAgentUsersLookupAsync();

        var ticketDetail = await _commentRepository.GetDetailOfTicketAsync(ticketId);

        var recipientEmails = adminUsers
            .Where(x => !string.IsNullOrWhiteSpace(x.Email))
            .Select(x => new { x.Email, x.UserName })
            .ToList();

        recipientEmails.AddRange(agentUsers
            .Where(x => !string.IsNullOrWhiteSpace(x.Email))
            .Select(x => new { x.Email, x.UserName }));

        var subject = $"New Ticket Assigned: {ticket.Title}";
        var body = $@"
                <p>We would like to inform you that a new ticket has been assigned to you. Below are the details:</p>
                <ul>
                    <li><strong>Ticket Title:</strong> {ticket.Title}</li>
                    <li><strong>Status:</strong> {ticket.StatusType}</li>
                    <li><strong>Created By:</strong> {ticketDetail.User?.Name ?? ""}</li>
                </ul>
                <p>Please review the ticket and take the necessary actions at your earliest convenience.</p>
                <p>Thank you for your attention.</p>
                <p style='color:red;'><strong>Note:</strong> Please do not reply to this email as it is auto-generated.</p>";

        var userEmailArgs = new EmailSendingArgs
        {
            EmailAddress = currentUserEmail,
            Name = currentUserName!,
            Subject = "Your Ticket Has Been Created Successfully",
            Body = $@"
                    <p>Dear {currentUserName},</p>
                    <p>Your ticket titled '<strong>{ticket.Title}</strong>' has been created successfully. Below are the details:</p>
                    <ul>
                        <li><strong>Ticket Title:</strong> {ticket.Title}</li>
                        <li><strong>Status:</strong> {ticket.StatusType}</li>
                    </ul>
                    <p>You will be notified once further actions are taken.</p>
                    <p>Thank you for using our system.</p>"
        };

        await _backgroundJobManager.EnqueueAsync(userEmailArgs);

        foreach (var recipient in recipientEmails)
        {
            var adminAgentEmailArgs = new EmailSendingArgs
            {
                EmailAddress = recipient.Email,
                Name = recipient.UserName,
                Subject = subject,
                Body = $"Dear {recipient.UserName},<br>{body}"
            };

            await _backgroundJobManager.EnqueueAsync(adminAgentEmailArgs);
        }
    }

    public async Task SendTicketEmailToAgent(Guid id)
    {
        var ticket = await _commentRepository.GetDetailOfTicketAsync(id);

        if (ticket == null)
        {
            throw new UserFriendlyException("Ticket not found");
        }

        var assignedUserEmail = ticket.AssignedToUser?.Email;
        var assignedUserName = ticket.AssignedToUser?.UserName;

        if (assignedUserEmail == null && assignedUserName == null)
        {
            return;
        }

        var emailArgs = new EmailSendingArgs
        {
            EmailAddress = assignedUserEmail!,
            Name = assignedUserName!,
            Subject = $"New Ticket Assigned: {ticket.Title}",
            Body = $@"
                <p>We would like to inform you that a new ticket has been assigned to you. Below are the details:</p>
                <ul>
                <li><strong>Ticket Title:</strong> {ticket.Title}</li>
                <li><strong>Status:</strong> {ticket.StatusType}</li>
                <li><strong>Created By:</strong> {ticket.User?.Name ?? "Unknown"}</li>
                </ul>
                <p>Please review the ticket and take the necessary actions at your earliest convenience.</p>
                <p>Thank you for your attention.</p>
                <p style='color:red;'><strong>Note:</strong> Please do not reply to this email as it is auto-generated.</p>"
        };

        await _backgroundJobManager.EnqueueAsync(emailArgs);
    }
}
