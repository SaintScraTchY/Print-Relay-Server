using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrintRelayServer.Application.Application.Interfaces;
using PrintRelayServer.Domain.Entities.Identity;
using PrintRelayServer.Shared.Contracts.Users;

namespace PrintRelayServer.API.Controllers;

[Route("/Api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserApplication _userApplication;
    public UserController(UserManager<AppUser> userManager, IUserApplication userApplication)
    {
        _userApplication = userApplication;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers(int pageNumber, int pageSize)
    {
        return Ok( await _userApplication.GetUsers(pageNumber, pageSize));
    }
    
    [HttpPost]
    public async Task<IActionResult> GetUsersByFilter(int pageNumber, int pageSize,GetUserFilter filter)
    {
        
        return Ok( await _userApplication.GetUsers(pageNumber, pageSize,filter));
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(AddUser addUser)
    {
        return Ok(await _userApplication.AddUser(addUser));
    }
    
    [HttpPut]
    public async Task<IActionResult> EditUser(Guid userId,EditUser editUser)
    {
        return Ok(await _userApplication.EditUser(userId,editUser));
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveUser(Guid userId)
    {
        return Ok(await _userApplication.RemoveUser(userId));
    }
}