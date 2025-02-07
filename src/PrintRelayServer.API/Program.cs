using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrintRelayServer.Domain.Entities.Identity;
using PrintRelayServer.Infrastructure.Contexts;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

// Add DbContext
builder.Services.AddDbContext<PrintRelayContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PrintRelayDB")));

// Add Identity
builder.Services.AddIdentity<AppUser, AppRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<PrintRelayContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();


app.MapOpenApi();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();