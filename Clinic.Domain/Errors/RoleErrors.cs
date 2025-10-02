using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Errors;
public static class RoleErrors
{
    public static readonly Error RoleNotFound = new("Role.NotFound", "No role was found with the given Id", StatusCodes.Status404NotFound);
    public static readonly Error DuplicatedRoleTitle = new("Role.DuplicatedTitle", "Another role with the same title is aleardy exist", StatusCodes.Status409Conflict);
    public static readonly Error InvalidPermission = new("Role.InvalidPermission", "Invalid permission", StatusCodes.Status400BadRequest);
}
