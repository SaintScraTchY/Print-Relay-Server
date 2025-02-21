using System.ComponentModel.DataAnnotations;

namespace PrintRelayServer.Shared.Contracts.Roles;

public class AddRole
{
    [Required]
    public string Title { get; set; }
    public IEnumerable<Guid> Permissions { get; set; }
}

public class EditRole
{
    public string Title { get; set; }
    public IEnumerable<Guid>? AddedPermissions { get; set; }
    public IEnumerable<Guid>? DeletedPermissions { get; set; }
}