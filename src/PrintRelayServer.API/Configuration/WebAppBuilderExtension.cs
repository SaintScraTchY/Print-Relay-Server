using Microsoft.AspNetCore.Identity; 
using Microsoft.EntityFrameworkCore;
using PrintRelayServer.Domain.Entities.Identity;
using PrintRelayServer.Domain.IRepositories;
using PrintRelayServer.Infrastructure.Contexts;
using PrintRelayServer.Infrastructure.Repositories;

namespace PrintRelayServer.API.Configuration;

public static class WebAppBuilderExtension
{
    public static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
    {
        // Add DbContext
        builder.Services.AddDbContext<PrintRelayContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("PrintRelayDB")));

        // Add Identity
        builder.Services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<PrintRelayContext>()
            .AddDefaultTokenProviders();

        return builder;
    }

    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        return builder;
    }
}