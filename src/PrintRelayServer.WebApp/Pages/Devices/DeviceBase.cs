using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using PrintRelayServer.Shared.Contracts.Device;
using PrintRelayServer.WebApp.Services;

namespace PrintRelayServer.WebApp.Pages.Devices;

public class DevicesBase : ComponentBase
{
    [Inject] public required IDeviceService DeviceService { get; set; }
    [Inject] public required IDialogService DialogService { get; set; }

    protected List<GetDevice> Devices { get; set; } = new();
    protected bool ShowAddDialog { get; set; }
    protected bool ShowEditDialog { get; set; }
    protected bool IsLoading { get; set; } = true;
    protected GetDevice SelectedDevice { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await LoadDevices();
        }
        finally
        {
            IsLoading = false;
        }
    }

    protected async Task LoadDevices()
    {
        Devices = await DeviceService.GetDevicesAsync();
    }

    protected async Task EditDevice(GetDevice device)
    {
        SelectedDevice = device;
        ShowEditDialog = true;
    }

    protected async Task DeleteDevice(GetDevice device)
    {
        var result = await DialogService.ShowConfirmationAsync(
            "Delete Device",
            $"Are you sure you want to delete {device.Name}?");

        if (result.Result.Equals(DialogResult.Ok<bool>))
        {
            await DeviceService.DeleteDeviceAsync(device.Id);
            await LoadDevices();
        }
    }
}