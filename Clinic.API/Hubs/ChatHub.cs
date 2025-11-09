using Clinic.Application.DTOs.Message;
using Clinic.Application.DTOs.User;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace Clinic.API.Hubs;

[Authorize]
public class ChatHub(UserManager<ApplicationUser> userManager,ApplicationDbContext dbContext) : Hub
{
    public static readonly ConcurrentDictionary<string, OnlineUserDto> onlineUsers = new();

    public override async Task OnConnectedAsync()
    {

        await base.OnConnectedAsync();

        var httpContext = Context.GetHttpContext();
        var receiverId = httpContext?.Request.Query["senderId"].ToString();
        var userName = Context.User!.Identity!.Name!;
        var currentUser =await userManager.FindByNameAsync(userName);
        var connectionId = Context.ConnectionId;

        if (onlineUsers.ContainsKey(userName))
        {
            onlineUsers[userName].ConnectionId = connectionId;
        }
        else
        {
            var user = new OnlineUserDto
            {
                ConnectionId = connectionId,
                UserName = userName,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                ImageUrl = currentUser.ImageUrl,
            };

            onlineUsers.TryAdd(userName, user);

            await Clients.AllExcept(connectionId).SendAsync("Notify", currentUser);
        }

        if (!string.IsNullOrEmpty(receiverId))
        {
            await LoadMessages(receiverId);
        }

        await Clients.User(currentUser.Id).SendAsync("OnlineUsers", await GetAllUsers());
    }

    public async Task SendMessage(MessageRequestDto request)
    {
        var newMsg = new Message
        {
            SenderId = Context.User.GetUserId(),
            ReceiverId = request.ReceiverId,
            Content = request.Content,
            CreatedDate = DateTime.UtcNow,
            IsRead = false,
        };

        dbContext.Messages.Add(newMsg);
        await dbContext.SaveChangesAsync();

        await Clients.User(request.ReceiverId).SendAsync("ReceiveNewMessage", newMsg);
    }


    public async Task NotifyTyping(string recipientUserName)
    {
        var senderUserName = Context.User.GetUserName();
        if (senderUserName is null)
            return;
        var connectionId = onlineUsers.Values.FirstOrDefault(x => x.UserName == recipientUserName)?.ConnectionId;

        if(connectionId is not null)
        {
            await Clients.Client(connectionId).SendAsync("NotifyTypingToUser", senderUserName);
        }
    }

    public async Task LoadMessages(string recipientId,int pageNumber = 1)
    {
        int pageSize = 10;
        var userName = Context.User.GetUserName();
        var currentUser = await userManager.FindByNameAsync(userName);

        if (currentUser is null)
            return;

        List<MessageResponseDto> messages = await dbContext.Messages
            .Where(m => (m.ReceiverId == recipientId && m.SenderId == currentUser.Id) || (m.ReceiverId == currentUser.Id && m.SenderId == recipientId))
            .OrderByDescending(x => x.CreatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(x => x.CreatedDate)
            .Select(x => new MessageResponseDto
            {
                Id = x.Id,
                Content = x.Content,
                CreatedDate = x.CreatedDate,
                ReceiverId = x.ReceiverId,
                SenderId = x.SenderId
            }).ToListAsync();


        foreach (var message in messages)
        {
            var msg =await dbContext.Messages.FirstOrDefaultAsync(x => x.Id == message.Id);

            if(msg is not null && msg.ReceiverId == currentUser.Id)
            {
                msg.IsRead = true;
                await dbContext.SaveChangesAsync();
            }
        }

        await Clients.User(currentUser.Id).SendAsync("ReceiveMessageList", messages);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userName = Context.User.GetUserName();

        onlineUsers.TryRemove(userName, out _);
        await Clients.All.SendAsync("OnlineUsers", await GetAllUsers());
    }
    private async Task<IEnumerable<OnlineUserDto>> GetAllUsers()
    {
        var userName = Context.User.GetUserName();
        var receiver = await userManager.FindByNameAsync(userName);

        var onlineUsersSet = new HashSet<string>(onlineUsers.Keys);

        var users =await userManager.Users.Select(u => new OnlineUserDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            UserName = u.UserName,
            ImageUrl = u.ImageUrl,
            IsOnline = onlineUsersSet.Contains(u.UserName),
            UnReadCount = dbContext.Messages.Count(m => m.SenderId == u.Id && m.ReceiverId == receiver.Id && !m.IsRead)
        }).OrderByDescending(u => u.IsOnline)
        .ToListAsync();

        return users;
    }
}
