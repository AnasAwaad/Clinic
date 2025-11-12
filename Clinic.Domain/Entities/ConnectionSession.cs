using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;
public class ConnectionSession
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string ConnectionId { get; set; } = null!;
    public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DisconnectedAt { get; set; }
}
