using Clinic.Application.DTOs.Notification;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Services;
public class NotificationService(IUnitOfWork unitOfWork,INotificationSender notificationSender) : INotificationService
{
    public async Task SendAsync(string userId, string title, string message)
    {
        var notification = new Notification
        {
            UserId = userId,
            Title = title,
            Message = message
        };

        await unitOfWork.Notifications.AddAsync(notification);
        await unitOfWork.SaveAsync();

        var request = new NotificationRequest(
            notification.Id,
            notification.Title,
            notification.Message,
            notification.CreatedAt,
            notification.IsRead
        );

        await notificationSender.SendToUserAsync(userId, request);
    }
}
