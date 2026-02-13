namespace PrintRelayServer.Domain.Entities.PrintAgg;

public enum PrintJobStatus
{
    Pending = 0,      // Created, waiting for agent
    Assigned = 1,     // Sent to agent
    Downloading = 2,  // Agent downloading file
    InQueue = 3,     // Agent has file, waiting for printer
    Paused =4,       // Agent paused the job, waiting for user action
    Printing = 5,     // Actively printing
    Completed = 6,    // Success
    Failed = 7,       // Error occurred
    Cancelled = 8,   // User canceled the job 
}