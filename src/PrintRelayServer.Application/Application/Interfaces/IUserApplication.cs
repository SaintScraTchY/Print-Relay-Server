using PrintRelayServer.Shared.Contracts.Base;
using PrintRelayServer.Shared.Contracts.Roles;
using PrintRelayServer.Shared.Contracts.Users;

namespace PrintRelayServer.Application.Application.Interfaces;

public interface IUserApplication
{
    #region Roles

    public Task<BaseResult<IList<GetRole>>> GetRoles();
    //public Task<BaseResult<GetRole>> AddRole(AddRole addRole);
    //public Task<BaseResult<GetRole>> EditRole(Guid roleId, EditRole editRole);
    //public Task<BaseResult<GetRole>> RemoveRole(Guid roleId);

    #endregion

    #region Users

    public Task<BaseResult<PaginatedResult<GetUser>>> GetUsers(int pageNumber,int pageSize);
    public Task<BaseResult<PaginatedResult<GetUser>>> GetUsers(int pageNumber,int pageSize,GetUserFilter getUserFilter);
    public Task<BaseResult<GetUser>> AddUser(AddUser addUser);
    public Task<BaseResult<GetUser>> EditUser(Guid userId,EditUser editUser);
    public Task<BaseResult<GetUser>> RemoveUser(Guid userId);

    #endregion
}