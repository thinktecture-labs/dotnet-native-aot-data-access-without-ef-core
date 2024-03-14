using System;

namespace WebApp.DatabaseAccess.Model;

public sealed class Address
{
    public Guid Id { get; set; }
    public Guid ContactId { get; set; }
    public string Street { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}