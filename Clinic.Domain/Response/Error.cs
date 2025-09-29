using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Response;
public record Error(string code, string Description, int? statusCode)
{
    public static readonly Error None = new(string.Empty, string.Empty, null);
}
