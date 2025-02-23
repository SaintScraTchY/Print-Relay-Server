using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrintRelayServer.Application.Application.Interfaces;
using PrintRelayServer.Application.Mappers;
using PrintRelayServer.Domain.Entities.Identity;
using PrintRelayServer.Domain.IRepositories.IIDentityRepo;
using PrintRelayServer.Shared.Contracts.Base;
using PrintRelayServer.Shared.Contracts.Roles;
using PrintRelayServer.Shared.Contracts.Users;

namespace PrintRelayServer.Application.Application.Implementations;

public class UserApplication : IUserApplication
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IAppUserRepo _appUserRepo;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IAppRoleRepo _appRoleRepo;
    public UserApplication(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IAppRoleRepo appRoleRepo, IAppUserRepo appUserRepo)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _appRoleRepo = appRoleRepo;
        _appUserRepo = appUserRepo;
    }

    public async Task<BaseResult<IList<GetRole>>> GetRoles()
    {
        return ReturnResult<IList<GetRole>>
            .Success(IdentityMapper.MapToGetRoles(await _roleManager.Roles.ToListAsync()));
    }

    public async Task<BaseResult<PaginatedResult<GetUser>>> GetUsers(int pageNumber, int pageSize)
    {
        var users = await _appUserRepo.GetPaginatedAsync(pageNumber,pageSize,orderBy:x => x.OrderByDescending(d=>d.Id));
        var result = new PaginatedResult<GetUser>
        {
            Results = IdentityMapper.MapToGetUsers(users.Results),
            TotalCount = users.TotalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        return ReturnResult<PaginatedResult<GetUser>>.Success(result);
    }

    public async Task<BaseResult<PaginatedResult<GetUser>>> GetUsers(int pageNumber, int pageSize, GetUserFilter getUserFilter)
    {
        var users = await _appUserRepo.GetPaginatedAsync(pageNumber, pageSize,
            x =>
                (getUserFilter.UserName != null && x.UserName.Contains(getUserFilter.UserName))
                && (getUserFilter.Email != null && x.Email.Contains(getUserFilter.Email))
                && (getUserFilter.Roles != null && x.Roles.Any(ur => getUserFilter.Roles.Contains(ur.Id)))
                && (getUserFilter.FirstName != null && x.FirstName.Contains(getUserFilter.FirstName))
                && (getUserFilter.LastName != null && x.LastName.Contains(getUserFilter.LastName))
            , includes: "Roles"
            , orderBy: x => x.OrderByDescending(d => d.Id));
        
        var result = new PaginatedResult<GetUser>
        {
            Results = IdentityMapper.MapToGetUsers(users.Results),
            TotalCount = users.TotalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        return ReturnResult<PaginatedResult<GetUser>>.Success(result);
    }

    public async Task<BaseResult<GetUser>> AddUser(AddUser addUser)
    {
        if (await _userManager.Users.AnyAsync(x => x.Email == addUser.Email || x.UserName == addUser.UserName))
            return ReturnResult<GetUser>.Error(null,"User already exists");
        
        var createdUser = await _userManager.CreateAsync(new AppUser
        {
            UserName = addUser.UserName,
            Email = addUser.Email,
            FirstName = addUser.FirstName,
            LastName = addUser.LastName,
        }, addUser.Password);
        
        return ReturnResult<GetUser>.Success(null);
    }

    public async Task<BaseResult<GetUser>> EditUser(Guid userId,EditUser editUser)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user == null)
            return ReturnResult<GetUser>.Error(null,"User not found");

        if ((editUser.Email != null || editUser.UserName != null) &&
            await _userManager.Users.AnyAsync(x =>
                (editUser.Email != null && x.Email == editUser.Email) ||
                (editUser.UserName != null && x.UserName == editUser.UserName)))
        {
            return ReturnResult<GetUser>.Error(null,"a User With this Email or UserName already exists");
        }
        
        user.EditUser(editUser.FirstName, editUser.LastName, editUser.Email,editUser.UserName);
        await _userManager.UpdateAsync(user);
        return ReturnResult<GetUser>.Success(IdentityMapper.MapToGetUser(user));
    }

    public async Task<BaseResult<GetUser>> RemoveUser(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user == null)
            return ReturnResult<GetUser>.Error(null,"User not found");

        await _userManager.DeleteAsync(user);
        return ReturnResult<GetUser>.Success(null);
    }
}