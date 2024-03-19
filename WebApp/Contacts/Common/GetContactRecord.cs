using System;

namespace WebApp.Contacts.Common;

public readonly record struct GetContactRecord(
    Guid ContactId,
    string FirstName,
    string LastName,
    string? Email,
    string? Phone,
    Guid? AddressId,
    string? Street,
    string? ZipCode,
    string? City
);