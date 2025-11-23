using Clinic.Application.DTOs.Message;
using Clinic.Application.DTOs.User;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace Clinic.API.Hubs;

[Authorize]
public class ChatHub(UserManager<ApplicationUser> userManager,ApplicationDbContext dbContext) : Hub
{

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User!.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new InvalidOperationException("UserId missing");

        var connectionId = Context.ConnectionId;

        dbContext.ConnectionSessions.Add(new ConnectionSession
        {
            UserId = userId,
            ConnectionId = connectionId,
            ConnectedAt = DateTime.UtcNow
        });

        var user = await userManager.FindByIdAsync(userId);

        if(user is null)
            return;

        user.IsOnline = true;
        user.LastSeen = DateTime.Now;

        await dbContext.SaveChangesAsync();

        var onlineUser = new OnlineUserDto
        {
            Id = user.Id,
            UserName = user.UserName!,
            FullName = user.FullName,
            ImageUrl = user.ImageUrl
        };

        await Clients.User(userId).SendAsync("OnlineUsers", await GetAllUsers(userId));
        await Clients.AllExcept(userId).SendAsync("NotifyOnlineUser", onlineUser);

        await Clients.Others.SendAsync("UserBecameOnline", onlineUser);

        await base.OnConnectedAsync();

    }

    public async Task SendMessage(MessageRequestDto request)
    {
        var userId = Context.User!.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new InvalidOperationException("UserId missing");

        var newMsg = new Message
        {
            SenderId = userId,
            ReceiverId = request.ReceiverId,
            Content = request.Content,
            CreatedDate = DateTime.UtcNow,
            IsRead = false,
        };

        dbContext.Messages.Add(newMsg);
        await dbContext.SaveChangesAsync();

        await Clients.User(request.ReceiverId).SendAsync("ReceiveNewMessage", newMsg);
    }


    public async Task NotifyTyping(string recipientUserId)
    {
        var userId = Context.User!.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new InvalidOperationException("UserId missing");

        await Clients.User(recipientUserId).SendAsync("NotifyTypingToUser",userId);
    }

    public async Task LoadMessages(string recipientId,int pageNumber = 1)
    {
        int pageSize = 10;
        var userId = Context.User!.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new InvalidOperationException("UserId missing");

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
            return;
        var count = await dbContext.Messages.CountAsync(m => (m.ReceiverId == recipientId && m.SenderId == userId) || (m.ReceiverId == userId && m.SenderId == recipientId));

        var messages = await dbContext.Messages
            .Where(m => (m.ReceiverId == recipientId && m.SenderId == userId) || (m.ReceiverId == userId && m.SenderId == recipientId))
            .OrderByDescending(m => m.CreatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(m => m.CreatedDate)
            .Select(m => new MessageResponseDto
            {
                Id = m.Id,
                Content = m.Content,
                CreatedDate = m.CreatedDate,
                ReceiverId = m.ReceiverId,
                SenderId = m.SenderId,
                IsRead = m.IsRead
            }).ToListAsync();

        foreach (var message in messages)
        {
            if(message is not null && message.ReceiverId == userId)
            {
                message.IsRead = true;
                await dbContext.SaveChangesAsync();
            }
        }

        var totalPages = count/pageSize + 1;
        Console.WriteLine(totalPages);
        await Clients.User(userId).SendAsync("ReceiveMessageList", messages,totalPages);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return;

        var connectionId = Context.ConnectionId;

        var session = await dbContext.ConnectionSessions
            .FirstOrDefaultAsync(x => x.ConnectionId == connectionId);

        if (session != null)
        {
            session.DisconnectedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
        }

        
        var user = await userManager.FindByIdAsync(userId);

        if(user is null)
            return;

        user.IsOnline = false;
        user.LastSeen = DateTime.Now;

        await dbContext.SaveChangesAsync();

        await Clients.Others.SendAsync("UserWentOffline", new {user.Id,user.LastSeen});

        await base.OnDisconnectedAsync(exception);
    }
    private async Task<IEnumerable<OnlineUserDto>> GetAllUsers(string viewerId)
    {
        var users =await userManager.Users.Select(u => new OnlineUserDto
        {
            Id = u.Id,
            FullName = u.FullName,
            UserName = u.UserName!,
            ImageUrl = u.ImageUrl,
            IsOnline = u.IsOnline,
            LastSeen = u.LastSeen,
            UnReadCount = dbContext.Messages.Count(m => m.SenderId == u.Id && m.ReceiverId == viewerId && !m.IsRead)
        })
        .Where(u=>u.Id!=viewerId)
        .OrderByDescending(u => u.IsOnline)
        .ToListAsync();

        return users;
    }
}
