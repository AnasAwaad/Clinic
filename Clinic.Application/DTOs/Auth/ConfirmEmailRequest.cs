using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Auth;
public class ConfirmEmailRequest
{
    public string Id { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

}
