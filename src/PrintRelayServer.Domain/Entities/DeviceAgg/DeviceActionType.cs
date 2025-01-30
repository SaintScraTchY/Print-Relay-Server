using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.DeviceAgg;

public class DeviceActionType : Entity<Guid>
{
    #region Navigations
    public DeviceType DeviceType { get; set; }
    public DeviceAction DeviceAction { get; set; }
    #endregion
}