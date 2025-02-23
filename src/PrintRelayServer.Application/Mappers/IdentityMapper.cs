using PrintRelayServer.Domain.Entities.Identity;
using PrintRelayServer.Shared.Contracts.Roles;
using PrintRelayServer.Shared.Contracts.Users;
using Riok.Mapperly.Abstractions;

namespace PrintRelayServer.Application.Mappers;

[Mapper]
public static partial class IdentityMapper
{
    public static partial IList<GetRole> MapToGetRoles(IList<AppRole> sources);
    public static partial IList<GetUser> MapToGetUsers(IList<AppUser> sources);
    public static partial GetUser MapToGetUser(AppUser sources);
}