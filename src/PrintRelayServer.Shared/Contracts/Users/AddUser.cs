using System.ComponentModel.DataAnnotations;

namespace PrintRelayServer.Shared.Contracts.Users;

public class AddUser
{
    [MinLength(3)]
    [Required]
    public string FirstName { get; set; }
    
    [MinLength(3)]
    [Required]
    public string LastName { get; set; }
    
    [MinLength(3)]
    [Required]
    public string UserName { get; set; }
    
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    
    [MinLength(6)]
    [Required]
    public string Password { get; set; }
}