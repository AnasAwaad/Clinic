using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;
public class Message
{
    public int Id { get; set; }
    public string SenderId { get; set; } = string.Empty;
    public string ReceiverId { get; set; } = string.Empty;
    public byte[] EncryptedMessage { get; set; } = [];
    public byte[] EncryptedAesKey { get; set; } = [];
    public byte[] Iv { get; set; } = [];
    public DateTime CreatedDate { get; set; }
    public bool IsRead { get; set; }
    public ApplicationUser? Sender { get; set; }
    public ApplicationUser? Receiver { get; set; }
}
