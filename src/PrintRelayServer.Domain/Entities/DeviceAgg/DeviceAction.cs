using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.DeviceAgg;

/// <summary>
/// not Implemented, for Defining Possible Actions of particular Device
/// Example : the option for printing colorless,or etc...
/// </summary>
public class DeviceAction : Entity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }

    #region Navigations
    public ICollection<DeviceType> DeviceTypesType { get; set; }
    #endregion
}