using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrintRelayServer.Application.Application.Implementations;
using PrintRelayServer.Application.Application.Interfaces;

namespace PrintRelayServer.Application.Configuration;

/// <summary>
/// Extension methods for registering services in DI.
/// Call this from Program.cs or Startup.cs
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPrintJobService, PrintJobService>();
        services.AddScoped<IAgentRegistryService, AgentRegistryService>();
        services.AddScoped<IFileStorageService, LocalFileStorage>();
        services.AddScoped<IUserApplication, UserApplication>();
        services.AddScoped<IDeviceApplication,DeviceApplication>();
        services.AddScoped<PrintApplication,PrintApplication>();
        
        return services;
    }
}