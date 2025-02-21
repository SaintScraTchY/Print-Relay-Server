using PrintRelayServer.Application.Application.Implementations;
using PrintRelayServer.Shared.Contracts.Base;
using PrintRelayServer.Shared.Contracts.Roles;
using PrintRelayServer.Shared.Contracts.Users;

namespace PrintRelayServer.Application.Application.Interfaces;

public class UserApplication : IUserApplication
{
    #region Roles
    public Task<BaseResult<IList<GetRole>>> GetRoles()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult<GetRole>> AddRole(AddRole addRole)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult<GetRole>> EditRole(Guid roleId, EditRole editRole)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult<GetUser>> RemoveRole(Guid roleId)
    {
        throw new NotImplementedException();
    }
    #endregion

     #region Users
    public Task<BaseResult<PaginatedResult<GetUser>>> GetUsers(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult<PaginatedResult<GetUser>>> GetUsers(int pageNumber, int pageSize, GetUserFilter getUserFilter)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult<GetUser>> AddUser(AddUser addUser)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult<GetUser>> EditUser(EditUser editUser)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult<GetUser>> RemoveUser(Guid userId)
    {
        throw new NotImplementedException();
    }
    #endregion

}