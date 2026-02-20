using Microsoft.AspNetCore.Mvc;
using PrintRelayServer.Application.Application.Interfaces;
using PrintRelayServer.Shared.Contracts.PrintJob;

namespace PrintRelayServer.API.Controllers;

[Route("/api/[controller]/[action]")]
[ApiController]
public class PrintController : ControllerBase
{
    private readonly IPrintApplication _printApplication;

    public PrintController(IPrintApplication printApplication)
    {
        _printApplication = printApplication;
    }

    public async Task<IActionResult> GetPrintJobs(int pageNumber, int pageSize)
    {
        return Ok(await _printApplication.GetPrintJobs(pageNumber, pageSize));
    }

    public async Task<IActionResult> GetPrintJobsByFilter(int pageNumber, int pageSize, GetPrintFilter filter)
    {
        return Ok(await _printApplication.GetPrintJobsByFilter(pageNumber,pageSize,filter));
    }
}