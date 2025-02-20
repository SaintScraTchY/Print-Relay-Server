using Microsoft.AspNetCore.Components;
using PrintRelayServer.Shared.Contracts.Device;

namespace PrintRelayServer.WebApp.Components.Pages;

public partial class Devices : ComponentBase
{
    //public IQueryable<GetDevice> DevicesList;
    //public List<GetDevice> ListDevice;

    // protected override async Task OnInitializedAsync()
    // {
    //      ListDevice = new List<GetDevice>()
    //      {
    //          new GetDevice()
    //          {
    //              Id = Guid.NewGuid(),
    //              Code = "002",
    //              Name = "John Printer",
    //              Owner = new GetUser(),
    //              DeviceType = new GetDeviceType(),
    //              OsIdentifier = "Printer"
    //          },
    //          new GetDevice()
    //          {
    //              Id = Guid.NewGuid(),
    //              Code = "003",
    //              Name = "John Printer",
    //              Owner = new GetUser(),
    //              DeviceType = new GetDeviceType(),
    //              OsIdentifier = "Printer1"
    //          },
    //          new GetDevice()
    //          {
    //              Id = Guid.NewGuid(),
    //              Code = "003",
    //              Name = "Doe Printer",
    //              Owner = new GetUser(),
    //              DeviceType = new GetDeviceType(),
    //              OsIdentifier = "Printer2"
    //          }
    //      };
    //      DevicesList = ListDevice.AsQueryable();
    //      await base.OnInitializedAsync();
    // }
}
