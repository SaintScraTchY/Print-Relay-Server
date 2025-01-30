using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.ManagementAgg;

public class ResourceAccess : FullEntity
{
    
}

public enum ResourceAccessType
{
    Receive = 1,
    Send,
    View,
}