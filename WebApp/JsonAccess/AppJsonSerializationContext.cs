using System.Collections.Generic;
using System.Text.Json.Serialization;
using WebApp.Contacts.GetContacts;
using WebApp.Contacts.UpdateContact;

namespace WebApp.JsonAccess;

[JsonSerializable(typeof(UpdateContactDto))]
[JsonSerializable(typeof(List<ContactListDto>))]
[JsonSerializable(typeof(IDictionary<string, string[]>))]
public sealed partial class AppJsonSerializationContext : JsonSerializerContext;