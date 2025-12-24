using Clinic.Application.DTOs.Message;
using Clinic.Application.DTOs.User;
using Clinic.Domain.Consts;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace Clinic.Infrastructure.Hubs;

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
            ImageUrl = user.ImageUrl,
            PhoneNumber = user.PhoneNumber
        };
        // TODO : for secretary get doctor and patients 
        if(Context.User!.IsInRole(AppRoles.Patient))
            await Clients.User(userId).SendAsync("OnlineUsers", await GetAllSecretaries(userId));
        else
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

        await Clients.Users(request.ReceiverId,userId).SendAsync("ReceiveNewMessage", newMsg);
    }


    public async Task NotifyTyping(string recipientUserId)
    {
        var userId = Context.User!.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new InvalidOperationException("UserId missing");

        await Clients.User(recipientUserId).SendAsync("NotifyTypingToUser",userId);
    }
    public async Task DeleteMessage(int messageId)
    {
        var userId = Context.User!.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new InvalidOperationException("UserId missing");

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
            return;

        var message = await dbContext.Messages
            .Where(m=>m.Id==messageId && m.SenderId==userId)
            .FirstOrDefaultAsync();

        if(message == null)
            return;

        dbContext.Messages.Remove(message);
        await dbContext.SaveChangesAsync();
        
        await Clients.Users(userId,message.ReceiverId).SendAsync("DeletedMessage",message);
    }

    public async Task UpdateMessage(UpdateMessageRequestDto request)
    {
        var userId = Context.User!.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new InvalidOperationException("UserId missing");

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
            return;

        var message = await dbContext.Messages
            .Where(m => m.Id == request.Id && m.SenderId == userId)
            .FirstOrDefaultAsync();

        if (message == null) return;

        message.Content = request.Content;

        await dbContext.SaveChangesAsync();

        await Clients.Users(userId, message.ReceiverId).SendAsync("UpdatedMessage", message);
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
            .ToListAsync();

        foreach (var message in messages)
        {
            if(message is not null && message.ReceiverId == userId)
            {
                message.IsRead = true;
            }
        }
        await dbContext.SaveChangesAsync();


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
            dbContext.ConnectionSessions.Remove(session);
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
        return await (from u in dbContext.Users 
         join ur in dbContext.UserRoles on u.Id equals ur.UserId
         join r in dbContext.Roles on ur.RoleId equals r.Id into roles
         where !roles.Any(x=>x.Name == AppRoles.Patient) && u.Id!=viewerId && !u.IsDisabled && !u.IsDeleted
         select new OnlineUserDto
         {
             Id = u.Id,
             FullName = u.FullName,
             UserName = u.UserName!,
             ImageUrl = u.ImageUrl,
             IsOnline = u.IsOnline,
             LastSeen = u.LastSeen,
             PhoneNumber = u.PhoneNumber,
         })
         .Distinct()
         .OrderByDescending(u=>u.IsOnline)
         .ToListAsync();
    }

    private async Task<IEnumerable<OnlineUserDto>> GetAllSecretaries(string viewerId)
    {
        return await (from u in dbContext.Users
                      join ur in dbContext.UserRoles on u.Id equals ur.UserId
                      join r in dbContext.Roles on ur.RoleId equals r.Id into roles
                      where roles.Any(x => x.Name == AppRoles.Secretary) && u.Id != viewerId && !u.IsDisabled && !u.IsDeleted
                      select new OnlineUserDto
                      {
                          Id = u.Id,
                          FullName = u.FullName,
                          UserName = u.UserName!,
                          ImageUrl = u.ImageUrl,
                          IsOnline = u.IsOnline,
                          LastSeen = u.LastSeen,
                          PhoneNumber = u.PhoneNumber,
                      })
         .Distinct()
         .OrderByDescending(u => u.IsOnline)
         .ToListAsync();
    }
}
