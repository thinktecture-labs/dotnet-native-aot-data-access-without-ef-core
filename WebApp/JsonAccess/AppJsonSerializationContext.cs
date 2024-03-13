using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApp.JsonAccess;

[JsonSerializable(typeof(IDictionary<string, string[]>))]
public sealed partial class AppJsonSerializationContext : JsonSerializerContext;