﻿namespace PrintRelayServer.Shared.Contracts.Users;

public class EditUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
}