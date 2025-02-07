using Microsoft.AspNetCore.Mvc;

namespace PrintRelayServer.API.Controllers;

[Route("/Api/[controller]/[action]")]
[ApiController]
public class DeviceController : ControllerBase
{
    public DeviceController()
    {
        
    }

    #region Device

    [HttpGet]
    public async Task<IActionResult> GetAllDevices()
    {
        return Ok();
    }

    [HttpPut("{key:guid}")]
    public async Task<ActionResult> EditDevice(Guid key)
    {
        return Ok();
    }

    [HttpDelete("{key:guid}")]
    public async Task<ActionResult> DeleteDevice(Guid key)
    {
        return Ok();
    }

    #endregion

    #region DeviceType

    [HttpGet("Type")]
    public async Task<IActionResult> GetAllDeviceTypes()
    {
        return Ok();
    }

    [HttpPost("Type")]
    public async Task<IActionResult> CreateDeviceType()
    {
        return Ok();
    }

    [HttpPut("Type/{key:guid}")]
    public async Task<ActionResult> EditDeviceType(Guid key)
    {
        return Ok();
    }

    [HttpDelete("Type/{key:guid}")]
    public async Task<ActionResult> DeleteDeviceType(Guid key)
    {
        return Ok();
    }
    #endregion

    #region DeviceTypeOption

    [HttpGet("TypeOption/{key:guid}")]
    public async Task<IActionResult> GetAllDeviceTypeOptionsByType(Guid key)
    {
        return Ok();
    }

    [HttpPost("TypeOption")]
    public async Task<IActionResult> CreateDeviceTypeOption()
    {
        return Ok();
    }

    [HttpPut("TypeOption/{key:guid}")]
    public async Task<ActionResult> EditDeviceTypeOption(Guid key)
    {
        return Ok();
    }

    [HttpDelete("TypeOption/{key:guid}")]
    public async Task<ActionResult> DeleteDeviceTypeOption(Guid key)
    {
        return Ok();
    }
    #endregion

    #region DeviceTypeOptionValue

    [HttpGet("TypeOptionValue/{key:guid}")]
    public async Task<IActionResult> GetAllDeviceTypeOptionValuesByTypeOptin(Guid key)
    {
        return Ok();
    }

    [HttpPost("TypeOptionValue")]
    public async Task<IActionResult> CreateDeviceTypeOptionValue()
    {
        return Ok();
    }

    [HttpPut("TypeOptionValue/{key:guid}")]
    public async Task<ActionResult> EditDeviceTypeOptionValue(Guid key)
    {
        return Ok();
    }

    [HttpDelete("TypeOptionValue/{key:guid}")]
    public async Task<ActionResult> DeleteDeviceTypeOptionValue(Guid key)
    {
        return Ok();
    }
    #endregion
}