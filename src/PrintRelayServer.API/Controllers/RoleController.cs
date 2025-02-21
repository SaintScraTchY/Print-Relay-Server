using Microsoft.AspNetCore.Mvc;
using PrintRelayServer.Shared.Contracts.Roles;

namespace PrintRelayServer.API.Controllers;

[Route("/Api/[controller]/[action]")]
[ApiController]
public class RoleController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRoles(int pageNumber, int pageSize)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddRole()
    {
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> EditRole()
    {
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveRole(Guid roleId)
    {
        return Ok();
    }
     
}