using Microsoft.AspNetCore.Identity;
using PrintRelayServer.Domain.Entities.Identity;
using PrintRelayServer.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PrintRelayServer.Infrastructure.Configuration;

/// <summary>
/// Extension methods for registering Infrastructure services in DI.
/// Call this from Program.cs or Startup.cs
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add DbContext with PostgreSQL
        services.AddDbContext<PrintRelayContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("PrintRelayDB");
            
            options.UseNpgsql(
                connectionString,
                npgsqlOptions =>
                {
                    // Migrations assembly (where migrations will be stored)
                    npgsqlOptions.MigrationsAssembly("PrintRelayServer.Infrastructure");
                    
                    // Enable retry on failure (for transient network issues)
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorCodesToAdd: null);
                    
                    // Command timeout (30 seconds default)
                    npgsqlOptions.CommandTimeout(30);
                }
            );
            
            // Development settings
            if (configuration.GetValue<bool>("IsDevelopment"))
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });
        
        // Add Identity
        services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<PrintRelayContext>()
            .AddDefaultTokenProviders();
        
        // TODO: Register application services here later
        // services.AddScoped<IPrintJobService, PrintJobService>();
        // services.AddScoped<IAgentRegistryService, AgentRegistryService>();
        // services.AddScoped<IFileStorageService, LocalFileStorage>();
        
        return services;
    }
}

// USAGE IN PROGRAM.CS:
// =====================
// 
// using PrintRelayServer.Infrastructure;
// 
// var builder = WebApplication.CreateBuilder(args);
// 
// // Add Infrastructure (includes DbContext)
// builder.Services.AddInfrastructure(builder.Configuration);
// 
// var app = builder.Build();
// 
// // Seed database in development
// if (app.Environment.IsDevelopment())
// {
//     using var scope = app.Services.CreateScope();
//     var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     await DbSeeder.SeedAsync(db);
// }
// 
// app.Run();