using Clinic.Application.DTOs.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Services;
public interface INotificationSender
{
    Task SendToUserAsync(string userId, NotificationRequest notification);
}
