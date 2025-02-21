using Microsoft.AspNetCore.Components;
using PrintRelayServer.Shared.Contracts.Device;
using PrintRelayServer.Shared.Contracts.DeviceType;
using PrintRelayServer.Shared.Contracts.Users;

namespace PrintRelayServer.WebApp.Components.Pages;

public partial class Devices : ComponentBase
{
    protected IQueryable<GetDevice> DevicesList;
     protected override async Task OnInitializedAsync()
     {
          DevicesList =  new List<GetDevice>()
          {
              new GetDevice()
              {
                  Id = Guid.NewGuid(),
                  Code = "002",
                  Name = "John Printer",
                  Owner = new GetUser(),
                  DeviceType = new GetDeviceType(),
                  OsIdentifier = "Printer"
              },
              new GetDevice()
              {
                  Id = Guid.NewGuid(),
                  Code = "003",
                  Name = "John Printer",
                  Owner = new GetUser(),
                  DeviceType = new GetDeviceType(),
                  OsIdentifier = "Printer1"
              },
              new GetDevice()
              {
                  Id = Guid.NewGuid(),
                  Code = "003",
                  Name = "Doe Printer",
                  Owner = new GetUser(),
                  DeviceType = new GetDeviceType(),
                  OsIdentifier = "Printer2"
              }
          }.AsQueryable();
          await base.OnInitializedAsync();
     }
}
