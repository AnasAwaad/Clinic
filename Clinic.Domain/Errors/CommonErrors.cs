using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Errors;
public static class CommonErrors
{
    public static readonly Error InvalidRange = new("Common.InvalidRange","End date must be greater than start date.",StatusCodes.Status400BadRequest);

    public static readonly Error RangeTooLarge = new("Common.RangeTooLarge","Requested range is too large. Try a smaller period.",StatusCodes.Status400BadRequest);
}
