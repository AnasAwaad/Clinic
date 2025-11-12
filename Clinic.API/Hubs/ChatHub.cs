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
    //public static readonly ConcurrentDictionary<string, OnlineUserDto> onlineUsers = new();

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User!.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new InvalidOperationException("UserId missing");


        //var httpContext = Context.GetHttpContext();
        //var receiverId = httpContext?.Request.Query["senderId"].ToString();
        //var userName = Context.User.GetUserName();
        //var currentUser =await userManager.FindByNameAsync(userName);

        var connectionId = Context.ConnectionId;

        dbContext.ConnectionSessions.Add(new ConnectionSession
        {
            UserId = userId,
            ConnectionId = connectionId,
            ConnectedAt = DateTime.UtcNow
        });

        var user = await userManager.FindByIdAsync(userId);

        user.IsOnline = true;
        user.LastSeen = DateTime.Now;

        await dbContext.SaveChangesAsync();

        await Clients.User(userId).SendAsync("OnlineUsers", await GetAllUsers(userId));

        await Clients.Others.SendAsync("UserBecameOnline", new
        {
            Id = user.Id,
            userName = user.UserName,
            firstName = user.FirstName,
            lastName = user.LastName,
            imageUrl = user.ImageUrl
        });

        await base.OnConnectedAsync();


        //if (onlineUsers.ContainsKey(userName))
        //{
        //    onlineUsers[userName].ConnectionId = connectionId;
        //}
        //else
        //{
        //    var user = new OnlineUserDto
        //    {
        //        ConnectionId = connectionId,
        //        UserName = userName,
        //        FirstName = currentUser.FirstName,
        //        LastName = currentUser.LastName,
        //        ImageUrl = currentUser.ImageUrl,
        //    };

        //    onlineUsers.TryAdd(userName, user);

        //    await Clients.AllExcept(connectionId).SendAsync("Notify", currentUser);
        //}

        //if (!string.IsNullOrEmpty(receiverId))
        //{
        //    await LoadMessages(receiverId);
        //}

        //await Clients.User(currentUser.Id).SendAsync("OnlineUsers", await GetAllUsers());
    }

    public async Task SendMessage(MessageRequestDto request)
    {
        //var newMsg = new Message
        //{
        //    SenderId = Context.User.GetUserId(),
        //    ReceiverId = request.ReceiverId,
        //    Content = request.Content,
        //    CreatedDate = DateTime.UtcNow,
        //    IsRead = false,
        //};

        //dbContext.Messages.Add(newMsg);
        //await dbContext.SaveChangesAsync();

        //await Clients.User(request.ReceiverId).SendAsync("ReceiveNewMessage", newMsg);
    }


    public async Task NotifyTyping(string recipientUserName)
    {
        //var senderUserName = Context.User.GetUserName();
        //if (senderUserName is null)
        //    return;
        //var connectionId = onlineUsers.Values.FirstOrDefault(x => x.UserName == recipientUserName)?.ConnectionId;

        //if(connectionId is not null)
        //{
        //    await Clients.Client(connectionId).SendAsync("NotifyTypingToUser", senderUserName);
        //}
    }

    public async Task LoadMessages(string recipientId,int pageNumber = 1)
    {
        //int pageSize = 10;
        //var userName = Context.User.GetUserName();
        //var currentUser = await userManager.FindByNameAsync(userName);

        //if (currentUser is null)
        //    return;

        //List<MessageResponseDto> messages = await dbContext.Messages
        //    .Where(m => (m.ReceiverId == recipientId && m.SenderId == currentUser.Id) || (m.ReceiverId == currentUser.Id && m.SenderId == recipientId))
        //    .OrderByDescending(x => x.CreatedDate)
        //    .Skip((pageNumber - 1) * pageSize)
        //    .Take(pageSize)
        //    .OrderBy(x => x.CreatedDate)
        //    .Select(x => new MessageResponseDto
        //    {
        //        Id = x.Id,
        //        Content = x.Content,
        //        CreatedDate = x.CreatedDate,
        //        ReceiverId = x.ReceiverId,
        //        SenderId = x.SenderId
        //    }).ToListAsync();


        //foreach (var message in messages)
        //{
        //    var msg =await dbContext.Messages.FirstOrDefaultAsync(x => x.Id == message.Id);

        //    if(msg is not null && msg.ReceiverId == currentUser.Id)
        //    {
        //        msg.IsRead = true;
        //        await dbContext.SaveChangesAsync();
        //    }
        //}

        //await Clients.User(currentUser.Id).SendAsync("ReceiveMessageList", messages);
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
        user.IsOnline = false;
        user.LastSeen = DateTime.Now;

        await dbContext.SaveChangesAsync();

        await Clients.Others.SendAsync("UserWentOffline", new {user.Id,user.LastSeen});

        await base.OnDisconnectedAsync(exception);

        //var userName = Context.User.GetUserName();

        //onlineUsers.TryRemove(userName, out _);
        //await Clients.All.SendAsync("OnlineUsers", await GetAllUsers());
    }
    private async Task<IEnumerable<OnlineUserDto>> GetAllUsers(string viewerId)
    {
        var users =await userManager.Users.Select(u => new OnlineUserDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            UserName = u.UserName,
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
