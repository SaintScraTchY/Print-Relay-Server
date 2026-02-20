using Microsoft.AspNetCore.Mvc;
using PrintRelayServer.Application.Application.Interfaces;
using PrintRelayServer.Domain.Entities.FileAgg;
using PrintRelayServer.Infrastructure.Contexts;

namespace PrintRelayServer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IFileStorageService _fileStorage;
    private readonly PrintRelayContext _db;
    private readonly ILogger<FilesController> _logger;
    
    // TODO: Replace with real user ID from auth context
    private static readonly Guid TEMP_USER_ID = Guid.Parse("00000000-0000-0000-0000-000000000001");
    
    public FilesController(
        IFileStorageService fileStorage,
        PrintRelayContext db,
        ILogger<FilesController> logger)
    {
        _fileStorage = fileStorage;
        _db = db;
        _logger = logger;
    }
    
    /// <summary>
    /// Upload a file for printing
    /// </summary>
    /// <param name="file">File to upload</param>
    /// <returns>File metadata including ID</returns>
    [HttpPost("upload")]
    [RequestSizeLimit(100_000_000)] // 100MB max
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");
        
        // Validate file size (100MB max)
        if (file.Length > 100_000_000)
            return BadRequest("File too large (max 100MB)");
        
        try
        {
            // Store file
            StoredFileMetadata metadata;
            await using (var stream = file.OpenReadStream())
            {
                metadata = await _fileStorage.StoreFileAsync(
                    file.FileName,
                    stream,
                    file.ContentType);
            }
            
            // Create database record
            var managedFile = new ManagedFile(
                //TEMP_USER_ID, // TODO: Get from auth context  
                file.FileName,
                metadata.FilePath,
                metadata.Size,
                metadata.ContentType);
            
            managedFile.SetHash(metadata.Hash);
            
            _db.ManagedFiles.Add(managedFile);
            await _db.SaveChangesAsync();
            
            _logger.LogInformation(
                "File uploaded: {FileName} ({Size} bytes, ID: {FileId})",
                file.FileName, metadata.Size, managedFile.Id);
            
            return Ok(new
            {
                id = managedFile.Id,
                fileName = file.FileName,
                size = metadata.Size,
                contentType = metadata.ContentType,
                hash = metadata.Hash
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "File upload failed for {FileName}", file.FileName);
            return StatusCode(500, "File upload failed");
        }
    }
    
    /// <summary>
    /// Download a file by ID
    /// </summary>
    /// <param name="id">File ID</param>
    [HttpGet("{id}/download")]
    public async Task<IActionResult> DownloadFile(Guid id)
    {
        var file = await _db.ManagedFiles.FindAsync(id);
        
        if (file == null)
            return NotFound();
        
        try
        {
            var stream = await _fileStorage.GetFileStreamAsync(file.Path);
            
            return File(
                stream, 
                file.ContentType ?? "application/octet-stream", 
                file.FileName);
        }
        catch (FileNotFoundException)
        {
            _logger.LogError("File not found in storage: {FilePath}", file.Path);
            return NotFound("File not found in storage");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "File download failed for {FileId}", id);
            return StatusCode(500, "File download failed");
        }
    }
    
    /// <summary>
    /// Get file metadata
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFileInfo(Guid id)
    {
        var file = await _db.ManagedFiles.FindAsync(id);
        
        if (file == null)
            return NotFound();
        
        return Ok(new
        {
            id = file.Id,
            fileName = file.FileName,
            size = file.Size,
            contentType = file.ContentType,
            hash = file.Hash,
            uploadedAt = file.CreatedAt
        });
    }
}