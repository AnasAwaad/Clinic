using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Role;
public class RoleRequest
{
    public string Name { get; set; } = string.Empty;
    public IList<string> Permissions { get; set; } = default!;
}
