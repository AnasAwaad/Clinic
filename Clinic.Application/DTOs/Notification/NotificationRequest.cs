using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Notification;
public record NotificationRequest(
    int Id,
    string Title,
    string Message,
    DateTime CreatedAt,
    bool IsRead
);