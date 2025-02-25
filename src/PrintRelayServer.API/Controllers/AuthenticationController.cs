using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace PrintRelayServer.API.Controllers;

[Route("/Api/[controller]/[action]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Refresh()
    {
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync();
        return Ok();
    }
}