using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrintRelayServer.Domain.Entities.Identity;
using PrintRelayServer.Shared.Contracts.Users;

namespace PrintRelayServer.API.Controllers;

[Route("/Api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    public UserController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers(int pageNumber, int pageSize)
    {
        
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(AddUser addUser)
    {
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> EditUser(EditUser editUser)
    {
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveUser(Guid userId)
    {
        return Ok();
    }
}