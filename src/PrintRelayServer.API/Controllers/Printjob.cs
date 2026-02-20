using Microsoft.AspNetCore.Mvc;
using PrintRelayServer.Application.Application.Interfaces;
using PrintRelayServer.Domain.Entities.PrintAgg;

namespace PrintRelayServer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrintJobsController : ControllerBase
{
    private readonly IPrintJobService _printJobService;
    private readonly ILogger<PrintJobsController> _logger;
    
    // TODO: Replace with real user ID from auth context
    private static readonly Guid TEMP_USER_ID = Guid.Parse("00000000-0000-0000-0000-000000000001");
    
    public PrintJobsController(
        IPrintJobService printJobService,
        ILogger<PrintJobsController> logger)
    {
        _printJobService = printJobService;
        _logger = logger;
    }
    
    /// <summary>
    /// Create a new print job
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreatePrintJob([FromBody] CreatePrintJobRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            // Create print job detail
            var detail = new PrintJobDetail(
                printerIdentifier: request.PrinterIdentifier ?? "default",
                width: request.Width ?? 0,
                height: request.Height ?? 0,
                paperSizeUnit: request.PaperSizeUnit,
                printPaper: request.PrintPaper,
                priority: request.Priority,
                requestCount: request.Copies,
                margins: request.Margins ?? "0",
                quality: request.Quality);
            
            // Create print job
            var jobId = await _printJobService.CreatePrintJobAsync(
                TEMP_USER_ID, // TODO: Get from auth context
                request.DeviceId,
                request.FileId,
                detail);
            
            _logger.LogInformation(
                "Print job created: {JobId} for device {DeviceId}",
                jobId, request.DeviceId);
            
            return Ok(new { jobId });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create print job");
            return StatusCode(500, "Failed to create print job");
        }
    }
    
    /// <summary>
    /// Get job by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetJob(Guid id)
    {
        var job = await _printJobService.GetJobByIdAsync(id);
        
        if (job == null)
            return NotFound();
        
        return Ok(new
        {
            id = job.Id,
            status = job.Status.ToString(),
            deviceId = job.DeviceId,
            deviceName = job.Device?.Name,
            fileId = job.FileId,
            fileName = job.File?.FileName,
            assignedAgentId = job.AssignedAgentId,
            createdAt = job.CreatedAt,
            assignedAt = job.AssignedAt,
            startedAt = job.StartedAt,
            completedAt = job.CompletedAt,
            errorMessage = job.ErrorMessage,
            retryCount = job.RetryCount
        });
    }
    
    /// <summary>
    /// Cancel a print job
    /// </summary>
    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelJob(Guid id)
    {
        try
        {
            await _printJobService.CancelJobAsync(id, TEMP_USER_ID);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

/// <summary>
/// Request to create a print job
/// </summary>
public class CreatePrintJobRequest
{
    public Guid DeviceId { get; set; }
    public Guid FileId { get; set; }
    
    // Print settings
    public string? PrinterIdentifier { get; set; }
    public float? Width { get; set; }
    public float? Height { get; set; }
    public PrintPaperUnit PaperSizeUnit { get; set; } = PrintPaperUnit.Millimeter;
    public PrintPaper PrintPaper { get; set; } = PrintPaper.A4;
    public PrintPriority Priority { get; set; } = PrintPriority.Medium;
    public uint Copies { get; set; } = 1;
    public string? Margins { get; set; }
    public PrintQuality Quality { get; set; } = PrintQuality.Average;
}