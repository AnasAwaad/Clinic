using Clinic.Application.DTOs.Notification;
using Clinic.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Services;
public class NotificationSender(IHubContext<NotificationHub> hubContext) : INotificationSender
{
    public async Task SendToUserAsync(string userId, NotificationRequest notification)
    {
        await hubContext
            .Clients
            .Group(userId)
            .SendAsync("ReceiveNotification", notification);
    }
}
