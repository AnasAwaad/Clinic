using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Consts;
public static class AppRoles
{
    public const string SuperAdmin = nameof(SuperAdmin);
    public const string Doctor = nameof(Doctor);
    public const string Secretary = nameof(Secretary);
    public const string Patient = nameof(Patient);
}
