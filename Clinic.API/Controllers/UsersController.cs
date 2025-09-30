using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    //[HasPermission(Permissions.GetUsers)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await userService.GetAllAsync());
    }
}
