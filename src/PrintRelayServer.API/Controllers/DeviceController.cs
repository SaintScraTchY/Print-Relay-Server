using Microsoft.AspNetCore.Mvc;
using PrintRelayServer.Application.Application.Interfaces;
using PrintRelayServer.Shared.Contracts.Device;
using PrintRelayServer.Shared.Contracts.DeviceType;
using PrintRelayServer.Shared.Contracts.DeviceTypeOption;

namespace PrintRelayServer.API.Controllers;

[Route("/Api/[controller]/[action]")]
[ApiController]
public class DeviceController : ControllerBase
{
    private readonly IDeviceApplication _deviceApplication;
    public DeviceController(IDeviceApplication deviceApplication)
    {
        _deviceApplication = deviceApplication;
    }

    #region Device

    [HttpGet]
    public async Task<IActionResult> GetAllDevices(int pageNumber, int pageSize)
    {
        return Ok(await _deviceApplication.GetDeviceList(pageNumber, pageSize));
    }

    [HttpPut("{key:guid}")]
    public async Task<ActionResult> EditDevice(Guid key,EditDevice editDevice)
    {
        var result = await _deviceApplication.EditDevice(key,editDevice);
        return result.IsSucceeded ? Ok(result) : BadRequest(result); 
    }

    [HttpDelete("{key:guid}")]
    public async Task<ActionResult> DeleteDevice(Guid key)
    {
        var result = await _deviceApplication.RemoveDevice(key);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }

    #endregion

    #region DeviceType

    [HttpGet("Type")]
    public async Task<IActionResult> GetAllDeviceTypes(int pageNumber, int pageSize)
    {
        return Ok(await _deviceApplication.GetDeviceTypeList(pageNumber, pageSize));
    }

    [HttpPost("Type")]
    public async Task<IActionResult> CreateDeviceType(AddDeviceType addDeviceType)
    {
        var result = await _deviceApplication.AddDeviceType(addDeviceType);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }

    [HttpPut("Type/{key:guid}")]
    public async Task<ActionResult> EditDeviceType(Guid key,EditDeviceType editDeviceType)
    {
        var result = await _deviceApplication.EditDeviceType(key, editDeviceType);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("Type/{key:guid}")]
    public async Task<ActionResult> DeleteDeviceType(Guid key)
    {
        var result = await _deviceApplication.RemoveDeviceType(key);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    #endregion

    #region DeviceTypeOption

    [HttpGet("TypeOption/{key:guid}")]
    public async Task<IActionResult> GetAllDeviceTypeOptionsByType(Guid key)
    {
        return Ok(await _deviceApplication.GetDeviceTypeOptionList(key));
    }

    [HttpPost("TypeOption")]
    public async Task<IActionResult> CreateDeviceTypeOption(AddDeviceTypeOption addDeviceTypeOption)
    {
        var result = await _deviceApplication.AddDeviceTypeOption(addDeviceTypeOption);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }

    [HttpPut("TypeOption/{key:guid}")]
    public async Task<ActionResult> EditDeviceTypeOption(Guid key,EditDeviceTypeOption editDeviceTypeOption)
    {
        var result = await _deviceApplication.EditDeviceTypeOption(key,editDeviceTypeOption);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("TypeOption/{key:guid}")]
    public async Task<ActionResult> DeleteDeviceTypeOption(Guid key)
    {
        var result = await _deviceApplication.RemoveDeviceTypeOption(key);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
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