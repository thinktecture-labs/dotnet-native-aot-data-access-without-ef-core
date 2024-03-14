using System;
using System.Collections.Generic;

namespace WebApp.DatabaseAccess.Model;

public sealed class Contact
{
    private List<Address>? _addresses;
    
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public List<Address> Addresses
    {
        get => _addresses ??= [];
        set => _addresses = value;
    }
}