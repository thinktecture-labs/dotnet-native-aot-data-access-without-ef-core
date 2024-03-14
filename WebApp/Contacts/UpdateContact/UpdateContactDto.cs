using System;

namespace WebApp.Contacts.UpdateContact;

public sealed record UpdateContactDto(Guid Id, string FirstName, string LastName, string? Email, string? Phone);