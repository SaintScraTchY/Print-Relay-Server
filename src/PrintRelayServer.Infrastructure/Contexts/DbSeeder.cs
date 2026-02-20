using Microsoft.EntityFrameworkCore;
using PrintRelayServer.Domain.Entities.DeviceAgg;

namespace PrintRelayServer.Infrastructure.Contexts;

/// <summary>
/// Seeds initial data for development and testing.
/// Call this from Program.cs during development.
/// </summary>
public static class DbSeeder
{
    public static async Task SeedAsync(PrintRelayContext context)
    {
        // Ensure database is created
        await context.Database.MigrateAsync();
        
        // Seed only if database is empty
        if (await context.DeviceTypes.AnyAsync())
            return; // Already seeded
        
        Console.WriteLine("🌱 Seeding database...");
        
        // Seed Device Types
        await SeedDeviceTypes(context);
        
        Console.WriteLine("✅ Database seeded successfully!");
    }
    
    private static async Task SeedDeviceTypes(PrintRelayContext context)
    {
        var deviceTypes = new[]
        {
            new DeviceType("Thermal Printer", "Receipt and label printers using thermal technology"),
            new DeviceType("Laser Printer", "High-speed office laser printers"),
            new DeviceType("Inkjet Printer", "Color inkjet printers for documents and photos"),
            new DeviceType("Label Printer", "Specialized label and barcode printers"),
            new DeviceType("3D Printer", "Additive manufacturing 3D printers"),
            new DeviceType("Virtual Printer", "PDF or file-based virtual printers for testing")
        };
        
        await context.DeviceTypes.AddRangeAsync(deviceTypes);
        await context.SaveChangesAsync();
        
        Console.WriteLine($"  ✓ Seeded {deviceTypes.Length} device types");
    }
    
    /// <summary>
    /// Seeds test data including a sample user, device, and agent.
    /// Only call this in development/testing environments!
    /// </summary>
    public static async Task SeedTestDataAsync(PrintRelayContext context, Guid testUserId)
    {
        if (await context.Devices.AnyAsync())
            return; // Already has test data
        
        Console.WriteLine("🧪 Seeding test data...");
        
        // Get virtual printer device type
        var virtualPrinterType = await context.DeviceTypes
            .FirstAsync(dt => dt.Name == "Virtual Printer");
        
        // Create test device
        var testDevice = new Device(
            name: "Test Virtual Printer",
            description: "Virtual printer for development and testing",
            osIdentifier: "VIRTUAL_TEST_001",
            ownerId: testUserId,
            deviceTypeId: virtualPrinterType.Id
        );
        
        await context.Devices.AddAsync(testDevice);
        await context.SaveChangesAsync();
        
        Console.WriteLine("  ✓ Created test device");
        Console.WriteLine("✅ Test data seeded successfully!");
    }
}